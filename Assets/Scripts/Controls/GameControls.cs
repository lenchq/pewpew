//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/DesktopControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @GameControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DesktopControls"",
    ""maps"": [
        {
            ""name"": ""Mouse"",
            ""id"": ""832d8d58-30b8-4a12-a555-521aa622fa65"",
            ""actions"": [
                {
                    ""name"": ""Pan"",
                    ""type"": ""PassThrough"",
                    ""id"": ""aaa09074-ba63-4ab5-8bcc-5c543de40a7b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1dd56514-7fa7-44f3-bdaf-0b56e52046af"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Drag"",
                    ""type"": ""PassThrough"",
                    ""id"": ""22fa2704-04de-4dc4-bcda-c34b6f2fd485"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""c9ac380a-3ee2-4670-bfef-2fede6958311"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""51e4ffa6-ecf7-41b8-b10b-4332805f14d9"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c1940ee-507c-4731-b8df-7a61c48b9fd4"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""29ed72a6-5690-4e19-8c72-717a8c02417f"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02e4562c-d738-4287-a074-ba21f19f44df"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KM"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Touchscreen"",
            ""id"": ""fa88b748-eae6-49ad-a4a7-f2330d2ef3b3"",
            ""actions"": [
                {
                    ""name"": ""Tap"",
                    ""type"": ""Button"",
                    ""id"": ""192bebbc-6932-4ad6-bb6b-8ee04f0fb25d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""24bde037-84e5-457f-8516-ca76ad522153"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""touch"",
                    ""action"": ""Tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""id"": ""6ca69616-1706-4b58-8f2b-a432a3225234"",
            ""actions"": [
                {
                    ""name"": ""EscapeKey"",
                    ""type"": ""Button"",
                    ""id"": ""4d320367-9777-45c5-aff1-eb0908e25373"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cf0ae126-d76e-4d03-a10a-912b590c32e4"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KM;touch"",
                    ""action"": ""EscapeKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KM"",
            ""bindingGroup"": ""KM"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""touch"",
            ""bindingGroup"": ""touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_Pan = m_Mouse.FindAction("Pan", throwIfNotFound: true);
        m_Mouse_Zoom = m_Mouse.FindAction("Zoom", throwIfNotFound: true);
        m_Mouse_Drag = m_Mouse.FindAction("Drag", throwIfNotFound: true);
        m_Mouse_Click = m_Mouse.FindAction("Click", throwIfNotFound: true);
        // Touchscreen
        m_Touchscreen = asset.FindActionMap("Touchscreen", throwIfNotFound: true);
        m_Touchscreen_Tap = m_Touchscreen.FindAction("Tap", throwIfNotFound: true);
        // Keyboard
        m_Keyboard = asset.FindActionMap("Keyboard", throwIfNotFound: true);
        m_Keyboard_EscapeKey = m_Keyboard.FindAction("EscapeKey", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Mouse
    private readonly InputActionMap m_Mouse;
    private IMouseActions m_MouseActionsCallbackInterface;
    private readonly InputAction m_Mouse_Pan;
    private readonly InputAction m_Mouse_Zoom;
    private readonly InputAction m_Mouse_Drag;
    private readonly InputAction m_Mouse_Click;
    public struct MouseActions
    {
        private @GameControls m_Wrapper;
        public MouseActions(@GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pan => m_Wrapper.m_Mouse_Pan;
        public InputAction @Zoom => m_Wrapper.m_Mouse_Zoom;
        public InputAction @Drag => m_Wrapper.m_Mouse_Drag;
        public InputAction @Click => m_Wrapper.m_Mouse_Click;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void SetCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterface != null)
            {
                @Pan.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnPan;
                @Pan.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnPan;
                @Pan.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnPan;
                @Zoom.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnZoom;
                @Drag.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnDrag;
                @Drag.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnDrag;
                @Drag.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnDrag;
                @Click.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnClick;
            }
            m_Wrapper.m_MouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pan.started += instance.OnPan;
                @Pan.performed += instance.OnPan;
                @Pan.canceled += instance.OnPan;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @Drag.started += instance.OnDrag;
                @Drag.performed += instance.OnDrag;
                @Drag.canceled += instance.OnDrag;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
            }
        }
    }
    public MouseActions @Mouse => new MouseActions(this);

    // Touchscreen
    private readonly InputActionMap m_Touchscreen;
    private ITouchscreenActions m_TouchscreenActionsCallbackInterface;
    private readonly InputAction m_Touchscreen_Tap;
    public struct TouchscreenActions
    {
        private @GameControls m_Wrapper;
        public TouchscreenActions(@GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Tap => m_Wrapper.m_Touchscreen_Tap;
        public InputActionMap Get() { return m_Wrapper.m_Touchscreen; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TouchscreenActions set) { return set.Get(); }
        public void SetCallbacks(ITouchscreenActions instance)
        {
            if (m_Wrapper.m_TouchscreenActionsCallbackInterface != null)
            {
                @Tap.started -= m_Wrapper.m_TouchscreenActionsCallbackInterface.OnTap;
                @Tap.performed -= m_Wrapper.m_TouchscreenActionsCallbackInterface.OnTap;
                @Tap.canceled -= m_Wrapper.m_TouchscreenActionsCallbackInterface.OnTap;
            }
            m_Wrapper.m_TouchscreenActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Tap.started += instance.OnTap;
                @Tap.performed += instance.OnTap;
                @Tap.canceled += instance.OnTap;
            }
        }
    }
    public TouchscreenActions @Touchscreen => new TouchscreenActions(this);

    // Keyboard
    private readonly InputActionMap m_Keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_Keyboard_EscapeKey;
    public struct KeyboardActions
    {
        private @GameControls m_Wrapper;
        public KeyboardActions(@GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @EscapeKey => m_Wrapper.m_Keyboard_EscapeKey;
        public InputActionMap Get() { return m_Wrapper.m_Keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @EscapeKey.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnEscapeKey;
                @EscapeKey.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnEscapeKey;
                @EscapeKey.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnEscapeKey;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @EscapeKey.started += instance.OnEscapeKey;
                @EscapeKey.performed += instance.OnEscapeKey;
                @EscapeKey.canceled += instance.OnEscapeKey;
            }
        }
    }
    public KeyboardActions @Keyboard => new KeyboardActions(this);
    private int m_KMSchemeIndex = -1;
    public InputControlScheme KMScheme
    {
        get
        {
            if (m_KMSchemeIndex == -1) m_KMSchemeIndex = asset.FindControlSchemeIndex("KM");
            return asset.controlSchemes[m_KMSchemeIndex];
        }
    }
    private int m_touchSchemeIndex = -1;
    public InputControlScheme touchScheme
    {
        get
        {
            if (m_touchSchemeIndex == -1) m_touchSchemeIndex = asset.FindControlSchemeIndex("touch");
            return asset.controlSchemes[m_touchSchemeIndex];
        }
    }
    public interface IMouseActions
    {
        void OnPan(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnDrag(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
    }
    public interface ITouchscreenActions
    {
        void OnTap(InputAction.CallbackContext context);
    }
    public interface IKeyboardActions
    {
        void OnEscapeKey(InputAction.CallbackContext context);
    }
}