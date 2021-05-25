use bibliotheek
set nocount on


/*Eventueel objecten verwijderen*/
drop table if exists Stripboek
drop table if exists Boek
drop table if exists Auteur
drop table if exists Ontlening
drop table if exists Itemgenre
drop table if exists Genre
drop table if exists Reservatie
drop table if exists Tijdschrift
drop table if exists Stripboek
drop table if exists Medewerker
drop table if exists Lid
drop table if exists Exemplaar
drop table if exists Item

/*tabellen*/
create table Item
(
	id int not null identity(1,1),
	titel nvarchar(50) not null,
	beschrijving nvarchar(500) null,
	coverfoto varbinary(max) null,
	uitgeverij nvarchar(50) not null,
	leeftijd_van int null,
	leeftijd_tot int null,
	taal nchar(2) not null,
	constraint PK_Item primary key (id)

)

create table Exemplaar
(
	id int not null identity,
	nummer int not null,
	item_id int not null,
	constraint PK_Exemplaar primary key (id),
	constraint FK_Exemplaar_item_id foreign key (item_id) references Item (id)
)

create table Lid
(
	lidnummer nchar(8) not null,
	voornaam nvarchar(20) not null,
	achternaam nvarchar(20) not null,
	geboortedatum date not null,
	straat nvarchar(50) not null,
	nummer nvarchar(10) not null,
	postcode nchar(4) not null,
	gemeente nvarchar(20) not null,
	vervaldatum_lidkaart date not null,
	gsm nchar(12) null,
	constraint PK_Lid primary key (lidnummer),
)

create table Ontlening
(
	id int not null identity,
	datum_uit date not null,
	uiterste_datum_in date not null,
	werkelijke_datum_in date null,
	exemplaar_id int not null,
	lid_lidnummer nchar(8) not null,
	boetebedrag int null, 
	boetedatum date null,
	constraint PK_Ontlening primary key (id),
	constraint FK_Ontlening_exemplaar_id foreign key (exemplaar_id) references Exemplaar (id),
	constraint FK_Ontlening_lid_lidnummer foreign key (lid_lidnummer) references Lid (lidnummer)
)

create table Medewerker
(
	id int not null identity,
	voornaam nvarchar(20) not null,
	achternaam nvarchar(20) not null,
	code_pasje nchar(4) not null,
	passwoord nvarchar(20) not null,
	constraint PK_Medewerker primary key (id),
	constraint UK_Medewerker_code_pasje unique (code_pasje)
)

create table Genre
(
	naam nvarchar(30) not null
	constraint PK_Genre primary key (naam)
)

create table Itemgenre
(
	item_id int not null identity,
	genre_naam nvarchar(30) not null,
	constraint PK_Itemgenre primary key (item_id, genre_naam),
	constraint FK_Itemgenre_item_id foreign key (item_id) references Item (id),
	constraint FK_Itemgenre_genre_naam foreign key (genre_naam) references Genre (naam)
)

create table Reservatie
(
	id int not null identity,
	datum_reservatie date not null,
	exemplaar_id int not null,
	lid_lidnummer nchar(8) not null,
	constraint PK_Reservatie primary key (id),
	constraint FK_Reservatie_exemplaar_id foreign key (exemplaar_id) references Exemplaar (id),
	constraint FK_Reservatie_lid_lidnummer foreign key (lid_lidnummer) references Lid (lidnummer)
)

create table Tijdschrift
(
	item_id int not null identity,
	jaargang nchar(4) not null,
	nummer int not null
	constraint PK_Tijdschrift primary key (item_id),
	constraint FK_Tijdschrift_item_id foreign key (item_id) references Item (id)
)

create table Auteur
(
	id int not null identity,
	voornaam nvarchar(20) not null,
	achternaam nvarchar(20) not null
	constraint PK_Auteur primary key (id)
)

create table Stripboek
(
	item_id int not null identity,
	volgnummer int not null,
	auteur_id int not null
	constraint PK_Stripboek primary key (item_id),
	constraint FK_Stripboek_item_id foreign key (item_id) references Item (id),
	constraint FK_Stripboek_auteur_id foreign key (auteur_id) references Auteur (id)
)

create table Boek
(
	item_id int not null identity,
	isbn nchar(13) not null,
	aantal_paginas int not null,
	auteur_id int not null
	constraint PK_Boek primary key (item_id),
	constraint FK_Boek_item_id foreign key (item_id) references Item (id),
	constraint FK_Boek_auteur_id foreign key (auteur_id) references Auteur (id),
	constraint UK_Boek_isbn unique (isbn)
)

