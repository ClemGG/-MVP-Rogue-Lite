using Project.Display;
using Project.Tiles;

namespace Project.Behaviours.Tiles
{
    public abstract class ItemTileBehaviour : TileBehaviour
    {
        public override void OnActorEntered(ActorTile other, Cell thisCell, Tile thisTile)
        {
            if (other is PlayerTile)
            {
                MessageLog.Print($"The Player walked on a {thisTile.TileName}.");
            }
        }

        public override void OnActorInteracted(PlayerTile player, Cell thisCell, Tile thisTile)
        {
            //TODO: Implement Inventory, add item to Inventory if possible, leave it otherwise.

            //MessageLog.Print($"You added a {thisTile.TileName} to your inventory.");
            //MessageLog.Print($"No free slot left in your inventory; you left the {thisTile.TileName} where you found it.");
            //thisCell.Tiles.Remove(thisTile);
        }

        public virtual void OnItemConsumed(PlayerTile player, Cell thisCell, Tile thisTile)
        {
            //When consumed, remove this Item from the Inventory and apply its effects to the Player
        }
    }
}