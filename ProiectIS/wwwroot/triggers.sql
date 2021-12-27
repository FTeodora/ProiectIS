delimiter $$
CREATE TRIGGER insertStudent
    AFTER INSERT
    ON users FOR EACH ROW
    begin
    if(new.rol='STUDENT') then
        insert into student(id,score,wonMatches) values (new.id,0,0);
	end if;
    if(new.rol='PROFESOR') then
    insert into profesor(id) values (new.id);
    end if;
end$$
