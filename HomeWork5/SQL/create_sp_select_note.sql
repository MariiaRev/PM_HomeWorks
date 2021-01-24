create function select_note(noteId uuid)
returns table
(
	note_id uuid,
	note_header character varying(128),
	note_body character varying(1024),
	note_is_deleted bool,
	note_modified_at timestamptz,
	note_user_id integer,
	user_first_name character varying(128),
	user_last_name character varying(128)
)
LANGUAGE plpgsql
AS $$
begin
	return query
		select n.id, n.header, n.body, n.is_deleted, n.modified_at, n.user_id, u.first_name, u.last_name
		from notes n join users u on n.user_id = u.id
		where n.id = noteId;
end;
$$;