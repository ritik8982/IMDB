using System;
using System.Collections.Generic;
using System.Linq;
using Assignment.Models.DBModels;
using Assignment.Repository.Interfaces;
using Moq;

namespace DemoApp.Test.MockResources
{
    public class GenreMock
    {
        /// <summary>
        /// See how we are using Moq - https://github.com/moq/moq4
        /// </summary>


        public static readonly Mock<IGenreRepository> GenreRepoMock = new Mock<IGenreRepository>();

        private static readonly List<Genre> ListOfGenre = new List<Genre>
        {
            new Genre
            {
                Id = 1,
                Name = "G1",
            },
             new Genre
            {
                Id = 2,
                Name = "G2",
            }
        };

        public static void MockGetAll()
        {
            GenreRepoMock.Setup(x => x.Get()).Returns(ListOfGenre.ToList());
        }

        public static void MockGet()
        {
            GenreRepoMock.Setup(x => x.Get(It.Is<int>(
             i => ListOfGenre.FirstOrDefault
                 (genre => genre.Id == i) != null)
        )).Returns(ListOfGenre.First(x => x.Id == 1));
        }

        public static void MockDelete()
        {
            // i is representing jo parameter me aane wala hai delete() function ke
            //if the function provided to delete is returning true when the return value that we have specified will be returned
            //otherwise the default value will be returned in this case is 0
            GenreRepoMock.Setup(x => x.Delete(It.Is<int>(
              i => ListOfGenre.FirstOrDefault
                  (genre => genre.Id == i) != null)
         )).Returns(2);
        }

        public static void MockCreate()
        {
            GenreRepoMock.Setup(x => x.Create(It.IsAny<Genre>())).Returns(2);
        }

        public static void MockUpdate()
        {
            GenreRepoMock.Setup(x => x.Update(It.Is<int>(
              i => ListOfGenre.FirstOrDefault
                  (genre => genre.Id == i) != null), It.IsAny<Genre>())).Returns(2);
        }
    }
}