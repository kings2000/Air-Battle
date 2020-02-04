using UnityEngine;

public interface InputSignalListener
{
    /// <summary>
    /// Detects when the mouse button was clicked or finger touches the screen
    /// </summary>
    void OnInputDown(Vector3 pointerPosition = new Vector3());
    /// <summary>
    /// Detects when the mouse button is released or when the finger is released
    /// </summary>
    void OnInputUp(Vector3 pointerPosition = new Vector3());
    /// <summary>
    /// Detecs when the mouse button is held down or when the finger is touching the screen
    /// </summary>
    void OnInput(Vector3 pointerPosition = new Vector3());
}
