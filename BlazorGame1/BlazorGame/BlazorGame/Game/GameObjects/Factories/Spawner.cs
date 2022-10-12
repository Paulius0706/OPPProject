namespace BlazorGame.Game.GameObjects.Factories
{
    public class Spawner
    {
        public BulletObject bulletObject;
        public CollectibleObject collectibleObject;
        // Constructor
        public Spawner(Factory factory)
        {
            bulletObject = factory.CreateBulletObject();
            collectibleObject = factory.CreateCollectibleObject();
        }
        public void Mutate()
        {
            bulletObject.Mutate();
            collectibleObject.Mutate();
        }
    }
}
