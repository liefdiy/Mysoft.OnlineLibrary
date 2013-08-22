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
   '已藏书最多10W本计算，限定每人最多借阅10本书，BookList长度等于5*10+1*10,5是BookId最大长度，1是分隔符',
   'user', @CurrentUser, 'table', 'BatchLog', 'column', 'BooksCount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '本次借阅图书id列表，用逗号分隔',
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
   ISBN                 varchar(13）          null,
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
   '类别id',
   'user', @CurrentUser, 'table', 'Books', 'column', 'CategoryId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '图书编号，用于图书管理，需要唯一性约束',
   'user', @CurrentUser, 'table', 'Books', 'column', 'BookSerialNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '图书名称',
   'user', @CurrentUser, 'table', 'Books', 'column', 'BookName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '作者，多个用逗号分隔',
   'user', @CurrentUser, 'table', 'Books', 'column', 'Author'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '译著者',
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
   '出版社',
   'user', @CurrentUser, 'table', 'Books', 'column', 'Press'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '出版日期',
   'user', @CurrentUser, 'table', 'Books', 'column', 'PublishDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '单价',
   'user', @CurrentUser, 'table', 'Books', 'column', 'Price'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '评分，0~5分',
   'user', @CurrentUser, 'table', 'Books', 'column', 'Grade'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '图书封面url',
   'user', @CurrentUser, 'table', 'Books', 'column', 'PicUrl'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '简介',
   'user', @CurrentUser, 'table', 'Books', 'column', 'Brief'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '图书状态，1在馆，2外借，3遗失，4计划购买',
   'user', @CurrentUser, 'table', 'Books', 'column', 'Status'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
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
   '所有类别',
   'user', @CurrentUser, 'table', 'Category'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '父级类别',
   'user', @CurrentUser, 'table', 'Category', 'column', 'FatherId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '类别名称',
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
   '借阅批次号',
   'user', @CurrentUser, 'table', 'History', 'column', 'BatchId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '读者名称',
   'user', @CurrentUser, 'table', 'History', 'column', 'Reader'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '借阅日期',
   'user', @CurrentUser, 'table', 'History', 'column', 'BorrowDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '预计归还日期',
   'user', @CurrentUser, 'table', 'History', 'column', 'ReturnDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '实际归还日期',
   'user', @CurrentUser, 'table', 'History', 'column', 'ActualReturnDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1借阅，2归还，3续借，4挂失',
   'user', @CurrentUser, 'table', 'History', 'column', 'OperationType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建日期',
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
   '最多可借阅数量',
   'user', @CurrentUser, 'table', 'Readers', 'column', 'MaxNum'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最多可借阅天数',
   'user', @CurrentUser, 'table', 'Readers', 'column', 'MaxDays'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1可用，2警告，3冻结，4拉黑',
   'user', @CurrentUser, 'table', 'Readers', 'column', 'Status'
go

