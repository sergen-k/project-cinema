import sqlite3 

create_movie = """
CREATE TABLE IF NOT EXISTS "Movie" (
  `id` INT NOT NULL,
  `title` TEXT,
  `description` TEXT,
  `director` TEXT, genre TEXT,
  `ageLimit` INT ,
  `duration` REAL,
  PRIMARY KEY (`id`)
);
"""

insert_default_movies = """
INSERT INTO Movie VALUES(0,'The Lord of the Rings: Fellowship of the ring','It''s a movie about a lord who wants rings','Peter Jackson','Fantasy',13,178.0);
INSERT INTO Movie VALUES(1,'The Lord of the Rings: The Two Towers','It''s a movie about saruman and Barad Dur and Isengard, or maybe minas tirith and minas morgul or maybe Minas Tirith and Cirith Ungol','Peter Jackson','Fantasy',13,178.0);
INSERT INTO Movie VALUES(2,'The Lord of the Rings: The return of the kind','The kind returns','Peter Jackson','Fantasy',13,178.0);
INSERT INTO Movie VALUES(3,'Inception','A thief who steals secrets through dreams is offered a chance to erase his past by planting an idea in a target''s mind.','Christopher Nolan','Sci-Fi',13,148.0);
INSERT INTO Movie VALUES(4,'The Matrix','A hacker learns that reality as he knows it is a simulation controlled by machines and joins a rebellion to fight back.','Lana Wachowski, Lilly Wachowski','Sci-Fi',16,136.0);
INSERT INTO Movie VALUES(5,'Spirited Away','A ten-year-old girl becomes trapped in a world of spirits and must find a way to save her parents and return home.','Hayao Miyazaki','Animation',6,125.0);
INSERT INTO Movie VALUES(6,'The Dark Knight','Batman faces the Joker, a criminal mastermind who plunges Gotham City into chaos and tests the limits of heroism.','Christopher Nolan','Action',13,152.0);
INSERT INTO Movie VALUES(7,'Interstellar','As Earth becomes uninhabitable, a team of astronauts travels through a wormhole in search of a new home for humanity.','Christopher Nolan','Sci-Fi',13,169.0);
INSERT INTO Movie VALUES(8,'Casino Royale','James Bond is sent to a high-stakes poker tournament in Montenegro to bankrupt a terrorist financier who funds international terrorism.','Martin Campbell','Action',13,144.0);
INSERT INTO Movie VALUES(9,'Skyfall','When MI6 is attacked and its secrets exposed, James Bond must track down a dangerous former agent while M faces a reckoning of her own.','Sam Mendes','Action',13,143.0);
INSERT INTO Movie VALUES(10,'Goldfinger','Bond uncovers a plot by the wealthy gold smuggler Auric Goldfinger to contaminate the gold reserve at Fort Knox.','Guy Hamilton','Action',13,110.0);
INSERT INTO Movie VALUES(11,'Kingsman: The Secret Service','A street-smart kid is recruited into a secret British intelligence agency and must prove himself in a training program that rivals the toughest of schools.','Matthew Vaughn','Action',16,129.0);
INSERT INTO Movie VALUES(12,'Kingsman: The Golden Circle','After their headquarters are destroyed, the surviving Kingsman agents team up with their American counterparts to stop a drug lord.','Matthew Vaughn','Action',16,141.0);
INSERT INTO Movie VALUES(13,'Mission: Impossible - Fallout','Ethan Hunt and his team race to prevent a trio of plutonium cores from falling into the hands of a terrorist group.','Christopher McQuarrie','Action',13,147.0);
INSERT INTO Movie VALUES(14,'Shaun of the Dead','A slacker in London tries to win back his girlfriend and save his friends and mother when the city is overrun by zombies.','Edgar Wright','Comedy',16,99.0);
"""
create_timetable_table = """
CREATE TABLE IF NOT EXISTS "Timetable" (
  `id` INT NOT NULL ,
  `startTime` INT,
  `movieId` INT,
  `roomNumber` INT,
  PRIMARY KEY (`id`),
  FOREIGN KEY (`movieId`)
      REFERENCES `Movie`(`id`)
);"""

