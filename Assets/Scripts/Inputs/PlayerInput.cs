
using Project.Display;
using UnityEngine;

namespace Project.Input
{
    public class PlayerInput
    {
        #region Fields

        private static PlayerControls _playerControls 
        {
            get
            {
                if(_controls == null)
                {
                    _controls = new PlayerControls();
                    _controls.Enable();
                    GetTriggerInputs();
                }
                return _controls;
            }
        }
        private static PlayerControls _controls;

        private const float c_autoMoveDelay = .3f;   //Press duration before setting s_IsMoving to true every c_moveDelay seconds.
        private const float c_autoMoveInterval = .1f;   //Sets s_IsMoving to true once every c_moveDelay seconds instead of just once when pressed.
        private static bool _shouldStartTimers { get; set; }
        private static float _autoMoveDelayTimer { get; set; }
        private static float _autoMoveIntervalTimer { get; set; }

        public static bool s_UseItem { get; private set; }
        public static int s_UseItemIndex { get; private set; }  //Used to retrieve the item index depending on the key pressed


        public static bool s_ToggleHelp { get; private set; }


        public static bool s_Waits { get; private set; }
        public static bool s_IsMoving { get; private set; }
        public static bool s_Interacts { get; private set; }
        public static bool s_RightClick { get; private set; }
        public static bool s_IsCheckingTiles { get; private set; }
        public static Vector2Int s_MoveDirResult { get; private set; }
        private static Vector2Int s_moveDirPlus { get; set; }
        private static Vector2Int s_moveDirDiagonal { get; set; }

        #endregion



        #region Methods



        //Called in the GameManager's Update method
        public static void Update()
        {
            GetContinuousInputs();
        }

        //Called only once to assign the conditions to the input triggers.
        //Used to assign conditions to the inputs' started and canceled events.
        private static void GetTriggerInputs()
        {
            _playerControls.Player.MovePlus.started += ctx => _shouldStartTimers = true;
            _playerControls.Player.MoveDiagonal.started += ctx => _shouldStartTimers = true;
            _playerControls.Player.MovePlus.canceled += ctx => 
            {
                _shouldStartTimers = false;
                _autoMoveDelayTimer = 0f;
            };
            _playerControls.Player.MoveDiagonal.canceled += ctx =>
            {
                _shouldStartTimers = false;
                _autoMoveDelayTimer = 0f;
            };
        }

        //Called each Update to set the conditions to the new input values
        //Used to assign conditions to the inputs' performed and triggered events.
        private static void GetContinuousInputs()
        {
            s_moveDirPlus = Vector2Int.zero;
            s_moveDirDiagonal = Vector2Int.zero;
            s_MoveDirResult = Vector2Int.zero;

            s_UseItem = _playerControls.UI.UseItem.triggered;
            //Converts the key pressed to an int to select an item in the Inventory
            if (s_UseItem)
            {
                for (int i = 0; i < _playerControls.UI.UseItem.controls.Count; i++)
                {
                    if(_playerControls.UI.UseItem.activeControl.name == _playerControls.UI.UseItem.controls[i].name)
                    {
                        s_UseItemIndex = i;
                    }
                }
            }

            s_ToggleHelp = _playerControls.UI.ToggleHelp.triggered;
            s_RightClick = _playerControls.Debug.RegenerateDungeon.triggered;
            s_Interacts = _playerControls.Player.Interact.triggered;
            s_Waits = _playerControls.Player.Wait.triggered;
            if (_playerControls.Player.Examine.triggered)
            {
                s_IsCheckingTiles = !s_IsCheckingTiles;
                MessageLog.Print(s_IsCheckingTiles ? "You enter Examine Mode." : "You exit Examine Mode.");
                if (!s_IsCheckingTiles)
                {
                    InspectorLog.ClearDescription();
                }
            }



            if (_playerControls.Player.MovePlus.triggered || _playerControls.Player.MoveDiagonal.triggered)
            {
                GetMovementInputs();
            }

            //If we hold the move axis long enough, we set a short delay to set s_IsMoving to true in repeat.
            if (_shouldStartTimers)
            {
                if(_autoMoveDelayTimer < c_autoMoveDelay)
                {
                    _autoMoveDelayTimer += Time.deltaTime;
                }
                else
                {
                    if (_autoMoveIntervalTimer < c_autoMoveInterval)
                    {
                        _autoMoveIntervalTimer += Time.deltaTime;
                    }
                    else
                    {
                        _autoMoveIntervalTimer = 0f;
                        GetMovementInputs();
                    }
                }
            }

            s_IsMoving = s_MoveDirResult != Vector2Int.zero;
        }


        private static void GetMovementInputs()
        {
            s_moveDirPlus = Vector2Int.RoundToInt(_playerControls.Player.MovePlus.ReadValue<Vector2>());
            s_moveDirDiagonal = Vector2Int.RoundToInt(_playerControls.Player.MoveDiagonal.ReadValue<Vector2>());

            //Remaps diagonal input to get diagonal movement vector
            if (s_moveDirDiagonal != Vector2Int.zero)
            {
                s_moveDirDiagonal = s_moveDirDiagonal == new Vector2Int(0, 1) ? Vector2Int.one :            //9 : from up to top right
                                    s_moveDirDiagonal == new Vector2Int(0, -1) ? -Vector2Int.one :          //1 : from down to bottom left
                                    s_moveDirDiagonal == new Vector2Int(-1, 0) ? new Vector2Int(-1, 1) :    //7 : from left to top left
                                                                                 new Vector2Int(1, -1);     //3 : from right to bottom right
            }

            //The result we'll actually use for the movement of the player
            if (s_moveDirPlus == Vector2Int.zero)
            {
                s_MoveDirResult = s_moveDirDiagonal;
            }
            if (s_moveDirDiagonal == Vector2Int.zero)
            {
                s_MoveDirResult = s_moveDirPlus;
            }

        }


        public static string GetInventoryChar(int index)
        {
            return _playerControls.UI.UseItem.controls[index].displayName;
        }

        #endregion
    }
}