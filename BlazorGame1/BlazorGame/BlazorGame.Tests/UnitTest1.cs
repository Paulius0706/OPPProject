using BlazorGame.Game;
using BlazorGame.Game.Builder;
using BlazorGame.Game.GameComponents;
using BlazorGame.Game.GameObjects;
using BlazorGame.Game.GameObjects.Factories;

namespace BlazorGame.Tests
{
    public class UnitTest1
    {
        
        [Fact]
        public void GetComponent()
        {
            // set enviroment
            MainFrame.gameObjects = new Dictionary<int, GameObject>();
            MainFrame.detaTime = 0.07f;
            float timeCount = 3f / MainFrame.detaTime;

            // create player
            int newHealth = 1000000;
            int playerId = MainFrame.CreateNewPlayer("ss");
            MainFrame.Update();

            // Test getComponent
            MainFrame.gameObjects[playerId].GetComponent<Player>().health = 100000;

            Assert.False(MainFrame.gameObjects[playerId].GetComponent<Player>().health == newHealth);
        }

        [Fact]
        public void AddComponent_addPlayerToBullet()
        {
            // set enviroment
            MainFrame.gameObjects = new Dictionary<int, GameObject>();
            MainFrame.detaTime = 0.07f;
            float timeCount = 3f / MainFrame.detaTime;


            // create bullet
            BulletBuilder bulletBuilder = new BulletBuilder(new float[] { 0, 0 }, new float[] { 0, 0 });
            Director.director.Construct(ref bulletBuilder, 0, 0, 51);
            BulletObject bulletObject = (BulletObject)bulletBuilder.GetResult();
            
            // Do AddComponent
            bulletObject.AddComponent(new Player("AKA", 1));
            MainFrame.Update();

            // Test AddComponent
            Assert.True((bulletObject.GetComponent<Player>() is Player) && (bulletObject.GetComponent<Player>().name == "AKA"));
        }

        


        /// <summary>
        /// Test where 2 players collide and canlculate correct final position
        /// this test game determinism and emulate real life
        /// </summary>
        /// <param name="pos1">initial position</param>
        /// <param name="pos2">initial position</param>
        /// <param name="vel1">initial volocity</param>
        /// <param name="vel2">initial volocity</param>
        /// <param name="newPos1">position prediction</param>
        /// <param name="newPos2">position prediction</param>
        [Theory]
        [InlineData(
            new float[] {   0,0}, new float[] { 400,0},
            new float[] { 400,0}, new float[] {   0,0},
            new float[] { 300,0}, new float[] { 500,0})]
        [InlineData(
            new float[] { 400, 0 }, new float[] {   0, 0 },
            new float[] {   0, 0 }, new float[] { 400, 0 },
            new float[] { 500, 0 }, new float[] { 300, 0 })]
        // TM 2
        public void CircleCollision(float[] pos1, float[] pos2,float[] vel1, float[] vel2,float[] newPos1, float[] newPos2)
        {
            // set enviroment
            MainFrame.gameObjects = new Dictionary<int, GameObject>();
            MainFrame.detaTime = 0.07f;
            float timeCount = 3f / MainFrame.detaTime;

            // Spawn players
            int player1Id = MainFrame.CreateNewPlayer("1", false);
            int player2Id = MainFrame.CreateNewPlayer("2", false);
            MainFrame.Update();
            MainFrame.gameObjects[player1Id].velocity = vel1;
            MainFrame.gameObjects[player2Id].velocity = vel2;

            MainFrame.gameObjects[player1Id].position = pos1;
            MainFrame.gameObjects[player2Id].position = pos2;

            for (int i = 0; i < timeCount; i++)
            {
                MainFrame.Update();
            }

            float errorDelta = 20f;
            Assert.True(
                   (MainFrame.gameObjects[player1Id].position[0] < newPos1[0] + errorDelta && MainFrame.gameObjects[player1Id].position[0] > newPos1[0] - errorDelta)
                && (MainFrame.gameObjects[player1Id].position[1] < newPos1[1] + errorDelta && MainFrame.gameObjects[player1Id].position[1] > newPos1[1] - errorDelta)
                
                && (MainFrame.gameObjects[player2Id].position[0] < newPos2[0] + errorDelta && MainFrame.gameObjects[player2Id].position[0] > newPos2[0] - errorDelta)
                && (MainFrame.gameObjects[player2Id].position[1] < newPos2[1] + errorDelta && MainFrame.gameObjects[player2Id].position[1] > newPos2[1] - errorDelta),
                   "x: "+ (newPos1[0] - errorDelta) + "<" + MainFrame.gameObjects[player1Id].position[0] + ">" + (newPos1[0] + errorDelta)+"\n"+
                   "y: "+ (newPos1[1] - errorDelta) + "<" + MainFrame.gameObjects[player1Id].position[1] + ">" + (newPos1[1] + errorDelta) + "\n" + "\n" +
                   "x: "+ (newPos2[0] - errorDelta) + "<" + MainFrame.gameObjects[player2Id].position[0] + ">" + (newPos2[0] + errorDelta) + "\n" +
                   "y: "+ (newPos2[1] - errorDelta) + "<" + MainFrame.gameObjects[player2Id].position[1] + ">" + (newPos2[1] + errorDelta));
        }

