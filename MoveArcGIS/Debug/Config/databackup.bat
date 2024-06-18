@echo off
rem =============================================
rem 需要配置的参数说明:
rem BKDIR：是调用本批处理时传进来的参数 BKFILE即要备份到的数据文件目录。
rem Author:王政
rem ==================================================
echo 备份开始...
echo 当前的时间是： %DATE% %time%
set BKDIR=D:\
set BKFILE=%Date:0,4%%Date:5,2%%Date:8,2%
set HHMMSS=%time:~0,2%%time:~3,2%%time:~6,2%
set Instance=5151
set Server=192.168.15.236
set User=sde
set Pw=sde
if not exist %BKDIR%\%BKFILE%_LOGIC ( md %BKDIR%\%BKFILE%_LOGIC ) else ( echo 目录 %BKDIR%\%BKFILE%_LOGIC 已经存在)
net send %userdomain%  数据库逻辑备份已于:%DATE% %time% 完成！
echo .
echo 备份已于：%DATE% %time% 完成！
echo .
rem 移动批处理的日志文件到备份目录下面
xcopy %BAT_HOME%\LogicBackup_%BKFILE%*.log %BKDIR%\%BKFILE%_LOGIC\
echo 数据已成功导出
