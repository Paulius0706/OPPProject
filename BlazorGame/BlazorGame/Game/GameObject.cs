using Blazor.Extensions.Canvas.Canvas2D;
using System.Numerics;

namespace BlazorGame.Game
{
    public class GameObject
    {

        //LOGIC
        public int id { get; set; }
        public float[] position { get; set; }
        public float[] velocity { get; set; }
        public bool Unmoving { get; set; } // states if object can have velocity
        public bool ColliderDetector { get; set; }

        public List<ObjectComponent> components;
        public List<Render> renders;

        public static GameObject CreatePlayer()
        {
            Player playerComponent = new Player("Admin", 1);
            Collider collider = new Collider(new float[] { -50f, -50f }, 100f, false, true);
            GameObject gameObject = new GameObject(new float[] { 1280f / 2f, 720f / 2f });
            gameObject.AddComponent(playerComponent);
            gameObject.AddComponent(collider);
            gameObject.renders.Add(new Game.Render(new int[] { 0, 0 }, new int[] { 0, 50 }, 10, "black"));
            gameObject.renders.Add(new Game.Render(new int[] { -50, -50 }, new int[] { 50, 50 }, Game.Render.Type.box, "green"));
            gameObject.renders.Add(new Game.Render(new int[] { 0, 0 }, new int[] { 50, 50 }, Game.Render.Type.box, "green"));
            gameObject.renders.Add(new Game.Render(new int[] { -50, 0 }, new int[] { 50, 50 }, Game.Render.Type.box, "black"));
            gameObject.renders.Add(new Game.Render(new int[] { 0, -50 }, new int[] { 50, 50 }, Game.Render.Type.box, "black"));
            gameObject.renders.Add(new Game.Render(new int[] { 0, 0 }, 50, "red"));

            return gameObject;
        }

        public GameObject(float[] position)
        {
            id = MainFrame.gameObjectsCounting;
            MainFrame.gameObjectsCounting++;
            this.position = position;
            this.velocity = new float[2];
            this.components = new List<ObjectComponent>();
            this.renders = new List<Render>();
        }

        public void AddComponent(ObjectComponent objectComponent)
        {
            objectComponent.gameObject = id;
            components.Add(objectComponent);
        }

        public void Update()
        {
            //needs collider
            if (!Unmoving)
            {
                UpdateVelocity();
                position[0] += velocity[0] * MainFrame.detaTime;
                position[1] += velocity[1] * MainFrame.detaTime;
            }
            foreach (ObjectComponent objectComponent in components)
            {
                objectComponent.Update();
            }
            

        }
        public void UpdateVelocity()
        {
            if (components[0].GetType() == typeof(Player))
            {
                //easy access to inputs
                float moveSpeed = (components[0] as Player).moveSpeed;
                float[] inputs = (components[0] as Player).inputs;
                // velocity change
                float x = 0f;
                float y = 0f;
                // acceleration
                x = inputs[0] > 0f ?  moveSpeed * MainFrame.detaTime : x;
                x = inputs[0] > 0f && velocity[0] >  moveSpeed ? 0f  : x;

                x = inputs[0] < 0f ? -moveSpeed * MainFrame.detaTime : x;
                x = inputs[0] < 0f && velocity[0] < -moveSpeed ? 0f  : x;

                y = inputs[1] > 0f ?  moveSpeed * MainFrame.detaTime : y;
                y = inputs[1] > 0f && velocity[1] >  moveSpeed ? 0f  : y;

                y = inputs[1] < 0f ? -moveSpeed * MainFrame.detaTime : y;
                y = inputs[1] < 0f && velocity[1] < -moveSpeed ? 0f  : y;

                //deacceleration
                if (inputs[0] == 0)
                {
                    if (velocity[0] > 0) { x = velocity[0] > moveSpeed * MainFrame.detaTime ? -moveSpeed * MainFrame.detaTime : -velocity[0]; }
                    if (velocity[0] < 0) { x = velocity[0] < -moveSpeed * MainFrame.detaTime ? moveSpeed * MainFrame.detaTime : -velocity[0]; }
                }
                if (inputs[1] == 0)
                {
                    if (velocity[1] > 0) { y = velocity[1] > moveSpeed * MainFrame.detaTime ? -moveSpeed * MainFrame.detaTime : -velocity[1]; }
                    if (velocity[1] < 0) { y = velocity[1] < -moveSpeed * MainFrame.detaTime ? moveSpeed * MainFrame.detaTime : -velocity[1]; }
                }
                //assing new velocity
                velocity[0] += x;
                velocity[1] += y;
            }
        }
        public void Render(int playerId, ref Canvas2DContext context)
        {
            int xoffset = (int)MainFrame.gameObjects[playerId].position[0] + 1280 / 2;
            int yoffset = (int)MainFrame.gameObjects[playerId].position[1] + 720 / 2;
            //Console.WriteLine("ren count:" + renders.Count);
            for (int i = 0; i < renders.Count; i++)
            {
                if (renders[i].type == Game.Render.Type.box)
                {
                    context.SetFillStyleAsync(renders[i].str);
                    context.FillRectAsync(
                        position[0] +renders[i].offset[0] - xoffset,
                        position[1] +renders[i].offset[1] - yoffset,
                        renders[i].size[0], renders[i].size[1]
                        );
                }
                if (renders[i].type == Game.Render.Type.circle)
                {
                    context.SetFillStyleAsync(renders[i].str);
                    context.BeginPathAsync();
                    context.ArcAsync(
                        position[0] + renders[i].offset[0] - xoffset,
                        position[1] + renders[i].offset[1] - yoffset,
                        renders[i].radius, 0, 2 * Math.PI
                        );
                    context.ClosePathAsync();
                    context.FillAsync();
                    context.StrokeAsync();
                }
                //if (renders[i].type == Game.Render.Type.line)
                //{
                //    //context.SetFillStyleAsync(renders[i].str);
                //    context.BeginPathAsync();
                //    context.MoveToAsync(
                //        position[0] + renders[i].offset[0] - xoffset,
                //        position[1] + renders[i].offset[1] - yoffset);
                //    context.LineToAsync(
                //        position[0] + renders[i].offset1[0] - xoffset,
                //        position[1] + renders[i].offset1[1] - yoffset);
                //    context.StrokeAsync();
                //}
            }
        }
    }
}
