CREATE DATABASE QuanLyCuaHangMyPham
GO

USE QuanLyCuaHangMyPham
GO

CREATE TABLE TaiKhoan
(
	ID			INT IDENTITY (1, 1) PRIMARY KEY,
	Ten			NVARCHAR (100) NOT NULL,
	TenDangNhap	VARCHAR (100) NOT NULL,
	MatKhau		VARCHAR (100) NOT NULL,
	Loai		INT NOT NULL, -- 0: Staff | 1: Admin
	GioiTinh	NVARCHAR (4) NOT NULL,
	NgaySinh	DATETIME,
	NgayVaoLam	DATETIME,
	SoCMND		INT,
	DiaChi		NVARCHAR (100),
	SDT			CHAR (100),
	TrangThai	BIT NOT NULL DEFAULT 1, -- 0: Disable | 1: Enable

	CHECK (GioiTinh = 'Nam' OR GioiTinh = N'Nữ'),
	CHECK (NgaySinh < NgayVaoLam)

)
GO

CREATE TABLE DoiTac
(
	MaDT		INT IDENTITY (1, 1) PRIMARY KEY,
	TenDT		NVARCHAR (50),
	DiaChi		NVARCHAR (50),
	SDT			VARCHAR (15),
	Loai		BIT NOT NULL DEFAULT 1, -- 0: Customer | 1: Supplier
	TrangThai	BIT NOT NULL DEFAULT 1 -- 0: Disable | 1: Enable
)
GO

CREATE TABLE PhanLoaiSP
(
	MaPL		INT IDENTITY (1, 1) PRIMARY KEY,
	TenPL		NVARCHAR (50),
	MoTa		NVARCHAR (500),
	TrangThai	BIT NOT NULL DEFAULT 1 -- 0: Disable | 1: Enable
)
GO

CREATE TABLE SanPham
(
	MaSP		INT IDENTITY (1, 1) PRIMARY KEY,
	TenSP		NVARCHAR (50) NOT NULL,
	PhanLoai	INT FOREIGN KEY REFERENCES dbo.PhanLoaiSP (MaPL),
	GiaNhap		INT,
	Gia			INT, -- GianBan
	DonViTinh	NVARCHAR (20),
	SoLuongTon	INT,
	XuatXu		NVARCHAR (100) DEFAULT 'Unknown',
	NhaCC		INT FOREIGN KEY REFERENCES dbo.DoiTac (MaDT),
	MoTa		NVARCHAR (500),
	TrangThai	BIT NOT NULL DEFAULT 1, -- 0: Disable | 1: Enable

	CHECK (SoLuongTon >= 0)
)
GO

--CREATE TABLE NhanVien 
--(
--	MaNV		INT IDENTITY (1, 1) PRIMARY KEY,
--	TenNV		NVARCHAR (50) NOT NULL,
--	GioiTinh	NVARCHAR (4) NOT NULL,
--	NgaySinh	DATETIME,
--	NgayVaoLam	DATETIME NOT NULL,
--	SoCMND		INT,
--	DiaChi		NVARCHAR (100),
--	SDT			CHAR (100),
--	IDTK		INT FOREIGN KEY REFERENCES dbo.TaiKhoan (ID),
--	TrangThai	BIT NOT NULL DEFAULT 1, -- 0: Disable | 1: Enable

--	CHECK (GioiTinh = 'Nam' OR GioiTinh = N'Nữ'),
--	CHECK (NgaySinh < NgayVaoLam)
--)
--GO

CREATE TABLE HoaDon
(
	MaHD		INT IDENTITY (1, 1) PRIMARY KEY,
	MaTK		INT FOREIGN KEY REFERENCES dbo.TaiKhoan (ID),
	MaKH		INT FOREIGN KEY REFERENCES dbo.DoiTac (MaDT),
	NgayLap		DATETIME NOT NULL DEFAULT GETDATE (),
	NgayTT		DATETIME,
	GiamGia		INT NOT NULL DEFAULT 0,
	TongTien	FLOAT,
	Loai		BIT NOT NULL DEFAULT 1, -- 0: Import | 1: Export
	TrangThai	BIT NOT NULL DEFAULT 1 -- 0: Disable | 1: Enable
)
GO

