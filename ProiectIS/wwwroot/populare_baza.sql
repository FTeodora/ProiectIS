insert into profesor(id) values(1);
insert into Question(authorID,questionSubject,enunt,timeout,correctAnswer,wrongAnswer1,wrongAnswer2,wrongAnswer3,approved) values(1,"Mate","Acesta este un enunt de test","30","raspunsul corect","raspuns gresit1","raspuns gresit2","raspuns gresit3",true);
insert into Question(authorID,questionSubject,enunt,timeout,correctAnswer,wrongAnswer1,wrongAnswer2,wrongAnswer3,approved) 
values(1,"Biology","How many legs does a spider have?","30","8","9","6","10",true),
(1,"Media","What is the name of the toy cowboy in Toy Story?","30","Woody","Willy","Steve","Buzz",true),
(1,"Geography","What is the color of an emerald? ","30","Green","Blue","Pink","Yellow",true),
(1,"Media","Whose nose grew longer every time he lied? ","30","Pinocchio","Little Red Riding Hood","Dumbo","The Grinch",true),
(1,"Media","What type of fish is Nemo? ","30","Clownfish","Bass","Cavefish","Pearlfish",true),
(1,"Biology","What do caterpillars turn into? ","30","Butterflies","Dragon-fly","Fireflies","Worms",true),
(1,"Culture","On which holiday do you go trick-or-treating?","30","Halloween","Christmas","Easter","New Years Eve",true),
(1,"Biology","What is a doe?","30","A female deer","A female ox","A female duck","A female horse",true),
(1,"Biology","What do bees make?","30","Honey","Anthills","Webs","Water",true),
(1,"Culture","What kind of cat is considered bad luck?","30","Black cats","White cats","All cats","Ginger cats",true),
(1,"Biology","Which is the fastest land animal?","30","Ceetah","Lion","Giraffe","Puma",true);
insert into Users(username,pass,nume,prenume,eMail,rol) values('profesor','profesor','domnul','profesor','prof@gmail.com','PROFESOR');
insert into savedQuiz(authorID,title) values(12, "Test de test"),(12, "Alt test");
insert into quizQuestions(quizID,questionID) values(2,2),(2,4);
