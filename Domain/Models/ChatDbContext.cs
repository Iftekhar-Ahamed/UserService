using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

public partial class ChatDbContext : DbContext
{
    public ChatDbContext()
    {
    }

    public ChatDbContext(DbContextOptions<ChatDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblUserChatFriend> TblUserChatFriends { get; set; }

    public virtual DbSet<TblUserChatFriendShipStatus> TblUserChatFriendShipStatuses { get; set; }

    public virtual DbSet<TblUserInformation> TblUserInformations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ChatDB;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblUserChatFriend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tbluserchatfriend_pk");

            entity.ToTable("TblUserChatFriend");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('tbl_user_chat_friend_id_seq'::regclass)");
            entity.Property(e => e.CreationDateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.FriendShipStatus).WithMany(p => p.TblUserChatFriends)
                .HasForeignKey(d => d.FriendShipStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbluserchatfriend_tbluserchatfriendshipstatus_id_fk");

            entity.HasOne(d => d.User).WithMany(p => p.TblUserChatFriends)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbluserchatfriend_tbluserinformation_userid_fk");
        });

        modelBuilder.Entity<TblUserChatFriendShipStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TblUserChatFriend_pkey");

            entity.ToTable("TblUserChatFriendShipStatus");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('tbl_user_chat_friend_ship_status_id_seq'::regclass)");
            entity.Property(e => e.ApproveStatus).HasDefaultValue((short)1);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<TblUserInformation>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("TblUserInformation_pkey");

            entity.ToTable("TblUserInformation");

            entity.Property(e => e.ContactNumber).HasMaxLength(15);
            entity.Property(e => e.ContactNumberCountryCode).HasMaxLength(3);
            entity.Property(e => e.CreationDateTime).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Email).HasMaxLength(320);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastModifiedDateTime).HasColumnType("timestamp without time zone");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(5);
        });
        modelBuilder.HasSequence("tbl_user_chat_friend_id_seq");
        modelBuilder.HasSequence("tbl_user_chat_friend_ship_status_id_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
