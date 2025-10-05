using TMPro;
using UnityEngine;

namespace WhackAndBlite
{
    [RequireComponent(typeof(TMP_Text))]
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Player Player;
        private TMP_Text Text;

        private void Start()
        {
            Text = GetComponent<TMP_Text>();
        }

        private void OnGUI()
        {
            Text.text = Player.Health.ToString();
        }
    }
}