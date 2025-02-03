namespace ChessLogic
{
    public class Bishop : Piece
    {
        public override PieceType Type => PieceType.Bishop; //he's a bishop!
        public override Player Color { get; } //piece color

        private static readonly Direction[] dirs = new Direction[] //diagnol movement only, kinda simple compared to other pieces
        {
            Direction.NorthWest,
            Direction.NorthEast,
            Direction.SouthWest,
            Direction.SouthEast
        };

        public Bishop(Player color) //set color
        {
            Color = color;
        }

        public override Piece Copy() //makes copy for piece movement
        {
            Bishop copy = new Bishop(Color);
            copy.HasMoved = HasMoved; //catholic or protestant? or Lutheran?
            return copy;
        }

        public override IEnumerable<Move> GetMoves(Position from, Board board) //ensures it's legal
        {
            return MovePositionsInDirs(from, board, dirs).Select(to => new NormalMove(from, to));
        }
    }
}
