namespace ChessLogic
{
    public class DoublePawn : Move
    //https://en.wikipedia.org/wiki/En_passant - I had no idea what this was but have always done it, apparently french for "in passing"
    //learn something new everyday - insert French joke somewhere about how they should always play white
    //double pawn for moving foward twice - but also mixed with EnPassant because pawns get all the cool moves - cool little guy
    {
        public override MoveType Type => MoveType.DoublePawn;
        public override Position FromPos { get; } //start position
        public override Position ToPos { get; } // end position

        private readonly Position skippedPos;

        public DoublePawn(Position from, Position to) //construct me for establishing positions and where we're going
            //how to chnage name of variables all across code
            //-ctrl shift L?
            ///really don't want ot hash through all of this again
        {
            FromPos = from;
            ToPos = to;
            skippedPos = new Position((from.Row + to.Row) / 2, from.Column);
        }
        
        public override bool Execute(Board board) //execute move
        {
            Player player = board[FromPos].Color;
            board.SetPawnSkipPosition(player, skippedPos);
            new NormalMove(FromPos, ToPos).Execute(board);

            return true;
        }
    }
}
