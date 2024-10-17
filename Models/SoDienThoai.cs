using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTCSDLMayBay.Models
{
    [Table("SoDienThoai")]
    public class SoDienThoai
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(14)]
        public string Sdt { get; set; }
        [Required]
        public int nguoiLonId { get; set; }

        [ForeignKey("nguoiLonId")]
        public virtual NguoiLon NguoiLon { get; set; }
       
    }
}