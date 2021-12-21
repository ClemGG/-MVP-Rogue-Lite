using Project.Colors;
using Project.Input;
using Project.Items;
using Project.Logic;
using TMPro;
using UnityEngine;

namespace Project.Display
{
    public static class InventoryLog
    {

        #region Private Accessors

        private static RectTransform ItemListParent
        {
            get
            {
                return _p = _p != null ? _p : GameObject.Find("inventory content").GetComponent<RectTransform>();
            }
        }
        private static RectTransform _p;

        private static GameObject[] ItemListInUI
        {
            get
            {
                if (_ils == null)
                {
                    _ils = new GameObject[GameSystem.c_MaxInventorySize];
                    for (int i = 0; i < GameSystem.c_MaxInventorySize; i++)
                    {
                        _ils[i] = ItemListParent.GetChild(i).gameObject;
                        _ils[i].SetActive(false);
                    }
                }

                return _ils;
            }
        }
        private static GameObject[] _ils;

        #endregion


        #region Public Methods

        //Called each time an Item is added or removed from the Inventory
        public static void DisplayItems()
        {
            for (int i = 0; i < Inventory.s_Items.Length; i++)
            {
                if (Inventory.s_Items[i].Count > 0)
                {
                    ItemTile item = Inventory.s_Items[i].Peek();
                    TextMeshProUGUI textLine = ItemListInUI[i].GetComponent<TextMeshProUGUI>();
                    string amount = item.IsStackable ? $" x{Inventory.s_Items[i].Count}" : null;
                    textLine.text = $"{ColorLibrary.ColoredText(PlayerInput.GetInventoryChar(i), ColorLibrary.InventoryChar)}: {ColorLibrary.ColoredText(item.TileName + amount, item.TextColorInFOV.Color)}";
                    ItemListInUI[i].SetActive(true);
                }
                else
                {
                    ItemListInUI[i].SetActive(false);
                }

            }
        }


        #endregion

    }
}