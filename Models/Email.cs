using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LTCSDLMayBay.Models
{
    [Table("Email")]
    public class Email
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Ten { get; set; }
        public int nguoiLonId { get; set; }

        [ForeignKey("nguoiLonId")]
        public virtual NguoiLon NguoiLon { get; set; }
        
    }
}