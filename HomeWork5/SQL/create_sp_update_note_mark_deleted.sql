create function update_note_mark_deleted(noteId uuid) returns bool
as $$
begin
	update notes
	set is_deleted = true 
	where id = noteId;
return  found;
end;
$$
language plpgsql;
