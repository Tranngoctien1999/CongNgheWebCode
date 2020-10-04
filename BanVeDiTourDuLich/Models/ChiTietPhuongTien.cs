namespace BanVeDiTourDuLich
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhuongTien")]
    public partial class ChiTietPhuongTien
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string MaLoaiVe { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string MaPhuongTien { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime ThoiGianKhoiHanh { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] ThoiGianDi { get; set; }

        public virtual LoaiVe LoaiVe { get; set; }

        public virtual PhuongTien PhuongTien { get; set; }
    }
}
