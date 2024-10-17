using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTCSDLMayBay.Models
{
    [Table("ChuyenBay")]
    public class ChuyenBay
    {
         [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaCB { get; set; }

        public string TenCB { get; set; }
        public int tuyenBayId { get; set; }
        [ForeignKey("tuyenBayId")]
        public virtual TuyenBay TuyenBay { get; set; }
       

   
    }
}