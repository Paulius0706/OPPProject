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
        public Purpose purpose = Purpose.none;
        public string str;
        
        public enum Type{none ,box, circle, image, line}
        public enum Purpose
        {
            none,
            body,
            cannon,
            cannonLines,
            healthBarFrame,
            healthBar,
            expBar,
            expBarFrame
        }
        public Render(int[] offset, int[] size, Type type, string str)
        {
            this.size = size;
            this.offset = offset;
            this.offset1 = new int[2];
            this.radius = 0;
            this.width = 0;
            this.type = type;
            this.str = str;
        }
        public Render(int[] offset, int radius, string str)
        {
            this.size = new int[2];
            this.offset = offset;
            this.offset1 = new int[2];
            this.radius = radius;
            this.width = 0;
            this.type = Type.circle;
            this.str = str;
        }
        public Render(int[] offset, int[] offset1, int width, string str)
        {
            this.size = new int[2];
            this.offset = offset;
            this.offset1 = offset1;
            this.radius = 0;
            this.width = width;
            this.type = Type.line;
            this.str = str;
        }
        public Render()
        {
            this.size = new int[2];
            this.offset = new int[2];
            this.offset1 = new int[2];
            this.radius = 0;
            this.width = 0;
            this.type = Type.line;
            this.str = "";
        }
    }
}
