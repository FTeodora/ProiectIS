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