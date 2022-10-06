using BlazorGame.Game.Builder;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents
{
    public class Cannons : ObjectComponent
    {

        public float timer = 0;
        public float shootInterval = 0;
        public bool shooting;
        public float[] dimensions;

        public List<Cannon> cannons;

        public Cannons()
        {
            shootInterval = 1f;
            timer = 1f;
            dimensions = new float[2] { 1, 0 };
            cannons = new List<Cannon>();
            cannons.Add(new Cannon(new float[] { 0, 40 }, new float[] { 100, 40 }));
            cannons.Add(new Cannon(new float[] { 0, -40 }, new float[] { 100, -40 }));
            cannons.Add(new Cannon(new float[] { 0, 0 }, new float[] { 100, 0 }));
        }
        public void SetCannons(int x, int y)
        {
            x = x - MainFrame.offsetX;
            y = y - MainFrame.offsetY;
            float len = MathF.Sqrt(MathF.Abs(x) * MathF.Abs(x) + MathF.Abs(y) * MathF.Abs(y));
            dimensions[0] = x / len;
            dimensions[1] = y / len;
            foreach (Cannon cannon in cannons) cannon.SetCannon(dimensions);
        }
        public void SetShooting(bool shooting) { this.shooting = shooting; }

        public override void Update()
        {
            //foreach(Cannon cannon in cannons) { cannon.Update(); }
            if (gameObject != null)
            {
                timer += MainFrame.detaTime;
                if (timer > shootInterval && shooting) { SpawnBullets(); }
            }
        }
        public void SpawnBullets()
        {
            timer = 0;
            foreach(Cannon cannon in cannons)
            {
                cannon.Shooting();
            }
            //for (int i = 0; i < lenghts.Length; i++)
            //{
            //    if (shootingCannons[i])
            //    {
            //        float x = dimensions[0] * lenghts[i] + MainFrame.gameObjects[gameObject].position[0];
            //        float y = dimensions[1] * lenghts[i] + MainFrame.gameObjects[gameObject].position[1];
            //        float vx = dimensions[0] * lenghts[i];
            //        float vy = dimensions[1] * lenghts[i];
            //        BulletBuilder bulletBuilder = new BulletBuilder(new float[] { x, y }, new float[] { vx, vy });
            //        Director.director.Construct(ref bulletBuilder, gameObject, damage, damage);
            //        GameObject bullet = bulletBuilder.GetResult();
            //        MainFrame.createGameObjectsQueue.Enqueue(bullet);
            //    }
            //}
        }
        public override void CollisonTrigger(int gameObject, string data, int number) { }
        public override void ConnectionUpdate()
        {
            Console.WriteLine("cannon GameObject:" + gameObject.id);
            for (int i = 0; i < cannons.Count; i++)
            {
                cannons[i].gameObject = gameObject;
                cannons[i].renderCannonId = i;
                cannons[i].renderConnonLineId = i + 3;
            }
        }

    }
}
