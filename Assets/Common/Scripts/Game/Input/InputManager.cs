


using FletcherLibraries;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    public InputMappings InputMappings;

    #if UNITY_EDITOR
    void OnValidate() {
        InputMappings = new();
        foreach (var action in InputSystem.actions) {
            if (!InputMappings.ContainsKey(action.name)) {
                InputMappings.Add(action.name, new InputMapping(InputActionReference.Create(action), null));
            }
        }
    }
    #endif

}