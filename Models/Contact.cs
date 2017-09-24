using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Contacts.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        [Required]
        public string Telephone { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public string Info { get; set; }
        public int Organ_Id { get; set; }
        [ForeignKey("Organ_Id")]
        public Organ Organ { get; set; }

    }

}