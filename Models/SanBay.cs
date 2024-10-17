using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LTCSDLMayBay.Models
{
    [Table("SanBay")]
    public class SanBay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaSB { get; set; }

        [Required]
        [StringLength(30)]
        public string TenSB { get; set; }

        [Required]
        [StringLength(20)]
        public string DiaChi { get; set; }

       
       
    }
}