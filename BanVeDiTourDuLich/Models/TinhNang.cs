using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace BanVeDiTourDuLich.Models
{
    public class TinhNang
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTinhNang { get; set; }

        [StringLength(20)]
        public string MaTour { get; set; }

        public string TenTinhNang { get; set; }

        public string ThongTinTinhNang { get; set; }

        public virtual ChiTietTour ChiTietTour {get; set; }
    }
}