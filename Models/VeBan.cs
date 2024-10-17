using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTCSDLMayBay.Models
{
    [Table("VeBan")]
    public class VeBan
    {
       

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int veDatId { get; set; }
        [Required]
        public int nguoiLon_id { get; set; }

        [ForeignKey("veDatId")]
        public virtual VeDat VeDat { get; set; }


        [ForeignKey("nguoiLon_id")]
        public virtual NhanVien NhanVien { get; set; }
        
    }
}