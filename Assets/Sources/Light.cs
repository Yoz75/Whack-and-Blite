using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace WhackAndBlite
{
    [RequireComponent(typeof(AudioSource))]
    public class Light : Entity<EntityProps>
    {
        [SerializeField] private Player Player;
        [SerializeField] private AudioClip EnemyKilledSfx;
        [SerializeField] private float DamageWaitTime = 1f;

        private AudioSource Source;

        private Dictionary<Enemy, Coroutine> DamageCoroutines = new();

        private void Start()
        {
            base.Start();
            Source = GetComponent<AudioSource>();
            Source.clip = EnemyKilledSfx;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var enemy = collision.GetComponent<Enemy>();

            DamageCoroutines[enemy] = StartCoroutine(DamageOverTime(enemy));            
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var enemy = collision.GetComponent<Enemy>();
            if(enemy != null && DamageCoroutines.ContainsKey(enemy))
            {
                StopCoroutine(DamageCoroutines[enemy]);
                Source.Play();
            }
        }

        private IEnumerator DamageOverTime(Enemy enemy)
        {
            while(enemy != null)
            {
                if(enemy.GetProps().Damage < 0)
                {
                    Player.Heal(enemy.GetProps().Damage);
                    Destroy(enemy.gameObject);
                }
                enemy.Damage(Props.Damage * PlayerInventory.DamageUpgrade);
                yield return new WaitForSeconds(DamageWaitTime);
            }
        }
    }
}
