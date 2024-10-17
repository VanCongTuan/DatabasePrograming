using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LTCSDLMayBay.Models
{
    [Table("NhanVien")]
    public class NhanVien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string HoVaTen { get; set; }

        public DateTime NgaySinh { get; set; }

        [Required]
        [StringLength(5)]
        public string GioiTinh { get; set; }

        [Required]
        public int Luong { get; set; }

        [Required]
        public int taiKhoanId { get; set; }


       
        [ForeignKey("taiKhoanId")]
        public virtual TaiKhoan TaiKhoan { get; set; }
       

       
    }
}