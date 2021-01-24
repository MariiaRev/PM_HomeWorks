CREATE FUNCTION insert_note(noteId uuid, header varchar(128), body varchar(1024), userId integer)
 RETURNS void
 LANGUAGE sql
AS $function$
INSERT INTO notes(id, header, body, user_id) VALUES (noteId, header, body, userId);
$function$
;
