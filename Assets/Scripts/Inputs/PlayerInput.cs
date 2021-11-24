
using UnityEngine;

namespace Project.Input
{
    public class PlayerInput
    {
        #region Fields

        private static PlayerControls _playerControls { get; set; }

        private const float c_autoMoveDelay = .3f;   //Press duration before setting s_IsMoving to true every c_moveDelay seconds.
        private const float c_autoMoveInterval = .15f;   //Sets s_IsMoving to true once every c_moveDelay seconds instead of just once when pressed.
        private static bool _shouldStartTimers { get; set; }
        private static float _autoMoveDelayTimer { get; set; }
        private static float _autoMoveIntervalTimer { get; set; }

        public static bool s_isMoving { get; private set; }
        public static bool s_interacts { get; private set; }
        public static bool s_rightClick { get; private set; }
        public static Vector2Int s_moveDirResult { get; private set; }
        private static Vector2Int s_moveDirPlus { get; set; }
        private static Vector2Int s_moveDirDiagonal { get; set; }

        #endregion



        #region Methods

        //Called once in the GameManager's Start method
        public static void Init()
        {
            _playerControls = new PlayerControls();
            _playerControls.Enable();

            GetTriggerInputs();
        }


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
            s_moveDirResult = Vector2Int.zero;

            s_rightClick = _playerControls.Debug.RegenerateDungeon.triggered;
            s_interacts = _playerControls.Player.Interact.triggered;

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

            s_isMoving = s_moveDirResult != Vector2Int.zero;
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
                s_moveDirResult = s_moveDirDiagonal;
            }
            if (s_moveDirDiagonal == Vector2Int.zero)
            {
                s_moveDirResult = s_moveDirPlus;
            }

        }

        #endregion
    }
}