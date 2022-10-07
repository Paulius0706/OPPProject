using BlazorGame.Game.GameComponents;

namespace BlazorGame.Game.GameObjects
{
    public class PlayerObject : GameObject
    {

        public override void Create()
        {
            //conclrete Product
            base.objectType = ObjectType.player;
            base.mass = 10f;
        }

        public void SetInputs(int x, int y)
        {
            (components[typeof(Player)] as Player).SetInputs(x, y);
        }
        public void SetCannons(int x, int y)
        {
            (components[typeof(Cannons)] as Cannons).SetCannons(x, y);
        }
        public void SetShooting(bool shooting)
        {
            (components[typeof(Cannons)] as Cannons).SetShooting(shooting);
        }
    }
}
