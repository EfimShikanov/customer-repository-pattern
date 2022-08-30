using CustomerDatalayer.Entities;
using CustomerDatalayer.Interfaces;
using CustomerDatalayer.Repositories;
using CustomerDatalayer.Tests.Entities;
using FluentAssertions;

namespace CustomerDatalayer.Tests.Repositories
{
    public class CustomerRepositoryTest
    {
        [Fact]
        public void ShouldBeAbleToCreateCustomerRepository()
        {
            CustomerRepository repository = new();
            repository.Should().NotBeNull();
        }

        [Fact]
        public void ShouldImplementIRepository()
        {
            CustomerRepository repository = new();
            repository.GetType().GetInterfaces().Should().Contain((typeof(IRepository<Customer>)));
        }

        [Fact]
        public void ShouldBeAbleToCreateCustomer()
        {
            CustomerRepositoryFixture.DeleteAll();

            CustomerRepository repository = new();

            var customer = CustomerRepositoryFixture.GetCustomer();

            var createdCustomer = repository.Create(customer);

            createdCustomer.Should().NotBeNull();
            createdCustomer.FirstName.Should().Be(customer.FirstName);
            createdCustomer.LastName.Should().Be(customer.LastName);
            createdCustomer.PhoneNumber.Should().Be(customer.PhoneNumber);
            createdCustomer.Email.Should().Be(customer.Email);
            createdCustomer.TotalPurchasesAmount.Should().Be(customer.TotalPurchasesAmount);
        }

        [Fact]
        public void ShouldBeAbleToReadCustomer()
        {
            CustomerRepositoryFixture.DeleteAll();

            CustomerRepository repository = new();

            var customer = CustomerRepositoryFixture.GetCustomer();

            var createdCustomer = repository.Create(customer);
            var readCustomer = repository.Read(createdCustomer.CustomerId);

            readCustomer.Should().NotBeNull();
            readCustomer.FirstName.Should().Be(customer.FirstName);
            readCustomer.LastName.Should().Be(customer.LastName);
            readCustomer.PhoneNumber.Should().Be(customer.PhoneNumber);
            readCustomer.Email.Should().Be(customer.Email);
            readCustomer.TotalPurchasesAmount.Should().Be(customer.TotalPurchasesAmount);
        }

        [Fact]
        public void ShouldNotBeAbleToReadCustomer()
        {
            CustomerRepositoryFixture.DeleteAll();

            CustomerRepository repository = new();
            var readCustomer = repository.Read(0);

            readCustomer.Should().BeNull();
        }

        [Fact]
        public void ShouldBeAbleToUpdateCustomer()
        {
            CustomerRepositoryFixture.DeleteAll();

            CustomerRepository repository = new();

            var customer = CustomerRepositoryFixture.GetCustomer();

            var createdCustomer = repository.Create(customer);

            createdCustomer.FirstName = "Garry";
            repository.Update(createdCustomer);

            var updatedCustomer = repository.Read(createdCustomer.CustomerId);

            updatedCustomer.Should().NotBeNull();
            updatedCustomer.FirstName.Should().Be("Garry");
            updatedCustomer.LastName.Should().Be(createdCustomer.LastName);
            updatedCustomer.PhoneNumber.Should().Be(createdCustomer.PhoneNumber);
            updatedCustomer.Email.Should().Be(createdCustomer.Email);
            updatedCustomer.TotalPurchasesAmount.Should().Be(createdCustomer.TotalPurchasesAmount);
        }

        [Fact]
        public void ShouldNotBeAbleToUpdateCustomer()
        {
            CustomerRepositoryFixture.DeleteAll();

            CustomerRepository repository = new();

            var customer = CustomerRepositoryFixture.GetCustomer();

            var createdCustomer = repository.Create(customer);

            createdCustomer.CustomerId = 0;
            createdCustomer.FirstName = "Garry";
            int updatedCustomers = repository.Update(createdCustomer);

            updatedCustomers.Should().Be(0);
        }

        [Fact]
        public void ShouldBeAbleToDeleteCustomer()
        {
            CustomerRepositoryFixture.DeleteAll();

            CustomerRepository repository = new();

            var customer = CustomerRepositoryFixture.GetCustomer();

            var createdCustomer = repository.Create(customer);

            int deletedRows = repository.Delete(createdCustomer.CustomerId);

            deletedRows.Should().Be(1);
        }

        [Fact]
        public void ShouldNotBeAbleToDeleteCustomer()
        {
            CustomerRepositoryFixture.DeleteAll();

            CustomerRepository repository = new();

            var customer = CustomerRepositoryFixture.GetCustomer();

            repository.Create(customer);

            int deletedRows = repository.Delete(0);

            deletedRows.Should().Be(0);
        }
    }

    public class CustomerRepositoryFixture
    {
        public static void DeleteAll()
        {
            var repository = new CustomerRepository();
            repository.DeleteAll();
        }

        public static Customer GetCustomer()
        {
            var customer = new Customer
            {
                FirstName = "Harold",
                LastName = "Johnson",
                PhoneNumber = "12673935933",
                Email = "HaroldSJohnson@armyspy.com",
                TotalPurchasesAmount = 0
            };

            return customer;
        }
    }
}