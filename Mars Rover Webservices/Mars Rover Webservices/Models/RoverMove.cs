namespace Mars_Rover_Webservices.Models
{
    /// <summary>
    /// Carrier object denoting a rover move ooperation.
    /// </summary>
    public class RoverMove
    {
        public int? RoverId { get; set; }
        public string MovementInstruction { get; set; }
    }
}