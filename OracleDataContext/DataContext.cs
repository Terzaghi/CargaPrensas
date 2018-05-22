using Oracle.ManagedDataAccess.Client;

namespace OracleDataContext
{
    public class DataContext : System.IDisposable
    {
        public OracleConnection Context { get; private set; }

        public string User { get; private set; }
        public string Password { get; private set; }
        public string Source { get; private set; }

        public string ConnectionString { get; private set; }

        public DataContext(string pConnectionString)
        {
            ConnectionString = pConnectionString;

            Context = new OracleConnection(pConnectionString);

            var builder = new OracleConnectionStringBuilder(Context.ConnectionString);

            User = builder.UserID;
            Password = builder.Password;
            Source = builder.DataSource;

            Context.Open();
        }

        public void Dispose()
        {
            Context.Close();
        }
    }
}
