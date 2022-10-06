using BlazorGame.Game.Builder;
using BlazorGame.Game.GameComponents.Colliders;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents
{
    public class Cannon
    {
        public static readonly float fromPointerShootDistance = 10f;

        public float[] offset1; // fowards backwards 
        public float[] offset2; // left right
        public float[] dimensions;
        public float barrelLenght;
        public bool shooting;

        private int gameObjectId = -1;
        private int playerId = -1;
        private int cannonsId = -1;
        public int renderCannonId = -1;
        public int renderConnonLineId = -1;

        public GameObject gameObject
        {
            get { if (!MainFrame.gameObjects.ContainsKey(gameObjectId)) return null; return MainFrame.gameObjects[gameObjectId]; }
            set { gameObjectId = value.id; if (MainFrame.gameObjects.ContainsKey(gameObjectId)) MainFrame.gameObjects[gameObjectId] = value; }
        }
        public Player player
        {
            get 
            {
                if (!MainFrame.gameObjects.ContainsKey(gameObjectId)
                    //|| MainFrame.gameObjects[gameObjectId].components.ContainsKey(typeof(Player))
                    || playerId == -1
                    || MainFrame.gameObjects[gameObjectId].components.Count >= playerId
                    || MainFrame.gameObjects[gameObjectId].components[playerId] is not Player) return null;
                return (MainFrame.gameObjects[gameObjectId].components[(int)PlayerObject.PlayerComponents.player] as Player); 
            }
            set 
            { 
                playerId = value.id; 
                if (MainFrame.gameObjects.ContainsKey(gameObjectId)
                    && playerId != -1
                    && MainFrame.gameObjects[gameObjectId].components.Count < playerId
                    && MainFrame.gameObjects[gameObjectId].components[playerId] is Player)
                    MainFrame.gameObjects[gameObjectId].components[playerId] = value; }
        }
        public Cannons cannons
        {
            get 
            {
                if (!MainFrame.gameObjects.ContainsKey(gameObjectId)
                    || cannonsId == -1
                    || MainFrame.gameObjects[gameObjectId].components.Count >= cannonsId
                    || MainFrame.gameObjects[gameObjectId].components[cannonsId] is not Cannons) return null; 
                return (MainFrame.gameObjects[gameObjectId].components[(int)PlayerObject.PlayerComponents.cannon] as Cannons); }
            set 
            {
                cannonsId = value.id; 
                if (MainFrame.gameObjects.ContainsKey(gameObjectId)
                    && cannonsId != -1
                    && MainFrame.gameObjects[gameObjectId].components.Count < cannonsId
                    && MainFrame.gameObjects[gameObjectId].components[cannonsId] is Cannons)
                    MainFrame.gameObjects[gameObjectId].components[cannonsId] = value;
            }
        }
        public Render renderCannon
        {
            get { if (!MainFrame.gameObjects.ContainsKey(gameObjectId)) return null; return MainFrame.gameObjects[gameObjectId].renders[renderCannonId]; }
            set { if (MainFrame.gameObjects.ContainsKey(gameObjectId)) MainFrame.gameObjects[gameObjectId].renders[renderCannonId] = value; }
        }
        public Render renderCannonLine
        {
            get { if (!MainFrame.gameObjects.ContainsKey(gameObjectId)) return null; return MainFrame.gameObjects[gameObjectId].renders[renderConnonLineId]; }
            set { if (MainFrame.gameObjects.ContainsKey(gameObjectId)) MainFrame.gameObjects[gameObjectId].renders[renderConnonLineId] = value; }
        }


        public Cannon(float[] offset1, float[] offset2)
        {
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
            if (renderCannon != null)
            {
                renderCannon.offset = renderOffset;
                renderCannon.offset1 = renderOffset1;
            }
            if(renderCannonLine != null)
            {
                renderCannonLine.offset = renderOffset;
                renderCannonLine.offset1 = renderOffset1;
            }
        }
        public void Shooting()
        {
            float[] velocity = new float[2];
            velocity[0] = renderCannon.offset1[0];
            velocity[1] = renderCannon.offset1[1];
            velocity[0] -= renderCannon.offset[0];
            velocity[1] -= renderCannon.offset[1];
            velocity[0] = velocity[0] / barrelLenght * 300f;
            velocity[1] = velocity[1] / barrelLenght * 300f;

            BulletBuilder bulletBuilder = new BulletBuilder(new float[2] { renderCannon.offset1[0] + gameObject.position[0], renderCannon.offset1[1] + gameObject.position[1] } , velocity);
            Director.director.Construct(ref bulletBuilder, gameObject.id, player.damage, player.damage);
            MainFrame.Instantiate(bulletBuilder.GetResult());
        }
    }
}
