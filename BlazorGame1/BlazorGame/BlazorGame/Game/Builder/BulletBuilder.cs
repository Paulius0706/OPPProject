using BlazorGame.Game.GameObjects;
using static BlazorGame.Game.GameObjects.GameObject;

namespace BlazorGame.Game.Builder
{
    public class BulletBuilder : Builder
    {
        public BulletBuilder(float[] position, float[] velocity)
        {
            base.gameObject = new BulletObject();
            base.gameObject.mass = 1f;
            base.gameObject.objectType = ObjectType.bullet;
            base.gameObject.deacceleration = GameObject.BulletDecceleration;
            base.gameObject.position = position;
            base.gameObject.velocity = velocity;
        }

    }
}
