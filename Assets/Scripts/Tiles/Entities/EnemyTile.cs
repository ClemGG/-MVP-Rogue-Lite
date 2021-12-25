using Project.Behaviours.AI;
using UnityEngine;

namespace Project.Tiles
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Rogue/Entities/Actors/Enemy")]
    public class EnemyTile : ActorTile
    {


        [field: Tooltip("The stats of the Actor (health, strength).")]
        public EnemyAI AI
        {
            get
            {
                //Auto-retrieves an ActorStats using the TileName.
                return _aiTemplate = _aiTemplate != null ? _aiTemplate : AILibrary.GetAI(TileName);
            }
            set
            {
                _aiTemplate = value;
            }
        }
        private EnemyAI _aiTemplate;


        public override void OnTick()
        {
            //AI is called before Movement to allow the AI to change the movement pattern
            //depending on its state (if its chasing the player or not, etc.)
            if (AI) AI.OnTick(this);
            base.OnTick();
        }

    }
}