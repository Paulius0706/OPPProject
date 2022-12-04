namespace BlazorGame.Game.Command
{
    public class DeletePlayerScore : Command
    {
        private int playerId;
        public DeletePlayerScore(int playerId)
        {
            this.playerId = playerId;
        }

        public override void Execute()
        {
            MainFrame.DeleteScore(playerId);
        }
    }
}
