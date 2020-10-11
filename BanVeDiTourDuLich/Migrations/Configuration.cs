using System;

namespace BanVeDiTourDuLich.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BanVeDiTourDuLich.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BanVeDiTourDuLich.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            DiaDiem diaDiem1 = new DiaDiem()
            {
                MaDiaDiem = "DIADIEM1",
                DiaChi = "236 Hoàng Quốc Việt , Bắc Từ Liêm , Hà Nội",
                TenDiaDiem = "Học viện kĩ thuật quân sự",
                DuongDanAnh = "/Content/images/Destinations/MADIADIEM1.jpg"
            };

            DiaDiem diaDiem2 = new DiaDiem()
            {
                MaDiaDiem = "DIADIEM2",
                DiaChi = "Lực Điền ,Minh Châu , Yên mỹ ,Hưng Yên",
                TenDiaDiem = "Nhà",
                DuongDanAnh = "/Content/images/Destinations/MADIADIEM2.jpg"
            };

            context.DiaDiems.AddOrUpdate(diaDiem1, diaDiem2);

            context.Tours.AddOrUpdate(new Tour()
            {
                MaTour = "Tour1",
                DiaDiemDi = diaDiem1,
                DiaDiemDen = diaDiem2,
                MaDiemDen = diaDiem1.MaDiaDiem,
                MaDiemDi = diaDiem2.MaDiaDiem,
                SoGio = 10,
                ThoigianDi = new DateTime(2020 , 10 , 29 , 8 , 20 , 10),
            });
        }
    }
}
