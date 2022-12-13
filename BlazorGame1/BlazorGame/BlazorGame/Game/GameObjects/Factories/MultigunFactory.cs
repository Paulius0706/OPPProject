using BlazorGame.Game.Builder;
using BlazorGame.Game.GameComponents.Units;

namespace BlazorGame.Game.GameObjects.Factories
{
    public class MultigunFactory : Factory
    {
        public MultigunFactory(GameObject gameObject)
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
            Director.GetInstance().Construct(ref collectibleBuilder, Collectible.CollectibleType.col2);
            return collectibleBuilder.GetResult() as CollectibleObject;
        }

        public override MobObject CreateMobObject()
        {
            MobBuilder mobBuilder = new MobBuilder(new float[] { 0, 0 }, new float[] { 0, 0 });
            Director.GetInstance().Construct(ref mobBuilder, 1);
            return mobBuilder.GetResult() as MobObject;
        }
    }
}
