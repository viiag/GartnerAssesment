using System;
using Mars_Rover_Webservices.Controllers;
using Mars_Rover_Webservices.Models;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Collections.Generic;

namespace Mars_Rover_Webservices.Tests.Controllers
{
    [TestClass]
    public class RoverControllerTest
    {
        [TestCleanup]
        public void CleanupAfterTests()
        {
            RoverContext.ResetContext();
        }

        [TestMethod]
        public void GetRoverPosition_Success()
        {
            RoverController controller = new RoverController();

            RoverContext.Rovers.Add(1234, new Rover()
            {
                    RoverId = 1234
                    , RoverName = "Foo"
                    , CurrentPosition = new Position() { XPosition = 2, YPosition = -4}
            });

            var result = controller.GetRoverPosition(1234) as OkNegotiatedContentResult<Rover>;

            Assert.IsNotNull(result);
            Assert.AreEqual("N", result.Content.CurrentPosition.Heading);
            Assert.AreEqual(2, result.Content.CurrentPosition.XPosition);
            Assert.AreEqual(-4, result.Content.CurrentPosition.YPosition);
        }

        [TestMethod]
        public void CreateRover_Success()
        {
            RoverController controller = new RoverController();
            // You this needs to return the request URI as it's a post so we're mocking it here.
            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/rovers")
            };

            int roverId = 1;
            string roverName = "foo";
            RoverInfo basicInput = new RoverInfo() { RoverId = roverId, RoverName = roverName };
            var result = controller.CreateRover(basicInput) as CreatedNegotiatedContentResult<Rover>;

            Assert.IsNotNull(result);
            Assert.AreEqual(roverId, result.Content.RoverId);
            Assert.AreEqual(roverName, result.Content.RoverName);
        }

        [TestMethod]
        public void RenameRover_Success()
        {
            RoverController controller = new RoverController();

            RoverContext.Rovers.Add(1234, new Rover() { RoverId = 1234, RoverName = "Foo" });

            RoverInfo updatedInfo = new RoverInfo() { RoverId = 1234, RoverName = "bar" };

            var result = controller.RenameRover(updatedInfo) as OkResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("bar", RoverContext.Rovers[1234].RoverName, "Rover name was not updated");
        }

        [TestMethod]
        public void MoveRover_Success()
        {
            RoverController controller = new RoverController();

            RoverContext.Rovers.Add(1234, new Rover() { RoverId = 1234, RoverName = "Foo" });

            RoverMove movementInsruction = new RoverMove() { RoverId = 1234, MovementInstruction = "RMM" };

            var result = controller.MoveRover(movementInsruction) as OkNegotiatedContentResult<Rover>;
            Assert.IsNotNull(result);
            Assert.AreEqual("E", result.Content.CurrentPosition.Heading, "Rover did not rotate right correctly");
            Assert.AreEqual(2, result.Content.CurrentPosition.XPosition, "Rover did not move right correctly");

            movementInsruction = new RoverMove() { RoverId = 1234, MovementInstruction = "LLMMM" };
            result = controller.MoveRover(movementInsruction) as OkNegotiatedContentResult<Rover>;

            Assert.AreEqual("W", result.Content.CurrentPosition.Heading, "Rover did not rotate left correctly");
            Assert.AreEqual(-1, result.Content.CurrentPosition.XPosition, "Rover did not move left correctly");
        }

        /******************************************************\
         * I should also test all the failure states, but
         * for the purposes of this excercise I'm opting not
         * to since it adds a whole mess of time to a simple test
        */
        #region Failure Tests
        [TestMethod, Ignore]
        public void GetRoverPosition_Fail()
        {
            throw new NotImplementedException();
        }

        [TestMethod, Ignore]
        public void CreateRover_Fail()
        {
            throw new NotImplementedException();
        }

        [TestMethod, Ignore]
        public void RenameRover_Fail()
        {
            throw new NotImplementedException();
        }

        [TestMethod, Ignore]
        public void MoveRover_Fail()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
