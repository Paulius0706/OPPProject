﻿namespace BlazorGame.Game.GameComponents.RendersDecorum.Decorator
{
    public class HealthDecorator : DecoratorRender
    {
        public enum Type
        {
            backGround = 0,
            fill = 1,
        }
        public float fillWidth { get; private set; }
        public HealthDecorator(BoxRender fill, BoxRender backGround)
        {
            renders = new List<Render>();
            renders.Add(backGround);
            renders.Add(fill);
            fillWidth = fill.size[0];
        }

        public void Update(float health, float maxHealth)
        {
            if (health <= 0) { (renders[(int)Type.fill] as BoxRender).size[0] = 1; } 
            else if(health > maxHealth) { (renders[(int)Type.fill] as BoxRender).size[0] = (int)fillWidth; }
            else { (renders[(int)Type.fill] as BoxRender).size[0] = (int)(fillWidth * (health / maxHealth)); }
        }
    }
}
