using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BaoCaoTTCM.Models;

public partial class QlbanGiayContext : DbContext
{
    public QlbanGiayContext()
    {
    }

    public QlbanGiayContext(DbContextOptions<QlbanGiayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TAnh> TAnhs { get; set; }

    public virtual DbSet<TChiTietHdb> TChiTietHdbs { get; set; }

    public virtual DbSet<TChiTietHdn> TChiTietHdns { get; set; }

    public virtual DbSet<THang> THangs { get; set; }

    public virtual DbSet<THoaDonBan> THoaDonBans { get; set; }

    public virtual DbSet<THoaDonNhap> THoaDonNhaps { get; set; }

    public virtual DbSet<TKhachHang> TKhachHangs { get; set; }

    public virtual DbSet<TKichThuoc> TKichThuocs { get; set; }

    public virtual DbSet<TNhaCungCap> TNhaCungCaps { get; set; }

    public virtual DbSet<TNhanVien> TNhanViens { get; set; }

    public virtual DbSet<TSanPham> TSanPhams { get; set; }

    public virtual DbSet<TSize> TSizes { get; set; }

    public virtual DbSet<TUser> TUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=VIET\\SQLEXPRESS;Initial Catalog=QLBanGiay;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TAnh>(entity =>
        {
            entity.HasKey(e => new { e.MaSp, e.TenFileAnh });

            entity.ToTable("tAnh");

            entity.Property(e => e.MaSp).HasMaxLength(10);
            entity.Property(e => e.TenFileAnh).HasMaxLength(50);

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.TAnhs)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tAnh_tSanPham");
        });

