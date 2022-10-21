﻿using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.Builder;
using BlazorGame.Game.Command;
using BlazorGame.Game.GameComponents;
using BlazorGame.Game.GameObjects;
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
            //foo()
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
        public static List<Score> scores = new List<Score>();
        public static Dictionary<int, GameObject> gameObjects= new Dictionary<int, GameObject>();
        private static Queue<GameObject> createGameObjectsQueue = new Queue<GameObject>();
        private static Queue<int> destroyGameObjectsQueue = new Queue<int>();
        public static bool gameStarted = false;
        public static int offsetX = 1280 / 2;
        public static int offsetY = 720 / 2;

        public static void addScore(int playerId, float score)
        {
            foreach (var item in scores)
            {
                if(item._playerId == playerId)
                {
                    item._score += score;
                }
            }
        }

        public static void deleteScore(int playerId)
        {
            for(int i = 0; i < scores.Count; i++)
            {
                if(scores[i]._playerId == playerId)
                {
                    scores.RemoveAt(i);
                    break;
                }
                
            }
        }

        public static void setScore(int playerId)
        {
            Score score = new Score(playerId,0);
        }

        public static int CreateNewPlayer(string name, bool spawner = true)
        {
            // instatijuoti player
            PlayerBuilder playerBuilder = new PlayerBuilder(new float[]{ 0,0});
            Director.director.Construct(ref playerBuilder,name,1);
            GameObject gameObject = playerBuilder.GetResult();
            if (!spawner) (gameObject as PlayerObject).GetComponent<Player>().setSpawner = false;
            gameObject.id = gameObjectsCounting;
            gameObjectsCounting++;
            Instantiate(gameObject);
            //gameObjectsCounting++;
            // sets render and update active
            gameStarted = true;

            //testing
            Console.WriteLine("GAME SATRT playerID:" + gameObject.id + " PlayerComponentIndex:" + 0 + " GameObjects-exist: " + (gameObjects != null));
            return gameObject.id;
        }
        public static void Destroy(GameObject gameObject)
        {
            destroyGameObjectsQueue.Enqueue(gameObject.id);
        }
        public static void Instantiate(GameObject gameObject)
        {
            createGameObjectsQueue.Enqueue(gameObject);
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
                    //Console.WriteLine("Pre Create new object (id):" + gameObject.id + " (type):" + gameObject.objectType);
                    if (gameObject.id == -1)
                    {
                        gameObject.id = gameObjectsCounting;
                        gameObjectsCounting++;
                    }
                    if (gameObjects.ContainsKey(gameObject.id))
                    {
                        //Console.WriteLine("    Error Same key (id):" + gameObject.id + " (type):" + gameObject.objectType);
                    }
                    else 
                    {
                        //Console.WriteLine("    Create new object (id):" + gameObject.id + " (type):" + gameObject.objectType);
                        gameObjects.Add(gameObject.id, gameObject);
                        gameObjects[gameObject.id].ConnectionUpdate();
                    }
                    //Console.WriteLine();

                }
                while(destroyGameObjectsQueue.Count > 0)
                {
                    gameObjects.Remove(destroyGameObjectsQueue.Dequeue());
                }
            }
        }


        //public static void Render(int playerId, ref Canvas2DContext context)
        //{
        //    if (gameStarted)
        //    {
        //        context.SetFillStyleAsync("lightgray");
        //        context.FillRectAsync(0, 0, 1280, 720);
        //        //Console.WriteLine("obj count:" + gameObjects.Count);
        //        foreach (GameObject gameObject in gameObjects.Values)
        //        {
        //            gameObject.Render(playerId, ref context);
        //        }
        //    }
        //}
    }
}
