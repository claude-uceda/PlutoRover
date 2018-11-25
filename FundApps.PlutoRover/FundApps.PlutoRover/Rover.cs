using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FundApps.PlutoRoverTest")]

namespace FundApps.PlutoRover
{
    public class Rover
    {
        public Planisfere Planisfere { get; private set; }

        public Coordinate Coordinate { get; private set; }

        public Orientation Orientation { get; private set; }


        public Rover(Planisfere planisfere, Position position)
        {
            Planisfere = planisfere;
            Coordinate = position.Coordinate;
            Orientation = position.Orientation;
        }

        internal Orientation Rotate(int direction)
        {
            var rotation = ((int) Orientation) + direction;
            if (rotation > 4)
            {
                rotation = rotation - 4;
            }
            else if (rotation < 0)
            {
                rotation = rotation + 4;
            }

            return (Orientation) rotation;
        }

        internal Coordinate Step()
        {
            var result = new Coordinate {X = Coordinate.X, Y = Coordinate.Y};

            Action<Coordinate> moveFunc;
            int delta;
            switch (Orientation)
            {
                case Orientation.North:
                {
                    delta = 1;
                    moveFunc = coordinate => { coordinate.Y += delta; };
                    break;
                }
                case Orientation.South:
                {
                    delta = -1;
                    moveFunc = coordinate => { coordinate.Y += delta; };
                    break;
                }
                case Orientation.East:
                {
                    delta = 1;
                    moveFunc = coordinate => { coordinate.X += delta; };
                    break;
                }
                case Orientation.West:
                {
                    delta = -1;
                    moveFunc = coordinate => { coordinate.X += delta; };
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            moveFunc.Invoke(result);

            //ensuring coordinates ar e not out of the map
            if (result.Y > (Planisfere.Height - 1))
                result.Y -= (Planisfere.Height);
            else if (result.Y < 0)
                result.Y += Planisfere.Height;

            if (result.X > (Planisfere.Width - 1))
                result.X -= (Planisfere.Width);
            else if (result.X < 0)
                result.X += Planisfere.Width;

            return result;
        }
    }
}
