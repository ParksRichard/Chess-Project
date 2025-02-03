namespace ChessLogic
{
    public class Knight : Piece //horsing around = break time
    {
        public override PieceType Type => PieceType.Knight;
        public override Player Color { get; } //stores color

        public Knight(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Knight copy = new Knight(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        private static IEnumerable<Position> PotentialToPositions(Position from)
        {
            foreach (Direction VerticalDirection in new Direction[] { Direction.North, Direction.South })
            {
                foreach (Direction HorizontalDirection in new Direction[] { Direction.West, Direction.East })
                {
                    yield return from + 2 * VerticalDirection + HorizontalDirection;
                    yield return from + 2 * HorizontalDirection + VerticalDirection;
                }
            }
        }

       // private static readonly Position[] moveOffsets = new Position[]
       //{
       //     new Position(2, 1), new Position(2, -1), new Position(-2, 1), new Position(-2, -1),
       //     new Position(1, 2), new Position(1, -2), new Position(-1, 2), new Position(-1, -2)
       //};

        private IEnumerable<Position> MovePositions(Position from, Board board) //potential positinos
        {
            return PotentialToPositions(from).Where(pos => Board.IsInside(pos) 
                && (board.IsEmpty(pos) || board[pos].Color != Color));
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board) //legal moves

        {
            return MovePositions(from, board).Select(to => new NormalMove(from, to));
        }
    }
}
