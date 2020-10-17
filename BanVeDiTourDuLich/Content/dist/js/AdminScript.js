var TongQuanLink = document.getElementById("#TongQuanLink");
TongQuanLink.addEventListener("click", () => {
        $(TongQuanLink).addClass("active");
        $(document.getElementById("#QuanLyVeLink")).removeClass("active");
    $(document.getElementById("#TongQuanLink")).removeClass("active");
})

var QuanLyVeLink = document.getElementById("#QuanLyVeLink");
QuanLyVeLink.addEventListener("click", () => {
    $(QuanLyVeLink).addClass("active");
    $(document.getElementById("#TongQuanLink")).removeClass("active");
    $(document.getElementById("#QuanLyNguoiDungLink")).removeClass("active");
})

var QuanLyNguoiDungLink = document.getElementById("#QuanLyNguoiDungLink");
QuanLyNguoiDungLink.addEventListener("click", () => {
    $(QuanLyNguoiDungLink).addClass("active");
    $(document.getElementById("#QuanLyVeLink")).removeClass("active");
    $(document.getElementById("#TongQuanLink")).removeClass("active");
})