create_seats_table = """
CREATE TABLE IF NOT EXISTS "Seat" (
  `id` INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
  `roomnumber` INT,
  `seattype` INT,
  `seatCost` REAL
);
"""
insert_room_1 = """
INSERT INTO Seat VALUES(1,1,1,10.0);
INSERT INTO Seat VALUES(2,1,1,10.0);
INSERT INTO Seat VALUES(3,1,1,10.0);
INSERT INTO Seat VALUES(4,1,1,10.0);
INSERT INTO Seat VALUES(5,1,1,10.0);
INSERT INTO Seat VALUES(6,1,1,10.0);
INSERT INTO Seat VALUES(7,1,1,10.0);
INSERT INTO Seat VALUES(8,1,1,10.0);
INSERT INTO Seat VALUES(9,1,1,10.0);
INSERT INTO Seat VALUES(10,1,1,10.0);
INSERT INTO Seat VALUES(11,1,1,10.0);
INSERT INTO Seat VALUES(12,1,1,10.0);
INSERT INTO Seat VALUES(13,1,1,10.0);
INSERT INTO Seat VALUES(14,1,1,10.0);
INSERT INTO Seat VALUES(15,1,1,10.0);
INSERT INTO Seat VALUES(16,1,1,10.0);
INSERT INTO Seat VALUES(17,1,1,10.0);
INSERT INTO Seat VALUES(18,1,1,10.0);
INSERT INTO Seat VALUES(19,1,1,10.0);
INSERT INTO Seat VALUES(20,1,1,10.0);
INSERT INTO Seat VALUES(21,1,1,10.0);
INSERT INTO Seat VALUES(22,1,1,10.0);
INSERT INTO Seat VALUES(23,1,1,10.0);
INSERT INTO Seat VALUES(24,1,1,10.0);
INSERT INTO Seat VALUES(25,1,1,10.0);
INSERT INTO Seat VALUES(26,1,1,10.0);
INSERT INTO Seat VALUES(27,1,1,10.0);
INSERT INTO Seat VALUES(28,1,1,10.0);
INSERT INTO Seat VALUES(29,1,1,10.0);
INSERT INTO Seat VALUES(30,1,1,10.0);
INSERT INTO Seat VALUES(31,1,1,10.0);
INSERT INTO Seat VALUES(32,1,1,10.0);
INSERT INTO Seat VALUES(33,1,1,10.0);
INSERT INTO Seat VALUES(34,1,2,15.0);
INSERT INTO Seat VALUES(35,1,2,15.0);
INSERT INTO Seat VALUES(36,1,1,10.0);
INSERT INTO Seat VALUES(37,1,1,10.0);
INSERT INTO Seat VALUES(38,1,1,10.0);
INSERT INTO Seat VALUES(39,1,1,10.0);
INSERT INTO Seat VALUES(40,1,1,10.0);
INSERT INTO Seat VALUES(41,1,1,10.0);
INSERT INTO Seat VALUES(42,1,1,10.0);
INSERT INTO Seat VALUES(43,1,1,10.0);
INSERT INTO Seat VALUES(44,1,1,10.0);
INSERT INTO Seat VALUES(45,1,2,15.0);
INSERT INTO Seat VALUES(46,1,2,15.0);
INSERT INTO Seat VALUES(47,1,2,15.0);
INSERT INTO Seat VALUES(48,1,2,15.0);
INSERT INTO Seat VALUES(49,1,1,10.0);
INSERT INTO Seat VALUES(50,1,1,10.0);
INSERT INTO Seat VALUES(51,1,1,10.0);
INSERT INTO Seat VALUES(52,1,1,10.0);
INSERT INTO Seat VALUES(53,1,1,10.0);
INSERT INTO Seat VALUES(54,1,1,10.0);
INSERT INTO Seat VALUES(55,1,1,10.0);
INSERT INTO Seat VALUES(56,1,2,15.0);
INSERT INTO Seat VALUES(57,1,2,15.0);
INSERT INTO Seat VALUES(58,1,3,20.0);
INSERT INTO Seat VALUES(59,1,3,20.0);
INSERT INTO Seat VALUES(60,1,2,15.0);
INSERT INTO Seat VALUES(61,1,2,15.0);
INSERT INTO Seat VALUES(62,1,1,10.0);
INSERT INTO Seat VALUES(63,1,1,10.0);
INSERT INTO Seat VALUES(64,1,1,10.0);
INSERT INTO Seat VALUES(65,1,1,10.0);
INSERT INTO Seat VALUES(66,1,1,10.0);
INSERT INTO Seat VALUES(67,1,1,10.0);
INSERT INTO Seat VALUES(68,1,2,15.0);
INSERT INTO Seat VALUES(69,1,2,15.0);
INSERT INTO Seat VALUES(70,1,3,20.0);
INSERT INTO Seat VALUES(71,1,3,20.0);
INSERT INTO Seat VALUES(72,1,2,15.0);
INSERT INTO Seat VALUES(73,1,2,15.0);
INSERT INTO Seat VALUES(74,1,1,10.0);
INSERT INTO Seat VALUES(75,1,1,10.0);
INSERT INTO Seat VALUES(76,1,1,10.0);
INSERT INTO Seat VALUES(77,1,1,10.0);
INSERT INTO Seat VALUES(78,1,1,10.0);
INSERT INTO Seat VALUES(79,1,1,10.0);
INSERT INTO Seat VALUES(80,1,2,15.0);
INSERT INTO Seat VALUES(81,1,2,15.0);
INSERT INTO Seat VALUES(82,1,3,20.0);
INSERT INTO Seat VALUES(83,1,3,20.0);
INSERT INTO Seat VALUES(84,1,2,15.0);
INSERT INTO Seat VALUES(85,1,2,15.0);
INSERT INTO Seat VALUES(86,1,1,10.0);
INSERT INTO Seat VALUES(87,1,1,10.0);
INSERT INTO Seat VALUES(88,1,1,10.0);
INSERT INTO Seat VALUES(89,1,1,10.0);
INSERT INTO Seat VALUES(90,1,1,10.0);
INSERT INTO Seat VALUES(91,1,1,10.0);
INSERT INTO Seat VALUES(92,1,2,15.0);
INSERT INTO Seat VALUES(93,1,2,15.0);
INSERT INTO Seat VALUES(94,1,3,20.0);
INSERT INTO Seat VALUES(95,1,3,20.0);
INSERT INTO Seat VALUES(96,1,2,15.0);
INSERT INTO Seat VALUES(97,1,2,15.0);
INSERT INTO Seat VALUES(98,1,1,10.0);
INSERT INTO Seat VALUES(99,1,1,10.0);
INSERT INTO Seat VALUES(100,1,1,10.0);
INSERT INTO Seat VALUES(101,1,1,10.0);
INSERT INTO Seat VALUES(102,1,1,10.0);
INSERT INTO Seat VALUES(103,1,1,10.0);
INSERT INTO Seat VALUES(104,1,1,10.0);
INSERT INTO Seat VALUES(105,1,2,15.0);
INSERT INTO Seat VALUES(106,1,2,15.0);
INSERT INTO Seat VALUES(107,1,2,15.0);
INSERT INTO Seat VALUES(108,1,2,15.0);
INSERT INTO Seat VALUES(109,1,1,10.0);
INSERT INTO Seat VALUES(110,1,1,10.0);
INSERT INTO Seat VALUES(111,1,1,10.0);
INSERT INTO Seat VALUES(112,1,1,10.0);
INSERT INTO Seat VALUES(113,1,1,10.0);
INSERT INTO Seat VALUES(114,1,1,10.0);
INSERT INTO Seat VALUES(115,1,1,10.0);
INSERT INTO Seat VALUES(116,1,1,10.0);
INSERT INTO Seat VALUES(117,1,1,10.0);
INSERT INTO Seat VALUES(118,1,2,15.0);
INSERT INTO Seat VALUES(119,1,2,15.0);
INSERT INTO Seat VALUES(120,1,1,10.0);
INSERT INTO Seat VALUES(121,1,1,10.0);
INSERT INTO Seat VALUES(122,1,1,10.0);
INSERT INTO Seat VALUES(123,1,1,10.0);
INSERT INTO Seat VALUES(124,1,1,10.0);
INSERT INTO Seat VALUES(125,1,1,10.0);
INSERT INTO Seat VALUES(126,1,1,10.0);
INSERT INTO Seat VALUES(127,1,1,10.0);
INSERT INTO Seat VALUES(128,1,1,10.0);
INSERT INTO Seat VALUES(129,1,1,10.0);
INSERT INTO Seat VALUES(130,1,1,10.0);
INSERT INTO Seat VALUES(131,1,1,10.0);
INSERT INTO Seat VALUES(132,1,1,10.0);
INSERT INTO Seat VALUES(133,1,1,10.0);
INSERT INTO Seat VALUES(134,1,1,10.0);
INSERT INTO Seat VALUES(135,1,1,10.0);
INSERT INTO Seat VALUES(136,1,1,10.0);
INSERT INTO Seat VALUES(137,1,1,10.0);
INSERT INTO Seat VALUES(138,1,1,10.0);
INSERT INTO Seat VALUES(139,1,1,10.0);
INSERT INTO Seat VALUES(140,1,1,10.0);
INSERT INTO Seat VALUES(141,1,1,10.0);
INSERT INTO Seat VALUES(142,1,1,10.0);
INSERT INTO Seat VALUES(143,1,1,10.0);
INSERT INTO Seat VALUES(144,1,1,10.0);
INSERT INTO Seat VALUES(145,1,1,10.0);
INSERT INTO Seat VALUES(146,1,1,10.0);
INSERT INTO Seat VALUES(147,1,1,10.0);
INSERT INTO Seat VALUES(148,1,1,10.0);
INSERT INTO Seat VALUES(149,1,1,10.0);
INSERT INTO Seat VALUES(150,1,1,10.0);
"""
create_reservation_table = """
CREATE TABLE IF NOT EXISTS "Reservation" (
  `id` INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
  `movieShowingId` INT,
  `userId` INT, People INTEGER,
  FOREIGN KEY (`movieShowingId`)
      REFERENCES `Timetable`(`id`),
  FOREIGN KEY (`userId`)
      REFERENCES `User`(`id`)
);"""


