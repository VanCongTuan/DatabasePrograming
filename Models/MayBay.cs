using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTCSDLMayBay.Models
{
    [Table("MayBay")]
    public class MayBay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaMb { get; set; }

        [Required]
        [StringLength(20)]
        public string TenMB { get; set; }

      
    }
}