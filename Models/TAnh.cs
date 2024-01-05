using System;
using System.Collections.Generic;

namespace BaoCaoTTCM.Models;

public partial class TAnh
{
    public string MaSp { get; set; } = null!;

    public string TenFileAnh { get; set; } = null!;

    public virtual TSanPham MaSpNavigation { get; set; } = null!;
}
