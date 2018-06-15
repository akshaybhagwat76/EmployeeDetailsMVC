using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModel
{
    public class StateVM
    {

        public int StateId { get; set; }
        public string StateName { get; set; }
        public Nullable<int> CountryId { get; set; }

    }
}