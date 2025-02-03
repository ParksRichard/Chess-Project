namespace ChessLogic
{
    public class NormalMove : Move
    {
        public override MoveType Type => MoveType.Normal;
        public override Position FromPos { get; }
        public override Position ToPos { get; }

        public NormalMove(Position from, Position to) //constructor
        {
            FromPos = from;
            ToPos = to;
        }

        //public override bool Execute(Board board)
        //{
        //    Piece piece = board[FromPos];
        //    bool capture = !board.IsEmpty(ToPos);
        //    board[ToPos] = piece;
        //    board[FromPos] = null;
        //    piece.HasMoved = true;

        //    return capture || piece.Type == PieceType.Pawn;
        //}

        public override bool Execute(Board board) //better variance?
        {
            Piece piece = board[FromPos]; //start position

            if (piece == null) //is there something there?
            {
                return false; // invalid move
            }

            bool capture = !board.IsEmpty(ToPos); 
            board[ToPos] = piece; //move piece
            board[FromPos] = null; 
            piece.HasMoved = true; //marked as moved

            return capture || piece.Type == PieceType.Pawn; 
        }

        //be cool to make this 3D with piece taking animations
    }
}
