using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_CRUD_Personal_Imagen.Models
{
    public class PersonalModel
    {
        [Range(0,int.MaxValue)]
        public int codper { get; set; }
        [Required]
        public string nomper { get; set; }

        public DateTime fecing { get; set; }
        
        public decimal sueper { get; set; }
        public string fotoper { get; set; }
    }
}