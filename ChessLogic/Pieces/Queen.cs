namespace ChessLogic
{
    public class Queen : Piece
    {
        public override PieceType Type => PieceType.Queen; //sacrifice the queen!
        public override Player Color { get; }

        private static readonly Direction[] dirs = new Direction[] //movement direction, goes everywhere
        {
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West,
            Direction.NorthWest,
            Direction.NorthEast,
            Direction.SouthWest,
            Direction.SouthEast
        };

        public Queen(Player color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Queen copy = new Queen(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board) //still goes everywhere
        {
            return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
        }

        //public override IEnumerable<Move> GetMoves(Position from, Board board)
        //{
        //    foreach (Position to in MovePositionsInDirs(from, board, dirs))
        //    {
        //        if (!board.IsInCheck(Color, from, to)) // dont' leave king in check? - how to implement like automatic saftey protocol
        //          //different board perspectives make it hard to envision peices and whatnot
        //        {
        //            yield return new NormalMove(from, to);
        //        }
        //    }
        //}
    }
}
