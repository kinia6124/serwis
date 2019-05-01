using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Serwis.Models
{
    

    public class Repair_status
    {
        
        public int ID { get; set; }
        [Display(Name = "Numer zgłoszenia")]
        public int RequestID { get; set; }
        [Display(Name = "Numer Klienta")]
        public int ClientID { get; set; }
       
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        [Display(Name = "Data rozpoczęcia")]
        public DateTime Data_rozpoczęcia { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        [Display(Name = "Data zakończenia")]
        public DateTime Data_zakończenia { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Opis naprawy")]
        public string Opis_naprawy { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
        public virtual Client Client { get; set; }
       
    }
}