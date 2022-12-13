using BlazorGame.Game.GameComponents.Units.Mobs;

namespace BlazorGame.Game.GameComponents.States
{
    public class CalmMobState : MobState
    {
        public override void Act(Mob mob)
        {
            mob.bodyRender.color = "lime";
            Random random = new Random();
            float[] newDir = mob.tragetDirection;
            newDir[0] += (random.NextSingle() * 2f - 1f) * 3f * MainFrame.DeltaTime;
            newDir[1] += (random.NextSingle() * 2f - 1f) * 3f * MainFrame.DeltaTime;
            float len = MathF.Sqrt(newDir[0] * newDir[0] + newDir[1] * newDir[1]);
            if(len != 0)
            {
                newDir[0] /= len * 5f;
                newDir[1] /= len * 5f;
                mob.tragetDirection = newDir;
            }
            
        }
    }
}
