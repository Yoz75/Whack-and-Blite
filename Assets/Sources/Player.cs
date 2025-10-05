using CSC;
using System;
using UnityEngine;

namespace WhackAndBlite
{
    [RequireComponent(typeof(AudioSource))]
    public class Player : Entity<EntityProps>
    {
        [SerializeField] private AudioClip DamagedClip;

        private AudioSource AudioSource;
        private long Score_;
        private DateTime StartTime;

        public long Score => Score_;

        public TimeSpan SurvivedTime => DateTime.Now - StartTime;

        private void Start()
        {
            base.Start();
            StartTime = DateTime.Now;

            AudioSource = GetComponent<AudioSource>();
            AudioSource.clip = DamagedClip;
        }

        public void AddScore(long score)
        {
            Score_ += score;
        }

        protected override void OnDamaged(float damage)
        {
            AudioSource.Play();
        }

        protected override void OnDied()
        {
            PlayerInventory.Money += Converter.Score2Coins(Score);
            GameState.Instance.Lose("");
        }
    }
}
