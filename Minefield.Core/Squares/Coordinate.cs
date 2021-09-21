namespace Minefield.Core.Squares
{
    public class Coordinate
    {
        internal Coordinate() { }

        public Coordinate(Square square)
        {
            _Row = (square.Row + 1).ToString();

            switch (square.Column)
            {
                case 0:
                    _Column = "A";
                    break;
                case 1:
                    _Column = "B";
                    break;
                case 2:
                    _Column = "C";
                    break;
                case 3:
                    _Column = "D";
                    break;
                case 4:
                    _Column = "E";
                    break;
                case 5:
                    _Column = "F";
                    break;
                case 6:
                    _Column = "G";
                    break;
                case 7:
                    _Column = "H";
                    break;
            }
        }

        private readonly string _Column;
        private readonly string _Row;

        public string Column { get { return _Column; } }
        public string Row { get { return _Row; } }
    }
}
