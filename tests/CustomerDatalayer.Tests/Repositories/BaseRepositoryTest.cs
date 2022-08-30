using CustomerDatalayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CustomerDatalayer.Tests.Repositories
{
    public class BaseRepositoryTest
    {
        [Fact]
        public void ShouldBeAbleToGetConnection()
        {
            var connection = BaseRepository.GetConnection();

            connection.Should().NotBeNull();
        }

        [Fact]
        public void ShouldBeAbleToOpenConnection()
        {
            var connection = BaseRepository.GetConnection();
            connection.Open();

            connection.State.Should().Be(ConnectionState.Open);
        }
    }
}
