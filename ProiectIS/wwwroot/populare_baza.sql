
insert into Users(username,pass,nume,prenume,eMail,rol) values('admin','admin','adminul','adminurilor','admin@gmail.com','ADMIN'),
('moderator','moderator','domnul','moderator','mod@gmail.com','MODERATOR');

-- ('profesor','profesor','domnul','profesor','prof@gmail.com','PROFESOR'),
-- ('student','student','un','student','stud@gmail.com','STUDENT'),
-- ('profesor2','profesor2','celalalt','profesor','profesor@yahoo.com','PROFESOR'),
-- ('profesor3','profesor3','un nou domn','profesor','domn.profesor@yahoo.com','PROFESOR'),
-- ('student2','student2','alt','student','stud@yahoo.com','STUDENT'),
-- ('student3','student3','celalalt','student','student@gmail.com','STUDENT'),
-- ('student4','student4','un nou','student','student4@yahoo.com','STUDENT'),
-- ('student5','student5','inca un','student','student5@gmail.com','STUDENT'),
-- ('student6','student6','noul','student','stud.ent6@gmail.com','STUDENT'),
-- ('student7','student7','un student','in plus','stud.ent7@gmail.com','STUDENT')
insert into Question(authorID,questionSubject,enunt,timeout,correctAnswer,wrongAnswer1,wrongAnswer2,wrongAnswer3,approved) values
(3,"Proba","Acesta este un enunt de test","12","raspunsul corect","raspuns gresit1","raspuns gresit2","raspuns gresit3",true),
(3,"Biology","How many legs does a spider have?","2","8","9","6","10",true),
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
insert into quizQuestions(quizID,questionID) values(2,2),(2,4), (1,1),(1,4);
insert into grup(nume,descriere) values('Grup de proba','Un grup de test cu destui membri'),
										('Grup de proba2','Un alt grup de test cu destui membri'),
                                        ('Grup de proba3','Un grup de test care nu are destui membri');
insert into grupMember(grupID,studentID,groupScore) values(1,4,1000),(1,7,467),(1,8,784), (2,9,165),(2,10,1000),(2,11,666) ,(3,12,0);
insert into grupLeader(grupID,profesorID) values(1,3),(2,5),(3,6),(1,5);
