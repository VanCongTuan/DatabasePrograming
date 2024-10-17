using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTCSDLMayBay.Models
{
    [Table("HanhKhach")]
    public class HanhKhach
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public string GioiTinh { get; set; }

        [Required]
        [StringLength(30)]
        public string HoTen { get; set; }

        public DateTime? NgSinh { get; set; }
    }
}