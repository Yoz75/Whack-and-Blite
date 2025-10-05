using TMPro;
using UnityEngine;

namespace WhackAndBlite
{
    [RequireComponent(typeof(TMP_Text))]
    public class MoneyUI : MonoBehaviour
    {
        private TMP_Text Text;

        private void Start()
        {
            Text = GetComponent<TMP_Text>();
        }

        private void OnGUI()
        {
            Text.text = PlayerInventory.Money.ToString("0.00");
        }
    }
}