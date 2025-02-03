namespace ChessLogic
{
    public class Castle : Move //don't need to move your king if you just kill everything first
    {
        public override MoveType Type { get; } //how castling?
        public override Position FromPos { get; } //where king
        public override Position ToPos { get; } //king desitnation - like bahamas

        private readonly Direction kingMoveDir; //king direction
        private readonly Position rookFromPos; //I like to move it move it?
        private readonly Position rookToPos; //rook desitnation

        public Castle(MoveType type, Position kingPos) //castling type constructor
        {
            Type = type;
            FromPos = kingPos;

            if (type == MoveType.CastleKS) ///king side
            {
                kingMoveDir = Direction.East;
                ToPos = new Position(kingPos.Row, 6);
                rookFromPos = new Position(kingPos.Row, 7);
                rookToPos = new Position(kingPos.Row, 5);
            }
            else if (type == MoveType.CastleQS) //queen side
            {
                kingMoveDir = Direction.West;
                ToPos = new Position(kingPos.Row, 2);
                rookFromPos = new Position(kingPos.Row, 0); //literally squares are numbers and numbers are squares
                rookToPos = new Position(kingPos.Row, 3); //and my brain hurts
            }
        }
        public override bool Execute(Board board) //execute castle move
        {
            new NormalMove(FromPos, ToPos).Execute(board);
            new NormalMove(rookFromPos, rookToPos).Execute(board);

            return false;
        }

        public override bool IsLegal(Board board) //test to see if it follows the castling rules
        {
            Player player = board[FromPos].Color; //who's move

            if (board.IsInCheck(player)) //king not in check?
            {
                return false;
            }

            //if (!board.HasCastlingRights(player, Type)) //first time castleing?
            //{
            //    return false;
            //}

            //if (!AreSquaresBetweenEmpty(board)) //nothing in between
            //{
            //    return false;
            //}

            Board copy = board.Copy();
            Position kingPosInCopy = FromPos;


            for (int i = 0; i < 2; i++)
            {
                new NormalMove(kingPosInCopy, kingPosInCopy + kingMoveDir).Execute(copy);
                kingPosInCopy += kingMoveDir;

                if (copy.IsInCheck(player)) //helps determine if the King iis in check at any point during the castling movement, sort of minute detail but everything the internet says is true
                {
                    return false;
                }
            }

            return true; //good to castle, castle is good!
        }
    }
}
