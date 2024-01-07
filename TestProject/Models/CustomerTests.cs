using System;
using System.Collections.Generic;
using Xunit;
using Hotel.Domain.Model;
using Hotel.Domain.Exceptions;

namespace TestProject.Models
{
    public class CustomerTests
    {
        [Fact]
        public void AddMember_ValidMember_ShouldAddMemberToList()
        {
            // Arrange
            var customer = new Customer();
            var member = new Member("John Doe", new DateOnly(1990, 1, 1));

            // Act
            customer.AddMember(member);

            // Assert
            Assert.Contains(member, customer.GetMembers());
        }

        [Fact]
        public void AddMember_DuplicateMember_ShouldThrowCustomerException()
        {
            // Arrange
            var customer = new Customer();
            var member = new Member("John Doe", new DateOnly(1990, 1, 1));

            // Act
            customer.AddMember(member);

            // Assert
            Assert.Throws<CustomerException>(() => customer.AddMember(member));
        }

        [Fact]
        public void RemoveMember_ExistingMember_ShouldRemoveMemberFromList()
        {
            // Arrange
            var customer = new Customer();
            var member = new Member("John Doe", new DateOnly(1990, 1, 1));
            customer.AddMember(member);

            // Act
            customer.RemoveMember(member);

            // Assert
            Assert.DoesNotContain(member, customer.GetMembers());
        }

        [Fact]
        public void RemoveMember_NonExistingMember_ShouldThrowCustomerException()
        {
            // Arrange
            var customer = new Customer();
            var member = new Member("John Doe", new DateOnly(1990, 1, 1));

            // Act & Assert
            Assert.Throws<CustomerException>(() => customer.RemoveMember(member));
        }

        [Fact]
        public void GetMembers_EmptyList_ShouldReturnEmptyList()
        {
            // Arrange
            var customer = new Customer();

            // Act
            var members = customer.GetMembers();

            // Assert
            Assert.Empty(members);
        }

        [Fact]
        public void GetMembers_NonEmptyList_ShouldReturnCorrectMembers()
        {
            // Arrange
            var customer = new Customer();
            var member1 = new Member("John Doe", new DateOnly(1990, 1, 1));
            var member2 = new Member("Jane Doe", new DateOnly(1985, 5, 15));

            customer.AddMember(member1);
            customer.AddMember(member2);

            // Act
            var members = customer.GetMembers();

            // Assert
            Assert.Equal(2, members.Count);
            Assert.Contains(member1, members);
            Assert.Contains(member2, members);
        }
    }
}