CREATE TABLE CTHoaDon
(
	MaHD		INT FOREIGN KEY REFERENCES HoaDon (MaHD),
	MaSP		INT FOREIGN KEY REFERENCES SanPham(MaSP),
	SoLuong		INT NOT NULL DEFAULT 0,
	ThanhTien	INT NOT NULL DEFAULT 0,
	TrangThai	BIT NOT NULL DEFAULT 1 -- 0: Disable | 1: Enable

	PRIMARY KEY (MaHD, MaSP),
	CHECK (SoLuong >= 0)
)
GO

-- TaiKhoan
INSERT INTO dbo.TaiKhoan (Ten, TenDangNhap, MatKhau, Loai, GioiTinh, NgaySinh, NgayVaoLam, SoCMND, DiaChi, SDT)
	VALUES ('Admin', 'admin', '1', 1, 'Nam', '20020602', '20240602', 123456789, N'Lâm Hà', '1900100069')
INSERT INTO dbo.TaiKhoan (Ten, TenDangNhap, MatKhau, Loai, GioiTinh, NgaySinh, NgayVaoLam, SoCMND, DiaChi, SDT)
	VALUES (N'Nguyễn Văn A', 'staff', '1', 0, 'Nam', '19990609', '20190906', 987654321, N'Lâm Đồng', '1900199969')
GO

-- DoiTac
INSERT INTO dbo.DoiTac (TenDT, DiaChi, SDT, Loai) VALUES ('Naruko', 'Taiwan', '0123456789', 1)
INSERT INTO dbo.DoiTac (TenDT, DiaChi, SDT, Loai) VALUES (N'Công ty dược Galderma Laboratories', 'Germany', '0123456789', 1)
INSERT INTO dbo.DoiTac (TenDT, DiaChi, SDT, Loai) VALUES ('Hasaki', N'Việt Nam', '0999999999', 1)
INSERT INTO dbo.DoiTac (TenDT, DiaChi, SDT, Loai) VALUES ('Nivea', 'Germany', '1092847561', 1)
INSERT INTO dbo.DoiTac (TenDT, DiaChi, SDT, Loai) VALUES ('SILKYGIRL', 'Thailand', '9555250054', 1)
INSERT INTO dbo.DoiTac (TenDT, DiaChi, SDT, Loai) VALUES ('MAYBELLINE', 'China', '6902395743', 1)
GO

-- PhanLoaiSP
INSERT INTO dbo.PhanLoaiSP (TenPL) VALUES (N'Chăm sóc da mặt')
INSERT INTO dbo.PhanLoaiSP (TenPL) VALUES (N'Trang điểm')
GO

CREATE PROC USP_InsertProduct 
	@TenSP		NVARCHAR (50),
	@PhanLoai	INT, -- REF
	@Gia		INT,
	@DonViTinh	NVARCHAR (20),
	@SoLuongTon	INT,
	@XuatXu		NVARCHAR (100),
	@NhaCC		VARCHAR (10), -- REF
	@MoTa		NVARCHAR (500)
AS
BEGIN
	INSERT INTO dbo.SanPham (TenSP, PhanLoai, Gia, DonViTinh, SoLuongTon, XuatXu, NhaCC, MoTa)
	VALUES (@TenSP, @PhanLoai, @Gia, @DonViTinh, @SoLuongTon, @XuatXu, @NhaCC, @MoTa)
END
GO

