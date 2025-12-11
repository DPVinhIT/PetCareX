CREATE DATABASE PetCareX_DB;
GO

USE PetCareX_DB;
GO

-- =========================
-- 1. ACCOUNT LOGIN
-- =========================
CREATE TABLE AccountLogin (
    Username VARCHAR(20) PRIMARY KEY,
    Password NVARCHAR(255) NOT NULL
);
GO

-- =========================
-- 2. BRANCH
-- =========================
CREATE TABLE Branch (
    BranchID VARCHAR(20) PRIMARY KEY,
    BranchName NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255),
    PhoneNumber VARCHAR(15),
    Email VARCHAR(100),
    OpenTime INT,
    CloseTime INT
);
GO

-- =========================
-- 3. EMPLOYEE
-- =========================
CREATE TABLE Employee (
    EmployeeID   VARCHAR(20) PRIMARY KEY,
    FullName     NVARCHAR(100) NOT NULL,
    Birthday     DATE,
    Gender       NVARCHAR(10),
    PhoneNumber  VARCHAR(15),
    StartDate    DATE,
    BaseSalary   DECIMAL(18, 2),
    MID          VARCHAR(20),       -- Manager ID
    Role         NVARCHAR(50),
    Username VARCHAR(20),       -- liên kết tới ACCOUNT_LOGIN
    FOREIGN KEY (MID)          REFERENCES Employee(EmployeeID),
    FOREIGN KEY (Username) REFERENCES AccountLogin(Username)
);
GO

-- =========================
-- 4. WORK SCHEDULE
-- =========================
CREATE TABLE WorkSchedule (
    EmployeeID VARCHAR(20),
    WorkDate   DATE,
    WorkTime   INT,
    Shift      NVARCHAR(50),
    MID        VARCHAR(20),
    PRIMARY KEY (EmployeeID, WorkDate, WorkTime),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (MID)        REFERENCES Employee(EmployeeID)
);
GO

-- =========================
-- 5. LEAVE REQUEST
-- =========================
CREATE TABLE LeaveRequest (
    EmployeeID VARCHAR(20),
    StartDate  DATE,
    EndDate    DATE,
    Reason     NVARCHAR(255),
    Status     NVARCHAR(50),
    MID        VARCHAR(20),
    PRIMARY KEY (EmployeeID, StartDate, EndDate),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (MID)        REFERENCES Employee(EmployeeID)
);
GO

-- =========================
-- 6. TRANSFER HISTORY
-- =========================
CREATE TABLE TransferHistory (
    BranchID   VARCHAR(20),
    EmployeeID VARCHAR(20),
    StartDate  DATE,
    EndDate    DATE,
    PRIMARY KEY (BranchID, EmployeeID),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (BranchID)   REFERENCES Branch(BranchID)
);
GO

-- =========================
-- 7. DEGREE & CERTIFICATE
-- =========================
CREATE TABLE Degree (
    DegreeID       VARCHAR(20) PRIMARY KEY,
    Major          NVARCHAR(100),
    School         NVARCHAR(100),
    GraduationYear INT
);
GO

CREATE TABLE Certificate (
    CertificateID     VARCHAR(20) PRIMARY KEY,
    Name              NVARCHAR(100),
    IssueDate         DATE,
    IssueOrganization NVARCHAR(200)
);
GO

-- =========================
-- 8. DOCTOR & NURSE
-- =========================
CREATE TABLE Doctor (
    DID          VARCHAR(20) PRIMARY KEY,
    Specialization NVARCHAR(100),
    EducationLevel NVARCHAR(100),
    DegreeID     VARCHAR(20),
    CerticateID  VARCHAR(20),
    FOREIGN KEY (DID)         REFERENCES Employee(EmployeeID),
    FOREIGN KEY (DegreeID)    REFERENCES Degree(DegreeID),
    FOREIGN KEY (CerticateID) REFERENCES Certificate(CertificateID)
);
GO

CREATE TABLE Nurse (
    NID            VARCHAR(20) PRIMARY KEY,
    Specialization NVARCHAR(100),
    EducationLevel NVARCHAR(100),
    DegreeID       VARCHAR(20),
    FOREIGN KEY (NID)      REFERENCES Employee(EmployeeID),
    FOREIGN KEY (DegreeID) REFERENCES Degree(DegreeID)
);
GO

