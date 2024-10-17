using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTCSDLMayBay.Models
{
    [Table("PhieuDatCho")]
    public class PhieuDatCho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      
        public int MaPhieu { get; set; }

        [Required]
        public DateTime NgMua { get; set; } = DateTime.Now;

        [Required]
        [StringLength(20)]
        public string TrangThai { get; set; }

      
       
    }
}