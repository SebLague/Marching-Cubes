using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCamera : MonoBehaviour
{
    public float speed = 5;
    public float acceleration = 0.5f;
    Vector3 momentum = Vector3.zero;
    float turbo = 1f;

    public float angularSpeed = 45f;
    public float angularAcceleration = 0.5f;
    Vector2 angularMomentum = Vector2.zero;

    bool controlsActive = false;

    float fps = 0;

    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Set controls to active
        controlsActive = true;
    }

    void Update()
    {
        if (controlsActive)
        {
            /* MOVEMENT */

            // Get user input
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            float d = 0;
            if(Input.GetKey(KeyCode.Space)){
                d = 1;
            }
            else if(Input.GetKey(KeyCode.LeftControl)){
                d = -1;
            }

            // Convert user input to a normalized 3D vector
            Vector3 trajectory = new Vector3(h, d, v).normalized;

            // Check for 'turbo' button and gradually increase or decrease turbo modifier accordingly
            if (Input.GetKey(KeyCode.LeftShift))
            {
                turbo = Mathf.MoveTowards(turbo, 2f, Time.deltaTime * turbo);
            }
            else
            {
                turbo = Mathf.MoveTowards(turbo, 1f, Time.deltaTime * turbo);
            }

            // Set momentum by lerping to trajectory multiplied by desired speed and turbo
            momentum = Vector3.Lerp(momentum, trajectory * speed * turbo, Time.deltaTime * acceleration * 10f);

            // Move the camera
            transform.Translate(momentum * Time.deltaTime);

            /* LOOK */

            // Get user input
            float pitch = -Input.GetAxis("Mouse Y");
            float yaw = Input.GetAxis("Mouse X");

            // Reverse yaw when upside down for more intuitive input
            if (Mathf.Abs(transform.eulerAngles.z - 180) <= 1f)
            {
                yaw *= -1f;
            }

            // Convert user input to a 2D vector
            Vector2 rotation = new Vector2(yaw, pitch);

            // Set angularMomentum by lerping to rotation
            angularMomentum = Vector2.Lerp(angularMomentum, rotation, Time.deltaTime * angularAcceleration * 10f);

            // Rotate the camera
            transform.Rotate((Vector3.up * angularMomentum.x + transform.right * angularMomentum.y) * angularSpeed * Time.deltaTime, Space.World);
        }

        /* UTIL */

        // Toggle cursor lock and control activation with tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                controlsActive = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                controlsActive = true;
            }
        }

        // Calculate frames per second
        fps = Mathf.Round((fps + Mathf.Round(1f / Time.deltaTime)) * 0.5f);
    }

    void OnGUI(){
        // Render FPS to GUI
        GUIStyle style = new GUIStyle();
        style.clipping = TextClipping.Overflow;
        GUI.Label(new Rect(32,32,100,10), fps + "fps", style);
    }
}
