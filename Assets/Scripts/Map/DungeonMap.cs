using System.Text;
using UnityEngine;
using TMPro;

using Project.Colors;

namespace Project.Map
{
    public static class DungeonMap
    {
        public static Cell[,] s_Map { get; set; }
        public static Vector2Int s_Size { get; set; }   //Keeps the map size in memory for future iterations through the array
        private static StringBuilder _stringBuilder { get; set; }
        private static TextMeshProUGUI _mapTextField { get; set; }
        

        public static void Init(int xSize, int ySize)
        {
            s_Size = new Vector2Int(xSize, ySize);
            s_Map = new Cell[xSize, ySize];
            _stringBuilder = new StringBuilder(xSize * ySize);

            _mapTextField ??= GameObject.Find("map").GetComponent<TextMeshProUGUI>();

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
        /// If we don't call Init() before a Generate(), use this to clean the Map before filling the Cells with new Tiles
        /// </summary>
        public static void Clear()
        {
            if (s_Map != null)
            {
                foreach (Cell cell in s_Map)
                {
                    cell.Clear();
                    cell.Tiles.Add(TileLibrary.Wall);
                }
            }
        }

        public static void Draw()
        {
            _stringBuilder.Clear();

            for (int y = 0; y < s_Size.y; y++)
            {
                for (int x = 0; x < s_Size.x; x++)
                {
                    Cell currentCell = s_Map[x, y];
                    Tile topTile = currentCell.Tiles[currentCell.Tiles.Count - 1];
                    string tileAppearance;
                    Color32 symbolColorFOV;
                    Color32 backgroundColorFOV;

                    //If the Player hasn't seen this Cell yet, we don't draw it
                    if (!currentCell.IsExplored)
                    {
                        symbolColorFOV = ColorLibrary.None;
                        backgroundColorFOV = ColorLibrary.None;
                    }
                    else
                    {
                        //Swaps the color is the Cell is visible or not
                        if (currentCell.IsInFov)
                        {
                            symbolColorFOV = topTile.TextColorInFOV.Color;
                            backgroundColorFOV = topTile.BackgroundColorInFOV.Color;
                        }
                        else
                        {
                            symbolColorFOV = topTile.TextColorOutFOV.Color;
                            backgroundColorFOV = topTile.BackgroundColorOutFOV.Color;
                        }
                    }


                    tileAppearance = ColorLibrary.ColoredCharAndBackground(topTile.Symbol, symbolColorFOV, backgroundColorFOV);

                    _stringBuilder.Append(tileAppearance);
                }

                _stringBuilder.Append("\n");
            }

            _mapTextField.text = _stringBuilder.ToString();
        }
    }
}