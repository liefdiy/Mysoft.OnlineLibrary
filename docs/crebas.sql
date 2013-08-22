/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2013/8/21 13:15:12                           */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('BatchLog')
            and   type = 'U')
   drop table BatchLog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Books')
            and   type = 'U')
   drop table Books
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Category')
            and   type = 'U')
   drop table Category
go

if exists (select 1
            from  sysobjects
           where  id = object_id('History')
            and   type = 'U')
   drop table History
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Readers')
            and   type = 'U')
   drop table Readers
go

/*==============================================================*/
/* Table: BatchLog                                              */
/*==============================================================*/
create table BatchLog (
   BatchId              numeric              identity,
   Reader               varchar(16)          null,
   BooksCount           int                  null,
   BookList             varchar(64)          null,
   CreateDate           datetime             null default getdate(),
   constraint PK_BATCHLOG primary key (BatchId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�Ѳ������10W�����㣬�޶�ÿ��������10���飬BookList���ȵ���5*10+1*10,5��BookId��󳤶ȣ�1�Ƿָ���',
   'user', @CurrentUser, 'table', 'BatchLog', 'column', 'BooksCount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���ν���ͼ��id�б��ö��ŷָ�',
   'user', @CurrentUser, 'table', 'BatchLog', 'column', 'BookList'
go

/*==============================================================*/
/* Table: Books                                                 */
/*==============================================================*/
create table Books (
   BookId               int                  identity,
   CategoryId           int                  null,
   BookSerialNo         varchar(20)          null,
   BookName             nvarchar(100)        null,
   Author               nvarchar(200)        null,
   TranslationAuthor    nvarchar(200)        null,
   ISBN                 varchar(13��          null,
   Press                nvarchar(50)         null,
   PublishDate          Datetime             null,
   Price                decimal(8,2)         null,
   Grade                int                  null,
   PicUrl               varchar(256)         null,
   Brief                nvachar(1000)        null,
   Status               int                  null default 1,
   CreateDate           datetime             null default getdate(),
   constraint PK_BOOKS primary key (BookId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���id',
   'user', @CurrentUser, 'table', 'Books', 'column', 'CategoryId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ͼ���ţ�����ͼ�������ҪΨһ��Լ��',
   'user', @CurrentUser, 'table', 'Books', 'column', 'BookSerialNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ͼ������',
   'user', @CurrentUser, 'table', 'Books', 'column', 'BookName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���ߣ�����ö��ŷָ�',
   'user', @CurrentUser, 'table', 'Books', 'column', 'Author'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '������',
   'user', @CurrentUser, 'table', 'Books', 'column', 'TranslationAuthor'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ISBN',
   'user', @CurrentUser, 'table', 'Books', 'column', 'ISBN'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '������',
   'user', @CurrentUser, 'table', 'Books', 'column', 'Press'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 'Books', 'column', 'PublishDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', @CurrentUser, 'table', 'Books', 'column', 'Price'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���֣�0~5��',
   'user', @CurrentUser, 'table', 'Books', 'column', 'Grade'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ͼ�����url',
   'user', @CurrentUser, 'table', 'Books', 'column', 'PicUrl'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���',
   'user', @CurrentUser, 'table', 'Books', 'column', 'Brief'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ͼ��״̬��1�ڹݣ�2��裬3��ʧ��4�ƻ�����',
   'user', @CurrentUser, 'table', 'Books', 'column', 'Status'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ʱ��',
   'user', @CurrentUser, 'table', 'Books', 'column', 'CreateDate'
go

/*==============================================================*/
/* Table: Category                                              */
/*==============================================================*/
create table Category (
   CategoryId           int                  not null,
   FatherId             int                  null,
   CategoryName         nvarchar(50)         null,
   constraint PK_CATEGORY primary key (CategoryId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�������',
   'user', @CurrentUser, 'table', 'Category'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�������',
   'user', @CurrentUser, 'table', 'Category', 'column', 'FatherId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�������',
   'user', @CurrentUser, 'table', 'Category', 'column', 'CategoryName'
go

/*==============================================================*/
/* Table: History                                               */
/*==============================================================*/
create table History (
   HistoryId            int                  identity,
   BatchId              varchar(64)          null,
   Reader               varchar(16)          null,
   BookId               int                  null,
   BorrowDate           datetime             null,
   ReturnDate           datetime             null,
   ActualReturnDate     datetime             null,
   OperationType        int                  null,
   CreateDate           datetime             null default getdate(),
   constraint PK_HISTORY primary key (HistoryId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�������κ�',
   'user', @CurrentUser, 'table', 'History', 'column', 'BatchId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 'History', 'column', 'Reader'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 'History', 'column', 'BorrowDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Ԥ�ƹ黹����',
   'user', @CurrentUser, 'table', 'History', 'column', 'ReturnDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'ʵ�ʹ黹����',
   'user', @CurrentUser, 'table', 'History', 'column', 'ActualReturnDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1���ģ�2�黹��3���裬4��ʧ',
   'user', @CurrentUser, 'table', 'History', 'column', 'OperationType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 'History', 'column', 'CreateDate'
go

/*==============================================================*/
/* Table: Readers                                               */
/*==============================================================*/
create table Readers (
   ReaderId             int                  identity,
   UserId               varchar(16)          null,
   DeptId               int                  null,
   MaxNum               int                  null,
   MaxDays              int                  null,
   Status               int                  null default 1,
   constraint PK_READERS primary key (ReaderId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���ɽ�������',
   'user', @CurrentUser, 'table', 'Readers', 'column', 'MaxNum'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���ɽ�������',
   'user', @CurrentUser, 'table', 'Readers', 'column', 'MaxDays'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1���ã�2���棬3���ᣬ4����',
   'user', @CurrentUser, 'table', 'Readers', 'column', 'Status'
go

