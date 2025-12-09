import csv
import random
from datetime import datetime, timedelta

from faker import Faker

fake = Faker("en_US")
fake_en = Faker("en_US")
random.seed(42)

# ================== SỐ LƯỢNG BẢN GHI ==================
N_BRANCH = 10  # 10 chi nhánh
N_DEGREE = 200  # nhiều loại bằng cấp
N_CERT = 300  # nhiều chứng chỉ
N_EMPLOYEE = 500  # ~50 NV/chi nhánh
N_DOCTOR = 120  # bác sĩ
N_NURSE = 150  # y tá

# bảng lịch / giao dịch dùng test index, để to (>= 70k ở 1 số bảng)
N_WORK_SCHEDULE = 200000
N_LEAVE_REQUEST = 5000
N_TRANSFER_HISTORY = 2000

N_CUSTOMER = 80000
N_CARD = 50000

N_PRODUCT = 1500
N_DRUG = 300
N_VACCINE = 120
N_SERVICE = 200
N_VACC_PKG = 40
N_PACKAGE_VACCINE = 150

N_PET = 120000
N_EXAM = 150000
N_PRESCRIPTION = 100000
N_PRES_DRUG = 300000

N_VACCINATION = 100000
N_SURGERY = 20000
N_MONITOR = 60000

N_DISCOUNT = 80

N_ORDER = 150000
N_ORDER_DETAIL = 400000

N_INVOICE = 150000
N_INVOICE_PRODUCT = 400000
N_APPLY_DISCOUNT = 100000

N_BRANCH_SERVICE = 1000
N_BRANCH_PRODUCT = 15000

N_APPOINTMENT = 200000
N_REVIEW = 40000
N_INVOICE_SERVICE = 150000


# ================== HÀM TIỆN ÍCH ==================


def random_date(start, end):
    delta = end - start
    result = start + timedelta(days=random.randrange(delta.days + 1))
    return result.strftime("%Y-%m-%d")


def random_datetime(start, end):
    delta = end - start
    seconds = random.randrange(delta.days * 24 * 3600 + 1)
    result = start + timedelta(seconds=seconds)
    return result.strftime("%Y-%m-%d %H:%M:%S")


def pick(lst):
    return random.choice(lst)


# phone nhân viên: 10 số, unique
used_employee_phones = set()


def gen_unique_employee_phone():
    while True:
        prefix = random.choice(["03", "05", "07", "08", "09"])
        tail = "".join(str(random.randint(0, 9)) for _ in range(8))
        phone = prefix + tail
        if phone not in used_employee_phones:
            used_employee_phones.add(phone)
            return phone


# phone khách: 10 số, có thể trùng
def gen_phone():
    prefix = random.choice(["03", "05", "07", "08", "09"])
    tail = "".join(str(random.randint(0, 9)) for _ in range(8))
    return prefix + tail


CITIES_DISTRICTS = {
    "TP. Hồ Chí Minh": [
        "Quận 1",
        "Quận 2",
        "Quận 3",
        "Quận 4",
        "Quận 5",
        "Quận 6",
        "Quận 7",
        "Quận 8",
        "Quận 9",
        "Quận 10",
        "Quận 11",
        "Quận 12",
        "Bình Thạnh",
        "Gò Vấp",
        "Tân Bình",
        "Tân Phú",
        "Thủ Đức",
        "Phú Nhuận",
        "Bình Tân",
        "Cần Thơ",
    ],
    "Hà Nội": [
        "Ba Đình",
        "Hoàn Kiếm",
        "Đống Đa",
        "Cầu Giấy",
        "Thanh Xuân",
        "Hai Bà Trưng",
        "Hoàng Mai",
        "Long Biên",
        "Bắc Từ Liêm",
        "Tây Hồ",
        "Hà Đông",
        "Thanh Trì",
        "Gia Lâm",
        "Đông Anh",
    ],
    "Đà Nẵng": [
        "Hải Châu",
        "Thanh Khê",
        "Sơn Trà",
        "Ngũ Hành Sơn",
        "Liên Chiểu",
        "Cẩm Lệ",
    ],
    "Hải Phòng": [
        "Hồng Bàng",
        "Ngô Quyền",
        "Lê Chân",
        "Kiến An",
        "Đồ Sơn",
        "Vân Đồn",
    ],
    "Cần Thơ": ["Ninh Kiều", "Bình Thủy", "Cái Răng", "Ô Môn", "Thốt Nốt"],
}


def gen_address():
    city = pick(list(CITIES_DISTRICTS.keys()))
    district = pick(CITIES_DISTRICTS[city])
    street = f"{random.randint(1, 200)} {fake.street_name()}"
    return f"{street} - {district} - {city}"


# ================== MAIN ==================


