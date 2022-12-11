using DbUp.Postgresql;
using DbUp;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace SMITBron.DBMigrations
{
    public class DBMigrations
    {
        public static void Run(string connectionString, ILogger logger)
        {
            var up = DeployChanges.To
                    .PostgresqlDatabase(connectionString).WithTransactionPerScript()
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), x => { return x.EndsWith(".psql"); })
                    .LogTo(new DbUpLogger(logger));

            up.Configure(c => c.Journal = new PostgresqlTableJournal(() => c.ConnectionManager, () => c.Log, "public", "schemaversions"));

            var upgrader = up.Build();
            
            var result = upgrader.PerformUpgrade();

            if (result.Successful == false)
            {
                throw result.Error;
            }
        }

    }
}