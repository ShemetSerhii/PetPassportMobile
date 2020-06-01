using System;
using System.Collections.Generic;

namespace PetPassport.Models
{
    public class MedicalRow
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public DateTime? DateExpiry { get; set; }

        public IEnumerable<AttachmentDto> Attachments { get; set; }
    }
}