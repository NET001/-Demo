--创建数据库
--简单创建
CREATE DATABASE Demo_CJL
--详细配置创建
CREATE DATABASE Demo_CJL
ON
(
	NAME='Demo_CJL',
	--路径
	FILENAME='D:\Demo\Demo_CJL.mdf',
	--初始大小
	SIZE=5MB,
	--最大大小
	MAXSIZE=20MB,
	--每次增长多少
	FILEGROWTH=10%
)
LOG ON
(
	NAME='Demo_CJL_LOG',
	FILENAME='D:\Demo\Demo_CJL_log.ldf',
	SIZE=2MB,
	MAXSIZE=5MB,
	FILEGROWTH=1MB
)
--数据库是否存在判断
IF EXISTS(SELECT * FROM sysdatabases WHERE name='Demo_CJL')
BEGIN
	SELECT '数据库已存在'
END
ELSE
BEGIN
	SELECT '执行数据库创建'
END
--删除数据库
DROP DATABASE Demo_CJL
--备份数据库
----添加备份配置
EXEC sp_addumpdevice 'disk','Demo_CJLBack','D:\Demo\Demo_CJL.bak'
----指定表执行指定备份配置
BACKUP DATABASE Demo_CJL TO Demo_CJLBack
--创建表
CREATE TABLE Demo_CJL
(
	--设置为自增长主机
	Id INT PRIMARY KEY IDENTIFIED(1,1),
	Guid VARCHAR(40)
)