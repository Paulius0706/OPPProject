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
            Render render;
            // add cannon renders
            render = new Render(); render.purpose = Render.Purpose.cannonLines;
            playerBuilder.BuildRender(render);
            render = new Render(); render.purpose = Render.Purpose.cannonLines;
            playerBuilder.BuildRender(render);
            render = new Render(); render.purpose = Render.Purpose.cannonLines;
            playerBuilder.BuildRender(render);
            render = new Render(new int[] { 0, 0 }, new int[] { 0, 100 }, 10, "black"); render.purpose = Render.Purpose.cannon;
            playerBuilder.BuildRender(render);
            render = new Render(); render.purpose = Render.Purpose.cannon;
            playerBuilder.BuildRender(render);
            render = new Render(); render.purpose = Render.Purpose.cannon;
            playerBuilder.BuildRender(render);

            // add body renders
            render = new Render(new int[] { 0, 0 }, 50, "blue"); render.purpose = Render.Purpose.body;
            //render = new Render(); render.purpose = Render.Purpose.body;
            playerBuilder.BuildRender(render);
            // add health bar renders
            render = new Render(new int[] { -40, 28 }, new int[] { 80, 10 }, Render.Type.box, "grey"); render.purpose = Render.Purpose.healthBarFrame;
            playerBuilder.BuildRender(render);
            render = new Render(new int[] { -39, 29 }, new int[] { 78, 8  }, Render.Type.box, "red"); render.purpose = Render.Purpose.healthBar;
            playerBuilder.BuildRender(render);
            // add exp bar renders
            render = new Render(new int[] { -40, 38 }, new int[] { 80, 6 }, Render.Type.box, "grey"); render.purpose = Render.Purpose.expBarFrame;
            playerBuilder.BuildRender(render);
            render = new Render(new int[] { -39, 39 }, new int[] { 78, 4 }, Render.Type.box, "yellow"); render.purpose = Render.Purpose.expBar;
            playerBuilder.BuildRender(render);
        }
        public void Construct(ref CollectibleBuilder collectibleBuilder)
        {
            // add components
            collectibleBuilder.BuildCollectible(Collectible.CollectibleType.col1);
            collectibleBuilder.BuildCicleCollider(new float[] { 0f, 0f }, 20f);
            Render render;
            // add body renders
            render = new Render(new int[] { 0, 0 }, 20, "brown"); render.purpose = Render.Purpose.body;
            collectibleBuilder.BuildRender(render);
            // add health bar renders
            render = new Render(new int[] { -15, 5 }, new int[] { 30, 10 }, Render.Type.box, "grey"); render.purpose = Render.Purpose.healthBarFrame;
            collectibleBuilder.BuildRender(render);
            render = new Render(new int[] { -14, 6 }, new int[] { 28, 8 }, Render.Type.box, "red"); render.purpose = Render.Purpose.healthBar;
            collectibleBuilder.BuildRender(render);
        }
        public void Construct(ref BulletBuilder bulletBuilder, int shooter, float damage, float health)
        {
            // add components
            bulletBuilder.BuildBullet(shooter, damage, health);
            bulletBuilder.BuildCicleCollider(new float[] { 0f, 0f }, 15f,false,true);
            Render render;
            // add body renders
            render = new Render(new int[] { 0, 0 }, 15, "red"); render.purpose = Render.Purpose.body;
            bulletBuilder.BuildRender(render);
        }
    }
}
