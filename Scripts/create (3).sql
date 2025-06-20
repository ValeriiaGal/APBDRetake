-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2024-05-10 08:05:50.608

-- tables
-- Table: Client
CREATE TABLE Client (
    Id int  NOT NULL IDENTITY,
    Fullname varchar(200)  NOT NULL,
    Email varchar(200)  NOT NULL,
    City varchar(100)  NULL,
    CONSTRAINT Client_ak_1 UNIQUE (Email),
    CONSTRAINT Client_pk PRIMARY KEY  (Id)
);

-- Table: Operator
CREATE TABLE Operator (
    Id int  NOT NULL IDENTITY,
    Name varchar(50)  NOT NULL,
    CONSTRAINT Operator_ak_1 UNIQUE (Name),
    CONSTRAINT Operator_pk PRIMARY KEY  (Id)
);

-- Table: PhoneNumber
CREATE TABLE PhoneNumber (
    Id int  NOT NULL IDENTITY,
    Operator_Id int  NOT NULL,
    Client_Id int  NOT NULL,
    Number varchar(20)  NOT NULL,
    CONSTRAINT PhoneNumber_ak_1 UNIQUE (Number),
    CONSTRAINT PhoneNumber_pk PRIMARY KEY  (Id)
);

-- foreign keys
-- Reference: PhoneNumber_Client (table: PhoneNumber)
ALTER TABLE PhoneNumber ADD CONSTRAINT PhoneNumber_Client
    FOREIGN KEY (Client_Id)
    REFERENCES Client (Id);

-- Reference: PhoneNumber_Operator (table: PhoneNumber)
ALTER TABLE PhoneNumber ADD CONSTRAINT PhoneNumber_Operator
    FOREIGN KEY (Operator_Id)
    REFERENCES Operator (Id);

insert into Operator (Name) values ('Orange');

insert into Client (Fullname, Email, City)
values  ('Piotr Kowalski', 'piotr.kowalski@gmail.com', 'Warsaw'),
        ('Ewa Kowalska', 'ewa.kowalska@gmail.com', 'Krakow');

insert into PhoneNumber (Operator_Id, Client_Id, Number) values (1, 1, '+48456133764');
insert into PhoneNumber (Operator_Id, Client_Id, Number) values (1, 1, '+48389742337');
insert into PhoneNumber (Operator_Id, Client_Id, Number) values (1, 2, '+48561912486');


-- End of file.

