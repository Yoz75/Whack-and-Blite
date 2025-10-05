using TMPro;
using System.Collections;
using UnityEngine;

namespace WhackAndBlite
{
    [RequireComponent(typeof(TMP_Text))]
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private Player Player;
        [SerializeField] private long PointsPerIteration;
        [SerializeField] private float AnimationTime;

        private TMP_Text Text;
        private long Score, TargetScore;

        private void Start()
        {
            Text = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            if(Player.Score != Score)
            {
                StartCoroutine(AddScoreCoroutine(Player.Score - TargetScore));
                TargetScore = Player.Score;
            }
        }

        private void OnGUI()
        {
            Text.text = Score.ToString();
        }

        private IEnumerator AddScoreCoroutine(long score)
        {
            long iterationsCount = score / PointsPerIteration;
            float sleepTime = AnimationTime / iterationsCount;

            for(int i = 0; i < iterationsCount; i++)
            {
                Score += PointsPerIteration;
                yield return new WaitForSeconds(sleepTime);
            }
        }
    }
}
