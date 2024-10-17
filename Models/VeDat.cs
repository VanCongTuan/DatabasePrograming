using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LTCSDLMayBay.Models
{
    [Table("VeDat")]
    public class VeDat
    {
        
        
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int MaVe { get; set; }

            [Required]
            public DateTime NgDat { get; set; } = DateTime.Now;

            [StringLength(4)]
            public string GheDaDat { get; set; }

            [StringLength(20)]
            public string TinhTrangVe { get; set; }
            [Required]
            public int lichBayId { get; set; }
            [Required]
            public int hanhKhachId { get; set; }
            [Required]
            public int hangVeId { get; set; }
            [Required]
            public int phieuDatChoId { get; set; }

            [ForeignKey("lichBayId")]
            public virtual LichBay LichBay { get; set; }


            [ForeignKey(" hanhKhachId")]
            public virtual HanhKhach HanhKhach { get; set; }

            [ForeignKey("hangVeId")]
            public virtual HangVe HangVe { get; set; }

            [ForeignKey("phieuDatChoId")]
            public virtual PhieuDatCho PhieuDatCho { get; set; }



        
    }
}