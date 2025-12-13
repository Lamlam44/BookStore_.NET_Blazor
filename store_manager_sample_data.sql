-- =====================================================
-- STORE MANAGER DATABASE - SAMPLE DATA SCRIPT (FIXED)
-- =====================================================

-- Tắt kiểm tra khóa ngoại tạm thời để tránh lỗi nếu thứ tự chưa chuẩn tuyệt đối
SET FOREIGN_KEY_CHECKS = 0;

-- =====================================================
-- 1. ACCOUNTS (Tài khoản - Độc lập)
-- =====================================================
INSERT INTO `Accounts` (Id, Username, PasswordHash, Email, PositionName, Phone, RoleName, IsActive, HireDate, CreatedAt, UpdatedAt, IsDeleted) VALUES
('acc-admin-001', 'admin', '$2a$11$DyVN1GyOqLvLVRwNB3LjWO0aEjTHjJiMvpVyTBs1e9hVjVvqPYDdK', 'admin@bookstore.com', 'Quản lý cửa hàng', '0923456789', 'ADMIN', 1, '2024-01-15', NOW(), NOW(), 0),
('acc-staff-001', 'staff01', '$2a$11$bvVD2eW8nj9k8lLmPqRxzO.d8eKsJzYzVxYxZkHkI9eYfXvVpqMXW', 'staff01@bookstore.com', 'Nhân viên bán hàng', '0933456789', 'STAFF', 1, '2024-02-01', NOW(), NOW(), 0),
('acc-staff-002', 'staff02', '$2a$11$vVyYzxAb.kLmNoPqRsT9aU1vVyYzxAbCdEfGhIjKlMnOpQrStUvWx', 'staff02@bookstore.com', 'Nhân viên kho', '0943456789', 'STAFF', 1, '2024-02-15', NOW(), NOW(), 0),
('acc-cashier-001', 'cashier01', '$2a$11$cDxYzAbCdEfGhIjKlMnOpQrStUvWxYzAbCdEfGhIjKlMnOpQrStUv', 'cashier01@bookstore.com', 'Thu ngân', '0953456789', 'STAFF', 1, '2024-03-01', NOW(), NOW(), 0);

-- =====================================================
-- 2. CATEGORIES (Danh mục - Độc lập)
-- =====================================================
INSERT INTO `Categories` (Id, CategoryName, CategoryCode, Status, CreatedAt, UpdatedAt, IsDeleted) VALUES
('cat-001', 'Tiểu thuyết', 'NOVEL', 'ACTIVE', NOW(), NOW(), 0),
('cat-002', 'Khoa học - Công nghệ', 'TECH', 'ACTIVE', NOW(), NOW(), 0),
('cat-003', 'Kinh tế - Kinh doanh', 'BUSINESS', 'ACTIVE', NOW(), NOW(), 0),
('cat-004', 'Tự help - Phát triển bản thân', 'SELFHELP', 'ACTIVE', NOW(), NOW(), 0),
('cat-005', 'Trẻ em', 'CHILDREN', 'ACTIVE', NOW(), NOW(), 0);

-- =====================================================
-- 3. AUTHORITIES / AUTHORS (Tác giả - Độc lập)
-- =====================================================
INSERT INTO `Authorities` (Id, Name, Code, Status, CreatedAt, UpdatedAt, IsDeleted) VALUES
('author-001', 'Nguyễn Nhật Ánh', 'NNA-001', 'ACTIVE', NOW(), NOW(), 0),
('author-002', 'Haruki Murakami', 'HM-001', 'ACTIVE', NOW(), NOW(), 0),
('author-003', 'George R.R. Martin', 'GRM-001', 'ACTIVE', NOW(), NOW(), 0),
('author-004', 'J.K. Rowling', 'JKR-001', 'ACTIVE', NOW(), NOW(), 0),
('author-005', 'Kazuo Ishiguro', 'KI-001', 'ACTIVE', NOW(), NOW(), 0),
('author-006', 'Margaret Atwood', 'MA-001', 'ACTIVE', NOW(), NOW(), 0);

-- =====================================================
-- 4. PUBLISHERS (Nhà xuất bản - Độc lập)
-- =====================================================
INSERT INTO `Publisher` (Id, Name, Address, Code, Status, CreatedAt, UpdatedAt, IsDeleted) VALUES
('pub-001', 'NXB Trẻ', '123 Đường Trần Hưng Đạo, TP.HCM', 'NXB-TRE', 'ACTIVE', NOW(), NOW(), 0),
('pub-002', 'NXB Lao Động', '456 Đường Hai Bà Trưng, Hà Nội', 'NXB-LD', 'ACTIVE', NOW(), NOW(), 0),
('pub-003', 'NXB Hội Nhà Văn', '789 Đường Phan Bội Châu, Hà Nội', 'NXB-HNV', 'ACTIVE', NOW(), NOW(), 0),
('pub-004', 'Penguin Books', '321 Oxford Street, London, UK', 'PENGUIN', 'ACTIVE', NOW(), NOW(), 0),
('pub-005', 'Harper Collins', '654 Market Street, New York, USA', 'HARPER', 'ACTIVE', NOW(), NOW(), 0);

-- =====================================================
-- 5. SUPPLIERS (Nhà cung cấp - Độc lập)
-- =====================================================
INSERT INTO `Suppliers` (Id, SupplierName, ContactPerson, Phone, Address, CreatedAt, UpdatedAt, IsDeleted) VALUES
('sup-001', 'Công ty Phân phối sách Trẻ', 'Trần Văn A', '0234567890', '123 Đường Lê Lợi, TP.HCM', NOW(), NOW(), 0),
('sup-002', 'NXB Lao Động - Chi nhánh Hà Nội', 'Phạm Thị B', '0243456789', '456 Đường Đê La Thành, Hà Nội', NOW(), NOW(), 0),
('sup-003', 'Công ty TNHH Sách Việt', 'Lê Văn C', '0525678901', '789 Đường Nguyễn Huệ, Đà Nẵng', NOW(), NOW(), 0),
('sup-004', 'Penguin Books Distribution', 'John Smith', '0865432109', '321 Oxford Street, London, UK', NOW(), NOW(), 0),
('sup-005', 'Harper Collins Asia', 'Maria Garcia', '0973456789', '654 Market Street, Singapore', NOW(), NOW(), 0);