EXEC USP_InsertProduct N'Mặt nạ tràm trà', 1, 23000, N'Miếng', 10, N'Đài Loan', 1, N'Kiểm Soát Dầu Và Giảm Mụn'
EXEC USP_InsertProduct N'Sữa rửa mặt Cetaphil', 1, 285000, 'Chai', 20, '', 2, N'Dịu Nhẹ Không Xà Phòng'
EXEC USP_InsertProduct N'Nước Tẩy Trang Simple', 1, 54000, 'Chai', 25, N'Anh Quốc', 3, N'Làm Sạch Trang Điểm Vượt Trội'

EXEC USP_InsertProduct N'Son Dưỡng Môi Nivea Hương Đào', 2, 51000, N'Cây', 40, N'Đức', 4, N'Peach Shine Lip Balm 4.8g'
EXEC USP_InsertProduct N'Kem Che Khuyết Điểm Silkygirl 02', 2, 133000, N'Hộp', 50, 'Malaysia', 5, N'Natural Tông Tự Nhiên 2ml'
EXEC USP_InsertProduct 'Mascara Maybelline', 2, 133000, 'Chai', 100, N'Mỹ', 6, N'Làm Dày Mi & Ngăn Rụng Mi Màu Đen 9.2ml'
GO

-- NhanVien
--INSERT INTO dbo.NhanVien (TenNV, GioiTinh, NgaySinh, NgayVaoLam, SoCMND, DiaChi, SDT, IDTK)
--	VALUES (N'Nguyễn Văn A', 'Nam', '19990609', '20190906', '987654321', N'Lâm Đồng', 1900199969, 2)
--GO

CREATE PROC USP_AddBill
	@MaTK		INT, -- REF
	@MaKH		INT, -- REF
	@NgayTT		DATETIME,
	@GiamGia	INT,
	@TongTien	FLOAT,
	@Loai		BIT
AS
BEGIN
	INSERT INTO dbo.HoaDon (MaTK, MaKH, NgayTT, GiamGia, TongTien, Loai) VALUES (@MaTK, @MaKH, @NgayTT, @GiamGia, @TongTien, @Loai)
END
GO

CREATE PROC USP_DeleteBill
	@MaHD INT
AS
BEGIN
	DELETE dbo.CTHoaDon WHERE MaHD = @MaHD
	DELETE dbo.HoaDon WHERE MaHD = @MaHD
END
GO

CREATE PROC USP_AddPartner
	@TenDT	NVARCHAR (50),
	@DiaChi	NVARCHAR (50),
	@SDT	VARCHAR (15),
	@Loai	BIT
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.DoiTac WHERE TenDT = @TenDT AND DiaChi = @DiaChi AND SDT = @SDT)
	BEGIN
		INSERT INTO dbo.DoiTac (TenDT, DiaChi, SDT, Loai) VALUES (@TenDT, @DiaChi, @SDT, @Loai)
	END
END
GO

CREATE PROC USP_AddBillInfor
	@MaHD		INT, -- REF
	@MaSP		INT, -- REF
	@SoLuong	INT
AS
BEGIN
	INSERT INTO dbo.CTHoaDon (MaHD, MaSP, SoLuong) VALUES (@MaHD, @MaSP, @SoLuong)
END
GO

CREATE PROC USP_DeleteBillInfor
	@MaHD	INT,
	@TenSP	NVARCHAR (50)
AS
BEGIN
	DECLARE @MaSP NVARCHAR (50) = (SELECT MaSP FROM dbo.SanPham WHERE TenSP = @TenSP)
	DELETE FROM dbo.CTHoaDon WHERE MaHD = @MaHD AND MaSP = @MaSP
END
GO

CREATE PROC USP_CheckOut
	@MaHD		INT,
	@TenSP		NVARCHAR (50),
	@SoLuong	INT
AS
BEGIN
	DECLARE @MaSP NVARCHAR (50) = (SELECT MaSP FROM dbo.SanPham WHERE TenSP = @TenSP)
	UPDATE dbo.CTHoaDon SET SoLuong = @SoLuong WHERE MaHD = @MaHD AND MaSP = @MaSP
	UPDATE dbo.SanPham SET SoLuongTon = SoLuongTon - @SoLuong WHERE TenSP = @TenSP
