namespace BlazorGame.Game.GameComponents.Units.Visitor
{
    public interface Visitor
    {
        public void visitBullet(Bullet bullet);
        public void visitCollectible(Collectible collectible);
        public void visitPlayer(Player player);

    }
}
