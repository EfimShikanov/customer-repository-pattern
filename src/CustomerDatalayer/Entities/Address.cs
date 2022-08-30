using System.Data.SqlClient;

namespace CustomerDatalayer.Entities
{
    public class Address
    {
        public int AddressId { get; set; } = -1;
        public int CustomerId { get; set; } = -1;
        public string AddressLine { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; }
        public string AddressType { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public Address() { }
        public Address(SqlDataReader reader)
        {
            AddressId = (int)reader["AddressId"];
            CustomerId = (int)reader["CustomerId"];
            AddressLine = (string)reader["AddressLine"];
            AddressLine2 = (string)reader["AddressLine2"];
            AddressType = (string)reader["AddressType"];
            City = (string)reader["City"];
            PostalCode = (string)reader["PostalCode"];
            State = (string)reader["State"];
            Country = (string)reader["Country"];
        }
    }
}