-- =====================================================
-- 6. BOOKS (Sách - Phụ thuộc: Authors, Publishers, Categories)
-- (Phần này bị thiếu trong code cũ của bạn, tôi đã bổ sung lại để tránh lỗi)
-- =====================================================
INSERT INTO `Books` (Id, Title, AuthorId, PublisherId, Isbn, CategoryId, RetailPrice, Image, Status, CreatedAt, UpdatedAt, IsDeleted) VALUES
('book-001', 'Thời niên thiếu của tôi', 'author-001', 'pub-001', '9786043732055', 'cat-001', 85000, 'https://example.com/img1.jpg', 'SELL_ACTIVE', NOW(), NOW(), 0),
('book-002', 'Norwegian Wood', 'author-002', 'pub-003', '9786043732050', 'cat-001', 120000, 'https://example.com/img2.jpg', 'SELL_ACTIVE', NOW(), NOW(), 0),
('book-003', 'A Game of Thrones', 'author-003', 'pub-005', '9780553103547', 'cat-001', 250000, 'https://example.com/img3.jpg', 'SELL_ACTIVE', NOW(), NOW(), 0),
('book-004', 'Harry Potter', 'author-004', 'pub-004', '9780747532699', 'cat-005', 180000, 'https://example.com/img4.jpg', 'SELL_ACTIVE', NOW(), NOW(), 0),
('book-005', 'Never Let Me Go', 'author-005', 'pub-004', '9780203394676', 'cat-001', 150000, 'https://example.com/img5.jpg', 'SELL_ACTIVE', NOW(), NOW(), 0),
('book-006', 'The Handmaids Tale', 'author-006', 'pub-005', '9780385490818', 'cat-001', 220000, 'https://example.com/img6.jpg', 'SELL_ACTIVE', NOW(), NOW(), 0),
('book-007', 'Lập trình Python', 'author-001', 'pub-002', '9786043732140', 'cat-002', 189000, 'https://example.com/img7.jpg', 'SELL_ACTIVE', NOW(), NOW(), 0),
('book-008', 'The Lean Startup', 'author-002', 'pub-004', '9780307887894', 'cat-003', 210000, 'https://example.com/img8.jpg', 'SELL_ACTIVE', NOW(), NOW(), 0);

-- =====================================================
-- 7. INVENTORIES (Tồn kho - Phụ thuộc: Books)
-- =====================================================
INSERT INTO `Inventories` (Id, BookId, AvailableStock, ReservedStock, CreatedAt, UpdatedAt, IsDeleted) VALUES
('inv-001', 'book-001', 50, 0, NOW(), NOW(), 0),
('inv-002', 'book-002', 30, 2, NOW(), NOW(), 0),
('inv-003', 'book-003', 20, 1, NOW(), NOW(), 0),
('inv-004', 'book-004', 45, 3, NOW(), NOW(), 0),
('inv-005', 'book-005', 25, 0, NOW(), NOW(), 0),
('inv-006', 'book-006', 35, 2, NOW(), NOW(), 0),
('inv-007', 'book-007', 60, 5, NOW(), NOW(), 0),
('inv-008', 'book-008', 28, 1, NOW(), NOW(), 0);

-- =====================================================
-- 8. VOUCHERS (Phiếu giảm giá - Độc lập)
-- =====================================================
INSERT INTO `Vouchers` (Id, Name, Code, Type, DiscountValue, MinOrderValue, MaxDiscountValue, UsageCount, StartDate, EndDate, IsActive, CreatedAt, UpdatedAt, IsDeleted) VALUES
('vouch-001', 'Giảm 10% tất cả sách', 'SAVE10', 'Percentage', 10, 0, 500000, 100, '2024-11-01', '2024-12-31', 1, NOW(), NOW(), 0),
('vouch-002', 'Giảm 50.000đ đơn hàng', 'SAVE50K', 'AMOUNT', 50000, 100000, 50000, 50, '2024-12-01', '2024-12-31', 1, NOW(), NOW(), 0),
('vouch-003', 'Giảm 15% cho tiểu thuyết', 'NOVEL15', 'Percentage', 15, 200000, 300000, 30, '2024-12-01', '2024-12-31', 1, NOW(), NOW(), 0),
('vouch-004', 'Black Friday - Giảm 30%', 'BLACKFRI30', 'Percentage', 30, 500000, 1000000, 20, '2024-11-20', '2024-11-30', 0, NOW(), NOW(), 0),
('vouch-005', 'Miễn phí vận chuyển', 'FREESHIP', 'AMOUNT', 30000, 300000, 30000, 100, '2024-12-01', '2024-12-31', 1, NOW(), NOW(), 0);

-- =====================================================
-- 9. CUSTOMERS (Khách hàng - Độc lập)
-- =====================================================
INSERT INTO `customers` (Id, Name, Address, Phone, CreatedAt, UpdatedAt, IsDeleted) VALUES
('cust-001', 'Nguyễn Văn Sáng', '123 Nguyễn Hữu Cảnh, HCM', '0901234567', NOW(), NOW(), 0),
('cust-002', 'Trần Thị Hương', '456 Lê Lợi, HCM', '0912345678', NOW(), NOW(), 0);