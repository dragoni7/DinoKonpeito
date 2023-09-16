using Godot;
using System;

namespace DinoKonpeito.Component
{
    public partial class HealthComponent : Node2D
    {
        [Export]
        public float MaxHealth { get => _maxHealth; private set { _maxHealth = value; } }

        public float CurrentHealth { get => _health; private set { _health = value; } }

        private float _maxHealth;
        private float _health;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            CurrentHealth = MaxHealth;
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }
    }
}
