using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serwis.Models
{
    public class Client
    {
        
        public int ID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage="Imię nie może być dłuższe niż 50 znaków.")]
        [Display(Name ="Imię")]
        public string Imię { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Nazwisko nie może być dłuższe niż 50 znaków.")]
        [Display(Name ="Nazwisko")]
        public string Nazwisko { get; set; }

        [Display(Name = "Adres")]
        public string Adres { get; set; }
        [Display(Name = "Miasto")]
        public string Miasto { get; set; }
        [Display(Name = "Nr telefonu")]
        public string Nr_telefonu { get; set; }
        [Display(Name = "Adres email")]
        public string Adres_email { get; set; }

        [Display(Name ="Imię i Nazwisko")]
        public string ImięNazwisko
        {
            get
            {
                return Imię + " " + Nazwisko;
            }
        }

        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Repair_status> Repair_statuses { get; set; }
    }
}