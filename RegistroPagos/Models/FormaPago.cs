using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroPagos.Models
{
    public class FormaPago
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Forma de Pago")]
        [Required]
        public string Descripcion { get; set; }
    }
}
