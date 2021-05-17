using System;

namespace Domain.Entities
{
    public class Attachment : AuditableEntity
    {
        public Guid AttachmentId { get; set; }
        public string FileExtension { get; set; }
        public string FileContentType { get; set; }
        public string FileName { get; set; }
        public long FileLenght { get; set; }
        public int ReferenceModelTypeId { get; set; }
        public long ReferenceModelKey { get; set; }
    }
}
