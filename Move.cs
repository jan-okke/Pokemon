namespace Pokemon
{
    class Move
    {
        public int BasePower;
        public string Name;
        public int Accuracy;
        public PokemonType Type;
        public MoveCategory Category;
        public int PowerPoints;
        public int CurrentPowerPoints;
        public int PPUpsUsed;
        public int Priority;
        public static Move Empty = new("", 0, 0, 0, PokemonType.None, MoveCategory.Status, 0);
        public Move(string name, int basePower, int accuracy, int priority, PokemonType pType, MoveCategory category, int powerPoints)
        {
            BasePower = basePower;
            Name = name;
            Accuracy = accuracy;
            Priority = priority;
            Type = pType;
            Category = category;
            PowerPoints = powerPoints;
            CurrentPowerPoints = powerPoints;
            PPUpsUsed = 0;
        }
    }
}