END
GO

CREATE PROC USP_ImportCheckOut
	@MaHD		INT,
	@TenSP		NVARCHAR (50),
	@SoLuong	INT
AS
BEGIN
	DECLARE @MaSP NVARCHAR (50) = (SELECT MaSP FROM dbo.SanPham WHERE TenSP = @TenSP)
	UPDATE dbo.CTHoaDon SET SoLuong = @SoLuong WHERE MaHD = @MaHD AND MaSP = @MaSP
	UPDATE dbo.SanPham SET SoLuongTon = SoLuongTon + @SoLuong WHERE TenSP = @TenSP
END
GO

CREATE FUNCTION USF_ConvertToUnsign(@strInput NVARCHAR (4000))
RETURNS NVARCHAR(4000)
AS
BEGIN
	IF @strInput IS NULL
		RETURN @strInput

	IF @strInput = ''
		RETURN @strInput

	DECLARE @RT NVARCHAR (4000)
	DECLARE @SIGN_CHARS NCHAR (136)
	DECLARE @UNSIGN_CHARS NCHAR (136)
	
	SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' + NCHAR (272) + NCHAR (208)
	SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD'
	
	DECLARE @COUNTER INT = 1
	DECLARE @COUNTER1 INT

	WHILE (@COUNTER <= LEN (@strInput))
	BEGIN
		SET @COUNTER1 = 1

			WHILE (@COUNTER1 <= LEN (@SIGN_CHARS) + 1)
			BEGIN
				IF UNICODE (SUBSTRING (@SIGN_CHARS, @COUNTER1, 1)) = UNICODE (SUBSTRING (@strInput, @COUNTER, 1))
				BEGIN
					IF @COUNTER = 1
						SET @strInput = SUBSTRING (@UNSIGN_CHARS, @COUNTER1, 1) + SUBSTRING (@strInput, @COUNTER+1, LEN (@strInput) - 1)
					ELSE
						SET @strInput = SUBSTRING (@strInput, 1, @COUNTER - 1) + SUBSTRING (@UNSIGN_CHARS, @COUNTER1, 1) + SUBSTRING (@strInput, @COUNTER + 1, LEN (@strInput) - @COUNTER)
						BREAK
				END
				SET @COUNTER1 = @COUNTER1 + 1
			END

		SET @COUNTER = @COUNTER + 1
	END

	SET @strInput = REPLACE (@strInput, ' ', '-')
	
	RETURN @strInput
END
GO

CREATE PROC USP_AddCategory
	@TenPL	NVARCHAR (50),
	@MoTa	NVARCHAR (500)
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.PhanLoaiSP WHERE TenPL = @TenPL)
	BEGIN
		INSERT INTO dbo.PhanLoaiSP (TenPL, MoTa) VALUES (@TenPL, @MoTa)
	END
END
GO

CREATE PROC USP_AddProduct
	@TenSP		NVARCHAR (50),
	@PhanLoai	INT, -- REF
	@GiaNhap	INT,
	@Gia		INT,
	@DonViTinh	NVARCHAR (20),
	@SoLuongTon	INT,
	@XuatXu		NVARCHAR (100),
	@NhaCC		INT, -- REF
	@MoTa		NVARCHAR (500)
AS
BEGIN
	IF EXISTS (SELECT * FROM dbo.SanPham WHERE TenSP = @TenSP)
	BEGIN
		UPDATE dbo.SanPham SET TrangThai = 1 WHERE TenSP = @TenSP
	END
	ELSE IF EXISTS (SELECT * FROM dbo.PhanLoaiSP WHERE MaPL = @PhanLoai AND TrangThai = 1) AND EXISTS (SELECT * FROM dbo.DoiTac WHERE MaDT = @NhaCC AND TrangThai = 1)
	BEGIN
		INSERT INTO dbo.SanPham (TenSP, PhanLoai, GiaNhap, Gia, DonViTinh, SoLuongTon, XuatXu, NhaCC, MoTa)
			VALUES (@TenSP, @PhanLoai, @GiaNhap, @Gia, @DonViTinh, @SoLuongTon, @XuatXu, @NhaCC, @MoTa)
	END

