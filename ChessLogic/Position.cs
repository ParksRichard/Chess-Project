namespace ChessLogic
{
    public class Position
    {
        public int Row { get; }
        public int Column { get; }

        public Position(int row, int column) //constructor for posiitions on board
        {
            Row = row;
            Column = column;
        }

        public Player SquareColor() //determine square color
        {
            if ((Row + Column) % 2 == 0)
            {
                return Player.White;
            }

            return Player.Black;
        }

        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   Row == position.Row &&
                   Column == position.Column;
        }

        public override int GetHashCode() //hashign for positions
        {
            return HashCode.Combine(Row, Column);
        }

        public static bool operator ==(Position left, Position right) //equal positions
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position left, Position right) //not equal positions
        {
            return !(left == right);
        }

        public static Position operator +(Position pos, Direction dir)
        {
            return new Position(pos.Row + dir.RowDelta, pos.Column + dir.ColumnDelta);
        }
    }
}
