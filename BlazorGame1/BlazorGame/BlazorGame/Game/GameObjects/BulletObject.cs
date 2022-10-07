using static System.Net.Mime.MediaTypeNames;

namespace BlazorGame.Game.GameObjects
{
    public class BulletObject : GameObject
    {
        public override void Create()
        {
            base.objectType = ObjectType.bullet;
            base.mass = 1f;
            base.deacceleration = BulletDecceleration;

        }
    }
}
