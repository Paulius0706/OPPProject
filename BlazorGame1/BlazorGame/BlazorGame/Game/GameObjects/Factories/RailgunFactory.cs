using BlazorGame.Game.Builder;

namespace BlazorGame.Game.GameObjects.Factories
{
    public class RailgunFactory : Factory
    {
        public RailgunFactory(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
        public override BulletObject CreateBulletObject()
        {
            //needs override
            BulletBuilder bulletBuilder = new BulletBuilder(new float[] { 0, 0 }, new float[] { 0, 0 });
            Director.GetInstance().Construct(ref bulletBuilder, gameObject.Id, player.damage, player.damage);
            return bulletBuilder.GetResult() as BulletObject;
        }

        public override CollectibleObject CreateCollectibleObject()
        {
            //needs override
            CollectibleBuilder collectibleBuilder = new CollectibleBuilder(new float[] { 0, 0 });
            Director.GetInstance().Construct(ref collectibleBuilder, GameComponents.Collectible.CollectibleType.col1);
            return collectibleBuilder.GetResult() as CollectibleObject;
        }
    }
}
