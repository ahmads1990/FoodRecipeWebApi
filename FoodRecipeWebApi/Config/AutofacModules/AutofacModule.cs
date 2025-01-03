﻿using Autofac;
using AutoMapper;
using FoodRecipeWebApi.Data.Repo;
using FoodRecipeWebApi.Helpers;
using FoodRecipeWebApi.Services.Email;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace FoodRecipeWebApi.Config.AutofacModules;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DbContext>().InstancePerLifetimeScope();

        builder.RegisterType<EmailService>()
                    .As<IEmailSender>()
                    .InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        builder.Register(ctx => new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(Program).Assembly);
        }))
        .AsSelf()
        .SingleInstance();

        builder.Register(ctx =>
        {
            var config = ctx.Resolve<MapperConfiguration>();
            return config.CreateMapper(ctx.Resolve);
        })
        .As<IMapper>()
        .InstancePerLifetimeScope();
        builder.RegisterType<ImageHelper>().SingleInstance();

        builder.RegisterAssemblyTypes(typeof(Program).Assembly)
            .Where(c => c.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}
