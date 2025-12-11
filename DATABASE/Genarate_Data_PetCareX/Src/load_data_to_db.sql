-- Fix BULK INSERT configuration for UTF-8 BOM files
-- Drop old procedures
USE PetCareX_DB
GO
DROP PROCEDURE IF EXISTS sp_BulkInsertAllData;
DROP PROCEDURE IF EXISTS sp_DeleteAllData;
DROP PROCEDURE IF EXISTS sp_CheckDataStatus;
GO

CREATE PROCEDURE dbo.sp_BulkInsertAllData
    @CsvPath NVARCHAR(MAX) = 'V:\CHUYEN_NGANH\CSDLNC\Genarate_Data_PetCareX\Data\'
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @sql NVARCHAR(MAX);
    
    PRINT '========== STARTING BULK INSERT PROCESS ==========';
    PRINT 'CSV Path: ' + @CsvPath;
    
    -- Disable constraints
    ALTER TABLE dbo.AccountLogin NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Customer NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.CardMembership NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.WorkSchedule NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.LeaveRequest NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.TransferHistory NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Doctor NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Nurse NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Pet NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Examination NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Prescription NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.PrescriptionDrug NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Vaccination NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.VaccinationPackage NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.PackageVaccine NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Surgery NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.PostSurgeryMonitoring NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Orders NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.OrderDetail NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Invoice NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.InvoiceProduct NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.ApplyDiscount NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.BranchService NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.BranchProduct NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Appointment NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Review NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.InvoiceService NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Discount NOCHECK CONSTRAINT ALL;

    PRINT 'Disabling foreign key constraints...';
    
    -- BULK INSERT with UTF-8 support (CODEPAGE = 65001 for Vietnamese)
    SET @sql = 'BULK INSERT dbo.Branch FROM ''' + @CsvPath + 'Branch.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Branch';

    SET @sql = 'BULK INSERT dbo.Degree FROM ''' + @CsvPath + 'Degree.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Degree';

    SET @sql = 'BULK INSERT dbo.Certificate FROM ''' + @CsvPath + 'Certificate.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Certificate';

    SET @sql = 'BULK INSERT dbo.Employee FROM ''' + @CsvPath + 'Employee.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Employee';

    SET @sql = 'BULK INSERT dbo.WorkSchedule FROM ''' + @CsvPath + 'WorkSchedule.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into WorkSchedule';

    SET @sql = 'BULK INSERT dbo.LeaveRequest FROM ''' + @CsvPath + 'LeaveRequest.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into LeaveRequest';

    SET @sql = 'BULK INSERT dbo.TransferHistory FROM ''' + @CsvPath + 'TransferHistory.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into TransferHistory';

    SET @sql = 'BULK INSERT dbo.Doctor FROM ''' + @CsvPath + 'Doctor.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Doctor';

    SET @sql = 'BULK INSERT dbo.Nurse FROM ''' + @CsvPath + 'Nurse.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Nurse';

    SET @sql = 'BULK INSERT dbo.MembershipLevel FROM ''' + @CsvPath + 'MembershipLevel.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into MembershipLevel';

    SET @sql = 'BULK INSERT dbo.Customer FROM ''' + @CsvPath + 'Customer.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Customer';

    SET @sql = 'BULK INSERT dbo.CardMembership FROM ''' + @CsvPath + 'CardMembership.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into CardMembership';

    SET @sql = 'BULK INSERT dbo.Product FROM ''' + @CsvPath + 'Product.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Product';

    SET @sql = 'BULK INSERT dbo.Drug FROM ''' + @CsvPath + 'Drug.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Drug';

    SET @sql = 'BULK INSERT dbo.Vaccine FROM ''' + @CsvPath + 'Vaccine.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Vaccine';

    SET @sql = 'BULK INSERT dbo.Service FROM ''' + @CsvPath + 'Service.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Service';

    SET @sql = 'BULK INSERT dbo.VaccinationPackage FROM ''' + @CsvPath + 'VaccinationPackage.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into VaccinationPackage';

    SET @sql = 'BULK INSERT dbo.PackageVaccine FROM ''' + @CsvPath + 'PackageVaccine.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into PackageVaccine';

    SET @sql = 'BULK INSERT dbo.Pet FROM ''' + @CsvPath + 'Pet.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Pet';

    SET @sql = 'BULK INSERT dbo.Examination FROM ''' + @CsvPath + 'Examination.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Examination';

    SET @sql = 'BULK INSERT dbo.Prescription FROM ''' + @CsvPath + 'Prescription.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Prescription';

    SET @sql = 'BULK INSERT dbo.PrescriptionDrug FROM ''' + @CsvPath + 'PrescriptionDrug.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into PrescriptionDrug';

    SET @sql = 'BULK INSERT dbo.Vaccination FROM ''' + @CsvPath + 'Vaccination.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Vaccination';

    SET @sql = 'BULK INSERT dbo.Surgery FROM ''' + @CsvPath + 'Surgery.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Surgery';

    SET @sql = 'BULK INSERT dbo.PostSurgeryMonitoring FROM ''' + @CsvPath + 'PostSurgeryMonitoring.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into PostSurgeryMonitoring';

    SET @sql = 'BULK INSERT dbo.Discount FROM ''' + @CsvPath + 'Discount.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Discount';

    SET @sql = 'BULK INSERT dbo.Orders FROM ''' + @CsvPath + 'Orders.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Orders';

    SET @sql = 'BULK INSERT dbo.OrderDetail FROM ''' + @CsvPath + 'OrderDetail.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into OrderDetail';

    SET @sql = 'BULK INSERT dbo.PaymentMethod FROM ''' + @CsvPath + 'PaymentMethod.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into PaymentMethod';

    SET @sql = 'BULK INSERT dbo.Invoice FROM ''' + @CsvPath + 'Invoice.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Invoice';

    SET @sql = 'BULK INSERT dbo.InvoiceProduct FROM ''' + @CsvPath + 'InvoiceProduct.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into InvoiceProduct';

    SET @sql = 'BULK INSERT dbo.ApplyDiscount FROM ''' + @CsvPath + 'ApplyDiscount.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into ApplyDiscount';

    SET @sql = 'BULK INSERT dbo.BranchService FROM ''' + @CsvPath + 'BranchService.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into BranchService';

    SET @sql = 'BULK INSERT dbo.BranchProduct FROM ''' + @CsvPath + 'BranchProduct.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into BranchProduct';

    SET @sql = 'BULK INSERT dbo.Appointment FROM ''' + @CsvPath + 'Appointment.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Appointment';

    SET @sql = 'BULK INSERT dbo.Review FROM ''' + @CsvPath + 'Review.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into Review';

    SET @sql = 'BULK INSERT dbo.InvoiceService FROM ''' + @CsvPath + 'InvoiceService.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into InvoiceService';

    SET @sql = 'BULK INSERT dbo.AccountLogin FROM ''' + @CsvPath + 'AccountLogin.csv'' WITH (FIELDTERMINATOR = '','', ROWTERMINATOR = ''\n'', FIRSTROW = 2, CODEPAGE = ''65001'', TABLOCK, MAXERRORS = 1000)';
    EXEC sp_executesql @sql;
    PRINT 'Inserted into AccountLogin';
    
    -- Re-enable constraints
    PRINT 'Re-enabling foreign key constraints...';
    
    ALTER TABLE dbo.AccountLogin CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Customer CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.CardMembership CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.WorkSchedule CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.LeaveRequest CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.TransferHistory CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Doctor CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Nurse CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Pet CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Examination CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Prescription CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.PrescriptionDrug CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Vaccination CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.VaccinationPackage CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.PackageVaccine CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Surgery CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.PostSurgeryMonitoring CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Orders CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.OrderDetail CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Invoice CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.InvoiceProduct CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.ApplyDiscount CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.BranchService CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.BranchProduct CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Appointment CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Review CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.InvoiceService CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Discount CHECK CONSTRAINT ALL;

    PRINT '========== ALL DATA IMPORTED SUCCESSFULLY ==========';
