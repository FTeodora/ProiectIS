create database kohaat;
use kohaat;

create table users(id bigint auto_increment primary key, username varchar(45), pass varchar(45));
insert into users(username,pass) values ('sal','cf');

select * from users where username='poate' and pass='merge';
alter table users add column nume varchar(31),
add column prenume varchar(63),
add column eMail varchar(63),
add column rol enum('ADMIN','MODERATOR', 'PROFESOR', 'STUDENT');
select * from users;

create table users(id bigint auto_increment primary key, 
					username varchar(31), 
                    pass varchar(31),
                    nume varchar(31),
                    prenume varchar(63),
                    eMail varchar(63),
                    rol enum('ADMIN','MODERATOR', 'PROFESOR', 'STUDENT')
                    );
create table student(id bigint auto_increment primary key,
				score bigint,
                wonMatches bigint,
                FOREIGN KEY (id) REFERENCES Users(id));
create table profesor(id bigint auto_increment primary key,
FOREIGN KEY (id) REFERENCES Users(id));
create table grup(id bigint auto_increment primary key);
create table grupMember(studentID bigint, 
				grupID bigint,
				FOREIGN KEY (studentID) REFERENCES student(id),
				FOREIGN KEY (grupID) REFERENCES grup(id),
				groupScore bigint);
create table grupLeader(profesorID bigint, 
				grupID bigint,
				FOREIGN KEY (profesorID) REFERENCES profesor(id),
				FOREIGN KEY (grupID) REFERENCES grup(id));
create table scheduledMatch(challengerID bigint,
						challengedID bigint,
						FOREIGN KEY (challengedID) REFERENCES grup(id),
						FOREIGN KEY (challengerID) REFERENCES grup(id));
create table Question(id bigint auto_increment primary key,
					authorID bigint,
                    questionSubject varchar(45),
                    enunt varchar(255),
                    timeout time,
                    correctAnswer varchar(100),
                    wrongAnswer1 varchar(100),
                    wrongAnswer2 varchar(100),
                    wrongAnswer3 varchar(100),
                    approved bool, -- determina daca intrebarea a fost aprobata sau nu de un moderator(adica daca poate fi pusa in circulatie)
                    FOREIGN KEY (authorID) REFERENCES profesor(id)
                    
);
