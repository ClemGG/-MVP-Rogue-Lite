using Project.Tiles;
using UnityEngine;

namespace Project.Behaviours.AI
{
    public abstract class EnemyAI : ScriptableObject
    {
        public abstract void OnTick(ActorTile actor);
    }
}