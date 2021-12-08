using Project.Display;
using UnityEngine;
using Project.Tiles;
using Project.ValueTypes;

namespace Project.Behaviours.Tiles
{

    /// <summary>
    /// Describes how the Tile reacts when an Actor interacts with it.
    /// This allows to setup different behaviours for different Tiles
    /// (ex: Non-walkable Tiles will print an error, trap Tiles will affect any Actor walking on it, etc.)
    /// </summary>
    [CreateAssetMenu(fileName = "New Tile Behaviour", menuName = "Rogue/Actors/Behaviours/Tile")]
    public class TileBehaviour : ScriptableObject
    {
        [field: SerializeField]
        private TileBehaviourType BehaviourType { get; set; }
        [field: SerializeField]
        private string TextOnCollision { get; set; }


        public void OnActorEntered(ActorTile attackingActor, Cell thisCell, Tile thisTile)
        {
            switch (BehaviourType)
            {
                case TileBehaviourType.Collision:
                    PrintCollision(attackingActor, thisTile);
                    break;

                case TileBehaviourType.Combat:
                    AttackActor(attackingActor, thisTile);
                    break;

                default:
                    break;
            }
        }

        private void PrintCollision(ActorTile actor, Tile thisTile)
        {
            MessageLog.Print(string.Format(TextOnCollision, actor.TileName, thisTile.TileName));
        }


        private void AttackActor(ActorTile attackingActor, Tile thisTile)
        {
            //We only engage combat if the target (this) is also an Actor
            if(thisTile is ActorTile)
            {
                CombatSystem.ConductAttack(attackingActor, thisTile as ActorTile);
            }
        }
    }
}