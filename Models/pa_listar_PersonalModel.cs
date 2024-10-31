using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_CRUD_Personal_Imagen.Models
{
    public class pa_listar_PersonalModel
    {
        [Range(1,int.MaxValue)]
        public int codper { get; set; }
        [Required]
        public string nomper { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString ="{0:yyyy-MM-dd}")]
        public DateTime fecing { get; set; }
        public decimal sueper { get; set; }
        public string fotoper { get; set; }
        public int antiguedad { get; set; }
        public decimal acumulado { get; set; }
    }
}