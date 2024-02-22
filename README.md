# Job24H
Project : website to find jobs
Website tìm việc làm
Role : Nhà tuyển dụng, Người Tìm việc
Nhà Tuyển Dụng : Đang tin tuyển dụng, xem thông tin tuyển dụng, xem các hồ sơ ứng tuyển,...
Người Tìm Việc : Tìm việc, ứng tuyển, Tạo hồ sơ ứng tuyển online,...

Setup and config:
Run on .NET 6
Trong Project 
   Back-end: 
+ Thay đổi đường dẫn đến database trong file appsettings.
+ Thực hiện Migration qua terminal bằng câu lênh Entity Frameword : dotnet ef migrations add [Name Migration] / Code first : gen database từ Model trong project
+ Xóa hết data hiện có trong database chạy file sql : Viẹclam24H.sql để có data
Run 2 project Back-end và Front-end song song.
