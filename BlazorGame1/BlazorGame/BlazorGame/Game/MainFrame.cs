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
        public static event Func<DateTime,Task> updateEvent;
        public static event Func<Task> mouseEvent;
        public static event Func<Task> keyResetEvent;
        public static Stopwatch watch = new Stopwatch();
        public static float detaTime;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(100);
            while (!stoppingToken.IsCancellationRequested)
            {
                
                if (!watch.IsRunning) watch.Start();
                if(watch.ElapsedMilliseconds > 60)
                {
                    detaTime = (float)watch.ElapsedMilliseconds / 1000f;
                    watch.Restart();
                    Update();
                    if (updateEvent != null) await updateEvent?.Invoke(DateTime.Now);
                }

                
                
            }
        }
        


        public static int gameObjectsCounting = 0;
        public static Dictionary<int, GameObject> gameObjects= new Dictionary<int, GameObject>();
        public static Queue<GameObject> createGameObjectsQueue = new Queue<GameObject>();
        public static Queue<int> destroyGameObjectsQueue = new Queue<int>();
        public static bool gameStarted = false;
        public static int offsetX = 1280 / 2;
        public static int offsetY = 720 / 2;

        public static int CreateNewPlayer()
        {
            // instatijuoti player
            GameObject gameObject = GameObject.CreatePlayer();
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
                //Console.WriteLine("objects count:" + gameObjects.Count);
                foreach (GameObject gameObject in gameObjects.Values)
                {
                    gameObject.Update();
                }
                while(createGameObjectsQueue.Count > 0)
                {
                    GameObject gameObject = createGameObjectsQueue.Dequeue();
                    gameObjects.Add(gameObject.id, gameObject);
                }
                while(destroyGameObjectsQueue.Count > 0)
                {
                    gameObjects.Remove(destroyGameObjectsQueue.Dequeue());
                }
            }
        }
        public static void Render(int playerId, ref Canvas2DContext context)
        {
            if (gameStarted)
            {
                context.SetFillStyleAsync("lightgray");
                context.FillRectAsync(0, 0, 1280, 720);
                //Console.WriteLine("obj count:" + gameObjects.Count);
                foreach (GameObject gameObject in gameObjects.Values)
                {
                    gameObject.Render(playerId, ref context);
                }
            }
        }
        /// <summary>
        /// [Depricated]
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="key"></param>
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
