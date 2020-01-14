@echo off
echo Using java 12
SET cwd=%cd%
cd %2
"C:\Program Files\Java\jdk-12.0.2\bin\java.exe" -Djava.system.class.loader=MyClassLoader -jar "%cwd%/RunTest12.jar" %1 %2