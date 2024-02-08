using Npgsql;

namespace infrastructure
{
    public static class Utilities
    {
        public static readonly Uri Uri;
        public static readonly string ProperlyFormattedConnectionString;

        static Utilities()
        {
            string rawConnectionString;
            string envKeyName = "pgconn";
            
            rawConnectionString = Environment.GetEnvironmentVariable(envKeyName)!;
            if(rawConnectionString == null)
            {
                throw new Exception("Something is wrong with your pgconn!");
            }

            try
            {
                Uri = new Uri(rawConnectionString);
                ProperlyFormattedConnectionString = string.Format(
                "Server={0};Database={1};User Id={2};Password={3};Port={4};Pooling=true;MaxPoolSize=3",
                Uri.Host,
                Uri.AbsolutePath.Trim('/'),
                Uri.UserInfo.Split(':')[0],
                Uri.UserInfo.Split(':')[1],
                Uri.Port > 0 ? Uri.Port : 5432);
            new NpgsqlDataSourceBuilder(ProperlyFormattedConnectionString).Build().OpenConnection().Close();
            }
            catch(Exception e)
            {
                throw new Exception("Your connection cannot be found or used." +
                 " Are you sure you are using PosgradeSQl", e);
            }
        }
    }
}