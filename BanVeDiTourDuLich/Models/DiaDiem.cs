namespace BanVeDiTourDuLich
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DiaDiem")]
    public partial class DiaDiem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DiaDiem()
        {
            Tours = new HashSet<Tour>();
            Tours1 = new HashSet<Tour>();
        }

        [Key]
        [StringLength(20)]
        public string MaDiaDiem { get; set; }

        [Required]
        public string TenDiaDiem { get; set; }

        [Required]
        public string DiaChi { get; set; }

        public string DuongDanAnh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tour> Tours { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tour> Tours1 { get; set; }
    }
}
