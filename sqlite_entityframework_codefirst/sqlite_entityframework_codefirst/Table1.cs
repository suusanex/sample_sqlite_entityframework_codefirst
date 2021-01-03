using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlite_entityframework_codefirst
{
    [Table("Table1")]
    public class Table1
    {
        [Key]
        [Column(Order = 1)]
        [MaxLength(260)]
        public string idStr1 { get; set; }

        [Key]
        [Column(Order = 2)]
        [MaxLength(32767)]
        public string idStr2 { get; set; }

        [MaxLength(16)]
        public string value1 { get; set; }

        public byte[] data1 { get; set; }

    }
}
