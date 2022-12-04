using BlazorGame.Game.GameObjects;
using static BlazorGame.Game.GameObjects.GameObject;

namespace BlazorGame.Game.Builder
{
    public class CollectibleBuilder : Builder
    {
        public CollectibleBuilder(float[] position)
        {
            base.gameObject = new CollectibleObject();
            base.gameObject.Mass = 1f;
            base.gameObject.objectType = ObjectType.Collectible;
            base.gameObject.Deacceleration = GameObject.CollectiblesDecceleration;
            base.gameObject.Position = position;
        }
    }
}
