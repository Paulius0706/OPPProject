using BlazorGame.Game.GameComponents;
using System.Runtime.CompilerServices;

namespace BlazorGame.Game.DataTypes
{
    public class ComponentsDictionary
    {
        private readonly Dictionary<Type, object> dicionary = new Dictionary<Type, object>();
        public T Get<T>() =>  (T)dicionary[typeof(T)];
        public void Add<T>(T component) => dicionary[typeof(T)] = component;
        public bool ContainsKey<T>() => dicionary.ContainsKey(typeof(T));
        public void Remove<T>() => dicionary.Remove(typeof(T));

    }
}
