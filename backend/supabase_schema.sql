-- =====================================================
-- PetCareX Database Schema for PostgreSQL/Supabase
-- Converted from SQL Server syntax
-- =====================================================

-- =========================
-- 1. ACCOUNT LOGIN
-- =========================
CREATE TABLE AccountLogin (
    Username VARCHAR(20) PRIMARY KEY,
    Password VARCHAR(255) NOT NULL
);

-- =========================
-- 2. BRANCH
-- =========================
CREATE TABLE Branch (
    BranchID VARCHAR(20) PRIMARY KEY,
    BranchName VARCHAR(100) NOT NULL,
    Address VARCHAR(255),
    PhoneNumber VARCHAR(15),
    Email VARCHAR(100),
    OpenTime INT,
    CloseTime INT
);

-- =========================
-- 3. EMPLOYEE
-- =========================
CREATE TABLE Employee (
    EmployeeID VARCHAR(20) PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Birthday DATE,
    Gender VARCHAR(10),
    PhoneNumber VARCHAR(15),
    StartDate DATE,
    BaseSalary DECIMAL(18, 2),
    MID VARCHAR(20),
    Role VARCHAR(50),
    Username VARCHAR(20),
    FOREIGN KEY (MID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (Username) REFERENCES AccountLogin(Username)
);

-- =========================
-- 4. WORK SCHEDULE
-- =========================
CREATE TABLE WorkSchedule (
    EmployeeID VARCHAR(20),
    WorkDate DATE,
    WorkTime INT,
    Shift VARCHAR(50),
    MID VARCHAR(20),
    PRIMARY KEY (EmployeeID, WorkDate, WorkTime),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (MID) REFERENCES Employee(EmployeeID)
);

-- =========================
-- 5. LEAVE REQUEST
-- =========================
CREATE TABLE LeaveRequest (
    EmployeeID VARCHAR(20),
    StartDate DATE,
    EndDate DATE,
    Reason VARCHAR(255),
    Status VARCHAR(50),
    MID VARCHAR(20),
    PRIMARY KEY (EmployeeID, StartDate, EndDate),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (MID) REFERENCES Employee(EmployeeID)
);

-- =========================
-- 6. TRANSFER HISTORY
-- =========================
CREATE TABLE TransferHistory (
    BranchID VARCHAR(20),
    EmployeeID VARCHAR(20),
    StartDate DATE,
    EndDate DATE,
    PRIMARY KEY (BranchID, EmployeeID),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (BranchID) REFERENCES Branch(BranchID)
);

-- =========================
-- 7. DEGREE & CERTIFICATE
-- =========================
CREATE TABLE Degree (
    DegreeID VARCHAR(20) PRIMARY KEY,
    Major VARCHAR(100),
    School VARCHAR(100),
    GraduationYear INT
);

CREATE TABLE Certificate (
    CertificateID VARCHAR(20) PRIMARY KEY,
    Name VARCHAR(100),
    IssueDate DATE,
    IssueOrganization VARCHAR(200)
);

-- =========================
-- 8. DOCTOR & NURSE
-- =========================
CREATE TABLE Doctor (
    DID VARCHAR(20) PRIMARY KEY,
    Specialization VARCHAR(100),
    EducationLevel VARCHAR(100),
    DegreeID VARCHAR(20),
    CerticateID VARCHAR(20),
    FOREIGN KEY (DID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (DegreeID) REFERENCES Degree(DegreeID),
    FOREIGN KEY (CerticateID) REFERENCES Certificate(CertificateID)
);

CREATE TABLE Nurse (
    NID VARCHAR(20) PRIMARY KEY,
    Specialization VARCHAR(100),
    EducationLevel VARCHAR(100),
    DegreeID VARCHAR(20),
    FOREIGN KEY (NID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (DegreeID) REFERENCES Degree(DegreeID)
);

-- =========================
-- 9. CUSTOMER & MEMBERSHIP
-- =========================
CREATE TABLE Customer (
    CustomerID VARCHAR(20) PRIMARY KEY,
    FullName VARCHAR(100),
    PhoneNumber VARCHAR(15),
    Email VARCHAR(100),
    CCCD VARCHAR(20) UNIQUE,
    Gender VARCHAR(10),
    Birthday DATE,
    Username VARCHAR(20),
    FOREIGN KEY (Username) REFERENCES AccountLogin(Username)
);

CREATE TABLE MembershipLevel (
    LevelID VARCHAR(10) PRIMARY KEY,
    LevelName VARCHAR(50),
    AnnualSpendingThreshold INT,
    RetentionThreshold INT,
    DiscountRate FLOAT
);

CREATE TABLE CardMembership (
    CardID VARCHAR(20) PRIMARY KEY,
    RegistrationDate DATE,
    LoyalPoint INT,
    LevelID VARCHAR(10),
    CustomerID VARCHAR(20),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (LevelID) REFERENCES MembershipLevel(LevelID)
);

-- =========================
-- 10. PET
-- =========================
CREATE TABLE Pet (
    PetID VARCHAR(20) PRIMARY KEY,
    CustomerID VARCHAR(20),
    PetName VARCHAR(50),
    Species VARCHAR(50),
    Breed VARCHAR(50),
    Birthday DATE,
    Gender VARCHAR(10),
    HealthStatus VARCHAR(255),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);

-- =========================
-- 11. PRODUCT / DRUG / VACCINE
-- =========================
CREATE TABLE Product (
    ProductID VARCHAR(20) PRIMARY KEY,
    ProductName VARCHAR(100),
    ProductType VARCHAR(50),
    SellingPrice DECIMAL(18, 2)
);

CREATE TABLE Drug (
    DrugID VARCHAR(20) PRIMARY KEY,
    DrugName VARCHAR(100),
    DrugType VARCHAR(100)
);

CREATE TABLE Vaccine (
    VaccineID VARCHAR(20) PRIMARY KEY,
    VaccineName VARCHAR(100),
    ExpiryDate DATE,
    ProductionDate DATE,
    VaccineType VARCHAR(100),
    Manufacturer VARCHAR(100),
    SideEffects VARCHAR(255)
);

-- =========================
-- 12. SERVICE & VACCINATION PACKAGE
-- =========================
CREATE TABLE Service (
    ServiceID VARCHAR(20) PRIMARY KEY,
    ServiceName VARCHAR(100),
    ServiceDescription VARCHAR(200),
    DID VARCHAR(20),
    FOREIGN KEY (DID) REFERENCES Doctor(DID)
);

CREATE TABLE VaccinationPackage (
    VPID VARCHAR(20) PRIMARY KEY,
    Duration INT,
    DiscountRate FLOAT,
    FOREIGN KEY (VPID) REFERENCES Service(ServiceID)
);

CREATE TABLE PackageVaccine (
    VPID VARCHAR(20),
    VaccineID VARCHAR(20),
    Quantity INT,
    PRIMARY KEY (VPID, VaccineID),
    FOREIGN KEY (VPID) REFERENCES VaccinationPackage(VPID),
    FOREIGN KEY (VaccineID) REFERENCES Vaccine(VaccineID)
);

-- =========================
-- 13. EXAMINATION & PRESCRIPTION
-- =========================
CREATE TABLE Examination (
    EID VARCHAR(20) PRIMARY KEY,
    PetID VARCHAR(20),
    ExaminationDate TIMESTAMP,
    Symptoms TEXT,
    Diagnoses TEXT,
    FollowUpDate DATE,
    FOREIGN KEY (PetID) REFERENCES Pet(PetID)
);

CREATE TABLE Prescription (
    PrescriptionID VARCHAR(20) PRIMARY KEY,
    EID VARCHAR(20),
    CreateDate TIMESTAMP,
    Note VARCHAR(255),
    FOREIGN KEY (EID) REFERENCES Examination(EID)
);

CREATE TABLE PrescriptionDrug (
    PrescriptionID VARCHAR(20),
    DrugID VARCHAR(20),
    Quantity INT,
    UsageInstruction VARCHAR(100),
    PRIMARY KEY (PrescriptionID, DrugID),
    FOREIGN KEY (PrescriptionID) REFERENCES Prescription(PrescriptionID),
    FOREIGN KEY (DrugID) REFERENCES Drug(DrugID)
);

-- =========================
-- 14. VACCINATION & SURGERY
-- =========================
CREATE TABLE Vaccination (
    VID VARCHAR(20) PRIMARY KEY,
    VaccineID VARCHAR(20),
    VaccinationDate TIMESTAMP,
    Dosage VARCHAR(50),
    FOREIGN KEY (VID) REFERENCES Service(ServiceID),
    FOREIGN KEY (VaccineID) REFERENCES Vaccine(VaccineID)
);

CREATE TABLE Surgery (
    SurgeryID VARCHAR(20) PRIMARY KEY,
    PetID VARCHAR(20),
    SurgeryStatus VARCHAR(100),
    SurgeryType VARCHAR(100),
    AnesthesiaType VARCHAR(100),
    SurgeryDate TIMESTAMP,
    DiagnosisNote VARCHAR(200),
    FOREIGN KEY (PetID) REFERENCES Pet(PetID),
    FOREIGN KEY (SurgeryID) REFERENCES Service(ServiceID)
);

CREATE TABLE PostSurgeryMonitoring (
    MonitorID VARCHAR(20) PRIMARY KEY,
    SurgeryID VARCHAR(20),
    NurseID VARCHAR(20),
    CheckTime TIMESTAMP,
    Status VARCHAR(255),
    Note VARCHAR(255),
    FOREIGN KEY (SurgeryID) REFERENCES Surgery(SurgeryID),
    FOREIGN KEY (NurseID) REFERENCES Nurse(NID)
);

-- =========================
-- 15. DISCOUNT
-- =========================
CREATE TABLE Discount (
    DiscountID VARCHAR(20) PRIMARY KEY,
    DiscountName VARCHAR(100),
    StartDate DATE,
    EndDate DATE,
    TargetUser VARCHAR(50),
    Percentage FLOAT,
    MID VARCHAR(20),
    FOREIGN KEY (MID) REFERENCES Employee(EmployeeID)
);

-- =========================
-- 16. ORDERS & ORDER DETAIL
-- =========================
CREATE TABLE Orders (
    OrderID VARCHAR(20) PRIMARY KEY,
    CustomerID VARCHAR(20),
    SalesPersonID VARCHAR(20),
    CreateDate DATE,
    CreateTime TIME,
    Status VARCHAR(50),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (SalesPersonID) REFERENCES Employee(EmployeeID)
);

CREATE TABLE OrderDetail (
    OrderID VARCHAR(20),
    ProductID VARCHAR(20),
    Quantity INT,
    TemporaryPrice DECIMAL(18, 2),
    PRIMARY KEY (OrderID, ProductID),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

-- =========================
-- 17. PAYMENT & INVOICE
-- =========================
CREATE TABLE PaymentMethod (
    PaymentTypeID VARCHAR(20) PRIMARY KEY,
    MethodName VARCHAR(20),
    Description VARCHAR(200)
);

CREATE TABLE Invoice (
    InvoiceID VARCHAR(20) PRIMARY KEY,
    CustomerID VARCHAR(20),
    CID VARCHAR(20),
    CardID VARCHAR(20),
    CreatedDate DATE,
    CreatedTime TIME,
    TotalPrice DECIMAL(18, 2),
    PaymentMoney DECIMAL(18, 2),
    PaymentTypeID VARCHAR(20),
    FOREIGN KEY (CardID) REFERENCES CardMembership(CardID),
    FOREIGN KEY (PaymentTypeID) REFERENCES PaymentMethod(PaymentTypeID),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (CID) REFERENCES Employee(EmployeeID)
);

CREATE TABLE InvoiceProduct (
    InvoiceID VARCHAR(20),
    OrderID VARCHAR(20),
    PRIMARY KEY (InvoiceID, OrderID),
    FOREIGN KEY (InvoiceID) REFERENCES Invoice(InvoiceID),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);

CREATE TABLE ApplyDiscount (
    InvoiceID VARCHAR(20),
    DiscountID VARCHAR(20),
    AppliedDate DATE,
    PRIMARY KEY (InvoiceID, DiscountID),
    FOREIGN KEY (InvoiceID) REFERENCES Invoice(InvoiceID),
    FOREIGN KEY (DiscountID) REFERENCES Discount(DiscountID)
);

-- =========================
-- 18. BRANCH - SERVICE / PRODUCT
-- =========================
CREATE TABLE BranchService (
    BranchID VARCHAR(20),
    ServiceID VARCHAR(20),
    PRIMARY KEY (BranchID, ServiceID),
    FOREIGN KEY (BranchID) REFERENCES Branch(BranchID),
    FOREIGN KEY (ServiceID) REFERENCES Service(ServiceID)
);

CREATE TABLE BranchProduct (
    BranchID VARCHAR(20),
    ProductID VARCHAR(20),
    StockQuantity INT,
    PRIMARY KEY (BranchID, ProductID),
    FOREIGN KEY (BranchID) REFERENCES Branch(BranchID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

-- =========================
-- 19. APPOINTMENT & REVIEW & INVOICE SERVICE
-- =========================
CREATE TABLE Appointment (
    AppointmentID VARCHAR(20) PRIMARY KEY,
    CreateDate DATE,
    CreateTime TIME,
    Room VARCHAR(50),
    Date DATE,
    Time TIME,
    BranchID VARCHAR(20),
    ServiceID VARCHAR(20),
    CustomerID VARCHAR(20),
    RID VARCHAR(20),
    FOREIGN KEY (BranchID) REFERENCES Branch(BranchID),
    FOREIGN KEY (ServiceID) REFERENCES Service(ServiceID),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (RID) REFERENCES Employee(EmployeeID)
);

CREATE TABLE Review (
    ReviewID VARCHAR(20) PRIMARY KEY,
    CustomerID VARCHAR(20),
    InvoiceID VARCHAR(20),
    ServiceQuantityScore INT CHECK (ServiceQuantityScore BETWEEN 1 AND 10),
    StaffAttitudeScore INT CHECK (StaffAttitudeScore BETWEEN 1 AND 10),
    OverallSatisfaction INT CHECK (OverallSatisfaction BETWEEN 1 AND 10),
    Comment TEXT,
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (InvoiceID) REFERENCES Invoice(InvoiceID)
);

CREATE TABLE InvoiceService (
    InvoiceID VARCHAR(20),
    ServiceID VARCHAR(20),
    PRIMARY KEY (InvoiceID, ServiceID),
    FOREIGN KEY (ServiceID) REFERENCES Service(ServiceID),
    FOREIGN KEY (InvoiceID) REFERENCES Invoice(InvoiceID)
);
