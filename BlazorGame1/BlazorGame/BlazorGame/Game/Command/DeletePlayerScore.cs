namespace BlazorGame.Game.Command
{
    public class DeletePlayerScore : Command
    {
        int _playerId;
        public DeletePlayerScore(int playerId)
        {
            this._playerId = playerId;
        }

        public override void Execute()
        {
            MainFrame.deleteScore(_playerId);
        }
    }
}
