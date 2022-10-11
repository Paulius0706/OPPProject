using BlazorGame.Game.GameComponents;
using BlazorGame.Game.GameObjects;
using static BlazorGame.Game.GameObjects.GameObject;

namespace BlazorGame.Game.Builder
{
    public class Director
    {
        public static Director director = new Director();
        public void Construct(ref PlayerBuilder playerBuilder, string name, int level)
        {
            // add components
            playerBuilder.BuildPlayer(name, level);
            playerBuilder.BuildCannons();
            playerBuilder.BuildCicleCollider(new float[] { 0f, 0f }, 50f, false, true);
        }
        public void Construct(ref CollectibleBuilder collectibleBuilder, Collectible.CollectibleType collectibleType)
        {
            // add components
            collectibleBuilder.BuildCollectible(collectibleType);
            collectibleBuilder.BuildCicleCollider(new float[] { 0f, 0f }, 20f);
        }
        public void Construct(ref BulletBuilder bulletBuilder, int shooter, float damage, float health)
        {
            // add components
            bulletBuilder.BuildBullet(shooter, damage, health);
            bulletBuilder.BuildCicleCollider(new float[] { 0f, 0f }, 15f,false,true);
        }
    }
}
