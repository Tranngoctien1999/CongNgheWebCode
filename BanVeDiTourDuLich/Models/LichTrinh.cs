using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.Models
{
    public class LichTrinh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTinhNang { get; set; }
        public string MaTour { get; set; }
        public int Ngay { get; set; }
        public string MoTa { get; set; }
        public string ChiTiet { get; set; }
        public string DuongDanAnh { get; set; }
    }
}