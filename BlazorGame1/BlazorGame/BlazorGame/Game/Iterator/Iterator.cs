using Microsoft.AspNetCore.SignalR;
using System;

namespace BlazorGame.Game.Iterator
{
    public interface Iterator
    {
        public Boolean Exist();
        public void Next();
        public Object Get();
        public void First();
    }
}
