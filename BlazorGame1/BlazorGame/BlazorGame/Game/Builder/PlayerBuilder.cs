using BlazorGame.Game.GameObjects;
using System;

namespace BlazorGame.Game.Builder
{
    public class PlayerBuilder : Builder
    {
        public PlayerBuilder(float[] position)
        {
            base.gameObject = new PlayerObject();
            base.gameObject.mass = 10f;
            base.gameObject.objectType = GameObject.ObjectType.player;
            base.gameObject.position = position;
        }
    }
}
