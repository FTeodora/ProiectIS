create database kohaat;
use kohaat;
create table users(id bigint auto_increment primary key, 
					username varchar(31) unique, 
                    pass varchar(31),
                    nume varchar(31),
                    prenume varchar(63),
                    eMail varchar(63),
                    rol enum('ADMIN','MODERATOR', 'PROFESOR', 'STUDENT')
                    );
ALTER TABLE users ADD UNIQUE (username); 
create table student(id bigint primary key,
				score bigint,
                wonMatches bigint,
                FOREIGN KEY (id) REFERENCES Users(id));
create table profesor(id bigint primary key,
FOREIGN KEY (id) REFERENCES Users(id));
create table grup(id bigint auto_increment primary key,
				nume varchar(45),
                descriere varchar(255));
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
                    timeout tinyint,
                    correctAnswer varchar(100),
                    wrongAnswer1 varchar(100),
                    wrongAnswer2 varchar(100),
                    wrongAnswer3 varchar(100),
                    approved bool, -- determina daca intrebarea a fost aprobata sau nu de un moderator(adica daca poate fi pusa in circulatie)
                    FOREIGN KEY (authorID) REFERENCES profesor(id)
                    
);

create table savedQuiz(id bigint auto_increment primary key,
authorID bigint,
title varchar(50),
FOREIGN KEY (authorID) REFERENCES profesor(id));
create table quizQuestions(quizID bigint,questionID bigint,
FOREIGN KEY (quizID) REFERENCES savedQuiz(id),
FOREIGN KEY (questionID) REFERENCES question(id));

