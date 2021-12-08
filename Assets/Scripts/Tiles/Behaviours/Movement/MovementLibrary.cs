using System.Reflection;
using UnityEngine;

namespace Project.Behaviours.Movement
{
    public static class MovementLibrary
    {
        //TODO : Use Object Pooling instead of those Instantiate() to recycle the Tiles instead of wasting memory on recreating them.
        public static Movement PlayerMovement { get { return Object.Instantiate(Resources.Load<Movement>("Behaviours/Movement/PlayerMovement")); } }
        public static Movement RatMovement { get { return Object.Instantiate(Resources.Load<Movement>("Behaviours/Movement/RatMovement")); } }


        private static PropertyInfo[] _fieldsNames
        {
            get
            {
                return _fields ??= typeof(MovementLibrary).GetProperties();
            }
        }
        private static PropertyInfo[] _fields;


        //This method uses the actor's name to retrieve the actual property field sharing the name name in the format {name}FOV
        public static Movement GetMovement(string actorName)
        {
            string statsName = $"{actorName}Movement";
            for (int i = 0; i < _fieldsNames.Length; i++)
            {
                if (statsName == _fieldsNames[i].Name)
                {
                    return (Movement)_fieldsNames[i].GetValue(_fieldsNames[i]);
                }
            }

            return null;
        }

    }
}
