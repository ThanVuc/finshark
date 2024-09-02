using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Stock
{
    public class CreateRequestStockDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol can not over 10 char")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MinLength(3, ErrorMessage = "Company name have to over 3 char")]
        [MaxLength(256, ErrorMessage = "Company name can not over 256 char")]
        public string CompanyName { get; set; } = string.Empty;
        [Range(1,100000000, ErrorMessage = "1< Purchase <10000000")]
        public decimal Purchase { get; set; }
        [Range(1,100000000, ErrorMessage = "1< LastDiv <10000000")]
        public decimal LastDiv { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Indistry have to over 3 char")]
        [MaxLength(256, ErrorMessage = "Indistry can not over 256 char")]
        public string Industry { get; set; } = string.Empty;
        [Range(1,50000000000, ErrorMessage = "1< MarketCap <5000000000")]
        public long MarketCap { get; set; }
    }
}