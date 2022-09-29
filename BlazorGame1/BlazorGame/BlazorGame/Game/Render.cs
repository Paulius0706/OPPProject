using System.Drawing;

namespace BlazorGame.Game
{
    public class Render
    {
        public int[] size;
        public int[] offset;
        public int radius;
        public int[] offset1;
        public int width;
        public Type type;
        public string str;
        public enum Type{none ,box, circle, image, line}
        public Render(int[] offset, int[] size, Type type, string str)
        {
            this.size = size;
            this.offset = offset;
            this.type = type;
            this.str = str;
        }
        public Render(int[] offset, int radius, string str)
        {
            this.offset = offset;
            this.radius = radius;
            this.type = Type.circle;
            this.str = str;
        }
        public Render(int[] offset, int[] offset1, int width, string str)
        {
            this.offset = offset;
            this.offset1 = offset1;
            this.width = width;
            this.type = Type.line;
            this.str = str;
        }
        public Render()
        {
            this.type = Type.none;
        }
    }
}
