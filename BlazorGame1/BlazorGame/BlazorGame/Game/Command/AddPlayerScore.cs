using BlazorGame.Game.GameComponents;

namespace BlazorGame.Game.Command
{
    public class AddPlayerScore : Command
    {
        private int playerId;
        private float score;
        public AddPlayerScore(int playerId, float score)
        {
            this.playerId = playerId;
            this.score = score;
        }

        public override void Execute()
        {
            MainFrame.AddScore(playerId, score);
        }
    }
}
