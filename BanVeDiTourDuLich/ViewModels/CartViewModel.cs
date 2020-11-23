using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanVeDiTourDuLich.ViewModels
{
    public class CartViewModel
    {
        public List<ThongTinVeTrongGio> ThongTinVeTrongGios { get; set; }
        public StripeKey StripeKey { get; set; }
    }
}