using Godot;

namespace DinoKonpeito.Component
{
    public partial class HealthComponent : Node2D
    {
        [Signal]
        public delegate void DeathEventHandler();

        [Export]
        public float MaxHealth { get => _maxHealth; private set { _maxHealth = value; } }

        public float CurrentHealth { get => _health; private set { _health = value; } }

        private float _maxHealth;
        private float _health;
        public override void _Ready()
        {
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                EmitSignal(SignalName.Death);
            }
        }
    }
}
