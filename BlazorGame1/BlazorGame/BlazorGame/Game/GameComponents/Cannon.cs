using BlazorGame.Game.Builder;
using BlazorGame.Game.GameComponents.Colliders;
using BlazorGame.Game.GameComponents.RendersDecorum;
using BlazorGame.Game.GameComponents.RendersDecorum.Decorator;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents
{
    public class Cannon
    {
        public static readonly float fromPointerShootDistance = 10f;

        public int[] offset1; // fowards backwards 
        public int[] offset2; // left right
        public int width;
        public string color;
        public float[] dimensions;
        public float barrelLenght;
        public bool shooting;

        private int gameObjectId = -1;

        private int gunsRenderId = -1;
        private int gunRenderId = -1;

        public GameObject gameObject
        {
            get { if (!MainFrame.gameObjects.ContainsKey(gameObjectId)) return null; return MainFrame.gameObjects[gameObjectId]; }
            set { gameObjectId = value.id; if (MainFrame.gameObjects.ContainsKey(gameObjectId)) MainFrame.gameObjects[gameObjectId] = value; }
        }
        public Player player
        {
            get 
            {
                if (gameObject == null) return null;
                if (!gameObject.ContainsComponent<Player>()) return null;
                return gameObject.GetComponent<Player>(); 
            }
            set 
            { 
                if (gameObject != null && gameObject.ContainsComponent<Player>()) player = value; 
            }
        }
        public Renders renders
        {
            get
            {
                if (gameObject == null) return null;
                if (!gameObject.ContainsComponent<Renders>()) return null;
                return gameObject.GetComponent<Renders>();
            }
            set
            {
                if (gameObject != null && gameObject.ContainsComponent<Renders>()) renders = value;
            }
        }
        public GunsDecorator gunsDecorator
        {
            get
            {
                if (renders == null || gunsRenderId == -1) return null;
                if (renders.renders.Count <= gunsRenderId) return null;
                if (renders.renders[gunsRenderId] is not GunsDecorator) return null;
                return renders.renders[gunsRenderId] as GunsDecorator;
            }
            set
            {
                if (gunsRenderId != -1 && renders != null && renders.renders.Count > gunsRenderId && renders.renders[gunsRenderId] is GunsDecorator)
                    renders.renders[gunsRenderId] = value;
            }
        }
        public GunDecorator gunDecorator
        {
            get
            {
                if (gunsDecorator == null || gunsRenderId == -1) return null;
                if (gunsDecorator.renders.Count <= gunRenderId) return null;
                if (gunsDecorator.renders[gunRenderId] is not GunDecorator) return null;
                return gunsDecorator.renders[gunRenderId] as GunDecorator;
            }
            set
            {
                if (gunRenderId != -1 && gunsDecorator != null && gunsDecorator.renders.Count > gunsRenderId && gunsDecorator.renders[gunsRenderId] is GunDecorator)
                    gunsDecorator.renders[gunsRenderId] = value;
            }
        }
        



        public Cannon(int[] offset1, int[] offset2, string color, int width)
        {
            this.color = color;
            this.width = width;
            this.offset1 = offset1;
            this.offset2 = offset2;
            this.barrelLenght = MathF.Sqrt(
                (offset2[0] - offset1[0]) * (offset2[0] - offset1[0]) +
                (offset2[1] - offset1[1]) * (offset2[1] - offset1[1]));
            this.dimensions = new float[2] { 1, 0 };
        }
        public void SetCannon(float[] dimensions)
        {
            //0 fowards backwards
            //1 left right 
            this.dimensions = dimensions;

            float angle = MathF.Atan2(dimensions[1], dimensions[0]);
            (float sin, float cos) = MathF.SinCos(angle);



            int[] renderOffset = new int[2];
            renderOffset[0] = (int)(sin * offset1[1] + cos * offset1[0]);
            renderOffset[1] = (int)(-cos * offset1[1] + sin * offset1[0]);
            int[] renderOffset1 = new int[2];
            renderOffset1[0] = (int)(sin * offset2[1] + cos * offset2[0]);
            renderOffset1[1] = (int)(-cos * offset2[1] + sin * offset2[0]);
            if (gunDecorator != null)
            {
                (gunDecorator.renders[(int)GunDecorator.Type.gun] as LineRender).offset  = renderOffset;
                (gunDecorator.renders[(int)GunDecorator.Type.gun] as LineRender).offset1 = renderOffset1;
                (gunDecorator.renders[(int)GunDecorator.Type.detail] as LineRender).offset = renderOffset;
                (gunDecorator.renders[(int)GunDecorator.Type.detail] as LineRender).offset1 = renderOffset1;
            }
        }
        public void Shooting()
        {
            if(gunDecorator != null)
            {
                float[] velocity = new float[2];
                velocity[0] = (gunDecorator.renders[(int)GunDecorator.Type.gun] as LineRender).offset1[0];
                velocity[1] = (gunDecorator.renders[(int)GunDecorator.Type.gun] as LineRender).offset1[1];
                velocity[0] -= (gunDecorator.renders[(int)GunDecorator.Type.gun] as LineRender).offset[0];
                velocity[1] -= (gunDecorator.renders[(int)GunDecorator.Type.gun] as LineRender).offset[1];
                velocity[0] = velocity[0] / barrelLenght * 300f;
                velocity[1] = velocity[1] / barrelLenght * 300f;

                if(player.spawner != null)
                {
                    BulletObject bulletObject = player.spawner.bulletObject.Clone();
                    bulletObject.position = new float[2]{
                        (gunDecorator.renders[(int)GunDecorator.Type.gun] as LineRender).offset1[0] + gameObject.position[0],
                        (gunDecorator.renders[(int)GunDecorator.Type.gun] as LineRender).offset1[1] + gameObject.position[1]
                    };
                    bulletObject.velocity = velocity;
                    MainFrame.Instantiate(bulletObject);
                }
            }
        }
        public void ConnectionUpdate(GameObject gameObject)
        {
            this.gameObject = gameObject;
            for(int i=0;i< renders.renders.Count; i++)
            {
                if(renders.renders[i] is GunsDecorator)
                {
                    gunsRenderId = i;

                    LineRender main = new LineRender(offset1, offset2, width, color);
                    LineRender details = new LineRender(offset1, offset2, width + 4, "black");
                    GunDecorator gunDecorator = new GunDecorator(main, details);

                    gunsDecorator.renders.Add(gunDecorator);
                    gunRenderId = gunsDecorator.renders.Count - 1;
                    gunsDecorator.ConnectionUpdate(gameObject);
                    break;
                }
            }
        }
    }
}
