using System;
using System.Collections.Generic;
using System.Linq;
using CustomerDatalayer.Entities;
using CustomerDatalayer.Repositories;
using FluentAssertions;
using CustomerDatalayer.Interfaces;

namespace CustomerDatalayer.Tests.Repositories
{
    public class AddressRepositoryTest
    {
        [Fact]
        public void ShouldBeAbleToCreateAddressRepository()
        {
            AddressRepository repository = new();
            repository.Should().NotBeNull();
        }

        [Fact]
        public void ShouldImplementIRepository()
        {
            AddressRepository repository = new();
            repository.GetType().GetInterfaces().Should().Contain((typeof(IRepository<Address>)));
        }

        [Fact]
        public void ShouldBeAbleToCreateAddress()
        {
            AddressRepositoryFixture.DeleteAll();

            AddressRepository repository = new();

            var customer = AddressRepositoryFixture.GetAddress();

            var createdAddress = repository.Create(customer);

            createdAddress.Should().NotBeNull();
            createdAddress.AddressLine.Should().Be(customer.AddressLine);
            createdAddress.AddressLine2.Should().Be(customer.AddressLine2);
            createdAddress.AddressType.Should().Be(customer.AddressType);
            createdAddress.City.Should().Be(customer.City);
            createdAddress.PostalCode.Should().Be(customer.PostalCode);
            createdAddress.State.Should().Be(customer.State);
            createdAddress.Country.Should().Be(customer.Country);
        }

        [Fact]
        public void ShouldBeAbleToReadAddress()
        {
            AddressRepositoryFixture.DeleteAll();

            AddressRepository repository = new();

            var customer = AddressRepositoryFixture.GetAddress();

            var createdAddress = repository.Create(customer);
            var readAddress = repository.Read(createdAddress.AddressId);

            readAddress.Should().NotBeNull();
            readAddress.AddressLine.Should().Be(customer.AddressLine);
            readAddress.AddressLine2.Should().Be(customer.AddressLine2);
            readAddress.AddressType.Should().Be(customer.AddressType);
            readAddress.City.Should().Be(customer.City);
            readAddress.PostalCode.Should().Be(customer.PostalCode);
            createdAddress.State.Should().Be(customer.State);
            createdAddress.Country.Should().Be(customer.Country);
        }

        [Fact]
        public void ShouldNotBeAbleToReadAddress()
        {
            AddressRepositoryFixture.DeleteAll();

            AddressRepository repository = new();
            var readAddress = repository.Read(0);

            readAddress.Should().BeNull();
        }

        [Fact]
        public void ShouldBeAbleToUpdateAddress()
        {
            AddressRepositoryFixture.DeleteAll();

            AddressRepository repository = new();

            var customer = AddressRepositoryFixture.GetAddress();

            var createdAddress = repository.Create(customer);

            createdAddress.AddressLine = "addressLine";
            repository.Update(createdAddress);

            var updatedAddress = repository.Read(createdAddress.AddressId);

            updatedAddress.Should().NotBeNull();
            updatedAddress.AddressLine.Should().Be("addressLine");
            updatedAddress.AddressLine2.Should().Be(createdAddress.AddressLine2);
            updatedAddress.AddressType.Should().Be(createdAddress.AddressType);
            updatedAddress.City.Should().Be(createdAddress.City);
            updatedAddress.PostalCode.Should().Be(createdAddress.PostalCode);
            createdAddress.State.Should().Be(customer.State);
            createdAddress.Country.Should().Be(customer.Country);
        }

        [Fact]
        public void ShouldNotBeAbleToUpdateAddress()
        {
            AddressRepositoryFixture.DeleteAll();

            AddressRepository repository = new();

            var customer = AddressRepositoryFixture.GetAddress();

            var createdAddress = repository.Create(customer);

            createdAddress.AddressId = 0;
            createdAddress.AddressLine = "Dolor St.";
            int updatedAddresss = repository.Update(createdAddress);

            updatedAddresss.Should().Be(0);
        }

        [Fact]
        public void ShouldBeAbleToDeleteAddress()
        {
            AddressRepositoryFixture.DeleteAll();

            AddressRepository repository = new();

            var customer = AddressRepositoryFixture.GetAddress();

            var createdAddress = repository.Create(customer);

            int deletedRows = repository.Delete(createdAddress.AddressId);

            deletedRows.Should().Be(1);
        }

        [Fact]
        public void ShouldNotBeAbleToDeleteAddress()
        {
            AddressRepositoryFixture.DeleteAll();

            AddressRepository repository = new();

            var customer = AddressRepositoryFixture.GetAddress();

            repository.Create(customer);

            int deletedRows = repository.Delete(0);

            deletedRows.Should().Be(0);
        }
    }

    public class AddressRepositoryFixture
    {
        public static void DeleteAll()
        {
            AddressRepository repository = new();
            repository.DeleteAll();
        }

        public static Address GetAddress()
        {
            CustomerRepositoryFixture.DeleteAll();
            CustomerRepository repository = new();
            var customer = CustomerRepositoryFixture.GetCustomer();
            var createdCustomer = repository.Create(customer);

            var address = new Address
            {
                CustomerId = createdCustomer.CustomerId,
                AddressLine = "Ipsum St.",
                AddressLine2 = "6",
                AddressType = "Billing",
                City = "Las-Vegas",
                PostalCode = "66666",
                State = "Nevada",
                Country = "United States"
            };

            return address;
        }
    }
}
