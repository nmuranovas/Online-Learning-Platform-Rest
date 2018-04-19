﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Project_OLP_Rest.Data;
using System;

namespace Project_OLP_Rest.Data.Migrations
{
    [DbContext(typeof(OlpContext))]
    [Migration("20180419162353_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Project_OLP_Rest.Domain.ChatBot", b =>
                {
                    b.Property<int>("ChatBotId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Link");

                    b.Property<string>("Name");

                    b.HasKey("ChatBotId");

                    b.ToTable("ChatBots");
                });

            modelBuilder.Entity("Project_OLP_Rest.Domain.ChatSession", b =>
                {
                    b.Property<int>("ChatSessionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChatBotId");

                    b.Property<string>("Data");

                    b.HasKey("ChatSessionId");

                    b.HasIndex("ChatBotId");

                    b.ToTable("ChatSessions");
                });

            modelBuilder.Entity("Project_OLP_Rest.Domain.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChatBotId");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("CourseId");

                    b.HasIndex("ChatBotId")
                        .IsUnique();

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Project_OLP_Rest.Domain.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("GroupId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Project_OLP_Rest.Domain.GroupCourse", b =>
                {
                    b.Property<int>("GroupId");

                    b.Property<int>("CourseId");

                    b.HasKey("GroupId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("GroupCourse");
                });

            modelBuilder.Entity("Project_OLP_Rest.Domain.Module", b =>
                {
                    b.Property<int>("ModuleId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseId");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("ModuleId");

                    b.HasIndex("CourseId");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("Project_OLP_Rest.Domain.Record", b =>
                {
                    b.Property<int>("RecordId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("ModuleId");

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.HasKey("RecordId");

                    b.HasIndex("ModuleId");

                    b.ToTable("Records");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Record");
                });

            modelBuilder.Entity("Project_OLP_Rest.Domain.TeacherCourse", b =>
                {
                    b.Property<int>("CourseId");

                    b.Property<int>("TeacherId");

                    b.HasKey("CourseId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherCourses");
                });

            modelBuilder.Entity("Project_OLP_Rest.Domain.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("Project_OLP_Rest.Domain.Exercise", b =>
                {
                    b.HasBaseType("Project_OLP_Rest.Domain.Record");

                    b.Property<string>("AnswerRegex");

                    b.Property<bool>("IsCompleted");

                    b.ToTable("Exercise");

                    b.HasDiscriminator().HasValue("Exercise");
                });

            modelBuilder.Entity("Project_OLP_Rest.Domain.Student", b =>
                {
                    b.HasBaseType("Project_OLP_Rest.Domain.User");

                    b.Property<int>("GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("Student");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("Project_OLP_Rest.Domain.Teacher", b =>
                {
                    b.HasBaseType("Project_OLP_Rest.Domain.User");


                    b.ToTable("Teacher");

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("Project_OLP_Rest.Domain.ChatSession", b =>
                {
                    b.HasOne("Project_OLP_Rest.Domain.ChatBot", "ChatBot")
                        .WithMany("ChatSessions")
                        .HasForeignKey("ChatBotId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project_OLP_Rest.Domain.Course", b =>
                {
                    b.HasOne("Project_OLP_Rest.Domain.ChatBot", "ChatBot")
                        .WithOne("Course")
                        .HasForeignKey("Project_OLP_Rest.Domain.Course", "ChatBotId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project_OLP_Rest.Domain.GroupCourse", b =>
                {
                    b.HasOne("Project_OLP_Rest.Domain.Course", "Course")
                        .WithMany("GroupCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project_OLP_Rest.Domain.Group", "Group")
                        .WithMany("GroupCourses")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project_OLP_Rest.Domain.Module", b =>
                {
                    b.HasOne("Project_OLP_Rest.Domain.Course", "Course")
                        .WithMany("Modules")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project_OLP_Rest.Domain.Record", b =>
                {
                    b.HasOne("Project_OLP_Rest.Domain.Module", "Module")
                        .WithMany("Records")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project_OLP_Rest.Domain.TeacherCourse", b =>
                {
                    b.HasOne("Project_OLP_Rest.Domain.Course", "Course")
                        .WithMany("TeacherCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project_OLP_Rest.Domain.Teacher", "Teacher")
                        .WithMany("TeacherCourses")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project_OLP_Rest.Domain.Student", b =>
                {
                    b.HasOne("Project_OLP_Rest.Domain.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
