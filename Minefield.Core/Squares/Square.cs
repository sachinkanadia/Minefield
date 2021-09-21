namespace Minefield.Core.Squares
{
    public class Square
    {
        public Square(int row, int column)
        {
            _Row = row;
            _Column = column;
        }

        private readonly int _Row;
        private readonly int _Column;
        private bool _HasMine;

        public int Row { get { return _Row; } }
        public int Column { get { return _Column; } }
        public bool HasMine 
        {
            get 
            { 
                return _HasMine;
            } 
            set 
            {
                if (_HasMine == value) 
                    return;

                _HasMine = value; 
            }
        }

        public Coordinate ToCoordinate()
        {
            return new Coordinate(this);
        }
    }
}