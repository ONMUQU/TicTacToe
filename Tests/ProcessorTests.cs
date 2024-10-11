using TicTacToe;
namespace Tests
{
    [TestClass]
    public class ProcessorTests
    {

        [TestMethod]
        public void MoveTest()
        {
            Processor proc = new();
            Assert.AreEqual(true, proc.TryToMove(Side.X, 1, 1));

            Assert.AreEqual(false, proc.TryToMove(Side.O, 1, 1));
            proc.Reset();
        }

        [TestMethod]
        public void GameStateTest()
        {
            Processor proc = new();
            { // diagonal test
                proc.TryToMove(Side.X, 0, 0);
                proc.TryToMove(Side.X, 1, 1);
                proc.TryToMove(Side.X, 2, 2);

                var expected = GameState.X_wins;
                Assert.AreEqual(expected, proc.CurrentGameState());
            }
            proc.Reset();
            { // row test
                proc.TryToMove(Side.O, 0, 0);
                proc.TryToMove(Side.O, 0, 1);
                proc.TryToMove(Side.O, 0, 2);

                var expected = GameState.O_wins;
                Assert.AreEqual(expected, proc.CurrentGameState());
            }
            proc.Reset();
            { // column test
                proc.TryToMove(Side.X, 0, 0);
                proc.TryToMove(Side.X, 1, 0);
                proc.TryToMove(Side.X, 2, 0);

                var expected = GameState.X_wins;
                Assert.AreEqual(expected, proc.CurrentGameState());
            }
            proc.Reset();
            { // draw test
                proc.TryToMove(Side.O, 0, 0);
                proc.TryToMove(Side.O, 0, 1);
                proc.TryToMove(Side.O, 1, 2);
                proc.TryToMove(Side.O, 2, 0);
                proc.TryToMove(Side.X, 0, 2);
                proc.TryToMove(Side.X, 1, 0);
                proc.TryToMove(Side.X, 1, 1);
                proc.TryToMove(Side.X, 2, 1);
                proc.TryToMove(Side.X, 2, 2);
                var expected = GameState.Draw;
                Assert.AreEqual(expected, proc.CurrentGameState());
            }
            proc.Reset();
            { // not over test
                proc.TryToMove(Side.X, 1, 1);
                proc.TryToMove(Side.O, 1, 0);

                var expected = GameState.NotOver;
                Assert.AreEqual(expected, proc.CurrentGameState());
            }
            proc.Reset();
        }
    }
}