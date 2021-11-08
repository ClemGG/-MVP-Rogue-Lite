using System;
using UnityEngine;

namespace Project.Colors
{
    [Serializable]
    public struct ColorInPalette
    {
        [Tooltip("The tag used to retrieve the color automatically from Colors.cs by reflection.")]
        public int ColorName;

        public Color32 Color;
    }
}