create table users (
    id serial not null,
    firstname varchar(50),
    lastname varchar(100),
    phone varchar(15),
    email varchar(50),
    primary key(id)
);

insert into users(firstname, lastname, phone, email) 
values('Cesar', 'Villalobos Olmos', '3461005286', 'cesar-09@hotmail.com');

insert into users(firstname, lastname, phone, email) 
values('Lizhet', 'Sandoval Vallejo', '3461093552', 'lichita11@hotmail.com');

select * from users;