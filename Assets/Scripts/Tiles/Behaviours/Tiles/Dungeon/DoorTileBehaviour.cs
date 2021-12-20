using Project.Tiles;
using UnityEngine;

namespace Project.Behaviours.Tiles
{
    [CreateAssetMenu(fileName = "Door Behaviour", menuName = "Rogue/Actors/Behaviours/Tile/Door")]
    public class DoorTileBehaviour : TileBehaviour
    {
        private bool IsOpen { get; set; }

        public override void OnActorEntered(ActorTile other, Cell thisCell, Tile thisTile)
        {
            OpenDoor(thisTile);
        }

        private void OpenDoor(Tile thisTile)
        {
            IsOpen = false;
            thisTile.Symbol = '_';
            thisTile.Description = "An opened door.";
            thisTile.SeeThrough = true;
        }
    }
}