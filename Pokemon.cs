namespace Pokemon
{
    class Pokemon
    {
        public Stats EVS;                   // Effort Values
        public Stats IVS;                   // Individual Values
        public Stats BaseStats;             // Base Stats
        public Stats Stats;                 // Actual Stats
        public Stats StatStages;            // Stat Stage Modifiers
        public string Name;                 // Name
        public string NickName;             // Nickname
        public int Level;                   // Level
        public int Experience;              // Experience
        public List<Move> Moves;            // List of Moves
        public List<PokemonType> Types;     // List of Types
        public int CurrentHP;               // Current Stats.HP
        public PokemonNature Nature;        // Nature
        public PokemonStatus Status;        // Status condition
        public int StatusTurnsLeft;         // Status condition turns left
        public Item? HeldItem;              // Held Item
        public int ExpYield;                // Exp Yield for defeating
        public bool isAlive = true;
        public Pokemon(string name, int level, Stats baseStats, List<PokemonType> pokemonTypes, int expYield)
        {
            EVS = new(0,0,0,0,0,0);
            IVS = new(0,0,0,0,0,0);
            BaseStats = baseStats;
            StatStages = new(1,1,1,1,1,1);
            Name = name;
            NickName = name;
            Level = level;
            Experience = Business.CalculateExpAtLevel(level);
            Business.CalculatePokemonStats(this);
            Moves = new();
            Types = pokemonTypes;
            CurrentHP = (int)Stats.HP;
            Nature = PokemonNature.None;
            HeldItem = null;
            ExpYield = expYield;
            Status = PokemonStatus.OK;
            StatusTurnsLeft = 0;
        }
    }
}