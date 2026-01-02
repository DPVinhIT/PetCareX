--CN1) Bác sĩ xem lịch khám được phân công
CREATE PROCEDURE sp_BS_XemLichKham
(
    @RID NVARCHAR(10),        
    @FromDate DATE = NULL,    
    @ToDate DATE = NULL       
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra bác sĩ có lịch hay không
    IF NOT EXISTS (
        SELECT 1 
        FROM APPOINTMENT
        WHERE RID = @RID
    )
    BEGIN
        PRINT N'Bác sĩ chưa được phân công ca khám nào.';
        RETURN;
    END

    SELECT
        AppointmentID,
        [Date]       AS NgayKham,
        [Time]       AS GioKham,
        Room         AS Phong,
        BranchID     AS ChiNhanh,
        ServiceID    AS DichVu,
        CustomerID   AS KhachHang
    FROM APPOINTMENT
    WHERE
        RID = @RID
        AND (
            (@FromDate IS NULL AND @ToDate IS NULL)
            OR
            ([Date] BETWEEN @FromDate AND @ToDate)
        )
    ORDER BY
        [Date],
        [Time];
END
GO



--2)
CREATE PROCEDURE sp_BS_ThemHoSoKham
(
    @EID            NVARCHAR(10),
    @PetID          NVARCHAR(10),
    @Symptoms       NVARCHAR(255),
    @Diagnosis      NVARCHAR(255),
    @FollowUpDate   DATE = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- 1. Kiểm tra EID đã tồn tại trong Examination chưa
        IF EXISTS (
            SELECT 1 FROM Examination WHERE EID = @EID
        )
        BEGIN
            THROW 50001, N'Hồ sơ khám với EID này đã tồn tại.', 1;
        END

        -- 2. Kiểm tra EID có tồn tại trong Service không
        IF NOT EXISTS (
            SELECT 1 FROM Service WHERE ServiceID = @EID
        )
        BEGIN
            THROW 50002, N'EID không tồn tại trong danh sách dịch vụ.', 1;
        END

        -- 3. Kiểm tra Pet tồn tại
        IF NOT EXISTS (
            SELECT 1 FROM Pet WHERE PetID = @PetID
        )
        BEGIN
            THROW 50003, N'Không tồn tại thú cưng với PetID đã cho.', 1;
        END

        -- 4. Thêm hồ sơ khám
        INSERT INTO Examination
        (
            EID,
            PetID,
            ExaminationDate,
            Symptoms,
            Diagnoses,
            FollowUpDate
        )
        VALUES
        (
            @EID,
            @PetID,
            GETDATE(),
            @Symptoms,
            @Diagnosis,
            @FollowUpDate
        );

    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO



--drop procedure sp_BS_KeToaThuoc

CREATE PROCEDURE sp_BS_KeToaThuoc
(
    --@PrescriptionID VARCHAR(20),
    @EID VARCHAR(20),
    @Note NVARCHAR(255),
    @DrugList DrugListType READONLY 
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- 1. Kiểm tra hồ sơ khám
        IF NOT EXISTS (SELECT 1 FROM Examination WHERE EID = @EID) AND NOT EXISTS (SELECT 1 FROM Surgery WHERE SurgeryID = @EID)
        BEGIN
            RAISERROR (N'Hồ sơ khám không tồn tại.', 16, 1);
        END

        -- Tạo ID tăng dần
		DECLARE @maxID NVARCHAR(50), @PrescriptionID VARCHAR(20)
		SELECT @maxID = MAX(PrescriptionID) FROM Prescription
		--Nếu chưa tồn tại mã khách hàng nào
		IF @maxID IS NULL
		BEGIN
			SET @PrescriptionID = 'PRSC000001'
		END
		--Nếu đã tồn tại mã khách hàng từ trước trong hệ thống
		ELSE 
		BEGIN 
			DECLARE @num INT = CAST(RIGHT(@maxID, 6) AS INT) + 1;
			SET @PrescriptionID = 'PRSC' + RIGHT('000000' + CAST(@num AS VARCHAR(10)), 6)
		END

        -- -- 2. Tránh trùng toa
        -- IF EXISTS (SELECT 1 FROM Prescription WHERE PrescriptionID = @PrescriptionID)
        -- BEGIN
        --     RAISERROR (N'Toa thuốc đã tồn tại.', 16, 1);
        -- END

        -- 3. Tạo toa thuốc
        INSERT INTO Prescription
        (
            PrescriptionID,
            EID,
            CreateDate,
            Note
        )
        VALUES
        (
            @PrescriptionID,
            @EID,
            GETDATE(),
            @Note
        );

        
        -- Kiểm tra danh sách thuốc không rỗng
   
        IF NOT EXISTS (SELECT 1 FROM @DrugList)
        BEGIN
            RAISERROR (N'Danh sách thuốc phải có ít nhất 1 thuốc.', 16, 1);
        END

        -- 5. Thêm thuốc vào toa
        INSERT INTO PrescriptionDrug
        (
            PrescriptionID,
            DrugID,
            Quantity,
            UsageInstruction
        )
        SELECT
            @PrescriptionID,
            DrugID,
            Quantity,
            UsageInstruction
        FROM @DrugList;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR (@ErrMsg, 16, 1);
    END CATCH
END
GO


--drop procedure sp_BS_ThucHienTiemPhong
--4)


CREATE PROCEDURE sp_BS_ThucHienTiemPhong
(
    @VID VARCHAR(20),        -- ServiceID
    --@PetID VARCHAR(20),      -- Mã thú cưng
    @VaccineID VARCHAR(20),
    --@VaccinationDate DATETIME,
    @Dosage NVARCHAR(50)
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra dịch vụ tiêm phòng tồn tại
    IF NOT EXISTS (SELECT 1 FROM Service WHERE ServiceID = @VID)
    BEGIN
        RAISERROR (N'Dịch vụ tiêm phòng không tồn tại.', 16, 1);
        RETURN;
    END

    -- Kiểm tra thú cưng tồn tại
    -- IF NOT EXISTS (SELECT 1 FROM Pet WHERE PetID = @PetID)
    -- BEGIN
    --     RAISERROR (N'Thú cưng không tồn tại.', 16, 1);
    --     RETURN;
    -- END

    -- Kiểm tra vaccine tồn tại
    IF NOT EXISTS (SELECT 1 FROM Vaccine WHERE VaccineID = @VaccineID)
    BEGIN
        RAISERROR (N'Vắc-xin không tồn tại.', 16, 1);
        RETURN;
    END

    -- Tạo bản ghi tiêm phòng
    INSERT INTO Vaccination
    (
        VID,
        VaccineID,
        VaccinationDate,
        Dosage
    )
    VALUES
    (
        @VID,
        @VaccineID,
        GETDATE(),
        @Dosage
    );
    
END
GO

--select * from Vaccination where VID = 'SRV0006'

--drop PROCEDURE sp_BS_CapNhatHoSoPhauThuat

--5)
CREATE PROCEDURE sp_BS_CapNhatHoSoPhauThuat
(
    @SurgeryID        VARCHAR(20),
    @PetID            VARCHAR(20),
    @SurgeryType      NVARCHAR(100),
    @AnesthesiaType   NVARCHAR(100),
    --@SurgeryDate      DATETIME,
    @SurgeryStatus    NVARCHAR(100),
    @DiagnosisNote    NVARCHAR(200)
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra ca phẫu thuật đã tồn tại chưa
    IF EXISTS (
        SELECT 1 FROM Surgery WHERE SurgeryID = @SurgeryID
    )
    BEGIN
        RAISERROR (N'Mã ca phẫu thuật đã tồn tại.', 16, 1);
        RETURN;
    END
    IF NOT EXISTS (SELECT 1 FROM Service WHERE ServiceID = @SurgeryID)
    BEGIN
        RAISERROR (N'Dịch vụ phẫu thuật không tồn tại.', 16, 1);
        RETURN;
    END
     --Kiểm tra thú cưng tồn tại
     IF NOT EXISTS (SELECT 1 FROM Pet WHERE PetID = @PetID)
     BEGIN
         RAISERROR (N'Thú cưng không tồn tại.', 16, 1);
         RETURN;
     END
    -- Thêm hồ sơ phẫu thuật
    INSERT INTO Surgery
    (
        SurgeryID,
        PetID,
        SurgeryType,
        AnesthesiaType,
        SurgeryDate,
        SurgeryStatus,
        DiagnosisNote
    )
    VALUES
    (
        @SurgeryID,
        @PetID,
        @SurgeryType,
        @AnesthesiaType,
        GETDATE(),
        @SurgeryStatus,
        @DiagnosisNote
    );
END
GO

--select * from Surgery where SurgeryID = 'SRV0006'

--6)
--drop PROCEDURE sp_BS_LichSuPet

CREATE PROCEDURE sp_BS_LichSuPet
(
    @PetID VARCHAR(20),
    @FromDate DATE = NULL,
    @ToDate DATE = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra thú cưng tồn tại
    IF NOT EXISTS (SELECT 1 FROM Pet WHERE PetID = @PetID)
    BEGIN
        RAISERROR (N'Thú cưng không tồn tại.', 16, 1);
        RETURN;
    END

    -- Lịch sử khám
    SELECT
        e.PetID,
        e.EID               AS ServiceID,
        e.ExaminationDate   AS DateService,
        N'Examination'      AS ServiceType,
        p.PrescriptionID
    FROM Examination e
    JOIN Prescription p ON e.EID = p.EID
    WHERE e.PetID = @PetID
      AND (@FromDate IS NULL OR e.ExaminationDate >= @FromDate)
      AND (@ToDate   IS NULL OR e.ExaminationDate < DATEADD(DAY, 1, @ToDate))

    UNION ALL

    -- Lịch sử phẫu thuật
    SELECT
        s.PetID,
        s.SurgeryID         AS ServiceID,
        s.SurgeryDate       AS [Date],
        N'Surgery'          AS ServiceType,
        p.PrescriptionID
    FROM Surgery s
    JOIN Prescription p ON s.SurgeryID = p.EID  -- kiểm tra lại khóa Prescription
    WHERE s.PetID = @PetID
      AND (@FromDate IS NULL OR s.SurgeryDate >= @FromDate)
      AND (@ToDate   IS NULL OR s.SurgeryDate < DATEADD(DAY, 1, @ToDate))

END
GO
--select * from Examination where PetID = 'PET00001'
--SELECT
--        e.PetID,
--        e.EID               AS ServiceID,
--        e.ExaminationDate   AS CXZCX,
--        N'Examination'      AS ServiceType,
--        p.PrescriptionID	AS PrescriptionID
--    FROM Examination e
--    LEFT JOIN Prescription p ON e.EID = p.EID
--    WHERE e.PetID = 'PET56731'
--      AND (@FromDate IS NULL OR e.ExaminationDate >= @FromDate)
--      AND (@ToDate   IS NULL OR e.ExaminationDate < DATEADD(DAY, 1, @ToDate))
--SELECT * FROM Examination WHERE PetID = 'PET56731'
--SELECT * FROM Prescription JOIN Examination ON Prescription.EID = Examination.EID WHERE Examination.PetID = 'PET56731'

--EXEC sp_BS_LichSuPet @PetID = 'PET55432'
