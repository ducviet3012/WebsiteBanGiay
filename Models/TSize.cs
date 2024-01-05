using System;
using System.Collections.Generic;

namespace BaoCaoTTCM.Models;

public partial class TSize
{
    public string MaSp { get; set; } = null!;

    public string MaSize { get; set; } = null!;

    public virtual TKichThuoc MaSizeNavigation { get; set; } = null!;

    public virtual TSanPham MaSpNavigation { get; set; } = null!;
}
