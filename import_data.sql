-- Script Import dữ liệu từ CSV vào PetCareX_DB
-- Chạy script này sau khi đã tạo xong tất cả tables

USE PetCareX_DB;
GO

-- Tắt kiểm tra foreign key constraints tạm thời
EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';
GO

PRINT '========== BẮT ĐẦU IMPORT DỮ LIỆU ==========';

-- 1. Branch (không có FK)
BULK INSERT dbo.Branch 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Branch.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Branch';
GO

-- 2. Degree (không có FK)
BULK INSERT dbo.Degree 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Degree.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Degree';
GO

-- 3. Certificate (không có FK)
BULK INSERT dbo.Certificate 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Certificate.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Certificate';
GO

-- 4. MembershipLevel (không có FK)
BULK INSERT dbo.MembershipLevel 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\MembershipLevel.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported MembershipLevel';
GO

-- 5. Product (không có FK)
BULK INSERT dbo.Product 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Product.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Product';
GO

-- 6. Drug (không có FK)
BULK INSERT dbo.Drug 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Drug.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Drug';
GO

-- 7. Vaccine (không có FK)
BULK INSERT dbo.Vaccine 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Vaccine.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Vaccine';
GO

-- 8. Service (có FK đến Degree)
BULK INSERT dbo.Service 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Service.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Service';
GO

-- 9. Discount (không có FK)
BULK INSERT dbo.Discount 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Discount.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Discount';
GO

-- 10. Employee (có FK đến Branch, Degree, Certificate)
BULK INSERT dbo.Employee 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Employee.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Employee';
GO

-- 11. Doctor (có FK đến Employee)
BULK INSERT dbo.Doctor 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Doctor.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Doctor';
GO

-- 12. Nurse (có FK đến Employee)
BULK INSERT dbo.Nurse 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Nurse.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Nurse';
GO

-- 13. AccountLogin (không có FK)
BULK INSERT dbo.AccountLogin 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\AccountLogin.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported AccountLogin';
GO

-- 14. Customer (có FK đến AccountLogin)
BULK INSERT dbo.Customer 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Customer.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Customer';
GO

-- 15. CardMembership (có FK đến Customer, MembershipLevel)
BULK INSERT dbo.CardMembership 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\CardMembership.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported CardMembership';
GO

-- 16. Pet (có FK đến Customer)
BULK INSERT dbo.Pet 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Pet.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Pet';
GO

-- 17. BranchService (có FK đến Branch, Service)
BULK INSERT dbo.BranchService 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\BranchService.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported BranchService';
GO

-- 18. BranchProduct (có FK đến Branch, Product)
BULK INSERT dbo.BranchProduct 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\BranchProduct.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported BranchProduct';
GO

-- 19. WorkSchedule (có FK đến Employee)
BULK INSERT dbo.WorkSchedule 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\WorkSchedule.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported WorkSchedule';
GO

-- 20. LeaveRequest (có FK đến Employee)
BULK INSERT dbo.LeaveRequest 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\LeaveRequest.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported LeaveRequest';
GO

-- 21. TransferHistory (có FK đến Employee, Branch)
BULK INSERT dbo.TransferHistory 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\TransferHistory.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported TransferHistory';
GO

-- 22. Appointment (có FK đến Customer, Branch, Service)
BULK INSERT dbo.Appointment 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Appointment.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Appointment';
GO

-- 23. Examination (có FK đến Pet, Doctor)
BULK INSERT dbo.Examination 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Examination.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Examination';
GO

-- 24. Prescription (có FK đến Examination)
BULK INSERT dbo.Prescription 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Prescription.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Prescription';
GO

-- 25. PrescriptionDrug (có FK đến Prescription, Drug)
BULK INSERT dbo.PrescriptionDrug 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\PrescriptionDrug.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported PrescriptionDrug';
GO

-- 26. VaccinationPackage
BULK INSERT dbo.VaccinationPackage 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\VaccinationPackage.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported VaccinationPackage';
GO

-- 27. PackageVaccine
BULK INSERT dbo.PackageVaccine 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\PackageVaccine.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported PackageVaccine';
GO

-- 28. Vaccination
BULK INSERT dbo.Vaccination 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Vaccination.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Vaccination';
GO

-- 29. Surgery
BULK INSERT dbo.Surgery 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Surgery.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Surgery';
GO

-- 30. PostSurgeryMonitoring
BULK INSERT dbo.PostSurgeryMonitoring 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\PostSurgeryMonitoring.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported PostSurgeryMonitoring';
GO

-- 31. Orders
BULK INSERT dbo.Orders 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Orders.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Orders';
GO

-- 32. OrderDetail
BULK INSERT dbo.OrderDetail 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\OrderDetail.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported OrderDetail';
GO

-- 33. Invoice
BULK INSERT dbo.Invoice 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Invoice.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Invoice';
GO

-- 34. InvoiceProduct
BULK INSERT dbo.InvoiceProduct 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\InvoiceProduct.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported InvoiceProduct';
GO

-- 35. InvoiceService
BULK INSERT dbo.InvoiceService 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\InvoiceService.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported InvoiceService';
GO

-- 36. ApplyDiscount
BULK INSERT dbo.ApplyDiscount 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\ApplyDiscount.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported ApplyDiscount';
GO

-- 37. Review
BULK INSERT dbo.Review 
FROM 'C:\Users\Admin\Desktop\UI CSDL NC\Data\Review.csv' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, CODEPAGE = '65001');
PRINT '✓ Imported Review';
GO

-- Bật lại foreign key constraints
EXEC sp_MSforeachtable 'ALTER TABLE ? CHECK CONSTRAINT ALL';
GO

PRINT '========== IMPORT HOÀN TẤT ==========';

-- Kiểm tra số lượng records
SELECT 'Branch' AS TableName, COUNT(*) AS Records FROM Branch
UNION ALL SELECT 'Product', COUNT(*) FROM Product
UNION ALL SELECT 'Customer', COUNT(*) FROM Customer
UNION ALL SELECT 'Service', COUNT(*) FROM Service
UNION ALL SELECT 'Employee', COUNT(*) FROM Employee
UNION ALL SELECT 'Orders', COUNT(*) FROM Orders;
GO
