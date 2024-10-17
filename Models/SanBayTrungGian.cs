using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTCSDLMayBay.Models
{
    [Table("SanBayTrungGian")]
    public class SanBayTrungGian
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaSBTG { get; set; }

        [Required]
        public int ThoiGianDung { get; set; }
        [Required]
        public int SanBayId { get; set; }
        [Required]
        public int ChuyenBayId { get; set; }
        [Required]
        [StringLength(20)]
        public string GhiChu { get; set; } = "Dang";

        [ForeignKey("SanBayId")]
        public virtual SanBay SanBay { get; set; }
        [ForeignKey("ChuyenBayId")]
        public virtual ChuyenBay ChuyenBay { get; set; }
       
    }
}