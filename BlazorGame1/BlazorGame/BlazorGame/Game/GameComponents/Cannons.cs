using BlazorGame.Game.Builder;
using BlazorGame.Game.GameComponents.RendersDecorum;
using BlazorGame.Game.GameComponents.RendersDecorum.Decorator;
using BlazorGame.Game.GameComponents.Units.Visitor;
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
            cannons.Add(new Cannon(new int[] { 0, 40 }, new int[] { 100, 40 }, "black",10));
            cannons.Add(new Cannon(new int[] { 0, -40 }, new int[] { 100, -40 }, "black", 10));
            cannons.Add(new Cannon(new int[] { 0, 0 }, new int[] { 100, 0 }, "black", 10));
        }
        public void SetCannons(int x, int y)
        {
            x = x - MainFrame.OffsetX;
            y = y - MainFrame.OffsetY;
            float len = MathF.Sqrt(MathF.Abs(x) * MathF.Abs(x) + MathF.Abs(y) * MathF.Abs(y));
            dimensions[0] = x / len;
            dimensions[1] = y / len;
            foreach (Cannon cannon in cannons) cannon.SetCannon(dimensions);
        }
        public void SetShooting(bool shooting) { this.shooting = shooting; }

        public override void Update()
        {
            //foreach(Cannon cannon in cannons) { cannon.Update(); }
            if (GameObject != null)
            {
                timer += MainFrame.DeltaTime;
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
        }
        public override void CollisonTrigger(int gameObject) { }
        public override void ConnectionUpdate()
        {
            //Console.WriteLine("cannon GameObject:" + gameObject.id);
            if (GameObject.ContainsComponent<Renders>())
            {
                foreach (Render render in GameObject.GetComponent<Renders>().renders)
                { if (render is GunsDecorator) { (render as GunsDecorator).renders = new List<Render>(); } }

                for (int i = 0; i < cannons.Count; i++)
                {
                    cannons[i].gameObject = GameObject;
                    cannons[i].ConnectionUpdate(GameObject);
                }
            }
            
            
        }

        public override void accept(IVisitor visitor)
        {
            
        }
    }
}
