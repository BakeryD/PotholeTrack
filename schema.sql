
-- Switch to the system (aka master) database
USE master;
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
	id				int			identity(1,1),
	username		varchar(50)	not null,
	password		varchar(50)	not null,
	first_name		varchar(100) NOT NULL,
	last_name		varchar(100) NOT NULL,
	salt			varchar(50)	not null,
	phone_number	int NOT NULL,
	role			varchar(50)	default('user'),

	constraint pk_users primary key (id)
);


CREATE TABLE records 
(
	id				int NOT NULL Identity(1,1),
	submitter		int NOT NULL,
	date_posted		datetime NOT NULL,
	date_inspected	datetime NOT NULL,
	severity		int NOT NULL,
	repair_date		datetime NOT NULL,
	location		varchar(19) NOT NULL, -- We need to store lat. and long coordinates which are 8 digits each for most locations in Ohio + a comma
	status			int NOT NULL,
	report_count	int NOT NULL,
	description		TEXT NOT NULL,

	Constraint	pk_records	PRIMARY KEY (id),
	Constraint	fk_records_users	FOREIGN KEY (submitter) REFERENCES users(id)
);

CREATE TABLE claims 
(
	id				int NOT NULL Identity(1,1),
	submitter		int NOT NULL,
	pothole_record	int NOT NULL,
	date_submitted	int NOT NULL,

	Constraint	pk_claims PRIMARY KEY (id),
	Constraint	fk_claims_users		FOREIGN KEY (submitter) REFERENCES users(id),
	Constraint	fk_claims_records	FOREIGN KEY (pothole_record) REFERENCES records(id)
);


COMMIT TRANSACTION;