/*Invoer tabellen*/
/*ITEM*/
INSERT INTO Item (titel, beschrijving, coverfoto, uitgeverij, leeftijd_van, leeftijd_tot, taal)
VALUES 
	   /*Boeken*/
	   ('Ik zie je op het strand', 'Clemence ontmoet tijdens een vliegreis de aantrekkelijke, maar getrouwde Sam; als drie jaar later haar zus een nieuwe vriend heeft, blijkt dit Sam te zijn.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\strand.png', single_blob) as boek1), 'Uitgeverij Luitingh-Sijthoff', 12, 99, 'NL'),
	   ('Ragdoll', 'Rechercheur William Wolf Fawkes van de Metropolitan Police in Londen probeert met twee collega s de moordenaar te vinden die de resten van zijn slachtoffers tot een lappenpop aan elkaar naait.',(select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\ragdoll.jpg', single_blob) as boek2),'Uitgeverij Luitingh-Sijthoff', 8, 99, 'NL'),
	   ('Vind haar', 'Een rechercheur, werkzaam bij de politie in Boston, moet samen met haar team de zaak rond een getraumatiseerde jonge vrouw die voor de tweede maal gekidnapt is, als ook de vermissing van drie vrouwen oplossen.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Vindhaar.jpg', single_blob) as boek3),'Cargo', 6, 99, 'NL'),
	   ('Versplinterd', 'Rechercheurs Will Trent en Faith Mitchell maken jacht op de ontvoerder van een 17-jarig meisje dat na een bizarre moordpartij in haar eigen huis is gekidnapt.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Versplinterd.jpg', single_blob) as boek4),'HarperCollins', 6, 99, 'NL'),
	   ('Jager', 'Nadat de minister van Buitenlandse Zaken is vermoord, wordt Joona Linna overgehaald om weer voor de Zweedse veiligheidsdienst te gaan werken.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Jager.png', single_blob) as boek5),'Cargo', 6, 99, 'NL'),
	   ('Riskante relaties', 'Een Franse burggraaf die vrouwen naar zijn hand weet te zetten, stuit op een vrouw die weerstand biedt.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Riskante.png', single_blob) as boek6),'Uitgeverij De Arbeiderspers', 3, 99, 'NL'),
	   ('Sprakeloos', 'Familierelaas over de aftakeling van de moeder van de schrijver, wier taalvermogen na een beroerte is geminimaliseerd.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Sprakeloos.png', single_blob) as boek7),'Prometheus', 8, 99, 'NL'),
	   ('De dragers van het Eikenblad', 'Als het lente wordt, kunnen Will en Evanlyn eindelijk ontsnappen uit Skandia. Dan wordt Evanlyn ontvoerd door mysterieuze ruiters die het op de Skandie?¨rs hebben gemunt. Vanaf ca. 11 jaar.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Dragers.png', single_blob) as boek8),'Gottmer', 12, 99, 'NL'),
	   ('Honderd jaar eenzaamheid', 'Ontstaan, bloei en verval van het geslacht der Buendia s in de door hen gestichte stad Macondo.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Honderd.png', single_blob) as boek9),'Meulenhoff', 10, 99, 'NL'),
	   ('Zondagochtend breekt aan', 'De Engelse psychotherapeute Frieda Klein, in wier huis een lijk is ontdekt, komt tijdens haar speurtocht naar de dader met haar vrienden in tal van beklemmende en verbijsterende situaties terecht.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Zondagochtend.png', single_blob) as boek10),'Ambo|Anthos', 8, 99, 'NL'),
		   
	      /*Tijdschriften*/
	   ('Science News', 'Science News offers readers bold, contemporary, award-winning editorial content and detailed imagery. Concise, current and comprehensive, the magazine provides an approachable overview from all fields and applications of science and technology.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Science.jpg', single_blob) as tijdschrift1),'Society for Science & the Public', 7, 99, 'EN'),
	   ('Maximum PC', 'Maximum PC is the go-to resource for PC enthusiasts. Every issue is chock-full of the latest computer technology news, reviews of the hottest hardware, detailed how-to projects, and in-depth feature stories aimed at hardcore computer hobbyists and anyone else who wants to learn how to do more with their PC.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Maximum.jpg', single_blob) as tijdschrift2),'Future Publishing Ltd', 7, 9, 'EN'),
	   ('Harvard Business Review', 'For over 80 years, Harvard Business Review magazine has been an indispensable and unrivaled source of ideas, insight, and inspiration for business leaders worldwide. Each issue contains breakthrough ideas on strategy, leadership, innovation and management. Become a more effective leader by subscribing to Harvard Business Review.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Harvard.jpg', single_blob) as tijdschrift3),'Harvard Business School Publishing', 6, 99, 'EN'),
	   ('Yoga Journal', 'No matter your personal yoga style or level, Yoga Journal brings you all of the top teachers, wisdom, and inspiration you need to grow your practice both on and off the mat. Yoga Journal focuses on the body-mind-spirit connection and its importance in all aspects of personal development.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Yoga.jpg', single_blob) as tijdschrift4),'Pocket Outdoor Media', 7, 99, 'EN'),
	   ('The Wall Street Journal', 'Your Wall Street Journal digital membership includes unparalleled, 24/7 coverage of global markets and business – along with insightful reporting on U.S. and World news, politics, technology, lifestyle and more.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Wallstreet.jpg', single_blob) as tijdschrift5),'Dow Jones & Company Inc.', 7, 99, 'EN'),
	   ('NEWBEAUTY', 'Dedicated 100 percent to beauty, NewBeauty magazine is on the cutting-edge of the latest supercharged beauty products, advanced beauty treatments and anti-aging strategies. Experts, readers, celebrities and editors share their secrets and solutions for every beauty concern, making NewBeauty the definitive source for scientifically accurate, expert-driven, comprehensive information.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Newbeauty.jpg', single_blob) as tijdschrift6),'Sandow Media', 7, 99, 'EN'),
	   ('MIT Technology Review', 'Anyone wanting to understand real technological innovation, you’ll find it with MIT Technology Review. Our mission is to make technology a greater force for good by bringing about better-informed, more conscious technology decisions through authoritative, influential, and trustworthy journalism.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\MIT.jpg', single_blob) as tijdschrift7),'MIT Technology Review', 7, 99, 'EN'),
	   ('Analog Science Fiction and Fact', 'Analog Science Fiction and Fact Magazine brings together celebrated authors, new talent, and award-winning stories, poems, and articles, as it has since its launch in 1930.',(select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Analog.jpg', single_blob) as tijdschrift8),'Dell Magazines', NULL, NULL, 'EN'),
	   ('Forbes', 'Forbes magazine names the richest people and the biggest companies and covers global business stories with insight, solid sourcing, and the sort of groupie zeal usually reserved for fanzines. No merger, new ad campaign, or lawsuit goes unnoticed and stories always focus on the movers who are shaking things up. Read Forbes to make sense of todays volatile market or just for the sheer pleasure of reading good reporting.',(select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Forbes.jpg', single_blob) as tijdschrift9),'Forbes', 12, 99, 'EN'),
	   ('The New York Times Book Review', 'The New York Times Book Review has been one of the most influential and widely read book review publications in the industry since its first publication in 1896. Reviewers select 20-30 notable or important new titles each week, including exceptional new authors. Now, join book lovers and professionals in subscribing to the stand alone Book Review.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\NewYork.jpg', single_blob) as tijdschrift10),'The New York Times Company', 12, 99, 'EN'),
	   
	   /*Stripboeken*/
	   ('Haaiman', 'Een verwisseling van fototoestellen voor het Epcot-center in Florida, de bizarre Holy Hamburger Movement-beweging, een wrak van een schip met juwelen van een oliesjeik en het nieuwe vriendje van Fanny dat in een haai transformeert: dit lijken wel elementen voor een Kiekeboe-verhaal van een beginnend scenarist.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Haaiman.jpg', single_blob) as stripboek1),'Merho', 8, 99, 'NL'),
	   ('De jokkende joker', 'Tijdens een bezoek aan het poppentheater van Moe Marionet wordt Wiske omgetoverd in een houten pop. Om weer in een meisje van vlees en bloed veranderen, moet ze Komedie en Tragedie terug naar hun poppenkast in het poppenwoud brengen. Deze twee handpoppen zijn de heersers over de magie van het poppenspel en worden ook door Moe Marionet gevangen gehouden. Suske helpt Wiske, Komedie en Tragedie ontsnappen en samen storten ze zich in een fantastisch avontuur, achternagezeten door Moe Marionet', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Jokkende.jpg', single_blob) as stripboek2),'Standaard Uitgeverij', 8, 99, 'NL'),
	   ('De gevangene', 'Een rechercheur, werkzaam bij de politie in Boston, moet samen met haar team de zaak rond een getraumatiseerde jonge vrouw die voor de tweede maal gekidnapt is, als ook de vermissing van drie vrouwen oplossen.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Gevangene.jpg', single_blob) as stripboek3),'Cargo', 8, 99, 'NL'),
	   ('De weerwolf', 'De Roder Ridder zwerft door Duitsland en tekt door het zwarte woud. Daar ontmoet hij het mooie herderinnetje Hildergarde. Een wolf heeft haar kalfje het struikgewas ingesleurd. Johan besluit het meisje te helpen en zoekt het kalfje. Even later vinden er het dier doodgebeten in het struikgewas.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Weerwolf.jpg', single_blob) as stripboek4),'Standaard Uitgeverij', 8, 99, 'NL'),
	   ('Schiet niet op de pianist', 'In opdracht van Van de Kasseien moet Kiekeboe compromitterende negatieven van de afperser Karel Keizer ontfutselen. Wanneer Kiekeboe daarbij nog eens betrokken wordt bij de ontvoering van een beroemde pianist, tevens het vriendje van Fanny, wordt de grond wel heel heet onder zijn voeten.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Schiet.jpg', single_blob) as stripboek5),'Merho', 8, 99, 'NL'),
	   ('De onthoofde sfinx', 'Konstantinopel wordt bestraald door het hoofd van een sfinxbeeldje. Om de verlammende bestraling ongedaan te maken en om het geheim van de sfinx te leren kennen, reist Kiekeboe, in het gezelschap van archeoloog Vladimir Zarkovaag, naar Egypte. Een geheimzinnige groene mummie en zijn assistent Balthazar zitten hen op de hielen.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Sfinx.jpg', single_blob) as stripboek6),'Merho', 8, 99, 'NL'),
	   ('Taxi comitée', 'Als Kiekeboe door zijn baas Van de Kasseien ontslagen wordt, moet hij op zoek naar een andere job. Hij wordt chauffeur bij taxibedrijf Taxi Comitée. Zo komt hij in aanraking met de duistere kanten van het nachtleven in de grootstad.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Taxi.jpg', single_blob) as stripboek7),'Standaard Uitgeveri', 8, 99, 'NL'),
	   ('De kwakende queen', 'Barabas is erg aangedaan als hij ontdekt dat zijn voorvader beschuldigd werd van een vreselijke misdaad. Diezelfde overgrootvader beweert echter onschuldig te zijn. Barabas stuurt zijn vrienden naar het verleden om de zaak op te helderen.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Kwakende.jpg', single_blob) as stripboek8),'Standaard Uitgeverij', 8, 99, 'NL'),
	   ('De hoed van Robin', 'De buurt van de Kiekeboes wordt geplaagd door een golf van inbraken. Volgens inspecteur Sapperdeboere is dit het werk van de Bende van de Gladde Paling, die voor de politie ongrijpbaar is. Kiekeboe neemt geen risicos en installeert een alarmsysteem, dat echter niet feilloos werkt.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\kiekeboes.jpg', single_blob) as stripboek9),'Merho', 8, 99, 'NL'),
	   ('The Superior Spider-Man', 'Wanneer burgemeester J. Jonah Jameson publiek vernederd wordt door Jester en Screwball roept hij de hulp in van Spider-Man. Otto Octavius, nog altijd in het lichaam van Peter Parker, gaat achter de twee misdadigers aan en neemt ze te grazen.', (select * from openrowset(bulk 'C:\Users\Afrem\Desktop\CASUS AD&SQL\images\Superior.jpg', single_blob) as stripboek10),'Standaard Uitgeverij', 8, 99, 'NL')

