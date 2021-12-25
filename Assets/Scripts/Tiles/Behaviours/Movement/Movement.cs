using Project.Generation;
using Project.Tiles;
using Project.ValueTypes;
using UnityEngine;

namespace Project.Behaviours.Movement
{

    /// <summary>
    /// Describes how the Actor moves in the environment.
    /// This allows to setup different movement patterns for different Actors
    /// (ex: An enemy can move randomly, another can charge in a straight line, etc.)
    /// </summary>
    [CreateAssetMenu(fileName = "New Movement Behaviour", menuName = "Rogue/Actors/Behaviours/Movement")]
    public class Movement : ScriptableObject
    {
        [field: SerializeField]
        public MovementPatternType MovementPattern { get; set; }

        //[field: SerializeField, Tooltip("If, false, the Actor can only move in 4 directions.")]
        //public bool CanMoveIn8Directions { get; set; } = true;


        public void OnTick(ActorTile actor)
        {
            Move(actor);
        }

        protected void Move(ActorTile actor)
        {
            Cell actorCell = DungeonInfo.GetCellAt(actor.Position);
            Cell destCell = null;

            switch (MovementPattern)
            {
                case MovementPatternType.Input:
                    destCell = MovementPatterns.GetInputNextPosition(actor.Position);
                    break;

                case MovementPatternType.Random:
                    destCell = MovementPatterns.GetRandomAdjacentTile(actor.Position);
                    break;

                //case MovementPatternType.Explore:
                //    destCell = MovementPatterns.GetTileClosestToUnexploredFloorTile(actor.Position);
                //    break;

                //case MovementPatternType.Patrol:
                //    destCell = MovementPatterns.GetRandomRoomTile(actor.Position);
                //    break;

                case MovementPatternType.Chase:
                    destCell = MovementPatterns.GoToTile(actor.Position, DungeonInfo.s_Player);
                    break;

                default:
                    break;
            }

            //In case we haven't implemented a pattern method yet
            if (destCell == null) return;

            //If the destination is a Walkable Cell, move the ActorTile from the old Tile to the new one.
            if (destCell.Walkable)
            {
                actorCell.Tiles.Remove(actor);
                destCell.Tiles.Add(actor);
                actor.Position = destCell.Position;

                //If the cells contains a Tile that racts when entered, invoke its method.
                destCell.OnActorEntered(actor);

            }
            else
            {
                //Else, display in the Log what Tile we have bumped into.
                destCell.OnActorCollided(actor);
            }
        }
    }
}