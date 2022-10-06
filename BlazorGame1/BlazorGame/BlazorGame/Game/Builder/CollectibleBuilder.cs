using BlazorGame.Game.GameObjects;
using static BlazorGame.Game.GameObjects.GameObject;

namespace BlazorGame.Game.Builder
{
    public class CollectibleBuilder : Builder
    {
        public CollectibleBuilder(float[] position)
        {
            base.gameObject = new CollectibleObject();
            base.gameObject.mass = 1f;
            base.gameObject.objectType = ObjectType.collectible;
            base.gameObject.deacceleration = GameObject.CollectiblesDecceleration;
            base.gameObject.position = position;
        }
    }
}
