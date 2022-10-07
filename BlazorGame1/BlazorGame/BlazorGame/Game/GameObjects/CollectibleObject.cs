namespace BlazorGame.Game.GameObjects
{
    public class CollectibleObject : GameObject
    {
        public override void Create()
        {
            base.objectType = ObjectType.collectible;
            base.mass = 1f;
            base.deacceleration = CollectiblesDecceleration;
        }
    }
}
