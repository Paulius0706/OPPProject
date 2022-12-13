// Copyright (c) LAB 4 KTU. All rights reserved.
using BlazorGame.Game.Builder;
using BlazorGame.Game.GameComponents;
using BlazorGame.Game.GameComponents.RendersDecorum;
using BlazorGame.Game.GameComponents.RendersDecorum.Decorator;
using BlazorGame.Game.GameComponents.Units;

namespace BlazorGame.Game.GameObjects
{
    public class PlayerObject : GameObject
    {
        public override void Create()
        {
            // conclrete Product
            this.objectType = ObjectType.Player;
            this.Mass = 10f;

            Renders renders = new Renders();

            // guns
            GunsDecorator gunRender = new GunsDecorator();
            renders.renders.Add(gunRender);

            // body
            renders.renders.Add(new CircleRender(new int[] { 0, 0 }, 50, "blue"));

            // health
            BoxRender healthBorder = new BoxRender(new int[] { -40, 28 }, new int[] { 80, 10 }, "grey");
            BoxRender healthBackGround = new BoxRender(new int[] { -39, 29 }, new int[] { 78, 8 }, "black");
            FillerSpaceDecorator healthBar = new FillerSpaceDecorator(healthBackGround, healthBorder);
            BoxRender health = new BoxRender(new int[] { -39, 29 }, new int[] { 78, 8 }, "red");
            HealthDecorator healthDecorator = new HealthDecorator(health, healthBar);
            renders.renders.Add(healthDecorator);

            // exp
            BoxRender expBackBorder = new BoxRender(new int[] { -40, 38 }, new int[] { 80, 10 }, "grey");
            BoxRender expBackGround = new BoxRender(new int[] { -39, 39 }, new int[] { 78, 8 }, "white");
            FillerSpaceDecorator expBar = new FillerSpaceDecorator(expBackGround, expBackBorder);
            BoxRender exp = new BoxRender(new int[] { -39, 39 }, new int[] { 78, 8 }, "yellow");
            ExperienceDecorator experienceDecorator = new ExperienceDecorator(exp, expBar);
            renders.renders.Add(experienceDecorator);

            this.AddComponent(renders);
        }

        public void SetInputs(int x, int y)
        {
            this.GetComponent<Player>().SetInputs(x, y);
        }

        public void SetCannons(int x, int y)
        {
            this.GetComponent<Cannons>().SetCannons(x, y);
        }

        public void SetShooting(bool shooting)
        {
            this.GetComponent<Cannons>().SetShooting(shooting);
        }

        public void RenderUI()
        {
        }
    }
}
