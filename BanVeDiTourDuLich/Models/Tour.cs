namespace BanVeDiTourDuLich
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tour")]
    public partial class Tour
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tour()
        {
            Ves = new HashSet<Ve>();
        }

        [Key]
        [StringLength(20)]
        public string MaTour { get; set; }

        [Required]
        [StringLength(20)]
        public string MaDiemDen { get; set; }

        [Required]
        [StringLength(20)]
        public string MaDiemDi { get; set; }

        public DateTime ThoigianDi { get; set; }

        public int SoGio { get; set; }

        public virtual DiaDiem DiaDiemDi { get; set; }

        public virtual DiaDiem DiaDiemDen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ve> Ves { get; set; }
    }
}
