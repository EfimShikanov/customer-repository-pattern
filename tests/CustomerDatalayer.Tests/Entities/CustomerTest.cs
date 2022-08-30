using CustomerDatalayer.Entities;

namespace CustomerDatalayer.Tests.Entities
{
    public class CustomerTest
    {
        [Fact]
        public void ShouldBeAbleToCreateCustomer()
        {
            var customer = new Customer
            {
                FirstName = "Firstname",
                LastName = "Lastname",
                PhoneNumber = "0123456789",
                Email = "FirstLastname@mail.com",
                TotalPurchasesAmount = 0
            };

            Assert.NotNull(customer);
            Assert.Equal("Firstname", customer.FirstName);
            Assert.Equal("Lastname", customer.LastName);
            Assert.Equal("0123456789", customer.PhoneNumber);
            Assert.Equal("FirstLastname@mail.com", customer.Email);
            Assert.Equal(0, customer.TotalPurchasesAmount);
        }
    }
}