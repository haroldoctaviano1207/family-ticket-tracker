using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HO.FamilyTicketTracker.API.Models;

namespace HO.FamilyTicketTracker.API.Data
{
  public class ApplicationDbContext : IdentityDbContext<User>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<LookUpValue> LookupValues { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<Ticket>()
          .HasOne(t => t.Assignee)
          .WithMany(u => u.AssignedTickets)
          .HasForeignKey(t => t.AssigneeId)
          .OnDelete(DeleteBehavior.Restrict);

      builder.Entity<Ticket>()
          .HasOne(t => t.CreatedBy)
          .WithMany(u => u.CreatedTickets)
          .HasForeignKey(t => t.CreatedById)
          .OnDelete(DeleteBehavior.Restrict);

      builder.Entity<Comment>()
          .HasOne(c => c.User)
          .WithMany(u => u.Comments)
          .HasForeignKey(c => c.UserId)
          .OnDelete(DeleteBehavior.Cascade);

      builder.Entity<Comment>()
          .HasOne(c => c.Ticket)
          .WithMany(t => t.Comments)
          .HasForeignKey(c => c.TicketId)
          .OnDelete(DeleteBehavior.Cascade);

      builder.Entity<LookUpValue>()
          .HasIndex(lv => new { lv.CategoryType, lv.Code })
          .IsUnique();

      // RoleType
      builder.Entity<LookUpValue>().HasData(
          new LookUpValue { Id = 1, CategoryType = "RoleType", Code = "CHILD", DisplayName = "Child", SortOrder = 1 },
          new LookUpValue { Id = 2, CategoryType = "RoleType", Code = "PARENT", DisplayName = "Parent", SortOrder = 2 }
      );

      // TicketPriority
      builder.Entity<LookUpValue>().HasData(
          new LookUpValue { Id = 3, CategoryType = "TicketPriority", Code = "LOW", DisplayName = "Low", SortOrder = 1 },
          new LookUpValue { Id = 4, CategoryType = "TicketPriority", Code = "MEDIUM", DisplayName = "Medium", SortOrder = 2 },
          new LookUpValue { Id = 5, CategoryType = "TicketPriority", Code = "HIGH", DisplayName = "HIGH", SortOrder = 3 }
      );

      // TicketCategory
      builder.Entity<LookUpValue>().HasData(
          new LookUpValue { Id = 6, CategoryType = "TicketCategory", Code = "GENERAL", DisplayName = "General", SortOrder = 1 },
          new LookUpValue { Id = 7, CategoryType = "TicketCategory", Code = "CHORES", DisplayName = "Chores", SortOrder = 2 },
          new LookUpValue { Id = 8, CategoryType = "TicketCategory", Code = "MAINTENANCE", DisplayName = "Maintenance", SortOrder = 3 },
          new LookUpValue { Id = 9, CategoryType = "TicketCategory", Code = "HOMEWORK_HELP", DisplayName = "Homework Help", SortOrder = 4 },
          new LookUpValue { Id = 10, CategoryType = "TicketCategory", Code = "SHOPPING", DisplayName = "Shopping", SortOrder = 5 },
          new LookUpValue { Id = 11, CategoryType = "TicketCategory", Code = "CLEANING", DisplayName = "Cleaning", SortOrder = 6 },
          new LookUpValue { Id = 12, CategoryType = "TicketCategory", Code = "COOKING", DisplayName = "Cooking", SortOrder = 7 },
          new LookUpValue { Id = 13, CategoryType = "TicketCategory", Code = "YARD_WORK", DisplayName = "Yard Work", SortOrder = 8 },
          new LookUpValue { Id = 14, CategoryType = "TicketCategory", Code = "PET_CARE", DisplayName = "Pet Care", SortOrder = 9 },
          new LookUpValue { Id = 15, CategoryType = "TicketCategory", Code = "OTHER", DisplayName = "Other", SortOrder = 10 }
      );

      // TicketStatus
      builder.Entity<LookUpValue>().HasData(
          new LookUpValue { Id = 16, CategoryType = "TicketStatus", Code = "OPEN", DisplayName = "Open", SortOrder = 1 },
          new LookUpValue { Id = 17, CategoryType = "TicketStatus", Code = "IN_PROGRESS", DisplayName = "In Progress", SortOrder = 2 },
          new LookUpValue { Id = 18, CategoryType = "TicketStatus", Code = "PENDING_REVIEW", DisplayName = "Pending Review", SortOrder = 3 },
          new LookUpValue { Id = 19, CategoryType = "TicketStatus", Code = "COMPLETED", DisplayName = "Completed", SortOrder = 4 },
          new LookUpValue { Id = 20, CategoryType = "TicketStatus", Code = "CANCELLED", DisplayName = "Cancelled", SortOrder = 5 }
      );
    }
  }
}
