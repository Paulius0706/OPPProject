namespace BlazorGame.Game.Memento
{
    public class Memento
    {
        public Experience State;

        public Memento(Experience state)
        {
            this.State = state;
        }

        public Experience getState()
        {
            return this.State;
        }

    }
}
