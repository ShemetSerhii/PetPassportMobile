using System;

namespace PetPassport.Models
{
    public class Pet
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Picture { get; set; }

        public string PicturePath { get; set; }
    }
}