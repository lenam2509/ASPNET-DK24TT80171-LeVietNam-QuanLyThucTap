-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Máy chủ: 127.0.0.1
-- Thời gian đã tạo: Th6 26, 2026 lúc 05:20 AM
-- Phiên bản máy phục vụ: 8.0.45
-- Phiên bản PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `internship_db`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `enterprises`
--

CREATE TABLE `enterprises` (
  `MaDN` int NOT NULL,
  `TenDN` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DiaChi` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Đang đổ dữ liệu cho bảng `enterprises`
--

INSERT INTO `enterprises` (`MaDN`, `TenDN`, `DiaChi`) VALUES
(1, 'CTY MQ SOLUTION', '199/8 Đ. Ngô Chí Quốc, Tam Bình, Hồ Chí Minh'),
(2, 'CTY FPT', 'Tòa nhà FPT, số 10 phố Phạm Văn Bạch, phường Cầu Giấy, Thành phố Hà Nội, Việt Nam.');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `internshipregistrations`
--

CREATE TABLE `internshipregistrations` (
  `Id` int NOT NULL,
  `MSSV` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `MaDN` int NOT NULL,
  `NguoiHuongDanDN` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ThoiGianThucTap` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `TrangThai` int NOT NULL,
  `MaGV` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NhanXetGV` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Diem` double DEFAULT NULL,
  `FileBaoCao` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Đang đổ dữ liệu cho bảng `internshipregistrations`
--

INSERT INTO `internshipregistrations` (`Id`, `MSSV`, `MaDN`, `NguoiHuongDanDN`, `ThoiGianThucTap`, `TrangThai`, `MaGV`, `NhanXetGV`, `Diem`, `FileBaoCao`) VALUES
(1, 'SV01', 1, 'Lê Tấn Phát', '2 tháng', 1, 'GV02', 'tốt', 10, '/uploads/8a8c2ed2-a5a1-43a4-a579-4cc7123027be_682_CV 682.sign.pdf'),
(2, 'SV02', 1, 'Lê Tuấn Trần', '2 tháng', 1, 'GV01', NULL, NULL, NULL),
(3, 'SV03', 2, 'Lê Thạch Đạo', '2 tháng', 1, 'GV02', 'ok', 9, '/uploads/d98fde35-f076-468d-a32a-dd24da5a1363_QUYET DINH 2506_signed.pdf');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `lecturers`
--

CREATE TABLE `lecturers` (
  `MaGV` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `HoTen` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Khoa` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Password` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Đang đổ dữ liệu cho bảng `lecturers`
--

INSERT INTO `lecturers` (`MaGV`, `HoTen`, `Khoa`, `Password`) VALUES
('GV01', 'LÊ VĂN TÈO', 'CNTT', '$2a$11$cqQsxXdBmyza/HQaIIc22e1i2ODJc6gQqTrUtimA/i4jRrgruR6Pq'),
('GV02', 'LÊ NHẬT THỊNH', 'CNTT', '$2a$11$30i.27QtMjOZ95d5/qAER.fWM/GgccxTqugkdGuSb2cExKMKermTK');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `students`
--

CREATE TABLE `students` (
  `MSSV` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `HoTen` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Lop` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Nganh` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `KhoaHoc` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Password` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `HocKy` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Đang đổ dữ liệu cho bảng `students`
--

INSERT INTO `students` (`MSSV`, `HoTen`, `Lop`, `Nganh`, `KhoaHoc`, `Password`, `HocKy`) VALUES
('SV01', 'LÊ VIỆT NAM', 'DK24TT80171', 'CNTT', 'ASP.NET', '$2a$11$Hx1y3PcQEtffhvZ5RYiCzOFmbY2.qqMGFa6YucVyxpEHRkGTjclXC', 'Học kỳ 1'),
('SV02', 'LÊ NHẬT THỊNH', 'DK24TT80172', 'CNTT', 'ASP.NET', '$2a$11$dhCf7z59.21S4aKwm4RsKOgHPwm4hX4O/lD0Oytnj0K9TPwqwvwf.', 'Học kỳ 1'),
('SV03', 'LÊ THỊ NHÀN', 'DK24TT80173', 'CNTT', 'ASP.NET', '$2a$11$0vxkQ/XtHzu6doB9wqZOKevNoiW1GQfvGxeKwXgSnYw663R/1AhIC', 'Học kỳ 2'),
('SV04', 'LÊ TIẾN DŨNG', 'DK24TT80171', 'CNTT', 'ASP.NET', '$2a$11$yiBHFej19Q.TJvFpsujp2eBvGUNB302t3A4QcOU9TaWi7Q.fzmG0K', 'Học kỳ 1');

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `enterprises`
--
ALTER TABLE `enterprises`
  ADD PRIMARY KEY (`MaDN`);

--
-- Chỉ mục cho bảng `internshipregistrations`
--
ALTER TABLE `internshipregistrations`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_InternshipRegistrations_MaDN` (`MaDN`),
  ADD KEY `IX_InternshipRegistrations_MaGV` (`MaGV`),
  ADD KEY `IX_InternshipRegistrations_MSSV` (`MSSV`);

--
-- Chỉ mục cho bảng `lecturers`
--
ALTER TABLE `lecturers`
  ADD PRIMARY KEY (`MaGV`);

--
-- Chỉ mục cho bảng `students`
--
ALTER TABLE `students`
  ADD PRIMARY KEY (`MSSV`);

--
-- AUTO_INCREMENT cho các bảng đã đổ
--

--
-- AUTO_INCREMENT cho bảng `enterprises`
--
ALTER TABLE `enterprises`
  MODIFY `MaDN` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT cho bảng `internshipregistrations`
--
ALTER TABLE `internshipregistrations`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Các ràng buộc cho các bảng đã đổ
--

--
-- Các ràng buộc cho bảng `internshipregistrations`
--
ALTER TABLE `internshipregistrations`
  ADD CONSTRAINT `FK_InternshipRegistrations_Enterprises_MaDN` FOREIGN KEY (`MaDN`) REFERENCES `enterprises` (`MaDN`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_InternshipRegistrations_Lecturers_MaGV` FOREIGN KEY (`MaGV`) REFERENCES `lecturers` (`MaGV`),
  ADD CONSTRAINT `FK_InternshipRegistrations_Students_MSSV` FOREIGN KEY (`MSSV`) REFERENCES `students` (`MSSV`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
