using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Serwis.Models
{
    public enum Stanowisko
    {
        Serwisant, Kierownik_Serwisu, Doradca_Serwisowy, Informatyk, Administrator
    }

    public class Employee
    {
        public int ID { get; set; }

        [StringLength(50, ErrorMessage = "Imię nie może być dłuższe niż 50 znaków.")]
        [Display(Name = "Imię")]
        public string Imię { get; set; }

        [StringLength(50, ErrorMessage = "Imię nie może być dłuższe niż 50 znaków.")]
        [Display(Name = "Nazwisko")]
        public string Nazwisko { get; set; }
        [Display(Name = "Stanowisko")]
        public Stanowisko? Stanowisko { get; set; }
        [Display(Name = "Adres email")]
        public string Adres_email { get; set; }

        [Display(Name = "Imię i Nazwisko")]
        public string ImięNazwisko
        {
            get
            {
                return Imię + " " + Nazwisko;
            }
        }

        public virtual ICollection<Request> Requests { get; set; }
      
    }
}