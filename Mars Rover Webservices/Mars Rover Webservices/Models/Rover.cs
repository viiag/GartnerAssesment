namespace Mars_Rover_Webservices.Models
{
    public class Rover
    {
        public Rover()
        {
            CurrentPosition = new Position();
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