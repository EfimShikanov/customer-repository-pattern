using CustomerDatalayer.Entities;
using CustomerDatalayer.Interfaces;
using CustomerDatalayer.Repositories;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDatalayer.Tests.Repositories
{
    public class NoteRepositoryTest
    {
        [Fact]
        public void ShouldBeAbleToCreateAddressRepository()
        {
            CustomerNoteRepository repository = new();
            repository.Should().NotBeNull();
        }

        [Fact]
        public void ShouldImplementIRepository()
        {
            CustomerNoteRepository repository = new();
            repository.GetType().GetInterfaces().Should().Contain((typeof(IRepository<CustomerNote>)));
        }

        [Fact]
        public void ShouldBeAbleToCreateAddress()
        {
            NoteRepositoryFixture.DeleteAll();

            CustomerNoteRepository repository = new();

            var note = NoteRepositoryFixture.GetAddress();

            var createdNote = repository.Create(note);

            createdNote.Should().NotBeNull();
            createdNote.CustomerId.Should().Be(note.CustomerId);
            createdNote.Note.Should().Be(note.Note);
        }

        [Fact]
        public void ShouldBeAbleToReadAddress()
        {
            NoteRepositoryFixture.DeleteAll();

            CustomerNoteRepository repository = new();

            var note = NoteRepositoryFixture.GetAddress();

            var createdNote = repository.Create(note);
            var readNote = repository.Read(createdNote.CustomerId);

            readNote.Should().NotBeNull();
            createdNote.CustomerId.Should().Be(note.CustomerId);
            createdNote.Note.Should().Be(note.Note);
        }

        [Fact]
        public void ShouldNotBeAbleToReadAddress()
        {
            NoteRepositoryFixture.DeleteAll();

            CustomerNoteRepository repository = new();
            var readNote = repository.Read(0);

            readNote.Should().BeNull();
        }

        [Fact]
        public void ShouldBeAbleToUpdateAddress()
        {
            NoteRepositoryFixture.DeleteAll();

            CustomerNoteRepository repository = new();

            var note = NoteRepositoryFixture.GetAddress();

            var createdNote = repository.Create(note);

            createdNote.Note = "addressLine";
            repository.Update(createdNote);

            var updatedNote = repository.Read(createdNote.CustomerId);

            updatedNote.Should().NotBeNull();
            createdNote.CustomerId.Should().Be(note.CustomerId);
            createdNote.Note.Should().Be("addressLine");
        }

        [Fact]
        public void ShouldNotBeAbleToUpdateAddress()
        {
            NoteRepositoryFixture.DeleteAll();

            CustomerNoteRepository repository = new();

            var customer = NoteRepositoryFixture.GetAddress();

            var createdNote = repository.Create(customer);

            createdNote.CustomerId = 0;
            createdNote.Note = "text";
            int updatedNote = repository.Update(createdNote);

            updatedNote.Should().Be(0);
        }

        [Fact]
        public void ShouldBeAbleToDeleteAddress()
        {
            NoteRepositoryFixture.DeleteAll();

            CustomerNoteRepository repository = new();

            var customer = NoteRepositoryFixture.GetAddress();

            var createdNote = repository.Create(customer);

            int deletedRows = repository.Delete(createdNote.CustomerId);

            deletedRows.Should().Be(1);
        }

        [Fact]
        public void ShouldNotBeAbleToDeleteAddress()
        {
            NoteRepositoryFixture.DeleteAll();

            CustomerNoteRepository repository = new();

            var customer = NoteRepositoryFixture.GetAddress();

            repository.Create(customer);

            int deletedRows = repository.Delete(0);

            deletedRows.Should().Be(0);
        }
    }

    public class NoteRepositoryFixture
    {
        public static void DeleteAll()
        {
            CustomerNoteRepository repository = new();
            repository.DeleteAll();
        }

        public static CustomerNote GetAddress()
        {
            CustomerRepositoryFixture.DeleteAll();
            CustomerRepository repository = new();
            var customer = CustomerRepositoryFixture.GetCustomer();
            var createdCustomer = repository.Create(customer);

            var address = new CustomerNote
            {
                CustomerId = createdCustomer.CustomerId,
                Note = "text"
            };

            return address;
        }
    }
}
