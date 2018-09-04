namespace Mars_Rover_Webservices.Models
{
    /// <summary>
    /// Simple representation of rover identifier info
    /// </summary>
    public class RoverInfo
    {
        /// <summary>
        /// Identifier for this rover instance
        /// </summary>
        public int? RoverId { get; set; }
        /// <summary>
        /// Name of the Rover
        /// </summary>
        public string RoverName { get; set; }
    }
}