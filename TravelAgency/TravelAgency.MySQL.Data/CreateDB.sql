CREATE DATABASE travels
	CHARACTER SET utf8
	COLLATE utf8_hungarian_ci;

use travels;

create table travelModes (
  id int not null auto_increment primary key,
  name varchar(100) not null unique
);

create table hotels (
  id int not null auto_increment primary key,
  name varchar(200) not null unique,
  stars int not null,
  address varchar(200) not null,
  webPageUrl varchar(200) not null,
  description longtext,
  constraint CK_stars check (stars >= 0 and stars <= 7)
);

create table offers (
  id int not null auto_increment primary key,
  destination varchar(100) not null,
  travelModeId int not null,
  startDate datetime not null,
  endDate datetime not null,
  hotelId int not null,
  price numeric(12, 2) not null,
  description longtext,
  maxParticipants int not null,
  photo longtext,
  constraint fk_offers_travelModeId foreign key (travelModeId)
    references travelModes (id),
  constraint fk_offers_hotelId foreign key (hotelId)
    references hotels (id)
);


