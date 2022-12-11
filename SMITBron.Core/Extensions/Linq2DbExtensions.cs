using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITBron.Core.Extensions
{
    public static class Linq2DbExtensions
    {
        [LinqToDB.Sql.Expression(ProviderName.PostgreSQL, "COUNT(*) OVER()", ServerSideOnly = true)]
        public static int CountAllOver { get; set; }
    }
}
