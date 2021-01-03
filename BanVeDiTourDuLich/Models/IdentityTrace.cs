using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.Models
{
    public class IdentityTrace
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Key { get; set; }

        public int KhachHangIdentity { get; set; }

        public int NhanVienIdentity { get; set; }

        public int KhachHangOptionalIdentity { get; set; }
    }
}