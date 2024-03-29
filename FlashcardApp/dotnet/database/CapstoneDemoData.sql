USE [master]
GO
--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END
/****** Object:  Database [final_capstone]    Script Date: 8/12/2021 1:30:24 PM ******/
CREATE DATABASE [final_capstone]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'final_capstone', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\final_capstone.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'final_capstone_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\final_capstone_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [final_capstone] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [final_capstone].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [final_capstone] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [final_capstone] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [final_capstone] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [final_capstone] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [final_capstone] SET ARITHABORT OFF 
GO
ALTER DATABASE [final_capstone] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [final_capstone] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [final_capstone] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [final_capstone] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [final_capstone] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [final_capstone] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [final_capstone] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [final_capstone] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [final_capstone] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [final_capstone] SET  ENABLE_BROKER 
GO
ALTER DATABASE [final_capstone] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [final_capstone] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [final_capstone] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [final_capstone] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [final_capstone] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [final_capstone] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [final_capstone] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [final_capstone] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [final_capstone] SET  MULTI_USER 
GO
ALTER DATABASE [final_capstone] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [final_capstone] SET DB_CHAINING OFF 
GO
ALTER DATABASE [final_capstone] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [final_capstone] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [final_capstone] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [final_capstone] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [final_capstone] SET QUERY_STORE = OFF
GO
USE [final_capstone]
GO
/****** Object:  Table [dbo].[card_types]    Script Date: 8/12/2021 1:30:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[card_types](
	[type_id] [int] IDENTITY(1,1) NOT NULL,
	[card_type_name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_card_types] PRIMARY KEY CLUSTERED 
(
	[type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cards]    Script Date: 8/12/2021 1:30:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cards](
	[card_id] [int] IDENTITY(1,1) NOT NULL,
	[difficulty] [int] NOT NULL,
	[front] [varchar](500) NOT NULL,
	[back] [varchar](500) NOT NULL,
	[type_id] [int] NOT NULL,
 CONSTRAINT [PK_cards] PRIMARY KEY CLUSTERED 
(
	[card_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cards_tags]    Script Date: 8/12/2021 1:30:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cards_tags](
	[card_id] [int] NOT NULL,
	[tag_id] [int] NOT NULL,
 CONSTRAINT [PK_cards_tags] PRIMARY KEY CLUSTERED 
(
	[card_id] ASC,
	[tag_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category_types]    Script Date: 8/12/2021 1:30:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category_types](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[category_name] [varchar](20) NOT NULL,
 CONSTRAINT [PK_category_types] PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[decks]    Script Date: 8/12/2021 1:30:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[decks](
	[deck_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[description] [varchar](200) NULL,
	[category_id] [int] NOT NULL,
	[is_public] [bit] NOT NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_decks] PRIMARY KEY CLUSTERED 
(
	[deck_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[decks_cards]    Script Date: 8/12/2021 1:30:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[decks_cards](
	[deck_id] [int] NOT NULL,
	[card_id] [int] NOT NULL,
 CONSTRAINT [PK_decks_cards] PRIMARY KEY CLUSTERED 
(
	[deck_id] ASC,
	[card_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[session_types]    Script Date: 8/12/2021 1:30:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[session_types](
	[session_type_id] [int] IDENTITY(1,1) NOT NULL,
	[session_type_name] [varchar](10) NOT NULL,
 CONSTRAINT [PK_session_types] PRIMARY KEY CLUSTERED 
(
	[session_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sessions]    Script Date: 8/12/2021 1:30:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sessions](
	[session_id] [int] IDENTITY(1,1) NOT NULL,
	[session_type_id] [int] NOT NULL,
	[session_description] [varchar](200) NULL,
	[time_created] [datetime] NOT NULL,
	[score] [int] NULL,
	[time_completed] [time](7) NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_sessions] PRIMARY KEY CLUSTERED 
(
	[session_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sessions_cards]    Script Date: 8/12/2021 1:30:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sessions_cards](
	[session_id] [int] NOT NULL,
	[card_id] [int] NOT NULL,
	[status] [bit] NOT NULL,
 CONSTRAINT [PK_sessions_cards] PRIMARY KEY CLUSTERED 
(
	[session_id] ASC,
	[card_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sessions_decks]    Script Date: 8/12/2021 1:30:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sessions_decks](
	[session_id] [int] NOT NULL,
	[deck_id] [int] NOT NULL,
 CONSTRAINT [PK_sessions_decks] PRIMARY KEY CLUSTERED 
(
	[session_id] ASC,
	[deck_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tag_types]    Script Date: 8/12/2021 1:30:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tag_types](
	[tag_id] [int] IDENTITY(1,1) NOT NULL,
	[tag_name] [varchar](20) NOT NULL,
 CONSTRAINT [PK_tag_types] PRIMARY KEY CLUSTERED 
(
	[tag_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 8/12/2021 1:30:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password_hash] [varchar](200) NOT NULL,
	[salt] [varchar](200) NOT NULL,
	[user_role] [varchar](50) NOT NULL,
	[avatar_url] [varchar](100) NULL,
	[is_active] [bit] NOT NULL,
 CONSTRAINT [PK_user_id] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[card_types] ON 

INSERT [dbo].[card_types] ([type_id], [card_type_name]) VALUES (1, N'Inactive')
INSERT [dbo].[card_types] ([type_id], [card_type_name]) VALUES (2, N'Question')
INSERT [dbo].[card_types] ([type_id], [card_type_name]) VALUES (3, N'Definition')
INSERT [dbo].[card_types] ([type_id], [card_type_name]) VALUES (4, N'Image')
SET IDENTITY_INSERT [dbo].[card_types] OFF
GO
SET IDENTITY_INSERT [dbo].[cards] ON 

INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (1, 1, N'In past times, what would a gentleman keep in his fob pocket?', N'Watch', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (2, 1, N'What is the largest organ of the human body?', N'Skin', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (3, 1, N'French is an official language in Canada.', N'True', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (4, 1, N'What does the "S" stand for in the abbreviation SIM, as in SIM card? ', N'Subscriber', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (5, 1, N'What alcoholic drink is made from molasses?', N'Rum', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (6, 1, N'Which American president appears on a one dollar bill?', N'George Washington', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (7, 1, N'What geometric shape is generally used for stop signs?', N'Octagon', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (8, 1, N'Red Vines is a brand of what type of candy?', N'Licorice', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (9, 1, N'What is the nickname of the US state of California?', N'Golden State', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (10, 1, N'The color orange is named after the fruit.', N'True', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (11, 1, N'You can legally drink alcohol while driving in Mississippi.', N'True', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (12, 1, N'Pluto is a planet.', N'False', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (13, 1, N'Adolf Hitler was born in Australia. ', N'False', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (14, 1, N'What do the letters of the fast food chain KFC stand for?', N'Kentucky Fried Chicken', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (15, 1, N'What machine element is located in the center of fidget spinners?', N'Bearings', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (16, 1, N'What zodiac sign is represented by a pair of scales?', N'Libra', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (17, 1, N'According to the nursery rhyme, what fruit did Little Jack Horner pull out of his Christmas pie?', N'Plum', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (18, 1, N'The Canadian $1 coin is colloquially known as a what?', N'Loonie', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (19, 1, N'Which country has the union jack in its flag?', N'New Zealand', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (20, 1, N'The mitochondria is the powerhouse of the cell.', N'True', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (21, 1, N'League of Legends, DOTA 2, Smite and Heroes of the Storm are all part of which game genre?', N'Multiplayer Online Battle Arena (MOBA)', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (22, 1, N'What is the name of Team Fortress 2''s Heavy Weapons Guy''s minigun?', N'Sasha', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (23, 1, N'The Mann Co. Store from Team Fortress 2 has the slogan "We hire mercenaries and get in fights"', N'False', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (24, 1, N'In the fighting game "Skullgirls" which character utilizes a folding chair called the "Hurting" as a weapon?', N'Beowulf', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (25, 1, N'In "Super Mario 3D World", the Double Cherry power-up originated from a developer accidentally making two characters controllable.', N'True', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (26, 1, N'The 2005 video game "Call of Duty 2: Big Red One" is not available on PC.', N'True', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (27, 1, N'Who is the leader of Team Instinct in Pokemon Go?', N'Spark', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (28, 1, N'In what year was the first "Mass Effect" game released?', N'2007', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (29, 1, N'Who turns out to be the true victor in the Battle of Armageddon in Mortal Kombat?', N'Shao Kahn', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (30, 1, N'What is the name of the 4-armed Chaos Witch from the 2016 video game "Battleborn"?', N'Orendi', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (31, 1, N'What minimum level in the Defence skill is needed to equip Dragon Armour in the MMO RuneScape?', N'60', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (32, 1, N'When was the top-down online RPG "Space Station 13" released?', N'2003', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (33, 1, N'Who created the indie adventure game "Night in the Woods"?', N'Alec Holowka', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (34, 1, N'In "Overwatch", which hero is able to wallride?', N'Lucio', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (35, 1, N'What are Sans and Papyrus named after in "Undertale"?', N'Fonts', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (36, 1, N'In Counter-Strike: Global Offensive, what''s the rarity of discontinued skins called?', N'Contraband', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (37, 1, N'Rincewind from the 1995 Discworld game was voiced by which member of Monty Python?', N'Eric Idle', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (38, 1, N'Which Nintendo 64 game did NOT have Luigi in it?', N'Super Mario 64', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (39, 1, N'Doki Doki Literature Club was developed in Japan.', N'False', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (40, 1, N'In the game "Fire Emblem: Shadow Dragon", what is the central protagonist''s name?', N'Marth', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (41, 3, N'In the hexadecimal system, what number comes after 9?', N'The Letter A', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (42, 3, N'How many zeros are there in a googol?', N'100', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (43, 3, N'The Pythagorean theorem states that the square of the hypotenuse is equal to the product of the squares of the other two sides.', N'False', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (44, 3, N'What are the first 6 digits of the number "Pi"?', N'3.14159', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (45, 3, N'What is the Roman numeral for 500?', N'D', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (46, 3, N'What is the first Mersenne prime exponent over 1000?', N'1279', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (47, 3, N'The proof for the Chinese Remainder Theorem used in Number Theory was NOT developed by its first publisher, Sun Tzu.', N'True', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (48, 3, N'111,111,111 x 111,111,111 = 12,345,678,987,654,321', N'True', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (49, 3, N'What is the alphanumeric representation of the imaginary number?', N'i', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (50, 3, N'To the nearest whole number, how many radians are in a whole circle?', N'6', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (51, 3, N'What Greek letter is used to signify summation?', N'Sigma', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (52, 3, N'E = MC', N'False', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (53, 3, N'Zero factorial is equal to zero. ', N'False', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (54, 3, N'A "Millinillion" is a real number.', N'True', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (55, 3, N'The set of all algebraic numbers is countable.', N'True', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (56, 3, N'You can square root a negative number with an imaginary number "i".', N'True', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (57, 3, N'Which greek mathematician ran through the streets of Syracuse naked while shouting "Eureka" after discovering the principle of displacement?', N'Archimedes', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (58, 3, N'How many books are in Euclid''s Elements of Geometry?', N'13', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (59, 3, N'Which of the following dice is not a platonic solid?', N'10-sided die', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (60, 3, N'Which mathematician refused the Fields Medal?', N'Grigori Perelman', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (61, 5, N'How many times did Martina Navratilova win the Wimbledon Singles Championship?', N'Nine', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (62, 5, N'What tool lends it''s name to a last-stone advantage in an end in Curling?', N'Hammer', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (63, 5, N'With which doubles partner did John McEnroe have most success?', N'Peter Fleming', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (64, 5, N'Where was the Games of the XXII Olympiad held?', N'Moscow', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (65, 5, N'Which car company is the only Japanese company which won the 24 Hours of Le Mans?', N'Mazda', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (66, 5, N'Which Italian footballer told Neuer where he''s putting his shot and dragging it wide, during the match Italy-Germany, UEFA EURO 2016?', N'Pelle', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (67, 5, N'Which male player won the gold medal of table tennis singles in 2016 Olympics Games?', N'Ma Long (China)', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (68, 5, N'Which female player won the gold medal of table tennis singles in 2016 Olympics Games?', N'DING Ning (China)', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (69, 5, N'What is the full name of the footballer "Cristiano Ronaldo"?', N'Cristiano Ronaldo dos Santos Aveiro', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (70, 5, N'Which year was the third Super Bowl held?', N'1969', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (71, 5, N'What team did England beat in the semi-final stage to win in the 1966 World Cup final?', N'Portugal', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (72, 5, N'Which city features all of their professional sports teams'' jersey''s with the same color scheme?', N'Pittsburgh', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (73, 5, N'Which player "kung-fu kicked" a Crystal Palace fan in January 1995?', N'Eric Cantona', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (74, 5, N'Who is Manchester United''s leading appearance maker?', N'Ryan Giggs', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (75, 5, N'The Mazda 787B won the 24 Hours of Le Mans in what year?', N'1991', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (76, 5, N'What is "The Sport of Kings"?', N'Horse Racing', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (77, 5, N'Which of these Russian cities did NOT contain a stadium that was used in the 2018 FIFA World Cup?', N'Vladivostok', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (78, 5, N'In Canadian football, scoring a rouge is worth how many points?', N'1', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (79, 5, N'The AHL affiliate team of the Boston Bruins is named what?', N'Providence Bruins', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (80, 5, N'Which English football team is nicknamed "The Tigers"?', N'Hull City', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (81, 3, N'What is the capital of Peru?', N'Lima', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (82, 3, N'How many countries are inside the United Kingdom?', N'Four', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (83, 3, N'The flag of South Africa features 7 colours.', N'False', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (84, 3, N'What is the largest non-continental island in the world?', N'Greenland', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (85, 3, N'The land of Gotland is located in which European country?', N'Sweden', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (86, 3, N'On which continent is the country of Angola located?', N'Africa', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (87, 3, N'Which of these countries is NOT located in Africa?', N'Suriname', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (88, 3, N'How many countries are larger than Australia?', N'5', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (89, 3, N'The capital of the US State Ohio is the city of Chillicothe.', N'False', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (90, 3, N'Antarctica is the largest desert in the world.', N'True', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (91, 3, N'Which country has the abbreviation "CH"?', N'Switzerland', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (92, 3, N'Liechtenstein does not have an airport.', N'True', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (93, 3, N'What is the capital of Belarus?', N'Minsk', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (94, 3, N'What continent is the country Lesotho in?', N'Africa', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (95, 3, N'How many rivers are in Saudi Arabia?', N'0', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (96, 3, N'Frankenmuth, a US city nicknamed "Little Bavaria", is located in what state?', N'Michigan', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (97, 3, N'The Pyrenees mountains are located on the border of which two countries?', N'France and Spain', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (98, 3, N'Which of these countries is not a United Nations member state?', N'Niue', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (99, 3, N'What was the African nation of Zimbabwe formerly known as?', N'Rhodesia', 2)
GO
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (100, 3, N'The formerly East-Prussian city of Konigsberg is known as which Russian City today?', N'Kaliningrad', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (101, 5, N'Japan was part of the Allied Powers during World War I.', N'True', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (102, 5, N'The Kingdom of Prussia briefly held land in Estonia.', N'False', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (103, 5, N'When did Canada leave the confederation to become their own nation?', N'July 1st, 1867', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (104, 5, N'When was the SS or Schutzstaffel established?', N'April 4th, 1925', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (105, 5, N'During the Wars of the Roses (1455 - 1487) which Englishman was dubbed "the Kingmaker"?', N'Richard Neville', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (106, 5, N'When was Adolf Hitler appointed as Chancellor of Germany?', N'January 30, 1933', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (107, 5, N'During the Roman Triumvirate of 42 BCE, what region of the Roman Republic was given to Lepidus?', N'Hispania ', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (108, 5, N'The Second Boer War in 1899 was fought where?', N'South Africa', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (109, 5, N'What was the real name of the Albanian national leader Skanderbeg?', N'Gjergj Kastrioti', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (110, 5, N'The Hagia Sophia was commissioned by which emperor of the Byzantine Empire?', N'Justinian I', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (111, 5, N'The Battle of Trafalgar took place on October 23rd, 1805', N'False', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (112, 5, N'When was the city of Rome, Italy founded?', N'753 BCE', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (113, 5, N'How many women joined the United States Armed Services during World War II?', N'350,000', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (114, 5, N'The USS Missouri (BB-63) last served in the Korean War.', N'False', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (115, 5, N'What was the name of the German offensive operation in October 1941 to take Moscow before winter?', N'Operation Typhoon', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (116, 5, N'The main objective of the German operation "Case Blue" during World War II was originally to capture what?', N'Caucasus', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (117, 5, N'What did the abbreviation "RMS"; stand for in the RMS Titanic in 1912?', N'Royal Mail Ship', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (118, 5, N'The ancient city of Chichen Itza was built by which civilization?', N'Mayans', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (119, 5, N'The son of which pope supposedly held a lecherous fete involving 50 courtesans in the papal palace?', N'Alexander VI', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (120, 5, N'Which is the hull NO. of the Fletcher class destroyer Fletcher?', N'DD-445', 2)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (121, 5, N'https://images.metmuseum.org/CRDImages/ao/web-large/DP-576-001.jpg', N'Artist: Metropolitan Painter | Title: Vessel, Mythological Scene', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (122, 5, N'https://images.metmuseum.org/CRDImages/ao/web-large/1979.206.534.jpg', N'Artist:  | Title: Nose Ornament', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (123, 5, N'https://images.metmuseum.org/CRDImages/ao/web-large/1979.206.484a_1a.jpg', N'Artist:  | Title: Ear Spool', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (124, 5, N'https://images.metmuseum.org/CRDImages/ao/web-large/vs64_228_701.jpg', N'Artist:  | Title: Tupu (pin)', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (125, 5, N'https://images.metmuseum.org/CRDImages/ao/web-large/DP-13440-020.jpg', N'Artist:  | Title: Tupu (pin)', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (126, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DP109484.jpg', N'Artist: Fra Carnevale (Bartolomeo di Giovanni Corradini) | Title: The Birth of the Virgin', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (127, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DT5.jpg', N'Artist: Botticelli (Alessandro di Mariano Filipepi) | Title: The Last Communion of Saint Jerome', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (128, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DP-17230-001.jpg', N'Artist: Gerard David | Title: The Crucifixion', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (129, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DT1025.jpg', N'Artist: Paul Gauguin | Title: Ia Orana Maria (Hail Mary)', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (130, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DP-1410-001.jpg', N'Artist: Gerard David | Title: Virgin and Child with Four Angels', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (131, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DT28_DT29.jpg', N'Artist: Hans Memling | Title: Tommaso di Folco Portinari (1428–1501); Maria Portinari (Maria Maddalena Baroncelli, born 1456)', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (132, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DP247630.jpg', N'Artist: Théodore Rousseau | Title: The Forest in Winter at Sunset', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (133, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DP-15888-001.jpg', N'Artist: Théodore Rousseau | Title: A River in a Meadow', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (134, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DT9354.jpg', N'Artist: Sebastiano Ricci | Title: The Baptism of Christ', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (135, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DP-16333-001.jpg', N'Artist: Bartolomé Estebán Murillo | Title: Don Andrés de Andrade y la Cal', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (136, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DP345354.jpg', N'Artist: Aelbert Bouts | Title: Head of Saint John the Baptist on a Charger', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (137, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DP-15089-001.jpg', N'Artist: Raffaellino del Garbo (also known as Raffaelle de'' Capponi and Raffaelle de'' Carli) | Title: Holy Family with an Angel', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (138, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DT1476.jpg', N'Artist: Gerard David | Title: The Nativity with Donors and Saints Jerome and Leonard', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (139, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DP-19586-001.jpg', N'Artist: Hans Memling | Title: Salvator Mundi', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (140, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DT1461.jpg', N'Artist: Quinten Massys | Title: Portrait of a Woman', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (141, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DP218061.jpg', N'Artist: Gerard David | Title: Christ Blessing', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (142, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DP-18259-001.jpg', N'Artist: Théodore Chassériau | Title: Desdemona (The Song of the Willow)', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (143, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DP-16339-001.jpg', N'Artist: Justus of Ghent (Joos van Wassenhove) | Title: The Adoration of the Magi', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (144, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DT44.jpg', N'Artist: Rosa Bonheur | Title: The Horse Fair', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (145, 5, N'https://images.metmuseum.org/CRDImages/ep/web-large/DP320128.jpg', N'Artist: Paul Cézanne | Title: Madame Cézanne (Hortense Fiquet, 1850–1922) in a Red Dress', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (146, 5, N'https://images.metmuseum.org/CRDImages/as/web-large/DP153705.jpg', N'Artist: Han Gan | Title: Night-Shining White', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (147, 5, N'https://images.metmuseum.org/CRDImages/as/web-large/2015_300_10_Burke_website.jpg', N'Artist:  | Title: Ten Verses on Oxherding', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (148, 5, N'https://images.metmuseum.org/CRDImages/as/web-large/DP298241.jpg', N'Artist: Sesson Yubai ???? | Title: Poem on the Theme of a Monk’s Life', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (149, 5, N'https://images.metmuseum.org/CRDImages/as/web-large/2015_300_251a_Burke_website.jpg', N'Artist:  | Title: Flying Apsaras (Hiten)', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (150, 5, N'https://images.metmuseum.org/CRDImages/as/web-large/DP-14487-006_crd.jpg', N'Artist: Gai Qi | Title: Famous Women', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (151, 5, N'https://images.metmuseum.org/CRDImages/as/web-large/DP-15820-005_crd.jpg', N'Artist: Li Jie | Title: Fisherman''s Lodge At Mount Xisai', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (152, 5, N'https://images.metmuseum.org/CRDImages/as/web-large/DP167812_CRD.jpg', N'Artist: Guo Xi | Title: Old Trees, Level Distance', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (153, 5, N'https://images.metmuseum.org/CRDImages/as/web-large/CT_35767.jpg', N'Artist: Bada Shanren (Zhu Da) | Title: Letters to Fang Shiguan', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (154, 5, N'https://images.metmuseum.org/CRDImages/as/web-large/2015_300_253ab_Burke_website.jpg', N'Artist:  | Title: Tobatsu Bishamonten', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (155, 5, N'https://images.metmuseum.org/CRDImages/as/web-large/DP-15583-002.jpg', N'Artist:  | Title: The Deity Vajrabhairava, Tantric Form of the Bodhisattva Manjushri', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (156, 5, N'https://images.metmuseum.org/CRDImages/as/web-large/DP361158.jpg', N'Artist: Kano Naizen | Title: Zheng Huangniu and Yushanzhu', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (157, 5, N'https://images.metmuseum.org/CRDImages/as/web-large/DP151528.jpg', N'Artist: Li Gonglin | Title: The Classic of Filial Piety', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (158, 5, N'https://images.metmuseum.org/CRDImages/as/web-large/DP213003_CRD.jpg', N'Artist: Unidentified artist | Title: Eighteen Songs of a Nomad Flute: The Story of Lady Wenji', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (159, 5, N'https://images.metmuseum.org/CRDImages/as/web-large/1985_121_A_sf.jpg', N'Artist: Chen Hongshou | Title: Landscapes, Figures, and Flowers', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (160, 5, N'https://images.metmuseum.org/CRDImages/as/web-large/DP230507_CRD.jpg', N'Artist: Xianyu Shu | Title: Song of the Stone Drums', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (161, 5, N'https://images.metmuseum.org/CRDImages/as/web-large/DP-12232-187.jpg', N'Artist: Ike Taiga | Title: Evening Glow in a Mountain Village and Calligraphy', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (162, 5, N'https://images.metmuseum.org/CRDImages/as/web-large/DP357987.jpg', N'Artist: Ike Taiga | Title: Pine Tree and Calligraphy', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (163, 5, N'https://images.metmuseum.org/CRDImages/as/web-large/DP146538_CRD.jpg', N'Artist: Zhao Mengjian | Title: Poems on Painting Plum Blossoms and Bamboo', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (164, 5, N'https://images.metmuseum.org/CRDImages/is/web-large/DP240367.jpg', N'Artist:  | Title: Damascus Room', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (165, 5, N'https://images.metmuseum.org/CRDImages/is/web-large/DP234083.jpg', N'Artist: Habiballah of Sava | Title: "The Concourse of the Birds", Folio 11r from a Mantiq al-tair (Language of the Birds)', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (166, 5, N'https://images.metmuseum.org/CRDImages/is/web-large/DP123181.jpg', N'Artist:  | Title: Pierced Bowl Signed by Hasan al-Qashani', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (167, 5, N'https://images.metmuseum.org/CRDImages/is/web-large/DP235240.jpg', N'Artist:  | Title: Standing Figure with Jeweled Headdress', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (168, 5, N'https://images.metmuseum.org/CRDImages/is/web-large/DP231246.jpg', N'Artist:  | Title: Bowl', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (169, 5, N'https://images.metmuseum.org/CRDImages/is/web-large/DP235239.jpg', N'Artist:  | Title: Standing Figure with Feathered Headdress', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (170, 5, N'https://images.metmuseum.org/CRDImages/is/web-large/DP262928.jpg', N'Artist:  | Title: Head of a Central Asian Figure in a Pointed Cap', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (171, 5, N'https://images.metmuseum.org/CRDImages/is/web-large/DP170373.jpg', N'Artist:  | Title: Head from a Figure with a Beaded Headdress', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (172, 5, N'https://images.metmuseum.org/CRDImages/is/web-large/DP108574.jpg', N'Artist: Abu''l Qasim Firdausi | Title: "Isfandiyar''s Third Course: He Slays a Dragon", Folio from a Shahnama (Book of Kings)', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (173, 5, N'https://images.metmuseum.org/CRDImages/is/web-large/DP235838.jpg', N'Artist: Sultan Muhammad Nur | Title: Anthology of Persian Poetry in Oblong Format (Safina)', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (174, 5, N'https://images.metmuseum.org/CRDImages/is/web-large/DP231356.jpg', N'Artist: Nanha | Title: "Page of Calligraphy Illuminated with Animals and Plants in a Field of Flowers", Folio from the Shah Jahan Album', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (175, 5, N'https://images.metmuseum.org/CRDImages/is/web-large/DP230043.jpg', N'Artist:  | Title: Textile Fragment with Ogival Pattern', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (176, 5, N'https://images.metmuseum.org/CRDImages/is/web-large/DP367585.jpg', N'Artist:  | Title: Mold Fragment with Musicians', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (177, 5, N'https://images.metmuseum.org/CRDImages/is/web-large/DP353091.jpg', N'Artist:  | Title: Head of a Central Asian Figure', 4)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (178, 3, N'rhinoceros', N'a large, heavily built plant-eating mammal with one or two horns on the nose and thick folded skin, native to Africa and southern Asia. All kinds have become endangered through hunting.', 3)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (179, 3, N'elephant', N'a very large plant-eating mammal with a prehensile trunk, long curved ivory tusks, and large ears, native to Africa and southern Asia. It is the largest living land animal.', 3)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (180, 3, N'whale', N'a very large marine mammal with a streamlined hairless body, a horizontal tail fin, and a blowhole on top of the head for breathing.', 3)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (181, 3, N'gorilla', N'a powerfully built great ape with a large head and short neck, found in the forests of central Africa. It is the largest living primate.', 3)
INSERT [dbo].[cards] ([card_id], [difficulty], [front], [back], [type_id]) VALUES (182, 3, N'giraffe', N'a large African mammal with a very long neck and forelegs, having a coat patterned with brown patches separated by lighter lines. It is the tallest living animal.', 3)
SET IDENTITY_INSERT [dbo].[cards] OFF
GO
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (1, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (2, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (3, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (4, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (5, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (6, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (7, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (8, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (9, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (10, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (11, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (12, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (13, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (14, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (15, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (16, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (17, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (18, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (19, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (20, 2)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (21, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (22, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (23, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (24, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (25, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (26, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (27, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (28, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (29, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (30, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (31, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (32, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (33, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (34, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (35, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (36, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (37, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (38, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (39, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (40, 4)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (41, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (42, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (43, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (44, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (45, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (46, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (47, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (48, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (49, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (50, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (51, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (52, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (53, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (54, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (55, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (56, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (57, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (58, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (59, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (60, 3)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (61, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (62, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (63, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (64, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (65, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (66, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (67, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (68, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (69, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (70, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (71, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (72, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (73, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (74, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (75, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (76, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (77, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (78, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (79, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (80, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (81, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (82, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (83, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (84, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (85, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (86, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (87, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (88, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (89, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (90, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (91, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (92, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (93, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (94, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (95, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (96, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (97, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (98, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (99, 5)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (100, 5)
GO
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (101, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (102, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (103, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (104, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (105, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (106, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (107, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (108, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (109, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (110, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (111, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (112, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (113, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (114, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (115, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (116, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (117, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (118, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (119, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (120, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (121, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (121, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (122, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (122, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (123, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (123, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (124, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (124, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (125, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (125, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (126, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (126, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (127, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (127, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (128, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (128, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (129, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (129, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (130, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (130, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (131, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (131, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (132, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (132, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (133, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (133, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (134, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (134, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (135, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (135, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (136, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (136, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (137, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (137, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (138, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (138, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (139, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (139, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (140, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (140, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (141, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (141, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (142, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (142, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (143, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (143, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (144, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (144, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (145, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (145, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (146, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (146, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (147, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (147, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (148, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (148, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (149, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (149, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (150, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (150, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (151, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (151, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (152, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (152, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (153, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (153, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (154, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (154, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (155, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (155, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (156, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (156, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (157, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (157, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (158, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (158, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (159, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (159, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (160, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (160, 8)
GO
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (161, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (161, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (162, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (162, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (163, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (163, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (164, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (164, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (165, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (165, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (166, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (166, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (167, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (167, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (168, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (168, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (169, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (169, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (170, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (170, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (171, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (171, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (172, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (172, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (173, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (173, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (174, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (174, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (175, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (175, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (176, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (176, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (177, 7)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (177, 8)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (178, 9)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (179, 9)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (180, 9)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (181, 9)
INSERT [dbo].[cards_tags] ([card_id], [tag_id]) VALUES (182, 9)
GO
SET IDENTITY_INSERT [dbo].[category_types] ON 

INSERT [dbo].[category_types] ([category_id], [category_name]) VALUES (1, N'General Knowledge')
INSERT [dbo].[category_types] ([category_id], [category_name]) VALUES (2, N'Entertainment')
INSERT [dbo].[category_types] ([category_id], [category_name]) VALUES (3, N'Science & Nature')
INSERT [dbo].[category_types] ([category_id], [category_name]) VALUES (4, N'Computers')
INSERT [dbo].[category_types] ([category_id], [category_name]) VALUES (5, N'Mathematics')
INSERT [dbo].[category_types] ([category_id], [category_name]) VALUES (6, N'Mythology')
INSERT [dbo].[category_types] ([category_id], [category_name]) VALUES (7, N'Sports')
INSERT [dbo].[category_types] ([category_id], [category_name]) VALUES (8, N'Geography')
INSERT [dbo].[category_types] ([category_id], [category_name]) VALUES (9, N'History')
INSERT [dbo].[category_types] ([category_id], [category_name]) VALUES (10, N'Politics')
INSERT [dbo].[category_types] ([category_id], [category_name]) VALUES (11, N'Art')
INSERT [dbo].[category_types] ([category_id], [category_name]) VALUES (12, N'Celebrities')
INSERT [dbo].[category_types] ([category_id], [category_name]) VALUES (13, N'Animals')
INSERT [dbo].[category_types] ([category_id], [category_name]) VALUES (14, N'Vehicles')
INSERT [dbo].[category_types] ([category_id], [category_name]) VALUES (15, N'Gadgets')
SET IDENTITY_INSERT [dbo].[category_types] OFF
GO
SET IDENTITY_INSERT [dbo].[decks] ON 

INSERT [dbo].[decks] ([deck_id], [name], [description], [category_id], [is_public], [user_id]) VALUES (1, N'Card Archive', N'Cards not included in any deck', 1, 0, 2)
INSERT [dbo].[decks] ([deck_id], [name], [description], [category_id], [is_public], [user_id]) VALUES (2, N'General Knowledge 101', N'easy general knowledge trivia questions', 1, 1, 2)
INSERT [dbo].[decks] ([deck_id], [name], [description], [category_id], [is_public], [user_id]) VALUES (3, N'Video Games 101', N'easy video game trivia questions', 2, 1, 6)
INSERT [dbo].[decks] ([deck_id], [name], [description], [category_id], [is_public], [user_id]) VALUES (4, N'Math 301', N'medium math trivia questions', 5, 0, 4)
INSERT [dbo].[decks] ([deck_id], [name], [description], [category_id], [is_public], [user_id]) VALUES (5, N'Sports 501', N'hard sports trivia questions', 7, 1, 2)
INSERT [dbo].[decks] ([deck_id], [name], [description], [category_id], [is_public], [user_id]) VALUES (6, N'Geography 301', N'medium geography trivia questions', 8, 1, 2)
INSERT [dbo].[decks] ([deck_id], [name], [description], [category_id], [is_public], [user_id]) VALUES (7, N'History 501', N'hard history trivia questions', 9, 1, 4)
INSERT [dbo].[decks] ([deck_id], [name], [description], [category_id], [is_public], [user_id]) VALUES (8, N'Arts of Africa, Oceania and the Americas', N'a collection of art objects from Africa, Oceania and the Americas', 11, 1, 2)
INSERT [dbo].[decks] ([deck_id], [name], [description], [category_id], [is_public], [user_id]) VALUES (9, N'European Paintings', N'a collection of paintings from Europe', 11, 1, 5)
INSERT [dbo].[decks] ([deck_id], [name], [description], [category_id], [is_public], [user_id]) VALUES (10, N'Asian Art', N'a collection of art objects Asia', 11, 1, 2)
INSERT [dbo].[decks] ([deck_id], [name], [description], [category_id], [is_public], [user_id]) VALUES (11, N'Islamic Art', N'a collection of art objects of Islamic heritage', 11, 1, 2)
INSERT [dbo].[decks] ([deck_id], [name], [description], [category_id], [is_public], [user_id]) VALUES (13, N'Animals', N'a collection of animal names', 13, 1, 2)
SET IDENTITY_INSERT [dbo].[decks] OFF
GO
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 1)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 2)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 3)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 4)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 5)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 6)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 7)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 8)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 9)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 10)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 11)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 12)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 13)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 14)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 15)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 16)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 17)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 18)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 19)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (2, 20)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 21)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 22)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 23)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 24)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 25)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 26)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 27)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 28)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 29)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 30)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 31)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 32)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 33)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 34)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 35)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 36)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 37)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 38)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 39)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (3, 40)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 41)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 42)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 43)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 44)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 45)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 46)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 47)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 48)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 49)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 50)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 51)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 52)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 53)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 54)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 55)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 56)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 57)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 58)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 59)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (4, 60)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 61)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 62)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 63)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 64)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 65)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 66)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 67)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 68)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 69)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 70)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 71)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 72)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 73)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 74)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 75)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 76)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 77)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 78)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 79)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (5, 80)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 81)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 82)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 83)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 84)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 85)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 86)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 87)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 88)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 89)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 90)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 91)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 92)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 93)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 94)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 95)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 96)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 97)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 98)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 99)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (6, 100)
GO
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 101)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 102)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 103)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 104)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 105)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 106)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 107)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 108)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 109)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 110)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 111)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 112)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 113)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 114)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 115)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 116)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 117)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 118)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 119)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (7, 120)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (8, 121)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (8, 122)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (8, 123)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (8, 124)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (8, 125)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 126)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 127)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 128)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 129)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 130)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 131)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 132)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 133)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 134)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 135)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 136)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 137)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 138)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 139)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 140)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 141)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 142)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 143)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 144)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (9, 145)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (10, 146)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (10, 147)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (10, 148)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (10, 149)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (10, 150)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (10, 151)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (10, 152)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (10, 153)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (10, 154)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (10, 155)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (10, 156)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (10, 157)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (10, 158)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (10, 159)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (10, 160)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (10, 161)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (10, 162)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (10, 163)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (11, 164)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (11, 165)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (11, 166)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (11, 167)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (11, 168)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (11, 169)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (11, 170)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (11, 171)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (11, 172)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (11, 173)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (11, 174)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (11, 175)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (11, 176)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (11, 177)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (13, 178)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (13, 179)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (13, 180)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (13, 181)
INSERT [dbo].[decks_cards] ([deck_id], [card_id]) VALUES (13, 182)
GO
SET IDENTITY_INSERT [dbo].[session_types] ON 

INSERT [dbo].[session_types] ([session_type_id], [session_type_name]) VALUES (1, N'Study')
INSERT [dbo].[session_types] ([session_type_id], [session_type_name]) VALUES (2, N'Test')
SET IDENTITY_INSERT [dbo].[session_types] OFF
GO
SET IDENTITY_INSERT [dbo].[sessions] ON 

INSERT [dbo].[sessions] ([session_id], [session_type_id], [session_description], [time_created], [score], [time_completed], [user_id]) VALUES (1, 1, N'paul math practice', CAST(N'2021-08-12T10:27:39.277' AS DateTime), 20, CAST(N'10:42:39.2766667' AS Time), 4)
INSERT [dbo].[sessions] ([session_id], [session_type_id], [session_description], [time_created], [score], [time_completed], [user_id]) VALUES (2, 2, N'paul math test', CAST(N'2021-08-12T10:32:59.643' AS DateTime), 15, CAST(N'10:47:59.6433333' AS Time), 4)
INSERT [dbo].[sessions] ([session_id], [session_type_id], [session_description], [time_created], [score], [time_completed], [user_id]) VALUES (3, 1, N'ryan art practice', CAST(N'2021-08-12T10:32:59.647' AS DateTime), 25, CAST(N'10:44:59.6466667' AS Time), 5)
INSERT [dbo].[sessions] ([session_id], [session_type_id], [session_description], [time_created], [score], [time_completed], [user_id]) VALUES (4, 2, N'ryan art test', CAST(N'2021-08-12T10:32:59.650' AS DateTime), 30, CAST(N'10:42:59.6500000' AS Time), 5)
INSERT [dbo].[sessions] ([session_id], [session_type_id], [session_description], [time_created], [score], [time_completed], [user_id]) VALUES (5, 1, N'julian video games practice', CAST(N'2021-08-12T10:32:59.650' AS DateTime), 12, CAST(N'10:45:59.6500000' AS Time), 6)
INSERT [dbo].[sessions] ([session_id], [session_type_id], [session_description], [time_created], [score], [time_completed], [user_id]) VALUES (6, 2, N'julian video games test', CAST(N'2021-08-12T10:32:59.650' AS DateTime), 20, CAST(N'10:47:59.6500000' AS Time), 6)
INSERT [dbo].[sessions] ([session_id], [session_type_id], [session_description], [time_created], [score], [time_completed], [user_id]) VALUES (7, 1, N'jon animals practice', CAST(N'2021-08-12T10:32:59.650' AS DateTime), 5, CAST(N'10:37:59.6500000' AS Time), 7)
INSERT [dbo].[sessions] ([session_id], [session_type_id], [session_description], [time_created], [score], [time_completed], [user_id]) VALUES (8, 2, N'jon animals test', CAST(N'2021-08-12T10:32:59.650' AS DateTime), 5, CAST(N'10:36:59.6500000' AS Time), 7)
SET IDENTITY_INSERT [dbo].[sessions] OFF
GO
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 41, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 42, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 43, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 44, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 45, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 46, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 47, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 48, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 49, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 50, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 51, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 52, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 53, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 54, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 55, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 56, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 57, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 58, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 59, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (1, 60, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 41, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 42, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 43, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 44, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 45, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 46, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 47, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 48, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 49, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 50, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 51, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 52, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 53, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 54, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 55, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 56, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 57, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 58, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 59, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (2, 60, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 126, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 127, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 128, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 129, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 130, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 131, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 132, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 133, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 134, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 135, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 136, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 137, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 138, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 139, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 140, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 141, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 142, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 143, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 144, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 145, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 146, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 147, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 148, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 149, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 150, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 151, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 152, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 153, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 154, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 155, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 156, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 157, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 158, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 159, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 160, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 161, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 162, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (3, 163, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 126, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 127, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 128, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 129, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 130, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 131, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 132, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 133, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 134, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 135, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 136, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 137, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 138, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 139, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 140, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 141, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 142, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 143, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 144, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 145, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 146, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 147, 1)
GO
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 148, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 149, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 150, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 151, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 152, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 153, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 154, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 155, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 156, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 157, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 158, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 159, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 160, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 161, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 162, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (4, 163, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 21, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 22, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 23, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 24, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 25, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 26, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 27, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 28, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 29, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 30, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 31, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 32, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 33, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 34, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 35, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 36, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 37, 0)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 38, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 39, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (5, 40, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 21, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 22, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 23, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 24, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 25, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 26, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 27, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 28, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 29, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 30, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 31, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 32, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 33, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 34, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 35, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 36, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 37, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 38, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 39, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (6, 40, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (7, 178, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (7, 179, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (7, 180, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (7, 181, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (7, 182, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (8, 178, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (8, 179, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (8, 180, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (8, 181, 1)
INSERT [dbo].[sessions_cards] ([session_id], [card_id], [status]) VALUES (8, 182, 1)
GO
INSERT [dbo].[sessions_decks] ([session_id], [deck_id]) VALUES (1, 4)
INSERT [dbo].[sessions_decks] ([session_id], [deck_id]) VALUES (2, 4)
INSERT [dbo].[sessions_decks] ([session_id], [deck_id]) VALUES (3, 9)
INSERT [dbo].[sessions_decks] ([session_id], [deck_id]) VALUES (3, 10)
INSERT [dbo].[sessions_decks] ([session_id], [deck_id]) VALUES (4, 9)
INSERT [dbo].[sessions_decks] ([session_id], [deck_id]) VALUES (4, 10)
INSERT [dbo].[sessions_decks] ([session_id], [deck_id]) VALUES (5, 3)
INSERT [dbo].[sessions_decks] ([session_id], [deck_id]) VALUES (6, 3)
INSERT [dbo].[sessions_decks] ([session_id], [deck_id]) VALUES (7, 13)
INSERT [dbo].[sessions_decks] ([session_id], [deck_id]) VALUES (8, 13)
GO
SET IDENTITY_INSERT [dbo].[tag_types] ON 

INSERT [dbo].[tag_types] ([tag_id], [tag_name]) VALUES (1, N'None')
INSERT [dbo].[tag_types] ([tag_id], [tag_name]) VALUES (2, N'general')
INSERT [dbo].[tag_types] ([tag_id], [tag_name]) VALUES (3, N'math')
INSERT [dbo].[tag_types] ([tag_id], [tag_name]) VALUES (4, N'video games')
INSERT [dbo].[tag_types] ([tag_id], [tag_name]) VALUES (5, N'sports')
INSERT [dbo].[tag_types] ([tag_id], [tag_name]) VALUES (6, N'geography')
INSERT [dbo].[tag_types] ([tag_id], [tag_name]) VALUES (7, N'history')
INSERT [dbo].[tag_types] ([tag_id], [tag_name]) VALUES (8, N'art')
INSERT [dbo].[tag_types] ([tag_id], [tag_name]) VALUES (9, N'animals')
SET IDENTITY_INSERT [dbo].[tag_types] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt], [user_role], [avatar_url], [is_active]) VALUES (1, N'user', N'Jg45HuwT7PZkfuKTz6IB90CtWY4=', N'LHxP4Xh7bN0=', N'user', NULL, 1)
INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt], [user_role], [avatar_url], [is_active]) VALUES (2, N'admin', N'YhyGVQ+Ch69n4JMBncM4lNF/i9s=', N'Ar/aB2thQTI=', N'admin', NULL, 1)
INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt], [user_role], [avatar_url], [is_active]) VALUES (4, N'paul', N'IXNCIut9Wq5ha4xoLn3s5xtlnT4=', N'3kPvYZZNkS4=', N'user', NULL, 1)
INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt], [user_role], [avatar_url], [is_active]) VALUES (5, N'ryan', N'FyC2llCN6PKJqqpLJdOccjIXNNI=', N'4zLZSds7FNE=', N'user', NULL, 1)
INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt], [user_role], [avatar_url], [is_active]) VALUES (6, N'julian', N'avA+Ic3f7GhQFYK/0Zmm65pcWvI=', N'68g8jaWCUKU=', N'user', NULL, 1)
INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt], [user_role], [avatar_url], [is_active]) VALUES (7, N'jon', N'uYWfJbWtat8cCoM+ClXi8mQjOlE=', N'DaVuzsuKTuE=', N'user', NULL, 1)
INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt], [user_role], [avatar_url], [is_active]) VALUES (8, N'walt', N'f/0G8E0YG2q5N+O5z1sc8Pzv3n8=', N'JUmaWMTkFzA=', N'user', NULL, 1)
INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt], [user_role], [avatar_url], [is_active]) VALUES (9, N'nick', N'/9wjHY/YSnHKdyZtyFnli3LxdDg=', N'/oJ4/Vp4/QI=', N'user', NULL, 1)
INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt], [user_role], [avatar_url], [is_active]) VALUES (10, N'tom', N'QPUKeyD3DIIl/s8k670W+9bHrIM=', N'g0csTUquJ1o=', N'user', NULL, 1)
SET IDENTITY_INSERT [dbo].[users] OFF
GO
ALTER TABLE [dbo].[sessions] ADD  DEFAULT (getdate()) FOR [time_created]
GO
ALTER TABLE [dbo].[cards]  WITH CHECK ADD  CONSTRAINT [FK_cards_card_types] FOREIGN KEY([type_id])
REFERENCES [dbo].[card_types] ([type_id])
GO
ALTER TABLE [dbo].[cards] CHECK CONSTRAINT [FK_cards_card_types]
GO
ALTER TABLE [dbo].[cards_tags]  WITH CHECK ADD  CONSTRAINT [FK_cards_tags_cards] FOREIGN KEY([card_id])
REFERENCES [dbo].[cards] ([card_id])
GO
ALTER TABLE [dbo].[cards_tags] CHECK CONSTRAINT [FK_cards_tags_cards]
GO
ALTER TABLE [dbo].[cards_tags]  WITH CHECK ADD  CONSTRAINT [FK_cards_tags_tags] FOREIGN KEY([tag_id])
REFERENCES [dbo].[tag_types] ([tag_id])
GO
ALTER TABLE [dbo].[cards_tags] CHECK CONSTRAINT [FK_cards_tags_tags]
GO
ALTER TABLE [dbo].[decks]  WITH CHECK ADD  CONSTRAINT [FK_decks_category] FOREIGN KEY([category_id])
REFERENCES [dbo].[category_types] ([category_id])
GO
ALTER TABLE [dbo].[decks] CHECK CONSTRAINT [FK_decks_category]
GO
ALTER TABLE [dbo].[decks]  WITH CHECK ADD  CONSTRAINT [FK_decks_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[decks] CHECK CONSTRAINT [FK_decks_users]
GO
ALTER TABLE [dbo].[decks_cards]  WITH CHECK ADD  CONSTRAINT [FK_deck_card_cards] FOREIGN KEY([card_id])
REFERENCES [dbo].[cards] ([card_id])
GO
ALTER TABLE [dbo].[decks_cards] CHECK CONSTRAINT [FK_deck_card_cards]
GO
ALTER TABLE [dbo].[decks_cards]  WITH CHECK ADD  CONSTRAINT [FK_deck_card_decks] FOREIGN KEY([deck_id])
REFERENCES [dbo].[decks] ([deck_id])
GO
ALTER TABLE [dbo].[decks_cards] CHECK CONSTRAINT [FK_deck_card_decks]
GO
ALTER TABLE [dbo].[sessions]  WITH CHECK ADD  CONSTRAINT [FK_sessions_session_type] FOREIGN KEY([session_type_id])
REFERENCES [dbo].[session_types] ([session_type_id])
GO
ALTER TABLE [dbo].[sessions] CHECK CONSTRAINT [FK_sessions_session_type]
GO
ALTER TABLE [dbo].[sessions]  WITH CHECK ADD  CONSTRAINT [FK_sessions_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[sessions] CHECK CONSTRAINT [FK_sessions_users]
GO
ALTER TABLE [dbo].[sessions_cards]  WITH CHECK ADD  CONSTRAINT [FK_sessions_cards_cards] FOREIGN KEY([card_id])
REFERENCES [dbo].[cards] ([card_id])
GO
ALTER TABLE [dbo].[sessions_cards] CHECK CONSTRAINT [FK_sessions_cards_cards]
GO
ALTER TABLE [dbo].[sessions_cards]  WITH CHECK ADD  CONSTRAINT [FK_sessions_cards_sessions] FOREIGN KEY([session_id])
REFERENCES [dbo].[sessions] ([session_id])
GO
ALTER TABLE [dbo].[sessions_cards] CHECK CONSTRAINT [FK_sessions_cards_sessions]
GO
ALTER TABLE [dbo].[sessions_decks]  WITH CHECK ADD  CONSTRAINT [FK_sessions_decks_decks] FOREIGN KEY([deck_id])
REFERENCES [dbo].[decks] ([deck_id])
GO
ALTER TABLE [dbo].[sessions_decks] CHECK CONSTRAINT [FK_sessions_decks_decks]
GO
ALTER TABLE [dbo].[sessions_decks]  WITH CHECK ADD  CONSTRAINT [FK_sessions_decks_sessions] FOREIGN KEY([session_id])
REFERENCES [dbo].[sessions] ([session_id])
GO
ALTER TABLE [dbo].[sessions_decks] CHECK CONSTRAINT [FK_sessions_decks_sessions]
GO
ALTER TABLE [dbo].[cards]  WITH CHECK ADD CHECK  (([difficulty]>(0) AND [difficulty]<(6)))
GO
USE [master]
GO
ALTER DATABASE [final_capstone] SET  READ_WRITE 
GO
