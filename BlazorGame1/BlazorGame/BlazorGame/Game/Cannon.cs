namespace BlazorGame.Game
{
    public class Cannon
    {
        // need to fix offsets and stuff

        public static readonly float fromPointerShootDistance = 10f;
        public int gameObject = -1;
        
        public float[] lenghts;
        public bool[] renderUse;
        public float[] dimensions;
        public float[] startOffset;
        public float[] finOffset;
        public bool[] shootingCannons;

        public float timer = 0;
        public float shootInterval = 0;
        public bool shooting;
        public int damage;

        public Cannon()
        {
            lenghts = new float[] { 100,0,0,0,0,0};
            renderUse = new bool[] { true,false, false, false, false, false, };
            dimensions = new float[] { 0.5f,0.5f};
            startOffset = new float[] { 0, 0, 0, 0, 0, 0 };
            finOffset = new float[] { 0, 0, 0, 0, 0, 0 };
            shootingCannons = new bool[] { true, false, false, false, false, false };
            shootInterval = 1f;
            timer = 1f;
        }
        public void SetCannon(int x, int y)
        {
            x = x - MainFrame.offsetX;
            y = y - MainFrame.offsetY;
            float len = MathF.Sqrt(MathF.Abs(x) * MathF.Abs(x) + MathF.Abs(y) * MathF.Abs(y));
            dimensions[0] = (float)x / len;
            dimensions[1] = (float)y / len;
        }
        public void SetShooting(bool shooting) { this.shooting = shooting; }

        public void Update()
        {
            for(int i = 0; i < lenghts.Length; i++)
            {
                if (renderUse[i] && gameObject != -1) {
                    MainFrame.gameObjects[gameObject].renders[i].offset1[0] = (int)((float)dimensions[0] * (float)lenghts[i]);
                    MainFrame.gameObjects[gameObject].renders[i].offset1[1] = (int)((float)dimensions[1] * (float)lenghts[i]);
                }
                
            }
            if(gameObject != -1)
            {
                timer += MainFrame.detaTime;
                if (timer > shootInterval && shooting) { SpawnBullets(); }
            }
        }
        public void SpawnBullets()
        {
            timer = 0;
            for (int i = 0; i < lenghts.Length; i++)
            {
                if (shootingCannons[i])
                {
                    float x = dimensions[0] * lenghts[i] + MainFrame.gameObjects[gameObject].position[0];
                    float y = dimensions[1] * lenghts[i] + MainFrame.gameObjects[gameObject].position[1];
                    float vx = dimensions[0] * lenghts[i];
                    float vy = dimensions[1] * lenghts[i];
                    GameObject bullet = GameObject.CreateBullet(new float[] { x, y }, gameObject, new float[] { vx, vy },damage);
                    MainFrame.createGameObjectsQueue.Enqueue(bullet);
                }
            }
        }

    }
}
