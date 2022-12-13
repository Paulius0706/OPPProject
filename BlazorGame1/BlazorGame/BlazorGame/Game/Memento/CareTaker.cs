namespace BlazorGame.Game.Memento
{
    public class CareTaker
    {
        private List<Memento> MementoList = new List<Memento>();

        private Dictionary<string,Memento> MementoDictionary= new Dictionary<string,Memento>();

        public void add(Memento state)
        {
            MementoDictionary.Add(state.getState().Name, state);
        }

        public Memento get(string name)
        {
            return MementoDictionary[name];
        }

        public bool contains(string name)
        {
            return MementoDictionary.ContainsKey(name);
        }

    }
}
