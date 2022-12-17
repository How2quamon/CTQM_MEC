# CTQM CAR

## LƯU Ý: 
- Khi làm bài pull từ nhánh main về.
- Các css bỏ vào thư mục css.
- Các trang bỏ vào thư mục views.

# Cú pháp: 
## Thay Remote:
- Git init
- Git remote add origin link

## Clone code:
- Trước khi làm, nếu có thay đổi ở nhánh chính thì:
- - Tải code từ nhánh về: git pull origin Tên nhánh (cập nhập thêm cái file chưa có)
- - npm i (để tải các gói về)
- Clone code từ nhánh: git clone link --branch Tên nhánh

## Tạo nhánh mới: 
- Kiểm tra nhánh: git branch
- Tạo nhánh mới: git branch Tên nhánh mới
- Xóa nhánh: git branch -d Tên nhánh
- Truy cập vào nhánh: git checkout Tên nhánh
- Vừa tạo vừa truy cập: git checkout -b Tên nhánh

## Đẩy code: (Lưu ý: phải clone code về thì vscode mới tự động tải lên lại link trang clone)
- git init
- git add . 
- git commit -m “cập nhật cái gì ghi vô”
- Push lên nhánh: git push -u origin Nhánh (Nhớ chuyển vào nhánh của mình trước)
- (Hoặc) git push --set-upstream origin Nhánh

# Sau khi xong nếu thấy code chạy ổn thì nhớ tạo 1 pull request
