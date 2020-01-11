using openSDesk.API.Models;
using Microsoft.EntityFrameworkCore;

namespace openSDesk.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserPhoto> UserPhotos { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<SubStatus> SubStatuses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Attachement> Attachements { get; set; }
        public DbSet<Resolution> Resolutions { get; set; }
        public DbSet<ResolutionCode> ResolutionCodes { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyResponse> SurvayResponses { get; set; }
        public DbSet<UserGroupAssingment> UserGroupAssingments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(u => u.AssignedTo)
                .WithMany(t => t.TicketsAssigned)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(u => u.Requester)
                .WithMany(t => t.TicketsCreated)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserGroupAssingment>()
                .HasKey(ugf => new { ugf.UserId, ugf.UserGroupId });

            modelBuilder.Entity<UserGroupAssingment>()
                .HasOne(ugf => ugf.User)
                .WithMany(u => u.Groups)
                .HasForeignKey(ugf => ugf.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserGroupAssingment>()
                .HasOne(ugf => ugf.UserGroup)
                .WithMany(g => g.Users)
                .HasForeignKey(ugf => ugf.UserGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}