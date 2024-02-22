using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using viecLam24hBE.Models;

namespace viecLam24hBE.Commons
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {
                
        }

        public MyDbContext(DbContextOptions options) : base(options)
        {
               
        }

        #region DbSet
        public DbSet<ApplicantProfile> ApplicantProfiles { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

                optionsBuilder.UseSqlServer(config.GetConnectionString("DbContext"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ApplicantProfile>(u =>
            u.Property(p => p.Id).ValueGeneratedOnAdd()
            );


            modelBuilder.Entity<JobApplication>().HasData(
                new JobApplication
                {
                    Id = 1,
                    ApplicantId = 1,
                    ProfileName = "Hồ sơ ứng tuyển vị trí CEO",
                    Status = true,
                    JobPostId = 11,
                    CreatedAt = DateTime.Now.AddDays(-6),
                },
                                new JobApplication
                                {
                                    Id = 2,
                                    ApplicantId = 2,
                                    Status = false,
                                    ProfileName = "Hồ sơ ứng tuyển vị trí Marketing",
                                    JobPostId = 11,
                                    CreatedAt = DateTime.Now.AddDays(-12),
                                },
                                new JobApplication
                                {
                                    Id = 3,
                                    ApplicantId = 1,
                                    Status = false,
                                    ProfileName = "Hồ sơ ứng tuyển vị trí Junior",
                                    JobPostId = 12,
                                    CreatedAt = DateTime.Now.AddDays(-2),
                                }
           );

            modelBuilder.Entity<ApplicantProfile>().HasData(
                new ApplicantProfile
                {
                    Id = 1,
                    JobName = "CEO",
                    UserId = 1,
                    JobTypeId = 3,
                    Salary = 3000,
                    WorkingForm = "1",
                    Degree = "2",
                    Experence = "4",
                    WorkLocation = "Hà Nội",
                    CareerGoals = "Thống trị thế giới",
                    IsPublic = true,


                },
                new ApplicantProfile
                {
                    Id = 2,
                    JobName = "Marketing",
                    UserId = 1,
                    JobTypeId = 4,
                    Salary = 5000,
                    WorkingForm = "2",
                    Degree = "3",
                    Experence = "3",
                    WorkLocation = "Hà Nội",
                    CareerGoals = "Trở nên chuyên nghiệp hơn trong ngành Marketing",
                    IsPublic = false,


                }
                );

            modelBuilder.Entity<User>(u =>
                u.Property(p => p.Id).ValueGeneratedOnAdd()
                
            );
            modelBuilder.Entity<JobPost>(u =>
                u.Property(p => p.Id).ValueGeneratedOnAdd()
            );

            modelBuilder.Entity<JobType>(u =>
                u.Property(p => p.Id).ValueGeneratedOnAdd()
            );

            modelBuilder.Entity<JobType>().HasData(
                new JobType
                {
                    Id = 1,
                    Type = "Hành chính - Thư Kí"
                },
                new JobType
                {
                    Id = 2,
                    Type = "Khách sạn - Nhà hàng - Du lịch"
                },
                new JobType
                {
                    Id = 3,
                    Type = "Bán buôn - Bán lẻ - Quản lý cửa hàng"
                },
                new JobType
                {
                    Id = 4,
                    Type = "Marketing"
                },
                new JobType
                {
                    Id = 5,
                    Type = "Kinh doanh"
                },
                new JobType
                {
                    Id = 6,
                    Type = "Kế toán"
                },
                new JobType
                {
                    Id = 7,
                    Type = "Tài chính - Đầu tư"
                },
                new JobType
                {
                    Id = 8,
                    Type = "Tài chính - Đầu tư"
                },
                new JobType
                {
                    Id = 9,
                    Type = "Kiểm toán - Khoa học kĩ thuật"
                },
                new JobType
                {
                    Id = 10,
                    Type = "Nghề nghiệp khác"
                });

            modelBuilder.Entity<JobApplication>(u =>
                u.Property(p => p.Id).ValueGeneratedOnAdd()
                );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "Hieunv@gmail.com",
                    Password = "123",
                    CreatedAt = DateTime.Now,
                    UserName = "Nguyễn Văn Hiếu",
                    Active = true,
                    RoleId = 1,
                    Sex = "0",
                    Dob = new DateTime(2001, 11, 21),
                    Address = "Hà Nội",
                    Phone = "0967755934",
                    CompanyName = "Tổng Công Ty Xây Dựng Công trình Giao Thông 6 - Công Ty Cổ Phần"
                },
                    new User
                    {
                        Id = 2,
                        Email = "Chint@gmail.com",
                        Password = "123",
                        CreatedAt = DateTime.Now,
                        UserName = "Nguyễn Thùy Chi",
                        Active = true,
                        RoleId = 2,
                        Sex = "1",
                        Dob = new DateTime(2008, 7, 13),
                        Address = "Quảng Ninh",
                        CompanyName = "Công Ty Cổ Phần Dịch Vụ Thương mại Tổng Hợp VinCommerce"
                    }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Người tuyển dụng",
                    Description = "Nhà tuyển dụng nhân sự cho các công ty",
                    CreatedAt = DateTime.Now,
                },
                    new Role
                    {
                        Id = 2,
                        Name = "Người tìm việc",
                        Description = "Người tìm việc làm thông qua các bài đăng từ người tuyển dụng",
                        CreatedAt = DateTime.Now,
                    },
                    new Role
                    {
                        Id = 3,
                        Name = "Admin",
                        Description = "Admin",
                        CreatedAt = DateTime.Now,
                    }
            );

        }


    }
}

