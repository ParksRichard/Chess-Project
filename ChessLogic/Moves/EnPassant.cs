namespace ChessLogic
{
    public class EnPassant : Move
    {
        public override MoveType Type => MoveType.EnPassant; //is a move type
        public override Position FromPos { get; } //start position
        public override Position ToPos { get; } //destination

        private readonly Position capturePos; //position of probably dead pawn

        public EnPassant(Position from, Position to) //constructor
        {
            FromPos = from;
            ToPos = to;
            capturePos = new Position(from.Row, to.Column);
        }

        public override bool Execute(Board board) //pretty similar to every other class for moves and pieces
        {
            new NormalMove(FromPos, ToPos).Execute(board);
            board[capturePos] = null;

            return true;
        }
    }
}
