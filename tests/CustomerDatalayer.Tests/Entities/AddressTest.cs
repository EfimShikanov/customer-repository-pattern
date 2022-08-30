using CustomerDatalayer.Entities;

namespace CustomerDatalayer.Tests.Entities
{
    public class AddressTest
    {
        [Fact]
        public void ShouldBeAbleToCreateAddress()
        {
            Address address = new Address
            {
                AddressLine = "Ipsum St.",
                AddressLine2 = "6",
                AddressType = "Billing",
                City = "Las-Vegas",
                PostalCode = "89049",
                State = "Nevada",
                Country = "United States"
            };

            Assert.NotNull(address);
            Assert.Equal("Ipsum St.", address.AddressLine);
            Assert.Equal("6", address.AddressLine2);
            Assert.Equal("Billing", address.AddressType);
            Assert.Equal("Las-Vegas", address.City);
            Assert.Equal("89049", address.PostalCode);
            Assert.Equal("Nevada", address.State);
            Assert.Equal("United States", address.Country);
        }
    }
}
