using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace viecLam24hBE.Migrations
{
    public partial class CreateDBAG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTypeId = table.Column<int>(type: "int", nullable: true),
                    Salary = table.Column<double>(type: "float", nullable: true),
                    WorkingForm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CareerGoals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InformationTechnology = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: true),
                    NameReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PositionReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CV_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    JobTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    Experence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkingForm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinAge = table.Column<int>(type: "int", nullable: false),
                    MaxAge = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobPosts_JobTypes_JobTypeId",
                        column: x => x.JobTypeId,
                        principalTable: "JobTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobPosts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: true),
                    JobPostId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSave = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplications_ApplicantProfiles_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "ApplicantProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobApplications_JobPosts_JobPostId",
                        column: x => x.JobPostId,
                        principalTable: "JobPosts",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "JobTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Hành chính - Thư Kí" },
                    { 2, "Khách sạn - Nhà hàng - Du lịch" },
                    { 3, "Bán buôn - Bán lẻ - Quản lý cửa hàng" },
                    { 4, "Marketing" },
                    { 5, "Kinh doanh" },
                    { 6, "Kế toán" },
                    { 7, "Tài chính - Đầu tư" },
                    { 8, "Tài chính - Đầu tư" },
                    { 9, "Kiểm toán - Khoa học kĩ thuật" },
                    { 10, "Nghề nghiệp khác" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 22, 11, 0, 55, 925, DateTimeKind.Local).AddTicks(7819), "Nhà tuyển dụng nhân sự cho các công ty", "Người tuyển dụng" },
                    { 2, new DateTime(2024, 2, 22, 11, 0, 55, 925, DateTimeKind.Local).AddTicks(7820), "Người tìm việc làm thông qua các bài đăng từ người tuyển dụng", "Người tìm việc" },
                    { 3, new DateTime(2024, 2, 22, 11, 0, 55, 925, DateTimeKind.Local).AddTicks(7821), "Admin", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "Address", "Avatar", "CompanyName", "CreatedAt", "Dob", "Email", "Password", "Phone", "RoleId", "Sex", "UserName" },
                values: new object[] { 1, true, "Hà Nội", null, "Tổng Công Ty Xây Dựng Công trình Giao Thông 6 - Công Ty Cổ Phần", new DateTime(2024, 2, 22, 11, 0, 55, 925, DateTimeKind.Local).AddTicks(7800), new DateTime(2001, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hieunv@gmail.com", "123", "0967755934", 1, "0", "Nguyễn Văn Hiếu" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "Address", "Avatar", "CompanyName", "CreatedAt", "Dob", "Email", "Password", "Phone", "RoleId", "Sex", "UserName" },
                values: new object[] { 2, true, "Quảng Ninh", null, "Công Ty Cổ Phần Dịch Vụ Thương mại Tổng Hợp VinCommerce", new DateTime(2024, 2, 22, 11, 0, 55, 925, DateTimeKind.Local).AddTicks(7807), new DateTime(2008, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chint@gmail.com", "123", null, 2, "1", "Nguyễn Thùy Chi" });

            migrationBuilder.InsertData(
                table: "ApplicantProfiles",
                columns: new[] { "Id", "CV_name", "CareerGoals", "CompanyReference", "Degree", "Experence", "InformationTechnology", "IsPublic", "JobName", "JobTypeId", "NameReference", "PhoneReference", "PositionReference", "Salary", "Skills", "UserId", "WorkLocation", "WorkingForm" },
                values: new object[] { 1, null, "Thống trị thế giới", null, "2", "4", null, true, "CEO", 3, null, null, null, 3000.0, null, 1, "Hà Nội", "1" });

            migrationBuilder.InsertData(
                table: "ApplicantProfiles",
                columns: new[] { "Id", "CV_name", "CareerGoals", "CompanyReference", "Degree", "Experence", "InformationTechnology", "IsPublic", "JobName", "JobTypeId", "NameReference", "PhoneReference", "PositionReference", "Salary", "Skills", "UserId", "WorkLocation", "WorkingForm" },
                values: new object[] { 2, null, "Trở nên chuyên nghiệp hơn trong ngành Marketing", null, "3", "3", null, false, "Marketing", 4, null, null, null, 5000.0, null, 1, "Hà Nội", "2" });

            migrationBuilder.InsertData(
                table: "JobApplications",
                columns: new[] { "Id", "ApplicantId", "CreatedAt", "IsSave", "JobPostId", "ProfileName", "Status" },
                values: new object[] { 1, 1, new DateTime(2024, 2, 16, 11, 0, 55, 925, DateTimeKind.Local).AddTicks(7368), null, 11, "Hồ sơ ứng tuyển vị trí CEO", true });

            migrationBuilder.InsertData(
                table: "JobApplications",
                columns: new[] { "Id", "ApplicantId", "CreatedAt", "IsSave", "JobPostId", "ProfileName", "Status" },
                values: new object[] { 2, 2, new DateTime(2024, 2, 10, 11, 0, 55, 925, DateTimeKind.Local).AddTicks(7383), null, 11, "Hồ sơ ứng tuyển vị trí Marketing", false });

            migrationBuilder.InsertData(
                table: "JobApplications",
                columns: new[] { "Id", "ApplicantId", "CreatedAt", "IsSave", "JobPostId", "ProfileName", "Status" },
                values: new object[] { 3, 1, new DateTime(2024, 2, 20, 11, 0, 55, 925, DateTimeKind.Local).AddTicks(7385), null, 12, "Hồ sơ ứng tuyển vị trí Junior", false });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantProfiles_UserId",
                table: "ApplicantProfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_ApplicantId",
                table: "JobApplications",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobPostId",
                table: "JobApplications",
                column: "JobPostId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPosts_JobTypeId",
                table: "JobPosts",
                column: "JobTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPosts_UserId",
                table: "JobPosts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobApplications");

            migrationBuilder.DropTable(
                name: "ApplicantProfiles");

            migrationBuilder.DropTable(
                name: "JobPosts");

            migrationBuilder.DropTable(
                name: "JobTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
