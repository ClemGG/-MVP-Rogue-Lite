using Project.Behaviours.Tiles;
using Project.Display;
using Project.Generation;
using Project.Logic;
using System.Collections.Generic;
using System.Linq;

namespace Project.Items
{
    public static class Inventory
    {
        #region Accessors

        public static Stack<ItemTile>[] s_Items { get; internal set; }

        #endregion

        #region Public Methods

        public static void Init()
        {
            s_Items = new Stack<ItemTile>[GameSystem.c_MaxInventorySize];
            for (int i = 0; i < s_Items.Length; i++)
            {
                s_Items[i] = new Stack<ItemTile>(GameSystem.c_MaxInventoryStackSize);
            }
        }

        public static void AddItem(ItemTile item)
        {
            //We take the first available stack similar to our item
            Stack<ItemTile> freeStack = s_Items.FirstOrDefault(stack => stack.Count > 0 && stack.Peek().TileName == item.TileName && item.IsStackable && stack.Count < GameSystem.c_MaxInventoryStackSize);

            //If we didn't find one, we take the first free stack
            if (freeStack == null)
            {
                freeStack = s_Items.FirstOrDefault(stack => stack.Count == 0);

            }
            //Then, if we find a free stack, add the item to that stack, regardless of if the stack is empty or similar.
            //Since we use HasRoomLeftForItem() beforehand to determine if we can add an item, freeStack should never be null.
            if (freeStack != null)
            {
                freeStack.Push(item);
                InventoryLog.DisplayItems();
            }
        }

        //When the corresponding key is pressed, use the item at the designated location and remove it.
        public static void UseItem(int index)
        {

            //If the item is already present and there's enough space in the stack, add it to the stack.
            if (s_Items[index].Count > 0)
            {
                ItemTile item = s_Items[index].Peek();
                if (item.IsConsumable)
                {
                    (item.TileBehaviour as ItemTileBehaviour).OnItemConsumed(DungeonInfo.s_Player, item);
                    s_Items[index].Pop();
                    InventoryLog.DisplayItems();
                }
            }
        }

        public static bool HasRoomLeftForItem(ItemTile item)
        {
            bool free = false;

            for (int i = 0; i < s_Items.Length; i++)
            {
                if (s_Items[i].Count > 0)
                {
                    ItemTile peekedItem = s_Items[i].Peek();
                    //If the item is already present and there's enough space in the stack
                    if (s_Items[i].Count < GameSystem.c_MaxInventoryStackSize && peekedItem.IsStackable && peekedItem.TileName == item.TileName)
                    {
                        free = true;
                        break;
                    }
                }
                //If peekedItem = null, then the stack is empty and valid
                else
                {
                    free = true;
                    break;
                }
            }

            return free;
        }


        #endregion
    }
}