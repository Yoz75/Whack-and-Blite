using TMPro;
using UnityEngine;
using CSC;

namespace WhackAndBlite
{
    public class DefeatUI : MonoBehaviour
    {
        [SerializeField] private Player Player;
        [SerializeField] private GameObject DefeatScreen;
        [SerializeField] private TMP_Text CoinsText, SurvivedTimeText;

        private void Awake()
        {
            DefeatScreen.SetActive(false);
        }

        private void Start()
        {
            GameState.Instance.StateChanged += (state) => { if(state == State.Defeat) ShowDefeatScreen(); };            
        }

        private void ShowDefeatScreen()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            DefeatScreen.SetActive(true);
            CoinsText.text = Converter.Score2Coins(Player.Score).ToString("0.00");
            SurvivedTimeText.text = $"{Player.SurvivedTime.Minutes} Mins and {Player.SurvivedTime.Seconds} Secs";
        }
    }
}