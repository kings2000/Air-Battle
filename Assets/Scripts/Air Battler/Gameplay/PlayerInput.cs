using System.Collections;
using System.Collections.Generic;
using UnityEngine;

delegate void OnInputDown(Vector3 pointerPosition);
delegate void OnInputUp(Vector3 pointerPosition);
delegate void OnInput(Vector3 pointerPosition);

public static class PlayerInput
{
    static OnInputDown onInputDown;
    static OnInputUp onInputUp;
    static OnInput onInput;

    /// <summary>
    /// Connects the listener to listen for mouse input
    /// </summary>
    /// <param name="listener"></param>
    public static void Connect(InputSignalListener listener)
    {
        onInputDown += listener.OnInputDown;
        onInputUp += listener.OnInputUp;
        onInput += listener.OnInput;
    }

    /// <summary>
    /// Disconnect the listener to stop reciveing mouse input
    /// </summary>
    /// <param name="listener"></param>
    public static void DisConnect(InputSignalListener listener)
    {
        onInputDown -= listener.OnInputDown;
        onInputUp -= listener.OnInputUp;
        onInput -= listener.OnInput;
    }
    
    
    public static void Update()
    {
        DetectMouse();
        DetectTouch();
    }

    /// <summary>
    /// Use for mouse input detection (Mostly for development)
    /// </summary>
    static void DetectMouse()
    {
        if (GameManager.isMobile) return;
        if (Input.GetMouseButtonDown(0))
        {
            onInputDown?.Invoke(Input.mousePosition);
        }else if (Input.GetMouseButtonUp(0))
        {
            onInputUp?.Invoke(Input.mousePosition);
        }else if (Input.GetMouseButton(0))
        {
            onInput?.Invoke(Input.mousePosition);
        }
    }

    /// <summary>
    /// Detect the player touch input (first touch)
    /// </summary>
    static void DetectTouch()
    {

        if (Input.touchCount > 0 && GameManager.isMobile)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                onInputDown?.Invoke(touch.position);
            }else if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                onInput?.Invoke(touch.position);
                
            }else if(touch.phase == TouchPhase.Ended)
            {
                onInputUp?.Invoke(touch.position);
            }

        }
    }
}
