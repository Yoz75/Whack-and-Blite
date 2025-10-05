using CSC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackAndBlite
{

    public class EnemySpawner : MonoBehaviour
    {
        [Serializable] struct EnemyBatchSettings : IWeighted
        {
            public int Weight_;
            public float Margin;
            public EnemyProps[] Enemies;

            public int Weight => Weight_;
        }
        [SerializeField] private List<Transform> SpawnPoints;
        [SerializeField] private List<EnemyBatchSettings> EnemyBatches;
        [SerializeField] private float WaitTime, TimeError;
        [SerializeField] private GameObject BatchTemplate, EnemyTemplate;
        [SerializeField] private Player Target;

        private void Start()
        {
            StartCoroutine(SpawnCoroutine());

            GameState.Instance.StateChanged += (state) =>
            {
                if(state == State.Defeat)
                {
                    Destroy(gameObject);
                }
            };
        }

        IEnumerator SpawnCoroutine()
        {
            while(true)
            {
                Transform spawnPoint = SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Count)];

                float waitTime = WaitTime + UnityEngine.Random.Range(-1, 1) * TimeError * UnityEngine.Random.value;
                waitTime /= GameDifficulty.Instance.Difficuly;
                yield return new WaitForSeconds(waitTime);

                var batchSettings = WeightedSelect<List<EnemyBatchSettings>, EnemyBatchSettings>.
                    SelectRandom(EnemyBatches);

                var batchObject = Instantiate(BatchTemplate, spawnPoint);
                batchObject.transform.localPosition = Vector3.zero;
                batchObject.transform.localRotation = Quaternion.identity;
                batchObject.GetComponent<EnemyBatch>().EnemiesCount = batchSettings.Enemies.Length;
                batchObject.name = "EnemiesBatch";


                float totalMargin = batchSettings.Margin;
                foreach(EnemyProps enemy in batchSettings.Enemies)
                {
                    var enemyObject = Instantiate(EnemyTemplate, batchObject.transform);

                    enemyObject.transform.localPosition = Vector3.zero;
                    enemyObject.transform.localRotation = Quaternion.identity;
                    enemyObject.transform.localPosition += Vector3.up * totalMargin;

                    var enemyComponent = enemyObject.GetComponent<Enemy>();
                    enemyComponent.SetProps(enemy);
                    enemyComponent.Heal(float.PositiveInfinity);
                    enemyComponent.Target = Target;

                    totalMargin += batchSettings.Margin;
                }
            }
        }
    }
}
