namespace Mars_Rover_Webservices.Models
{
    /// <summary>
    /// Carrier object denoting a rover move ooperation.
    /// </summary>
    public class RoverMove
    {
        /// <summary>
        /// Identifier for the rover
        /// </summary>
        public int? RoverId { get; set; }
        /// <summary>
        /// String of serial movement instructions
        /// </summary>
        public string MovementInstruction { get; set; }
    }
}