set PROTOCS="..\ProtoGen\CSharp\protogen.exe" -output_directory=..\..\client\Assets\Scripts\StaticData\

cd %cd%
del /s /q .\java\*.*
del /s /q .\data\*.*
del /s /q .\proto\*.*
del /s /q .\py\*.*


python tool.py BUILDING xls\Core_Sys.xlsm
python tool.py ITEM_RES xls\Core_Sys.xlsm

python tool.py DAMI xls\Bldg_Func.xlsm
python tool.py SHUCAI xls\Bldg_Func.xlsm
python tool.py HUAFEI xls\Bldg_Func.xlsm
python tool.py SHUIGUO xls\Bldg_Func.xlsm
python tool.py ZHUJUAN xls\Bldg_Func.xlsm
python tool.py SILIAO xls\Bldg_Func.xlsm
python tool.py JING xls\Bldg_Func.xlsm
python tool.py LUSHUI xls\Bldg_Func.xlsm
python tool.py KUANGQUANSHUI xls\Bldg_Func.xlsm
python tool.py WUXIANDIAN xls\Bldg_Func.xlsm
python tool.py CANGKU xls\Bldg_Func.xlsm
python tool.py DIANCHIZU xls\Bldg_Func.xlsm
python tool.py JIANSHENFANG xls\Bldg_Func.xlsm
python tool.py JSFADIANZHAN xls\Bldg_Func.xlsm
python tool.py TAIYANGNENG xls\Bldg_Func.xlsm
python tool.py LIANYOU xls\Bldg_Func.xlsm
python tool.py LIANGANG xls\Bldg_Func.xlsm
python tool.py HUNNINGTU xls\Bldg_Func.xlsm
python tool.py SONGSHU xls\Bldg_Func.xlsm
python tool.py TIEHUA xls\Bldg_Func.xlsm
python tool.py MUCAIJIAGONG xls\Bldg_Func.xlsm
python tool.py QIANG xls\Bldg_Func.xlsm
python tool.py DAMEN xls\Bldg_Func.xlsm

protoc.exe --java_out=./java proto/*.proto


for /f "delims=" %%i in ('dir /b "proto\*.proto"') do (
	%PROTOCS% %%i
)
pause 