-- =========================
-- 9. CUSTOMER & MEMBERSHIP
-- =========================
CREATE TABLE Customer (
    CustomerID   VARCHAR(20) PRIMARY KEY,
    FullName     NVARCHAR(100),
    PhoneNumber  VARCHAR(15),
    Email        VARCHAR(100),
    CCCD         VARCHAR(20) UNIQUE,
    Gender       NVARCHAR(10),
    Birthday     DATE,
    Username VARCHAR(20),   -- liên kết tới ACCOUNT_LOGIN
    FOREIGN KEY (Username) REFERENCES AccountLogin(Username)
);
GO

CREATE TABLE MembershipLevel (
    LevelID                 VARCHAR(10) PRIMARY KEY,
    LevelName               NVARCHAR(50),
    AnnualSpendingThreshold INT,
    RetentionThreshold      INT,
    DiscountRate            FLOAT
);
GO

CREATE TABLE CardMembership (
    CardID          VARCHAR(20) PRIMARY KEY,
    RegistrationDate DATE,
    LoyalPoint      INT,
    LevelID         VARCHAR(10),
    CustomerID      VARCHAR(20),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (LevelID)    REFERENCES MembershipLevel(LevelID)
);
GO

-- =========================
-- 10. PET
-- =========================
CREATE TABLE Pet (
    PetID        VARCHAR(20) PRIMARY KEY,
    CustomerID   VARCHAR(20),
    PetName      NVARCHAR(50),
    Species      NVARCHAR(50),
    Breed        NVARCHAR(50),
    Birthday     DATE,
    Gender       NVARCHAR(10),
    HealthStatus NVARCHAR(255),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);
GO

-- =========================
-- 11. PRODUCT / DRUG / VACCINE
-- =========================
CREATE TABLE Product (
    ProductID    VARCHAR(20) PRIMARY KEY,
    ProductName  NVARCHAR(100),
    ProductType  NVARCHAR(50),
    SellingPrice DECIMAL(18, 2)
);
GO

CREATE TABLE Drug (
    DrugID   VARCHAR(20) PRIMARY KEY,
    DrugName NVARCHAR(100),
    DrugType NVARCHAR(100)
);
GO

CREATE TABLE Vaccine (
    VaccineID      VARCHAR(20) PRIMARY KEY,
    VaccineName    NVARCHAR(100),
    ExpiryDate     DATE,
    ProductionDate DATE,
    VaccineType    NVARCHAR(100),
    Manufacturer   NVARCHAR(100),
    SideEffects    NVARCHAR(255)
);
GO

-- =========================
-- 12. SERVICE & VACCINATION PACKAGE
-- =========================
CREATE TABLE Service (
    ServiceID          VARCHAR(20) PRIMARY KEY,
    ServiceName        NVARCHAR(100),
    ServiceDescription NVARCHAR(200),
    DID                VARCHAR(20),
    FOREIGN KEY (DID) REFERENCES Doctor(DID)
);
GO

CREATE TABLE VaccinationPackage (
    VPID         VARCHAR(20) PRIMARY KEY,
    Duration     INT,
    DiscountRate FLOAT,
    FOREIGN KEY (VPID) REFERENCES Service(ServiceID)
);
GO

CREATE TABLE PackageVaccine (
    VPID      VARCHAR(20),
    VaccineID VARCHAR(20),
    Quantity  INT,
    PRIMARY KEY (VPID, VaccineID),
    FOREIGN KEY (VPID)     REFERENCES VaccinationPackage(VPID),
    FOREIGN KEY (VaccineID) REFERENCES Vaccine(VaccineID)
);
GO

-- =========================
-- 13. EXAMINATION & PRESCRIPTION
-- =========================
CREATE TABLE Examination (
    EID             VARCHAR(20) PRIMARY KEY,
    PetID           VARCHAR(20),
    ExaminationDate DATETIME,
    Symptoms        NVARCHAR(MAX),
    Diagnoses       NVARCHAR(MAX),
    FollowUpDate    DATE,
    FOREIGN KEY (PetID) REFERENCES Pet(PetID)
);
GO

