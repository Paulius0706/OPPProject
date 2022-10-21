namespace BlazorGame.Game.Command
{
    //electronic device
    public class Score
    {
        public int _playerId;
        public float _score; 
        
        public Score(int playerId,float score)
        {
            _playerId = playerId;
            _score = score;
        }

    }
}
