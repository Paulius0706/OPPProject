using BlazorGame.Game.GameComponents.Units.Mobs;

namespace BlazorGame.Game.GameComponents.States
{
    public abstract class MobState
    {
        public bool invalid = false;
        public abstract void Act(Mob mob);
    }
}
