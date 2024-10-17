using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTCSDLMayBay.Models
{
    [Table("BaoCaoDoanhThu")]
    public class BaoCaoDoanhThu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime NgayXuat { get; set; } = DateTime.Today;

        [Required]
        public DateTime ThangDoanhThu { get; set; }

        public float TongDoanhThu { get; set; }

       
    }
}