        modelBuilder.Entity<TChiTietHdb>(entity =>
        {
            entity.HasKey(e => new { e.SoHdb, e.MaSp });

            entity.ToTable("tChiTietHDB");

            entity.Property(e => e.SoHdb)
                .HasMaxLength(10)
                .HasColumnName("SoHDB");
            entity.Property(e => e.MaSp).HasMaxLength(10);
            entity.Property(e => e.KhuyenMai).HasMaxLength(50);
            entity.Property(e => e.Slban).HasColumnName("SLBan");

            entity.HasOne(d => d.SoHdbNavigation).WithMany(p => p.TChiTietHdbs)
                .HasForeignKey(d => d.SoHdb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tChiTietHDB_tHoaDonBan");
        });

        modelBuilder.Entity<TChiTietHdn>(entity =>
        {
            entity.HasKey(e => new { e.SoHdn, e.MaSp });

            entity.ToTable("tChiTietHDN");

            entity.Property(e => e.SoHdn)
                .HasMaxLength(10)
                .HasColumnName("SoHDN");
            entity.Property(e => e.MaSp)
                .HasMaxLength(10)
                .HasColumnName("MaSP");
            entity.Property(e => e.KhuyenMai).HasMaxLength(50);
            entity.Property(e => e.Slnhap).HasColumnName("SLNhap");

            entity.HasOne(d => d.SoHdnNavigation).WithMany(p => p.TChiTietHdns)
                .HasForeignKey(d => d.SoHdn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tChiTietHDN_tHoaDonNhap");
        });

        modelBuilder.Entity<THang>(entity =>
        {
            entity.HasKey(e => e.MaHang);

            entity.ToTable("tHang");

            entity.Property(e => e.MaHang).HasMaxLength(10);
            entity.Property(e => e.TenHang).HasMaxLength(30);
        });

        modelBuilder.Entity<THoaDonBan>(entity =>
        {
            entity.HasKey(e => e.SoHdb);

            entity.ToTable("tHoaDonBan");

            entity.Property(e => e.SoHdb)
                .HasMaxLength(10)
                .HasColumnName("SoHDB");
            entity.Property(e => e.MaKh)
                .HasMaxLength(10)
                .HasColumnName("MaKH");
            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayBan).HasColumnType("date");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.THoaDonBans)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK_tHoaDonBan_tKhachHang");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.THoaDonBans)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK_tHoaDonBan_tNhanVien");
        });

        modelBuilder.Entity<THoaDonNhap>(entity =>
        {
            entity.HasKey(e => e.SoHdn);

            entity.ToTable("tHoaDonNhap");

            entity.Property(e => e.SoHdn)
                .HasMaxLength(10)
                .HasColumnName("SoHDN");
            entity.Property(e => e.MaNcc)
                .HasMaxLength(10)
                .HasColumnName("MaNCC");
            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayNhap).HasColumnType("date");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.THoaDonNhaps)
                .HasForeignKey(d => d.MaNcc)
                .HasConstraintName("FK_tHoaDonNhap_tNhaCungCap");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.THoaDonNhaps)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK_tHoaDonNhap_tNhanVien");
        });

        modelBuilder.Entity<TKhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh);

            entity.ToTable("tKhachHang");

            entity.Property(e => e.MaKh)
                .HasMaxLength(10)
                .HasColumnName("MaKH");
            entity.Property(e => e.DiaChi).HasMaxLength(30);
            entity.Property(e => e.DienThoai).HasMaxLength(15);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.TenKh)
                .HasMaxLength(50)
                .HasColumnName("TenKH");
            entity.Property(e => e.UserName).HasMaxLength(30);

            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.TKhachHangs)
                .HasForeignKey(d => d.UserName)
                .HasConstraintName("FK_tKhachHang_tUser");
        });

        modelBuilder.Entity<TKichThuoc>(entity =>
        {
            entity.HasKey(e => e.MaSize);

            entity.ToTable("tKichThuoc");

            entity.Property(e => e.MaSize).HasMaxLength(10);
        });

        modelBuilder.Entity<TNhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc);

            entity.ToTable("tNhaCungCap");

            entity.Property(e => e.MaNcc)
                .HasMaxLength(10)
                .HasColumnName("MaNCC");
            entity.Property(e => e.TenNcc)
                .HasMaxLength(30)
                .HasColumnName("TenNCC");
        });

        modelBuilder.Entity<TNhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv);

            entity.ToTable("tNhanVien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .HasColumnName("MaNV");
            entity.Property(e => e.Cccd)
                .HasMaxLength(15)
                .HasColumnName("CCCD");
            entity.Property(e => e.DiaChi).HasMaxLength(30);
            entity.Property(e => e.DienThoai).HasMaxLength(15);
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.TenNv)
                .HasMaxLength(50)
                .HasColumnName("TenNV");
            entity.Property(e => e.UserName).HasMaxLength(30);

            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.TNhanViens)
                .HasForeignKey(d => d.UserName)
                .HasConstraintName("FK_tNhanVien_tUser");
        });

        modelBuilder.Entity<TSanPham>(entity =>
        {
            entity.HasKey(e => e.MaSp).HasName("PK_tSanPham_1");

            entity.ToTable("tSanPham");

            entity.Property(e => e.MaSp).HasMaxLength(10);
            entity.Property(e => e.Anh).HasMaxLength(20);
            entity.Property(e => e.MaHang).HasMaxLength(10);
            entity.Property(e => e.MaNcc)
                .HasMaxLength(50)
                .HasColumnName("MaNCC");
            entity.Property(e => e.TenSp).HasMaxLength(50);

            entity.HasOne(d => d.MaHangNavigation).WithMany(p => p.TSanPhams)
                .HasForeignKey(d => d.MaHang)
                .HasConstraintName("FK_tSanPham_tHang");
        });

        modelBuilder.Entity<TSize>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tSize");

            entity.Property(e => e.MaSize).HasMaxLength(10);
            entity.Property(e => e.MaSp).HasMaxLength(10);

            entity.HasOne(d => d.MaSizeNavigation).WithMany()
                .HasForeignKey(d => d.MaSize)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tSize_tKichThuoc");

            entity.HasOne(d => d.MaSpNavigation).WithMany()
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tSize_tSanPham");
        });

        modelBuilder.Entity<TUser>(entity =>
        {
            entity.HasKey(e => e.UserName);

            entity.ToTable("tUser");

            entity.Property(e => e.UserName).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
