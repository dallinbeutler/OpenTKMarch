using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace OpenTKmarch
{
    class Camera
    {
       public  enum Camera_Movement
        {
            FORWARD,
            BACKWARD,
            LEFT,
            RIGHT
        }

        // Default camera values
        const float YAW = -90.0f;
        const float PITCH = 0.0f;
        const float SPEED = 2.5f;
        const float SENSITIVITY = 0.3f;
        const float ZOOM = 45.0f;

        Camera attributes;
        public Vector3 Position, Front, Up, Right, WorldUp;

        //Euler Angles
        public float Yaw, Pitch;

        //camera options
        public float MovementSpeed, Zoom, aspectRatio;
        public float MouseSensitivity = .2f;

        public const string vertexShaderSource =
@"
#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoord;

out vec2 TexCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
	gl_Position = projection * view * model * vec4(aPos, 1.0f);
	TexCoord = vec2(aTexCoord.x, aTexCoord.y);
}
";
        public const string fragmentShaderSource =
    @"
#version 330 core
out vec4 FragColor;

in vec2 TexCoord;

// texture samplers
uniform sampler2D texture1;
uniform sampler2D texture2;

void main()
{
	// linearly interpolate between both textures (80% container, 20% awesomeface)
    vec4 t0 = texture2D(texture1, TexCoord);
    vec4 t1 = texture2D(texture2, TexCoord);
	FragColor = mix(t0, t1, t1.a);// + vec4(0.0f,0.0f,0.0f,1.0f);
}
";

        public ShaderProgram shaderProgram = 
            new ShaderProgram(vertexShaderSource,
                              fragmentShaderSource, 
                              "model",
                              "view",
                              "projection",
                              "texture1",
                              "texture2");

        public Camera(float width, float height, Vector3? position = null, Vector3? up = null, float yaw = YAW, float pitch = PITCH)
        {
            aspectRatio = width / height;
            Position = position.HasValue? position.Value : new Vector3();
            WorldUp = up.HasValue ? up.Value : new Vector3(0f, 1f, 0f);
            Yaw = yaw;
            Pitch = pitch;
            updateCameraVectors();
            MovementSpeed = .001f;
        }

        // Constructor with scalar values
        Camera(float posX, float posY, float posZ, float upX, float upY, float upZ, float yaw, float pitch)
        {
            Position = new Vector3(posX, posY, posZ);
            WorldUp = new Vector3(upX, upY, upZ);
            Yaw = yaw;
            Pitch = pitch;
            updateCameraVectors();
        }

        // Returns the view matrix calculated using Euler Angles and the LookAt Matrix
        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + Front, Up);
        }

        // Processes input received from any keyboard-like input system. Accepts input parameter in the form of camera defined ENUM (to abstract it from windowing systems)
        public void ProcessKeyboard(Camera_Movement direction, float deltaTime)
        {
            float velocity = MovementSpeed * deltaTime;
            if (direction == Camera_Movement.FORWARD)
                Position += Front * velocity;
            if (direction == Camera_Movement.BACKWARD)
                Position -= Front * velocity;
            if (direction == Camera_Movement.LEFT)
                Position -= Right * velocity;
            if (direction == Camera_Movement.RIGHT)
                Position += Right * velocity;
        }

        // Processes input received from a mouse input system. Expects the offset value in both the x and y direction.
        public void ProcessMouseMovement(float xoffset, float yoffset, bool constrainPitch = true)
        {
            xoffset *= MouseSensitivity;
            yoffset *= MouseSensitivity;

            Yaw += xoffset;
            Pitch += -yoffset;

            // Make sure that when pitch is out of bounds, screen doesn't get flipped
            if (constrainPitch)
            {
                if (Pitch > 89.0f)
                    Pitch = 89.0f;
                if (Pitch < -89.0f)
                    Pitch = -89.0f;
            }

            // Update Front, Right and Up Vectors using the updated Euler angles
            updateCameraVectors();
        }

        // Processes input received from a mouse scroll-wheel event. Only requires input on the vertical wheel-axis
        void ProcessMouseScroll(float yoffset)
        {
            if (Zoom >= 1.0f && Zoom <= 45.0f)
                Zoom -= yoffset;
            if (Zoom <= 1.0f)
                Zoom = 1.0f;
            if (Zoom >= 45.0f)
                Zoom = 45.0f;
        }


    // Calculates the front vector from the Camera's (updated) Euler Angles
    void updateCameraVectors()
        {
            // Calculate the new Front vector
            double pitchRad = Pitch.toRadD();
            double yawRad = Yaw.toRadD();
            var pitchCos = Math.Cos(pitchRad);
            Math.Cos(3f);
            float x = (float)(Math.Cos(yawRad) * pitchCos);
            float y = (float)Math.Sin(pitchRad);
            float z = (float)(Math.Sin(yawRad) * pitchCos);

            Vector3 front = new Vector3(x,y,z);

            Front = front.Normalized();
            
            // Also re-calculate the Right and Up vector
            Right = Vector3.Normalize(Vector3.Cross(Front, WorldUp));  // Normalize the vectors, because their length gets closer to 0 the more you look up or down which results in slower movement.
            Up = Vector3.Normalize(Vector3.Cross(Right, Front));
        }

     void render()
        {
            shaderProgram.Use();
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(Yaw, aspectRatio, 0, 2000);//Perspective(Zoom, aspectRatio, 0f, 1000);
            shaderProgram.SetMat4("projection",ref projection);

            Matrix4 view = this.GetViewMatrix();
            shaderProgram.SetMat4("view",ref view);

            
        }
    }
}
