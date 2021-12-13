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
                    ""type"": ""PassThrough"",
                    ""id"": ""9efb3f43-39f5-4057-b846-0fe12a168cca"",
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
                    ""type"": ""Value"",
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
                    ""name"": ""Sword Weapon"",
                    ""type"": ""Button"",
                    ""id"": ""f3611410-27fe-4253-9781-d7765139f881"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Bow Weapon"",
                    ""type"": ""Button"",
                    ""id"": ""ebc2c2f0-c19c-4154-99ac-24c3bd02f0b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""bc669470-5ef2-459b-bfb2-98c964f397e9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""af49b4ee-e627-488d-bd93-74b3d18fa716"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2f67cd2c-3eb7-46d0-95bb-bb3881e9e28d"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""827c8572-19cb-4bcc-b6b5-6282e95f694c"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""730d5544-8434-4ff7-9cef-d007beec9212"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eaa24305-53da-48bf-af38-c84fce9ba27e"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
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
                    ""groups"": ""Keyboard"",
                    ""action"": ""Health Refill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b2093d4-f5fd-46c5-aa03-2030af840274"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Health Refill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""57b5cad1-6820-4ee7-bc34-2d72d263a16c"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
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
                    ""groups"": ""Mouse"",
                    ""action"": ""HeavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d5102c24-6b68-406f-8bf7-e74ffd1b6e30"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HeavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6a0ee9b-4a8b-49c3-8a5e-7b1b47d53189"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Sword Weapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4361f8a2-b614-44cc-83db-52a9e5aa28bc"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sword Weapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0f3c7a2-37d4-4fe9-8cf2-014a8ae7c4b8"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Bow Weapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""324fcbbf-34bd-4cdc-8ebf-8c2e0ab78af5"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Bow Weapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""83be6b2d-d586-49da-80d6-898ca31f422b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ef91db81-ede9-44f5-b393-2e42713b999f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""10094644-78b2-4ed0-aef8-755e248abe05"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bc069dd0-30ca-4237-acdf-d9029b66e31b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""50964199-0507-4373-b80b-f9bbd7c29dc6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""c8c34468-ae88-43bd-bf2f-bfedd88e21ad"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""28057da3-b666-4929-8cc8-aef565f89a06"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f6ea807c-1861-4813-923a-ca91ab7cf8ab"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""21db49c8-863c-4e90-acdc-eb47f26d61c5"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""af73e583-c4d2-4e13-ac6d-3de57de7a82b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""75027ed9-0ce4-4ea1-bd2d-64a1304c3a34"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""92e61cf8-3482-458c-a338-e1ef9ea5dcfb"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fcdd115e-6626-46bd-bb73-41a3680bb6d2"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""98541916-9f67-43f4-bd73-6259168c6b98"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3b63d1a6-1de3-44fc-94e8-99711239630a"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""HorizontalMovement"",
                    ""id"": ""c405ebe3-58ce-4d70-97db-1c5a3f2e46d9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c743915f-2788-4224-9533-4e3439251a63"",
                    ""path"": ""<Keyboard>/A"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a5fc0f01-3418-4408-9e7f-baee87d36841"",
                    ""path"": ""<Keyboard>/D"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""HorizontalMovementArrows"",
                    ""id"": ""22c803c9-538c-4c1d-aac6-ef6a0dad6ec8"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3aaaeef0-53c3-45ba-913c-d4c2fa8ece21"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""cd330350-e321-430c-88a2-545aab89ec63"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""VerticalMovement"",
                    ""id"": ""62ae73a6-49e9-4809-b98d-78b7e4646447"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""008d1ea8-88f1-4241-851d-03b20575d521"",
                    ""path"": ""<Keyboard>/S"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ef2f3d8b-b473-4fd5-a7a5-4b64c5f960b1"",
                    ""path"": ""<Keyboard>/W"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""VerticalMovementArrows"",
                    ""id"": ""c7cac121-3442-4940-939f-b861f7ded692"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""35e35394-342e-4f8e-b675-76f4acc331e4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""81d80c1f-65f4-4aa3-980f-a97222e4a6f6"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b6c0a971-41c6-4b93-b0b1-a878457d9149"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Dodgeroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cd7dd0ca-d582-46a1-860a-39cf9aeb4498"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dodgeroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
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
        m_WindlessLand_SwordWeapon = m_WindlessLand.FindAction("Sword Weapon", throwIfNotFound: true);
        m_WindlessLand_BowWeapon = m_WindlessLand.FindAction("Bow Weapon", throwIfNotFound: true);
        m_WindlessLand_Movement = m_WindlessLand.FindAction("Movement", throwIfNotFound: true);
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
    private readonly InputAction m_WindlessLand_SwordWeapon;
    private readonly InputAction m_WindlessLand_BowWeapon;
    private readonly InputAction m_WindlessLand_Movement;
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
        public InputAction @SwordWeapon => m_Wrapper.m_WindlessLand_SwordWeapon;
        public InputAction @BowWeapon => m_Wrapper.m_WindlessLand_BowWeapon;
        public InputAction @Movement => m_Wrapper.m_WindlessLand_Movement;
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
                @SwordWeapon.started -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnSwordWeapon;
                @SwordWeapon.performed -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnSwordWeapon;
                @SwordWeapon.canceled -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnSwordWeapon;
                @BowWeapon.started -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnBowWeapon;
                @BowWeapon.performed -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnBowWeapon;
                @BowWeapon.canceled -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnBowWeapon;
                @Movement.started -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_WindlessLandActionsCallbackInterface.OnMovement;
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
                @SwordWeapon.started += instance.OnSwordWeapon;
                @SwordWeapon.performed += instance.OnSwordWeapon;
                @SwordWeapon.canceled += instance.OnSwordWeapon;
                @BowWeapon.started += instance.OnBowWeapon;
                @BowWeapon.performed += instance.OnBowWeapon;
                @BowWeapon.canceled += instance.OnBowWeapon;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
            }
        }
    }
    public WindlessLandActions @WindlessLand => new WindlessLandActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_MouseSchemeIndex = -1;
    public InputControlScheme MouseScheme
    {
        get
        {
            if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
            return asset.controlSchemes[m_MouseSchemeIndex];
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
        void OnSwordWeapon(InputAction.CallbackContext context);
        void OnBowWeapon(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
    }
}
