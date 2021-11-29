// GENERATED AUTOMATICALLY FROM 'Assets/Source Material/Miranda Greting/Inputs/PlayerInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""WindlessLand"",
            ""id"": ""e20702bc-8457-4d60-8fe1-766c674ce8b5"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""bc669470-5ef2-459b-bfb2-98c964f397e9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""020c1d09-0309-470e-b4f5-da6515d3806a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""1ccda27e-cb1c-48bc-945e-cbb985782f9e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HeavyAttack"",
                    ""type"": ""Button"",
                    ""id"": ""8b186f12-b5f8-40ed-b20c-f2f55c09a0c0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Health Refill"",
                    ""type"": ""Button"",
                    ""id"": ""664bf9cb-e5e6-4780-8e10-5c2fa45a16d8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dodgeroll"",
                    ""type"": ""Button"",
                    ""id"": ""922eb730-e5bf-410e-a779-a33a62871ca0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Melee Weapon"",
                    ""type"": ""Button"",
                    ""id"": ""f3611410-27fe-4253-9781-d7765139f881"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ranged Weapon"",
                    ""type"": ""Button"",
                    ""id"": ""ebc2c2f0-c19c-4154-99ac-24c3bd02f0b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""HorizontalMovement"",
                    ""id"": ""c405ebe3-58ce-4d70-97db-1c5a3f2e46d9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c743915f-2788-4224-9533-4e3439251a63"",
                    ""path"": ""<Keyboard>/A"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a5fc0f01-3418-4408-9e7f-baee87d36841"",
                    ""path"": ""<Keyboard>/D"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""HorizontalMovementGamepad"",
                    ""id"": ""2fc26e2b-e439-4cc6-8fbc-b0dfb74b9b7f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f79bf9ca-41eb-4e03-9ebe-151859fc9fac"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""777bcd6d-aff6-4635-a7a1-4bc0eacd883c"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""VerticalMovement"",
                    ""id"": ""62ae73a6-49e9-4809-b98d-78b7e4646447"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""008d1ea8-88f1-4241-851d-03b20575d521"",
                    ""path"": ""<Keyboard>/S"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ef2f3d8b-b473-4fd5-a7a5-4b64c5f960b1"",
                    ""path"": ""<Keyboard>/W"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""VerticalMoveGamepad"",
                    ""id"": ""f0fd3880-f570-4e8b-9c66-8d2d754434ea"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8e537b2e-8ca0-4dff-ad6c-2f208ebc8133"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ad57929a-5b41-4e7b-bee4-e29ab7bcaa56"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""af49b4ee-e627-488d-bd93-74b3d18fa716"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""827c8572-19cb-4bcc-b6b5-6282e95f694c"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""730d5544-8434-4ff7-9cef-d007beec9212"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1cb3ec1e-6a93-40cc-beda-393fb92cbcdb"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Health Refill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b6f2c18-540d-47b3-b444-1eb4207ee442"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""Dodgeroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""57b5cad1-6820-4ee7-bc34-2d72d263a16c"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HeavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5363b65-0d65-4614-9969-fcf424677068"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HeavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6a0ee9b-4a8b-49c3-8a5e-7b1b47d53189"",
                    ""path"": ""<Keyboard>/#(1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Melee Weapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0f3c7a2-37d4-4fe9-8cf2-014a8ae7c4b8"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ranged Weapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""ControlScheme"",
            ""bindingGroup"": ""ControlScheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<DualShock4GampadiOS>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<XboxOneGampadiOS>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // WindlessLand
        m_WindlessLand = asset.FindActionMap("WindlessLand", throwIfNotFound: true);
        m_WindlessLand_Move = m_WindlessLand.FindAction("Move", throwIfNotFound: true);
        m_WindlessLand_Interact = m_WindlessLand.FindAction("Interact", throwIfNotFound: true);
        m_WindlessLand_Attack = m_WindlessLand.FindAction("Attack", throwIfNotFound: true);
        m_WindlessLand_HeavyAttack = m_WindlessLand.FindAction("HeavyAttack", throwIfNotFound: true);
        m_WindlessLand_HealthRefill = m_WindlessLand.FindAction("Health Refill", throwIfNotFound: true);
        m_WindlessLand_Dodgeroll = m_WindlessLand.FindAction("Dodgeroll", throwIfNotFound: true);
        m_WindlessLand_MeleeWeapon = m_WindlessLand.FindAction("Melee Weapon", throwIfNotFound: true);
        m_WindlessLand_RangedWeapon = m_WindlessLand.FindAction("Ranged Weapon", throwIfNotFound: true);
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

    // WindlessLand
    private readonly InputActionMap m_WindlessLand;
    private IWindlessLandActions m_WindlessLandActionsCallbackInterface;
    private readonly InputAction m_WindlessLand_Move;
    private readonly InputAction m_WindlessLand_Interact;
    private readonly InputAction m_WindlessLand_Attack;
    private readonly InputAction m_WindlessLand_HeavyAttack;
    private readonly InputAction m_WindlessLand_HealthRefill;
    private readonly InputAction m_WindlessLand_Dodgeroll;
    private readonly InputAction m_WindlessLand_MeleeWeapon;
    private readonly InputAction m_WindlessLand_RangedWeapon;
    public struct WindlessLandActions
    {
        private @PlayerInputs m_Wrapper;
        public WindlessLandActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_WindlessLand_Move;
        public InputAction @Interact => m_Wrapper.m_WindlessLand_Interact;
        public InputAction @Attack => m_Wrapper.m_WindlessLand_Attack;
        public InputAction @HeavyAttack => m_Wrapper.m_WindlessLand_HeavyAttack;
        public InputAction @HealthRefill => m_Wrapper.m_WindlessLand_HealthRefill;
        public InputAction @Dodgeroll => m_Wrapper.m_WindlessLand_Dodgeroll;
        public InputAction @MeleeWeapon => m_Wrapper.m_WindlessLand_MeleeWeapon;
        public InputAction @RangedWeapon => m_Wrapper.m_WindlessLand_RangedWeapon;
        public InputActionMap Get() { return m_Wrapper.m_WindlessLand; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WindlessLandActions set) { return set.Get(); }
        public void SetCallbacks(IWindlessLandActions instance)
        {
            if (m_Wrapper.m_WindlessLandActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnMove;
                @Interact.started -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnInteract;
                @Attack.started -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnAttack;
                @HeavyAttack.started -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.performed -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.canceled -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnHeavyAttack;
                @HealthRefill.started -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnHealthRefill;
                @HealthRefill.performed -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnHealthRefill;
                @HealthRefill.canceled -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnHealthRefill;
                @Dodgeroll.started -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnDodgeroll;
                @Dodgeroll.performed -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnDodgeroll;
                @Dodgeroll.canceled -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnDodgeroll;
                @MeleeWeapon.started -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnMeleeWeapon;
                @MeleeWeapon.performed -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnMeleeWeapon;
                @MeleeWeapon.canceled -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnMeleeWeapon;
                @RangedWeapon.started -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnRangedWeapon;
                @RangedWeapon.performed -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnRangedWeapon;
                @RangedWeapon.canceled -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnRangedWeapon;
            }
            m_Wrapper.m_WindlessLandActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @HeavyAttack.started += instance.OnHeavyAttack;
                @HeavyAttack.performed += instance.OnHeavyAttack;
                @HeavyAttack.canceled += instance.OnHeavyAttack;
                @HealthRefill.started += instance.OnHealthRefill;
                @HealthRefill.performed += instance.OnHealthRefill;
                @HealthRefill.canceled += instance.OnHealthRefill;
                @Dodgeroll.started += instance.OnDodgeroll;
                @Dodgeroll.performed += instance.OnDodgeroll;
                @Dodgeroll.canceled += instance.OnDodgeroll;
                @MeleeWeapon.started += instance.OnMeleeWeapon;
                @MeleeWeapon.performed += instance.OnMeleeWeapon;
                @MeleeWeapon.canceled += instance.OnMeleeWeapon;
                @RangedWeapon.started += instance.OnRangedWeapon;
                @RangedWeapon.performed += instance.OnRangedWeapon;
                @RangedWeapon.canceled += instance.OnRangedWeapon;
            }
        }
    }
    public WindlessLandActions @WindlessLand => new WindlessLandActions(this);
    private int m_ControlSchemeSchemeIndex = -1;
    public InputControlScheme ControlSchemeScheme
    {
        get
        {
            if (m_ControlSchemeSchemeIndex == -1) m_ControlSchemeSchemeIndex = asset.FindControlSchemeIndex("ControlScheme");
            return asset.controlSchemes[m_ControlSchemeSchemeIndex];
        }
    }
    public interface IWindlessLandActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnHeavyAttack(InputAction.CallbackContext context);
        void OnHealthRefill(InputAction.CallbackContext context);
        void OnDodgeroll(InputAction.CallbackContext context);
        void OnMeleeWeapon(InputAction.CallbackContext context);
        void OnRangedWeapon(InputAction.CallbackContext context);
    }
}
