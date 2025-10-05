using CSC;
using UnityEngine;

namespace WhackAndBlite
{
    [RequireComponent(typeof(AudioSource))]
    public class Upgrader : MonoBehaviour
    {
        /*
            ALL THIS THING IS A HUGE GOVNOCODE, BUT I HAVE 31 MINUTES LEFT AND I HAVE TO DO MAAANY THINGS
         */
        [SerializeField] private AnimationCurve SpeedUpgradeCurve;
        [SerializeField] private float SpeedCurveAmplitude;
        [SerializeField] private int MaxSpeedUpgrades;

        [SerializeField] private AnimationCurve DamageUpgradeCurve;
        [SerializeField] private float DamageCurveAmplitude;
        [SerializeField] private int MaxDamageUpgrades;

        [SerializeField] private AudioClip CantUpgradeClip;

        private AudioSource Source;

        private static int SpeedUpgradesCount;
        private static int DamageUpgradesCount;

        public static float CurrentDamageCost
        {
            get;
            private set;
        } = 1;
        public static float CurrentSpeedCost
        {
            get;
            private set;
        } = 1;

        private void Start()
        {
            Source = GetComponent<AudioSource>();
            Source.clip = CantUpgradeClip;
        }

        public void UpgradeSpeed()
        {
            if(PlayerInventory.Money < CurrentSpeedCost || SpeedUpgradesCount > MaxSpeedUpgrades)
            {
                Source.Play();
                return;
            }

            SpeedUpgradesCount++;
            float evaluated = Math.Remap(SpeedUpgradesCount, 0, MaxSpeedUpgrades, 0, 1);

            PlayerInventory.SpeedUpgrade += SpeedUpgradeCurve.Evaluate(evaluated) * SpeedCurveAmplitude;

            PlayerInventory.Money -= CurrentSpeedCost;
            CurrentSpeedCost += 1;
        }

        public void UpgradeDamage()
        {
            if(PlayerInventory.Money < CurrentDamageCost || DamageUpgradesCount > MaxDamageUpgrades)
            {
                Source.Play();
                return;
            }

            DamageUpgradesCount++;
            float evaluated = Math.Remap(DamageUpgradesCount, 0, MaxDamageUpgrades, 0, 1);

            PlayerInventory.DamageUpgrade += DamageUpgradeCurve.Evaluate(evaluated) * DamageCurveAmplitude;

            PlayerInventory.Money -= CurrentDamageCost;
            CurrentDamageCost += 1;
        }
    }
}