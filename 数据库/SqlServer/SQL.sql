--�������ݿ�
--�򵥴���
CREATE DATABASE Demo_CJL
--��ϸ���ô���
CREATE DATABASE Demo_CJL
ON
(
	NAME='Demo_CJL',
	--·��
	FILENAME='D:\Demo\Demo_CJL.mdf',
	--��ʼ��С
	SIZE=5MB,
	--����С
	MAXSIZE=20MB,
	--ÿ����������
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
--���ݿ��Ƿ�����ж�
IF EXISTS(SELECT * FROM sysdatabases WHERE name='Demo_CJL')
BEGIN
	SELECT '���ݿ��Ѵ���'
END
ELSE
BEGIN
	SELECT 'ִ�����ݿⴴ��'
END
--ɾ�����ݿ�
DROP DATABASE Demo_CJL
--�������ݿ�
----��ӱ�������
EXEC sp_addumpdevice 'disk','Demo_CJLBack','D:\Demo\Demo_CJL.bak'
----ָ����ִ��ָ����������
BACKUP DATABASE Demo_CJL TO Demo_CJLBack
--������
CREATE TABLE Demo_CJL
(
	--����Ϊ����������
	Id INT PRIMARY KEY IDENTIFIED(1,1),
	Guid VARCHAR(40)
)