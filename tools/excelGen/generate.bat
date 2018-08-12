cd %cd%
del /s /q .\java\*.*
del /s /q .\data\*.*
del /s /q .\proto\*.*
del /s /q .\py\*.*


python tool.py BUILDING xls\Core_Sys.xlsm
python tool.py ITEM_RES xls\Core_Sys.xlsm


protoc.exe --java_out=./java proto/*.proto

pause 