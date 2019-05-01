using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Serwis.ViewModel
{
    public class RequestStatistic
    {
        public int? ClientID { get; set; }

        public int RequestCount { get; set; }
    }
}