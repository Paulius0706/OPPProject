namespace BlazorGame.Game.GameComponents.RendersDecorum.Decorator
{
    public class ExperienceDecorator : DecoratorRender
    {
        public enum Type
        {
            backGround = 0,
            fill = 1,
        }
        public int fillWidth { get; private set; }
        public ExperienceDecorator(BoxRender fill, BoxRender backGround)
        {
            renders = new List<Render>();
            renders.Add(backGround);
            renders.Add(fill);
            fillWidth = fill.size[0];
        }

        public void Update(float exp, float maxExp)
        {
            if (exp <= 0) { (renders[(int)Type.fill] as BoxRender).size[0] = 1; }
            else if (exp > maxExp) { (renders[(int)Type.fill] as BoxRender).size[0] = (int)fillWidth; }
            else { (renders[(int)Type.fill] as BoxRender).size[0] = (int)(fillWidth * (exp / maxExp)); }
        }
    }
}
