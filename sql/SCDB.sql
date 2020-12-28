use master;

drop database users;
create database users;
use users;

create table Account (
id int primary key identity,
AccountName varchar(200),
Create_Date datetime,
Update_Date datetime
);

create table Contact (
id int primary key identity,
ContactSurname varchar(200),
ContactName varchar(200),
legalUser int unique
	foreign key (legalUser) references Account(id),
Create_Date datetime,
Update_Date datetime 
	
);

create table Users (
id int primary key identity,
userLogin varchar(200),
userPassword varchar(200),
individual int unique
	foreign key (individual) references Contact(id),
Create_Date datetime,
Update_Date datetime 
	
);

create table BillType (
id int primary key identity,
typeBill varchar(200),
Create_Date datetime,
Update_Date datetime
);

create table Bill (
id int primary key identity,
number varchar(200),

legalUser int 
	foreign key (legalUser) references Account(id) 
	on delete cascade 
	on update cascade,
billType int
	foreign key (billType) references BillType(id) 
	on delete cascade 
	on update cascade,
	Create_Date datetime,
Update_Date datetime
);

create table AdressType (
id int primary key identity,
type varchar(200),
Create_Date datetime,
Update_Date datetime
);

create table City (
id int primary key identity,

adressType int
	foreign key (adressType) references AdressType(id) 
	on delete cascade 
	on update cascade,
	Create_Date datetime,
Update_Date datetime
);

create table Street (
id int primary key identity,
StreetName varchar(200),

city int
	foreign key (city) references City(id) 
	on delete cascade 
	on update cascade,
	Create_Date datetime,
Update_Date datetime
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
	foreign key (street) references Street(id),
	Create_Date datetime,
Update_Date datetime
);

INSERT INTO Account(AccountName) VALUES
(' first account'),
('second account'),
('third account')
SELECT * FROM Account;

INSERT INTO Contact(ContactName,ContactSurname,legalUser) VALUES
('Howman', 'Jacob', 3),
('Ford', 'Gen', 1),
('Unerdo', 'Wam', 2)
SELECT * FROM Contact;

INSERT INTO Users(userLogin,userPassword,individual) VALUES
('login1', 'password1', 1),
('login3', 'password2', 2),
('login3', 'password3', 3)
SELECT * FROM Users;
DELETE FROM Users
UPDATE Users SET userLogin = 'we22we' WHERE individual = 1

INSERT INTO BillType(typeBill) VALUES
('Active'),
('Frozen'),
('Deleted')
SELECT * FROM BillType;

INSERT INTO Bill(number,legalUser,billType) VALUES
('KHJH53J342R', 2, 1),
('DGDPKO4251J', 3, 3),
('GDJKLJKLJL1', 1, 2)
SELECT * FROM Bill;

INSERT INTO AdressType(type) VALUES
('AdressType1'),
('AdressType2'),
('AdressType3')
SELECT * FROM AdressType;

INSERT INTO City(adressType) VALUES
(2),
(1),
(2)
SELECT * FROM City;

INSERT INTO Street(StreetName,city) VALUES
('First street', 1),
('second street', 2),
('third street', 2)
SELECT * FROM Street;

INSERT INTO Adress(is_actual,adressType,city,individual,legalUser,street) VALUES
(0, 2, 1, 2, 3, 3),
(1, 2, 1, 2, 1, 1),
(1, 3, 1, 1, 3, 1)
SELECT * FROM Adress;

DELETE FROM Bill
-----------------------------------------------

SELECT * FROM Users; --вывести всех с физ\без физ

DELETE FROM Users				-- удалить всех без физ
WHERE  individual IS NULL;

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


GO
CREATE VIEW AllUsers  
AS
	SELECT Contact.ContactName +' '+ Contact.ContactSurname AS EntityName,
	'Individual' AS EntityType
	FROM Contact
UNION ALL
	SELECT Account.AccountName,
	'Legal'
	FROM Account
GO
SELECT * FROM AllUsers
DROP VIEW AllUsers


CREATE TRIGGER allusers_insert on AllUsers
INSTEAD OF INSERT
AS
BEGIN
	INSERT INTO Account(AccountName) VALUES (NULL)

	INSERT INTO Contact(ContactName,ContactSurname,legalUser) VALUES
	((SELECT EntityName FROM inserted),
	NULL,
	(SELECT TOP 1 id FROM Account ORDER BY ID DESC))

	INSERT AllUsers(EntityName,EntityType)
    SELECT EntityName,EntityType
    FROM inserted;
END
GO
DROP TRIGGER allusers_insert
INSERT INTO AllUsers VALUES('Name1','legal')


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

GO
ALTER TRIGGER dateUser ON Users 
AFTER INSERT,UPDATE
AS
BEGIN
UPDATE Users  SET Update_Date = GETDATE() WHERE id IN (SELECT id FROM deleted)
IF((SELECT Create_Date FROM Users WHERE id IN (SELECT id FROM inserted)) IS NULL)
UPDATE Users  SET Create_Date = GETDATE() WHERE id IN (SELECT id FROM inserted)
END;
GO


