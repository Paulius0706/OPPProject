using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.Iterator
{
    public class GameObjectRepository : Container
    {
        

        public Iterator getIterator()
        {
            return new GameObjectIterator();
        }

        private class GameObjectIterator : Iterator
        {
            public List<int> gameObjectKeys = new List<int>();
            int index = 0;
            public bool Exist()
            {
                return index < gameObjectKeys.Count;
            }

            public void First()
            {
                gameObjectKeys = MainFrame.GameObjects.Keys.ToList();

                index = 0;


            }

            public object Get()
            {
                return MainFrame.GameObjects[gameObjectKeys[index]];
            }

            public void Next()
            {
                index++;
            }
        }
    }
}
