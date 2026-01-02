USE [PetCareX_DB]
GO

--1. Kiểm tra mức giảm giá của gói tiêm phòng có nằm trong khoảng 5-15% không
CREATE OR ALTER TRIGGER trg_KiemTraMucGiamGiaGoiTiemPhong
ON VaccinationPackage
AFTER INSERT, UPDATE
AS
BEGIN
	IF EXISTS(
		SELECT vp.VPID 
		FROM VaccinationPackage vp 
		WHERE vp.DiscountRate < 0.05 OR vp.DiscountRate > 0.15)
	BEGIN
		RAISERROR (N'Mức giảm giá phải nằm trong khoảng từ 5% đến 15%', 16, 1)
	END
END

GO

--2. Kiểm tra thời hạn gói tiêm phòng có phải 6 hoặc 12 tháng hay không
CREATE OR ALTER TRIGGER trg_KiemTraNgayKetThucGoiTiemPhong
ON VaccinationPackage
AFTER INSERT, UPDATE 
AS 
BEGIN
	IF EXISTS (SELECT vp.VPID FROM VaccinationPackage vp WHERE vp.Duration <> 6 and vp.Duration <> 12)
	BEGIN
		RAISERROR(N'Gói tiêm phòng chỉ có thời hạn là 6 hoặc 12 tháng', 16, 1)
	END
END

GO
