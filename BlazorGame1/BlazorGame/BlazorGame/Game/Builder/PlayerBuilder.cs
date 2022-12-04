using BlazorGame.Game.GameObjects;
using System;

namespace BlazorGame.Game.Builder
{
    public class PlayerBuilder : Builder
    {
        public PlayerBuilder(float[] position)
        {
            base.gameObject = new PlayerObject();
            base.gameObject.Mass = 10f;
            base.gameObject.objectType = GameObject.ObjectType.Player;
            base.gameObject.Position = position;
        }
    }
}
