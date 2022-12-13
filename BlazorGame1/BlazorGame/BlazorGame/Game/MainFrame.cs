// Copyright (c) LAB 4 KTU. All rights reserved.
using System.Diagnostics;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.Builder;
using BlazorGame.Game.Command;
using BlazorGame.Game.GameComponents.RendersDecorum.FlyWeight;
using BlazorGame.Game.GameComponents.Units;
using BlazorGame.Game.GameObjects;
using BlazorGame.Game.Mediator;

namespace BlazorGame.Game
{
    /// <summary>
    /// Sets backGround service and game engine.
    /// </summary>
    public class MainFrame : BackgroundService
    {
        public static bool GameStarted = false;
        public static int OffsetX = 1280 / 2;
        public static int OffsetY = 720 / 2;
        public static int GameObjectsCounting = 0;
        public static float DeltaTime;
        public static List<Score> Scores = new List<Score>();
        public static Dictionary<int, GameObject> GameObjects = new Dictionary<int, GameObject>();

        public static event Func<DateTime, Task> UpdateEvent = async delegate(DateTime s) { };

        private static Queue<GameObject> CreateGameObjectsQueue = new Queue<GameObject>();
        private static Queue<int> DestroyGameObjectsQueue = new Queue<int>();
        private static int width = 1280;
        private static int height = 720;
        private static int scoreWidth = 250;
        private static int scoreHeight = 50;

        private Stopwatch watch = new Stopwatch();

        /// <summary>
        /// Adds score to existing one.
        /// </summary>
        /// <param name="playerId"> player indentificator. </param>
        /// <param name="score"> ammount of points. </param>
        public static void AddScore(int playerId, float score)
        {
            foreach (var item in Scores)
            {
                if (item.PlayerId == playerId)
                {
                    item.ScorePoints += score;
                }
            }
        }

        /// <summary>
        /// Adds new score points item.
        /// </summary>
        /// <param name="playerId"> player indentificator. </param>
        public static void SetScore(int playerId)
        {
            Scores.Add(new Score(playerId, 0));
        }

        public static void DeleteScore(int playerId)
        {
            for (int i = 0; i < Scores.Count; i++)
            {
                if (Scores[i].PlayerId == playerId)
                {
                    Scores.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Renders scores.
        /// </summary>
        /// <param name="context"> client canvas. </param>
        public static void RenderScores(ref Canvas2DContext context)
        {
            for (int i = 0; i < Math.Min(MainFrame.Scores.Count, 10); i++)
            {
                if (MainFrame.GameObjects.ContainsKey(Scores[i].PlayerId) && GameObjects[Scores[i].PlayerId] is PlayerObject)
                {
                    string str = (i + 1) + ". " + MainFrame.GameObjects[Scores[i].PlayerId].GetComponent<Player>().name + " " + Scores[i].ScorePoints;
                    context.SetFillStyleAsync("silver");
                    context.FillRectAsync(width - scoreWidth - 5, +5 + (i * (scoreHeight + 5)), scoreWidth, scoreHeight);
                    context.SetFontAsync("48px MathSansItalic");
                    context.StrokeTextAsync(str, width - scoreWidth, +20 + (i * (scoreHeight + 5)) + (scoreHeight / 2f), scoreWidth);
                }
            }
        }

        /// <summary>
        /// Creates new player.
        /// </summary>
        /// <param name="name">Name of player.</param>
        /// <param name="spawner">Set spawner.</param>
        /// <returns>Player id.</returns>
        public static int CreateNewPlayer(string name, bool spawner = true)
        {
            // instatijuoti player
            PlayerBuilder playerBuilder = new PlayerBuilder(new float[] { 0, 0 });
            Director.GetInstance().Construct(ref playerBuilder, name, 1);

            GameObject gameObject = playerBuilder.GetResult();
            if (!spawner && gameObject is PlayerObject) { ((PlayerObject)gameObject).GetComponent<Player>().setSpawner = false; }

            gameObject.Id = GameObjectsCounting;
            GameObjectsCounting++;
            Instantiate(gameObject);
            GameStarted = true;

            Console.WriteLine("GAME SATRT playerID:" + gameObject.Id + " PlayerComponentIndex:" + 0 + " GameObjects-exist: " + (GameObjects != null));
            SetScore(gameObject.Id);
            return gameObject.Id;
        }

        /// <summary>
        /// Removes object in the game.
        /// </summary>
        /// <param name="gameObject">Object.</param>
        public static void Destroy(GameObject gameObject)
        {
            DestroyGameObjectsQueue.Enqueue(gameObject.Id);
        }

        /// <summary>
        /// Adds object to the game.
        /// </summary>
        /// <param name="gameObject">Object.</param>
        public static void Instantiate(GameObject gameObject)
        {
            CreateGameObjectsQueue.Enqueue(gameObject);
        }

        /// <summary>
        /// Sets game time.
        /// </summary>
        public static void Update()
        {
            if (GameStarted)
            {
                DamageMediator.Update();
                foreach (GameObject gameObject in GameObjects.Values)
                {
                    gameObject.Update();
                }

                while (CreateGameObjectsQueue.Count > 0)
                {
                    GameObject gameObject = CreateGameObjectsQueue.Dequeue();
                    if (gameObject.Id == -1)
                    {
                        gameObject.Id = GameObjectsCounting;
                        GameObjectsCounting++;
                    }

                    if (!GameObjects.ContainsKey(gameObject.Id))
                    {
                        GameObjects.Add(gameObject.Id, gameObject);
                        GameObjects[gameObject.Id].ConnectionUpdate();
                    }
                }

                while (DestroyGameObjectsQueue.Count > 0)
                {
                    GameObjects.Remove(DestroyGameObjectsQueue.Dequeue());
                }
            }
        }

        /// <summary>
        /// Execute starting code.
        /// </summary>
        /// <param name="stoppingToken">LOL param.</param>
        /// <returns> Not needed.</returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(100);
            RenderLib.Load();
            while (!stoppingToken.IsCancellationRequested)
            {
                if (!this.watch.IsRunning) 
                {
                    this.watch.Start(); 
                }

                if (this.watch.ElapsedMilliseconds > 60)
                {
                    DeltaTime = (float)this.watch.ElapsedMilliseconds / 1000f;
                    this.watch.Restart();
                    Update();
                    if (UpdateEvent != null) 
                    {
                        await UpdateEvent?.Invoke(DateTime.Now); 
                    }

                    Scores.Sort((emp1, emp2) => emp2.ScorePoints.CompareTo(emp1.ScorePoints));
                }
            }
        }
    }
}
