namespace Sxylo_Stock.Model
{
    public static class IISEXPRESS
    {
        #region Constants
        public const string DEFAULT_POLICY = "DEFAULT_POLICY";

        #endregion

        #region Public methods
        public static void AddCustomSecurity(this IServiceCollection services) 
        {
            services.AddCors(Options =>
            {
                Options.AddPolicy(DEFAULT_POLICY, builder =>
                {
                    builder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .WithOrigins("http://localhost:3000",  "http://192.168.100.10:3000");
                });
            });
        }

        #endregion
    }
}
