using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMITBron.Core.Entities
{
    [Table("guest")]
    public class Guest
    {
        [PrimaryKey]
        [Column("id", CanBeNull = false)]
        public Guid Id { get; set; }

        [Column("firstname", CanBeNull = false)]
        public string Firstname { get; set; }

        [Column("lastname", CanBeNull = false)]
        public string Lastname { get; set; }

        [Column("idcode", CanBeNull = false)]
        public string IdCode { get; set; }

        [Column("email", CanBeNull = false)]
        public string Email { get; set; }
    }
}
