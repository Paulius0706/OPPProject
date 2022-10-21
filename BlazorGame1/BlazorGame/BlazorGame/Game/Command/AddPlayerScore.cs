using BlazorGame.Game.GameComponents;

namespace BlazorGame.Game.Command
{
    public class AddPlayerScore : Command

    {

        int _playerId;
        float _score;
        public AddPlayerScore(int playerId, float score)
        {
            this._playerId = playerId;
            this._score = score;
        }

        public override void Execute()
        {
            MainFrame.addScore(_playerId, _score);
        }
    }
}
