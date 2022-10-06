using BlazorGame.Game.GameComponents;

namespace BlazorGame.Game.GameObjects
{
    public class PlayerObject : GameObject
    {

        public enum PlayerRenders
        {

            body = 6,
            expBar = 7,
            exp = 8,
            healthBar = 9,
            health = 10,
        }
        public enum PlayerComponents
        {
            player = 0,
            cannon = 1,
            collider = 2
        }

        public override void Create()
        {
            base.objectType = ObjectType.player;
            base.mass = 10f;
        }

        public void SetInputs(int x, int y)
        {
            (components[(int)PlayerComponents.player] as Player).SetInputs(x, y);
        }
        public void SetCannons(int x, int y)
        {
            (components[(int)PlayerComponents.cannon] as Cannons).SetCannons(x, y);
        }
        public void SetShooting(bool shooting)
        {
            (components[(int)PlayerComponents.cannon] as Cannons).SetShooting(shooting);
        }
    }
}
