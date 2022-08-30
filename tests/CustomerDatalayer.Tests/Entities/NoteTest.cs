using CustomerDatalayer.Entities;

namespace CustomerDatalayer.Tests.Entities
{
    public class CustomerNoteTest
    {
        [Fact]
        public void ShouldBeAbleToCreateCustomerNote()
        {
            CustomerNote note = new CustomerNote
            {
                CustomerId = 1,
                Note = "note text"
            };

            Assert.NotNull(note);
            Assert.Equal(1, note.CustomerId);
            Assert.Equal("note text", note.Note);
        }
    }
}