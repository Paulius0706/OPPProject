using BlazorGame.Game.Builder;
using BlazorGame.Game.GameComponents;
using BlazorGame.Game.GameComponents.RendersDecorum;
using BlazorGame.Game.GameComponents.RendersDecorum.Decorator;

namespace BlazorGame.Game.GameObjects
{
    public class PlayerObject : GameObject
    {
        public override void Create()
        {
            //conclrete Product
            base.objectType = ObjectType.player;
            base.mass = 10f;

            Renders renders = new Renders();

            //guns
            GunsDecorator gunRender = new GunsDecorator();
            renders.renders.Add(gunRender);

            //body
            renders.renders.Add(new CircleRender(new int[] { 0, 0}, 50, "blue"));

            //health
            BoxRender healthBackGround = new BoxRender(new int[] { -40, 28 }, new int[] { 80, 10 }, "grey");
            BoxRender health = new BoxRender(new int[] { -39, 29 }, new int[] { 78, 8 }, "red");
            HealthDecorator healthDecorator = new HealthDecorator(health, healthBackGround);
            renders.renders.Add(healthDecorator);

            //exp
            BoxRender expBackGround = new BoxRender(new int[] { -40, 38 }, new int[] { 80, 10 }, "grey");
            BoxRender exp = new BoxRender(new int[] { -39, 39 }, new int[] { 78, 8 }, "yellow");
            ExperienceDecorator experienceDecorator = new ExperienceDecorator(exp, expBackGround);
            renders.renders.Add(experienceDecorator);

            AddComponent(renders);
        }

        public void SetInputs(int x, int y)
        {
            GetComponent<Player>().SetInputs(x, y);
        }
        public void SetCannons(int x, int y)
        {
            GetComponent<Cannons>().SetCannons(x, y);
        }
        public void SetShooting(bool shooting)
        {
            GetComponent<Cannons>().SetShooting(shooting);
        }
    }
}
