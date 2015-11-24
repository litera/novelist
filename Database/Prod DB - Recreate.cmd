@cd /d %~dp0
@sqlcmd -S ad1a505f-8641-431f-a197-a555014106b6.sqlserver.sequelizer.com -d dbad1a505f8641431fa197a555014106b6 -U devyiztmmwihzngi -P ex3fFkfWFTCFKb6dNQ3LkAaL5Z7pyNpFdKvPFqf5fczHzn3C5pLoKKiKxqjPnXw2 -b -i "01 Drop Model.sql","02 Create Model.sql","03 Types.sql","04 Functions.sql","05 Procedures.sql","06 Static Data.sql","07 Test Data.sql"
@pause