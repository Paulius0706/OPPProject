namespace BlazorGame.Game.GameObjects
{
    public class CollectibleObject : GameObject
    {
        public enum CollectibleComponents
        {
            collectible = 0,
            collider = 1
        }

        public override void Create()
        {
            base.objectType = ObjectType.collectible;
            base.mass = 1f;
            base.deacceleration = CollectiblesDecceleration;
        }
    }
}
