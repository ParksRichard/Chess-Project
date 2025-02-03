namespace ChessLogic
{
    public class GameState //this program is brought to you by co-pilot and chatgpt and youtube
    {
        public Board Board { get; } //tracking chess pieces on the board
        public Player CurrentPlayer { get; private set; } //tracking player turns
        public Result Result { get; private set; } = null; //storing game outcomes

        private int noCaptureOrPawnMoves = 0; //tracking moves without captures or pawn moves
        private string stateString; //setting up a string to track game states

        private readonly Dictionary<string, int> stateHistory = new Dictionary<string, int>();

        //make a bunch of stuff readonly so it doesn't get changed in some weird way mid-game
        //like hardcoding everything because I didn't before and things got weird or didn't work right

        private readonly Stack<Move> moveHistory = new Stack<Move>(); // Tracks move history for undo

        public GameState(Player player, Board board)
        {
            CurrentPlayer = player;
            Board = board;

            stateString = new StateString(CurrentPlayer, board).ToString(); //tracks occurrences for repitition rule
            stateHistory[stateString] = 1;
        }

        public IEnumerable<Move> LegalMovesForPiece(Position pos) // method for all the moves a piece can make
        {
            if (Board.IsEmpty(pos) || Board[pos].Color != CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }

            Piece piece = Board[pos];
            IEnumerable<Move> moveCandidates = piece.GetMoves(pos, Board);
            return moveCandidates.Where(move => move.IsLegal(Board));
        }

        public void MakeMove(Move move) //making the piece move and updating game state
        {
            Board.SetPawnSkipPosition(CurrentPlayer, null);
            bool captureOrPawn = move.Execute(Board);

            moveHistory.Push(move); // Save move for undo

            if (captureOrPawn)
            {
                noCaptureOrPawnMoves = 0;
                stateHistory.Clear();
            }
            else
            {
                noCaptureOrPawnMoves++;
            }

            LogMove(move);

            CurrentPlayer = CurrentPlayer.Opponent();
            UpdateStateString();
            CheckForGameOver();
        }

        private void LogMove(Move move) //keeping tabs on moves made
        {
            Console.WriteLine($"Move: {move.Type} {move.FromPos} -> {move.ToPos}");
        }


        public void UndoMove()
            //undo special moves?
                // oh god..
        {
            if (moveHistory.Count == 0)
            {
                Console.WriteLine("No moves to undo!");
                return;
            }

            Move lastMove = moveHistory.Pop(); // Get last move
            //lastMove.Undo(Board); //reverse move that don't wanna work
            CurrentPlayer = CurrentPlayer.Opponent(); // swtich players

            // calculate state for repetition rule
            UpdateStateString();
            Console.WriteLine($"Move undone: {lastMove.FromPos} -> {lastMove.ToPos}");
        }

        public IEnumerable<Move> AllLegalMovesFor(Player player)
        {
            IEnumerable<Move> moveCandidates = Board.PiecePositionsFor(player).SelectMany(pos =>
            {
                Piece piece = Board[pos];
                return piece.GetMoves(pos, Board);
            });

            return moveCandidates.Where(move => move.IsLegal(Board));
        }

        private void CheckForGameOver() //ways to finish game with rules I had to look up
        {
            if (!AllLegalMovesFor(CurrentPlayer).Any())
            {
                if (Board.IsInCheck(CurrentPlayer))
                {
                    Result = Result.Win(CurrentPlayer.Opponent());
                }
                else
                {
                    Result = Result.Draw(EndReason.Stalemate);
                }
            }
            else if (Board.InsufficientMaterial())
            {
                Result = Result.Draw(EndReason.InsufficientMaterial);
            }
            else if (FiftyMoveRule())
            {
                Result = Result.Draw(EndReason.FiftyMoveRule);
            }
            else if (ThreefoldRepetition())
            {
                Result = Result.Draw(EndReason.ThreefoldRepetition);
            }
        }

        public bool IsGameOver() //please tell me it's over
        {
            return Result != null;
        }

        private bool FiftyMoveRule()
        {
            int fullMoves = noCaptureOrPawnMoves / 2;
            return fullMoves == 50;
        }

        private void UpdateStateString()
        {
            stateString = new StateString(CurrentPlayer, Board).ToString();

            if (!stateHistory.ContainsKey(stateString))
            {
                stateHistory[stateString] = 1;
            }
            else
            {
                stateHistory[stateString]++;
            }
        }

        private bool ThreefoldRepetition()
        {
            return stateHistory[stateString] == 3;
        }
    }
}
