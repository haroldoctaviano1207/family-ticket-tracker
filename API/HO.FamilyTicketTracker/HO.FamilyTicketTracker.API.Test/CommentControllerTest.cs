using HO.FamilyTicketTracker.API.Controllers;
using HO.FamilyTicketTracker.API.Models.DTOs;
using HO.FamilyTicketTracker.API.Models;
using HO.FamilyTicketTracker.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HO.FamilyTicketTracker.API.Test
{
  /// <summary>
  /// Sample unit tests for Controller
  /// </summary>
  public class CommentControllerTests
  {
    private readonly Mock<ICommentRepository> _mockCommentRepository;
    private readonly CommentController _controller;

    public CommentControllerTests()
    {
      _mockCommentRepository = new Mock<ICommentRepository>();
      _controller = new CommentController(_mockCommentRepository.Object);
    }

    [Fact]
    public async Task GetComments_ReturnsOkWithComments_WhenRepositoryIsSuccessful()
    {
      var ticketId = 1;
      var mockComments = MockData.GetComments().Take(2);
      foreach (var comment in mockComments)
      {
        comment.User = MockData.GetUsers().FirstOrDefault(u => u.Id == comment.UserId);
        comment.Ticket = new Ticket { Id = ticketId, Title = "Test Ticket" };
        comment.CreatedAt = DateTime.UtcNow;
      }

      _mockCommentRepository.Setup(repo => repo.GetAllAsync(ticketId))
                            .ReturnsAsync(mockComments);

      var result = await _controller.GetComments(ticketId);

      var okResult = Assert.IsType<OkObjectResult>(result);

      var returnedComments = Assert.IsAssignableFrom<IEnumerable<CommentModel>>(okResult.Value);

      Assert.Equal(2, returnedComments.Count());
      Assert.Equal("Test Comment 1", returnedComments.First().Content);
    }
  }
}
