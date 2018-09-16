using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Aon18.data.Context;
using Aon18.data.Interfaces;
using Aon18.data.Repository;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;

namespace Aon18.api.App_Start
{
    public class DependencyConfig
    {
        public static void RegisterDependencies()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            container.Register<ExamDbContext>(() => new ExamDbContext(), Lifestyle.Scoped);
            container.Register<IStudentDbRepository, StudentDbRepository>();
            container.Register<ISkeletDbRepository, SkeletDbRepository>(Lifestyle.Scoped);
            container.Register<IExamDbRepository, ExamDbRepository>(Lifestyle.Scoped);
            container.Verify();
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}