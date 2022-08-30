using CustomerDatalayer.Entities;
using CustomerDatalayer.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace CustomerDatalayer.Repositories
{
    public class CustomerNoteRepository : BaseRepository, IRepository<CustomerNote>
    {
        public CustomerNote Create(CustomerNote address)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new(
                    "INSERT INTO [Notes] (CustomerId, Note) " +
                    "OUTPUT INSERTED.[CustomerId], INSERTED.[Note] " +
                    "VALUES (@CustomerId, @Note)", connection);

                command.Parameters.AddRange(new[] {
                    new SqlParameter("@CustomerId", SqlDbType.Int) { Value = address.CustomerId },
                    new SqlParameter("@Note", SqlDbType.NVarChar, 100) { Value = address.Note },
                });

                var reader = command.ExecuteReader();
                reader.Read();

                return new CustomerNote(reader);
            }
        }

        public CustomerNote Read(int addressId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new("SELECT * FROM [Notes] WHERE CustomerId = @CustomerId", connection);

                command.Parameters.Add(new SqlParameter("@CustomerId", SqlDbType.Int) { Value = addressId });

                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new CustomerNote(reader);
                }
            }

            return null;
        }

        public int Update(CustomerNote address)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new(
                "UPDATE [Notes] " +
                "SET " +
                    "CustomerId = @CustomerId, " +
                    "Note = @Note " +
                "WHERE CustomerId = @CustomerId", connection);

                command.Parameters.AddRange(new[] {
                    new SqlParameter("@CustomerId", SqlDbType.Int) { Value = address.CustomerId },
                    new SqlParameter("@Note", SqlDbType.NVarChar, 100) { Value = address.Note },
                });

                return command.ExecuteNonQuery();
            }
        }

        public int Delete(int customerId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM [Notes] WHERE CustomerId = @CustomerId", connection);

                command.Parameters.Add(new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customerId });

                return command.ExecuteNonQuery();
            }
        }

        public void DeleteAll()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = new SqlCommand(
                    "DELETE FROM [Notes]",
                    connection);
                command.ExecuteNonQuery();
            }
        }
    }
}