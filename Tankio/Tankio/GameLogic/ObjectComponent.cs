using System.Net.Mail;
using System.Numerics;

namespace Tankio.GameLogic
{
    public abstract class ObjectComponent
    {
        public static int idCounter = 0;
        public int id;
        public int gameObject { get; set; } // gameobject ObjectComponent attached to
        
        public ObjectComponent() { }

        public abstract void Update();

        public abstract void Render();

        public abstract void Inputting(int x, int y);

    }
}
