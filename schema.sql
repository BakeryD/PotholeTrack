
USE master;-- Switch to the system (aka master) database

GO

-- Delete the potholeDB Database (IF EXISTS)
IF EXISTS(select * from sys.databases where name='potholeDB')
DROP DATABASE potholeDB;
GO

-- Create a new potholeDB Database
CREATE DATABASE potholeDB;
GO

-- Switch to the potholeDB Database
USE potholeDB
GO

BEGIN TRANSACTION;

CREATE TABLE users
(
    id              int         identity(1,1),
    username        varchar(50) NOT NULL,
    password        varchar(50) NOT NULL,
    firstname      varchar(100) NOT NULL,
    lastname       varchar(100) NOT NULL,
    salt            varchar(50) NOT NULL,
    phonenumber    int NOT NULL,
    role            varchar(50) default('user'),

    constraint pk_users primary key (id)
);


CREATE TABLE records 
(
    id              int NOT NULL Identity(1,1),
    submitter       int NOT NULL,
    datecreated    datetime NOT NULL,
    dateinspected  datetime,
    severity        int NOT NULL default(2),
    repairdate		datetime,
	lattitude		numeric(8,6) not null,
	longitude		numeric(8,6) not null,
	status          int NOT NULL default(1),
    reportcount    int NOT NULL default(1),
    description     TEXT NOT NULL,

    Constraint  pk_records  PRIMARY KEY (id),
    Constraint  fk_records_users    FOREIGN KEY (submitter) REFERENCES users(id)
);

CREATE TABLE claims 
(
    id              int NOT NULL Identity(1,1),
    submitter       int NOT NULL,
    potholerecord  int NOT NULL,
    datesubmitted  datetime NOT NULL,
    status          bit NOT NULL default(1), -- True = open claim, false = closed claim 
    description     TEXT NOT NULL,
    

    Constraint  pk_claims PRIMARY KEY (id),
    Constraint  fk_claims_users     FOREIGN KEY (submitter) REFERENCES users(id),
    Constraint  fk_claims_records   FOREIGN KEY (potholerecord) REFERENCES records(id)
);


COMMIT TRANSACTION;