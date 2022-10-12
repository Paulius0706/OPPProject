using BlazorGame.Game;
using BlazorGame.Game.Builder;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            bool result = false;

            Assert.False(result, "1 should not be prime");
        }

        enum objectType
        {
            col1,
            col2,
            col3,
            mob,
            player
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
        public void CircleCollision(
            float[] pos1, float[] pos2,
            float[] vel1, float[] vel2,
            float[] newPos1, float[] newPos2)
        {
            MainFrame.gameObjects = new Dictionary<int, GameObject>();
            PlayerBuilder playerBuilder0 = new PlayerBuilder(pos1);
            Director.director.Construct(ref playerBuilder0, "0", 0);
            PlayerObject player0 = (PlayerObject)playerBuilder0.GetResult();
            player0.id = 0;
            player0.velocity = vel1;
            MainFrame.gameObjects.Add(0, player0);

            PlayerBuilder playerBuilder1 = new PlayerBuilder(pos2);
            Director.director.Construct(ref playerBuilder1, "1", 0);
            PlayerObject player1 = (PlayerObject)playerBuilder1.GetResult();
            player0.id = 1;
            player1.velocity = vel2;
            MainFrame.gameObjects.Add(1, player0);

            
            MainFrame.detaTime = 0.07f;
            float timeCount = 3f / 0.01f;

            for(int i = 0; i < timeCount; i++)
            {
                MainFrame.Update();
            }
            float errorDelta = 10f;
            Assert.True(
                   (MainFrame.gameObjects[0].position[0] < newPos1[0] + errorDelta || MainFrame.gameObjects[0].position[0] > newPos1[0] - errorDelta)
                && (MainFrame.gameObjects[0].position[1] < newPos1[1] + errorDelta || MainFrame.gameObjects[0].position[1] > newPos1[1] - errorDelta)
                && (MainFrame.gameObjects[1].position[0] < newPos2[0] + errorDelta || MainFrame.gameObjects[1].position[0] > newPos2[0] - errorDelta)
                && (MainFrame.gameObjects[1].position[1] < newPos2[1] + errorDelta || MainFrame.gameObjects[1].position[1] > newPos2[1] - errorDelta));
        }
    }
}