/*Exemplaar*/
INSERT INTO Exemplaar(nummer, item_id)
VALUES
		('10', 1),
		('20', 2),
		('12', 3),
		('21', 4),
		('21', 5),
		('13', 6),
		('19', 7),
		('11', 8),
		('23', 9),
		('10', 10),
		('20', 11),
		('12', 12),
		('21', 13),
		('13', 14),
		('19', 15),
		('11', 16),
		('23', 17),
		('10', 18),
		('20', 19),
		('12', 20),
		('21', 21),
		('13', 22),
		('19', 23),
		('11', 24),
		('23', 25),
		('12', 26),
		('21', 27),
		('13', 28),
		('19', 29),
		('11', 30);

/*Lid*/
INSERT INTO Lid(lidnummer ,voornaam, achternaam, geboortedatum, straat, nummer, postcode, gemeente, vervaldatum_lidkaart, gsm)
VALUES
		('6487945', 'Ali','Daali','06/09/2000','Ternatstraat','12','1740','Ternat', '03/05/2021','+32491564060'),
		('5647864', 'Mona','Aoualad','04/08/1999','Bouhouillestraat','188','4845','Sart-lez-spa', '02/16/2023','+32573118003'),
		('3124567', 'Jawad','Shakraoui','03/04/2000','Campanulesstraat','406','7618','Taintigniest', '06/10/2021','+32484230423'),
		('53468453', 'Branden','Aciz','03/08/2000','Ninoofsesteenweg','272','1750','Lennik', '03/10/2023','+32402661417'),
		('18763542', 'Sami','Cavas','06/09/1999','Alexander Wouterstraat','4','1090','Jette', '08/09/2020','+32497502070');
		
