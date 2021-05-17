using System;

namespace Domain.Entities
{
    public class AuditableEntity
    {
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public long? CreateBy { get; set; }
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
        public long? LastUpdateBy { get; set; }
        public bool HasDeleted { get; set; }
    }

}
