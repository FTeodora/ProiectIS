

insert into Users(username,pass,nume,prenume,eMail,rol) values('admin','admin','adminul','adminurilor','admin@gmail.com','ADMIN'),
('moderator','moderator','domnul','moderator','mod@gmail.com','MODERATOR'),
('profesor','profesor','domnul','profesor','prof@gmail.com','PROFESOR'),
('student','student','un','student','stud@gmail.com','STUDENT');
insert into Question(authorID,questionSubject,enunt,timeout,correctAnswer,wrongAnswer1,wrongAnswer2,wrongAnswer3,approved) values
(3,"Proba","Acesta este un enunt de test","12","raspunsul corect","raspuns gresit1","raspuns gresit2","raspuns gresit3",true);
insert into Question(authorID,questionSubject,enunt,timeout,correctAnswer,wrongAnswer1,wrongAnswer2,wrongAnswer3,approved) 
values(3,"Biology","How many legs does a spider have?","2","8","9","6","10",true),
(3,"Media","What is the name of the toy cowboy in Toy Story?","12","Woody","Willy","Steve","Buzz",true),
(3,"Geography","What is the color of an emerald? ","12","Green","Blue","Pink","Yellow",true),
(3,"Media","Whose nose grew longer every time he lied? ","12","Pinocchio","Little Red Riding Hood","Dumbo","The Grinch",true),
(3,"Media","What type of fish is Nemo? ","12","Clownfish","Bass","Cavefish","Pearlfish",true),
(3,"Biology","What do caterpillars turn into? ","12","Butterflies","Dragon-fly","Fireflies","Worms",true),
(3,"Culture","On which holiday do you go trick-or-treating?","12","Halloween","Christmas","Easter","New Years Eve",true),
(3,"Biology","What is a doe?","12","A female deer","A female ox","A female duck","A female horse",true),
(3,"Biology","What do bees make?","12","Honey","Anthills","Webs","An annoying sound",true),
(3,"Culture","What kind of cat is considered bad luck?","12","Black cats","White cats","All cats","Ginger cats",true),
(3,"Biology","Which is the fastest land animal?","12","Ceetah","Lion","Giraffe","Puma",true);
insert into savedQuiz(authorID,title) values(3, "Test de test"),(3, "Alt test");
insert into quizQuestions(quizID,questionID) values(2,2),(2,4);
insert into quizQuestions(quizID,questionID) values(1,1),(1,4);
insert into grup(nume,descriere) values('a a','a a');
insert into notification(recipientID,senderID,message,accepted,declined) values(4, 5, "Ai fost provocat la un meci",false,false);