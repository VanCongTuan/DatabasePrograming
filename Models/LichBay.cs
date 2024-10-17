using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTCSDLMayBay.Models
{[Table("LichBay")]
    public class LichBay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaLB { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime NgayBay { get; set; }

        [Required]
        public int ThoiGianBay { get; set; }

        [StringLength(20)]
        public string TrangThai { get; set; }
        public int mayBayId { get; set; }

        [ForeignKey("mayBayId")]
        public virtual MayBay MayBay { get; set; }
        public int chuyenBayId { get; set; }


        [ForeignKey("chuyenBayId")]
        public virtual ChuyenBay ChuyenBay { get; set; }
        public int nhanVienId { get; set; }


        [ForeignKey("nhanVienId")]
        public virtual NhanVien NhanVien { get; set; }

       
    }
}