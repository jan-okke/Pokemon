namespace Pokemon
{
    class Trainer
    {
        public string Name;
        public TrainerCategory Category;
        public List<Pokemon> Party;
        public Trainer(string name, TrainerCategory category, List<Pokemon> party)
        {
            Name = name;
            Category = category;
            Party = party;
        }
    }
}