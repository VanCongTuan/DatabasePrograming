using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTCSDLMayBay.Models
{
    [Table("TreEm")]
    public class TreEm
    {
        [Key]
        [Required]
        public int Id_TreEm { get; set; }

        [Required]
        public int NguoiLon_id { get; set; }

        // Navigation properties
        [ForeignKey("Id_TreEm")]
        public HanhKhach HanhKhach { get; set; }
        [ForeignKey("NguoiLon_id")]
        public NguoiLon NguoiLon
        {
            get; set;

        }
    }
}