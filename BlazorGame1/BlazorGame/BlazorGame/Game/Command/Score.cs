namespace BlazorGame.Game.Command
{
    //electronic device
    public class Score
    {
        public int PlayerId;
        public float ScorePoints; 
        
        public Score(int playerId,float score)
        {
            PlayerId = playerId;
            ScorePoints = score;
        }

    }
}
