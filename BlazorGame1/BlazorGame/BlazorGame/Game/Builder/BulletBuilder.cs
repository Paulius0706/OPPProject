using BlazorGame.Game.GameObjects;
using static BlazorGame.Game.GameObjects.GameObject;

namespace BlazorGame.Game.Builder
{
    public class BulletBuilder : Builder
    {
        public BulletBuilder(float[] position, float[] velocity)
        {
            base.gameObject = new BulletObject();
            base.gameObject.Mass = 1f;
            base.gameObject.objectType = ObjectType.Bullet;
            base.gameObject.Deacceleration = GameObject.BulletDecceleration;
            base.gameObject.Position = position;
            base.gameObject.Velocity = velocity;
        }

    }
}
