using CustomerDatalayer.Entities;
using CustomerDatalayer.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace CustomerDatalayer.Repositories
{
    public class AddressRepository : BaseRepository, IRepository<Address>
    {
        public Address Create(Address address)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new(
                    "INSERT INTO [Address] (CustomerId, AddressLine, AddressLine2, AddressType, City, PostalCode, State, Country) " +
                    "OUTPUT " +
                        "INSERTED.[AddressId], INSERTED.[CustomerId], INSERTED.[AddressLine], " +
                        "INSERTED.[AddressLine2], INSERTED.[AddressType], " +
                        "INSERTED.[City], INSERTED.[PostalCode], " +
                        "INSERTED.[State], INSERTED.[Country] " +
                    "VALUES (@CustomerId, @AddressLine, @AddressLine2, @AddressType, @City, @PostalCode, @State, @Country)", connection);

                command.Parameters.AddRange(new[] {
                    new SqlParameter("@CustomerId", SqlDbType.Int) { Value = address.CustomerId },
                    new SqlParameter("@AddressLine", SqlDbType.NVarChar, 100) { Value = address.AddressLine },
                    new SqlParameter("@AddressLine2", SqlDbType.NVarChar, 100) { Value = address.AddressLine2 },
                    new SqlParameter("@AddressType", SqlDbType.NVarChar, 20) { Value = address.AddressType },
                    new SqlParameter("@City", SqlDbType.NVarChar, 50) { Value = address.City },
                    new SqlParameter("@PostalCode", SqlDbType.NVarChar, 6) { Value = address.PostalCode },
                    new SqlParameter("@State", SqlDbType.NVarChar, 20) { Value = address.State },
                    new SqlParameter("@Country", SqlDbType.NVarChar, 20) { Value = address.Country },
                });

                var reader = command.ExecuteReader();
                reader.Read();

                return new Address(reader);
            }
        }

        public Address Read(int addressId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new("SELECT * FROM [Address] WHERE AddressId = @AddressId", connection);

                command.Parameters.Add(new SqlParameter("@AddressId", SqlDbType.Int) { Value = addressId });

                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Address(reader);
                }
            }

            return null;
        }

        public int Update(Address address)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new(
                "UPDATE [Address] " +
                "SET " +
                    "CustomerId = @CustomerId, " +
                    "AddressLine = @AddressLine, " +
                    "AddressLine2 = @AddressLine2, " +
                    "AddressType = @AddressType, " +
                    "City = @City, " +
                    "PostalCode = @PostalCode, " +
                    "State = @State, " +
                    "Country = @Country " +
                "WHERE AddressID = @AddressID", connection);

                command.Parameters.AddRange(new[] {
                    new SqlParameter("@CustomerId", SqlDbType.Int) { Value = address.CustomerId },
                    new SqlParameter("@AddressLine", SqlDbType.NVarChar, 100) { Value = address.AddressLine },
                    new SqlParameter("@AddressLine2", SqlDbType.NVarChar, 100) { Value = address.AddressLine2 },
                    new SqlParameter("@AddressType", SqlDbType.NVarChar, 20) { Value = address.AddressType },
                    new SqlParameter("@City", SqlDbType.NVarChar, 50) { Value = address.City },
                    new SqlParameter("@PostalCode", SqlDbType.NVarChar, 6) { Value = address.PostalCode },
                    new SqlParameter("@State", SqlDbType.NVarChar, 20) { Value = address.State },
                    new SqlParameter("@Country", SqlDbType.NVarChar, 20) { Value = address.Country },
                    new SqlParameter("@AddressID", SqlDbType.Int) { Value = address.AddressId },
                });

                return command.ExecuteNonQuery();
            }
        }

        public int Delete(int customerId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM [Address] WHERE AddressId = @AddressId", connection);

                command.Parameters.Add(new SqlParameter("@AddressId", SqlDbType.Int) { Value = customerId });

                return command.ExecuteNonQuery();
            }
        }

        public void DeleteAll()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = new SqlCommand(
                    "DELETE FROM [Address]",
                    connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
