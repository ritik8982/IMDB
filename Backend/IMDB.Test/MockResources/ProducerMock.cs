using System;
using System.Collections.Generic;
using System.Linq;
using Assignment.Models.DBModels;
using Assignment.Repository.Interfaces;
using Moq;

namespace DemoApp.Test.MockResources
{
    public class ProducerMock
    {
        /// <summary>
        /// See how we are using Moq - https://github.com/moq/moq4
        /// </summary>
        
 
        public static readonly Mock<IProducerRepository> ProducerRepoMock = new Mock<IProducerRepository>();

        private static readonly List<Producer> ListOfProducers = new List<Producer>
        {
            new Producer
            {
                Id = 1,
                Name = "p1",
                Bio = "p1's bio",
                Gender = "male"
            },
             new Producer
            {
                Id = 3,
                Name = "p3",
                Bio = "p3's bio",
                Gender = "male"
            }
        };

        public static void MockGetAll()
        {
            ProducerRepoMock.Setup(x => x.Get()).Returns(ListOfProducers.ToList());
        }
        public static void MockGet()
        {
            ProducerRepoMock.Setup(x => x.Get(It.Is<int>(
             i => ListOfProducers.FirstOrDefault
                 (producer => producer.Id == i) != null)
        )).Returns(ListOfProducers.First(x => x.Id == 1));
        }
        public static void MockDelete()
        {
            // i is representing jo parameter me aane wala hai delete() function ke
            //if the function provided to delete is returning true when the return value that we have specified will be returned
            //otherwise the default value will be returned in this case is 0
            ProducerRepoMock.Setup(x => x.Delete(It.Is<int>(
              i => ListOfProducers.FirstOrDefault
                  (actor => actor.Id == i) != null)
         )).Returns(2);
        }
        public static void MockCreate()
        {
            ProducerRepoMock.Setup(x => x.Create(It.IsAny<Producer>())).Returns(2);
        }
        public static void MockUpdate()
        {
            ProducerRepoMock.Setup(x => x.Update(It.Is<int>(
              i => ListOfProducers.FirstOrDefault
                  (actor => actor.Id == i) != null), It.IsAny<Producer>())).Returns(2);
        }
    }
}