END;
GO

-- ==================== STORED PROCEDURE: sp_DeleteAllData ====================
-- Delete all data from PetCareX_DB tables (in reverse FK order)

CREATE PROCEDURE dbo.sp_DeleteAllData
AS
BEGIN
    SET NOCOUNT ON;
    
    PRINT '========== STARTING DATA DELETION PROCESS ==========';
    
    -- Disable constraints
    ALTER TABLE dbo.AccountLogin NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Customer NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.CardMembership NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.WorkSchedule NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.LeaveRequest NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.TransferHistory NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Doctor NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Nurse NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Pet NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Examination NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Prescription NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.PrescriptionDrug NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Vaccination NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.VaccinationPackage NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.PackageVaccine NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Surgery NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.PostSurgeryMonitoring NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Orders NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.OrderDetail NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Invoice NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.InvoiceProduct NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.ApplyDiscount NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.BranchService NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.BranchProduct NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Appointment NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Review NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.InvoiceService NOCHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Discount NOCHECK CONSTRAINT ALL;

    PRINT 'Disabling foreign key constraints...';
    
    -- Delete in reverse order of FK dependencies
    DELETE FROM dbo.AccountLogin; PRINT 'Deleted from AccountLogin';
    DELETE FROM dbo.InvoiceService; PRINT 'Deleted from InvoiceService';
    DELETE FROM dbo.Review; PRINT 'Deleted from Review';
    DELETE FROM dbo.Appointment; PRINT 'Deleted from Appointment';
    DELETE FROM dbo.BranchProduct; PRINT 'Deleted from BranchProduct';
    DELETE FROM dbo.BranchService; PRINT 'Deleted from BranchService';
    DELETE FROM dbo.ApplyDiscount; PRINT 'Deleted from ApplyDiscount';
    DELETE FROM dbo.InvoiceProduct; PRINT 'Deleted from InvoiceProduct';
    DELETE FROM dbo.Invoice; PRINT 'Deleted from Invoice';
    DELETE FROM dbo.OrderDetail; PRINT 'Deleted from OrderDetail';
    DELETE FROM dbo.Orders; PRINT 'Deleted from Orders';
    DELETE FROM dbo.PostSurgeryMonitoring; PRINT 'Deleted from PostSurgeryMonitoring';
    DELETE FROM dbo.Surgery; PRINT 'Deleted from Surgery';
    DELETE FROM dbo.PackageVaccine; PRINT 'Deleted from PackageVaccine';
    DELETE FROM dbo.VaccinationPackage; PRINT 'Deleted from VaccinationPackage';
    DELETE FROM dbo.Vaccination; PRINT 'Deleted from Vaccination';
    DELETE FROM dbo.PrescriptionDrug; PRINT 'Deleted from PrescriptionDrug';
    DELETE FROM dbo.Prescription; PRINT 'Deleted from Prescription';
    DELETE FROM dbo.Examination; PRINT 'Deleted from Examination';
    DELETE FROM dbo.Pet; PRINT 'Deleted from Pet';
    DELETE FROM dbo.CardMembership; PRINT 'Deleted from CardMembership';
    DELETE FROM dbo.Discount; PRINT 'Deleted from Discount';
    DELETE FROM dbo.TransferHistory; PRINT 'Deleted from TransferHistory';
    DELETE FROM dbo.LeaveRequest; PRINT 'Deleted from LeaveRequest';
    DELETE FROM dbo.WorkSchedule; PRINT 'Deleted from WorkSchedule';
    DELETE FROM dbo.Service; PRINT 'Deleted from Service';
    DELETE FROM dbo.Nurse; PRINT 'Deleted from Nurse';
    DELETE FROM dbo.Doctor; PRINT 'Deleted from Doctor';
    DELETE FROM dbo.Employee; PRINT 'Deleted from Employee';
    DELETE FROM dbo.Vaccine; PRINT 'Deleted from Vaccine';
    DELETE FROM dbo.Drug; PRINT 'Deleted from Drug';
    DELETE FROM dbo.Product; PRINT 'Deleted from Product';
    DELETE FROM dbo.Customer; PRINT 'Deleted from Customer';
    DELETE FROM dbo.MembershipLevel; PRINT 'Deleted from MembershipLevel';
    DELETE FROM dbo.Degree; PRINT 'Deleted from Degree';
    DELETE FROM dbo.Certificate; PRINT 'Deleted from Certificate';
    DELETE FROM dbo.Branch; PRINT 'Deleted from Branch';
    DELETE FROM dbo.PaymentMethod; PRINT 'Deleted from PaymentMethod';
    
    -- Re-enable constraints
    PRINT 'Re-enabling foreign key constraints...';
    
    ALTER TABLE dbo.AccountLogin CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Customer CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.CardMembership CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.WorkSchedule CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.LeaveRequest CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.TransferHistory CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Doctor CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Nurse CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Pet CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Examination CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Prescription CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.PrescriptionDrug CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Vaccination CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.VaccinationPackage CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.PackageVaccine CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Surgery CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.PostSurgeryMonitoring CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Orders CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.OrderDetail CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Invoice CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.InvoiceProduct CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.ApplyDiscount CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.BranchService CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.BranchProduct CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Appointment CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Review CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.InvoiceService CHECK CONSTRAINT ALL;
    ALTER TABLE dbo.Discount CHECK CONSTRAINT ALL;

    PRINT '========== ALL DATA DELETED SUCCESSFULLY ==========';
