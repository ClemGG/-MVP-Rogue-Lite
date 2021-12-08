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
        [field: SerializeField] private MovementPatternType _movementPattern { get; set; }
        private (Cell actorCell, Cell destCell) cells { get; set; } = (null, null);


        public void OnTick(ActorTile actor)
        {
            Move(actor);
        }

        protected void Move(ActorTile actor)
        {
            switch (_movementPattern)
            {
                case MovementPatternType.Input:
                    cells = MovementPatterns.GetInputNextPosition(actor.Position);
                    break;

                case MovementPatternType.Random:
                    cells = MovementPatterns.GetRandomAdjacentTile(actor.Position);
                    break;

                case MovementPatternType.Explore:
                    cells = MovementPatterns.GetTileClosestToUnexploredFloorTile(actor.Position);
                    break;

                default:
                    break;
            }

            //In case we haven't implemented a pattern method yet
            if (cells.Equals((null, null))) return;

            //If the destination is a Walkable Cell, move the ActorTile from the old Tile to the new one.
            if (cells.destCell.Walkable)
            {
                cells.actorCell.Tiles.Remove(actor);
                cells.destCell.Tiles.Add(actor);
                actor.Position = cells.destCell.Position;

                //If the cells contains a Tile that racts when entered, invoke its method.
                cells.destCell.OnActorEntered(actor);

            }
            else
            {
                //Else, display in the Log what Tile we have bumped into.
                cells.destCell.OnActorCollided(actor);
            }
        }
    }
}