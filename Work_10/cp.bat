@echo off
set /A need_copy_init=0
for %%x in (%*) do (
	if "%%x"=="-i" (
		set need_copy_init=1
	)
)
if %need_copy_init%==1 (
	mkdir CompileFolder
	copy /-Y ..\Test\CompileFolder\files.txt CompileFolder
	COPY /-Y ..\Test\Main.cs
	COPY /-Y ..\Test\_build.bat
	COPY /-Y ..\Test\_run.bat
)
COPY /Y ..\Test\CompileFolder\build.bat CompileFolder
COPY /Y ..\Test\CompileFolder\run.bat CompileFolder
COPY /Y ..\Test\CompileFolder\CheckSuccess.py CompileFolder
COPY /Y ..\Test\startcmd.bat
pause