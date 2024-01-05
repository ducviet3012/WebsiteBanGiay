using System;
using System.Collections.Generic;

namespace BaoCaoTTCM.Models;

public partial class TKhachHang
{
    public string MaKh { get; set; } = null!;

    public string? TenKh { get; set; }

    public string? DiaChi { get; set; }

    public string? GioiTinh { get; set; }

    public string? DienThoai { get; set; }

    public string? UserName { get; set; }

    public virtual ICollection<THoaDonBan> THoaDonBans { get; set; } = new List<THoaDonBan>();

    public virtual TUser? UserNameNavigation { get; set; }
}
