using BlazorGame.Game.GameComponents.Units.Mobs;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents.States
{
    public class AgressiveMobState : MobState
    {
        private readonly int gameObjectId;
        public AgressiveMobState(int gameObjectId)
        {
            this.gameObjectId = gameObjectId;
        }

        public override void Act(Mob mob)
        {
            mob.bodyRender.color = "maroon";
            if (!MainFrame.GameObjects.ContainsKey(gameObjectId)) { invalid = true; return; }
            float[] dist = new float[]
            {
                MainFrame.GameObjects[gameObjectId].Position[0] - mob.GameObject.Position[0],
                MainFrame.GameObjects[gameObjectId].Position[1] - mob.GameObject.Position[1]
            };
            float len = MathF.Sqrt(dist[0] * dist[0] + dist[1] * dist[1]);
            if (len > 0)
            {
                mob.tragetDirection = new float[2] { dist[0] / len, dist[1] / len };
            }
        }
    }
}
