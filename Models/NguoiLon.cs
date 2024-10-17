using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTCSDLMayBay.Models
{
    [Table("NguoiLon")]
    public class NguoiLon
    {
        [Key]
        public int idNguoiLon { get; set; }
        [Required]
        [StringLength(13)]
        public string CCCD { get; set; }
        [ForeignKey("idNguoiLon")]
        public virtual HanhKhach HanhKhach { get; set; }

    }
}