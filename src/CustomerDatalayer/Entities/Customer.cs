using System.Data.SqlClient;

namespace CustomerDatalayer.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; } = -1;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal? TotalPurchasesAmount { get; set; } = 0;

        public Customer() { }
        public Customer(SqlDataReader reader)
        {
            CustomerId = (int)reader["CustomerId"];
            FirstName = (string)reader["FirstName"];
            LastName = (string)reader["LastName"];
            PhoneNumber = (string)reader["PhoneNumber"];
            Email = (string)reader["Email"];
            TotalPurchasesAmount = (decimal?)reader["TotalPurchasesAmount"];
        }
    }
}