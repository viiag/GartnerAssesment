namespace Mars_Rover_Webservices.Models
{
    /// <summary>
    /// Simple remote rover
    /// </summary>
    public class Rover
    {
        /// <summary>
        /// Creates an empty rover with default position info
        /// </summary>
        public Rover()
        {
            CurrentPosition = new Position();
        }

        /// <summary>
        /// Creates a rover using passed in identifying info with a default position
        /// </summary>
        /// <param name="info"></param>
        public Rover(RoverInfo info) : this()
        {
            RoverId = info.RoverId;
            RoverName = info.RoverName;
        }

        /// <summary>
        /// Identifier for this rover instance
        /// </summary>
        public int? RoverId { get; set; }
        /// <summary>
        /// Name of the Rover
        /// </summary>
        public string RoverName { get; set; }
        /// <summary>
        /// The current X, Y and heading of the rover.
        /// </summary>
        public Position CurrentPosition { get; set; }
    }
}