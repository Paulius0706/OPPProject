namespace BlazorGame.Game.Memento
{
    public class Originator
    {
        private Experience? State;

        public void setState(Experience state)
        {
            this.State = state;
        }

        public Experience getState()
        {
            return State;
        }

        public Memento saveStateToMemento()
        {
            return new Memento(State);
        }

        public void getStateFromMemento(Memento memento)
        {
            State = memento.getState();
        }
    }
}
