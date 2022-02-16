using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Univer.Service.Courses;
using Univer.Service.Groups;
using Univer.Service.Instructors;
using Univer.Service.Specials;
using Univer.Service.Students;

namespace Univer
{
    public static class DependencyInjector
    {
        public static IServiceCollection RegisterServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICourseService), typeof(CourseService));
            services.AddScoped(typeof(IStudentService), typeof(StudentService));
            services.AddScoped(typeof(IGroupService), typeof(GroupService));
            services.AddScoped(typeof(ISpecialService), typeof(SpecialService));
            services.AddScoped(typeof(IInstructorService), typeof(InstructorService));
            return services;
        }
    }
}
