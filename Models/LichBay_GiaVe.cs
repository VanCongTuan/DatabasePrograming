using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTCSDLMayBay.Models
{
    [Table("LichBay_GiaVe")]
    public class LichBay_GiaVe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime NgayApDung { get; set; }

        [Required]
        public DateTime NgayKetThuc { get; set; }

        [Required]
        public int SoLuongGhe { get; set; }
        [Required]
        public int hangVeId { get; set; }
        [Required]
        public int lichBayId { get; set; }
        [Required]
        public int giaVeId { get; set; }
        [ForeignKey("hangVeId")]
        public virtual HangVe HangVe { get; set; }
        [ForeignKey("lichBayId")]
        public virtual LichBay LichBay { get; set; }
        [ForeignKey("giaVeId")]
        public virtual GiaVe GiaVe { get; set; }
        
    }
}