CREATE TABLE Prescription (
    PrescriptionID VARCHAR(20) PRIMARY KEY,
    EID            VARCHAR(20),
    CreateDate     DATETIME,
    Note           NVARCHAR(255),
    FOREIGN KEY (EID) REFERENCES Examination(EID)
);
GO

CREATE TABLE PrescriptionDrug (
    PrescriptionID   VARCHAR(20),
    DrugID           VARCHAR(20),
    Quantity         INT,
    UsageInstruction NVARCHAR(100),
    PRIMARY KEY (PrescriptionID, DrugID),
    FOREIGN KEY (PrescriptionID) REFERENCES Prescription(PrescriptionID),
    FOREIGN KEY (DrugID)         REFERENCES Drug(DrugID)
);
GO

-- =========================
-- 14. VACCINATION & SURGERY
-- =========================
CREATE TABLE Vaccination (
    VID             VARCHAR(20) PRIMARY KEY,
    VaccineID       VARCHAR(20),
    VaccinationDate DATETIME,
    Dosage          NVARCHAR(50),
    FOREIGN KEY (VID)       REFERENCES Service(ServiceID),
    FOREIGN KEY (VaccineID) REFERENCES Vaccine(VaccineID)
);
GO

CREATE TABLE Surgery (
    SurgeryID      VARCHAR(20) PRIMARY KEY,
    PetID          VARCHAR(20),
    SurgeryStatus  NVARCHAR(100),
    SurgeryType    NVARCHAR(100),
    AnesthesiaType NVARCHAR(100),
    SurgeryDate    DATETIME,
    DiagnosisNote  NVARCHAR(200),
    FOREIGN KEY (PetID)     REFERENCES Pet(PetID),
    FOREIGN KEY (SurgeryID) REFERENCES Service(ServiceID)
);
GO

CREATE TABLE PostSurgeryMonitoring (
    MonitorID NVARCHAR(20) PRIMARY KEY,
    SurgeryID VARCHAR(20),
    NurseID   VARCHAR(20),
    CheckTime DATETIME,
    Status    NVARCHAR(255),
    Note      NVARCHAR(255),
    FOREIGN KEY (SurgeryID) REFERENCES Surgery(SurgeryID),
    FOREIGN KEY (NurseID)   REFERENCES Nurse(NID)
);
GO

-- =========================
-- 15. DISCOUNT
-- =========================
CREATE TABLE Discount (
    DiscountID   VARCHAR(20) PRIMARY KEY,
    DiscountName NVARCHAR(100),
    StartDate    DATE,
    EndDate      DATE,
    TargetUser   NVARCHAR(50),
    Percentage   FLOAT,
    MID          VARCHAR(20),
    FOREIGN KEY (MID) REFERENCES Employee(EmployeeID)
);
GO

-- =========================
-- 16. ORDERS & ORDER DETAIL
-- =========================
CREATE TABLE Orders (
    OrderID       VARCHAR(20) PRIMARY KEY,
    CustomerID    VARCHAR(20),
    SalesPersonID VARCHAR(20),
    CreateDate    DATE,
    CreateTime    TIME,
    Status        NVARCHAR(50),
    FOREIGN KEY (CustomerID)    REFERENCES Customer(CustomerID),
    FOREIGN KEY (SalesPersonID) REFERENCES Employee(EmployeeID)
);
GO

CREATE TABLE OrderDetail (
    OrderID       VARCHAR(20),
    ProductID     VARCHAR(20),
    Quantity      INT,
    TemporaryPrice DECIMAL(18, 2),
    PRIMARY KEY (OrderID, ProductID),
    FOREIGN KEY (OrderID)   REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);
GO

-- =========================
-- 17. PAYMENT & INVOICE
-- =========================
CREATE TABLE PaymentMethod (
    PaymentTypeID VARCHAR(20) PRIMARY KEY,
    MethodName    NVARCHAR(20),
    Description   NVARCHAR(200)
);
GO

