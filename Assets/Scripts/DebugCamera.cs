using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCamera : MonoBehaviour
{
    public float speed = 50;
    Vector3 momentum = Vector3.zero;
    float turbo = 1f;

    public float angularSpeed = 45f;

    bool controlsActive = false;

    float fps = 0;
    [Header("Debug")]
    public bool showFps = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controlsActive = true;
    }

    void Update()
    {
        if (controlsActive)
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            float d = 0;
            if(Input.GetKey(KeyCode.Space)){
                d = 1;
            }
            else if(Input.GetKey(KeyCode.LeftControl)){
                d = -1;
            }

            Vector3 trajectory = new Vector3(h, d, v).normalized;

            transform.Translate(trajectory * speed * Time.deltaTime);

            /* LOOK */
            float pitch = -Input.GetAxis("Mouse Y");
            float yaw = Input.GetAxis("Mouse X");
            float roll = Input.GetAxis("Mouse ScrollWheel")*100;

            Vector3 rotation = new Vector3(yaw, pitch, roll);

            transform.Rotate((Vector3.up * rotation.x + transform.right * rotation.y + transform.forward * rotation.z) * angularSpeed * Time.deltaTime, Space.World);
        }

        /* UTIL */

        // Toggle cursor lock and control activation with tab
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0))
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
        if (showFps)
        {
            fps = Mathf.Round((fps + Mathf.Round(1f / Time.deltaTime)) * 0.5f);
        }
    }

    void OnGUI(){
        // Render FPS to GUI
        GUIStyle style = new GUIStyle();
        style.clipping = TextClipping.Overflow;
        if (showFps)
        {
            GUI.Label(new Rect(32, 32, 100, 10), fps + "fps", style);
        }
    }
}
