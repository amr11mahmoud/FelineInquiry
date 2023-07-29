using FelineInquiry.Core.Models.Entities.Users;
using FelineInquiry.Test.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Test
{
    public class UsersRepositoryTests
    {
        [Fact]
        public void GetUserById_UserAlreadyExist_ShouldBeActive()
        {
            // Arrange
            UserTestDataRepository dataRepository = new UserTestDataRepository();

            // Act
            // active - cbf6db3b-c4ee-46aa-9457-5fa8aefef33a
            // inactive 844e14ce-c055-49e9-9610-855669c9859b
            User? user = dataRepository.GetById(Guid.Parse("cbf6db3b-c4ee-46aa-9457-5fa8aefef33a"));

            //Assert

            Assert.True(user?.IsActive);
        }

        [Fact]
        public void GetUserById_UserAlreadyExist_ShouldBeInActive()
        {
            // Arrange
            UserTestDataRepository dataRepository = new UserTestDataRepository();

            // Act
            // active - cbf6db3b-c4ee-46aa-9457-5fa8aefef33a
            // inactive 844e14ce-c055-49e9-9610-855669c9859b
            User? user = dataRepository.GetById(Guid.Parse("844e14ce-c055-49e9-9610-855669c9859b"));

            //Assert

            Assert.False(user?.IsActive);
        }

        [Fact]
        public void GetUserById_NotSureIfUserExist_UserCanBeNull()
        {
            // Arrange
            UserTestDataRepository dataRepository = new UserTestDataRepository();

            // Act
            // active - cbf6db3b-c4ee-46aa-9457-5fa8aefef33a
            // inactive 844e14ce-c055-49e9-9610-855669c9859b
            // null 844e14ce-c055-4723-9610-855669c9859b
            User? user = dataRepository.GetById(Guid.Parse("844e14ce-c055-4723-9610-855669c9859b"));

            //Assert

            Assert.True(user == null);
        }

        [Fact]
        public async Task GetUserById_NotSureIfUserExist_UserCanBeNull_Async()
        {
            // Arrange
            UserTestDataRepository dataRepository = new UserTestDataRepository();

            // Act
            // active - cbf6db3b-c4ee-46aa-9457-5fa8aefef33a
            // inactive 844e14ce-c055-49e9-9610-855669c9859b
            // null 844e14ce-c055-4723-9610-855669c9859b
            User? user = await dataRepository.GetByIdAsync(Guid.Parse("844e14ce-c055-4723-9610-855669c9859b"));

            //Assert

            Assert.True(user == null);
        }

        [Fact]
        public async Task DemoHowToAssertException_Async()
        {
            // Arrange

            // Act and assert

            //await Assert.ThrowsAsync<ExceptionType>( async () => { 
            // await FunctionWeWantToTestAsync
            //});
        }

        /// <summary>
        /// MISTAKE !!! Don't do it this way
        /// This test will always pass as you call an async assertion but don't wait until it gets the result
        /// which will always considered as success ... so Don't ever call an async assertion inside a non async unit test
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void DemoHowToAssertException_Mistake()
        {
            // Arrange

            // Act and assert

            //await Assert.ThrowsAsync<ExceptionType>( async () => { 
            // await FunctionWeWantToTestAsync
            //});
        }

        [Fact]
        public void Demo_HowToAssertEvent()
        {
            // Arrange

            // Act

            //Assert

        }

    }
}
