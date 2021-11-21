using UnityEngine;

namespace Project.Actors.Behaviours.Movement
{

    /// <summary>
    /// Describes how the Actor moves in the environment.
    /// This allows to setup different movement patterns for different Actors
    /// (ex: An enemy can move randomly, another can charge in a straight line, etc.)
    /// </summary>
    [CreateAssetMenu(fileName = "New Movement Behaviour", menuName = "Rogue/Actors/Behaviours/Movement")]
    public abstract class Movement : ScriptableObject
    {
        public void OnTick(Vector2Int actorPosition)
        {
            Move(actorPosition);
        }

        private void Move(Vector2Int actorPosition)
        {

        }
    }
}