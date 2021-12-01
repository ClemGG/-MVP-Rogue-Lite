using Project.Colors;
using Project.Tiles;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

namespace Project.Display
{
    public static class MapLog
    {
        private static StringBuilder _stringBuilder 
        {
            get 
            {
                return _sb ??= new StringBuilder(_mapSize.x * _mapSize.y);
            }
        }
        private static StringBuilder _sb;
        private static TextMeshProUGUI _mapTextField 
        { 
            get 
            { 
                return _mapField ??= GameObject.Find("map").GetComponent<TextMeshProUGUI>(); 
            } 
            set => _mapField = value; 
        }
        private static TextMeshProUGUI _mapField;

        //Temporary variables
        private static Vector2Int _mapSize;
        private static Cell[,] _cells;
        private static int _lastCharIndex = -1;
        private static Vector3 _lastMousePos;
        private static Tile _lastExaminedTile = null;


        public static void Draw(Vector2Int mapSize, Cell[,] cells)
        {
            _mapSize = mapSize;
            _cells = cells;
            _stringBuilder.Clear();

            for (int y = 0; y < mapSize.y; y++)
            {
                for (int x = 0; x < mapSize.x; x++)
                {
                    Cell currentCell = cells[x, y];
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
            //These _last checks make sure the code runs only once per char to avoid uneccessary computations
            if (_lastMousePos != UnityEngine.Input.mousePosition)
            {
                _lastMousePos = UnityEngine.Input.mousePosition;
                Vector2Int coords = Vector2Int.zero;

                //Having \n characters messes up the characterCount, so we removed them
                int charIndex = TMP_TextUtilities.FindIntersectingCharacter(_mapTextField, UnityEngine.Input.mousePosition, null, true);

                if (_lastCharIndex != charIndex)
                {
                    _lastCharIndex = charIndex;

                    if (charIndex != -1 && charIndex != _mapTextField.textInfo.characterCount)
                    {
                        coords = new Vector2Int(charIndex % _mapSize.x, charIndex / _mapSize.x);
                        Cell cellUnderMouse = _cells[coords.x, coords.y];

                        //We only want to examine visible Cells
                        if (cellUnderMouse.IsInPlayerFov)
                        {
                            _lastExaminedTile = cellUnderMouse.Tiles[cellUnderMouse.Tiles.Count - 1];
                        }
                    }
                    else
                    {
                        _lastExaminedTile = null;
                    }

                    if (_lastExaminedTile != null)
                    {
                        Debug.Log($"Cell[{coords.x}, {coords.y}] Contains : {_lastExaminedTile.TileName}");
                    }
                }
            }

            return _lastExaminedTile;
        }

    }
}