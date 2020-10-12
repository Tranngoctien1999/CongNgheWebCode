using BanVeDiTourDuLich.Models;

namespace BanVeDiTourDuLich
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }

        public virtual DbSet<DiaDiem> DiaDiems { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiKhachHang> LoaiKhachHangs { get; set; }
        public virtual DbSet<LoaiNhanVien> LoaiNhanViens { get; set; }
        public virtual DbSet<LoaiPhuongTien> LoaiPhuongTiens { get; set; }
        public virtual DbSet<LoaiQuyen> LoaiQuyens { get; set; }
        public virtual DbSet<LoaiVe> LoaiVes { get; set; }
        public virtual DbSet<MaGiamGia> MaGiamGias { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<PhuongTien> PhuongTiens { get; set; }
        public virtual DbSet<ThongTinLoaiQuyenLoaiNhanVien> ThongTinLoaiQuyenLoaiNhanViens { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<Ve> Ves { get; set; }
        public virtual DbSet<ChiTietPhuongTien> ChiTietPhuongTiens { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<NhanXet> NhanXets { get; set;}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiaDiem>()
                .Property(e => e.MaDiaDiem)
                .IsUnicode(false);

            modelBuilder.Entity<DiaDiem>()
                .HasMany(e => e.Tours)
                .WithRequired(e => e.DiaDiemDi)
                .HasForeignKey(e => e.MaDiemDen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DiaDiem>()
                .HasMany(e => e.Tours1)
                .WithRequired(e => e.DiaDiemDen)
                .HasForeignKey(e => e.MaDiemDi)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaHoaDon)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaKhachHang)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.Ves)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MaKhachHang)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MaLoaiKhachHang)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiKhachHang>()
                .Property(e => e.MaLoaiKhachHang)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiKhachHang>()
                .Property(e => e.ChiTiet);

            modelBuilder.Entity<LoaiKhachHang>()
                .HasMany(e => e.KhachHangs)
                .WithRequired(e => e.LoaiKhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiNhanVien>()
                .Property(e => e.MaLoaiNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiNhanVien>()
                .HasMany(e => e.NhanViens)
                .WithRequired(e => e.LoaiNhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiNhanVien>()
                .HasMany(e => e.ThongTinLoaiQuyenLoaiNhanViens)
                .WithRequired(e => e.LoaiNhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiPhuongTien>()
                .Property(e => e.MaLoaiPhuongTien)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiPhuongTien>()
                .HasMany(e => e.PhuongTiens)
                .WithRequired(e => e.LoaiPhuongTien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiQuyen>()
                .Property(e => e.MaLoaiQuyen)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiQuyen>()
                .HasMany(e => e.Quyens)
                .WithRequired(e => e.LoaiQuyen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiQuyen>()
                .HasMany(e => e.ThongTinLoaiQuyenLoaiNhanViens)
                .WithRequired(e => e.LoaiQuyen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiVe>()
                .Property(e => e.MaLoaiVe)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiVe>()
                .HasMany(e => e.ChiTietPhuongTiens)
                .WithRequired(e => e.LoaiVe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiVe>()
                .HasMany(e => e.Ves)
                .WithRequired(e => e.LoaiVe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MaGiamGia>()
                .Property(e => e.MaGiamGia1)
                .IsUnicode(false);

            modelBuilder.Entity<MaGiamGia>()
                .HasMany(e => e.Ves)
                .WithOptional(e => e.MaGiamGia1)
                .HasForeignKey(e => e.MaGiamGia);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaLoaiNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhuongTien>()
                .Property(e => e.MaPhuongTien)
                .IsUnicode(false);

            modelBuilder.Entity<PhuongTien>()
                .Property(e => e.MaLoaiPhuongTien)
                .IsUnicode(false);

            modelBuilder.Entity<PhuongTien>()
                .HasMany(e => e.ChiTietPhuongTiens)
                .WithRequired(e => e.PhuongTien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ThongTinLoaiQuyenLoaiNhanVien>()
                .Property(e => e.MaLoaiQuyen)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinLoaiQuyenLoaiNhanVien>()
                .Property(e => e.MaLoaiNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<Tour>()
                .Property(e => e.MaTour)
                .IsUnicode(false);

            modelBuilder.Entity<Tour>()
                .Property(e => e.MaDiemDen)
                .IsUnicode(false);

            modelBuilder.Entity<Tour>()
                .Property(e => e.MaDiemDi)
                .IsUnicode(false);

            modelBuilder.Entity<Tour>()
                .Property(e => e.SoGio);

            modelBuilder.Entity<Tour>()
                .HasMany(e => e.Ves)
                .WithRequired(e => e.Tour)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tour>()
                .HasMany(e => e.LoaiVes)
                .WithRequired(e => e.Tour);

            modelBuilder.Entity<Ve>()
                .Property(e => e.MaVe)
                .IsUnicode(false);

            modelBuilder.Entity<Ve>()
                .Property(e => e.MaTour)
                .IsUnicode(false);

            modelBuilder.Entity<Ve>()
                .Property(e => e.MaHoaDon)
                .IsUnicode(false);

            modelBuilder.Entity<Ve>()
                .Property(e => e.MaGiamGia)
                .IsUnicode(false);

            modelBuilder.Entity<Ve>()
                .Property(e => e.MaLoaiVe)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhuongTien>()
                .Property(e => e.MaLoaiVe)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhuongTien>()
                .Property(e => e.MaPhuongTien)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietPhuongTien>()
                .Property(e => e.ThoiGianDi)
                .IsFixedLength();

            modelBuilder.Entity<Quyen>()
                .Property(e => e.MaQuyen)
                .IsUnicode(false);

            modelBuilder.Entity<Quyen>()
                .Property(e => e.MaLoaiQuyen)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.NhanXets)
                .WithRequired(e => e.KhachHang);

            modelBuilder.Entity<Tour>()
                .HasMany(e => e.NhanXets)
                .WithRequired(e => e.Tour);
            ;

            modelBuilder.Entity<NhanXet>()
                .ToTable("NhanXet");
        }
    }
}
