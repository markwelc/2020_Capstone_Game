// GENERATED AUTOMATICALLY FROM 'Assets/Characters/Player Controls.inputactions'

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
    ""name"": ""Player Controls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""87260a94-ba61-4056-8893-d05df3079eec"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3b7123b3-8e8e-4fea-90de-ea6d5d0edc59"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1b13d23a-62e4-4f6f-88fd-12396b9858fb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""063c2e87-7850-4b59-9fe3-af8653d51259"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""ff595060-8754-4785-b203-2fb733a72236"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeViewMode"",
                    ""type"": ""Button"",
                    ""id"": ""c3f466d9-0397-4e0d-b257-fa369a18c920"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CycleWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""47ceae40-ae82-4aa1-8306-13eac7157ce6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PauseGame"",
                    ""type"": ""Button"",
                    ""id"": ""6b507f1f-1fe4-46d7-bb8e-9b1142d93726"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pickup"",
                    ""type"": ""Button"",
                    ""id"": ""778aa146-7d5b-4d3f-ad34-4b3cd6dcad2e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""enableInventory"",
                    ""type"": ""Button"",
                    ""id"": ""f8c71a81-8c67-470c-b4c1-6e7037beba66"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Tool"",
                    ""type"": ""Button"",
                    ""id"": ""e9eb264e-a471-4480-bf8b-be6212a3bd38"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Tool2"",
                    ""type"": ""Button"",
                    ""id"": ""9ecc3959-85b9-458e-b08c-7a7ff58f3653"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Tool3"",
                    ""type"": ""Button"",
                    ""id"": ""727b9ff4-e966-44a7-84ae-6a1960c92684"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Tool4"",
                    ""type"": ""Button"",
                    ""id"": ""ac941264-e690-4a21-b284-e614e0d7ca8f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""cdd679c7-8e09-4a59-a40f-960aba91e900"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e5121888-fc7f-448b-9076-bb1d7f606e24"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""823cde20-234b-41d4-a369-a8fb1c34f5d6"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""65e132f3-91c5-4602-bb40-96c6ff49d2ca"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0721b5cd-328c-4596-b1a2-3d308b6a2fb8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a331c8d0-26fd-42c0-b37b-3f2761c85988"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7e7fbcdb-0816-4dec-8b6a-b629da2a99a3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2af00d26-c2db-46db-bc0c-ab0524e6758c"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""937ac829-7e98-402f-b9fa-1b75bd216d95"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false)"",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1ca15da-9da3-4fc2-a7a7-0a62ab44f627"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0b3d477-b631-43b1-b640-b2dc8ab0f30c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""97d4c99f-6eba-4759-a130-ccdfc3b7f762"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac862426-d6cf-431f-b5c4-91a26d5a9065"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ee3b3ed-c22f-4eba-89f8-0f223abfcb4f"",
                    ""path"": ""<Keyboard>/#(=)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeViewMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e062a685-47a7-4bcc-809b-4e615833b4e4"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d44a149-991b-47c5-88ca-47688b1698a9"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58d6f7ce-bd62-4c2e-ae34-5b7e686727b5"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad7404c6-a22a-4433-b954-ee8892bdb153"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pickup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffe3b8b1-9170-4c09-9b21-fba732137eb3"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pickup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""579517ed-5d02-46b6-9f74-142d35367ffc"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""enableInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92bf452f-d569-4080-99ef-38792a8c7d25"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""enableInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4f0d796-7e83-4d89-81e3-02c236869fba"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tool2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd8107cb-e0c4-4ac5-bc21-54cc0f719ae8"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tool2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""907ff4f6-dd75-478d-ad20-861467d97716"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tool"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cec5f449-6a70-45f4-85c7-bf9ce81a4d55"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tool"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""213ed7c4-85a8-4134-b2f5-504903370131"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tool3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""54744ff6-bc69-46d6-a3f7-2d6541084c33"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tool4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1f3032c-b013-4180-bcf0-84db15b17563"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tool4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""102d2997-fc09-4db9-b3f2-aa5e4106e034"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5199940e-a60b-4461-b245-b30cc6c11267"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
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
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Look = m_Gameplay.FindAction("Look", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Dash = m_Gameplay.FindAction("Dash", throwIfNotFound: true);
        m_Gameplay_ChangeViewMode = m_Gameplay.FindAction("ChangeViewMode", throwIfNotFound: true);
        m_Gameplay_CycleWeapon = m_Gameplay.FindAction("CycleWeapon", throwIfNotFound: true);
        m_Gameplay_PauseGame = m_Gameplay.FindAction("PauseGame", throwIfNotFound: true);
        m_Gameplay_Pickup = m_Gameplay.FindAction("Pickup", throwIfNotFound: true);
        m_Gameplay_enableInventory = m_Gameplay.FindAction("enableInventory", throwIfNotFound: true);
        m_Gameplay_Tool = m_Gameplay.FindAction("Tool", throwIfNotFound: true);
        m_Gameplay_Tool2 = m_Gameplay.FindAction("Tool2", throwIfNotFound: true);
        m_Gameplay_Tool3 = m_Gameplay.FindAction("Tool3", throwIfNotFound: true);
        m_Gameplay_Tool4 = m_Gameplay.FindAction("Tool4", throwIfNotFound: true);
        m_Gameplay_Aim = m_Gameplay.FindAction("Aim", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Look;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Dash;
    private readonly InputAction m_Gameplay_ChangeViewMode;
    private readonly InputAction m_Gameplay_CycleWeapon;
    private readonly InputAction m_Gameplay_PauseGame;
    private readonly InputAction m_Gameplay_Pickup;
    private readonly InputAction m_Gameplay_enableInventory;
    private readonly InputAction m_Gameplay_Tool;
    private readonly InputAction m_Gameplay_Tool2;
    private readonly InputAction m_Gameplay_Tool3;
    private readonly InputAction m_Gameplay_Tool4;
    private readonly InputAction m_Gameplay_Aim;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Look => m_Wrapper.m_Gameplay_Look;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Dash => m_Wrapper.m_Gameplay_Dash;
        public InputAction @ChangeViewMode => m_Wrapper.m_Gameplay_ChangeViewMode;
        public InputAction @CycleWeapon => m_Wrapper.m_Gameplay_CycleWeapon;
        public InputAction @PauseGame => m_Wrapper.m_Gameplay_PauseGame;
        public InputAction @Pickup => m_Wrapper.m_Gameplay_Pickup;
        public InputAction @enableInventory => m_Wrapper.m_Gameplay_enableInventory;
        public InputAction @Tool => m_Wrapper.m_Gameplay_Tool;
        public InputAction @Tool2 => m_Wrapper.m_Gameplay_Tool2;
        public InputAction @Tool3 => m_Wrapper.m_Gameplay_Tool3;
        public InputAction @Tool4 => m_Wrapper.m_Gameplay_Tool4;
        public InputAction @Aim => m_Wrapper.m_Gameplay_Aim;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Dash.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @ChangeViewMode.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeViewMode;
                @ChangeViewMode.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeViewMode;
                @ChangeViewMode.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeViewMode;
                @CycleWeapon.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCycleWeapon;
                @CycleWeapon.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCycleWeapon;
                @CycleWeapon.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCycleWeapon;
                @PauseGame.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPauseGame;
                @PauseGame.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPauseGame;
                @PauseGame.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPauseGame;
                @Pickup.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPickup;
                @Pickup.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPickup;
                @Pickup.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPickup;
                @enableInventory.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEnableInventory;
                @enableInventory.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEnableInventory;
                @enableInventory.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEnableInventory;
                @Tool.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTool;
                @Tool.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTool;
                @Tool.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTool;
                @Tool2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTool2;
                @Tool2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTool2;
                @Tool2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTool2;
                @Tool3.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTool3;
                @Tool3.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTool3;
                @Tool3.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTool3;
                @Tool4.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTool4;
                @Tool4.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTool4;
                @Tool4.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTool4;
                @Aim.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @ChangeViewMode.started += instance.OnChangeViewMode;
                @ChangeViewMode.performed += instance.OnChangeViewMode;
                @ChangeViewMode.canceled += instance.OnChangeViewMode;
                @CycleWeapon.started += instance.OnCycleWeapon;
                @CycleWeapon.performed += instance.OnCycleWeapon;
                @CycleWeapon.canceled += instance.OnCycleWeapon;
                @PauseGame.started += instance.OnPauseGame;
                @PauseGame.performed += instance.OnPauseGame;
                @PauseGame.canceled += instance.OnPauseGame;
                @Pickup.started += instance.OnPickup;
                @Pickup.performed += instance.OnPickup;
                @Pickup.canceled += instance.OnPickup;
                @enableInventory.started += instance.OnEnableInventory;
                @enableInventory.performed += instance.OnEnableInventory;
                @enableInventory.canceled += instance.OnEnableInventory;
                @Tool.started += instance.OnTool;
                @Tool.performed += instance.OnTool;
                @Tool.canceled += instance.OnTool;
                @Tool2.started += instance.OnTool2;
                @Tool2.performed += instance.OnTool2;
                @Tool2.canceled += instance.OnTool2;
                @Tool3.started += instance.OnTool3;
                @Tool3.performed += instance.OnTool3;
                @Tool3.canceled += instance.OnTool3;
                @Tool4.started += instance.OnTool4;
                @Tool4.performed += instance.OnTool4;
                @Tool4.canceled += instance.OnTool4;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnChangeViewMode(InputAction.CallbackContext context);
        void OnCycleWeapon(InputAction.CallbackContext context);
        void OnPauseGame(InputAction.CallbackContext context);
        void OnPickup(InputAction.CallbackContext context);
        void OnEnableInventory(InputAction.CallbackContext context);
        void OnTool(InputAction.CallbackContext context);
        void OnTool2(InputAction.CallbackContext context);
        void OnTool3(InputAction.CallbackContext context);
        void OnTool4(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
    }
}
