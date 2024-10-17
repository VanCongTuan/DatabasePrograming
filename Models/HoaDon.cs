using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTCSDLMayBay.Models
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime NgayLap { get; set; } = DateTime.Today;

        public float TongTien { get; set; }

        [Required]
        public DateTime NgHetHanThanhToan { get; set; }
        [Required]
        public int lichBayId { get; set; }

        [ForeignKey("lichBayId")]
        public virtual LichBay LichBay { get; set; }
       
    }
}