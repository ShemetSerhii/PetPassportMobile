using System;

namespace PetPassport.Models
{
    public class PetCreate
    {
        public string Name { get; set; }

        public string Kind { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}