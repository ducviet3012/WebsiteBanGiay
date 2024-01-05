using System;
using System.Collections.Generic;

namespace BaoCaoTTCM.Models;

public partial class TChiTietHdb
{
    public string SoHdb { get; set; } = null!;

    public string MaSp { get; set; } = null!;

    public int? Slban { get; set; }

    public string? KhuyenMai { get; set; }

    public double? DonGia { get; set; }

    public virtual THoaDonBan SoHdbNavigation { get; set; } = null!;
}
