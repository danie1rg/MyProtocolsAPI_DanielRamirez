using System;
using System.Collections.Generic;

namespace MyProtocolsAPI_DanielRamírez.Models
{
    public partial class ProtocolStep
    {
        public ProtocolStep()
        {
            ProtocolProtocols = new HashSet<Protocol>();
        }

        public int ProtocolStepsId { get; set; }
        public string Step { get; set; } = null!;
        public string? Description { get; set; }
        public int UserId { get; set; }

        public virtual User? User { get; set; } = null!;

        public virtual ICollection<Protocol>? ProtocolProtocols { get; set; }
    }
}
