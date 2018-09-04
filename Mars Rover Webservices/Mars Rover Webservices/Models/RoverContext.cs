using System.Collections.Generic;

namespace Mars_Rover_Webservices.Models
{
    /// <summary>
    /// Data context for all rover information
    /// </summary>
    /// <remarks>
    /// We're only using this as a static in-memory store as a shortcut for the excercise
    /// nominally you want this to be completely stateless and you'd dump this to a DB
    /// </remarks>
    public class RoverContext 
    {
        private static Dictionary<int, Rover> _rovers = new Dictionary<int, Rover>();
        /// <summary>
        /// All Rovers
        /// </summary>
        /// <remarks>
        /// Key: Rover Id
        /// Value: Rover
        /// </remarks>
        public static Dictionary<int, Rover> Rovers
        {
            get
            {
                return _rovers;
            }
        }
    }
}