﻿// <auto-generated />
using System;
using Jobsity.Chat.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Jobsity.Chat.DB.Migrations
{
    [DbContext(typeof(JobsityChatDataContext))]
    partial class JobsityChatDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Jobsity.Chat.Models.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(256)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("(sysutcdatetime())");

                    b.Property<Guid>("CreatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid?>("ReceiverId")
                        .HasMaxLength(256)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReceiverName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int?>("ReceiverType")
                        .HasColumnType("int");

                    b.Property<Guid?>("SenderUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("UpdatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UpdatedByName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Chat", (string)null);
                });

            modelBuilder.Entity("Jobsity.Chat.Models.ChatMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(256)
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ChatRoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("(sysutcdatetime())");

                    b.Property<Guid>("CreatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Read")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("ReadAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("UpdatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UpdatedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("ChatRoomId");

                    b.ToTable("ChatMessage", (string)null);
                });

            modelBuilder.Entity("Jobsity.Chat.Models.ChatRoom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(256)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("(sysutcdatetime())");

                    b.Property<Guid>("CreatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("UpdatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UpdatedByName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChatRoom", (string)null);
                });

            modelBuilder.Entity("Jobsity.Chat.Models.ChatRoomParticipant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(256)
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChatRoomId")
                        .HasMaxLength(256)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("(sysutcdatetime())");

                    b.Property<Guid>("CreatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("UpdatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UpdatedByName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasMaxLength(256)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChatRoomId");

                    b.ToTable("ChatRoomParticipant");
                });

            modelBuilder.Entity("Jobsity.Chat.Models.ChatMessage", b =>
                {
                    b.HasOne("Jobsity.Chat.Models.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jobsity.Chat.Models.ChatRoom", "ChatRoom")
                        .WithMany("Messages")
                        .HasForeignKey("ChatRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("ChatRoom");
                });

            modelBuilder.Entity("Jobsity.Chat.Models.ChatRoomParticipant", b =>
                {
                    b.HasOne("Jobsity.Chat.Models.ChatRoom", "ChatRoom")
                        .WithMany("Participants")
                        .HasForeignKey("ChatRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatRoom");
                });

            modelBuilder.Entity("Jobsity.Chat.Models.Chat", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Jobsity.Chat.Models.ChatRoom", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("Participants");
                });
#pragma warning restore 612, 618
        }
    }
}
