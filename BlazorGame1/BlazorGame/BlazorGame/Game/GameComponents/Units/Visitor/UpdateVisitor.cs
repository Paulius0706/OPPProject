using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents.Units.Visitor
{
    public class UpdateVisitor : IVisitor
    {
        public void Visit(GameObject obj)
        {
            try
            {
                obj.Update();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