END
GO

CREATE FUNCTION USF_GetAmountByDate(@FromDate DATE, @ToDate DATE)
RETURNS INT
AS
BEGIN
	DECLARE @amount INT = 0
	SELECT @amount += SoLuong
	FROM dbo.CTHoaDon CT,	(
								SELECT MaHD
								FROM dbo.HoaDon
								WHERE NgayLap >= @FromDate AND NgayLap <= @ToDate
							) AS HD
	WHERE CT.MaHD = HD.MaHD
	RETURN @amount
END
GO

CREATE FUNCTION USF_GetProfitByDate(@FromDate DATE, @ToDate DATE)
RETURNS INT
AS
BEGIN
	DECLARE @profit INT = 0
	SELECT @profit += ((Gia - GiaNhap) * SoLuong)
	FROM dbo.CTHoaDon CT,	(
								SELECT MaHD
								FROM dbo.HoaDon
								WHERE NgayLap >= @FromDate AND NgayLap <= @ToDate
							) AS HD, dbo.SanPham SP
	WHERE CT.MaHD = HD.MaHD AND CT.MaSP = SP.MaSP AND GiaNhap IS NOT NULL AND Gia IS NOT NULL
	RETURN @profit
END
GO

CREATE PROC USP_GetListBestSellProduct
	@FromDate DATE,
	@ToDate   DATE
AS
BEGIN

SELECT DISTINCT CT.MaSP AS [ID], SP.TenSP AS [Tên sản phẩm], Temp.SoLuong AS [Số lượng]
FROM dbo.CTHoaDon CT, dbo.HoaDon HD, dbo.SanPham SP,	(
															SELECT TOP (5) MaSP, COUNT (MaSP) AS SoLuong
															FROM dbo.CTHoaDon CT
															GROUP BY MaSP
															ORDER BY SoLuong DESC
														) AS Temp
WHERE CT.MaSP IN	(
						SELECT TOP (5) MaSP
						FROM dbo.CTHoaDon CT
						GROUP BY MaSP
						ORDER BY COUNT (MaSP) DESC
					) AND CT.MaHD = HD.MaHD AND CT.MaSP = SP.MaSP AND CT.MaSP = Temp.MaSP AND HD.NgayLap >= @FromDate AND HD.NgayLap <= @ToDate

END
GO

CREATE PROC USP_GetListBadSellProduct
	@FromDate DATE,
	@ToDate   DATE
AS
BEGIN

SELECT DISTINCT CT.MaSP AS [ID], SP.TenSP AS [Tên sản phẩm], Temp.SoLuong AS [Số lượng]
FROM dbo.CTHoaDon CT, dbo.HoaDon HD, dbo.SanPham SP,	(
															SELECT TOP (5) MaSP, COUNT (MaSP) AS SoLuong
															FROM dbo.CTHoaDon CT
															GROUP BY MaSP
															ORDER BY SoLuong ASC
														) AS Temp
WHERE CT.MaSP IN	(
						SELECT TOP (5) MaSP
						FROM dbo.CTHoaDon CT
						GROUP BY MaSP
						ORDER BY COUNT (MaSP) ASC
					) AND CT.MaHD = HD.MaHD AND CT.MaSP = SP.MaSP AND CT.MaSP = Temp.MaSP AND HD.NgayLap >= @FromDate AND HD.NgayLap <= @ToDate

END
GO

SELECT * FROM dbo.TaiKhoan
SELECT * FROM dbo.DoiTac
SELECT * FROM dbo.PhanLoaiSP
SELECT * FROM dbo.SanPham
--SELECT * FROM dbo.NhanVien
SELECT * FROM dbo.HoaDon
SELECT * FROM dbo.CTHoaDon

--DROP DATABASE QuanLyCuaHangMyPham