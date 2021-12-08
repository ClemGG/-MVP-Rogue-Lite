using Project.Tiles;
using UnityEngine;

namespace Project.Behaviours.Tiles
{
    [CreateAssetMenu(fileName = "Combat Behaviour", menuName = "Rogue/Actors/Behaviours/Tile/Combat")]
    public class CombatTileBehaviour : TileBehaviour
    {
        public override void OnActorCollided(ActorTile attacker, Cell thisCell, Tile thisTile)
        {
            AttackActor(attacker, thisTile);
        }


        private void AttackActor(ActorTile attacker, Tile thisTile)
        {
            //We only engage combat if the target (this) is also an Actor
            if (thisTile is ActorTile)
            {
                CombatSystem.ConductAttack(attacker, thisTile as ActorTile);
            }
        }
    }
}