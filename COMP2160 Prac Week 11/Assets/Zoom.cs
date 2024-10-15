using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Zoom : MonoBehaviour
{
    private Actions actions;
    private InputAction zoomAction;

    private void Awake()
    {
        actions = new Actions();
        zoomAction = actions.camera.zoom;
    }

    void OnEnable()
    {
        actions.camera.Enable();
    }

    void OnDisable()
    {
        actions.camera.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float zoomValue = zoomAction.ReadValue<float>();

        if (Camera.main.orthographic == false)
        {
            if (zoomValue > 0 && Camera.main.fieldOfView < 60)
            {
                Camera.main.fieldOfView += 1;
            }

            if (zoomValue < 0 && Camera.main.fieldOfView > 15)
            {
                Camera.main.fieldOfView -= 1;
            }
        } else
        {
            if (zoomValue > 0 && Camera.main.orthographicSize < 5)
            {
                Camera.main.orthographicSize += 0.1f;
            }

            if (zoomValue < 0 && Camera.main.orthographicSize > 1.5)
            {
                Camera.main.orthographicSize -= 0.1f;
            }
        }

        
    }
}
