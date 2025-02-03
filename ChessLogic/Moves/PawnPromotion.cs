namespace ChessLogic
{
    public class PawnPromotion : Move
    {
        public override MoveType Type => MoveType.PawnPromotion;
        public override Position FromPos { get; } //start
        public override Position ToPos { get; } //end spot

        private readonly PieceType newType; //piece type for promotion to

        public PawnPromotion(Position from, Position to, PieceType newType)
        {
            FromPos = from;
            ToPos = to;
            this.newType = newType;
        }

        private Piece CreatePromotionPiece(Player color) //who will I be now?
        {
            return newType switch
            {
                PieceType.Knight => new Knight(color),
                PieceType.Bishop => new Bishop(color),
                PieceType.Rook => new Rook(color),
                _ => new Queen(color) //default queen?
            };
        }

        public override bool Execute(Board board)
        {
            Piece pawn = board[FromPos];
            board[FromPos] = null;

            Piece promotionPiece = CreatePromotionPiece(pawn.Color); //replacing pawn with selected piece
            promotionPiece.HasMoved = true;
            board[ToPos] = promotionPiece; //removes pawn from board

            return true;
        }

        //public override bool IsLegal(Board board) //can pawns be promoted?
        //{
        //    Piece piece = board[FromPos];

        //}
    }
}
