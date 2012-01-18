@ECHO %0 %*
@pushd
@cd /D "%~dp0"
@cd

REM Replace with valid path to Ext

rd Apps\CandyShop\client\lib\ext
mklink /D Apps\CandyShop\client\lib\ext C:\Code\Lib\Ext\ext-4.0.7

@pause
@popd