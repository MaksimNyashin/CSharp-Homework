@echo off
set /A need_build=1
set /A need_info=0
for %%x in (%*) do (
	if "%%x"=="-b" (
		set need_build=0
	)
	if "%%x"=="-i" (
		set need_info=1
	)
)
if %need_build% == 1 (
	if %need_info% == 0 (
		call %~dp0\build.bat
	) else (
		call %~dp0\build.bat -i
	)
)
set /p suc=< %~dp0\suc.txt
if %suc%==1 (
	set /p texte=< %~dp0\files.txt
	cls
	::echo run: %texte%
	%~dp0\%texte%
)