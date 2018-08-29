::编译cs_proto的指令
set PROTOCS="..\tools\ProtoGen\CSharp\protogen.exe" -output_directory=..\client\Assets\Scripts\Proto\

::编译java_proto的指令
set JAVA_COMPILER_PATH=..\tools\ProtoGen\Java\protoc.exe

::遍历所有文件
set JAVA_TARGET_PATH=..\server\framework\src\
for /f "delims=" %%i in ('dir /b ".\*.proto"') do (
    ::echo %JAVA_COMPILER_PATH% --java_out=%JAVA_TARGET_PATH% %SOURCE_FOLDER%\%%i
    %JAVA_COMPILER_PATH% --java_out=%JAVA_TARGET_PATH% %%i
    if "%%i" == "message.proto" (
    	%PROTOCS% %%i --include_imports fighting.proto
	) else (
		%PROTOCS% %%i
	)
)
pause