﻿using BulungurAcademy.Application.Services;
using BulungurAcademy.Application.Services.ExamApplicants;
using BulungurAcademy.Application.Services.Exams;
using BulungurAcademy.Application.Services.Subjects;
using BulungurAcademy.Application.Services.Users;
using BulungurAcademy.Core.Services;
using BulungurAcademy.Infrastructure.Contexts;
using BulungurAcademy.Infrastructure.Repositories.ExamApplicants;
using BulungurAcademy.Infrastructure.Repositories.Exams;
using BulungurAcademy.Infrastructure.Repositories.Subjects;
using BulungurAcademy.Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;

namespace BulungurAcademy.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDbContexts(
        this IServiceCollection services,
        WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");

        services.AddDbContextPool<AppDbContext>(options =>
        {
            if (builder.Environment.IsProduction())
            {
                options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.EnableRetryOnFailure();
                });
            }
            else
            {
                options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.EnableRetryOnFailure();
                });
            }
        });

        return services;
    }

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IExamApplicantRepository, ExamApplicantRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<IExamRepository, ExamRepository>();

        return services;
    }

    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IExamApplicantFatory, ExamApplicantFatory>();
        services.AddScoped<IExamApplicantService, ExamApplicantService>();
        services.AddScoped<IExamFactory, ExamFactory>();
        services.AddScoped<IExamService, ExamService>();
        services.AddScoped<ISubjectFactory, SubjectFactory>();
        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<IUserFactory, Userfactory>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }

    public static IServiceCollection AddTelegramBotClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string botApiKey = configuration
            .GetSection("TelegramBot:ApiKey").Value;

        services.AddSingleton<ITelegramBotClient, TelegramBotClient>(
            botClient => new TelegramBotClient(botApiKey));

        return services;
    }

    public static IServiceCollection AddUpdateHandeler(
        this IServiceCollection services)
    {
        services.AddScoped<UpdateHandler>();
        return services;
    }

    public static IServiceCollection AddControllerMappers(
       this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddNewtonsoftJson();

        services.AddEndpointsApiExplorer();

        return services;
    }
}
