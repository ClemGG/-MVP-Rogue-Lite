
using System;
using UnityEngine;

namespace Project.Map
{

    /// <summary>
    /// A struct that defines a rectangle
    /// </summary>
    [Serializable]
    public struct Rectangle : IEquatable<Rectangle>
    {
        /// <summary>
        /// Specifies the Coordinates of the Rectangle (x and y at 0 being the top left)
        /// </summary>
        public Vector2Int Position { get; set; }

        /// <summary>
        /// Specifies the Width and Height of the Rectangle
        /// </summary>
        public Vector2Int Size { get; set; }


        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="Rectangle"/> struct, with the specified
        /// position, width, and height.
        /// </summary>
        /// <param name="x">The x coordinate of the top-left corner of the created <see cref="Rectangle"/>.</param>
        /// <param name="y">The y coordinate of the top-left corner of the created <see cref="Rectangle"/>.</param>
        /// <param name="width">The width of the created <see cref="Rectangle"/>.</param>
        /// <param name="height">The height of the created <see cref="Rectangle"/>.</param>
        public Rectangle(int x, int y, int width, int height)
           : this()
        {
            Position = new Vector2Int(x, y);
            Size = new Vector2Int(width, height);
        }

        /// <summary>
        /// Creates a new instance of <see cref="Rectangle"/> struct, with the specified
        /// location and size.
        /// </summary>
        /// <param name="position">The x and y coordinates of the top-left corner of the created <see cref="Rectangle"/>.</param>
        /// <param name="size">The width and height of the created <see cref="Rectangle"/>.</param>
        public Rectangle(Vector2Int position, Vector2Int size)
           : this()
        {
            Position = position;
            Size = size;
        }

        #endregion

        /// <summary>
        /// Returns a Rectangle with all of its values set to zero
        /// </summary>
        public static Rectangle Empty { get; } = new Rectangle();

        /// <summary>
        /// Returns the x-coordinate of the left side of the rectangle
        /// </summary>
        public int Left => Position.x;

        /// <summary>
        /// Returns the x-coordinate of the right side of the rectangle
        /// </summary>
        public int Right => Position.x + Size.x;

        /// <summary>
        /// Returns the y-coordinate of the top of the rectangle
        /// </summary>
        public int Top => Position.y;

        /// <summary>
        /// Returns the y-coordinate of the bottom of the rectangle
        /// </summary>
        public int Bottom => Position.y + Size.y;


        /// <summary>
        /// Returns the Vector2Int that specifies the center of the rectangle
        /// </summary>
        public Vector2Int Center => new Vector2Int(Position.x + Size.x / 2, Position.y + Size.y / 2);

        /// <summary>
        /// Returns a value that indicates whether the Rectangle is empty
        /// true if the Rectangle is empty; otherwise false
        /// </summary>
        public bool IsEmpty => Size.x == 0 && Size.y == 0 && Position.x == 0 && Position.y == 0;

        /// <summary>
        /// Determines whether two Rectangle instances are equal
        /// </summary>
        /// <param name="other">The Rectangle to compare this instance to</param>
        /// <returns>True if the instances are equal; False otherwise</returns>
        public bool Equals(Rectangle other)
        {
            return this == other;
        }

        /// <summary>
        /// Compares two rectangles for equality
        /// </summary>
        /// <param name="a">Rectangle on the left side of the equals sign</param>
        /// <param name="b">Rectangle on the right side of the equals sign</param>
        /// <returns>True if the rectangles are equal; False otherwise</returns>
        public static bool operator ==(Rectangle a, Rectangle b)
        {
            return a.Position.x == b.Position.x && a.Position.y == b.Position.y && a.Size.x == b.Size.x && a.Size.y == b.Size.y;
        }

        /// <summary>
        /// Determines whether this Rectangle contains a specified point represented by its x and y-coordinates
        /// </summary>
        /// <param name="x">The x-coordinate of the specified point</param>
        /// <param name="y">The y-coordinate of the specified point</param>
        /// <returns>True if the specified point is contained within this Rectangle; False otherwise</returns>
        public bool Contains(int x, int y)
        {
            return Position.x <= x && x < x + Size.x && Position.y <= y && y < y + Size.y;
        }

        /// <summary>
        /// Determines whether this Rectangle contains a specified Vector2Int
        /// </summary>
        /// <param name="value">The Vector2Int to evaluate</param>
        /// <returns>True if the specified Vector2Int is contained within this Rectangle; False otherwise</returns>
        public bool Contains(Vector2Int value)
        {
            return Position.x <= value.x && value.x < Position.x + Size.x && Position.y <= value.y && value.y < Position.y + Size.y;
        }

        /// <summary>
        /// Determines whether this Rectangle entirely contains the specified Rectangle
        /// </summary>
        /// <param name="value">The Rectangle to evaluate</param>
        /// <returns>True if this Rectangle entirely contains the specified Rectangle; False otherwise</returns>
        public bool Contains(Rectangle value)
        {
            return Position.x <= value.Position.x && value.Position.x + value.Size.x <= Position.x + Size.x && Position.y <= value.Position.y
                     && value.Position.y + value.Size.y <= Position.y + Size.y;
        }

