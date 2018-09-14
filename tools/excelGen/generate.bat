set PROTOCS="..\ProtoGen\CSharp\protogen.exe" -output_directory=..\..\client\Assets\Scripts\StaticData\

cd %cd%
del /s /q .\java\*.*
del /s /q .\data\*.*
del /s /q .\proto\*.*
del /s /q .\py\*.*


python tool.py BUILDING xls\Core_Sys.xlsm
python tool.py ITEM_RES xls\Core_Sys.xlsm
python tool.py PLAYER_LEVEL xls\Core_Sys.xlsm
python tool.py MANOR_LEVEL xls\Core_Sys.xlsm
python tool.py ROB_PROPORTION xls\Core_Sys.xlsm
python tool.py PLAYER_ATTR  xls\Core_Sys.xlsm
python tool.py ZOMBIE_ATTR xls\Core_Sys.xlsm
python tool.py ARITHMETIC_COEFFICIENT xls\Core_Sys.xlsm
python tool.py WORLD_EVENTS xls\Core_Sys.xlsm
python tool.py PURCHASE_LIM xls\Core_Sys.xlsm


python tool.py ATTRNAME xls\Bldg_Func.xlsm
python tool.py DAMI xls\Bldg_Func.xlsm
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
python tool.py LEIDA xls\Bldg_Func.xlsm
python tool.py CANGKU xls\Bldg_Func.xlsm
python tool.py DIANCHIZU xls\Bldg_Func.xlsm
python tool.py JIANSHENFANG xls\Bldg_Func.xlsm
python tool.py CHE1 xls\Bldg_Func.xlsm
python tool.py JSFADIANJI xls\Bldg_Func.xlsm
python tool.py TAIYANGNENG xls\Bldg_Func.xlsm
python tool.py LIANYOU xls\Bldg_Func.xlsm
python tool.py LIANGANG xls\Bldg_Func.xlsm
python tool.py HUNNINGTU xls\Bldg_Func.xlsm
python tool.py SONGSHU xls\Bldg_Func.xlsm
python tool.py MUCAIJIAGONG xls\Bldg_Func.xlsm
python tool.py QIANG xls\Bldg_Func.xlsm
python tool.py DAMEN xls\Bldg_Func.xlsm
python tool.py MAKEQIN xls\Bldg_Func.xlsm
python tool.py MG42 xls\Bldg_Func.xlsm


protoc.exe --java_out=./java proto/*.proto


for /f "delims=" %%i in ('dir /b "proto\*.proto"') do (
	%PROTOCS% proto\%%i
)
pause 