/*Ontlening*/
INSERT INTO Ontlening(datum_uit, uiterste_datum_in, werkelijke_datum_in, exemplaar_id, lid_lidnummer, boetebedrag, boetedatum)
VALUES
		('12/19/2021', '7/24/2024', '6/4/2021', 1, '6487945', 63, '6/4/2021'),
		('3/1/2022', '5/3/2024', '6/4/2021', 2, '5647864', null, null),
		('11/20/2021', '4/1/2025', '6/4/2021', 3, '3124567', null, null),
		('7/13/2021', '7/16/2025', '6/4/2021', 3, '53468453', null, null),
		('11/29/2022', '9/30/2023', '6/4/2021', 4, '18763542', 9, '2/26/2021');

/*Medewerker*/
INSERT INTO Medewerker(voornaam, achternaam, code_pasje, passwoord)
VALUES
		('Anthony', 'Pornel', '6471', 'kantoor'),
		('Bilal', 'Achaffay', '9745', 'informatica'),
		('Oumaima', 'Ahmiti', '1648', 'bachelor'),
		('Omar', 'Zouljami', '4561', 'hogeschool');

/*Auteur*/
INSERT INTO Auteur(voornaam, achternaam)
VALUES
		/*Boek*/
		('Jill','Mansell'),
		('Daniel','Cole'),
		('Lisa','Gardner'),
		('Karin','Slaughter'),
		('Lars','Kepler'),
		('Choderlos','de Laclos'),
		('Tom','Lanoye'),
		('John','Flanagan'),
		('Gabriel','Garcia Marquez'),
		('Nicci','French'),
		/*Striepboek*/
		('Dan','Slott'),
		('Peter','Van Gucht'), /*x2 suske en wiske*/
		('Marc','Legendre'),
		('Karel','Biddeloo'),
		('Peter','van Hooydonck'),/*Kiekeboes + suske en wiste (x4)*/ 
		('Joseph','Grieco'),
		/*Tijdschrift*/
		('Zak','Storey'),
		('Quinton','Quinton'),
		('Kara','Whitaker'),
		('Darnell','Gamble'),
		('Adalynn','Houston'),
		('Heaven','Mcintyre'),
		('Cullen','Fowler'),
		('Beckett','Mason'),
		('Brent','Graham'),
		('Adrienne','Blackwell')