GO
CREATE TRIGGER dateAccount ON Account
AFTER INSERT,UPDATE
AS
BEGIN
UPDATE Account  SET Update_Date = GETDATE() WHERE id IN (SELECT id FROM deleted)
IF((SELECT Create_Date FROM Account WHERE id IN (SELECT id FROM inserted)) IS NULL)
UPDATE Account  SET Create_Date = GETDATE() WHERE id IN (SELECT id FROM inserted)
END;
GO



DROP TRIGGER dateAccount

GO
CREATE TRIGGER dateAdress ON Adress 
AFTER INSERT,UPDATE
AS
BEGIN
UPDATE Adress SET Update_Date = GETDATE() WHERE id IN (SELECT id FROM deleted)
IF((SELECT Create_Date FROM Adress WHERE id IN (SELECT id FROM inserted)) IS NULL)
UPDATE Adress  SET Create_Date = GETDATE() WHERE id IN (SELECT id FROM inserted)
END;
GO

DROP TRIGGER dateAdress


GO
CREATE TRIGGER dateAdresstype ON AdressType
AFTER INSERT,UPDATE
AS
BEGIN
UPDATE AdressType SET Update_Date = GETDATE() WHERE id IN (SELECT id FROM deleted)
IF((SELECT Create_Date FROM AdressType WHERE id IN (SELECT id FROM inserted)) IS NULL)
UPDATE AdressType  SET Create_Date = GETDATE() WHERE id IN (SELECT id FROM inserted)
END;
GO

DROP TRIGGER dateAdresstype

GO
CREATE TRIGGER dateBill ON Bill
AFTER INSERT,UPDATE
AS
BEGIN
UPDATE Bill SET Update_Date = GETDATE() WHERE id IN (SELECT id FROM deleted)
IF((SELECT Create_Date FROM Bill WHERE id IN (SELECT id FROM inserted)) IS NULL)
UPDATE Bill  SET Create_Date = GETDATE() WHERE id IN (SELECT id FROM inserted)
END;
GO

DROP TRIGGER dateBill


GO
CREATE TRIGGER dateBilltype ON BillType
AFTER INSERT,UPDATE
AS
BEGIN
UPDATE BillType SET Update_Date = GETDATE() WHERE id IN (SELECT id FROM deleted)
IF((SELECT Create_Date FROM BillType WHERE id IN (SELECT id FROM inserted)) IS NULL)
UPDATE BillType  SET Create_Date = GETDATE() WHERE id IN (SELECT id FROM inserted)
END;
GO

DROP TRIGGER dateBilltype

GO
CREATE TRIGGER dateCity ON City
AFTER INSERT,UPDATE
AS
BEGIN
UPDATE City SET Update_Date = GETDATE() WHERE id IN (SELECT id FROM deleted)
IF((SELECT Create_Date FROM City WHERE id IN (SELECT id FROM inserted)) IS NULL)
UPDATE City  SET Create_Date = GETDATE() WHERE id IN (SELECT id FROM inserted)
END;
GO

DROP TRIGGER dateCity

GO
CREATE TRIGGER dateContact ON Contact
AFTER INSERT,UPDATE
AS
BEGIN
UPDATE Contact SET Update_Date = GETDATE() WHERE id IN (SELECT id FROM deleted)
IF((SELECT Create_Date FROM Contact WHERE id IN (SELECT id FROM inserted)) IS NULL)
UPDATE Contact  SET Create_Date = GETDATE() WHERE id IN (SELECT id FROM inserted)
END;
GO

DROP TRIGGER dateContact

GO
CREATE TRIGGER dateStreet ON Street
AFTER INSERT,UPDATE
AS
BEGIN
UPDATE Street SET Update_Date = GETDATE() WHERE id IN (SELECT id FROM deleted)
IF((SELECT Create_Date FROM Street WHERE id IN (SELECT id FROM inserted)) IS NULL)
UPDATE Street  SET Create_Date = GETDATE() WHERE id IN (SELECT id FROM inserted)
END;
GO

DROP TRIGGER dateStreet


GO 
CREATE TRIGGER Adress_INSERT ON Adress 
AFTER INSERT  
AS  
 UPDATE Adress SET is_actual = 0  
WHERE adressType = (SELECT adressType FROM inserted) and individual = (SELECT individual FROM inserted); 
DROP TRIGGER Adress_INSERT

INSERT INTO Adress(is_actual,adressType,city,individual,legalUser,street) VALUES (1, 1, 1, 1, 1, 1) 
SELECT * FROM Adress;	


CREATE TRIGGER users_insert on Users
INSTEAD OF INSERT
AS
BEGIN
	INSERT INTO Account(AccountName) VALUES (NULL)
	INSERT INTO Contact(ContactName,ContactSurname,legalUser) VALUES(NULL,NULL,(SELECT TOP 1 id FROM Account ORDER BY ID DESC))
	INSERT Users(userLogin,userPassword,individual)
    SELECT userLogin,userPassword,individual
    FROM inserted;
END
GO

drop trigger users_insert
INSERT INTO Users(userLogin,userPassword,individual) VALUES
('login4', 'password4', 5)

select * from Users
select * from Account
select * from Contact



