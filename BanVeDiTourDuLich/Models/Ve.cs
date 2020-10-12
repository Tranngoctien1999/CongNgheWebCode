namespace BanVeDiTourDuLich
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ve")]
    public partial class Ve
    {
        [Key]
        [StringLength(20)]
        public string MaVe { get; set; }

        

        [Required]
        [StringLength(20)]
        public string MaTour { get; set; }

        [Required]
        [StringLength(20)]
        public string MaHoaDon { get; set; }

        [StringLength(20)]
        public string MaGiamGia { get; set; }

        [Required]
        [StringLength(20)]
        public string MaLoaiVe { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual LoaiVe LoaiVe { get; set; }

        public virtual MaGiamGia MaGiamGia1 { get; set; }

        public virtual Tour Tour { get; set; }
    }
}
