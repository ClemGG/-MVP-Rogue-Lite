using Project.Tiles;
using System;
using UnityEngine;

namespace Project.Behaviours.AI
{
    [CreateAssetMenu(fileName = "Standard Enemy AI", menuName = "Rogue/Actors/Behaviours/AI/Standard Enemy AI")]
    public class StandardEnemyAI : EnemyAI
    {
        [field: SerializeField, Tooltip("After this number of turns, if the Player is out of view and this enemy is chasing him, return to patrol mode.")] 
        private int NbChaseTurnsBeforePatrol { get; set; } = 3;
        private int _curNbTurns;


        public override void OnTick(ActorTile thisActor)
        {
            ComputeState(thisActor);
        }

        private void ComputeState(ActorTile thisActor)
        {

            //The Enemy starts in a patrol state.
            //If the Enemy has spotted the Player, it will chase him and attack him.
            if (thisActor.Fov.IsPlayerInFOV())
            {
                if (thisActor.Movement.MovementPattern != ValueTypes.MovementPatternType.Chase)
                    Debug.Log($"The {thisActor.TileName} has spotted you.");

                thisActor.Movement.MovementPattern = ValueTypes.MovementPatternType.Chase;
                _curNbTurns = NbChaseTurnsBeforePatrol;
            }
            else
            {
                //Otherwise, if the Player stays out of the Enemy's FOV for a certain amount of turns,
                //the Enemy reverts back to its patrol state.
                if (_curNbTurns <= 0)
                {

                    if (thisActor.Movement.MovementPattern != ValueTypes.MovementPatternType.Random)
                        Debug.Log($"The {thisActor.TileName} has lost you.");

                    thisActor.Movement.MovementPattern = ValueTypes.MovementPatternType.Random;
                }
                else
                {
                    _curNbTurns--;
                }
            }
        }
    }
}