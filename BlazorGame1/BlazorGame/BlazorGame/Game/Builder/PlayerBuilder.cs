using BlazorGame.Game.GameObjects;
using System;
using static BlazorGame.Game.GameObjects.GameObject;
using static System.Net.Mime.MediaTypeNames;

namespace BlazorGame.Game.Builder
{
    public class PlayerBuilder : Builder
    {
        public PlayerBuilder(float[] position)
        {
            base.gameObject = new PlayerObject();
            base.gameObject.mass = 10f;
            base.gameObject.objectType = ObjectType.player;
            base.gameObject.position = position;
        }
    }
}
