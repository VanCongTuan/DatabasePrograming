using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTCSDLMayBay.Models
{
    [Table("TuyenBay")]
    public class TuyenBay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTuyenBay { get; set; }

       
        public int Id_SbDi { get; set; }


        public int Id_SbDen { get; set; }

        [ForeignKey("Id_SbDi")]
        public virtual SanBay SanBayDi { get; set; }

        [ForeignKey("Id_SbDen")]
        public virtual SanBay SanBayDen { get; set; }





    }
}