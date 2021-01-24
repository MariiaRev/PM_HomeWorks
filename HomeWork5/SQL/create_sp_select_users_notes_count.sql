create function select_users_notes_count()
returns table
(
	id integer,
	first_name character varying(128),
	last_name character varying(128),
	notes_number bigint
)
AS $$
begin
	return query
		select u.id, u.first_name, u.last_name, count(n.id)
		from users u left join notes n on u.id = n.user_id 
		group by u.id;
end;
$$
LANGUAGE plpgsql;