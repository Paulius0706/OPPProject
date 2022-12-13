using BlazorGame.Game.GameComponents.Units;
using BlazorGame.Game.GameObjects;
using static BlazorGame.Game.GameObjects.GameObject;
using static System.Net.Mime.MediaTypeNames;

namespace BlazorGame.Game.Builder
{
    public sealed class Director
    {
        private static readonly Director director = new Director();
        
		private static object threadLock = new object();
		private Director() { }
		public static Director GetInstance()
        {
           lock(threadLock) { return director;}
        }
        public void Construct(ref PlayerBuilder playerBuilder, string name, int level)
        {
           // if(director == null) director = new Director();
            // add components
            playerBuilder.BuildPlayer(name, level);
            playerBuilder.BuildCannons();
            playerBuilder.BuildCicleCollider(new float[] { 0f, 0f }, 50f, false, true);
        }
        public void Construct(ref CollectibleBuilder collectibleBuilder, Collectible.CollectibleType collectibleType)
        {
			//if (director == null) director = new Director();
			// add components
			collectibleBuilder.BuildCollectible(collectibleType);
            collectibleBuilder.BuildCicleCollider(new float[] { 0f, 0f }, 20f);
        }
        public void Construct(ref BulletBuilder bulletBuilder, int shooter, float damage, float health)
        {
			//if (director == null) director = new Director();
			// add components
			bulletBuilder.BuildBullet(shooter, damage, health);
            bulletBuilder.BuildCicleCollider(new float[] { 0f, 0f }, 15f,false,true);
        }

        public void Construct(ref MobBuilder mobBuilder, int size)
        {
            //if (director == null) director = new Director();
            // add components
            mobBuilder.BuildMob();
            mobBuilder.BuildCicleCollider(new float[] { 0f, 0f }, 15f, false, true);
        }
    }
}
