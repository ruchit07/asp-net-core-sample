using System;

namespace Project.Data.Models
{
    public class Active
    {
        public bool IsActive { get; set; } = true;
    }

    public class ActiveDelete
    {
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
    }

    public class ActiveCreate
    {
        public bool IsActive { get; set; } = true;
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = Helper.Utility.GetCurrentDateTime();
    }

    public class ActiveCreateDelete
    {
        public bool IsActive { get; set; } = true;
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = Helper.Utility.GetCurrentDateTime();
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
    }

    public class ActiveCreateDeleteUpdate
    {
        public bool IsActive { get; set; } = true;
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = Helper.Utility.GetCurrentDateTime();
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; } = Helper.Utility.GetCurrentDateTime();
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
    }
}
