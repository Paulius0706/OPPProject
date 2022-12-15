using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameComponents.Units.Visitor;
using BlazorGame.Game.GameObjects;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace BlazorGame.Game.GameComponents
{
    public abstract class ObjectComponent
    {
        public ObjectComponent nextObject;
        public static int IdCounter = 0;

        public GameObject GameObject 
        { 
            get 
            {
                if (!MainFrame.GameObjects.ContainsKey(gameObjectId)) return null;
                return MainFrame.GameObjects[gameObjectId]; 
            }
            set 
            {
                gameObjectId = value.Id;
                if (MainFrame.GameObjects.ContainsKey(gameObjectId)) 
                    MainFrame.GameObjects[gameObjectId] = value; 
            }
        }
        private int gameObjectId = -1;

        public ObjectComponent() { }
        public abstract void Update();
        public abstract void CollisonTrigger(int gameObjectId);

        public abstract void ConnectionUpdate();

        public abstract void accept(IVisitor visitor);

        public T AbstractGetComponent<T>() where T : ObjectComponent
        {
            if (this.GetType().IsSubclassOf(typeof(T))) { return (T)this; }
            else if (nextObject != null) { return (T)nextObject.AbstractGetComponent<T>(); }
            return null;
        }
    }
}
