using System;
using System.Collections.Generic;

namespace MyProtocolsAPI_DanielRamírez.Models
{
    public partial class Protocol
    {
        public Protocol()
        {
            ProtocolStepProtocolSteps = new HashSet<ProtocolStep>();
        }

        public int ProtocolId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public TimeSpan? AlarmHour { get; set; }
        public bool? AlarmActive { get; set; }
        public bool? AlarmJustInWeekDays { get; set; }
        public bool? Active { get; set; }
        public int UserId { get; set; }
        public int ProtocolCategory { get; set; }

        public virtual ProtocolCategory ProtocolCategoryNavigation { get; set; } = null!;
        public virtual User User { get; set; } = null!;

        public virtual ICollection<ProtocolStep> ProtocolStepProtocolSteps { get; set; }
    }
}
