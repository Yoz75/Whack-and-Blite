using UnityEngine;

namespace WhackAndBlite
{
    [CreateAssetMenu(menuName = "WnB/Enemy Props")]
    public class EnemyProps : EntityProps
    {
        public Sprite Sprite;
        public float Speed;
        public long KillScore;
        public bool ShouldWatchPlayer;
    }
}