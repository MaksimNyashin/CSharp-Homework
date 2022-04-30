@echo off
set /p dir_name="Directory name: "
echo %dir_name%
mkdir %dir_name%
copy Test\cp.bat %dir_name%
cd %dir_name%
cp.bat -i