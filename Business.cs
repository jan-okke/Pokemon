using PokemonExceptions;

namespace Pokemon
{
    class Business
    {
        public static void CalculatePokemonStats(Pokemon pokemon)
        {
            double oldHP = 0;
            if (pokemon.CurrentHP > 0) oldHP = pokemon.Stats.HP;
            pokemon.Stats.HP = Math.Floor(Math.Floor(2 * pokemon.BaseStats.HP + pokemon.IVS.HP + Math.Floor(pokemon.EVS.HP / 4)) * pokemon.Level / 100) + pokemon.Level + 10;
            pokemon.Stats.Attack = Math.Floor((Math.Floor(Math.Floor(2 * pokemon.BaseStats.Attack + pokemon.IVS.Attack + Math.Floor(pokemon.EVS.Attack / 4)) * pokemon.Level / 100) + 5) * GetNatureModAttack(pokemon.Nature));
            pokemon.Stats.Defense = Math.Floor((Math.Floor(Math.Floor(2 * pokemon.BaseStats.Defense + pokemon.IVS.Defense + Math.Floor(pokemon.EVS.Defense / 4)) * pokemon.Level / 100) + 5) * GetNatureModDefense(pokemon.Nature));
            pokemon.Stats.SpecialAttack = Math.Floor((Math.Floor(Math.Floor(2 * pokemon.BaseStats.SpecialAttack + pokemon.IVS.SpecialAttack + Math.Floor(pokemon.EVS.SpecialAttack / 4)) * pokemon.Level / 100) + 5) * GetNatureModSpecialAttack(pokemon.Nature));
            pokemon.Stats.SpecialDefense = Math.Floor((Math.Floor(Math.Floor(2 * pokemon.BaseStats.SpecialDefense + pokemon.IVS.SpecialDefense + Math.Floor(pokemon.EVS.SpecialDefense / 4)) * pokemon.Level / 100) + 5) * GetNatureModSpecialDefense(pokemon.Nature));
            pokemon.Stats.Speed = Math.Floor((Math.Floor(Math.Floor(2 * pokemon.BaseStats.Speed + pokemon.IVS.Speed + Math.Floor(pokemon.EVS.Speed / 4)) * pokemon.Level / 100) + 5) * GetNatureModSpeed(pokemon.Nature));
            if (oldHP!= 0 && oldHP < pokemon.Stats.HP) pokemon.CurrentHP += (int)(pokemon.Stats.HP - oldHP);
        }
        public static int CalculateExpAtLevel(int level)
        {
            return (int)Math.Pow(level, 3);
        }
        private static int CalculateLevelAtExp(int experience)
        {
            return (int)Math.Pow(experience, 1.0 / 3);
        }
        private static int CalculateExpGain(Pokemon defeatedPokemon)
        {
            int baseYield = defeatedPokemon.ExpYield;
            double luckyEgg = 1;
            double affection = 1;
            int level = defeatedPokemon.Level;
            int levelUser = level; // TODO
            double shared = 1;
            double trainerMod = 1;
            double pastEvolved = 1;
            double powerBoost = 1; // IGNORE
            double experience = ((baseYield * level / 5) * (1 / shared) * Math.Pow((2 * level + 10) / (level + levelUser + 10), 2.5) + 1) * trainerMod * luckyEgg * pastEvolved * affection * powerBoost;
            return (int)experience;
        }
        private static double GetNatureModAttack(PokemonNature nature)
        {
            if (nature == PokemonNature.Adamant) return 1.1;
            return 1;
        }
        private static double GetNatureModDefense(PokemonNature nature)
        {
            return 1;
        }
        private static double GetNatureModSpecialAttack(PokemonNature nature)
        {
            return 1;
        }
        private static double GetNatureModSpecialDefense(PokemonNature nature)
        {
            return 1;
        }
        private static double GetNatureModSpeed(PokemonNature nature)
        {
            return 1;
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
                CalculatePokemonStats(pokemon);
            }
            return text;
        }
        private static string LevelUp(Pokemon pokemon, int oldLevel, int newLevel)
        {
            return $"{pokemon.Name} reached level {newLevel}!"; // TODO
        }
        private static int CalculateDamage(Battle battle, Pokemon attacker, Pokemon defender, Move move)
        {
            if (move.Category == MoveCategory.Status) return 0;
            double damage;
            double power = move.BasePower;
            double attack = attacker.Stats.Attack * attacker.StatStages.Attack;
            double defense = defender.Stats.Defense * defender.StatStages.Defense;
            double specialAttack = attacker.Stats.SpecialAttack * attacker.StatStages.SpecialAttack;
            double specialDefense = defender.Stats.SpecialDefense * defender.StatStages.SpecialDefense;
            double targetMod = 1;
            double parentalBond = 1;
            double weather = 1;
            double critical = 1;
            double random = 1;
            double stab = CalculateStab(attacker, move);
            double effectivity = CalculateEffectivity(move.Type, defender.Types[0], defender.Types[1]);
            double burn = 1;
            double other = 1;
            double zMove = 1;
            if (move.Category == MoveCategory.Physical) damage = Math.Floor(Math.Floor(0.4 * attacker.Level + 2) * attack / (defense*50) + 2) * targetMod * parentalBond * weather * critical * random * stab * effectivity * burn * other * zMove;
            else if (move.Category == MoveCategory.Special) damage = Math.Floor(Math.Floor(0.4 * attacker.Level + 2) * specialAttack / (specialDefense*50) + 2) * targetMod * parentalBond * weather * critical * random * stab * effectivity * burn * other * zMove;
            else return -1;
            return (int)damage;
        }
        private static double CalculateEffectivity(PokemonType attackingType, PokemonType defendingType)
        {
            switch (attackingType) {
                case PokemonType.Water:
                    if (defendingType == PokemonType.Fire) return 2;
                    break;
                case PokemonType.Fire:
                    if (defendingType == PokemonType.Water) return .5;
                    break;
            }
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
            string text = "\nName\t\tLevel\t\tType 1\t\tType 2\t\tItem\t\tStatus\t\tHP\n";
            for (int i = 0; i < 112; i++) text += "=";
            text += "\n";
            foreach (Pokemon pokemon in pokemons)
            {
                text += pokemon.Name;
                for (int i = 0; i < 16-pokemon.Name.Length; i++) text += " ";
                text += pokemon.Level.ToString();
                for (int i = 0; i < 16-pokemon.Level.ToString().Length; i++) text += " ";
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
        public static string ShowPokemonStats(Pokemon pokemon)
        {
            string text = "\nStat\t\t\tBase\t\tEVs\t\tIVs\t\tValue\t\tStat Stages\n";
            for (int i = 0; i < 104; i++) text += "=";
            text += "\n";
            text += "HP\t\t\t";
            text += pokemon.BaseStats.HP.ToString();
            for (int i = 0; i < 16-pokemon.BaseStats.HP.ToString().Length; i++) text += " ";
            text += pokemon.IVS.HP.ToString();
            for (int i = 0; i < 16-pokemon.IVS.HP.ToString().Length; i++) text += " ";
            text += pokemon.EVS.HP.ToString();
            for (int i = 0; i < 16-pokemon.EVS.HP.ToString().Length; i++) text += " ";
            text += pokemon.CurrentHP.ToString() + " / " + pokemon.Stats.HP.ToString();
            for (int i = 0; i < 16-(pokemon.Stats.HP.ToString().Length + pokemon.CurrentHP.ToString().Length + 3); i++) text += " ";
            text += pokemon.StatStages.HP.ToString() + "\n";
            text += "Attack\t\t\t";
            text += pokemon.BaseStats.Attack.ToString();
            for (int i = 0; i < 16-pokemon.BaseStats.Attack.ToString().Length; i++) text += " ";
            text += pokemon.IVS.Attack.ToString();
            for (int i = 0; i < 16-pokemon.IVS.Attack.ToString().Length; i++) text += " ";
            text += pokemon.EVS.Attack.ToString();
            for (int i = 0; i < 16-pokemon.EVS.Attack.ToString().Length; i++) text += " ";
            text += pokemon.Stats.Attack.ToString();
            for (int i = 0; i < 16-pokemon.Stats.Attack.ToString().Length; i++) text += " ";
            text += pokemon.StatStages.Attack.ToString() + "\n";
            text += "Defense\t\t\t";
            text += pokemon.BaseStats.Defense.ToString();
            for (int i = 0; i < 16-pokemon.BaseStats.Defense.ToString().Length; i++) text += " ";
            text += pokemon.IVS.Defense.ToString();
            for (int i = 0; i < 16-pokemon.IVS.Defense.ToString().Length; i++) text += " ";
            text += pokemon.EVS.Defense.ToString();
            for (int i = 0; i < 16-pokemon.EVS.Defense.ToString().Length; i++) text += " ";
            text += pokemon.Stats.Defense.ToString();
            for (int i = 0; i < 16-pokemon.Stats.Defense.ToString().Length; i++) text += " ";
            text += pokemon.StatStages.Defense.ToString() + "\n";
            text += "Special Attack\t\t";
            text += pokemon.BaseStats.SpecialAttack.ToString();
            for (int i = 0; i < 16-pokemon.BaseStats.SpecialAttack.ToString().Length; i++) text += " ";
            text += pokemon.IVS.SpecialAttack.ToString();
            for (int i = 0; i < 16-pokemon.IVS.SpecialAttack.ToString().Length; i++) text += " ";
            text += pokemon.EVS.SpecialAttack.ToString();
            for (int i = 0; i < 16-pokemon.EVS.SpecialAttack.ToString().Length; i++) text += " ";
            text += pokemon.Stats.SpecialAttack.ToString();
            for (int i = 0; i < 16-pokemon.Stats.SpecialAttack.ToString().Length; i++) text += " ";
            text += pokemon.StatStages.SpecialAttack.ToString() + "\n";
            text += "Special Defense\t\t";
            text += pokemon.BaseStats.SpecialDefense.ToString();
            for (int i = 0; i < 16-pokemon.BaseStats.SpecialDefense.ToString().Length; i++) text += " ";
            text += pokemon.IVS.SpecialDefense.ToString();
            for (int i = 0; i < 16-pokemon.IVS.SpecialDefense.ToString().Length; i++) text += " ";
            text += pokemon.EVS.SpecialDefense.ToString();
            for (int i = 0; i < 16-pokemon.EVS.SpecialDefense.ToString().Length; i++) text += " ";
            text += pokemon.Stats.SpecialDefense.ToString();
            for (int i = 0; i < 16-pokemon.Stats.SpecialDefense.ToString().Length; i++) text += " ";
            text += pokemon.StatStages.SpecialDefense.ToString() + "\n";
            text += "Speed\t\t\t";
            text += pokemon.BaseStats.Speed.ToString();
            for (int i = 0; i < 16-pokemon.BaseStats.Speed.ToString().Length; i++) text += " ";
            text += pokemon.IVS.Speed.ToString();
            for (int i = 0; i < 16-pokemon.IVS.Speed.ToString().Length; i++) text += " ";
            text += pokemon.EVS.Speed.ToString();
            for (int i = 0; i < 16-pokemon.EVS.Speed.ToString().Length; i++) text += " ";
            text += pokemon.Stats.Speed.ToString();
            for (int i = 0; i < 16-pokemon.Stats.Speed.ToString().Length; i++) text += " ";
            text += pokemon.StatStages.Speed.ToString() + "\n";
            text += $"{pokemon.Nature} Nature\n";
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
            target.isAlive = target.CurrentHP > 0;
            if (!target.isAlive) target.Status = PokemonStatus.Fainted;
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
        public static bool IsBattleOver(Battle battle) {
            foreach (Pokemon pokemon in battle.PlayerPokemons) if (!pokemon.isAlive) return true;
            foreach (Pokemon pokemon in battle.EnemyPokemons) if (!pokemon.isAlive) return true;
            return false;
        }
        public static bool IsPlayerPartyAlive(List<Pokemon> partyPlayer) {
            foreach (Pokemon pokemon in partyPlayer) if (pokemon.isAlive) return true;
            return false;
        }
    }
}