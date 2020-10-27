using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.Models
{
    public class ChiTietTour
    {
        [StringLength(20)]
        public string MaTour { get; set; }

        public string ChiTiet { get; set; }

        public virtual ICollection<TinhNang> TinhNangs { get; set; }

        public virtual Tour Tour { get; set; }
    }
}