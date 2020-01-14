@echo off
echo Using Java 8
SET cwd=%cd%
cd %2
"C:\Program Files\Java\jdk1.8.0_171\bin\java.exe" -jar "%cwd%/RunTest8.jar" %1 %2
@REM "C:\Program Files\Java\jdk1.8.0_152\bin\java.exe" -jar RunTest8.jar %1 %2