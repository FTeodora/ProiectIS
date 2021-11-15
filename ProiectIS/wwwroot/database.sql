create database kohaat;
use kohaat;

create table users(id bigint auto_increment primary key, username varchar(45), pass varchar(45));
insert into users(username,pass) values ('sal','cf');

select * from users where username='poate' and pass='merge';