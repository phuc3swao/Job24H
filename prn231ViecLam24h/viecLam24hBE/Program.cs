
using AutoMapper;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using viecLam24hBE.Commons;
using viecLam24hBE.Configs;
using viecLam24hBE.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<MyDbContext>(opt =>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"))
        );
        builder.Services.AddScoped<UserService, UserServiceImpl>();
        builder.Services.AddScoped<UngTuyenService, UngTuyenServiceImpl>();
        builder.Services.AddScoped<JobTypeService, JobTypeServiceImpl>();
        builder.Services.AddScoped<JobPostService, JobPostServiceImpl>();
        builder.Services.AddScoped<JobApplicationService, JobApplicationServiceImpl>();
        builder.Services.AddScoped<ApplicantProfileService, ApplicantProfileServiceImpl>();
        builder.Services.AddAutoMapper(typeof(MapperConfig));


        builder.Services.AddControllers().AddOData(option => option.Select().Filter()
                .Count().OrderBy().Expand().SetMaxTop(100));
        builder.Services.AddCors();

        //var mapperConfig = new MapperConfiguration(mc =>
        //{
        //    mc.AddProfile(new MapperConfig());
        //});
        //IMapper mapper = mapperConfig.CreateMapper();
        //builder.Services.AddSingleton(mapper);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.UseCors(builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });

        app.Run();
    }
}