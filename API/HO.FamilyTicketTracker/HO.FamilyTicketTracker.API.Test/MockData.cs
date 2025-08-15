using HO.FamilyTicketTracker.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HO.FamilyTicketTracker.API.Test
{
  public class MockData
  {
    public static IEnumerable<User> GetUsers()
    {
      return new List<User> { new() { Id = "user1-id", FirstName = "Harold", Role = RoleType.Parent.ToString() },
                              new() { Id = "user2-id", FirstName = "Pode", Role = RoleType.Child.ToString() } };
    }

    public static IEnumerable<Comment> GetComments()
    {
      return new List<Comment> { new() { Id = 1, TicketId = 1, UserId = "user1-id", CreatedAt = DateTime.Now.AddHours(-1), Content = "Test Comment 1" },
                                 new() { Id = 2, TicketId = 1, UserId = "user2-id", CreatedAt = DateTime.Now },
                                 new() { Id = 3, TicketId = 2, UserId = "user1-id" } };
    }
  }
}
