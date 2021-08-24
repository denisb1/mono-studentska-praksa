#### Day 3 SQLite tables
```
CREATE TABLE Student(
	Id CHAR(36) PRIMARY KEY,
	FName NVARCHAR(50),
	LName NVARCHAR(50),
	College NVARCHAR(20),
	Age SMALLINT,
	CYear SMALLINT
);

CREATE TABLE Course(
	Id CHAR(36) PRIMARY KEY,
	Name NVARCHAR(20) UNIQUE
);

CREATE TABLE Enrollment(
	Id CHAR(36) PRIMARY KEY,
	StudentID CHAR(36),
	CourseID CHAR(36),
	CONSTRAINT sidfk FOREIGN KEY(StudentId) REFERENCES Student(ID),
	CONSTRAINT cidfk FOREIGN KEY(CourseId) REFERENCES Course(ID)
);
```

#### Day 4 & 5 PostgreSQL tables
```
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE Student(
	Id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
	FirstName VARCHAR(50),
	LastName VARCHAR(50),
	College VARCHAR(20),
	CollegeYear SMALLINT,
	Age SMALLINT
);

CREATE TABLE Course(
	Id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
	CourseName VARCHAR(20) UNIQUE,
	TeacherFirstName VARCHAR(50),
	TeacherLastName VARCHAR(50),
	Ects DECIMAL(2,1)
);

CREATE TABLE Enrollment(
	Id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
	StudentId UUID,
	CourseId UUID,
	CONSTRAINT sidfk FOREIGN KEY(StudentId) REFERENCES Student(Id),
	CONSTRAINT cidfk FOREIGN KEY(CourseId) REFERENCES Course(Id)
);
```

#### Day 6 Entity Framework Core

```
dotnet ef migrations add TestData --project Day6 --context TestingContext
dotnet ef database update TestData --project Day6 --context TestingContext
```
