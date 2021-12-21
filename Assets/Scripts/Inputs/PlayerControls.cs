// GENERATED AUTOMATICALLY FROM 'Assets/Resources/Inputs/Player Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Project.Input
{
    public class @PlayerControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""51d781ac-f9ef-4a32-804c-1f0e7d7e4874"",
            ""actions"": [
                {
                    ""name"": ""MovePlus"",
                    ""type"": ""Button"",
                    ""id"": ""8212ed20-c50f-4150-8b06-e9d83f5fd020"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""MoveDiagonal"",
                    ""type"": ""Button"",
                    ""id"": ""bc036ec8-e236-419a-a26c-d074f1024f40"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""c88c9843-fadf-40e4-8389-7aa47186be9e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Wait"",
                    ""type"": ""Button"",
                    ""id"": ""c428471c-0a06-410f-84b1-c26f8720c611"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Examine"",
                    ""type"": ""Button"",
                    ""id"": ""bc278548-dacb-46b5-a1ed-088dddc96786"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Numpad"",
                    ""id"": ""5b3991c1-e10d-4fea-b701-977c25c1de90"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovePlus"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d78846ce-8787-463d-b993-3d3b6aabd282"",
                    ""path"": ""<Keyboard>/numpad8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovePlus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3b862434-6f2d-47fa-a702-cfe45c65a7ac"",
                    ""path"": ""<Keyboard>/numpad2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovePlus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a271b166-7c98-44d9-a04d-f1526cb07f1f"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovePlus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f6e7d06d-b456-44ff-9906-92f9dfb40e47"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovePlus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ArrowKeys"",
                    ""id"": ""dd8ccc61-a0e6-4565-b21a-e6da15cef57d"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovePlus"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""239ede6a-b2bd-47c3-abc7-356532564968"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovePlus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fc317c55-d1b8-4147-99b0-4c8b4fde1fa8"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovePlus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d66f8493-2b7d-4887-93c9-056266665308"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovePlus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f4757cc4-83e4-4684-ad55-b5b0752c43b3"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MovePlus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""57df5be7-5f07-47f1-b7ce-18ba927d2d3f"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0319d4a5-6e0a-457e-9b22-0d34f990ed23"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fac36b3c-96e7-4b17-a578-cbaab4d6eb60"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""NumpadDiagonal"",
                    ""id"": ""568735f3-ad10-4954-8d40-85ea661b695c"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveDiagonal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4a8e9b8b-63ba-449b-8e81-7a31badd6573"",
                    ""path"": ""<Keyboard>/numpad9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveDiagonal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ff85c2ed-2203-43ed-8749-7f63ad8f9b96"",
                    ""path"": ""<Keyboard>/numpad1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveDiagonal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""21c9cce2-a80d-4460-9af6-a9dbd30d8960"",
                    ""path"": ""<Keyboard>/numpad7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveDiagonal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3f275e11-bbac-4400-addb-5158aacbaf4d"",
                    ""path"": ""<Keyboard>/numpad3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveDiagonal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ed82606c-1656-408e-a8ed-b0be471108ac"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Wait"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c72b0ef-ea6d-4d31-8fa0-309a37f9475d"",
                    ""path"": ""<Keyboard>/rightShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Wait"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""607bcdba-ab91-45b5-bff9-0c9736968854"",
                    ""path"": ""<Keyboard>/slash"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Examine"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""d85f03dd-85d6-4487-a59d-528225732e49"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""0bf2aa27-ff80-4bdf-a3fb-439ec30d9fca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Accept"",
                    ""type"": ""Button"",
                    ""id"": ""0a4ff329-19d9-4c14-9bee-9c57243d49e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleHelp"",
                    ""type"": ""Button"",
                    ""id"": ""b95017ac-ffb4-47a5-ab15-56ec393791b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleInventory"",
                    ""type"": ""Button"",
                    ""id"": ""a28fbfe4-261e-46a7-9653-c91ce7917f4f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""48611872-03bb-4a82-a795-1176a3a86845"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f014b48-1c1d-4a5c-a5cc-c81d9d79f5e1"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""787f3671-3bca-4661-b89b-10e8f3e7b7a4"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Numpad"",
                    ""id"": ""b395dbb4-8801-410f-9382-047daf5bcf22"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""01bb5fbd-6fba-4616-a541-3360fef1d03a"",
                    ""path"": ""<Keyboard>/numpad8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""bb2fee2a-9433-4fe8-b157-a2bdc9282cb2"",
                    ""path"": ""<Keyboard>/numpad2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""097d9ac7-3b02-4317-91a8-554bf51e2cb5"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""05bda90e-3dc6-4059-b840-818841ffa846"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ArrowKeys"",
                    ""id"": ""f4e5162b-76cb-402b-af91-f65d1cc58471"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3b2355df-b7c7-4cdf-93ed-e6e7a1b21b18"",
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
                    ""id"": ""d27f0713-4a47-458a-afcd-98241a662aee"",
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
                    ""id"": ""3e912cea-daef-41fd-ae5d-61deb08abae6"",
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
                    ""id"": ""24d55abf-2cae-4747-aaf4-3102dc451ab5"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""60eba281-875a-4c3d-9048-d7b822553041"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ToggleHelp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd87ebba-7c19-497e-a6c1-bbda4beb7b29"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ToggleInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Debug"",
            ""id"": ""e4b64747-45b7-41fb-9b11-a8d24e25c0c6"",
            ""actions"": [
                {
                    ""name"": ""RegenerateDungeon"",
                    ""type"": ""Button"",
                    ""id"": ""12918ba3-1121-4a7a-a708-07aea74600d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""52ca6980-ec7e-4f4f-98bb-d688818170d4"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""RegenerateDungeon"",
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
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_MovePlus = m_Player.FindAction("MovePlus", throwIfNotFound: true);
            m_Player_MoveDiagonal = m_Player.FindAction("MoveDiagonal", throwIfNotFound: true);
            m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
            m_Player_Wait = m_Player.FindAction("Wait", throwIfNotFound: true);
            m_Player_Examine = m_Player.FindAction("Examine", throwIfNotFound: true);
            // UI
            m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
            m_UI_Move = m_UI.FindAction("Move", throwIfNotFound: true);
            m_UI_Accept = m_UI.FindAction("Accept", throwIfNotFound: true);
            m_UI_ToggleHelp = m_UI.FindAction("ToggleHelp", throwIfNotFound: true);
            m_UI_ToggleInventory = m_UI.FindAction("ToggleInventory", throwIfNotFound: true);
            // Debug
            m_Debug = asset.FindActionMap("Debug", throwIfNotFound: true);
            m_Debug_RegenerateDungeon = m_Debug.FindAction("RegenerateDungeon", throwIfNotFound: true);
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

        // Player
        private readonly InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private readonly InputAction m_Player_MovePlus;
        private readonly InputAction m_Player_MoveDiagonal;
        private readonly InputAction m_Player_Interact;
        private readonly InputAction m_Player_Wait;
        private readonly InputAction m_Player_Examine;
        public struct PlayerActions
        {
            private @PlayerControls m_Wrapper;
            public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @MovePlus => m_Wrapper.m_Player_MovePlus;
            public InputAction @MoveDiagonal => m_Wrapper.m_Player_MoveDiagonal;
            public InputAction @Interact => m_Wrapper.m_Player_Interact;
            public InputAction @Wait => m_Wrapper.m_Player_Wait;
            public InputAction @Examine => m_Wrapper.m_Player_Examine;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    @MovePlus.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovePlus;
                    @MovePlus.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovePlus;
                    @MovePlus.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovePlus;
                    @MoveDiagonal.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveDiagonal;
                    @MoveDiagonal.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveDiagonal;
                    @MoveDiagonal.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveDiagonal;
                    @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                    @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                    @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                    @Wait.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWait;
                    @Wait.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWait;
                    @Wait.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWait;
                    @Examine.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnExamine;
                    @Examine.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnExamine;
                    @Examine.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnExamine;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @MovePlus.started += instance.OnMovePlus;
                    @MovePlus.performed += instance.OnMovePlus;
                    @MovePlus.canceled += instance.OnMovePlus;
                    @MoveDiagonal.started += instance.OnMoveDiagonal;
                    @MoveDiagonal.performed += instance.OnMoveDiagonal;
                    @MoveDiagonal.canceled += instance.OnMoveDiagonal;
                    @Interact.started += instance.OnInteract;
                    @Interact.performed += instance.OnInteract;
                    @Interact.canceled += instance.OnInteract;
                    @Wait.started += instance.OnWait;
                    @Wait.performed += instance.OnWait;
                    @Wait.canceled += instance.OnWait;
                    @Examine.started += instance.OnExamine;
                    @Examine.performed += instance.OnExamine;
                    @Examine.canceled += instance.OnExamine;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);

        // UI
        private readonly InputActionMap m_UI;
        private IUIActions m_UIActionsCallbackInterface;
        private readonly InputAction m_UI_Move;
        private readonly InputAction m_UI_Accept;
        private readonly InputAction m_UI_ToggleHelp;
        private readonly InputAction m_UI_ToggleInventory;
        public struct UIActions
        {
            private @PlayerControls m_Wrapper;
            public UIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_UI_Move;
            public InputAction @Accept => m_Wrapper.m_UI_Accept;
            public InputAction @ToggleHelp => m_Wrapper.m_UI_ToggleHelp;
            public InputAction @ToggleInventory => m_Wrapper.m_UI_ToggleInventory;
            public InputActionMap Get() { return m_Wrapper.m_UI; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
            public void SetCallbacks(IUIActions instance)
            {
                if (m_Wrapper.m_UIActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMove;
                    @Accept.started -= m_Wrapper.m_UIActionsCallbackInterface.OnAccept;
                    @Accept.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnAccept;
                    @Accept.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnAccept;
                    @ToggleHelp.started -= m_Wrapper.m_UIActionsCallbackInterface.OnToggleHelp;
                    @ToggleHelp.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnToggleHelp;
                    @ToggleHelp.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnToggleHelp;
                    @ToggleInventory.started -= m_Wrapper.m_UIActionsCallbackInterface.OnToggleInventory;
                    @ToggleInventory.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnToggleInventory;
                    @ToggleInventory.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnToggleInventory;
                }
                m_Wrapper.m_UIActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Accept.started += instance.OnAccept;
                    @Accept.performed += instance.OnAccept;
                    @Accept.canceled += instance.OnAccept;
                    @ToggleHelp.started += instance.OnToggleHelp;
                    @ToggleHelp.performed += instance.OnToggleHelp;
                    @ToggleHelp.canceled += instance.OnToggleHelp;
                    @ToggleInventory.started += instance.OnToggleInventory;
                    @ToggleInventory.performed += instance.OnToggleInventory;
                    @ToggleInventory.canceled += instance.OnToggleInventory;
                }
            }
        }
        public UIActions @UI => new UIActions(this);

        // Debug
        private readonly InputActionMap m_Debug;
        private IDebugActions m_DebugActionsCallbackInterface;
        private readonly InputAction m_Debug_RegenerateDungeon;
        public struct DebugActions
        {
            private @PlayerControls m_Wrapper;
            public DebugActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @RegenerateDungeon => m_Wrapper.m_Debug_RegenerateDungeon;
            public InputActionMap Get() { return m_Wrapper.m_Debug; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(DebugActions set) { return set.Get(); }
            public void SetCallbacks(IDebugActions instance)
            {
                if (m_Wrapper.m_DebugActionsCallbackInterface != null)
                {
                    @RegenerateDungeon.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnRegenerateDungeon;
                    @RegenerateDungeon.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnRegenerateDungeon;
                    @RegenerateDungeon.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnRegenerateDungeon;
                }
                m_Wrapper.m_DebugActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @RegenerateDungeon.started += instance.OnRegenerateDungeon;
                    @RegenerateDungeon.performed += instance.OnRegenerateDungeon;
                    @RegenerateDungeon.canceled += instance.OnRegenerateDungeon;
                }
            }
        }
        public DebugActions @Debug => new DebugActions(this);
        private int m_KeyboardSchemeIndex = -1;
        public InputControlScheme KeyboardScheme
        {
            get
            {
                if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
                return asset.controlSchemes[m_KeyboardSchemeIndex];
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
        public interface IPlayerActions
        {
            void OnMovePlus(InputAction.CallbackContext context);
            void OnMoveDiagonal(InputAction.CallbackContext context);
            void OnInteract(InputAction.CallbackContext context);
            void OnWait(InputAction.CallbackContext context);
            void OnExamine(InputAction.CallbackContext context);
        }
        public interface IUIActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnAccept(InputAction.CallbackContext context);
            void OnToggleHelp(InputAction.CallbackContext context);
            void OnToggleInventory(InputAction.CallbackContext context);
        }
        public interface IDebugActions
        {
            void OnRegenerateDungeon(InputAction.CallbackContext context);
        }
    }
}
