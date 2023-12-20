CREATE TABLE "Company" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"CompanyName"	TEXT,
	"OwnerName"	TEXT,
	"Product"	TEXT,
	"Revenue"	INTEGER,
	PRIMARY KEY("ID" AUTOINCREMENT)
);