        /// <summary>
        /// Compares two rectangles for inequality
        /// </summary>
        /// <param name="a">Rectangle on the left side of the equals sign</param>
        /// <param name="b">Rectangle on the right side of the equals sign</param>
        /// <returns>True if the rectangles are not equal; False otherwise</returns>
        public static bool operator !=(Rectangle a, Rectangle b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Changes the position of the Rectangles by the values of the specified Vector2Int
        /// </summary>
        /// <param name="offsetPoint">The values to adjust the position of the Rectangle by</param>
        public void Offset(Vector2Int offsetPoint)
        {
            Position = new Vector2Int(Position.x + offsetPoint.x, Position.y + offsetPoint.y);
        }

        /// <summary>
        /// Changes the position of the Rectangle by the specified x and y offsets
        /// </summary>
        /// <param name="offsetX">Change in the x-position</param>
        /// <param name="offsetY">Change in the y-position</param>
        public void Offset(int offsetX, int offsetY)
        {
            Position = new Vector2Int(Position.x + offsetX, Position.y + offsetY);
        }

        /// <summary>
        /// Pushes the edges of the Rectangle out by the specified horizontal and vertical values
        /// </summary>
        /// <param name="horizontalValue">Value to push the sides out by</param>
        /// <param name="verticalValue">Value to push the top and bottom out by</param>
        /// <exception cref="OverflowException">Thrown if the new width or height exceed Int32.MaxValue, or new x or y are smaller than int32.MinValue</exception>
        public void Inflate(int horizontalValue, int verticalValue)
        {
            Position = new Vector2Int(Position.x - horizontalValue, Position.y - verticalValue);
            Size = new Vector2Int(Size.x + horizontalValue * 2, Size.y + verticalValue * 2);
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return obj is Rectangle && this == (Rectangle)obj;
        }

        /// <summary>
        /// Returns a string that represents the current Rectangle
        /// </summary>
        /// <returns>A string that represents the current Rectangle</returns>
        public override string ToString()
        {
            return $"{{x:{Position.x} y:{Position.y} Width:{Size.x} Height:{Size.y}}}";
        }

        /// <summary>
        /// Gets the hash code for this object which can help for quick checks of equality
        /// or when inserting this Rectangle into a hash-based collection such as a Dictionary or Hashtable 
        /// </summary>
        /// <returns>An integer hash used to identify this Rectangle</returns>
        public override int GetHashCode()
        {
            return Position.x ^ Position.y ^ Size.x ^ Size.y;
        }

        /// <summary>
        /// Determines whether this Rectangle intersects with the specified Rectangle
        /// </summary>
        /// <param name="value">The Rectangle to evaluate</param>
        /// <returns>True if the specified Rectangle intersects with this one; False otherwise</returns>
        public bool Intersects(Rectangle value)
        {
            return value.Left < Right &&
                   Left < value.Right &&
                   value.Top < Bottom &&
                   Top < value.Bottom;
        }

        /// <summary>
        /// Determines whether this Rectangle intersects with the specified Rectangle
        /// </summary>
        /// <param name="value">The Rectangle to evaluate</param>
        /// <param name="result">True if the specified Rectangle intersects with this one; False otherwise</param>
        public void Intersects(ref Rectangle value, out bool result)
        {
            result = value.Left < Right &&
                     Left < value.Right &&
                     value.Top < Bottom &&
                     Top < value.Bottom;
        }

        /// <summary>
        /// Creates a Rectangle defining the area where one Rectangle overlaps with another Rectangle
        /// </summary>
        /// <param name="value1">The first Rectangle to compare</param>
        /// <param name="value2">The second Rectangle to compare</param>
        /// <returns>The area where the two specified Rectangles overlap. If the two Rectangles do not overlap the resulting Rectangle will be Empty</returns>
        public static Rectangle Intersect(Rectangle value1, Rectangle value2)
        {
            Intersect(ref value1, ref value2, out Rectangle rectangle);
            return rectangle;
        }

        /// <summary>
        /// Creates a Rectangle defining the area where one Rectangle overlaps with another Rectangle
        /// </summary>
        /// <param name="value1">The first Rectangle to compare</param>
        /// <param name="value2">The second Rectangle to compare</param>
        /// <param name="result">The area where the two specified Rectangles overlap. If the two Rectangles do not overlap the resulting Rectangle will be Empty</param>
        public static void Intersect(ref Rectangle value1, ref Rectangle value2, out Rectangle result)
        {
            if (value1.Intersects(value2))
            {
                int rightSide = Math.Min(value1.Position.x + value1.Size.x, value2.Position.x + value2.Size.x);
                int leftSide = Math.Max(value1.Position.x, value2.Position.x);
                int topSide = Math.Max(value1.Position.y, value2.Position.y);
                int bottomSide = Math.Min(value1.Position.y + value1.Size.y, value2.Position.y + value2.Size.y);
                result = new Rectangle(leftSide, topSide, rightSide - leftSide, bottomSide - topSide);
            }
            else
            {
                result = new Rectangle(0, 0, 0, 0);
            }
        }

        /// <summary>
        /// Creates a new Rectangle that exactly contains the specified two Rectangles
        /// </summary>
        /// <param name="value1">The first Rectangle to contain</param>
        /// <param name="value2">The second Rectangle to contain</param>
        /// <returns>A new Rectangle that exactly contains the specified two Rectangles</returns>
        public static Rectangle Union(Rectangle value1, Rectangle value2)
        {
            int x = Math.Min(value1.Position.x, value2.Position.x);
            int y = Math.Min(value1.Position.y, value2.Position.y);
            return new Rectangle(x, y, Math.Max(value1.Right, value2.Right) - x, Math.Max(value1.Bottom, value2.Bottom) - y);
        }

        /// <summary>
        /// Creates a new <see cref="Rectangle"/> that completely contains two other rectangles.
        /// </summary>
        /// <param name="value1">The first <see cref="Rectangle"/>.</param>
        /// <param name="value2">The second <see cref="Rectangle"/>.</param>
        /// <param name="result">The union of the two rectangles as an output parameter.</param>
        public static void Union(ref Rectangle value1, ref Rectangle value2, out Rectangle result)
        {
            result = Union(value1, value2);
        }
    }
}