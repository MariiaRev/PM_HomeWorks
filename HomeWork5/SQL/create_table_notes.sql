create table notes
(id uuid PRIMARY key not null,
header CHARACTER VARYING(128) NOT NULL,
body CHARACTER VARYING(1024) NOT null,
is_deleted bool not null default false,
user_id integer not null references users(id) on delete cascade,
modified_at TIMESTAMPTZ not null default CURRENT_TIMESTAMP
);

create index index_modified_at on notes(modified_at);