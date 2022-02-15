using System;
using System.Collections.Generic;

#nullable disable

namespace Data_Access_Layer.Audit
{
    public partial class AuditGreske
    {
        public long Id { get; set; }
        public DateTime? DatumIvreme { get; set; }
        public string Poruka { get; set; }
        public string Metoda { get; set; }
        public string UserId { get; set; }
    }
}
