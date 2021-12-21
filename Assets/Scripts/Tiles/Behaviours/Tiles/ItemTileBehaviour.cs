using Project.Display;
using Project.Items;
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
            ItemTile item = thisTile as ItemTile;
            if(Inventory.HasRoomLeftForItem(item))
            {
                Inventory.AddItem(thisTile as ItemTile);
                thisCell.Tiles.Remove(thisTile);
                MessageLog.Print($"You added a {item.TileName} to your inventory.");
            }
            else
            {
                MessageLog.Print($"No free slot left in your inventory; you left the {item.TileName} where you found it.");
            }
        }

        public virtual void OnItemConsumed(PlayerTile player, ItemTile thisTile)
        {
            //When consumed, remove this Item from the Inventory and apply its effects to the Player
        }
    }
}