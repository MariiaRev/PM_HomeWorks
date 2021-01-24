CREATE FUNCTION insert_user(firstname varchar(128), lastname varchar(128))
 RETURNS void
 LANGUAGE plpgsql 
AS $$
begin
INSERT INTO users(first_name, last_name) VALUES (firstname, lastname);
end;
$$
;
