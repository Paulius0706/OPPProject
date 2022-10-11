using BlazorGame.Game.Builder;
using BlazorGame.Game.GameComponents.Colliders;
using BlazorGame.Game.GameComponents.RendersDecorum;
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
        private int playerId = -1;
        private int cannonsId = -1;

        private int gunsRenderId = -1;
        private int gunRenderId = -1;
        private int gunLineRenderId = -1;

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
                    || !MainFrame.gameObjects[gameObjectId].components.ContainsKey(typeof(Player))) return null;
                return (MainFrame.gameObjects[gameObjectId].components[typeof(Player)] as Player); 
            }
            set 
            { 
                playerId = value.id; 
                if (MainFrame.gameObjects.ContainsKey(gameObjectId)
                    && MainFrame.gameObjects[gameObjectId].components.ContainsKey(typeof(Player)))
                    MainFrame.gameObjects[gameObjectId].components[typeof(Player)] = value; }
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
            if (gunsRenderId != -1 && gunRenderId != -1)
            {
                (((gameObject.components[typeof(Renders)] as Renders).renders[gunsRenderId] as DecoratorRender).renders[gunRenderId] as LineRender).offset  = renderOffset;
                (((gameObject.components[typeof(Renders)] as Renders).renders[gunsRenderId] as DecoratorRender).renders[gunRenderId] as LineRender).offset1 = renderOffset1;
            }
            if(gunsRenderId != -1 && gunLineRenderId != -1)
            {
                (((gameObject.components[typeof(Renders)] as Renders).renders[gunsRenderId] as DecoratorRender).renders[gunLineRenderId] as LineRender).offset = renderOffset;
                (((gameObject.components[typeof(Renders)] as Renders).renders[gunsRenderId] as DecoratorRender).renders[gunLineRenderId] as LineRender).offset1 = renderOffset1;
            }
        }
        public void Shooting()
        {
            if(gunsRenderId != -1 && gunRenderId != -1 && gunLineRenderId != -1)
            {
                float[] velocity = new float[2];
                velocity[0] = (((gameObject.components[typeof(Renders)] as Renders).renders[gunsRenderId] as DecoratorRender).renders[gunRenderId] as LineRender).offset1[0];
                velocity[1] = (((gameObject.components[typeof(Renders)] as Renders).renders[gunsRenderId] as DecoratorRender).renders[gunRenderId] as LineRender).offset1[1];
                velocity[0] -= (((gameObject.components[typeof(Renders)] as Renders).renders[gunsRenderId] as DecoratorRender).renders[gunRenderId] as LineRender).offset[0];
                velocity[1] -= (((gameObject.components[typeof(Renders)] as Renders).renders[gunsRenderId] as DecoratorRender).renders[gunRenderId] as LineRender).offset[1];
                velocity[0] = velocity[0] / barrelLenght * 300f;
                velocity[1] = velocity[1] / barrelLenght * 300f;

                BulletBuilder bulletBuilder = new BulletBuilder(
                    new float[2]{
                    (((gameObject.components[typeof(Renders)] as Renders).renders[gunsRenderId] as DecoratorRender).renders[gunRenderId] as LineRender).offset1[0] + gameObject.position[0],
                    (((gameObject.components[typeof(Renders)] as Renders).renders[gunsRenderId] as DecoratorRender).renders[gunRenderId] as LineRender).offset1[1] + gameObject.position[1]
                    }, velocity
                    );
                Director.director.Construct(ref bulletBuilder, gameObject.id, player.damage, player.damage);
                MainFrame.Instantiate(bulletBuilder.GetResult());
            }
        }
        public void ConnectionUpdate()
        {
            for(int i=0;i< (gameObject.components[typeof(Renders)] as Renders).renders.Count; i++)
            {
                if((gameObject.components[typeof(Renders)] as Renders).renders[i].type == RendersDecorum.Render.Type.Guns)
                {
                    gunsRenderId = i;

                    ((gameObject.components[typeof(Renders)] as Renders).renders[i] as DecoratorRender).renders.Add(
                        new LineRender(RendersDecorum.Render.Type.Gun, offset1, offset2, width + 3, color));
                    gunLineRenderId = ((gameObject.components[typeof(Renders)] as Renders).renders[i] as DecoratorRender).renders.Count - 1;
                    
                    ((gameObject.components[typeof(Renders)] as Renders).renders[i] as DecoratorRender).renders.Add(
                        new LineRender(RendersDecorum.Render.Type.Gun, offset1, offset2, width, color));
                    gunRenderId = ((gameObject.components[typeof(Renders)] as Renders).renders[i] as DecoratorRender).renders.Count - 1;

                    ((gameObject.components[typeof(Renders)] as Renders).renders[i] as DecoratorRender).ConnectionUpdate(gameObject);

                    break;
                }
            }
        }
    }
}
