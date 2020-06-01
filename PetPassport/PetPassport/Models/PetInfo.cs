using System;

namespace PetPassport.Models
{
    public class PetInfo : Pet
    {
        public string Kind { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}