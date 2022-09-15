using System.Numerics;
namespace Tankio.GameLogic
{
    public static class MainFarme
    {
        public static Dictionary<int,GameObject> gameObjects;
        public static int playerID;
        public static void StartGame()
        {
            gameObjects = new Dictionary<int, GameObject>();
            // instatijuoti player
            Player playerComponent = new Player("Admin", 1);
            Collider collider = new Collider(100f);
            GameObject gameObject = new GameObject(new Vector2(1280f/2f,720f/2f));
            gameObject.AddComponent(playerComponent);
            gameObject.AddComponent(collider);
            playerID = gameObject.id;
            gameObjects.Add(gameObject.id, gameObject);
        }
        public static void PlayerInputs(string key)
        {
            if(key == "KeyW") {
                gameObjects[playerID].components[0].Inputting(0, 1);
            }
            if (key == "KeyA") {
                gameObjects[playerID].components[0].Inputting(-1, 0);
            }
            if (key == "KeyS") {
                gameObjects[playerID].components[0].Inputting(0, -1);
            }
            if (key == "KeyD") {
                gameObjects[playerID].components[0].Inputting(1, 0);
            }
        }
        public static void Update()
        {
            foreach(GameObject gameObject in gameObjects.Values)
            {
                gameObject.Update();
            }
        }
        public static void Render()
        {
            foreach (GameObject gameObject in gameObjects.Values)
            {
                gameObject.Update();
            }
        }
    }
}
