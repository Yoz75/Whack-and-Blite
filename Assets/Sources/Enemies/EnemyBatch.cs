using UnityEngine;

namespace WhackAndBlite
{
    public class EnemyBatch : MonoBehaviour
    {
        public int EnemiesCount = -1;

        private void Update()
        {
            if(EnemiesCount == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}