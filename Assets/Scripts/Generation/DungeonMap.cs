using System.Text;
using UnityEngine;
using TMPro;

using Project.Colors;
using Project.Tiles;
using System.Linq;

namespace Project.Generation
{
    public static class DungeonMap
    {
        public static Cell[,] s_Map { get; set; }
        public static Vector2Int s_Size { get; set; }   //Keeps the map size in memory for future iterations through the array
        private static StringBuilder _stringBuilder { get; set; }
        private static TextMeshProUGUI _mapTextField { get; set; }


        public static void Init(Vector2Int size)
        {
            s_Size = size;
            s_Map = new Cell[s_Size.x, s_Size.y];
            _stringBuilder = new StringBuilder(s_Size.x * s_Size.y);

            _mapTextField ??= GameObject.Find("map").GetComponent<TextMeshProUGUI>();

            //Set Cell Position and fills all of them with Walls
            //We'll then carve up Feature in these Walls in the DungeonPatterns class
            for (int y = 0; y < s_Size.y; y++)
            {
                for (int x = 0; x < s_Size.x; x++)
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
                        if (currentCell.IsInPlayerFov)
                        {
                            symbolColorFOV = topTile.TextColorInFOV.Color;
                            backgroundColorFOV = topTile.BackgroundColorInFOV.Color;
                        }
                        else
                        {
                            //If the topTile isn't supposed to be drawn when out of view (like an Enemy for ex),
                            //We use the topmost Tile with a visible color instead.
                            if (topTile.TextColorOutFOV.Color.Equals(ColorLibrary.None))
                            {
                                topTile = currentCell.Tiles.Last(tile => !tile.TextColorOutFOV.Color.Equals(ColorLibrary.None));    //The the topmost Tile which has a color when out of FOV
                            }

                            symbolColorFOV = topTile.TextColorOutFOV.Color;
                            backgroundColorFOV = topTile.BackgroundColorOutFOV.Color;
                        }
                    }


                    tileAppearance = ColorLibrary.ColoredCharAndBackground(topTile.Symbol, symbolColorFOV, backgroundColorFOV);

                    _stringBuilder.Append(tileAppearance);
                }

                //Not needed anymore since out TMP Component auto wraps
                //and this messes up with GetTileUnderMouse().
                //_stringBuilder.Append("\n");
            }

            _mapTextField.text = _stringBuilder.ToString();
        }


        public static Tile GetTileUnderMouse()
        {
            Tile topTile = null;
            Vector2Int coords = Vector2Int.zero;

            //Having \n characters messes up the characterCount, so we removed them
            int charIndex = TMP_TextUtilities.FindIntersectingCharacter(_mapTextField, UnityEngine.Input.mousePosition, null, true);

            if (charIndex != -1 && charIndex != _mapTextField.textInfo.characterCount)
            {
                coords = new Vector2Int(charIndex % s_Size.x,  charIndex / s_Size.x);
                Cell cellUnderMouse = s_Map[coords.x, coords.y];

                //We only want to examine visible Cells
                if (cellUnderMouse.IsInPlayerFov)
                {
                    topTile = cellUnderMouse.Tiles[cellUnderMouse.Tiles.Count - 1];
                }
            }

            if (topTile != null)
            {
                Debug.Log($"Cell[{coords.x}, {coords.y}] Contains : {topTile.TileName}");
            }
            return topTile;
        }


    }
}