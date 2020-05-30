// GENERATED AUTOMATICALLY FROM 'Assets/Players/Barbarian/BarbarianInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputBarbarian : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputBarbarian()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""BarbarianInput"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""79679229-c408-4686-ad18-fa603c5624dd"",
            ""actions"": [
                {
                    ""name"": ""Attack"",
                    ""type"": ""PassThrough"",
                    ""id"": ""21cad49e-15ab-4af9-8afa-d5186838ca75"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6a65677f-1c92-4596-a141-cce09ffe2820"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""50434db2-6977-4561-baf4-9c872de933ad"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ability1"",
                    ""type"": ""PassThrough"",
                    ""id"": ""275d5336-7532-4e0b-8216-bbf73d9312f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Ability2"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cfe266f8-d7ec-43f8-b6ea-0d599bd04c3e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Ability3"",
                    ""type"": ""PassThrough"",
                    ""id"": ""516ae892-ecc6-48ce-8467-5bee52953254"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Ability4"",
                    ""type"": ""PassThrough"",
                    ""id"": ""bcd281e9-c22e-4a95-b5cd-fad4bd5c3696"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""PauseMenu"",
                    ""type"": ""PassThrough"",
                    ""id"": ""220e7907-dab9-4a4e-9141-5ae06573c94d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""af090248-ab4a-4e07-a833-3d09e85c15bb"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d4feb5e-0cbe-45ee-ba88-3689fb65db03"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b7579148-a289-44c0-a530-b647d4b69cb6"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""286a20ad-f8f0-48d5-b4a9-dbb1546ddc81"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Ability1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ce9a190-bd42-403e-a4ba-a04b2837391c"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Ability2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9111c448-d22b-4868-adfc-cdcc9dd39b45"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Ability3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fbc6ee90-43b2-4566-99c4-19402d6542db"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Ability4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6807b0c5-e758-4fcf-a1a2-442398cf3677"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Attack = m_Gameplay.FindAction("Attack", throwIfNotFound: true);
        m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
        m_Gameplay_Look = m_Gameplay.FindAction("Look", throwIfNotFound: true);
        m_Gameplay_Ability1 = m_Gameplay.FindAction("Ability1", throwIfNotFound: true);
        m_Gameplay_Ability2 = m_Gameplay.FindAction("Ability2", throwIfNotFound: true);
        m_Gameplay_Ability3 = m_Gameplay.FindAction("Ability3", throwIfNotFound: true);
        m_Gameplay_Ability4 = m_Gameplay.FindAction("Ability4", throwIfNotFound: true);
        m_Gameplay_PauseMenu = m_Gameplay.FindAction("PauseMenu", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_Attack;
    private readonly InputAction m_Gameplay_Movement;
    private readonly InputAction m_Gameplay_Look;
    private readonly InputAction m_Gameplay_Ability1;
    private readonly InputAction m_Gameplay_Ability2;
    private readonly InputAction m_Gameplay_Ability3;
    private readonly InputAction m_Gameplay_Ability4;
    private readonly InputAction m_Gameplay_PauseMenu;
    public struct GameplayActions
    {
        private @InputBarbarian m_Wrapper;
        public GameplayActions(@InputBarbarian wrapper) { m_Wrapper = wrapper; }
        public InputAction @Attack => m_Wrapper.m_Gameplay_Attack;
        public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
        public InputAction @Look => m_Wrapper.m_Gameplay_Look;
        public InputAction @Ability1 => m_Wrapper.m_Gameplay_Ability1;
        public InputAction @Ability2 => m_Wrapper.m_Gameplay_Ability2;
        public InputAction @Ability3 => m_Wrapper.m_Gameplay_Ability3;
        public InputAction @Ability4 => m_Wrapper.m_Gameplay_Ability4;
        public InputAction @PauseMenu => m_Wrapper.m_Gameplay_PauseMenu;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Attack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                @Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Look.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Ability1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility1;
                @Ability1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility1;
                @Ability1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility1;
                @Ability2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility2;
                @Ability2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility2;
                @Ability2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility2;
                @Ability3.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility3;
                @Ability3.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility3;
                @Ability3.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility3;
                @Ability4.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility4;
                @Ability4.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility4;
                @Ability4.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAbility4;
                @PauseMenu.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPauseMenu;
                @PauseMenu.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPauseMenu;
                @PauseMenu.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPauseMenu;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Ability1.started += instance.OnAbility1;
                @Ability1.performed += instance.OnAbility1;
                @Ability1.canceled += instance.OnAbility1;
                @Ability2.started += instance.OnAbility2;
                @Ability2.performed += instance.OnAbility2;
                @Ability2.canceled += instance.OnAbility2;
                @Ability3.started += instance.OnAbility3;
                @Ability3.performed += instance.OnAbility3;
                @Ability3.canceled += instance.OnAbility3;
                @Ability4.started += instance.OnAbility4;
                @Ability4.performed += instance.OnAbility4;
                @Ability4.canceled += instance.OnAbility4;
                @PauseMenu.started += instance.OnPauseMenu;
                @PauseMenu.performed += instance.OnPauseMenu;
                @PauseMenu.canceled += instance.OnPauseMenu;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnAttack(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnAbility1(InputAction.CallbackContext context);
        void OnAbility2(InputAction.CallbackContext context);
        void OnAbility3(InputAction.CallbackContext context);
        void OnAbility4(InputAction.CallbackContext context);
        void OnPauseMenu(InputAction.CallbackContext context);
    }
}
