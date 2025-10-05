using TMPro;
using UnityEngine;

namespace WhackAndBlite
{
    [RequireComponent(typeof(TMP_Text))]
    public class DamageUpgradeUI : MonoBehaviour
    {
        [SerializeField] private Upgrader Upgrader;
        private TMP_Text Text;

        private void Start()
        {
            Text = GetComponent<TMP_Text>();
        }

        private void OnGUI()
        {
            Text.text = "$" + Upgrader.CurrentDamageCost.ToString("0.00");
        }
    }
}
