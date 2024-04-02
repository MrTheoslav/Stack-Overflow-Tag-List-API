using Stack_Overflow_Tag_List_API.Interfaces;
using Stack_Overflow_Tag_List_API.Repository;
using Stack_Overflow_Tag_List_API.Services;

namespace Stack_Overflow_Tag_List_API
{
    public static class Extension
    {
        public static IServiceCollection AddInjection (this IServiceCollection services)
        {
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IOperations, Operations>();
            return services;
        }    
    }
}
