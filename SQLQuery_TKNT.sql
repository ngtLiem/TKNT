/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     18/10/2023 12:31:06 AM                       */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIET_TN') and o.name = 'FK_CHITIET__RELATIONS_PHONG_TR')
alter table CHITIET_TN
   drop constraint FK_CHITIET__RELATIONS_PHONG_TR
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIET_TN') and o.name = 'FK_CHITIET__RELATIONS_TIEN_NGH')
alter table CHITIET_TN
   drop constraint FK_CHITIET__RELATIONS_TIEN_NGH
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHU_NHA_TRO') and o.name = 'FK_CHU_NHA__RELATIONS_TAI_KHOA')
alter table CHU_NHA_TRO
   drop constraint FK_CHU_NHA__RELATIONS_TAI_KHOA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('NGUOI_THUE_TRO') and o.name = 'FK_NGUOI_TH_RELATIONS_TAI_KHOA')
alter table NGUOI_THUE_TRO
   drop constraint FK_NGUOI_TH_RELATIONS_TAI_KHOA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('NHA_TRO') and o.name = 'FK_NHA_TRO_RELATIONS_CHU_NHA_')
alter table NHA_TRO
   drop constraint FK_NHA_TRO_RELATIONS_CHU_NHA_
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('NHA_TRO') and o.name = 'FK_NHA_TRO_RELATIONS_DIA_CHI')
alter table NHA_TRO
   drop constraint FK_NHA_TRO_RELATIONS_DIA_CHI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PHONG_TRO') and o.name = 'FK_PHONG_TR_RELATIONS_NHA_TRO')
alter table PHONG_TRO
   drop constraint FK_PHONG_TR_RELATIONS_NHA_TRO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIET_TN')
            and   name  = 'RELATIONSHIP_10_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIET_TN.RELATIONSHIP_10_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIET_TN')
            and   name  = 'RELATIONSHIP_8_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIET_TN.RELATIONSHIP_8_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CHITIET_TN')
            and   type = 'U')
   drop table CHITIET_TN
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHU_NHA_TRO')
            and   name  = 'RELATIONSHIP_13_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHU_NHA_TRO.RELATIONSHIP_13_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CHU_NHA_TRO')
            and   type = 'U')
   drop table CHU_NHA_TRO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DIA_CHI')
            and   type = 'U')
   drop table DIA_CHI
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('NGUOI_THUE_TRO')
            and   name  = 'RELATIONSHIP_15_FK'
            and   indid > 0
            and   indid < 255)
   drop index NGUOI_THUE_TRO.RELATIONSHIP_15_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NGUOI_THUE_TRO')
            and   type = 'U')
   drop table NGUOI_THUE_TRO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('NHA_TRO')
            and   name  = 'RELATIONSHIP_6_FK'
            and   indid > 0
            and   indid < 255)
   drop index NHA_TRO.RELATIONSHIP_6_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('NHA_TRO')
            and   name  = 'RELATIONSHIP_4_FK'
            and   indid > 0
            and   indid < 255)
   drop index NHA_TRO.RELATIONSHIP_4_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NHA_TRO')
            and   type = 'U')
   drop table NHA_TRO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PHONG_TRO')
            and   name  = 'RELATIONSHIP_9_FK'
            and   indid > 0
            and   indid < 255)
   drop index PHONG_TRO.RELATIONSHIP_9_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PHONG_TRO')
            and   type = 'U')
   drop table PHONG_TRO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TAI_KHOAN')
            and   type = 'U')
   drop table TAI_KHOAN
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TIEN_NGHI')
            and   type = 'U')
   drop table TIEN_NGHI
go

