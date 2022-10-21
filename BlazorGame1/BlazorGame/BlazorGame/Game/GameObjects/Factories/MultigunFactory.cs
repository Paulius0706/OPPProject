﻿using BlazorGame.Game.Builder;

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
            Director.director.Construct(ref bulletBuilder, gameObject.id, player.damage, player.damage);
            return bulletBuilder.GetResult() as BulletObject;
        }

        public override CollectibleObject CreateCollectibleObject()
        {
            //needs override
            CollectibleBuilder collectibleBuilder = new CollectibleBuilder(new float[] { 0, 0 });
            Director.director.Construct(ref collectibleBuilder, GameComponents.Collectible.CollectibleType.col1);
            return collectibleBuilder.GetResult() as CollectibleObject;
        }
    }
}