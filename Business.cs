using PokemonExceptions;

namespace Pokemon
{
    class Business
    {
        public static void CalculatePokemonStats(Pokemon pokemon)
        {
            pokemon.Stats = pokemon.BaseStats; //TODO
        }
        public static int CalculateExpAtLevel(int level)
        {
            return 0; // TODO
        }
        private static int CalculateLevelAtExp(int experience)
        {
            return 0; // TODO
        }
        private static int CalculateExpGain(Pokemon defeatedPokemon)
        {
            return 0; // TODO
        }
        private static string AddExp(Pokemon pokemon, int amount)
        {
            string text = "";
            int oldLevel = pokemon.Level;
            pokemon.Experience += amount;
            int newLevel = CalculateLevelAtExp(pokemon.Experience);
            if (newLevel > oldLevel) {
                text += LevelUp(pokemon, oldLevel, newLevel);
                pokemon.Level = newLevel;
            }
            return text;
        }
        private static string LevelUp(Pokemon pokemon, int oldLevel, int newLevel)
        {
            return ""; // TODO
        }
        private static int CalculateDamage(Battle battle, Pokemon attacker, Pokemon defender, Move move)
        {
            return 1; // TODO
        }
        private static double CalculateEffectivity(PokemonType attackingType, PokemonType defendingType)
        {
            return 1; // TODO
        }
        private static double CalculateEffectivity(PokemonType attackingType, PokemonType defendingType, PokemonType defendingType2)
        {
            return CalculateEffectivity(attackingType, defendingType) * CalculateEffectivity(attackingType, defendingType2);
        }
        private static double CalculateStab(Pokemon pokemon, Move move)
        {
            foreach (PokemonType pType in pokemon.Types) {
                if (pType == move.Type) return 1.5;
            }
            return 1;
        }
        public static bool PokemonHoldsItem(Pokemon pokemon)
        {
            return pokemon.HeldItem is null;
        }
        public static bool Turn(Pokemon attacker, Pokemon defender, Move attackerMove, Move defenderMove)
        {
            // TODO
            return attacker.isAlive && defender.isAlive;
        }
        private static bool IsValidLevel(int level)
        {
            return 0 < level && level <= Constants.MAXLEVEL;
        }
        private static void GivePokemonItem(Pokemon pokemon, Item item)
        {
            pokemon.HeldItem = item;
        }
        private static void AddPokemonMove(Pokemon pokemon, Move move)
        {
            pokemon.Moves.Add(move);
        }
        public static Pokemon NewPokemon(string species, int level)
        {
            if (!IsValidLevel(level)) throw new InvalidLevelException(level);
            foreach (Pokemon pokemon in PokemonFactory.PokemonList)
            {
                if (pokemon.Name == species)
                {
                    pokemon.Level = level;
                    return pokemon;
                }
            }
            throw new InvalidSpeciesException(species);
        }
        public static Pokemon NewPokemon(string species, int level, string item)
        {
            if (!IsValidLevel(level)) throw new InvalidLevelException(level);
            foreach (Pokemon pokemon in PokemonFactory.PokemonList)
            {
                if (pokemon.Name == species)
                {
                    pokemon.Level = level;
                    bool validItem = false;
                    foreach (Item _item in ItemFactory.ItemList)
                    {
                        if (_item.Name == item)
                        {
                            validItem = true;
                            GivePokemonItem(pokemon, _item);
                        }
                    }
                    if (!validItem) throw new InvalidItemException(item);
                    return pokemon;
                }
            }
            throw new InvalidSpeciesException(species);
        }
        public static Pokemon NewPokemon(string species, int level, List<string> moveList)
        {
            if (!IsValidLevel(level)) throw new InvalidLevelException(level);
            foreach (Pokemon pokemon in PokemonFactory.PokemonList)
            {
                if (pokemon.Name == species)
                {
                    pokemon.Level = level;
                    foreach (string moveName in moveList)
                    {
                        bool validMove = false;
                        foreach (Move move in MoveFactory.MoveList)
                        {
                            if (move.Name == moveName)
                            {
                                validMove = true;
                                AddPokemonMove(pokemon, move);
                            }
                        }
                        if(!validMove) throw new InvalidMoveException(moveName);
                    }
                    return pokemon;
                }
            }
            throw new InvalidSpeciesException(species);
        }
        public static Pokemon NewPokemon(string species, int level, List<string> moveList, string item)
        {
            if (!IsValidLevel(level)) throw new InvalidLevelException(level);
            foreach (Pokemon pokemon in PokemonFactory.PokemonList)
            {
                if (pokemon.Name == species)
                {
                    pokemon.Level = level;
                    foreach (string moveName in moveList)
                    {
                        bool validMove = false;
                        foreach (Move move in MoveFactory.MoveList)
                        {
                            if (move.Name == moveName)
                            {
                                validMove = true;
                                AddPokemonMove(pokemon, move);
                            }
                        }
                        if(!validMove) throw new InvalidMoveException(moveName);
                    }
                    bool validItem = false;
                    foreach (Item _item in ItemFactory.ItemList)
                    {
                        if (_item.Name == item)
                        {
                            validItem = true;
                            GivePokemonItem(pokemon, _item);
                        }
                    }
                    if (!validItem) throw new InvalidItemException(item);
                    return pokemon;
                }
            }
            throw new InvalidSpeciesException(species);
        }
        public static string ShowPokemonParty(List<Pokemon> pokemons)
        {
            string text = "\nName\t\tType 1\t\tType 2\t\tItem\t\tStatus\t\tHP\n";
            for (int i = 0; i < 96; i++) text += "=";
            text += "\n";
            foreach (Pokemon pokemon in pokemons)
            {
                text += pokemon.Name;
                for (int i = 0; i < 16-pokemon.Name.Length; i++) text += " ";
                text += pokemon.Types[0].ToString();
                for (int i = 0; i < 16-pokemon.Types[0].ToString().Length; i++) text += " ";
                text += pokemon.Types[1].ToString();
                for (int i = 0; i < 16-pokemon.Types[1].ToString().Length; i++) text += " ";
                if (pokemon.HeldItem != null) {
                    text += pokemon.HeldItem.Name;
                    for (int i = 0; i < 16-pokemon.HeldItem.Name.Length; i++) text += " ";
                }
                else text += "\t\t";
                text += pokemon.Status.ToString();
                for (int i = 0; i < 16-pokemon.Status.ToString().Length; i++) text += " ";
                text += pokemon.CurrentHP.ToString() + " / " + pokemon.Stats.HP;
                text += "\n";
            }
            return text;
        }
        public static string ShowAvailableMoves(Pokemon pokemon)
        {
            string text = "\nName\t\tType\t\tCategory\tBase Power\tAccuracy\tPower Points\n";
            for (int i = 0; i < 96; i++) text += "=";
            text += "\n";
            foreach (Move move in pokemon.Moves)
            {
                text += move.Name;
                for (int i = 0; i < 16-move.Name.Length; i++) text += " ";
                text += move.Type.ToString();
                for (int i = 0; i < 16-move.Type.ToString().Length; i++) text += " ";
                text += move.Category.ToString();
                for (int i = 0; i < 16-move.Category.ToString().Length; i++) text += " ";
                text += move.BasePower.ToString();
                for (int i = 0; i < 16-move.BasePower.ToString().Length; i++) text += " ";
                text += move.Accuracy.ToString();
                for (int i = 0; i < 16-move.Accuracy.ToString().Length; i++) text += " ";
                text += move.CurrentPowerPoints.ToString() + " / " + move.PowerPoints.ToString();
                text += "\n";
            }
            return text;
        }
        public static string ShowBattleInfo(Battle battle)
        {
            string text = ShowPokemonParty(battle.PlayerPokemons);
            text += ShowPokemonParty(battle.EnemyPokemons);
            return text;
        }
        public static Battle StartTrainerBattle(List<Pokemon> playerPokemons, Trainer trainer)
        {
            System.Console.WriteLine($"{trainer.Category} {trainer.Name} would like to battle!");
            System.Console.WriteLine($"{trainer.Name} sent out {trainer.Party[0].Name}!");
            System.Console.WriteLine($"Go, {playerPokemons[0].Name}!");
            Battle battle = new Battle(playerPokemons, trainer.Party, Weather.None, Terrain.None);
            battle.Type = BattleType.SingleBattle;
            return battle;
        }
        public static Battle StartTrainerBattle(List<Pokemon> playerPokemons, Trainer trainer, Weather weather)
        {
            System.Console.WriteLine($"{trainer.Category} {trainer.Name} would like to battle!");
            System.Console.WriteLine($"{trainer.Name} sent out {trainer.Party[0].Name}!");
            System.Console.WriteLine($"Go, {playerPokemons[0].Name}!");
            Battle battle = new Battle(playerPokemons, trainer.Party, weather, Terrain.None);
            battle.Type = BattleType.SingleBattle;
            return battle;
        }
        public static Battle StartWildBattle(List<Pokemon> playerPokemons, string species, int level)
        {
            Pokemon wildPokemon = NewPokemon(species, level);
            Battle battle = new Battle(playerPokemons, new() {wildPokemon}, Weather.None, Terrain.None);
            battle.Type = BattleType.SingleBattle;
            return battle;
        }
        public static Battle StartWildBattle(List<Pokemon> playerPokemons, string species, int level, string item)
        {
            Pokemon wildPokemon = NewPokemon(species, level, item);
            Battle battle = new Battle(playerPokemons, new() {wildPokemon}, Weather.None, Terrain.None);
            battle.Type = BattleType.SingleBattle;
            return battle;
        }
        public static Battle StartWildBattle(List<Pokemon> playerPokemons, string species, int level, Weather weather)
        {
            Pokemon wildPokemon = NewPokemon(species, level);
            Battle battle = new Battle(playerPokemons, new() {wildPokemon}, weather, Terrain.None);
            battle.Type = BattleType.SingleBattle;
            return battle;
        }
        public static Battle StartWildBattle(List<Pokemon> playerPokemons, string species, int level, string item, Weather weather)
        {
            Pokemon wildPokemon = NewPokemon(species, level, item);
            Battle battle = new Battle(playerPokemons, new() {wildPokemon}, weather, Terrain.None);
            battle.Type = BattleType.SingleBattle;
            return battle;
        }
        private static Move ChooseRandomMove(Pokemon pokemon)
        {
            Random random = new();
            return pokemon.Moves[random.Next(0,pokemon.Moves.Count)];
        }
        private static bool DealDamage(Pokemon target, int amount)
        {
            target.CurrentHP -= amount;
            return target.CurrentHP > 0;
        }
        private static bool IsFaster(Pokemon attacker, Pokemon defender, Move moveAttacker, Move moveDefender)
        {
            if (moveAttacker.Priority > moveDefender.Priority) return true;
            if (moveAttacker.Priority < moveDefender.Priority) return false;
            return attacker.Stats.Speed > defender.Stats.Speed;
        }
        private static string Turn(Battle battle, Move attackerMove, Move defenderMove)
        {
            string text = "";
            int damage;
            double effectivity;
            int experience;
            Pokemon attacker = battle.PlayerPokemons[0];
            Pokemon defender = battle.EnemyPokemons[0];
            if (IsFaster(attacker, defender, attackerMove, defenderMove)) {
                text += $"{attacker.Name} used {attackerMove.Name}!\n";
                attackerMove.CurrentPowerPoints --;
                effectivity = CalculateEffectivity(attackerMove.Type, defender.Types[0], defender.Types[1]);
                if (effectivity == 0) text += "But it had no effect...\n";
                else if (effectivity < 1) text += "It's not very effective...\n";
                if (effectivity > 1) text += "It's super effective!\n";
                damage = CalculateDamage(battle, attacker, defender, attackerMove);
                if (!DealDamage(defender, damage)) {
                    text += $"{defender.Name} fainted!\n";
                    experience = CalculateExpGain(defender);
                    text += $"{attacker.Name} gained {experience} experience points!\n";
                    text += AddExp(attacker, experience);
                    return text;
                }
                text += $"{defender.Name} used {defenderMove.Name}!\n";
                defenderMove.CurrentPowerPoints --;
                effectivity = CalculateEffectivity(defenderMove.Type, attacker.Types[0], attacker.Types[1]);
                if (effectivity == 0) text += "But it had no effect...\n";
                else if (effectivity < 1) text += "It's not very effective...\n";
                if (effectivity > 1) text += "It's super effective!\n";
                damage = CalculateDamage(battle, defender, attacker, defenderMove);
                if (!DealDamage(attacker, damage)) {
                    text += $"{attacker.Name} fainted!\n";
                    experience = CalculateExpGain(attacker);
                    text += $"{defender.Name} gained {experience} experience points!\n";
                    text += AddExp(defender, experience);
                    return text;
                }
            }
            else {
                text += $"{defender.Name} used {defenderMove.Name}!\n";
                defenderMove.CurrentPowerPoints --;
                effectivity = CalculateEffectivity(defenderMove.Type, attacker.Types[0], attacker.Types[1]);
                if (effectivity == 0) text += "But it had no effect...\n";
                else if (effectivity < 1) text += "It's not very effective...\n";
                else if (effectivity > 1) text += "It's super effective!\n";
                damage = CalculateDamage(battle, defender, attacker, defenderMove);
                if (!DealDamage(attacker, damage)) {
                    text += $"{attacker.Name} fainted!\n";
                    experience = CalculateExpGain(attacker);
                    text += $"{defender.Name} gained {experience} experience points!\n";
                    text += AddExp(defender, experience);
                    return text;
                }
                text += $"{attacker.Name} used {attackerMove.Name}!\n";
                attackerMove.CurrentPowerPoints --;
                effectivity = CalculateEffectivity(attackerMove.Type, defender.Types[0], defender.Types[1]);
                if (effectivity == 0) text += "But it had no effect...\n";
                else if (effectivity < 1) text += "It's not very effective...\n";
                if (effectivity > 1) text += "It's super effective!\n";
                damage = CalculateDamage(battle, attacker, defender, attackerMove);
                if (!DealDamage(defender, damage)) {
                    text += $"{defender.Name} fainted!\n";
                    experience = CalculateExpGain(defender);
                    text += $"{attacker.Name} gained {experience} experience points!\n";
                    text += AddExp(attacker, experience);
                    return text;
                }
            }
            return text;
        }
        public static string UseMoveInBattle(Battle battle, string move)
        {
            bool validMove = false;
            Move m = Move.Empty;
            foreach (Move _move in battle.PlayerPokemons[0].Moves)
            {
                if (_move.Name == move)
                {
                    validMove = true;
                    m = _move;
                }
            }
            if (!validMove) throw new InvalidMoveException(move);
            return Turn(battle, m, ChooseRandomMove(battle.EnemyPokemons[0]));
        }
    }
}