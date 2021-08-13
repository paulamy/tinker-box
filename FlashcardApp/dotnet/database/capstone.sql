USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL,
	avatar_url varchar(100) NULL,
	is_active BIT NOT NULL,
	CONSTRAINT PK_user_id PRIMARY KEY (user_id),

);
CREATE TABLE sessions (
	session_id int IDENTITY(1,1) NOT NULL,
	session_type_id int NOT NULL,
	session_description varchar(200) NULL,
	time_created datetime DEFAULT GETDATE() NOT NULL,
	score int NULL, 
	time_completed time NULL,
	user_id int NOT NULL,
	CONSTRAINT PK_sessions PRIMARY KEY (session_id), 
);
CREATE TABLE decks (
	deck_id int IDENTITY(1,1) NOT NULL,
	name varchar(50) NOT NULL,
	description varchar(200) NULL,
	category_id int NOT NULL,
	is_public BIT NOT NULL,
	user_id int NOT NULL,
	CONSTRAINT PK_decks PRIMARY KEY (deck_id),
);
CREATE TABLE cards (
	card_id int IDENTITY(1,1) NOT NULL,
	difficulty int CHECK (difficulty > 0 AND difficulty < 6) NOT NULL,
	front varchar(500) NOT NULL,
	back varchar(500) NOT NULL,
	type_id int NOT NULL,
	CONSTRAINT PK_cards PRIMARY KEY (card_id),
);
CREATE TABLE session_types (
	session_type_id int IDENTITY(1,1) NOT NULL,
	session_type_name varchar(10) NOT NULL,
	CONSTRAINT PK_session_types PRIMARY KEY (session_type_id),
);
CREATE TABLE category_types (
	category_id int IDENTITY(1,1) NOT NULL,
	category_name varchar(20) NOT NULL,
	CONSTRAINT PK_category_types PRIMARY KEY (category_id),
);
CREATE TABLE tag_types (
	tag_id int IDENTITY(1,1) NOT NULL,
	tag_name varchar(20) NOT NULL,
	CONSTRAINT PK_tag_types PRIMARY KEY (tag_id),
);
CREATE TABLE card_types (
	type_id int IDENTITY(1,1) NOT NULL,
	card_type_name varchar(50) NOT NULL,
	CONSTRAINT PK_card_types PRIMARY KEY (type_id),
);
CREATE TABLE decks_cards (
	deck_id int NOT NULL,
	card_id int NOT NULL,
	CONSTRAINT PK_decks_cards PRIMARY KEY (deck_id, card_id),
);
CREATE TABLE sessions_cards (
	session_id int NOT NULL,
	card_id int NOT NULL,
	status BIT NOT NULL,
	CONSTRAINT PK_sessions_cards PRIMARY KEY (session_id, card_id),
);
CREATE TABLE sessions_decks (
	session_id int NOT NULL,
	deck_id int NOT NULL,
	CONSTRAINT PK_sessions_decks PRIMARY KEY (session_id, deck_id),
);
CREATE TABLE cards_tags (
	card_id int NOT NULL,
	tag_id int NOT NULL,
	CONSTRAINT PK_cards_tags PRIMARY KEY (card_id, tag_id),
);
ALTER TABLE sessions ADD CONSTRAINT FK_sessions_users FOREIGN KEY (user_id) REFERENCES users(user_id);
ALTER TABLE sessions ADD CONSTRAINT FK_sessions_session_type FOREIGN KEY (session_type_id) REFERENCES session_types(session_type_id);
ALTER TABLE decks ADD CONSTRAINT FK_decks_category FOREIGN KEY (category_id) REFERENCES category_types(category_id);
ALTER TABLE decks ADD CONSTRAINT FK_decks_users FOREIGN KEY (user_id) REFERENCES users(user_id);
ALTER TABLE cards ADD CONSTRAINT FK_cards_card_types FOREIGN KEY (type_id) REFERENCES card_types(type_id);
ALTER TABLE decks_cards ADD CONSTRAINT FK_deck_card_decks FOREIGN KEY (deck_id) REFERENCES decks(deck_id);
ALTER TABLE decks_cards ADD CONSTRAINT FK_deck_card_cards FOREIGN KEY (card_id) REFERENCES cards(card_id);
ALTER TABLE sessions_cards ADD CONSTRAINT FK_sessions_cards_sessions FOREIGN KEY (session_id) REFERENCES sessions(session_id);
ALTER TABLE sessions_cards ADD CONSTRAINT FK_sessions_cards_cards FOREIGN KEY (card_id) REFERENCES cards(card_id);
ALTER TABLE sessions_decks ADD CONSTRAINT FK_sessions_decks_sessions FOREIGN KEY (session_id) REFERENCES sessions(session_id);
ALTER TABLE sessions_decks ADD CONSTRAINT FK_sessions_decks_decks FOREIGN KEY (deck_id) REFERENCES decks(deck_id);
ALTER TABLE cards_tags ADD CONSTRAINT FK_cards_tags_cards FOREIGN KEY (card_id) REFERENCES cards(card_id);
ALTER TABLE cards_tags ADD CONSTRAINT FK_cards_tags_tags FOREIGN KEY (tag_id) REFERENCES tag_types(tag_id);

--populate default data
INSERT INTO users (username, password_hash, salt, user_role, is_active) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user', 1);
INSERT INTO users (username, password_hash, salt, user_role, is_active) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin', 1);

INSERT INTO category_types (category_name) VALUES ('General Knowledge');
INSERT INTO category_types (category_name) VALUES ('Entertainment');
INSERT INTO category_types (category_name) VALUES ('Science & Nature');
INSERT INTO category_types (category_name) VALUES ('Computers');
INSERT INTO category_types (category_name) VALUES ('Mathematics');
INSERT INTO category_types (category_name) VALUES ('Mythology');
INSERT INTO category_types (category_name) VALUES ('Sports');
INSERT INTO category_types (category_name) VALUES ('Geography');
INSERT INTO category_types (category_name) VALUES ('History');
INSERT INTO category_types (category_name) VALUES ('Politics');
INSERT INTO category_types (category_name) VALUES ('Art');
INSERT INTO category_types (category_name) VALUES ('Celebrities');
INSERT INTO category_types (category_name) VALUES ('Animals');
INSERT INTO category_types (category_name) VALUES ('Vehicles');
INSERT INTO category_types (category_name) VALUES ('Gadgets');

INSERT INTO session_types (session_type_name) VALUES ('Study');
INSERT INTO session_types (session_type_name) VALUES ('Test');

INSERT INTO card_types (card_type_name) VALUES('Inactive');
INSERT INTO card_types (card_type_name) VALUES('Question');
INSERT INTO card_types (card_type_name) VALUES('Definition');
INSERT INTO card_types (card_type_name) VALUES('Image');

--Archive Deck for inactive cards
INSERT INTO decks(name,description,category_id,is_public, user_id) VALUES('Card Archive','Cards not included in any deck',1,0,2);

--Empty tag for auto-generated cards
INSERT INTO tag_types (tag_name) VALUES('None')
GO