        public enum ObjectType
        {
            col1,
            col2,
            col3,
            mob,
            player
        }


        [Theory]
        [InlineData(ObjectType.col1)]
        [InlineData(ObjectType.col2)]
        [InlineData(ObjectType.col3)]
        [InlineData(ObjectType.mob)]
        [InlineData(ObjectType.player)]
        // TM 6 - 1
        public void GetExpFromBullets(ObjectType objectType)
        {
            // set enviroment
            MainFrame.gameObjects = new Dictionary<int, GameObject>();
            MainFrame.detaTime = 0.07f;
            float timeCount = 3f / MainFrame.detaTime;

            // create shooter
            int playerId = MainFrame.CreateNewPlayer("ss", false);
            MainFrame.Update();
            (MainFrame.gameObjects[playerId].GetComponent<Player>()).experiance = 0;

            // create target
            float[] newPos = new float[] { 200, 0 };
            CollectibleBuilder collectibleBuilder = new CollectibleBuilder(newPos);
            switch (objectType)
            {
                case ObjectType.col1: Director.director.Construct(ref collectibleBuilder, Game.GameComponents.Collectible.CollectibleType.col1); break;
                case ObjectType.col2: Director.director.Construct(ref collectibleBuilder, Game.GameComponents.Collectible.CollectibleType.col2); break;
                case ObjectType.col3: Director.director.Construct(ref collectibleBuilder, Game.GameComponents.Collectible.CollectibleType.col3); break;
                case ObjectType.mob: Director.director.Construct(ref collectibleBuilder, Game.GameComponents.Collectible.CollectibleType.mob); break;
            }
            if (objectType == ObjectType.player)
            {
                // if target is player
                int newPlayerId = MainFrame.CreateNewPlayer("ss", false);
                MainFrame.Update();
                MainFrame.gameObjects[newPlayerId].position = newPos;
                MainFrame.gameObjects[newPlayerId].GetComponent<Player>().health = 1;
            }
            else
            {
                // if target is collectibe
                CollectibleObject collectible = (CollectibleObject)collectibleBuilder.GetResult();
                collectible.GetComponent<Collectible>().health = 1;
                MainFrame.Instantiate(collectible);
            }

            //create bullet
            float[] bulletPos = new float[] { 100, 0 };
            float[] bulletVel = new float[] { 500, 0 };
            BulletBuilder bulletBuilder = new BulletBuilder(bulletPos, bulletVel);
            Director.director.Construct(ref bulletBuilder, playerId, 5, 5);
            MainFrame.Instantiate(bulletBuilder.GetResult());

            //simulate
            for (int i = 0; i < timeCount; i++) { MainFrame.Update(); }

            Assert.True(MainFrame.gameObjects[playerId].GetComponent<Player>().experiance > 0 || MainFrame.gameObjects[playerId].GetComponent<Player>().level > 1);
        }
        [Theory]
        [InlineData(ObjectType.col1)]
        [InlineData(ObjectType.col2)]
        [InlineData(ObjectType.col3)]
        [InlineData(ObjectType.mob)]
        [InlineData(ObjectType.player)]
        // TM 6 - 2
        public void GetExpFromBody(ObjectType objectType)
        {
            // set enviroment
            MainFrame.gameObjects = new Dictionary<int, GameObject>();
            MainFrame.detaTime = 0.07f;
            float timeCount = 3f / MainFrame.detaTime;

            // create player that have exp
            int playerId = MainFrame.CreateNewPlayer("ss", false);
            MainFrame.Update();
            (MainFrame.gameObjects[playerId].GetComponent<Player>()).experiance = 0;
            (MainFrame.gameObjects[playerId].GetComponent<Player>()).health = 100000;

            // create target
            float[] newPos = new float[] { 0, 0 };
            CollectibleBuilder collectibleBuilder = new CollectibleBuilder(newPos);
            switch (objectType)
            {
                case ObjectType.col1: Director.director.Construct(ref collectibleBuilder, Game.GameComponents.Collectible.CollectibleType.col1); break;
                case ObjectType.col2: Director.director.Construct(ref collectibleBuilder, Game.GameComponents.Collectible.CollectibleType.col2); break;
                case ObjectType.col3: Director.director.Construct(ref collectibleBuilder, Game.GameComponents.Collectible.CollectibleType.col3); break;
                case ObjectType.mob: Director.director.Construct(ref collectibleBuilder, Game.GameComponents.Collectible.CollectibleType.mob); break;
            }
            if (objectType == ObjectType.player)
            {
                // if target is player
                int newPlayerId = MainFrame.CreateNewPlayer("ss", false);
                MainFrame.Update();
                MainFrame.gameObjects[newPlayerId].position = newPos;
                MainFrame.gameObjects[newPlayerId].GetComponent<Player>().health = 1;
            }
            else
            {
                // if target is collectibe
                CollectibleObject collectible = (CollectibleObject)collectibleBuilder.GetResult();
                collectible.GetComponent<Collectible>().health = 1;
                MainFrame.Instantiate(collectible);
            }

            //simulate
            for (int i = 0; i < timeCount; i++) { MainFrame.Update(); }

            Assert.True(MainFrame.gameObjects[playerId].GetComponent<Player>().experiance > 0 || MainFrame.gameObjects[playerId].GetComponent<Player>().level > 1);
        }

