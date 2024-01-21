using System;
using System.Collections.Generic;
using System.Linq;
using Assignment.Models.DBModels;
using Assignment.Repository.Interfaces;
using Moq;

namespace DemoApp.Test.MockResources
{
    public class ActorMock
    {
        /// <summary>
        /// See how we are using Moq - https://github.com/moq/moq4
        /// </summary>
        
 
        public static readonly Mock<IActorRepository> ActorRepoMock = new Mock<IActorRepository>();

        private static readonly List<Actor> ListOfActors = new List<Actor>
        {
            new Actor
            {
                Id = 1,
                Name = "A1",
                Bio = "--",
                Gender = "male"
            },
             new Actor
            {
                Id = 3,
                Name = "A3",
                Bio = "--",
                Gender = "male"
            }
        };

        public static void MockGetAll()
        {
            ActorRepoMock.Setup(x => x.Get()).Returns(ListOfActors.ToList());
        }

        public static void MockGet()
        {
            ActorRepoMock.Setup(x => x.Get(It.Is<int>(
             i => ListOfActors.FirstOrDefault
                 (actor => actor.Id == i) != null)
        )).Returns(ListOfActors.First(x => x.Id == 1));
        }

        public static void MockDelete()
        {
            // i is representing jo parameter me aane wala hai delete() function ke
            //if the function provided to delete is returning true when the return value that we have specified will be returned
            //otherwise the default value will be returned in this case is 0
            ActorRepoMock.Setup(x => x.Delete(It.Is<int>(
              i => ListOfActors.FirstOrDefault
                  (actor => actor.Id == i) != null)
         )).Returns(2);
        }

        public static void MockCreate()
        {
            ActorRepoMock.Setup(x => x.Create(It.IsAny<Actor>())).Returns(2);
        }

        public static void MockUpdate()
        {
            ActorRepoMock.Setup(x => x.Update(It.Is<int>(
              i => ListOfActors.FirstOrDefault
                  (actor => actor.Id == i) != null), It.IsAny<Actor>())).Returns(2);
        }
    }
}