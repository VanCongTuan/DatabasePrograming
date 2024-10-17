using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTCSDLMayBay.Models
{
    [Table("ChiTietDoanhThu")]
    public class ChiTietDoanhThu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SoLuotBay { get; set; }

        public float TyLe { get; set; }

        public float DoanhThu { get; set; }

        public int lichBayId { get; set; }
        [ForeignKey("lichBayId")]
        public virtual TuyenBay TuyenBay { get; set; }
        public int baoCaoId { get; set; }
        [ForeignKey("baoCaoId")]
        public virtual BaoCaoDoanhThu BaoCao { get; set; }
    }
}