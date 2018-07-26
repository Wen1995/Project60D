cd %cd%
del /s /q .\java\*.*
del /s /q .\data\*.*
del /s /q .\proto\*.*
del /s /q .\py\*.*

python tool.py GLOBAL_CONFIG xls\Global.xlsm

python tool.py USER_INIT_BLDG xls\Global.xlsm
python tool.py USER_INIT_TECH xls\Global.xlsm
python tool.py USER_INIT_ITEM xls\Global.xlsm

python tool.py MINERAL_FIN xls\Global.xlsm

python tool.py BFP_TABLE_LIST xls\Global.xlsm





python tool.py TECH_BLDG xls\Core_Sys.xlsm
python tool.py TECH_PROD xls\Core_Sys.xlsm

python tool.py BUILDING xls\Core_Sys.xlsm
python tool.py BLDG_FUNC_PARAM xls\Core_Sys.xlsm

python tool.py ITEM_MIN xls\Core_Sys.xlsm
python tool.py ITEM_RES xls\Core_Sys.xlsm

python tool.py SHIP_MOD_TECH xls\Core_Sys.xlsm
python tool.py SMT_AVAIL_HULL_LIST xls\Core_Sys.xlsm
python tool.py SMT_AVAIL_CPNT_LIST xls\Core_Sys.xlsm
python tool.py SHIP_HULL_TECH xls\Core_Sys.xlsm
python tool.py SHIP_CPNT_TECH xls\Core_Sys.xlsm
python tool.py SHIP_TRAIT xls\Core_Sys.xlsm

python tool.py TASK_ONLINE_TIME xls\Core_Sys.xlsm




python tool.py BLDG_SS xls\Bldg_Func.xlsm
python tool.py BLDG_SPACEPORT xls\Bldg_Func.xlsm
python tool.py BLDG_FUNC_HULL_FACT xls\Bldg_Func.xlsm
python tool.py BLDG_CPNT_FACT xls\Bldg_Func.xlsm
python tool.py BLDG_SURF_COL xls\Bldg_Func.xlsm
python tool.py BLDG_REFNY xls\Bldg_Func.xlsm
python tool.py BLDG_POWER_PLANT xls\Bldg_Func.xlsm
python tool.py BLDG_ITEM_STG xls\Bldg_Func.xlsm


python tool.py GUIDE xls\Guide.xlsm

protoc.exe --java_out=./java proto/*.proto

call "generateVersion.bat" > .\data\version.txt

pause 