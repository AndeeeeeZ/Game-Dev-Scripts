using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/*
* Simple restart helper that reloads the current scene
* Need to be using Unity's new input system
* NOTE: need to adjust the script based on the name of the input action script and the input map
*/

public class RestartHelper : MonoBehaviour
{
    public UnityEvent ReloadCurrentScene; 
    private PlayerInput input;

    private void Awake()
    {
        input = new PlayerInput(); 
    }

    private void OnEnable()
    {
        input.Enable(); 
        input.Scene.Restart.performed += ReloadScene; 
    }

    private void OnDisable()
    {
        input.Scene.Restart.performed -= ReloadScene; 
        input.Disable(); 
    }

    public void ReloadScene(InputAction.CallbackContext context)
    {
        ReloadCurrentScene?.Invoke(); 
    }
}