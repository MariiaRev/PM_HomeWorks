create function select_user_notes(userId int)
returns table
(
	id uuid,
	header character varying(128),
	body character varying(1024),
	is_deleted bool,
	user_id int,
	modified_at timestamptz
)
language plpgsql
as $$
begin
	return query
		select n.*
		from notes n join users u on n.user_id =  u.id 
		where u.id = userId and n.is_deleted = false;
end;
$$;