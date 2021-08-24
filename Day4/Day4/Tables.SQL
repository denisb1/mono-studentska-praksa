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