/*==============================================================*/
/* Table: CHITIET_TN                                            */
/*==============================================================*/
create table CHITIET_TN (
   PT_MA                char(4)              not null,
   TN_MA                char(4)              not null
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_8_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_8_FK on CHITIET_TN (
PT_MA ASC
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_10_FK                                    */
/*==============================================================*/
create index RELATIONSHIP_10_FK on CHITIET_TN (
TN_MA ASC
)
go

/*==============================================================*/
/* Table: CHU_NHA_TRO                                           */
/*==============================================================*/
create table CHU_NHA_TRO (
   CNT_MA               char(4)              not null,
   USERNAME             varchar(30)          not null,
   CNT_HOTEN            varchar(80)          not null,
   CNT_NGAYSINH         datetime             not null,
   CNT_DIACHI           varchar(100)         not null,
   CNT_SDT              char(10)             not null,
   CNT_EMAIL            varchar(100)         not null,
   CNT_AVATAR           varchar(100)         not null,
   constraint PK_CHU_NHA_TRO primary key nonclustered (CNT_MA)
)
go



/*==============================================================*/
/* Table: DIA_CHI                                               */
/*==============================================================*/
create table DIA_CHI (
   DC_MA                char(4)              not null,
   DC_SONHA             varchar(80)          not null,
   DC_KHUVUC            varchar(80)          not null,
   DC_XAPHUONG          varchar(100)         not null,
   DC_QUANHUYEN         varchar(100)         not null,
   constraint PK_DIA_CHI primary key nonclustered (DC_MA)
)
go

/*==============================================================*/
/* Table: NGUOI_THUE_TRO                                        */
/*==============================================================*/
create table NGUOI_THUE_TRO (
   NTT_MA               char(4)              not null,
   USERNAME             varchar(30)          not null,
   NTT_HOTEN            varchar(80)          not null,
   NTT_SDT              char(10)             not null,
   NTT_NGAYSINH         datetime             not null,
   NTT_EMAIL            varchar(100)         not null,
   NTT_DIACHI           varchar(100)         not null,
   NTT_AVATAR           varchar(100)         not null,
   constraint PK_NGUOI_THUE_TRO primary key nonclustered (NTT_MA)
)
go



/*==============================================================*/
/* Table: NHA_TRO                                               */
/*==============================================================*/
create table NHA_TRO (
   NT_MA                char(4)              not null,
   DC_MA                char(4)              not null,
   CNT_MA               char(4)              not null,
   NT_TEN               varchar(80)          not null,
   NT_MOTA              text                 not null,
   constraint PK_NHA_TRO primary key nonclustered (NT_MA)
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_4_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_4_FK on NHA_TRO (
CNT_MA ASC
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_6_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_6_FK on NHA_TRO (
DC_MA ASC
)
go

/*==============================================================*/
/* Table: PHONG_TRO                                             */
/*==============================================================*/
create table PHONG_TRO (
   PT_MA                char(4)              not null,
   NT_MA                char(4)              not null,
   PT_TEN               varchar(80)          not null,
   PT_DIENTICH          varchar(10)          not null,
   PT_GIA               float                not null,
   PT_MOTA              text                 not null,
   constraint PK_PHONG_TRO primary key nonclustered (PT_MA)
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_9_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_9_FK on PHONG_TRO (
NT_MA ASC
)
go

/*==============================================================*/
/* Table: TAI_KHOAN                                             */
/*==============================================================*/
create table TAI_KHOAN (
   USERNAME             varchar(30)          not null,
   MATKHAU              varchar(50)          not null,
   ROLE                 varchar(50)          not null,
   constraint PK_TAI_KHOAN primary key nonclustered (USERNAME)
)
go

/*==============================================================*/
/* Table: TIEN_NGHI                                             */
/*==============================================================*/
create table TIEN_NGHI (
   TN_MA                char(4)              not null,
   TN_TEN               varchar(80)          not null,
   TN_MOTA              text                 not null,
   constraint PK_TIEN_NGHI primary key nonclustered (TN_MA)
)
go

alter table CHITIET_TN
   add constraint FK_CHITIET__RELATIONS_PHONG_TR foreign key (PT_MA)
      references PHONG_TRO (PT_MA)
go

alter table CHITIET_TN
   add constraint FK_CHITIET__RELATIONS_TIEN_NGH foreign key (TN_MA)
      references TIEN_NGHI (TN_MA)
go

alter table CHU_NHA_TRO
   add constraint FK_CHU_NHA__RELATIONS_TAI_KHOA foreign key (USERNAME)
      references TAI_KHOAN (USERNAME)
go

alter table NGUOI_THUE_TRO
   add constraint FK_NGUOI_TH_RELATIONS_TAI_KHOA foreign key (USERNAME)
      references TAI_KHOAN (USERNAME)
go

alter table NHA_TRO
   add constraint FK_NHA_TRO_RELATIONS_CHU_NHA_ foreign key (CNT_MA)
      references CHU_NHA_TRO (CNT_MA)
go

alter table NHA_TRO
   add constraint FK_NHA_TRO_RELATIONS_DIA_CHI foreign key (DC_MA)
      references DIA_CHI (DC_MA)
go

alter table PHONG_TRO
   add constraint FK_PHONG_TR_RELATIONS_NHA_TRO foreign key (NT_MA)
      references NHA_TRO (NT_MA)
go

