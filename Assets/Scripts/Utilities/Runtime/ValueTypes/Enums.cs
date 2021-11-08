using System;

namespace Project.Utilities.ValueTypes
{
    public static class Enums
    {
        public static T[] ValuesOf<T>() where T : Enum
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        public static int LengthOf<T>() where T : Enum
        {
            return ValuesOf<T>().Length;
        }

        public static void ForEach<T>(Action<T> action) where T : Enum
        {
            foreach (T value in ValuesOf<T>())
            {
                action.Invoke(value);
            }
        }

        public static T RandomIn<T>() where T : Enum
        {
            return ValuesOf<T>()[UnityEngine.Random.Range(0, LengthOf<T>())];
        }
    }
}