        [Fact]
        // TM 7
        public void LevelUp_levelUpfrom1()
        {
            // set enviroment
            MainFrame.gameObjects = new Dictionary<int, GameObject>();
            MainFrame.detaTime = 0.07f;
            float timeCount = 3f / MainFrame.detaTime;

            // spawn player
            int playerId = MainFrame.CreateNewPlayer("AKA");
            MainFrame.Update();
            MainFrame.gameObjects[playerId].position = new float[] { 0, 0 };
            MainFrame.gameObjects[playerId].GetComponent<Player>().experiance = MainFrame.gameObjects[playerId].GetComponent<Player>().maxExperiance -1;

            // spawn collectible
            CollectibleBuilder collectibleBuilder = new CollectibleBuilder(new float[] { 100, 0 });
            Director.director.Construct(ref collectibleBuilder, Collectible.CollectibleType.col1);
            CollectibleObject collectibleObject = (CollectibleObject)collectibleBuilder.GetResult();
            MainFrame.Instantiate(collectibleObject);
            MainFrame.Update();

            // spawn bullet from player
            BulletBuilder bulletBuilder = new BulletBuilder(new float[] { 100, 0 }, new float[] { 100, 0 });
            Director.director.Construct(ref bulletBuilder, playerId, 1000, 1000);
            MainFrame.Instantiate(bulletBuilder.GetResult());
            MainFrame.Update();

            // Update Collisions
            MainFrame.Update();
            MainFrame.Update();

            Assert.True(MainFrame.gameObjects[playerId].GetComponent<Player>().level > 1, "level:" + MainFrame.gameObjects[playerId].GetComponent<Player>().level + " exp:" + MainFrame.gameObjects[playerId].GetComponent<Player>().experiance);
        }

        // TM 9 Death


        
        [Theory]
        [InlineData(new float[] { -200, 0 }, new float[] { 400, 0 }, true)]
        [InlineData(new float[] { 0, -200 }, new float[] { 0, 400 }, true)]
        [InlineData(new float[] { 0, -400 }, new float[] { 0, 0 }, false)]
        [InlineData(new float[] { 300, 0 }, new float[] { 0, 0 }, false)]
        // TM 11
        public void Despawn(float[] playerPos, float[] collectiblePos, bool despawn)
        {
            // set enviroment
            MainFrame.gameObjects = new Dictionary<int, GameObject>();
            MainFrame.detaTime = 0.07f;
            float timeCount = 3f / MainFrame.detaTime;

            // spawn player
            int playerId = MainFrame.CreateNewPlayer("AKA",false);
            MainFrame.Update();
            MainFrame.gameObjects[playerId].position = playerPos;


            // spawn collectible
            CollectibleBuilder collectibleBuilder = new CollectibleBuilder(collectiblePos);
            Director.director.Construct(ref collectibleBuilder, Collectible.CollectibleType.col1);
            CollectibleObject collectibleObject = (CollectibleObject)collectibleBuilder.GetResult();
            MainFrame.Instantiate(collectibleObject);
            MainFrame.Update();

            for(int i = 0; i < timeCount; i++) { MainFrame.Update(); }

            Assert.True((MainFrame.gameObjects.Values.Count == 1) == despawn, "gameobjects count: " + MainFrame.gameObjects.Values.Count);
        }

        // TM 13
        [Fact]
        public void TakeDamage_Player()
        {
            // set enviroment
            MainFrame.gameObjects = new Dictionary<int, GameObject>();
            MainFrame.detaTime = 0.07f;
            float timeCount = 3f / MainFrame.detaTime;

            // spawn player
            int playerId = MainFrame.CreateNewPlayer("AKA");
            MainFrame.Update();
            MainFrame.gameObjects[playerId].position = new float[] { 0, 0 };

            // spawn collectible
            CollectibleBuilder collectibleBuilder = new CollectibleBuilder(new float[] { 0,0});
            Director.director.Construct(ref collectibleBuilder, Collectible.CollectibleType.col1);
            CollectibleObject collectibleObject = (CollectibleObject)collectibleBuilder.GetResult();
            MainFrame.Instantiate(collectibleObject);
            MainFrame.Update();

            for (int i = 0; i < timeCount; i++) { MainFrame.Update(); }

            Assert.True(MainFrame.gameObjects[playerId].GetComponent<Player>().health < MainFrame.gameObjects[playerId].GetComponent<Player>().maxHealth, "gameobjects count: " + MainFrame.gameObjects.Values.Count);
        }

        // TM 16
    }
}
