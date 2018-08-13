set PROTOCS="..\ProtoGen\CSharp\protogen.exe" -output_directory=..\..\client\Assets\Scripts\StaticData\

cd %cd%
del /s /q .\java\*.*
del /s /q .\data\*.*
del /s /q .\proto\*.*
del /s /q .\py\*.*


python tool.py BUILDING xls\Core_Sys.xlsm
python tool.py ITEM_RES xls\Core_Sys.xlsm


protoc.exe --java_out=./java proto/*.proto


for /f "delims=" %%i in ('dir /b "proto\*.proto"') do (
	%PROTOCS% %%i
)
pause 