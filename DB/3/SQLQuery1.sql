use master;

create database G_MyBase
on primary
(
    name = N'G_MyBase_mdf',
    filename = N'd:\sql\data\G_MyBase_mdf.mdf',
    size = 10240kb,
    maxsize = unlimited,
    filegrowth = 1024kb
),
(
    name = N'G_MyBase_ndf',
    filename = N'd:\sql\data\G_MyBase_ndf.ndf',
    size = 10240kb,
    maxsize = 1gb,
    filegrowth = 25%
),
filegroup fg1
(
    name = N'G_MyBase_fg1_1',
    filename = N'd:\sql\data\G_MyBase_fg1_1.ndf',
    size = 10240kb,
    maxsize = 1gb,
    filegrowth = 25%
),
(
    name = N'G_MyBase_fg1_2',
    filename = N'd:\sql\data\G_MyBase_fg1_2.ndf',
    size = 10240kb,
    maxsize = 1gb,
    filegrowth = 25%
)
log on
(
    name = N'G_MyBase_log',
    filename = N'd:\sql\logs\G_MyBase_log.ldf',
    size = 10240kb,
    maxsize = 2048gb,
    filegrowth = 10%
);
go
