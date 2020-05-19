// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""11cd0525-6f1c-43e8-8b65-aee168937410"",
            ""actions"": [
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""282de54b-c278-479c-afea-cca2013d6dcd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftStick"",
                    ""type"": ""Value"",
                    ""id"": ""30de9449-ed07-48a8-b8df-db104c962a2e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightStick"",
                    ""type"": ""Value"",
                    ""id"": ""a9133877-4cd9-47f2-abda-3a8090542882"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""X"",
                    ""type"": ""Value"",
                    ""id"": ""b6c56a5b-c069-42e9-bbab-cee6418133a0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Y"",
                    ""type"": ""Button"",
                    ""id"": ""70c97e69-9e74-4749-8fed-54f015a23acf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""A"",
                    ""type"": ""Button"",
                    ""id"": ""cb5baa83-a710-4279-8035-1b802940f819"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""B"",
                    ""type"": ""Button"",
                    ""id"": ""a90254f5-5ada-48f5-8ed5-30c8b50cc425"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftTrigger"",
                    ""type"": ""Button"",
                    ""id"": ""ffc87bfa-ff48-4196-b91e-ae73c7a2c74b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftBumper"",
                    ""type"": ""Button"",
                    ""id"": ""16e74dcf-bda0-440e-bd03-c61cd6ec68c0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightTrigger"",
                    ""type"": ""Button"",
                    ""id"": ""79b480a2-d4de-4770-bb42-8e8b2f106d52"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightBumper"",
                    ""type"": ""Button"",
                    ""id"": ""6ed38f16-3fce-41f3-8f54-69a51246f6ad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""b8306eac-b238-43b7-9e41-6b90073dc037"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1353a464-e231-4a27-aba4-8d6ef3702950"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""338a3d40-7e1f-4662-94b4-30928c2a971b"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8a98eec-f89c-4150-8c46-2045a0bed550"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a4d1571-3061-48f8-a465-512f9474e48f"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef50db08-eb3a-4ada-9e62-86868af82751"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b738355-d13b-4b87-b849-15e8bfb9da5e"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3146e25-e4e7-4aa5-8e29-df690e58e866"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82406ab2-f236-4bda-97db-ba1e2b46ea31"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b0edec6-da25-4ff7-b6a2-d47a94b1c33b"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""56e14a90-05c7-432e-b62d-d68345792949"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftBumper"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85d3a581-7d3a-4f42-8e5e-8903259932e8"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4043f8f7-1fd1-489d-bfbc-ae8f2d174285"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightBumper"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6cf1f25e-8e7b-450a-9b01-c076fc1bf86d"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Start = m_Gameplay.FindAction("Start", throwIfNotFound: true);
        m_Gameplay_LeftStick = m_Gameplay.FindAction("LeftStick", throwIfNotFound: true);
        m_Gameplay_RightStick = m_Gameplay.FindAction("RightStick", throwIfNotFound: true);
        m_Gameplay_X = m_Gameplay.FindAction("X", throwIfNotFound: true);
        m_Gameplay_Y = m_Gameplay.FindAction("Y", throwIfNotFound: true);
        m_Gameplay_A = m_Gameplay.FindAction("A", throwIfNotFound: true);
        m_Gameplay_B = m_Gameplay.FindAction("B", throwIfNotFound: true);
        m_Gameplay_LeftTrigger = m_Gameplay.FindAction("LeftTrigger", throwIfNotFound: true);
        m_Gameplay_LeftBumper = m_Gameplay.FindAction("LeftBumper", throwIfNotFound: true);
        m_Gameplay_RightTrigger = m_Gameplay.FindAction("RightTrigger", throwIfNotFound: true);
        m_Gameplay_RightBumper = m_Gameplay.FindAction("RightBumper", throwIfNotFound: true);
        m_Gameplay_Select = m_Gameplay.FindAction("Select", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Start;
    private readonly InputAction m_Gameplay_LeftStick;
    private readonly InputAction m_Gameplay_RightStick;
    private readonly InputAction m_Gameplay_X;
    private readonly InputAction m_Gameplay_Y;
    private readonly InputAction m_Gameplay_A;
    private readonly InputAction m_Gameplay_B;
    private readonly InputAction m_Gameplay_LeftTrigger;
    private readonly InputAction m_Gameplay_LeftBumper;
    private readonly InputAction m_Gameplay_RightTrigger;
    private readonly InputAction m_Gameplay_RightBumper;
    private readonly InputAction m_Gameplay_Select;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Start => m_Wrapper.m_Gameplay_Start;
        public InputAction @LeftStick => m_Wrapper.m_Gameplay_LeftStick;
        public InputAction @RightStick => m_Wrapper.m_Gameplay_RightStick;
        public InputAction @X => m_Wrapper.m_Gameplay_X;
        public InputAction @Y => m_Wrapper.m_Gameplay_Y;
        public InputAction @A => m_Wrapper.m_Gameplay_A;
        public InputAction @B => m_Wrapper.m_Gameplay_B;
        public InputAction @LeftTrigger => m_Wrapper.m_Gameplay_LeftTrigger;
        public InputAction @LeftBumper => m_Wrapper.m_Gameplay_LeftBumper;
        public InputAction @RightTrigger => m_Wrapper.m_Gameplay_RightTrigger;
        public InputAction @RightBumper => m_Wrapper.m_Gameplay_RightBumper;
        public InputAction @Select => m_Wrapper.m_Gameplay_Select;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Start.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnStart;
                @LeftStick.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftStick;
                @LeftStick.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftStick;
                @LeftStick.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftStick;
                @RightStick.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightStick;
                @RightStick.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightStick;
                @RightStick.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightStick;
                @X.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnX;
                @X.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnX;
                @X.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnX;
                @Y.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnY;
                @Y.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnY;
                @Y.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnY;
                @A.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnA;
                @A.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnA;
                @A.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnA;
                @B.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnB;
                @B.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnB;
                @B.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnB;
                @LeftTrigger.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftTrigger;
                @LeftTrigger.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftTrigger;
                @LeftTrigger.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftTrigger;
                @LeftBumper.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftBumper;
                @LeftBumper.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftBumper;
                @LeftBumper.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLeftBumper;
                @RightTrigger.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightTrigger;
                @RightTrigger.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightTrigger;
                @RightTrigger.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightTrigger;
                @RightBumper.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightBumper;
                @RightBumper.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightBumper;
                @RightBumper.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRightBumper;
                @Select.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelect;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
                @LeftStick.started += instance.OnLeftStick;
                @LeftStick.performed += instance.OnLeftStick;
                @LeftStick.canceled += instance.OnLeftStick;
                @RightStick.started += instance.OnRightStick;
                @RightStick.performed += instance.OnRightStick;
                @RightStick.canceled += instance.OnRightStick;
                @X.started += instance.OnX;
                @X.performed += instance.OnX;
                @X.canceled += instance.OnX;
                @Y.started += instance.OnY;
                @Y.performed += instance.OnY;
                @Y.canceled += instance.OnY;
                @A.started += instance.OnA;
                @A.performed += instance.OnA;
                @A.canceled += instance.OnA;
                @B.started += instance.OnB;
                @B.performed += instance.OnB;
                @B.canceled += instance.OnB;
                @LeftTrigger.started += instance.OnLeftTrigger;
                @LeftTrigger.performed += instance.OnLeftTrigger;
                @LeftTrigger.canceled += instance.OnLeftTrigger;
                @LeftBumper.started += instance.OnLeftBumper;
                @LeftBumper.performed += instance.OnLeftBumper;
                @LeftBumper.canceled += instance.OnLeftBumper;
                @RightTrigger.started += instance.OnRightTrigger;
                @RightTrigger.performed += instance.OnRightTrigger;
                @RightTrigger.canceled += instance.OnRightTrigger;
                @RightBumper.started += instance.OnRightBumper;
                @RightBumper.performed += instance.OnRightBumper;
                @RightBumper.canceled += instance.OnRightBumper;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnStart(InputAction.CallbackContext context);
        void OnLeftStick(InputAction.CallbackContext context);
        void OnRightStick(InputAction.CallbackContext context);
        void OnX(InputAction.CallbackContext context);
        void OnY(InputAction.CallbackContext context);
        void OnA(InputAction.CallbackContext context);
        void OnB(InputAction.CallbackContext context);
        void OnLeftTrigger(InputAction.CallbackContext context);
        void OnLeftBumper(InputAction.CallbackContext context);
        void OnRightTrigger(InputAction.CallbackContext context);
        void OnRightBumper(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
    }
}
