using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HappyOrSad.Models
{
    public class Village
    {
        [Key]
        public int VillageID { get; set; }
        public string VillageCode { get; set; }
        public string VillageName { get; set; }
    }
}