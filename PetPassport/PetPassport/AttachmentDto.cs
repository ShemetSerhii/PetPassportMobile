using System;

namespace PetPassport
{
    public class AttachmentDto
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public DateTime CreationDate { get; set; }
    }
}