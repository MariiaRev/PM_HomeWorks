--insert some users
select insert_user('Mariia', 'Revenko');
select insert_user('Santos', 'Otto');
select insert_user('Emma', 'Watson');


--insert some notes
select insert_note('9ad9db00-5e41-11eb-ae93-0242ac130002', 'my first note', 'the body of my first note consists of the  long long text', 1);
select insert_note('ea77f81b-f354-4596-8df9-44944192dbd0', 'my second note', 'here is something about how to cook a delecious salad for example', 1);
select insert_note('859d8dd9-62b9-4a6f-b6c0-dd2f3964384d', 'Todo list', '1. Study trends\n2.Make a cooking vlog\n3.Edit video', 2);
select insert_note('2b9e18f3-38b5-4898-a2a4-71a8cacb6bc0', 'Stuff to buy', 'Kitchen table\njeans\napples', 2);
select insert_note('202a2644-146a-47c0-94ba-e18f510105ca', 'need to learn fast', '1. unit testing\n2. postgresql, procedures/functions?', 1);


--select some notes
select * from select_note('ea77f81b-f354-4596-8df9-44944192dbd0');			--select existing note
select * from select_note('ea77f81b-f354-4596-8df9-44944192dbd1');			--select not existing note

--delete some notes
select update_note_mark_deleted('ea77f81b-f354-4596-8df9-44944192dbd0');	--mark deleted existing note of one user
select update_note_mark_deleted('2b9e18f3-38b5-4898-a2a4-71a8cacb6bc0');	--mark deleted existing note of another user
select update_note_mark_deleted('ea77f81b-f354-4596-8df9-44944192dbd1');	--mark deleted not existing note 

--select users notes count
select * from select_users_notes_count();

--select user notes
select * from select_user_notes(1);											--select notes of existing user with not deleted notes
select * from select_user_notes(2);											--select notes of existing user with not deleted notes
select * from select_user_notes(3);											--select notes of existing user without not deleted notes
select * from select_user_notes(17);										--select notes of not existing user