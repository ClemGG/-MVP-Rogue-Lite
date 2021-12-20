using Project.Display;
using Project.Tiles;
using UnityEngine;

namespace Project.Behaviours.Tiles
{
    [CreateAssetMenu(fileName = "Collision Behaviour", menuName = "Rogue/Actors/Behaviours/Tile/Collision")]
    public class CollisionTileBehaviour : TileBehaviour
    {

        [field: SerializeField]
        private string TextOnCollision { get; set; }

        public override void OnActorCollided(ActorTile other, Cell thisCell, Tile thisTile)
        {
            PrintCollision(other, thisTile);
        }


        private void PrintCollision(ActorTile other, Tile thisTile)
        {
            MessageLog.Print(string.Format(TextOnCollision, other.TileName, thisTile.TileName));
        }
    }
}