namespace BlazorGame.Game.GameComponents.Units.Visitor
{
    public class OnDestroyVisitor : Visitor
    {
        public void visitBullet(Bullet bullet)
        {
            throw new NotImplementedException();
        }

        public void visitCollectible(Collectible collectible)
        {
            throw new NotImplementedException();
        }

        public void visitPlayer(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
