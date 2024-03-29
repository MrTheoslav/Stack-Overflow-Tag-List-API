using Stack_Overflow_Tag_List_API.Interfaces;
using Stack_Overflow_Tag_List_API.Services;

namespace Stack_Overflow_Tag_List_API
{
    public static class Extension
    {
        public static IServiceCollection AddInjection (this IServiceCollection services)
        {
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IDatabaseTagService, DatabaseTagService>();
            return services;
        }    
    }
}
