namespace ChessLogic
{
    public abstract class Move
    {
        public abstract MoveType Type { get; } //what is the type of move?
        public abstract Position FromPos { get; } //start position
        public abstract Position ToPos { get; } //end position

        public abstract bool Execute(Board board);

        public virtual bool IsLegal(Board board) //can I do this or make up own rules?
        {
            Player player = board[FromPos].Color;
            Board boardCopy = board.Copy();
            Execute(boardCopy);
            return !boardCopy.IsInCheck(player);
        }


        //public void Undo(Board board)
        //{
        //    board[FromPos] = MovedPiece;  //  move piece back
        //    board[ToPos] = CapturedPiece; // Restore any captured piece

        //    // Handle special moves
        //    if (Type == MoveType.Castling)
        //        UndoCastling(board);
        //    else if (Type == MoveType.EnPassant)
        //        UndoEnPassant(board);
        //}

    }

}
