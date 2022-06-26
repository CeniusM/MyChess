using MyChess.FEN;

namespace MyChess.SpeedTester.Tests
{
    partial class Test
    {
        public static TestReport TestGeneratePossibleMoves()
        {
            int maxFENStrings = 20;
            int reps = 10;
            int repsPerRep = 100000;
            int FENStringAmount = 0;
            ulong avg = 0;

            if (RandomFENList.GetLenght() > maxFENStrings)
                FENStringAmount = maxFENStrings;
            else
                FENStringAmount = RandomFENList.GetLenght();

            for (int i = 0; i < FENStringAmount; i++)
            {
                ChessGame chessGame = new ChessGame(RandomFENList.GetFEN(i));
                avg += (ulong)MyStopwatch.Measure(chessGame.possibleMoves.GenerateMoves, reps, repsPerRep);
            }
            avg /= (ulong)reps;

            return new("Time of the avg chess board GeneratePossibleMoves " + repsPerRep + " time(s) on random FEN strings",
                        (long)avg);
        }
    }
}