using BlazorGame.Game.GameComponents;
using static System.Formats.Asn1.AsnWriter;

namespace BlazorGame.Game.Command
{
    public class ScoreReceiver : Score
    {
        private float score = 0;

        public ScoreReceiver(int playerId, float score) : base(playerId, score) { }
        public void addScore(string playerId, float score)
        {
            this.score += score;
        }

        public void deleteScore(string playerId)
        {
            score = 0;
        }
    }
}
