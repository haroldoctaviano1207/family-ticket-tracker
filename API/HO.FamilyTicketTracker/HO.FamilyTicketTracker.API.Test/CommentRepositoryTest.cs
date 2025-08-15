using HO.FamilyTicketTracker.API.Data;
using HO.FamilyTicketTracker.API.Models;
using HO.FamilyTicketTracker.API.Repository;
using Microsoft.EntityFrameworkCore;

namespace HO.FamilyTicketTracker.API.Test
{

  /// <summary>
  /// Sample unit tests for repository
  /// </summary>
  public class CommentRepositoryTest
  { 

    private ApplicationDbContext CreateInMemoryDbContext()
    {
      var options = new DbContextOptionsBuilder<ApplicationDbContext>()
          .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
          .Options;
      return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsCorrectCommentsForTicket_WithUserIncluded()
    {
      // Arrange
      using var context = CreateInMemoryDbContext();
      context.Users.AddRange(MockData.GetUsers());

      context.Comments.AddRange(MockData.GetComments());
      await context.SaveChangesAsync();

      var repository = new CommentRepository(context);
      int TESTID = 1;

      // Act
      var result = await repository.GetAllAsync(TESTID);

      // Assert
      Assert.Equal(2, result.Count());
      Assert.True(result.All(c => c.TicketId == TESTID));
      var commentsList = result.ToList();
      Assert.Equal(1, commentsList[0].Id);
      Assert.Equal(2, commentsList[1].Id);

      Assert.NotNull(commentsList[0].User);
      Assert.NotNull(commentsList[1].User);
      Assert.Equal("Harold", commentsList[0].User.FirstName);
      Assert.Equal("Pode", commentsList[1].User.FirstName);
    }
  }
}
