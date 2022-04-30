@echo off
for /F "tokens=1,2 delims=#" %%a in ('"prompt #$H#$E# & echo on & for %%b in (1) do rem"') do (
  set "DEL=%%a"
)

setlocal enabledelayedexpansion
set res=csc
set prefix=
for /f "tokens=*" %%a in (%~dp0\files.txt) do (
	set userstr=%%a
	set first=!userstr:~0,1!
	if !first!==- (
		set prefix=!userstr:~1!
	) else (
		if [!prefix!]==[] (
			set res=!res! %~dp0..\%%a.cs 
		) else (
			set res=!res! \!prefix!:%~dp0..\%%a.dll 
		)
	)
)
:: !res! >> CheckSuccess.py
:: !res!

set /A need_info=0
for %%x in (%*) do (
	if "%%x"=="-i" (
		set need_info=1
	)
)

if %need_info%==1 (
	set res=!res! -define:INFO;RUN
)

%res%|%~dp0\CheckSuccess.py

set /p suc=<%~dp0\suc.txt
if %suc%==1 (
	call :colorEcho 0a "Build finished successfully!"
) else (
	call :colorEcho 04 "Build failed!"
)
:: echo.

goto :eof

:colorEcho
echo %DEL% > "%~2"
findstr /v /a:%1 /R "^$" "%~2" nul
del "%~2" > nul 2>&1
EXIT /B 0

:eof