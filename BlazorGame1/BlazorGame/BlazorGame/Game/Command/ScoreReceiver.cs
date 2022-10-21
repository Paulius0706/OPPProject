namespace BlazorGame.Game.Command
{

    public class ScoreReceiver : Score
    {
        private float _score = 0;
        public void addScore(string playerId, float score)
        {
            _score += score;
        }

        public void deleteScore(string playerId)
        {
            _score = 0;
        }
    }
}
