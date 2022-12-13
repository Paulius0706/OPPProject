using BlazorGame.Game.GameObjects;
using static BlazorGame.Game.GameObjects.GameObject;

namespace BlazorGame.Game.Builder
{
    public class MobBuilder : Builder
    {
        public MobBuilder(float[] position, float[] velocity)
        {
            base.gameObject = new MobObject();
            base.gameObject.Mass = 3f;
            base.gameObject.objectType = ObjectType.Mob;
            base.gameObject.Deacceleration = GameObject.BulletDecceleration;
            base.gameObject.Position = position;
            base.gameObject.Velocity = velocity;
        }
    }
}
