namespace ChessLogic
{
    public enum EndReason //enum because why not, like pieces just make it set and seperate
    {
        Checkmate,
        Stalemate,
        FiftyMoveRule,
        InsufficientMaterial,
        ThreefoldRepetition
    }
}
