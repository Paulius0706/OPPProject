using BlazorGame.Game.GameComponents;
using BlazorGame.Game.GameObjects;
using static BlazorGame.Game.GameObjects.GameObject;

namespace BlazorGame.Game.Builder
{
    public class Director
    {
        private static Director director = null;

        public static void Construct(ref PlayerBuilder playerBuilder, string name, int level)
        {
            if(director == null) director = new Director();
            // add components
            playerBuilder.BuildPlayer(name, level);
            playerBuilder.BuildCannons();
            playerBuilder.BuildCicleCollider(new float[] { 0f, 0f }, 50f, false, true);
        }
        public static void Construct(ref CollectibleBuilder collectibleBuilder, Collectible.CollectibleType collectibleType)
        {
			if (director == null) director = new Director();
			// add components
			collectibleBuilder.BuildCollectible(collectibleType);
            collectibleBuilder.BuildCicleCollider(new float[] { 0f, 0f }, 20f);
        }
        public static void Construct(ref BulletBuilder bulletBuilder, int shooter, float damage, float health)
        {
			if (director == null) director = new Director();
			// add components
			bulletBuilder.BuildBullet(shooter, damage, health);
            bulletBuilder.BuildCicleCollider(new float[] { 0f, 0f }, 15f,false,true);
        }
    }
}
