@echo off
cd /d %~dp0
@echo on
REM ===== VCSExpress build =====
REM "C:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\VCSExpress.exe" FormsApplication02.csproj /rebuild 
"C:\Program Files\Microsoft Visual Studio 10.0\Common7\IDE\VCSExpress.exe" RichTextEditor.csproj /rebuild 

REM cp bin/Debug/FormsApplication02.exe .
REM cp obj/x86/Debug/*.resources .
cp bin/Release/RichTextEditor.exe .
REM cp obj/x86/Release/*.resources .
rm -rf bin obj Properties *.user *.suo *.vshost.exe *.pdb *.manifest
REM pause
