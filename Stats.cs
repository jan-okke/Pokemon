namespace Pokemon
{
    struct Stats
    {
        public double HP;
        public double Attack;
        public double Defense;
        public double SpecialAttack;
        public double SpecialDefense;
        public double Speed;
        public Stats(double hp, double atk, double def, double spatk, double spdef, double speed)
        {
            HP = hp;
            Attack = atk;
            Defense = def;
            SpecialAttack = spatk;
            SpecialDefense = spdef;
            Speed = speed;
        }
    }
}