def main():
    # Username -> Password (cho AccountLogin)
    accounts = {}

    # ---------- 1. BRANCH ----------
    branch_ids = []
    with open("Branch.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(
            [
                "BranchID",
                "BranchName",
                "Address",
                "PhoneNumber",
                "Email",
                "OpenTime",
                "CloseTime",
            ]
        )
        for i in range(1, N_BRANCH + 1):
            bid = f"B{i:03d}"
            branch_ids.append(bid)
            w.writerow(
                [
                    bid,
                    f"Chi nhánh {i}",
                    gen_address(),
                    gen_phone(),
                    f"branch{i}@petcarex.com",
                    8,  # OpenTime: 8h sáng
                    18,  # CloseTime: 18h chiều
                ]
            )

    # ---------- 2. DEGREE ----------
    degree_ids = []
    with open("Degree.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(["DegreeID", "Major", "School", "GraduationYear"])
        majors = [
            "Bác sĩ Thú y",
            "Y tá Thú y",
            "Kỹ thuật viên Thú y",
            "Chuyên gia Phẫu thuật",
            "Chuyên gia Nha khoa",
            "Chuyên gia Da liễu",
            "Chuyên gia Nội khoa",
            "Chuyên gia Ngoại khoa",
            "Chuyên gia Dinh dưỡng",
            "Chuyên gia Dược phẩm",
            "Chuyên gia Siêu âm",
            "Chuyên gia X-quang",
            "Chuyên gia Phòng chống dịch",
            "Chuyên gia Sinh sản",
            "Quản lý Bệnh viện Thú y",
        ]
        schools = [
            "ĐH Nông Lâm TP.HCM",
            "ĐH Thú y Hà Nội",
            "ĐH Cần Thơ",
            "ĐH Nông nghiệp Hà Nội",
            "ĐH Huế",
            "ĐH Đà Nẵng",
            "ĐH Tây Nguyên",
            "ĐH An Giang",
            "ĐH Hồng Đức",
            "ĐH Thái Nguyên",
        ]
        for i in range(1, N_DEGREE + 1):
            did = f"DEG{i:03d}"
            degree_ids.append(did)
            w.writerow(
                [
                    did,
                    pick(majors),
                    pick(schools),
                    random.randint(2005, 2023),
                ]
            )

    # ---------- 3. CERTIFICATE ----------
    cert_ids = []
    with open("Certificate.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(["CertificateID", "Name", "IssueDate", "IssueOrganization"])
        cert_names = [
            "Bằng Tiến sĩ Thú y",
            "Bằng Thạc sĩ Thú y",
            "Bằng Đại học Thú y",
            "Chứng chỉ Phẫu thuật Thú y",
            "Chứng chỉ Phẫu thuật Tim mạch",
            "Chứng chỉ Nha y Thú y",
            "Chứng chỉ Siêu âm chẩn đoán",
            "Chứng chỉ X-quang Thú y",
            "Chứng chỉ Phòng chống bệnh truyền nhiễm",
            "Chứng chỉ Quản lý bệnh viện Thú y",
            "Chứng chỉ Anestesia Thú y",
            "Chứng chỉ Dinh dưỡng Thú y",
            "Chứng chỉ Pháp y Thú y",
            "Chứng chỉ Sinh sản Thú y",
            "Chứng chỉ Bệnh da liễu Thú y",
            "Chứng chỉ Nội khoa Thú y",
            "Chứng chỉ Ngoại khoa Thú y",
            "Chứng chỉ Bệnh học Thú y",
            "Chứng chỉ Dịch tễ học Thú y",
            "Chứng chỉ Chẩn đoán lâm sàng",
        ]
        cert_orgs = [
            "Trường Đại học Nông Lâm TP.HCM",
            "Trường Đại học Thú y Hà Nội",
            "Viện Pasteur Nha Trang",
            "Bộ Nông nghiệp và Phát triển Nông thôn",
            "Hiệp hội Thú y Việt Nam",
            "Tổ chức Thú y Quốc tế (OIE)",
            "Đại học Cornell",
            "Đại học California",
            "Trường Veterinary Medicine USA",
            "Viện Nông nghiệp Australia",
            "Trường Đại học Kasetsart Thái Lan",
            "Bệnh viện Thú y Quốc gia",
            "Trung tâm Kiểm dịch Động vật",
            "Hiệp hội Thú y Châu Á-Thái Bình Dương",
        ]
        for i in range(1, N_CERT + 1):
            cid = f"CER{i:04d}"
            cert_ids.append(cid)
            w.writerow(
                [
                    cid,
                    pick(cert_names),
                    random_date(datetime(2010, 1, 1), datetime(2024, 1, 1)),
                    pick(cert_orgs),
                ]
            )

    # ---------- 4. EMPLOYEE ----------
    ROLE_PREFIX = {
        "Doctor": "DT",
        "Nurse": "NS",
        "Receptionist": "RC",
        "Cashier": "CS",
        "Manager": "MN",
        "Admin": "AD",
        "Staff": "EM",
    }

    employee_ids = [f"E{i:04d}" for i in range(1, N_EMPLOYEE + 1)]
    employee_roles = {}

    # Chia employee cho Doctor & Nurse
    doctor_emp_ids = set(employee_ids[:N_DOCTOR])
    nurse_emp_ids = set(employee_ids[N_DOCTOR : N_DOCTOR + N_NURSE])

    # Chọn Manager từ những người KHÔNG phải Doctor & Nurse
    non_medical_ids = [
        e
        for e in employee_ids
        if e not in doctor_emp_ids and e not in nurse_emp_ids
    ]
    N_MANAGER = N_BRANCH
    manager_ids = random.sample(
        non_medical_ids, min(N_MANAGER, len(non_medical_ids))
    )

    prefix_counters = {p: 1 for p in ROLE_PREFIX.values()}

    with open("Employee.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(
            [
                "EmployeeID",
                "FullName",
                "Birthday",
                "Gender",
                "PhoneNumber",
                "StartDate",
                "BaseSalary",
                "MID",
                "Role",
                "Username",
            ]
        )

        for emp_id in employee_ids:
            # Xác định role
            if emp_id in doctor_emp_ids:
                role = "Doctor"
            elif emp_id in nurse_emp_ids:
                role = "Nurse"
            elif emp_id in manager_ids:
                role = "Manager"
            else:
                role = random.choice(
                    ["Receptionist", "Cashier", "Admin", "Staff"]
                )

            employee_roles[emp_id] = role

            gender = random.choice(["Nam", "Nữ"])
            birthday = random_date(datetime(1975, 1, 1), datetime(2003, 12, 31))
            start_date = random_date(
                datetime(2015, 1, 1), datetime(2024, 12, 31)
            )
            salary = random.randint(6_000_000, 25_000_000)

            # MID: tất cả nhân viên phải có MID hợp lệ (FK valid)
            # Manager quản lý chính mình, các role khác do
            # Manager quản lý
            if role == "Manager":
                mid = emp_id
            else:
                mid = pick(manager_ids) if manager_ids else emp_id

            prefix = ROLE_PREFIX[role]
            cnt = prefix_counters[prefix]
            prefix_counters[prefix] += 1
            username = f"{prefix}{cnt:04d}"
            password = f"{username}@123"
            accounts[username] = password

            w.writerow(
                [
                    emp_id,
                    fake.name(),
                    birthday,
                    gender,
                    gen_unique_employee_phone(),
                    start_date,
                    salary,
                    mid,
                    role,
                    username,
                ]
            )

    # ---------- 5. WORK SCHEDULE ----------
    used_work_schedule = set()
    with open("WorkSchedule.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(["EmployeeID", "WorkDate", "WorkTime", "Shift", "MID"])
        shift_times = {
            "Ca sáng": 7,  # 07:00-12:00
            "Ca chiều": 13,  # 13:00-17:00
            "Ca tối": 18,  # 18:00+
        }
        count = 0
        max_attempts = N_WORK_SCHEDULE * 10
        attempts = 0
        while count < N_WORK_SCHEDULE and attempts < max_attempts:
            attempts += 1
            emp = pick(employee_ids)
            work_date = random_date(
                datetime(2024, 1, 1), datetime(2024, 12, 31)
            )
            shift = random.choice(list(shift_times.keys()))
            work_time = shift_times[shift]
            key = (emp, work_date, work_time)
            if key in used_work_schedule:
                continue
            used_work_schedule.add(key)
            mid = pick(manager_ids) if manager_ids else emp
            w.writerow([emp, work_date, work_time, shift, mid])
            count += 1

    # ---------- 6. LEAVE REQUEST ----------
    used_leave_request = set()
    with open("LeaveRequest.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(
            ["EmployeeID", "StartDate", "EndDate", "Reason", "Status", "MID"]
        )
        reasons = ["Nghỉ phép", "Nghỉ bệnh", "Việc gia đình"]
        statuses = ["Pending", "Approved", "Rejected"]
        count = 0
        max_attempts = N_LEAVE_REQUEST * 10
        attempts = 0
        while count < N_LEAVE_REQUEST and attempts < max_attempts:
            attempts += 1
            emp = pick(employee_ids)
            start_d = random_date(datetime(2023, 1, 1), datetime(2024, 12, 31))
            key = (emp, start_d)  # PK: EmployeeID + StartDate
            if key in used_leave_request:
                continue
            used_leave_request.add(key)
            start_d_obj = datetime.strptime(start_d, "%Y-%m-%d")
            end_d = (
                start_d_obj + timedelta(days=random.randint(1, 5))
            ).strftime("%Y-%m-%d")
            mid = pick(manager_ids) if manager_ids else emp
            w.writerow(
                [
                    emp,
                    start_d,
                    end_d,
                    pick(reasons),
                    pick(statuses),
                    mid,
                ]
            )
            count += 1

    # ---------- 7. TRANSFER HISTORY ----------
    used_transfers = set()
    with open(
        "TransferHistory.csv", "w", newline="", encoding="utf-8-sig"
    ) as f:
        w = csv.writer(f)
        w.writerow(["BranchID", "EmployeeID", "StartDate", "EndDate"])
        while len(used_transfers) < N_TRANSFER_HISTORY:
            bid = pick(branch_ids)
            emp = pick(employee_ids)
            key = (bid, emp)
            if key in used_transfers:
                continue
            used_transfers.add(key)
            start_d = random_date(datetime(2020, 1, 1), datetime(2023, 12, 31))
            start_d_obj = datetime.strptime(start_d, "%Y-%m-%d")
            end_d = (
                start_d_obj + timedelta(days=random.randint(30, 365))
            ).strftime("%Y-%m-%d")
            w.writerow([bid, emp, start_d, end_d])

    # ---------- 8. DOCTOR ----------
    with open("Doctor.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(
            [
                "DID",
                "Specialization",
                "EducationLevel",
                "DegreeID",
                "CerticateID",
            ]
        )
        specs = [
            "Thú y chó mèo",
            "Ngoại khoa",
            "Nội tổng quát",
            "Da liễu thú y",
        ]
        edu_levels = ["ĐH", "ThS", "TS"]
        for did in doctor_emp_ids:
            w.writerow(
                [
                    did,
                    pick(specs),
                    pick(edu_levels),
                    pick(degree_ids),
                    pick(cert_ids),
                ]
            )

    # ---------- 9. NURSE ----------
    with open("Nurse.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(["NID", "Specialization", "EducationLevel", "DegreeID"])
        specs = ["Chăm sóc hậu phẫu", "Tiêm ngừa", "Chăm sóc nội trú"]
        edu_levels = ["CĐ", "ĐH"]
        for nid in nurse_emp_ids:
            w.writerow([nid, pick(specs), pick(edu_levels), pick(degree_ids)])

    # ---------- 10. MEMBERSHIP LEVEL ----------
    level_ids = []
    with open(
        "MembershipLevel.csv", "w", newline="", encoding="utf-8-sig"
    ) as f:
        w = csv.writer(f)
        w.writerow(
            [
                "LevelID",
                "LevelName",
                "AnnualSpendingThreshold",
                "RetentionThreshold",
                "DiscountRate",
            ]
        )
        data = [
            ("L1", "Basic", 60, 0, 0.05),
            ("L2", "Standard", 240, 60, 0.10),
            ("L3", "Platinum", 99999, 160, 0.15),
        ]
        for lid, name, spend, retain, disc in data:
            level_ids.append(lid)
            w.writerow([lid, name, spend, retain, disc])

    # ---------- 11. CUSTOMER ----------
    customer_ids = []
    with open("Customer.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(
            [
                "CustomerID",
                "FullName",
                "PhoneNumber",
                "Email",
                "CCCD",
                "Gender",
                "Birthday",
                "Username",
            ]
        )
        used_cccd = set()

        for i in range(1, N_CUSTOMER + 1):
            cid = f"CUS{i:05d}"
            customer_ids.append(cid)
            birthday = random_date(datetime(1970, 1, 1), datetime(2010, 12, 31))
            gender = random.choice(["Nam", "Nữ"])

            # đảm bảo CCCD không trùng
            while True:
                cccd = str(random.randint(100000000000, 999999999999))
                if cccd not in used_cccd:
                    used_cccd.add(cccd)
                    break

            phone = gen_phone()
            email = fake.email()

            # Customer username tự do, có thể có hoặc không
            username = ""
            if random.random() < 0.5:
                base = fake.user_name()[:15] or "user"
                uname = base
                suffix = 1
                # đảm bảo không trùng username đã dùng (nhân viên + KH)
                while uname in accounts:
                    uname = f"{base}{suffix}"
                    suffix += 1
                username = uname
                accounts[username] = fake.password(length=10)

            w.writerow(
                [
                    cid,
                    fake.name(),
                    phone,
                    email,
                    cccd,
                    gender,
                    birthday,
                    username,
                ]
            )

    # ---------- 12. CARD MEMBERSHIP ----------
    card_ids = []
    with open("CardMembership.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(
            [
                "CardID",
                "RegistrationDate",
                "LoyalPoint",
                "LevelID",
                "CustomerID",
            ]
        )
        for i in range(1, N_CARD + 1):
            card = f"CARD{i:05d}"
            card_ids.append(card)
            reg = random_date(datetime(2020, 1, 1), datetime(2024, 12, 31))
            w.writerow(
                [
                    card,
                    reg,
                    random.randint(0, 10000),
                    pick(level_ids),
                    pick(customer_ids),
                ]
            )

    # ---------- 13. PRODUCT ----------
    product_ids = []
    with open("Product.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(["ProductID", "ProductName", "ProductType", "SellingPrice"])
        pt_types = [
            "Thức ăn",
            "Đồ chơi",
            "Phụ kiện",
            "Dược phẩm",
            "Vitamin",
            "Chăm sóc da",
            "Thiết bị y tế",
        ]
        product_names = {
            "Thức ăn": [
                "Thức ăn chó hạt khô Premium",
                "Thức ăn mèo cao cấp",
                "Thức ăn chó Poodle",
                "Thức ăn Tây Ban Nha",
                "Thức ăn thỏ hữu cơ",
            ],
            "Đồ chơi": [
                "Bóng cao su",
                "Dây kéo vải",
                "Chuông leng keng",
                "Cây mèo 3 tầng",
                "Tunnel chơi thỏ",
            ],
            "Phụ kiện": [
                "Dây xích chó",
                "Vòng cổ chó thông minh",
                "Giường nằm cho mèo",
                "Cát lót thỏ",
                "Lồng chim",
            ],
            "Dược phẩm": [
                "Siro ho cho chó",
                "Kem chữa ghề",
                "Thuốc tẩy giun",
                "Vitamin B12 tiêm",
                "Kháng sinh dạng bột",
            ],
            "Vitamin": [
                "Vitamin C dạng bột",
                "Canxi cho chó già",
                "Dầu cá tốt cho lông",
                "Probiotic tiêu hóa",
                "Glucosamine cho khớp",
            ],
            "Chăm sóc da": [
                "Sữa tắm chuyên dùng",
                "Dầu gội lông bóng",
                "Kem mềm da",
                "Xịt khử mùi",
                "Kem chống côn trùng",
            ],
            "Thiết bị y tế": [
                "Nhiệt kế điện tử",
                "Ngoạm cắt móng",
                "Bàn chải đánh răng",
                "Ký sinh trùng detector",
                "Máy hút bụi lông",
            ],
        }
        for i in range(1, N_PRODUCT + 1):
            pid = f"PRD{i:04d}"
            product_ids.append(pid)
            ptype = pick(pt_types)
            pname = pick(product_names.get(ptype, [f"Sản phẩm {ptype} {i}"]))
            w.writerow(
                [
                    pid,
                    pname,
                    ptype,
                    random.randint(20000, 1000000),
                ]
            )

    # ---------- 14. DRUG ----------
    drug_ids = []
    with open("Drug.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(["DrugID", "DrugName", "DrugType"])
        drug_types = [
            "Kháng sinh",
            "Giảm đau",
            "Vitamin",
            "Chống viêm",
            "Tiêu hoá",
            "Hô hấp",
            "Tim mạch",
            "Thần kinh",
        ]
        drug_names = {
            "Kháng sinh": [
                "Amoxicillin",
                "Oxytetracycline",
                "Enrofloxacin",
                "Gentamycin",
                "Trimethoprim",
            ],
            "Giảm đau": [
                "Paracetamol",
                "Dipyrone",
                "Meloxicam",
                "Carprofen",
                "Phenylbutazone",
            ],
            "Vitamin": [
                "Vitamin B Complex",
                "Vitamin C",
                "Vitamin A+D",
                "Vitamin E",
                "Taurine",
            ],
            "Chống viêm": [
                "Dexamethasone",
                "Prednisone",
                "Methylprednisolone",
                "Betamethasone",
                "Hydrocortisone",
            ],
            "Tiêu hoá": [
                "Ranitidine",
                "Omeprazole",
                "Metoclopramide",
                "Loperamide",
                "Bisacodyl",
            ],
            "Hô hấp": [
                "Doxapram",
                "Salbutamol",
                "Theophylline",
                "Bromhexine",
                "Codeine",
            ],
            "Tim mạch": [
                "Digoxin",
                "Captopril",
                "Furosemide",
                "Spironolactone",
                "Propranolol",
            ],
            "Thần kinh": [
                "Diazepam",
                "Phenobarbital",
                "Propofol",
                "Ketamine",
                "Midazolam",
            ],
        }
        for i in range(1, N_DRUG + 1):
            did = f"DRG{i:04d}"
            drug_ids.append(did)
            dtype = pick(drug_types)
            dname = pick(drug_names.get(dtype, [f"Thuốc {dtype} {i}"]))
            w.writerow([did, dname, dtype])

    # ---------- 15. VACCINE ----------
    vaccine_ids = []
    with open("Vaccine.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(
            [
                "VaccineID",
                "VaccineName",
                "ExpiryDate",
                "ProductionDate",
                "VaccineType",
                "Manufacturer",
                "SideEffects",
            ]
        )
        vtypes = [
            "Virus",
            "Vi khuẩn",
            "Hỗn hợp",
            "Recombinant",
            "Live attenuated",
        ]
        vaccine_names = [
            "DHP(Distemper - Hepatitis- Parvovirus)",
            "Rabies",
            "Bordetella",
            "Leptospirosis",
            "Feline Panleukopenia",
            "Feline Herpesvirus",
            "Feline Calicivirus",
            "Feline Leukemia",
            "FELV (Feline Leukemia Virus)",
            "FIP (Feline Infectious Peritonitis)",
            "Chlamydia",
            "Giardia Vaccine",
        ]
        manufacturers = [
            "Pfizer",
            "Merck",
            "Boehringer Ingelheim",
            "Virbac",
            "Ceva",
            "Zoetis",
            "Elanco",
            "MSD",
        ]
        side_effects = [
            "Sốt nhẹ - sưng vùng tiêm",
            "Mệt mỏi - quanh quẩn trong 24 giờ",
            "Phản ứng dị ứng nhẹ (hiếm gặp)",
            "Sưng hạch bạch huyết tạm thời",
            "Sốt cao - mất ăn trong vài ngày",
            "Phát ban nhẹ tại vị trí tiêm",
            "Không có tác dụng phụ đáng kể",
            "Phản ứng dị ứng nặng (rất hiếm)",
        ]
        for i in range(1, N_VACCINE + 1):
            vid = f"VAC{i:04d}"
            vaccine_ids.append(vid)
            prod = random_date(datetime(2020, 1, 1), datetime(2023, 12, 31))
            prod_obj = datetime.strptime(prod, "%Y-%m-%d")
            exp = (
                prod_obj + timedelta(days=random.randint(180, 900))
            ).strftime("%Y-%m-%d")
            w.writerow(
                [
                    vid,
                    pick(vaccine_names),
                    exp,
                    prod,
                    pick(vtypes),
                    pick(manufacturers),
                    pick(side_effects),
                ]
            )

    # ---------- 16. SERVICE (4 loại dịch vụ chuẩn) ----------
    service_ids = []
    service_ids_exam = []  # Khám tổng quát
    service_ids_vac_single = []  # Tiêm phòng (mũi lẻ)
    service_ids_vac_package = []  # Tiêm phòng theo gói
    service_ids_surgery = []  # Phẫu thuật

    doctor_ids_for_service = (
        list(doctor_emp_ids) if doctor_emp_ids else employee_ids
    )

    with open("Service.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(["ServiceID", "ServiceName", "ServiceDescription", "DID"])

        service_types = [
            "Khám tổng quát",  # dùng cho khám bệnh
            "Tiêm phòng",  # tiêm phòng lẻ
            "Tiêm phòng theo gói",  # gói tiêm phòng
            "Phẫu thuật",  # phẫu thuật
        ]
        exam_names = [
            "Khám tổng quát cơ bản",
            "Khám chẩn đoán bệnh",
            "Khám sàng lọc định kỳ",
            "Khám sau phẫu thuật",
            "Khám hậu phục hồi",
            "Khám da liễu",
            "Khám nội khoa",
            "Khám ngoại khoa",
            "Khám nha y",
            "Khám mắt thú y",
        ]
        vac_names = [
            "Tiêm vắc-xin bại liệt",
            "Tiêm vắc-xin dại",
            "Tiêm vắc-xin distemper",
            "Tiêm vắc-xin parvo",
            "Tiêm vắc-xin hepatitis",
            "Tiêm vắc-xin leptospira",
            "Tiêm vắc-xin bordetella",
            "Tiêm vắc-xin chlamydia",
            "Tiêm vắc-xin panleukopenia",
            "Tiêm vắc-xin herpes felina",
        ]
        vac_pkg_names = [
            "Gói tiêm phòng trí thức",
            "Gói tiêm phòng cơ bản",
            "Gói tiêm phòng đầy đủ",
            "Gói tiêm phòng nâng cao",
            "Gói tiêm phòng chuyên nghiệp",
        ]
        surgery_names = [
            "Phẫu thuật triệt sản",
            "Phẫu thuật hóa giải",
            "Phẫu thuật amputate",
            "Phẫu thuật da thịt",
            "Phẫu thuật cắt khối종",
            "Phẫu thuật chỉnh hình",
            "Phẫu thuật mắt",
            "Phẫu thuật tai mũi họng",
            "Phẫu thuật nha khoa",
            "Phẫu thuật tiêu hóa",
        ]

        for i in range(1, N_SERVICE + 1):
            sid = f"SRV{i:04d}"
            base_name = pick(service_types)

            if base_name == "Khám tổng quát":
                service_name = pick(exam_names)
                service_ids_exam.append(sid)
            elif base_name == "Tiêm phòng":
                service_name = pick(vac_names)
                service_ids_vac_single.append(sid)
            elif base_name == "Tiêm phòng theo gói":
                service_name = pick(vac_pkg_names)
                service_ids_vac_package.append(sid)
            elif base_name == "Phẫu thuật":
                service_name = pick(surgery_names)
                service_ids_surgery.append(sid)
            else:
                service_name = base_name

            service_ids.append(sid)

            w.writerow(
                [
                    sid,
                    service_name,
                    "Dịch vụ thú y tổng hợp",
                    pick(doctor_ids_for_service),
                ]
            )

    # ---------- 17. VACCINATION PACKAGE ----------
    vacc_pkg_ids = []
    with open(
        "VaccinationPackage.csv", "w", newline="", encoding="utf-8-sig"
    ) as f:
        w = csv.writer(f)
        w.writerow(["VPID", "Duration", "DiscountRate"])

        # Dùng dịch vụ "Tiêm phòng theo gói"
        candidates = service_ids_vac_package or service_ids
        selected = random.sample(candidates, min(N_VACC_PKG, len(candidates)))

        for sid in selected:
            vacc_pkg_ids.append(sid)
            w.writerow(
                [
                    sid,  # VPID = ServiceID
                    random.randint(180, 365),  # 6–12 tháng
                    random.choice([0.05, 0.10, 0.15]),
                ]
            )

    # ---------- 18. PACKAGE VACCINE ----------
    used_pkg_vac = set()
    with open("PackageVaccine.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(["VPID", "VaccineID", "Quantity"])
        while len(used_pkg_vac) < N_PACKAGE_VACCINE:
            vpid = pick(vacc_pkg_ids)
            vid = pick(vaccine_ids)
            key = (vpid, vid)
            if key in used_pkg_vac:
                continue
            used_pkg_vac.add(key)
            w.writerow([vpid, vid, random.randint(1, 5)])

    # ---------- 19. PET ----------
    pet_ids = []
    with open("Pet.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(
            [
                "PetID",
                "CustomerID",
                "PetName",
                "Species",
                "Breed",
                "Birthday",
                "Gender",
                "HealthStatus",
            ]
        )
        species_list = [
            "Chó",
            "Mèo",
            "Thỏ",
            "Chim",
            "Hamster",
            "Guinea Pig",
            "Rắn",
            "Tắc kè",
        ]
        dog_breeds = [
            "Phốc sóc",
            "Poodle",
            "Chó Phú Quốc",
            "Husky",
            "Golden Retriever",
            "Labrador",
            "Shiba Inu",
            "Corgi",
            "Dachshund",
            "Beagle",
            "Bulldog",
            "Samoyed",
        ]
        cat_breeds = [
            "Ba Tư",
            "Anh lông dài",
            "Anh lông ngắn",
            "Munchkin",
            "Ragdoll",
            "Maine Coon",
            "Bengal",
            "Siamese",
        ]
        small_animal_breeds = [
            "Lop ear",
            "Angora",
            "Holland Lop",
            "Teddy",
            "Skinny Pig",
            "Abyssinian",
            "Dwarf hamster",
            "Roborovski",
        ]
        bird_breeds = [
            "Chim cánh cụt",
            "Chim yểu",
            "Chim hoàng yến",
            "Vẹt",
            "Chim sẻ",
            "Chim bồ câu",
            "Chim chích bông",
        ]
        breeds = dog_breeds + cat_breeds + small_animal_breeds + bird_breeds
        healths = [
            "Khỏe mạnh",
            "Đang điều trị",
            "Sau phẫu thuật",
            "Phục hồi chấn thương",
            "Cần theo dõi",
            "Bệnh mãn tính",
        ]
        for i in range(1, N_PET + 1):
            pid = f"PET{i:05d}"
            pet_ids.append(pid)
            birthday = random_date(datetime(2018, 1, 1), datetime(2024, 1, 1))
            w.writerow(
                [
                    pid,
                    pick(customer_ids),
                    fake.first_name_nonbinary(),
                    pick(species_list),
                    pick(breeds),
                    birthday,
                    pick(["Đực", "Cái"]),
                    pick(healths),
                ]
            )

    # ---------- 20. EXAMINATION ----------
    exam_ids = []
    # Dùng service "Khám tổng quát"
    exam_candidates = service_ids_exam or service_ids
    exam_service_ids = random.sample(
        exam_candidates, min(len(exam_candidates), N_EXAM)
    )
    with open("Examination.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(
            [
                "EID",
                "PetID",
                "ExaminationDate",
                "Symptoms",
                "Diagnoses",
                "FollowUpDate",
            ]
        )
        for sid in exam_service_ids:
            exam_ids.append(sid)
            edate = random_datetime(
                datetime(2021, 1, 1), datetime(2024, 12, 31)
            )
            edate_obj = datetime.strptime(edate, "%Y-%m-%d %H:%M:%S")
            follow = (
                edate_obj + timedelta(days=random.randint(1, 30))
            ).strftime("%Y-%m-%d")
            symptoms_list = [
                "Sốt cao (38-39°C) - bỏ ăn - mệt mỏi",
                "Ho kéo dài - khó thở - chảy nước mũi",
                "Tiêu chảy nặng - nôn - bỏ ăn hoàn toàn",
                "Ngứa dữ dội - chảy máu tai từ gãi",
                "Liệt chi sau - không đứng được",
                "Chảy máu cam liên tục - nước tiểu đỏ",
                "Mắt đỏ - phù nề - khó mở mắt",
                "Hôi miệng kinh khủng - nướu sưng tấy",
                "Vết mổ sưng - chảy máu - sốc nhiễm trùng",
                "Tê liệt mặt - miệng méo",
                "Lờ đờ - mất sinh lực",
                "Bụng căng như quả bóng - đau nhiều",
            ]
            diagnoses_list = [
                "Viêm đường hô hấp cấp tính",
                "Virus Parvovirus (parvo) - nguy hiểm",
                "Viêm dạ dày ruột cấp do vi khuẩn",
                "Viêm tai ngoài do nấm Malassezia",
                "Chấn thương cột sống lưng thắt lưng",
                "Thiếu máu do sốc mất máu hoặc ký sinh",
                "Viêm mống mắt - cần chỉ định nhanh",
                "Bệnh nha chu giai đoạn cuối",
                "Nhiễm trùng vết mổ sau phẫu thuật",
                "Tổn thương thần kinh ngoại biên",
                "Bệnh đĩa đệm (bệnh IVDD)",
                "Tắc đường tiêu hóa hoặc xoắn dạ dày",
                "Suy thận cấp - cần xét nghiệm ngay",
                "Viêm tụy cấp (Pancreatitis)",
                "Xuất huyết nội bộ - cấp cứu",
            ]
            w.writerow(
                [
                    sid,
                    pick(pet_ids),
                    edate,
                    pick(symptoms_list),
                    pick(diagnoses_list),
                    follow,
                ]
            )

    # ---------- 21. PRESCRIPTION ----------
    prescription_ids = []
    with open("Prescription.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(["PrescriptionID", "EID", "CreateDate", "Note"])
        for i in range(1, N_PRESCRIPTION + 1):
            pid = f"PRSC{i:06d}"
            prescription_ids.append(pid)
            eid = pick(exam_ids)  # exam_ids now contains ServiceIDs
            cdate = random_datetime(
                datetime(2021, 1, 1), datetime(2024, 12, 31)
            )
            w.writerow(
                [
                    pid,
                    eid,
                    cdate,
                    "Dùng thuốc đủ liều, tái khám nếu không cải thiện.",
                ]
            )

    # ---------- 22. PRESCRIPTION DRUG ----------
    used_pres_drug = set()
    with open(
        "PrescriptionDrug.csv", "w", newline="", encoding="utf-8-sig"
    ) as f:
        w = csv.writer(f)
        w.writerow(["PrescriptionID", "DrugID", "Quantity", "UsageInstruction"])
        while len(used_pres_drug) < N_PRES_DRUG:
            pid = pick(prescription_ids)
            did = pick(drug_ids)
            key = (pid, did)
            if key in used_pres_drug:
                continue
            used_pres_drug.add(key)
            w.writerow(
                [
                    pid,
                    did,
                    random.randint(1, 10),
                    "Uống sau khi ăn, ngày 2 lần",
                ]
            )

    # ---------- 23. VACCINATION ----------
    # Dùng service "Tiêm phòng"
    vac_candidates = service_ids_vac_single or service_ids
    vacc_service_ids = random.sample(
        vac_candidates, min(len(vac_candidates), N_VACCINATION)
    )
    with open("Vaccination.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(["VID", "VaccineID", "VaccinationDate", "Dosage"])
        for sid in vacc_service_ids:
            vdate = random_datetime(
                datetime(2021, 1, 1), datetime(2024, 12, 31)
            )
            w.writerow([sid, pick(vaccine_ids), vdate, "1ml/kg"])

    # ---------- 24. SURGERY ----------
    # Dùng service "Phẫu thuật"
    surg_candidates = service_ids_surgery or service_ids
    surgery_service_ids = random.sample(
        surg_candidates, min(len(surg_candidates), N_SURGERY)
    )
    surgery_ids = surgery_service_ids
    # Lưu lại để dùng ở PostSurgeryMonitoring
    with open("Surgery.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(
            [
                "SurgeryID",
                "PetID",
                "SurgeryStatus",
                "SurgeryType",
                "AnesthesiaType",
                "SurgeryDate",
                "DiagnosisNote",
            ]
        )
        surg_status = ["Đã thực hiện", "Đang theo dõi"]
        surg_type = ["Thiến", "Mổ u", "Mổ bụng", "Nội soi"]
        anesth = ["Gây mê toàn thân", "Gây tê"]
        for sid in surgery_service_ids:
            sdate = random_datetime(
                datetime(2021, 1, 1), datetime(2024, 12, 31)
            )
            w.writerow(
                [
                    sid,
                    pick(pet_ids),
                    pick(surg_status),
                    pick(surg_type),
                    pick(anesth),
                    sdate,
                    "Phẫu thuật theo chỉ định bác sĩ",
                ]
            )

    # ---------- 25. POST SURGERY MONITORING ----------
    with open(
        "PostSurgeryMonitoring.csv", "w", newline="", encoding="utf-8-sig"
    ) as f:
        w = csv.writer(f)
        w.writerow(
            ["MonitorID", "SurgeryID", "NurseID", "CheckTime", "Status", "Note"]
        )
        statuses = ["Ổn", "Hơi mệt", "Cần theo dõi thêm"]
        nurse_list = list(nurse_emp_ids) or employee_ids
        for i in range(1, N_MONITOR + 1):
            mid = f"MON{i:06d}"
            srg = pick(surgery_ids)
            nurse = pick(nurse_list)
            ctime = random_datetime(
                datetime(2021, 1, 1), datetime(2024, 12, 31)
            )
            w.writerow(
                [
                    mid,
                    srg,
                    nurse,
                    ctime,
                    pick(statuses),
                    "Kiểm tra dấu hiệu sinh tồn, tình trạng vết mổ.",
                ]
            )

    # ---------- 26. DISCOUNT ----------
    discount_ids = []
    with open("Discount.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(
            [
                "DiscountID",
                "DiscountName",
                "StartDate",
                "EndDate",
                "TargetUser",
                "Percentage",
                "MID",
            ]
        )
        for i in range(1, N_DISCOUNT + 1):
            did = f"DST{i:04d}"
            discount_ids.append(did)
            sd = random_date(datetime(2023, 1, 1), datetime(2024, 6, 30))
            sd_obj = datetime.strptime(sd, "%Y-%m-%d")
            ed = (sd_obj + timedelta(days=random.randint(7, 60))).strftime(
                "%Y-%m-%d"
            )
            discount_names = [
                "Giảm giá khách VIP",
                "Khuyến mãi cuối tuần",
                "Flash sale 20% tất cả sản phẩm",
                "Khuyến mãi thanh viên mới",
                "Giảm giá dịch vụ khám bệnh",
                "Combo vaccine tiêm phòng",
                "Khuyến mãi tặng quà",
                "Giảm giá thẻ thành viên",
                "Giảm giá dành cho tập thể",
                "Khuyến mãi mua kèm thức ăn",
                "Giảm 50% dịch vụ grooming",
                "Khuyến mãi lên băng hàng tháng",
            ]
            target_users = [
                "Khách hàng mới",
                "Thành viên VIP",
                "Thành viên Standard",
                "Thành viên Cơ Bản",
                "Doanh nghiệp",
                "Tổ chức",
                "Khách hàng lần đầu",
                "Tất cả khách hàng",
                "Nhóm khách hàng đặc biệt",
                "Hội viên CLB thú cưng",
            ]
            w.writerow(
                [
                    did,
                    pick(discount_names),
                    sd,
                    ed,
                    pick(target_users),  # TargetUser: loại đối tượng khuyến mãi
                    random.choice([0.05, 0.1, 0.15, 0.2, 0.25]),
                    pick(manager_ids) if manager_ids else pick(employee_ids),
                ]
            )

    # ---------- 27. ORDERS ----------
    order_ids = []
    with open("Orders.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(
            [
                "OrderID",
                "CustomerID",
                "SalesPersonID",
                "CreateDate",
                "CreateTime",
                "Status",
            ]
        )
        status_list = ["Created", "Paid", "Cancelled"]
        for i in range(1, N_ORDER + 1):
            oid = f"ORD{i:06d}"
            order_ids.append(oid)
            cdate = random_date(datetime(2022, 1, 1), datetime(2024, 12, 31))
            w.writerow(
                [
                    oid,
                    pick(customer_ids),
                    pick(employee_ids),
                    cdate,
                    random.choice(
                        ["08:00:00", "10:30:00", "15:00:00", "19:00:00"]
                    ),
                    pick(status_list),
                ]
            )

    # ---------- 28. ORDER DETAIL ----------
    used_order_detail = set()
    with open("OrderDetail.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(["OrderID", "ProductID", "Quantity", "TemporaryPrice"])
        while len(used_order_detail) < N_ORDER_DETAIL:
            oid = pick(order_ids)
            pid = pick(product_ids)
            key = (oid, pid)
            if key in used_order_detail:
                continue
            used_order_detail.add(key)
            w.writerow(
                [
                    oid,
                    pid,
                    random.randint(1, 10),
                    random.randint(20000, 1000000),
                ]
            )

    # ---------- 29. PAYMENT METHOD ----------
    pay_ids = []
    with open("PaymentMethod.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(["PaymentTypeID", "MethodName", "Description"])
        methods = [
            ("PM01", "Tiền mặt", "Thanh toán tại quầy"),
            ("PM02", "Thẻ", "Thẻ ngân hàng"),
            ("PM03", "Chuyển khoản", "Internet banking"),
            ("PM04", "Ví điện tử", "Momo/ZaloPay, ..."),
        ]
        for pid, name, desc in methods:
            pay_ids.append(pid)
            w.writerow([pid, name, desc])

    # ---------- 30. INVOICE ----------
    invoice_ids = []
    with open("Invoice.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(
            [
                "InvoiceID",
                "CustomerID",
                "CID",
                "CardID",
                "CreatedDate",
                "CreatedTime",
                "TotalPrice",
                "PaymentMoney",
                "PaymentTypeID",
            ]
        )
        for i in range(1, N_INVOICE + 1):
            iid = f"INV{i:06d}"
            invoice_ids.append(iid)
            cdate = random_date(datetime(2022, 1, 1), datetime(2024, 12, 31))
            customer = pick(customer_ids)
            card = pick(card_ids)  # luôn là card hợp lệ
            total = random.randint(50000, 3_000_000)
            pay_money = total
            w.writerow(
                [
                    iid,
                    customer,
                    pick(employee_ids),
                    card,
                    cdate,
                    random.choice(
                        ["08:00:00", "11:00:00", "14:00:00", "18:00:00"]
                    ),
                    total,
                    pay_money,
                    pick(pay_ids),
                ]
            )

    # ---------- 31. INVOICE PRODUCT ----------
    used_inv_prod = set()
    with open("InvoiceProduct.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(["InvoiceID", "OrderID"])
        while len(used_inv_prod) < N_INVOICE_PRODUCT:
            iid = pick(invoice_ids)
            oid = pick(order_ids)
            key = (iid, oid)
            if key in used_inv_prod:
                continue
            used_inv_prod.add(key)
            w.writerow([iid, oid])

    # ---------- 32. APPLY DISCOUNT ----------
    used_apply = set()
    with open("ApplyDiscount.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(["InvoiceID", "DiscountID", "AppliedDate"])
        while len(used_apply) < N_APPLY_DISCOUNT:
            iid = pick(invoice_ids)
            did = pick(discount_ids)
            key = (iid, did)
            if key in used_apply:
                continue
            used_apply.add(key)
            adate = random_date(datetime(2022, 1, 1), datetime(2024, 12, 31))
            w.writerow([iid, did, adate])

    # ---------- 33. BRANCH SERVICE ----------
    used_bs = set()
    with open("BranchService.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(["BranchID", "ServiceID"])
        while len(used_bs) < N_BRANCH_SERVICE:
            bid = pick(branch_ids)
            sid = pick(service_ids)
            key = (bid, sid)
            if key in used_bs:
                continue
            used_bs.add(key)
            w.writerow([bid, sid])

    # ---------- 34. BRANCH PRODUCT ----------
    used_bp = set()
    with open("BranchProduct.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(["BranchID", "ProductID", "StockQuantity"])
        while len(used_bp) < N_BRANCH_PRODUCT:
            bid = pick(branch_ids)
            pid = pick(product_ids)
            key = (bid, pid)
            if key in used_bp:
                continue
            used_bp.add(key)
            w.writerow([bid, pid, random.randint(0, 300)])

    # ---------- 35. APPOINTMENT ----------
    appointment_ids = []
    with open("Appointment.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(
            [
                "AppointmentID",
                "CreateDate",
                "CreateTime",
                "Room",
                "Date",
                "Time",
                "BranchID",
                "ServiceID",
                "CustomerID",
                "RID",
            ]
        )
        for i in range(1, N_APPOINTMENT + 1):
            aid = f"APP{i:06d}"
            appointment_ids.append(aid)
            cdate = random_date(datetime(2022, 1, 1), datetime(2024, 12, 31))
            cdate_obj = datetime.strptime(cdate, "%Y-%m-%d")
            adate = (
                cdate_obj + timedelta(days=random.randint(0, 30))
            ).strftime("%Y-%m-%d")
            atime = random.choice(
                ["08:00:00", "09:00:00", "10:00:00", "14:00:00", "16:00:00"]
            )
            w.writerow(
                [
                    aid,
                    cdate,
                    random.choice(["07:30:00", "11:30:00", "13:00:00"]),
                    f"Phòng {random.randint(1, 10)}",
                    adate,
                    atime,
                    pick(branch_ids),
                    pick(service_ids),
                    pick(customer_ids),
                    pick(employee_ids),
                ]
            )

    # ---------- 36. REVIEW ----------
    with open("Review.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(
            [
                "ReviewID",
                "CustomerID",
                "InvoiceID",
                "ServiceQuantityScore",
                "StaffAttitudeScore",
                "OverallSatisfaction",
                "Comment",
            ]
        )
        for i in range(1, N_REVIEW + 1):
            rid = f"RVW{i:06d}"

            # Realistic review comments with different ratings
            rating_scores = {
                (10, 10, 10): [
                    "Bác sĩ giỏi, chuyên nghiệp, giải thích chi tiết!",
                    "Thú cưng của tôi đã khỏe lại nhờ điều trị tuyệt vời.",
                    "Phòng sạch, nhân viên thân thiện, dịch vụ chu đáo.",
                    "Sẽ quay lại và giới thiệu cho bạn bè, thực sự đáng giá!",
                    "Chuyên nghiệp từ bác sĩ đến nhân viên, rất hài lòng.",
                    "Thiết bị hiện đại, xử lý khoa học, bác sĩ tuyệt vời.",
                    "Không chỉ chữa bệnh mà còn dạy cách chăm sóc thú cưng.",
                    "Đã đem nhiều con đến, luôn hài lòng 100%.",
                    "Giá cả công bằng, chất lượng tuyệt hảo.",
                    "Đội ngũ bác sĩ cực kỳ tâm lý với thú cưng.",
                ],
                (9, 9, 9): [
                    "Rất tốt, chỉ tiếc một chút chốc chờ lâu hơi lâu.",
                    "Dịch vụ lành, nhân viên vui vẻ và chuyên nghiệp.",
                    "Bác sĩ giỏi, giải thích rõ tình trạng sức khỏe.",
                    "Khám kỹ lưỡng, khỏi hoàn toàn nên sẽ quay lại.",
                    "Nhân viên tư vấn rất tốt, bác sĩ tinh tế.",
                    "Phòng khám đẹp, bác sĩ vui tính và có tâm.",
                ],
                (8, 8, 8): [
                    "Khám tốt, bác sĩ giải thích kỹ, chỉ hơi đắt một chút.",
                    "Dịch vụ tốt, nhân viên vui vẻ, cơ sở vật chất ổn.",
                    "Rất ổn, nên tới đây nếu thú cưng bị bệnh.",
                    "Bác sĩ chuyên nghiệp, khám nhanh gọn, có kết quả.",
                    "Tuyệt, chỉ là thời gian khám hơi nhanh.",
                ],
                (7, 7, 7): [
                    "Được, bác sĩ còn được, cơ sở vật chất ổn.",
                    "Chấp được, giá cả hợp lý, khám cẩn thận.",
                    "Bình thường, dịch vụ ổn nhưng chưa tuyệt hảo.",
                    "Có thể ghé lại nếu cần thiết.",
                ],
                (6, 6, 6): [
                    "Bình thường, phục vụ ổn nhưng cần cải thiện.",
                    "Được, nhưng có chỗ chưa chu đáo lắm.",
                    "Khác biệt, giá hơi cao cho chất lượng này.",
                ],
            }

            # Pick rating based on probability (more 5-stars)
            rating = random.choices(
                [10, 9, 8, 7, 6], weights=[35, 30, 20, 10, 5]
            )[0]

            # Get appropriate comment for rating
            rating_key = (rating, rating, rating)
            default_comments = rating_scores[(7, 7, 7)]
            comment = pick(rating_scores.get(rating_key, default_comments))

            w.writerow(
                [
                    rid,
                    pick(customer_ids),
                    pick(invoice_ids),
                    rating,
                    rating,
                    rating,
                    comment,
                ]
            )

    # ---------- 37. INVOICE SERVICE ----------
    used_inv_srv = set()
    with open("InvoiceService.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(["InvoiceID", "ServiceID"])
        while len(used_inv_srv) < N_INVOICE_SERVICE:
            iid = pick(invoice_ids)
            sid = pick(service_ids)
            key = (iid, sid)
            if key in used_inv_srv:
                continue
            used_inv_srv.add(key)
            w.writerow([iid, sid])

    # ---------- 38. ACCOUNT LOGIN ----------
    with open("AccountLogin.csv", "w", newline="", encoding="utf-8-sig") as f:
        w = csv.writer(f)
        w.writerow(["Username", "Password"])
        for uname, pwd in sorted(accounts.items()):
            w.writerow([uname, pwd])

    print("Da tao xong tat ca CSV cho PetCareX_DB!")


if __name__ == "__main__":
    main()
