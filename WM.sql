create database WM
go
use WM
go

create table NguoiDung
(
	ID int primary key identity(1,1),
	Ten varchar(200),
	Email varchar(200),
	SDT bigint,
	UserName varchar(200),
	Pass varchar(200),
	Access nvarchar(max)    
);
create table Nhom
(
	Code varchar(200) primary key,
	TenNhom varchar(200)
);
create table ThamGia
(
	Id int identity(1,1) primary key,
	Nhom_code varchar(200),
	U_id int,
	foreign key (Nhom_code) references Nhom (Code),
	foreign key (U_id) references NguoiDung(ID),
	VaiTro nvarchar(max)			
);
create table CongViec
(
	ID int primary key identity(1,1),
	MoTa varchar(200),
	NgayBatDau datetime,
	NgayKetThuc datetime,
	TienDo varchar(200),
	Nhom_code varchar(200),
	U_id int,
	LoaiCongViec nvarchar(max),				
	FeedBack nvarchar(max),
	foreign key (Nhom_code) references Nhom (Code),
	foreign key (U_id) references NguoiDung(ID)
);

set identity_insert [NguoiDung] ON;

insert into NguoiDung (ID, Ten, Email, SDT, UserName, Pass, Access)
values (1, N'Admin', NULL, NULL, N'admin', N'admin', admin);

select * from NguoiDung;

alter table NguoiDung
add constraint unique_Ten unique (Ten);

alter table NguoiDung
add constraint unique_SDT unique (SDT);

alter table NguoiDung
add constraint unique_UserName unique (UserName);

select n.TenNhom, n.Code, nd.ID, nd.Ten as Leader
from  [WM].[dbo].[NguoiDung] as nd 
inner join [WM].[dbo].[ThamGia] as tg on nd.ID = tg.U_id 
inner join [WM].[dbo].[Nhom] as n     on n.Code = tg.Nhom_code
where tg.VaiTro = 'leader' 
