namespace ChessLogic
{
    public enum MoveType
    {
        Normal,
        CastleKS, //castling next to king
        CastleQS, //castling next to queen
        DoublePawn,
        EnPassant,
        PawnPromotion
    }
}
