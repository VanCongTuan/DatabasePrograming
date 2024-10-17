using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LTCSDLMayBay.Models
{
    [Table(" MayBay_Ghe")]
    public class MayBay_Ghe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int gheId { get; set; }
        [Required]
        public int hangVeId { get; set; }
        [Required]
        public int mayBayId { get; set; }
        [Required]
        [StringLength(3)]
        public string DayGhe { get; set; }

        [ForeignKey("gheId")]
        public virtual Ghe Ghe { get; set; }
        [ForeignKey("hangVeId")]
        public virtual HangVe HangVe { get; set; }


        [ForeignKey("mayBayId")]
        public virtual MayBay MayBay { get; set; }
        
    }
}