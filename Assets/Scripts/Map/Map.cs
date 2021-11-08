using System.Text;
using UnityEngine;
using TMPro;

using Project.Colors;

namespace Project.Map
{
    public class Map
    {
        public static Cell[,] s_Map { get; set; }
        private static StringBuilder s_stringBuilder { get; set; }

        //Keeps the map size in memory for future iterations through the array
        public static Vector2Int s_Size;

        public static void Init(int xSize, int ySize)
        {
            s_Size = new Vector2Int(xSize, ySize);
            s_Map = new Cell[xSize, ySize];
            s_stringBuilder = new StringBuilder(xSize * ySize);


            //Set Cell Position and fills all of them with Walls
            //We'll then carve up Feature in these Walls in the DungeonPatterns class
            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    s_Map[x, y] = new Cell();
                    s_Map[x, y].Position = new Vector2Int(x, y);
                    s_Map[x, y].Tiles.Add(TileLibrary.Wall);
                }
            }
        }

        /// <summary>
        /// Attemps to retrieve the last Tile added to the Cell in [x,y].
        /// </summary>
        /// <returns></returns>
        public static bool TryGetTopTile(int x, int y, out Tile tile)
        {
            bool hasATile = s_Map[x, y].Tiles.Count > 0;

            if (hasATile)
            {
                tile = s_Map[x, y].Tiles[s_Map[x, y].Tiles.Count - 1];
            }
            else
            {
                tile = null;
            }

            return hasATile;
        }

        /// <summary>
        /// Attemps to retrieve the last Tile added to the Cell in [x,y].
        /// </summary>
        /// <returns></returns>
        public static bool TryGetTopTile(Vector2Int position, out Tile tile)
        {
            bool hasATile = s_Map[position.x, position.y].Tiles.Count > 0;

            if (hasATile)
            {
                tile = s_Map[position.x, position.y].Tiles[s_Map[position.x, position.y].Tiles.Count - 1];
            }
            else
            {
                tile = null;
            }

            return hasATile;
        }

        public static void Draw(TextMeshProUGUI textField)
        {
            s_stringBuilder.Clear();


            for (int y = 0; y < s_Size.y; y++)
            {
                for (int x = 0; x < s_Size.x; x++)
                {
                    Cell currentCell = s_Map[x, y];

                    string tileAppearance;
                    Color32 symbolColorFOV;
                    Color32 backgroundColorFOV;

                    ///IMPORTANT : Uncomment this once we have implemented the FOV and the player

                    //If the Player hasn't seen this Cell yet, we don't draw it
                    //if (!currentCell.IsExplored)
                    //{
                    //    tileAppearance = " ";
                    //    s_stringBuilder.Append(tileAppearance);
                    //    continue;
                    //}

                    // Retrieves the symbol and colors of the last Tile added to this Cell
                    if (TryGetTopTile(x, y, out Tile topSymbol))
                    {

                        //Swaps the color is the Cell is visible or not
                        if (currentCell.IsInFov)
                        {
                            symbolColorFOV = topSymbol.TextColorInFOV.Color;
                            backgroundColorFOV = topSymbol.BackgroundColorInFOV.Color;
                        }
                        else
                        {
                            symbolColorFOV = topSymbol.TextColorOutFOV.Color;
                            backgroundColorFOV = topSymbol.BackgroundColorOutFOV.Color;
                        }


                        tileAppearance = ColorLibrary.ColoredCharAndBackground(topSymbol.Symbol, symbolColorFOV, backgroundColorFOV);
                    }
                    else
                    {
                        tileAppearance = " ";
                    }

                    s_stringBuilder.Append(tileAppearance);
                }

                s_stringBuilder.Append("\n");
            }

            textField.text = s_stringBuilder.ToString();
        }
    }
}