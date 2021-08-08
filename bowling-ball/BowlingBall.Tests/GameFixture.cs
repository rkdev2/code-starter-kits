using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingBall.Tests
{
    [TestClass]
    public class GameFixture
    {
        [TestMethod]
        public void Gutter_game_score_should_be_zero_test()
        {
            var game = new Game();
            Roll(game, 0, 20);
            Assert.AreEqual(0, game.GetScore());
        }

        [TestMethod]
        [DataRow(data1: 186, moreData: new[] { 10, 9, 1, 5, 5, 7, 1, 10, 10, 10, 9, 0, 8, 2, 9, 1, 10 })]
        [DataRow(data1: 300, moreData: new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 })]
        [DataRow(data1: 24, moreData: new[] { 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 1, 3, 1, 1, 1 })]
        [DataRow(data1: 148, moreData: new[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 3 })]
        [DataTestMethod]
        public void GivenBallScore_WhenRoll_ThenShouldCalculateExpectedResult(int expectedScore, params int[] ballScores)
        {
            var game = new Game();

            foreach (var score in ballScores)
            {
                game.Roll(score);
            }

            Assert.AreEqual(expectedScore, game.GetScore());
        }

        private void Roll(Game game, int pins, int times)
        {
            for (var i = 0; i < times; i++)
            {
                game.Roll(pins);
            }
        }
    }
}