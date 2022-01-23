using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class RedirDeltaInputAction : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }

    public @RedirDeltaInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerAction"",
            ""id"": ""6ac10a22-eb66-4817-977c-7df08d1d3e6d"",
            ""actions"": [
                {
                    ""name"": ""TouchDragLook"",
                    ""type"": ""Value"",
                    ""id"": ""a08754a1-3e3c-4a25-ba1b-4b2e50362070"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""00e0e8af-dc3a-428d-8f9d-459f6fe00707"",
                    ""path"": ""<Touchscreen>/touch0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchDragLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");

    }





    IEnumerable<InputBinding> IInputActionCollection2.bindings => throw new NotImplementedException();

    InputBinding? IInputActionCollection.bindingMask { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    ReadOnlyArray<InputDevice>? IInputActionCollection.devices { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    ReadOnlyArray<InputControlScheme> IInputActionCollection.controlSchemes => throw new NotImplementedException();

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    bool IInputActionCollection.Contains(InputAction action)
    {
        throw new NotImplementedException();
    }

    void IInputActionCollection.Disable()
    {
        throw new NotImplementedException();
    }

    void IInputActionCollection.Enable()
    {
        throw new NotImplementedException();
    }

    InputAction IInputActionCollection2.FindAction(string actionNameOrId, bool throwIfNotFound)
    {
        throw new NotImplementedException();
    }

    int IInputActionCollection2.FindBinding(InputBinding mask, out InputAction action)
    {
        throw new NotImplementedException();
    }

    IEnumerator<InputAction> IEnumerable<InputAction>.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }


}
