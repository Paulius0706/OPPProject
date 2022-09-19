using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using System.Diagnostics;
using System.Numerics;

namespace BlazorGame.Game
{
    public class MainFrame : BackgroundService
    {
        public static event Func<DateTime, Task> updateEvent;
        public static event Func<Task> renderEvent;
        public static event Func<Task> keyResetEvent;
        public static Stopwatch watch = new Stopwatch();
        public static float detaTime;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(20);
                if (!watch.IsRunning) watch.Start();
                if(watch.ElapsedMilliseconds > 16 || true)
                {
                    detaTime = watch.ElapsedMilliseconds / 1000f;
                    watch.Restart();
                    Update();
                    if (updateEvent != null) await updateEvent?.Invoke(DateTime.Now);
                    
                    //if (keyResetEvent != null) keyResetEvent?.Invoke();
                }

                
                
            }
        }
        


        public static int gameObjectsCounting = 0;
        public static Dictionary<int, GameObject> gameObjects= new Dictionary<int, GameObject>();
        public static bool gameStarted = false;

        public static int CreateNewPlayer()
        {
            // instatijuoti player
            Player playerComponent = new Player("Admin", 1);
            Collider collider = new Collider(new float[] { -50f, -50f }, 100f, false, true);
            GameObject gameObject = new GameObject(new float[] { 1280f / 2f, 720f / 2f });
            gameObject.AddComponent(playerComponent);
            gameObject.AddComponent(collider);
            gameObject.renders.Add(new Game.Render(new int[] { -50, -50 }, new int[] { 50, 50 }, Game.Render.Type.box, "green"));
            gameObject.renders.Add(new Game.Render(new int[] { 0, 0 }, new int[] { 50, 50 }, Game.Render.Type.box, "green"));
            gameObject.renders.Add(new Game.Render(new int[] { -50, 0 }, new int[] { 50, 50 }, Game.Render.Type.box, "black"));
            gameObject.renders.Add(new Game.Render(new int[] { 0 , -50 }, new int[] { 50, 50 }, Game.Render.Type.box, "black"));
            gameObject.renders.Add(new Game.Render(new int[] { 0, 0 },50, "red"));
            gameObjects.Add(gameObject.id, gameObject);
            

            // sets render and update active
            gameStarted = true;

            //testing
            Console.WriteLine("GAME SATRT playerID:" + gameObject.id + " PlayerComponentIndex:" + 0 + " GameObjects-exist: " + (gameObjects != null));
            return gameObject.id;
        }
        public static void Update()
        {
            if (gameStarted)
            {
                foreach (GameObject gameObject in gameObjects.Values)
                {
                    gameObject.Update();
                }
            }
        }
        public static void Render(int playerId, ref Canvas2DContext context)
        {
            if (gameStarted)
            {
                context.SetFillStyleAsync("lightgray");
                context.FillRectAsync(0, 0, 1280, 720);
                foreach (GameObject gameObject in gameObjects.Values)
                {
                    gameObject.Render(playerId, ref context);
                }
            }
        }
        public static void KeyUpdate(int playerId, string key)
        {
            //Console.WriteLine("keyupdate");
            if (key == "w" || key == "W") { (gameObjects[playerId].components[0] as Player).CorrectInputs(0, -1); }
            if (key == "s" || key == "S") { (gameObjects[playerId].components[0] as Player).CorrectInputs(0, 1); }
            if (key == "a" || key == "A") { (gameObjects[playerId].components[0] as Player).CorrectInputs(-1, 0); }
            if (key == "d" || key == "D") { (gameObjects[playerId].components[0] as Player).CorrectInputs(1, 0); }

        }
    }
}
