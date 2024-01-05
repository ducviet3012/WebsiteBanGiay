using System;
using System.Collections.Generic;

namespace BaoCaoTTCM.Models;

public partial class TSanPham
{
    public string MaSp { get; set; } = null!;

    public string? TenSp { get; set; }

    public string? MaNcc { get; set; }

    public string? MaHang { get; set; }

    public int? SoLuong { get; set; }

    public double? DonGiaNhap { get; set; }

    public double? DonGiaBan { get; set; }

    public int? ThoiGianBaoHanh { get; set; }

    public string? Anh { get; set; }

    public virtual THang? MaHangNavigation { get; set; }

    public virtual ICollection<TAnh> TAnhs { get; set; } = new List<TAnh>();
}