/*Boek*/
 INSERT INTO Boek(isbn, aantal_paginas, auteur_id)
 VALUES 
		('9780312930332', 134, 1),
		('9780331241451', 78, 2),
		('9780312641897', 67, 3),
		('9780312145678', 97, 4),
		('9780312687942', 86, 5),
		('9780312346816', 141, 6),
		('9780312197642', 297, 7),
		('9780312134619', 212, 8),
		('9780312115647', 167, 9),
		('9780312648912', 180, 10)

/*Stripboek*/
INSERT INTO Stripboek(volgnummer, auteur_id)
VALUES
		(58, 11),
		(304, 12),
		(251, 12),
		(47, 13),
		(55, 14),
		(4, 15),
		(94, 15),
		(313, 15),
		(88, 15),
		(12, 16)

/*Tijdschrift*/		
INSERT INTO Tijdschrift(jaargang, nummer)
VALUES
		(2010, 1),
		(2020, 1),
		(2020, 1),
		(2020, 1),
		(2020, 1),
		(2021, 1),
		(2008, 1),
		(2008, 1),
		(2021, 1),
		(2021, 1)

/*Genre*/
INSERT INTO Genre(naam)
VALUES
		('Romans'),
		('Thrillers'),
		('Detectives'),
		('Autobiografische literatuur'),
		('Fantasy')

/*Itemgenre*/
INSERT INTO Itemgenre(genre_naam)
VALUES	
		('Romans'),
		('Thrillers'),
		('Detectives'),
		('Detectives'),
		('Thrillers'),
		('Romans'),
		('Autobiografische literatuur'),
		('Fantasy'),
		('Romans'),
		('Thrillers')

/*Reservatie*/
INSERT INTO Reservatie(datum_reservatie, exemplaar_id, lid_lidnummer)
VALUES 
		('6/13/2021', 4, '6487945'),
		('7/24/2021', 8, '5647864'),
		('9/17/2021', 13, '5647864'),
		('8/02/2021', 24, '3124567'),
		('6/14/2021', 17, '6487945');


