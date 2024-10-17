using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTCSDLMayBay.Models
{
    [Table("QuyDinh")]
    public class QuyDinh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ThoiGianChamNhatDatVe { get; set; }

        [Required]
        public int ThoiGianChamNhatBanVe { get; set; }

        [Required]
        public int ThoiGianBayToiThieu { get; set; }

        [Required]
        public int SanBayTG_ToiDa { get; set; }

        [Required]
        public int ThoiGianDungToiThieu { get; set; }

        [Required]
        public int ThoiGianDungToiDa { get; set; }
    }
}