using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HO.FamilyTicketTracker.API.Models;
using System.Reflection.Emit;

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

      builder.Entity<LookUpValue>()
          .Property(e => e.IsActive)
          .HasDefaultValue(true);

      builder.Entity<LookUpValue>()
          .Property(e => e.CreatedAt)
          .HasDefaultValue(DateTime.Now.ToUniversalTime());

      builder.Entity<LookUpValue>()
          .Property(e => e.UpdatedAt)
          .HasDefaultValue(DateTime.Now.ToUniversalTime());

      // RoleType
      builder.Entity<LookUpValue>().HasData(
          new LookUpValue { Id = 1, CategoryType = "RoleType", Code = "CHILD", IsActive = true, DisplayName = "Child", SortOrder = 1 },
          new LookUpValue { Id = 2, CategoryType = "RoleType", Code = "PARENT", IsActive = true, DisplayName = "Parent", SortOrder = 2 }
      );

      // TicketPriority
      builder.Entity<LookUpValue>().HasData(
          new LookUpValue { Id = 3, CategoryType = "TicketPriority", Code = "LOW", IsActive = true, DisplayName = "Low", SortOrder = 1 },
          new LookUpValue { Id = 4, CategoryType = "TicketPriority", Code = "MEDIUM", IsActive = true, DisplayName = "Medium", SortOrder = 2 },
          new LookUpValue { Id = 5, CategoryType = "TicketPriority", Code = "HIGH", IsActive = true, DisplayName = "HIGH", SortOrder = 3 }
      );

      // TicketCategory
      builder.Entity<LookUpValue>().HasData(
          new LookUpValue { Id = 6, CategoryType = "TicketCategory", Code = "GENERAL", IsActive = true, DisplayName = "General", SortOrder = 1 },
          new LookUpValue { Id = 7, CategoryType = "TicketCategory", Code = "CHORES", IsActive = true, DisplayName = "Chores", SortOrder = 2 },
          new LookUpValue { Id = 8, CategoryType = "TicketCategory", Code = "MAINTENANCE", IsActive = true, DisplayName = "Maintenance", SortOrder = 3 },
          new LookUpValue { Id = 9, CategoryType = "TicketCategory", Code = "HOMEWORK_HELP", IsActive = true, DisplayName = "Homework Help", SortOrder = 4 },
          new LookUpValue { Id = 10, CategoryType = "TicketCategory", Code = "SHOPPING", IsActive = true, DisplayName = "Shopping", SortOrder = 5 },
          new LookUpValue { Id = 11, CategoryType = "TicketCategory", Code = "CLEANING", IsActive = true, DisplayName = "Cleaning", SortOrder = 6 },
          new LookUpValue { Id = 12, CategoryType = "TicketCategory", Code = "COOKING", IsActive = true, DisplayName = "Cooking", SortOrder = 7 },
          new LookUpValue { Id = 13, CategoryType = "TicketCategory", Code = "YARD_WORK", IsActive = true, DisplayName = "Yard Work", SortOrder = 8 },
          new LookUpValue { Id = 14, CategoryType = "TicketCategory", Code = "PET_CARE", IsActive = true, DisplayName = "Pet Care", SortOrder = 9 },
          new LookUpValue { Id = 15, CategoryType = "TicketCategory", Code = "OTHER", IsActive = true, DisplayName = "Other", SortOrder = 10 }
      );

      // TicketStatus
      builder.Entity<LookUpValue>().HasData(
          new LookUpValue { Id = 16, CategoryType = "TicketStatus", Code = "OPEN", IsActive = true, DisplayName = "Open", SortOrder = 1 },
          new LookUpValue { Id = 17, CategoryType = "TicketStatus", Code = "IN_PROGRESS", IsActive = true, DisplayName = "In Progress", SortOrder = 2 },
          new LookUpValue { Id = 18, CategoryType = "TicketStatus", Code = "PENDING_REVIEW", IsActive = true, DisplayName = "Pending Review", SortOrder = 3 },
          new LookUpValue { Id = 19, CategoryType = "TicketStatus", Code = "COMPLETED", IsActive = true, DisplayName = "Completed", SortOrder = 4 },
          new LookUpValue { Id = 20, CategoryType = "TicketStatus", Code = "CANCELLED", IsActive = true, DisplayName = "Cancelled", SortOrder = 5 }
      );
    }
  }
}