END;
GO

-- ==================== STORED PROCEDURE: sp_CheckDataStatus ====================
-- Check record counts in all 38 tables

CREATE PROCEDURE dbo.sp_CheckDataStatus
AS
BEGIN
    SET NOCOUNT ON;
    
    PRINT '========== DATABASE TABLE STATUS CHECK ==========';
    PRINT 'Table Name' + REPLICATE(' ', 30) + 'Record Count';
    PRINT REPLICATE('=', 55);
    
    DECLARE @TableName NVARCHAR(50);
    DECLARE @RecordCount INT;
    DECLARE @TotalRecords BIGINT = 0;
    
    CREATE TABLE #TableStats (
        TableName NVARCHAR(50),
        RecordCount INT
    );
    
    -- Check all tables
    INSERT INTO #TableStats VALUES ('Branch', (SELECT COUNT(*) FROM dbo.Branch));
    INSERT INTO #TableStats VALUES ('Degree', (SELECT COUNT(*) FROM dbo.Degree));
    INSERT INTO #TableStats VALUES ('Certificate', (SELECT COUNT(*) FROM dbo.Certificate));
    INSERT INTO #TableStats VALUES ('Employee', (SELECT COUNT(*) FROM dbo.Employee));
    INSERT INTO #TableStats VALUES ('WorkSchedule', (SELECT COUNT(*) FROM dbo.WorkSchedule));
    INSERT INTO #TableStats VALUES ('LeaveRequest', (SELECT COUNT(*) FROM dbo.LeaveRequest));
    INSERT INTO #TableStats VALUES ('TransferHistory', (SELECT COUNT(*) FROM dbo.TransferHistory));
    INSERT INTO #TableStats VALUES ('Doctor', (SELECT COUNT(*) FROM dbo.Doctor));
    INSERT INTO #TableStats VALUES ('Nurse', (SELECT COUNT(*) FROM dbo.Nurse));
    INSERT INTO #TableStats VALUES ('MembershipLevel', (SELECT COUNT(*) FROM dbo.MembershipLevel));
    INSERT INTO #TableStats VALUES ('Customer', (SELECT COUNT(*) FROM dbo.Customer));
    INSERT INTO #TableStats VALUES ('CardMembership', (SELECT COUNT(*) FROM dbo.CardMembership));
    INSERT INTO #TableStats VALUES ('Product', (SELECT COUNT(*) FROM dbo.Product));
    INSERT INTO #TableStats VALUES ('Drug', (SELECT COUNT(*) FROM dbo.Drug));
    INSERT INTO #TableStats VALUES ('Vaccine', (SELECT COUNT(*) FROM dbo.Vaccine));
    INSERT INTO #TableStats VALUES ('Service', (SELECT COUNT(*) FROM dbo.Service));
    INSERT INTO #TableStats VALUES ('VaccinationPackage', (SELECT COUNT(*) FROM dbo.VaccinationPackage));
    INSERT INTO #TableStats VALUES ('PackageVaccine', (SELECT COUNT(*) FROM dbo.PackageVaccine));
    INSERT INTO #TableStats VALUES ('Pet', (SELECT COUNT(*) FROM dbo.Pet));
    INSERT INTO #TableStats VALUES ('Examination', (SELECT COUNT(*) FROM dbo.Examination));
    INSERT INTO #TableStats VALUES ('Prescription', (SELECT COUNT(*) FROM dbo.Prescription));
    INSERT INTO #TableStats VALUES ('PrescriptionDrug', (SELECT COUNT(*) FROM dbo.PrescriptionDrug));
    INSERT INTO #TableStats VALUES ('Vaccination', (SELECT COUNT(*) FROM dbo.Vaccination));
    INSERT INTO #TableStats VALUES ('Surgery', (SELECT COUNT(*) FROM dbo.Surgery));
    INSERT INTO #TableStats VALUES ('PostSurgeryMonitoring', (SELECT COUNT(*) FROM dbo.PostSurgeryMonitoring));
    INSERT INTO #TableStats VALUES ('Discount', (SELECT COUNT(*) FROM dbo.Discount));
    INSERT INTO #TableStats VALUES ('Orders', (SELECT COUNT(*) FROM dbo.Orders));
    INSERT INTO #TableStats VALUES ('OrderDetail', (SELECT COUNT(*) FROM dbo.OrderDetail));
    INSERT INTO #TableStats VALUES ('PaymentMethod', (SELECT COUNT(*) FROM dbo.PaymentMethod));
    INSERT INTO #TableStats VALUES ('Invoice', (SELECT COUNT(*) FROM dbo.Invoice));
    INSERT INTO #TableStats VALUES ('InvoiceProduct', (SELECT COUNT(*) FROM dbo.InvoiceProduct));
    INSERT INTO #TableStats VALUES ('ApplyDiscount', (SELECT COUNT(*) FROM dbo.ApplyDiscount));
    INSERT INTO #TableStats VALUES ('BranchService', (SELECT COUNT(*) FROM dbo.BranchService));
    INSERT INTO #TableStats VALUES ('BranchProduct', (SELECT COUNT(*) FROM dbo.BranchProduct));
    INSERT INTO #TableStats VALUES ('Appointment', (SELECT COUNT(*) FROM dbo.Appointment));
    INSERT INTO #TableStats VALUES ('Review', (SELECT COUNT(*) FROM dbo.Review));
    INSERT INTO #TableStats VALUES ('InvoiceService', (SELECT COUNT(*) FROM dbo.InvoiceService));
    INSERT INTO #TableStats VALUES ('AccountLogin', (SELECT COUNT(*) FROM dbo.AccountLogin));
    
    -- Print results
    DECLARE cur CURSOR FOR
    SELECT TableName, RecordCount FROM #TableStats ORDER BY TableName;
    
    OPEN cur;
    FETCH NEXT FROM cur INTO @TableName, @RecordCount;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        PRINT @TableName + REPLICATE(' ', 35 - LEN(@TableName)) + CAST(@RecordCount AS VARCHAR(20));
        SET @TotalRecords = @TotalRecords + @RecordCount;
        FETCH NEXT FROM cur INTO @TableName, @RecordCount;
    END
    
    CLOSE cur;
    DEALLOCATE cur;
    
    PRINT REPLICATE('=', 55);
    PRINT 'TOTAL RECORDS' + REPLICATE(' ', 22) + CAST(@TotalRecords AS VARCHAR(20));
    PRINT '========== STATUS CHECK COMPLETE ==========';
    
    DROP TABLE #TableStats;
END;
GO

-- ==================== USAGE ====================
-- Execute the stored procedures:
-- EXEC sp_BulkInsertAllData;        -- Insert all data from CSV files
-- EXEC sp_DeleteAllData;             -- Delete all data from tables
-- EXEC sp_CheckDataStatus;           -- Check record counts in all tables
-- 
-- Or with custom path for insert:
-- EXEC sp_BulkInsertAllData @CsvPath = 'C:\Data\';

EXEC sp_DeleteAllData
EXEC sp_BulkInsertAllData
