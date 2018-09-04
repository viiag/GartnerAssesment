using System;
using System.Collections.Generic;
using System.Linq;

namespace Mars_Rover_Webservices.Models
{
    public class Position
    {
        /// <summary>
        /// Cardinal Direction
        /// </summary>
        public enum Direction
        {
            N,
            E,
            S,
            W
        }

        // The internal storage of heading. We need to use it as a number
        // internally, but we need to display it as a character going out.
        private Direction _heading;

        public Position()
        {
            XPosition = 0;
            YPosition = 0;
            _heading = Direction.N;
        }

        /// <summary>
        /// The current X Coordinate
        /// </summary>
        public int XPosition { get; set; }
        /// <summary>
        /// The current Y Coordinate
        /// </summary>
        public int YPosition { get;  set; }

        /// <summary>
        /// The current heading
        /// </summary>
        /// <remarks>
        /// These are the 4 cardinal direcitons express as "N, S, E, W"
        /// </remarks>
        public string Heading { get { return Enum.GetName(typeof(Position.Direction), _heading); }}

        /// <summary>
        /// Updates the current position
        /// </summary>
        /// <param name="movementInstructions">String of movement orders</param>
        /// <remarks>Values are restricted to "M", "L" and "R"</remarks>
        public void Update(string movementInstructions)
        {
            // Validate inputs
            List<char> instructions = new List<char>(movementInstructions.ToCharArray());
            char[] validInstructions = { 'M', 'R', 'L' };
            if(instructions.Any(x => !validInstructions.Contains(x)))
                throw new ArgumentException("Movement instructions are restricted to the values \"M\", \"L\", and \"R\"");

            foreach (char instruction in instructions)
            {
                switch (instruction)
                {
                    case 'R':
                        executeTurn(instruction);
                        break;
                    case 'L':
                        executeTurn(instruction);
                        break;
                    case 'M':
                        executeMove();
                        break;
                }
            }
        }

        /// <summary>
        /// Handles rotating the rover
        /// </summary>
        /// <param name="direction">90 Degree turn direction</param>
        private void executeTurn(char direction)
        {
            switch(direction)
            {
                case 'R':
                    _heading = (Direction)(((int)_heading + 1) % 4);
                    break;
                case 'L':
                    // You need to add 4 to the result to handle the case where you make the turn from north to west.
                    _heading = (Direction)((((int)_heading - 1) + 4) % 4);
                    break;
            }
        }

        /// <summary>
        /// Handles moving in a cardinal direction
        /// </summary>
        private void executeMove()
        {
            switch(_heading)
            {
                case Direction.N:
                    YPosition++;
                    break;
                case Direction.E:
                    XPosition++;
                    break;
                case Direction.S:
                    YPosition--;
                    break;
                case Direction.W:
                    XPosition--;
                    break;
            }
        }

    }
}