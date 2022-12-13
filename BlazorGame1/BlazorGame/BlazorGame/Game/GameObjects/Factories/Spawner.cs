namespace BlazorGame.Game.GameObjects.Factories
{
    public class Spawner
    {
        public BulletObject bulletObject;
        public CollectibleObject collectibleObject;
        public MobObject mobObject;
        // Constructor
        public Spawner(Factory factory)
        {
            bulletObject = factory.CreateBulletObject();
            collectibleObject = factory.CreateCollectibleObject();
            mobObject = factory.CreateMobObject();
        }
        public void Mutate()
        {
            bulletObject.Mutate();
            collectibleObject.Mutate();
            mobObject.Mutate();
        }
    }
}
