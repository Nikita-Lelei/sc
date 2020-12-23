use master;

drop database users;
create database users;
use users;

create table Account (
id int primary key identity,
AccountName varchar(200)
);

create table Contact (
id int primary key identity,
ContactSurname varchar(200),
ContactName varchar(200),
legalUser int unique
	foreign key (legalUser) references Account(id) 
	on delete cascade 
	on update cascade
);

create table Users (
id int primary key identity,
userLogin varchar(200),
userPassword varchar(200),
individual int unique
	foreign key (individual) references Contact(id) 
	on delete cascade 
	on update cascade
);

create table BillType (
id int primary key identity,
typeBill varchar(200)
);

create table Bill (
id int primary key identity, --reference mistake: primary key + foreign key  
number varchar(200),
legalUser int 
	foreign key (legalUser) references Account(id) 
	on delete cascade 
	on update cascade,
billType int
	foreign key (billType) references BillType(id) 
	on delete cascade 
	on update cascade
);

create table AdressType (
id int primary key identity,
type varchar(200)
);

create table City (
id int primary key identity,
adressType int
	foreign key (adressType) references AdressType(id) 
	on delete cascade 
	on update cascade
);

create table Street (
id int primary key identity,
StreetName varchar(200),
city int
	foreign key (city) references City(id) 
	on delete cascade 
	on update cascade
);

create table Adress (
id int primary key identity,
is_actual bit,
adressType int
	foreign key (adressType) references AdressType(id),
city int
	foreign key (city) references City(id),
individual int
	foreign key (individual) references Contact(id),
legalUser int 
	foreign key (legalUser) references Account(id),
street int 
	foreign key (street) references Street(id)
);

INSERT INTO Account VALUES
(' first account'),
('second account'),
('third account')
SELECT * FROM Account;

INSERT INTO Contact VALUES
('Howman', 'Jacob', 3),
('Ford', 'Gen', 1),
('Unerdo', 'Wam', 2)
SELECT * FROM Contact;

INSERT INTO Users VALUES
('login1', 'password1', 1),
('login3', 'password2', 2),
('login3', 'password3', 3)
SELECT * FROM Users;

INSERT INTO BillType VALUES
('Active'),
('Frozen'),
('Deleted')
SELECT * FROM BillType;

INSERT INTO Bill VALUES
('KHJH53J342R', 2, 1),
('DGDPKO4251J', 3, 3),
('GDJKLJKLJL1', 1, 2)
SELECT * FROM Bill;

INSERT INTO AdressType VALUES
('AdressType1'),
('AdressType2'),
('AdressType3')
SELECT * FROM AdressType;

INSERT INTO City VALUES
(2),
(1),
(2)
SELECT * FROM City;

INSERT INTO Street VALUES
('First street', 1),
('second street', 2),
('third street', 2)
SELECT * FROM Street;

INSERT INTO Adress VALUES
(0, 2, 1, 2, 3, 3),
(1, 2, 1, 2, 1, 1),
(1, 3, 1, 1, 3, 1)
SELECT * FROM Adress;

DELETE FROM Bill
-----------------------------------------------

SELECT * FROM Users; --вывести всех с физ\без физ

DELETE FROM Users				-- удалить всех без физ
WHERE  individual = null;

SELECT Contact.ContactName, Contact.ContactSurname   --одинаковый адрес
FROM Contact JOIN Adress ON
Contact.legalUser = Adress.legalUser
WHERE Adress.city IN (SELECT Adress.city FROM Adress GROUP BY city HAVING COUNT(city) > 1) AND 
Adress.street IN (SELECT Adress.street FROM Adress GROUP BY street HAVING COUNT(street) > 1);

SELECT Contact.ContactName, Contact.ContactSurname --одинаковый адрес физ с юр
FROM Contact JOIN Account ON
Contact.legalUser = Account.id JOIN Adress ON Adress.individual = Account.id
WHERE Adress.city IN (SELECT city FROM Adress adr1 JOIN Contact ON Contact.legalUser = adr1.legalUser GROUP BY city HAVING COUNT(city) > 1) AND 
Adress.street IN (SELECT street FROM Adress adr1 JOIN Contact ON Contact.legalUser = adr1.legalUser GROUP BY street HAVING COUNT(street) > 1)


SELECT Contact.ContactName, Contact.ContactSurname  --с замороженным счетом
FROM Contact JOIN Account ON Contact.legalUser = Account.id JOIN Bill ON Account.id = Bill.id JOIN BillType ON Bill.billType = BillType.id
WHERE BillType.typeBill = 'Frozen';


DROP VIEW AllUsers
GO
CREATE VIEW AllUsers 
AS
SELECT Contact.legalUser  AS Наименование,
		Contact.ContactName AS Имя,
		Contact.ContactSurname AS Фамилия
FROM Contact RIGHT JOIN Adress ON Contact.id = Adress.individual
GO
SELECT * FROM AllUsers

GO  
CREATE FUNCTION dbo.isFrozenBill(@legal_id int)  
RETURNS varchar(3)   
AS   
BEGIN  
	Declare @Result as varchar(3)
	set @Result = 'YES';
     IF (
	 (
	SELECT Account.id
	FROM Contact JOIN Account ON 
	Contact.legalUser = Account.id JOIN Bill ON
	Account.id = Bill.id JOIN BillType ON 
	Bill.billType = BillType.id
	WHERE Account.id = @legal_id and BillType.typeBill = 'Frozen')
	IS NULL)   
        SET @Result = 'NO';  
    RETURN @Result;  
END
GO
DROP FUNCTION isFrozenBill
SELECT dbo.isFrozenBill(3) AS Result;



		