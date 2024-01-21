using System;
using System.Collections.Generic;
using System.Linq;
using Assignment.Models.DBModels;
using Assignment.Repository;
using Assignment.Repository.Interfaces;
using Moq;

namespace DemoApp.Test.MockResources
{
    public class MovieMock
    {
        /// <summary>
        /// See how we are using Moq - https://github.com/moq/moq4
        /// </summary>
        
 
        public static readonly Mock<IMovieRepository> MovieRepoMock = new Mock<IMovieRepository>();

        private static readonly List<Movie> ListOfMovie = new List<Movie>
        {
            new Movie
            {
                Id = 1,
                Name = "M1",
                Plot = "--",
                CoverImage = "--",
                YearOfRelease = 2012,
                ProducerId = 1
            },
             new Movie
            {
                Id = 2,
                Name = "M2",
                Plot = "--",
                 CoverImage = "--",
                YearOfRelease = 2017,
                ProducerId = 2
            }
        };

        public static void MockGetAll()
        {
            MovieRepoMock.Setup(x => x.Get()).Returns(ListOfMovie.ToList());
        }

        public static void MockGet()
        {
            MovieRepoMock.Setup(x => x.Get(It.Is<int>(
             i => ListOfMovie.FirstOrDefault
                 (movie => movie.Id == i) != null)
        )).Returns(ListOfMovie.First(x => x.Id == 1));
        }

        public static void MockDelete()
        {
            // i is representing jo parameter me aane wala hai delete() function ke
            //if the function provided to delete is returning true when the return value that we have specified will be returned
            //otherwise the default value will be returned in this case is 0
            MovieRepoMock.Setup(x => x.Delete(It.Is<int>(
              i => ListOfMovie.FirstOrDefault
                  (movie => movie.Id == i) != null)
         )).Returns(2);
        }

        public static void MockCreate()
        {
            MovieRepoMock.Setup(x => x.Create(It.IsAny<Movie>(),It.IsAny<string>(),It.IsAny<string>())).Returns(2);
        }

        public static void MockUpdate()
        {
            MovieRepoMock.Setup(x => x.Update(It.Is<int>(
              i => ListOfMovie.FirstOrDefault
                  (actor => actor.Id == i) != null), It.IsAny<Movie>())).Returns(2);
        }
    }
}