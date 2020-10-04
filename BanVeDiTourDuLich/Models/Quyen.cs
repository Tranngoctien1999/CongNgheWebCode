namespace BanVeDiTourDuLich
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Quyen")]
    public partial class Quyen
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(15)]
        public string MaQuyen { get; set; }

        public string ChiTiet { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaLoaiQuyen { get; set; }

        public virtual LoaiQuyen LoaiQuyen { get; set; }
    }
}
