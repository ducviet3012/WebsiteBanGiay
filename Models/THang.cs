using System;
using System.Collections.Generic;

namespace BaoCaoTTCM.Models;

public partial class THang
{
    public string MaHang { get; set; } = null!;

    public string? TenHang { get; set; }

    public virtual ICollection<TSanPham> TSanPhams { get; set; } = new List<TSanPham>();
}