create_seat_order_table = """
CREATE TABLE IF NOT EXISTS "SeatOrder" (
  `id` INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
  `reservationId` INT,
  `seatId` INT,
  FOREIGN KEY (`reservationId`)
      REFERENCES `Reservation`(`id`),
  FOREIGN KEY (`seatId`)
      REFERENCES `Seat`(`id`)
);
"""

create_user_table = """
CREATE TABLE IF NOT EXISTS "User" (
  `id` INTEGER PRIMARY KEY AUTOINCREMENT,
  "FirstName" TEXT,
  "LastName" TEXT,
  `email` TEXT,
  `phoneNumber` INT,
  `password` TEXT,
  `dateOfBirth` INT,
  "role" TEXT
);"""

insert_admin  = """
    INSERT INTO User VALUES(1,'Yessin','Zoundri','admin@cinema.nl',612345678,'240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9',19900101,'admin');"""
create_consumable_table = """
CREATE TABLE Consumable (
    id INTEGER PRIMARY KEY,
    name TEXT NOT NULL,
    category TEXT NOT NULL,
    price REAL NOT NULL
);
"""

insert_default_consumable = """
INSERT INTO Consumable VALUES(1,'Small Popcorn','Food',4.5);
INSERT INTO Consumable VALUES(2,'Large Popcorn','Food',6.5);
INSERT INTO Consumable VALUES(3,'Nachos','Food',5.5);
INSERT INTO Consumable VALUES(4,'Cola','Drink',3.0);
INSERT INTO Consumable VALUES(5,'Water','Drink',2.5);
INSERT INTO Consumable VALUES(6,'Orange Juice','Drink',3.5);
"""

create_consumable_order_table = """
CREATE TABLE ConsumableOrder (
    id INTEGER PRIMARY KEY,
    reservationId INTEGER NOT NULL,
    consumableId INTEGER NOT NULL,
    quantity INTEGER NOT NULL,
    unitPrice REAL NOT NULL,

    FOREIGN KEY (reservationId) REFERENCES Reservation(id),
    FOREIGN KEY (consumableId) REFERENCES Consumable(id)
);
"""

import sqlite3
con = sqlite3.connect("./Project/DataSources/project.db")
con.execute(create_movie)
con.executescript(insert_default_movies)
con.execute(create_timetable_table)
con.execute(create_seats_table)
con.executescript(insert_room_1)
con.execute(create_reservation_table)
con.execute(create_seat_order_table)
con.execute(create_user_table)
con.execute(insert_admin)
con.execute(create_consumable_table)
con.executescript(insert_default_consumable)
con.execute(create_consumable_order_table)