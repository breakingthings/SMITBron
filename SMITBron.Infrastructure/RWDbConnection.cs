using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB;
using Microsoft.Extensions.Logging;

namespace SMITBron.Infrastructure
{
    public class RWDbConnection : DataConnection
    {
        public RWDbConnection(LinqToDBConnectionOptions<RWDbConnection> options) : base(options)
        {
            
        }

    }
}