CREATE TABLE Invoice (
    InvoiceID     VARCHAR(20) PRIMARY KEY,
    CustomerID    VARCHAR(20),
    CID           VARCHAR(20),
    CardID        VARCHAR(20),
    CreatedDate   DATE,
    CreatedTime   TIME,
    TotalPrice    DECIMAL(18, 2),
    PaymentMoney  DECIMAL(18, 2),
    PaymentTypeID VARCHAR(20),
    FOREIGN KEY (CardID)        REFERENCES CardMembership(CardID),
    FOREIGN KEY (PaymentTypeID) REFERENCES PaymentMethod(PaymentTypeID),
    FOREIGN KEY (CustomerID)    REFERENCES Customer(CustomerID),
    FOREIGN KEY (CID)           REFERENCES Employee(EmployeeID)
);
GO

CREATE TABLE InvoiceProduct (
    InvoiceID VARCHAR(20),
    OrderID   VARCHAR(20),
    PRIMARY KEY (InvoiceID, OrderID),
    FOREIGN KEY (InvoiceID) REFERENCES Invoice(InvoiceID),
    FOREIGN KEY (OrderID)   REFERENCES Orders(OrderID)
);
GO

CREATE TABLE ApplyDiscount (
    InvoiceID  VARCHAR(20),
    DiscountID VARCHAR(20),
    AppliedDate DATE,
    PRIMARY KEY (InvoiceID, DiscountID),
    FOREIGN KEY (InvoiceID)  REFERENCES Invoice(InvoiceID),
    FOREIGN KEY (DiscountID) REFERENCES Discount(DiscountID)
);
GO

-- =========================
-- 18. BRANCH - SERVICE / PRODUCT
-- =========================
CREATE TABLE BranchService (
    BranchID  VARCHAR(20),
    ServiceID VARCHAR(20),
    PRIMARY KEY (BranchID, ServiceID),
    FOREIGN KEY (BranchID)  REFERENCES Branch(BranchID),
    FOREIGN KEY (ServiceID) REFERENCES Service(ServiceID)
);
GO

CREATE TABLE BranchProduct (
    BranchID      VARCHAR(20),
    ProductID     VARCHAR(20),
    StockQuantity INT,
    PRIMARY KEY (BranchID, ProductID),
    FOREIGN KEY (BranchID)  REFERENCES Branch(BranchID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);
GO

-- =========================
-- 19. APPOINTMENT & REVIEW & INVOICE SERVICE
-- =========================
CREATE TABLE Appointment (
    AppointmentID VARCHAR(20) PRIMARY KEY,
    CreateDate    DATE,
    CreateTime    TIME,
    Room          NVARCHAR(50),
    Date          DATE,
    Time          TIME,
    BranchID      VARCHAR(20),
    ServiceID     VARCHAR(20),
    CustomerID    VARCHAR(20),
    RID           VARCHAR(20),
    FOREIGN KEY (BranchID)   REFERENCES Branch(BranchID),
    FOREIGN KEY (ServiceID)  REFERENCES Service(ServiceID),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (RID)        REFERENCES Employee(EmployeeID)
);
GO

CREATE TABLE Review (
    ReviewID              VARCHAR(20) PRIMARY KEY,
    CustomerID            VARCHAR(20),
    InvoiceID             VARCHAR(20),
    ServiceQuantityScore  INT CHECK (ServiceQuantityScore BETWEEN 1 AND 10),
    StaffAttitudeScore    INT CHECK (StaffAttitudeScore BETWEEN 1 AND 10),
    OverallSatisfaction   INT CHECK (OverallSatisfaction BETWEEN 1 AND 10),
    Comment               NVARCHAR(MAX),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (InvoiceID)  REFERENCES Invoice(InvoiceID)
);
GO

CREATE TABLE InvoiceService (
    InvoiceID VARCHAR(20),
    ServiceID VARCHAR(20),
    PRIMARY KEY (InvoiceID, ServiceID),
    FOREIGN KEY (ServiceID) REFERENCES Service(ServiceID),
    FOREIGN KEY (InvoiceID) REFERENCES Invoice(InvoiceID)
);
GO
