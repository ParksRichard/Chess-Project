namespace ChessLogic
{
    public abstract class Piece //class for inheritance - overall piece structure
    {
        public abstract PieceType Type { get; } //piece type
        public abstract Player Color { get; } //storing piece color
        public bool HasMoved { get; set; } = false; //has moved?

        public abstract Piece Copy(); //copy of piece for replacement after moves

        public abstract IEnumerable<Move> GetMoves(Position from, Board board); //showing all legeal moves - highlight?

        protected IEnumerable<Position> MovePositionsInDir(Position from, Board board, Direction dir) //availible moves
        {
            for (Position pos = from + dir; Board.IsInside(pos); pos += dir)
            {
                if (board.IsEmpty(pos))
                {
                    yield return pos;
                    continue;
                }

                Piece piece = board[pos];

                if (piece.Color != Color)
                {
                    yield return pos; //this is for killing the enemy
                }

                yield break;
            }
        }

        //how to implement not leaving king in check? - override or no?

        protected IEnumerable<Position> MovePositionsInDirs(Position from, Board board, Direction[] dirs)
        {
            return dirs.SelectMany(dir => MovePositionsInDir(from, board, dir));
        }

        //public virtual bool CanCaptureOpponentKing(Position from, Board board) //king killing in 3-4 lines
        //{
        //    return GetMoves(from, board).Any(move =>
        //    {
        //        Piece piece = board[move.ToPos];
        //        return piece != null && piece.Type == PieceType.King;
        //    });
        //}

        public virtual bool CanCaptureOpponentKing(Position from, Board board) //cleaner? - test
        {
            return GetMoves(from, board)
                .FirstOrDefault(move => board[move.ToPos]?.Type == PieceType.King) != null;
        }
    }
}
