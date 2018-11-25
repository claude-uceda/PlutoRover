using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FundApps.PlutoRoverTest")]

namespace FundApps.PlutoRover
{
    public class Rover
    {
        #region Propertie(s)

        private readonly Planisphere _planisfere;

        public Coordinate Coordinate { get; private set; }

        public Orientation Orientation { get; private set; }

        #endregion

        public Rover(Planisphere planisfere, Position position)
        {
            _planisfere = planisfere;
            Coordinate = position.Coordinate;
            Orientation = position.Orientation;
        }

        /// <summary>
        /// Moves the rover according to the instructions
        /// </summary>
        /// <param name="instruction"></param>
        /// <param name="instructions"></param>
        /// <returns></returns>
        public Position Move(char instruction, params char[] instructions)
        {
            var complete = new List<char> { instruction };
            if (instructions.Any())
            {
                complete.AddRange(instructions);
            }
            //iterate complete to ensure all instructions are known
            return Move(complete);
        }

        private Position Move(IEnumerable<char> instructions)
        {
            var position = new Position { Coordinate = Coordinate, Orientation = Orientation };

            foreach (var instruction in instructions)
            {
                try
                {
                    position = MoveOne(instruction);
                }
                catch
                {
                    break;
                }
            }

            return position;
        }

        internal Position MoveOne(char instruction)
        {
            var newCoordinate = Coordinate;
            var newOrientation = Orientation;

            switch (instruction)
            {
                case 'L':
                case 'l':
                {
                    newOrientation = Rotate(-1);
                    break;
                }
                case 'R':
                case 'r':
                {
                    newOrientation = Rotate(1);
                    break;
                }
                case 'F':
                case 'f':
                {
                    newCoordinate = Step(1);
                    CheckNextStep(newCoordinate);
                    break;
                }
                case 'B':
                case 'b':
                {
                    newCoordinate = Step(-1);
                    CheckNextStep(newCoordinate);
                    break;
                }
            }

            Coordinate = newCoordinate;
            Orientation = newOrientation;

            return new Position { Coordinate = Coordinate, Orientation = Orientation };
        }

        internal void CheckNextStep(Coordinate newCoordinate)
        {
            //todo: implement obstacle detection logic (sensor's result?)
            //Exception type to create and throw
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

        internal Coordinate Step(int direction)
        {
            var result = new Coordinate {X = Coordinate.X, Y = Coordinate.Y};

            Action<Coordinate> moveFunc;
            int delta;
            switch (Orientation)
            {
                case Orientation.North:
                {
                    delta = 1 * direction;
                    moveFunc = coordinate => { coordinate.Y += delta; };
                    break;
                }
                case Orientation.South:
                {
                    delta = -1 * direction;
                    moveFunc = coordinate => { coordinate.Y += delta; };
                    break;
                }
                case Orientation.East:
                {
                    delta = 1 * direction;
                    moveFunc = coordinate => { coordinate.X += delta; };
                    break;
                }
                case Orientation.West:
                {
                    delta = -1 * direction;
                    moveFunc = coordinate => { coordinate.X += delta; };
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            moveFunc.Invoke(result);

            //ensuring coordinates ar e not out of the map
            if (result.Y > (_planisfere.Height - 1))
                result.Y -= (_planisfere.Height);
            else if (result.Y < 0)
                result.Y += _planisfere.Height;

            if (result.X > (_planisfere.Width - 1))
                result.X -= (_planisfere.Width);
            else if (result.X < 0)
                result.X += _planisfere.Width;

            return result;
        }
    }
}
