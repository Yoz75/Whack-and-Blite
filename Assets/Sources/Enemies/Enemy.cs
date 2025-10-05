
using CSC;
using System.Collections;
using UnityEngine;

namespace WhackAndBlite
{
    [RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
    public class Enemy : Entity<EnemyProps>
    {
        private const float MaxAliveTime = 120;
        public Player Target;

        private SpriteRenderer Renderer;
        private Rigidbody2D Rigidbody;

        private Vector3 StartScale;
        private int Id;
        private static int LastId;

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public void SetProps(EnemyProps props)
        {
            Props = props;
        }

        protected override void OnDied()
        {
            Target.AddScore(Props.KillScore);
            transform.parent.gameObject.GetComponent<EnemyBatch>().EnemiesCount--;
            Destroy(gameObject);
        }

        private void Start()
        {
            base.Start();

            Id = LastId++;

            StartScale = transform.localScale;

            const string playerTag = "Player";
            Target = GameObject.FindGameObjectWithTag(playerTag).GetComponent<Player>();

            Renderer = GetComponent<SpriteRenderer>();
            Rigidbody = GetComponent<Rigidbody2D>();

            StartMovement();

            Renderer.sprite = Props.Sprite;

            StartCoroutine(DieCoroutine());
        }

        private void StartMovement()
        {
            Rigidbody.gravityScale = 0;
            if(Props.ShouldWatchPlayer)
            {
                Rigidbody.linearVelocity = Vector3.Normalize(Target.transform.position - transform.position);
            }
            else
            {
                Rigidbody.linearVelocity = transform.parent.TransformDirection(Vector3.right);
            }

            Rigidbody.linearVelocity *= Props.Speed * GameDifficulty.Instance.Difficuly;
        }

        private void Update()
        {
            const float minScale = 0.25f;

            var scale = transform.localScale;

            scale.x = Math.Remap(Health, 0, MaxHealth, minScale, StartScale.x);
            scale.y = Math.Remap(Health, 0, MaxHealth, minScale, StartScale.y);

            transform.localScale = scale;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<Player>();

            if(player != null)
            {
                player.Damage(Props.Damage * GameDifficulty.Instance.Difficuly);
                Destroy(gameObject);
            }
        }

        private IEnumerator DieCoroutine()
        {
            yield return new WaitForSeconds(MaxAliveTime);
            Destroy(gameObject);
        }
    }
}