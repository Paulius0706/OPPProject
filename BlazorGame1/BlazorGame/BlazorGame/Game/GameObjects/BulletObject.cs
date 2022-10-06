using static System.Net.Mime.MediaTypeNames;

namespace BlazorGame.Game.GameObjects
{
    public class BulletObject : GameObject
    {
        public enum BulletComponents
        {
            bullet = 0,
            collider = 1
        }


        public override void Create()
        {
            base.objectType = ObjectType.bullet;
            base.mass = 1f;
            base.deacceleration = BulletDecceleration;

        }
    }
}
