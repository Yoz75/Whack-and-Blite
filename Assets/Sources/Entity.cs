using CSC;

namespace WhackAndBlite
{
    public class Entity<T> : PropsHolder<T> where T : EntityProps
    {
        private float Health_;
        private bool IsAlive_ = true;

        public bool IsAlive { get => IsAlive_; }

        public float MaxHealth
        {
            get => Props.MaxHealth;
        }

        public float Health
        {
            get => Health_;
        }

        protected void Start()
        {
            Health_ = Props == null ? 0 : MaxHealth;
        }

        public void Heal(float health)
        {
            Health_ += health;

            if(Health_ < MaxHealth) Health_ = MaxHealth;
        }

        public void Damage(float damage)
        {
            Health_ -= damage;

            OnDamaged(damage);

            if(Health_ <= 0)
            { 
                OnDied();
            }
        }

        protected virtual void OnDamaged(float damage)
        {
            return;
        }

        protected virtual void OnDied()
        {
            return;
        }
    }
}