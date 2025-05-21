using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WDC_F24.Application.DTOs.Requests
{
    public class AddProudctRequestDto 
    {
        [Required(ErrorMessage = " name is required.")]
        [MaxLength(256, ErrorMessage = " name cannot exceed 256 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = " Price is required.")]
        //[MaxLength(256, ErrorMessage = " Price cannot exceed 256 characters.")]
        public decimal Price { get; set; }

    }
}
