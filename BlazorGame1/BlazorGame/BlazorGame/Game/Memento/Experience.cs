namespace BlazorGame.Game.Memento
{
    public class Experience
    {

        public string Name { get; set; }

        public int Level { get; set; }

        public float Exp { get; set; }

        public Experience(string name, int level, float exp)
        {
            Name = name;
            Level = level;
            Exp = exp;
        }
    }
}
