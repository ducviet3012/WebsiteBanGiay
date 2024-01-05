using System;
using System.Collections.Generic;

namespace BaoCaoTTCM.Models;

public partial class TNhanVien
{
    public string MaNv { get; set; } = null!;

    public string? TenNv { get; set; }

    public string? GioiTinh { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string? DiaChi { get; set; }

    public string? DienThoai { get; set; }

    public string? Cccd { get; set; }

    public string? UserName { get; set; }

    public virtual ICollection<THoaDonBan> THoaDonBans { get; set; } = new List<THoaDonBan>();

    public virtual ICollection<THoaDonNhap> THoaDonNhaps { get; set; } = new List<THoaDonNhap>();

    public virtual TUser? UserNameNavigation { get; set; }
}
