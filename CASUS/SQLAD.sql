--naam Dilmac
-- stdnr r0833187
set nocount on


DROP USER Steven 
DROP USER Afrem 
drop login Klant
drop login Admin
CREATE LOGIN Admin WITH PASSWORD = '';
CREATE USER Afrem FOR LOGIN Admin
GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO Afrem;
--LOGIN Klant
CREATE LOGIN Klant WITH PASSWORD = '';
Create user Steven FOR LOGIN Klant
GRANT SELECT, INSERT, UPDATE, DELETE ON [Winkelmand] TO Steven
GRANT SELECT, INSERT, UPDATE, DELETE ON [Winkelmand_item] TO Steven


/*Drops*/

--Tables
drop table if exists Afspraak
drop table if exists Admin
drop table if exists Product_categorie
drop table if exists Winkelmand_item
drop table if exists Winkelmand
drop table if exists Categorie
drop table if exists Product
drop table if exists Klant
drop table if exists ProductenDB
--Views
drop view if exists vwProduct
drop view if exists vwWinkelmand_item
drop view if exists vwWinkelmand
drop view if exists vwKlant
drop view if exists vwCategorie
drop view if exists vwProduct_Categorie
--Functions
drop function if exists fnPrijsWinkelmand
drop function if exists fnAantalVerkocht
drop function if exists fnAantalKlanten
drop function if exists fnUitverkoopProducten
--Procedures
drop procedure if exists spProductToevoegen
drop procedure if exists spProductWijzigen
drop procedure if exists spProduct_Verwijderen
drop procedure if exists spKlantToevoegen
drop procedure if exists spKlantWijzigen
drop procedure if exists spKlant_Verwijderen
drop procedure if exists spWinkelmandToevoegen
drop procedure if exists spWinkelmandWijzigen
drop procedure if exists spWinkelmand_Verwijderen
drop procedure if exists spWinkelmand_itemToevoegen
drop procedure if exists spWinkelmand_itemWijzigen
drop procedure if exists spWinkelmand_itemVerwijderen
drop procedure if exists spCategorieToevoegen
drop procedure if exists spCategorieWijzigen
drop procedure if exists spCategorie_Verwijderen
drop procedure if exists spProduct_CategorieToevoegen
drop procedure if exists spProduct_CategorieWijzigen
drop procedure if exists spProduct_Categorie_Verwijderen
--triggers
drop trigger if exists triKlantVerwijderen
drop trigger if exists triProductToevoegen

create table Admin(
	gebruikersnaam nvarchar(50) not null,
	passwoord nvarchar(50) not null
)

/*Tables*/

create table Klant
(
	id int not null identity(1,1),
	voornaam nvarchar(50) not null,
	achternaam nvarchar(50) not null,
	email nvarchar(255) not null,
	nummer nvarchar(6) not null,
	gemeente nvarchar(20) not null,
	postcode nvarchar(6) not null,
	telefoonnummer nvarchar(30) not null,
	geslacht nchar(1) not null default '?',
	constraint PK_Klant_Id primary key (id),
	constraint CK_Klant_geslacht check (geslacht in ('?','V', 'M')),
	constraint UK_Klant_email UNIQUE(email)

)

create table Product
(
	id int not null identity(1,1),
	naam nvarchar(50) not null,
	beschrijving nvarchar(500) null default '-',
	levertijd nvarchar(3) not null,
	voorraad nvarchar(3) not null default 'nee',
	kleur nvarchar(10) not null,
	foto image null,
	prijs decimal (6,2) not null,

	constraint PK_Product_Id primary key (id),
	constraint UK_Product_naam UNIQUE(naam),
	constraint CK_Product_prijs check (prijs > 0)
	
)

create table ProductenDB
(
	id int,
	naam nvarchar(50) not null,
	beschrijving nvarchar(500) null default '-',
	levertijd nvarchar(3) not null,
	voorraad nvarchar(3) not null default 'nee',
	kleur nvarchar(10) not null,
	prijs decimal (6,2) not null,
)

create table Winkelmand
(
	id int not null identity(1,1),
	datum_aanmaak date not null default getdate(),
	klant_id int not null,
	status nvarchar(8) not null,
	constraint PK_Winkelmand_Id primary key (id),
	constraint FK_Winkelmand_klant_id foreign key (klant_id) references Klant (id)

)

create table Winkelmand_item
(
	winkelmand_id int not null identity(1,1),
	product_id int not null,
	aantal int not null,
	status nvarchar(13) not null default 'Niet verkocht',
	constraint FK_Winkelmand_item foreign key (product_id) references Product (id),
	constraint CK_Winkelmand_item_aantal check (aantal > 0), 
	constraint UK_Winkelmand_item_product_id UNIQUE (product_id)

)

create table Categorie
(
	id int not null identity(1,1),
	naam nvarchar(50),
	constraint PK_Categorie_Id primary key (id),
	constraint FK_Categorie_id foreign key (id) references Product (id)
)

create table Product_Categorie
(
	categorie_naam nvarchar(50) not null,
	product_id int not null identity(1,1),
	constraint FK_Product_Categorie_id foreign key (product_id) references Categorie (id)
)



/*INSERT INTO
https://www.centralpoint.be/nl/laptops/
*/
GO

--Admin
INSERT INTO Admin (gebruikersnaam, passwoord)
VALUES ('Afrem', 'Odisee123')

--PRODUCT
INSERT INTO Product (naam, beschrijving, kleur, levertijd, voorraad, foto, prijs)
VALUES 
		/*LAPTOPS*/
		('Apple Mackbrook Pro', 'De Apple M1‑chip maakt de 13‑inch MacBook Pro ongenadig snel en krachtig. De CPU presteert tot 2,8 keer beter. De graphics zijn tot 5 keer zo snel. Dankzij de uiterst geavanceerde Neural Engine gaat machine learning tot 11 keer sneller. En de batterij gaat tot 20 uur mee, langer dan ooit in een Mac. Onze populairste notebook voor pro’s levert zo ongekende topprestaties.', 'Space grey', '-', 'Nee', '' ,1387),
		('Apple MacBook Air', 'Onze dunste en lichtste notebook bevat nu de Apple M1‑chip, waardoor hij een complete transformatie heeft ondergaan. De CPU is tot 3,5 keer zo snel, en de GPU is tot 5 keer zo snel. Dankzij de uiterst geavanceerde Neural Engine gaat machine learning tot 9 keer sneller. De batterij gaat langer mee dan ooit in een MacBook Air. En zonder ventilator werkt hij ook nog eens volkomen geruisloos. Dit is heel veel power die je zó met je meeneemt.', 'Grijs', '-', 'Nee', '',  933),
		('DELL XPS 9500', null,'Zwart','1','Ja','', 2383),
		('HP ProBook 450 G8','Met zijn nieuwe compacte ontwerp levert de zakelijke HP ProBook 450 laptop-pc snelle prestaties, optimale beveiliging en duurzaamheid voor flexibele medewerkers die zich verplaatsen tussen hun bureau, vergaderruimtes en thuis.','Zilver','1','Ja','', 860),
		('DELL Precision 3560',null,'Grijs','-','Nee','', 1311),
		('DELL Latitude 5520', null,'Grijs','1','Ja','', 927),
		('Acer Swift 1','Acer Swift 1 SF114-32-P7QA. Producttype: Notebook, Vormfactor: Clamshell. Processorfamilie: Intel® Pentium®, Processormodel: N5000, Frequentie van processor: 1,10 GHz. Beeldschermdiagonaal: 35,6 cm (14"), HD type: Full HD, Resolutie: 1920 x 1080 Pixels. Intern geheugen: 4 GB, Intern geheugentype: DDR4-SDRAM. Totale opslagcapaciteit: 128 GB, Opslagmedia: SSD. Ingebouwde grafische adapter. Inclusief besturingssysteem: Windows 10 S. Kleur van het product: Zilver','Zilver','-','Nee','', 395),
		('HP ProBook 650 G8', null,'Zilver','-','Nee','', 994),
		('Microsoft Surface Laptop 4',null,'Zwart','-','Nee','',2224),
		/*TOETSENBORDEN*/
		('Logitech K120',null,'Zwart','3-5','Ja','', 19),
		('Logitech K400 Plus',null,'Zwart',' 1','Ja','', 70),
		('Logitech Keyboard K120','Duurzame toetsen De toetsen kunnen tot wel 10 miljoen toetsaanslagen mee, dus u kunt blijven typen wanneer vele andere toetsenborden al de geest hebben gegeven. Stabiele, uitklapbare pootjes. U kunt de pootjes uitklappen om de hoek van het toetsenbord met 8 graden te vergroten om zo tegemoet te komen aan uw ergonomische behoeften.','Zwart','-','Nee','', 17),
		('Logitech MK330',null,'Zwart','1','Ja','', 52),
		('Logitech MK540','De MK540 Advanced is een draadloze toetsenbord- en muiscombinatie die meteen vertrouwd aanvoelt, en is ontworpen voor precisie, comfort en betrouwbaarheid. Het toetsenbord van normaal formaat heeft toetsen met een vertrouwde vorm en grootte, en de ergonomisch gevormde, symmetrische muis is ontworpen om comfortabel in elke hand te passen.',' Zwart','1','Ja','', 64),
		('Logitech MK710','Logitech MK710, Standard, Wireless, RF Wireless, QWERTY, Black, Mouse includedWaar comfort en productiviteit hand in hand gaan.','Zwart','1','Ja','', 103),
		('Logitech MK270','Wireless Combo MK270, 10 m, 2.4 GHz, QWERTY. Logitech MK270, Standard, Wireless, RF Wireless, QWERTY, Black, Mouse included','Zwart','3-5', 'Ja','', 40),
		('Logitech Desktop MK120','Logitech Desktop MK120, Standaard, Bedraad, USB, QWERTY, Zwart, Inclusief muis','Zwart','1','Ja','', 26),
		/*LASERPRINTERS*/
		('Brother MFC-L8690CDW','De MFC-L8690CDW is een professionele all-in-one kleurenlaserprinter die uitstekende printsnelheden combineert met geavanceerde mobiele en cloud mogelijkheden. Hierdoor integreert deze printer naadloos binnen uw bedrijf of kantoor.','Cyaan','-','Nee','', 422),
		('KYOCERA ECOSYS M5521','Deze professionele allrounder voldoet aan de eisen van elk bedrijf. De scannen, kopiëren, faxen en mobiele scanfuncties zorgen voor een optimaal en flexibel werkproces. De machine ondersteunt mobiel printen en is compatible met zowel Android als Apple iOS.','Zwart','-','Nee','', 284),
		('HP Color LaserJet Pro M255dw','Maak indruk met kleur en verhoog de efficiëntie. Profiteer van hoge kwaliteit in kleur en snelle dubbelzijdige printsnelheden. Bespaar tijd met Smart Task-snelkoppelingen in de HP Smart-app en print en scan vanaf uw telefoon.[2] Krijg naadloze verbindingen en sterke beveiligingsoplossingen.','Zwart','-','Nee','', 277),
		('HP Laser 107w','Kies voor een productieve printer voor een betaalbare prijs. Kies voor resultaten van hoge kwaliteit en print en scan vanaf uw telefoon.','Zwart','-','Nee','', 114),
		('Brother Professionele','De HL-L5100DN is ideaal voor het drukke kantoor. Deze laserprinter print in zwart-wit met een hoge snelheid tot 40 paginas per minuut en recto verso tot 20 zijden per minuut. Aansluiten kan via USB-interface of bekabeld netwerk.','Zwart','1','Ja','', 339),
		('HP Neverstop','espaar tot 60% op toner met de meegeleverde toner voor 5000 pagina Print zeven keer zoveel paginas met de meegeleverde toner. Vul voordelig de toner bij. Bespaar tijd door overal vandaan te printen en te scannen met de HP Smart-app.','Wit','1','Ja','', 226),
		('Canon i-SENSYS LBP312X','Maak snel hoogwaardige zwart-witprints op uw werk met deze compacte, high-speed kantoorprinter.','Wit','-','Nee','', 399),
		('Brother Netwerk 50 ppm','Brother HL-L6400DW, 1200 x 1200 DPI, 150000 paginas per maand, BR-Script 3,Epson FX,IBM ProPrinter,PCL 6,PDF 1.7, Laser, Zwart, 750 - 10000 paginas per maand','Zwart','-','Nee','', 525)

--KLANTEN
INSERT INTO Klant (voornaam, achternaam, email, nummer, gemeente, postcode, telefoonnummer, geslacht)
VALUES
		('Branden', 'Aciz', 'branden.aciz@hotmail.fr', '124', 'Jette', '1090', '+32497502070', 'M'),
		('Johan', 'Aciz', 'Johan.Aciz@gmail.com', '124', 'Jette', '1090', '+32421941913', 'M'),
		('Sami', 'Cavas', 'Samicavas@gmail.com', '8', 'Wemmel', '1013', '+321247965412', 'M'),
		('Ali', 'Daali', 'AliDaali@gmail.com', '47', 'Ternat', '1740', '+329815129423', 'M'),
		('Mona', 'Aoulad', 'MonaAoulad@gmail.com', '13', 'Turnhout', '2300', '+321321459478', 'V'),
		('Gamze', 'Cingoz', 'GamzeCingoz@gmail.com', '13', 'Laeken', '1020', '+323129457124', 'V'),
		('Yénora', 'Endean', 'pendean0@examiner.com	', '6', 'Fraga', '4625', '+32708477421', 'V'),
		('Gaïa', 'Southworth', 'csouthworth1@tripadvisor.com', '7069', 'Wielki', '80', '+32645978456', 'V'),
		('Mégane', 'Kerwin', 'dkerwin1o@google.com.au', '29', 'Los Dos Caminos', '452500', '+32647891234', 'V'),
		('Abayo', 'Oyinloye', 'abayomi.oyinloye@gmail.com', '13', 'Jette', '1090', '+32497102070', 'M'),
		('Jawad', 'Chakir', 'JawadChakir@gmail.com', '12', 'Laeken', '1020', '+32645781234', 'M')

--WINKELMAND
INSERT INTO Winkelmand (datum_aanmaak, klant_id,status)
VALUES
		('2021-06-08', 1,'OK'),
		('2021-07-27', 2,'OK'),
		('2021-08-01', 3,'NIET OK'),
		('2021-08-03', 4,'NIET OK'),
		('2021-07-27', 5,'OK'),
		('2019-04-13', 6,'OK'),
		('2020-11-19', 7,'OK'),
		('2021-08-04', 8,'NIET OK'),
		('2021-08-02', 9,'NIET OK')
--WINKELMAND ITEMS
INSERT INTO Winkelmand_item (product_id, aantal, status)
VALUES
		(1, 1, 'verkocht'),
		(7, 2, 'niet verkocht'),
		(4, 3, 'niet verkocht'),
		(19, 2, 'verkocht'),
		(17, 4, 'verkocht'),
		(23, 7, 'niet verkocht'),
		(2, 8, 'verkocht'),
		(16, 5, 'niet verkocht'),
		(20, 1, 'verkocht')

--CATEGORIE
INSERT INTO Categorie (naam)
VALUES
		('Laptops'),
		('Toetsenborden'),
		('Laser Printers')

/*VIEWS*/
GO
create view vwProduct
as 
	select * from Product

go

create view vwKlant
as
	select * from Klant
go

create view vwWinkelmand
as
	select * from Winkelmand
go

create view vwWinkelmand_item
as
	select * from Winkelmand_item
go

create view vwCategorie
as
	select * from Categorie
go

create view vwProduct_Categorie
as
	select * from Product_Categorie
go


/*PROCEDURES*/

/* PRODUCT TOEVOEGEN
Result = 1 nieuwe item toegevoegd.
Result = 2 niet toegevoegd item bestaat al.
*/
create procedure spProductToevoegen
	@id int,
	@naam nvarchar(50),
	@beschrijving nvarchar(500),
	@levertijd nvarchar(3),
	@voorraad nvarchar(3),
	@kleur nvarchar(10),
	@prijs decimal(6,2)
as
begin
	/*declare variables*/
	declare @result int = 0
	if not exists (select * from Product where id = @id and naam = @naam and beschrijving = @beschrijving and levertijd = @levertijd and voorraad = @voorraad and kleur = @kleur and prijs = @prijs)
	begin
		insert into Product(id, naam, beschrijving, levertijd, voorraad, kleur, prijs)
		values
				(@id, @naam, @beschrijving, @levertijd, @voorraad, @kleur, @prijs)
		set @result = 1
		print @result
	end
	else
	begin
		set @result = 2
		print @result
	end
	return @result
end
GO

/*
PRODUCT WIJZIGEN
return 1 = wijziging van product
return 2 = wijziging niet gemaakt al bestaande product
*/

create procedure spProductWijzigen
	@id int,
	@naam nvarchar(50),
	@beschrijving nvarchar(500),
	@levertijd nvarchar(3),
	@voorraad nvarchar(3),
	@kleur nvarchar(10),
	@prijs decimal(6,2)
AS
BEGIN
	/*declare variables*/
	declare @result int = 1
	if exists (select *
			   from Product 
			   where id = @id)
BEGIN
UPDATE Product
set naam = @naam, beschrijving = @beschrijving, levertijd = @levertijd, voorraad = @voorraad, kleur = @kleur, prijs = @prijs
where id = @id;
END
ELSE
BEGIN
	SET @result = 2;
END
return @result
end;
GO

/*
PRODUCT VERWIJDEREN
return 1 = product bestaat niet
*/
create procedure spProduct_Verwijderen
	@id int
AS
BEGIN
	declare @resultaat int = 0
	if exists (select * from Product where id = @id)
	begin
		delete Product where id = @id
	end
	else
	begin
		set @resultaat = 1
	end
	return @resultaat
end
GO
------------------------------------------------------------

/* KLANT TOEVOEGEN
Result = 1 nieuwe item toegevoegd.
Result = 2 niet toegevoegd item bestaat al.
*/

create procedure spKlantToevoegen
	@id int,
	@voornaam nvarchar(50),
	@achternaam nvarchar(50),
	@email nvarchar(255),
	@nummer nvarchar(6),
	@gemeente nvarchar(20),
	@postcode nvarchar(6),
	@telefoonnummer nvarchar(300),
	@geslacht nchar(1)
as
begin
	/*declare variables*/
	declare @result int = 0
	if not exists (select * from Klant where id = @id and voornaam = @voornaam and achternaam = @achternaam and email = @email and nummer = @nummer and gemeente = @gemeente and postcode = @postcode and telefoonnummer = @telefoonnummer and geslacht = @geslacht)
	begin
		insert into Klant(id, voornaam, achternaam, email, nummer, gemeente, postcode, telefoonnummer, geslacht)
		values
				(@id, @voornaam, @achternaam, @email, @nummer, @gemeente, @postcode, @telefoonnummer, @geslacht)
		set @result = 1
		print @result
	end
	else
	begin
		set @result = 2
		print @result
	end
	return @result
end
GO

/*
KLANT WIJZIGEN
return 1 = wijziging van Klant
return 2 = wijziging niet gemaakt al bestaande Klant
*/

create procedure spKlantWijzigen
	@id int,
	@voornaam nvarchar(50),
	@achternaam nvarchar(50),
	@email nvarchar(255),
	@nummer nvarchar(6),
	@gemeente nvarchar(20),
	@postcode nvarchar(6),
	@telefoonnummer nvarchar(300),
	@geslacht nchar(1)
AS
BEGIN
	/*declare variables*/
	declare @result int = 1
	if exists (select *
			   from Klant 
			   where id = @id)
BEGIN
UPDATE Klant
set voornaam = @voornaam, achternaam = @achternaam, email = @email, nummer = @nummer, gemeente = @gemeente, postcode = @postcode, telefoonnummer = @telefoonnummer, geslacht = @geslacht
where id = @id;
END
ELSE
BEGIN
	SET @result = 2;
END
return @result
end;
GO

/*
KLANT VERWIJDEREN
return 1 = product bestaat niet
*/
create procedure spKlant_Verwijderen
	@id int
AS
BEGIN
	declare @resultaat int = 0
	if exists (select * from Klant where id = @id)
	begin
		delete Klant where id = @id
	end
	else
	begin
		set @resultaat = 1
	end
	return @resultaat
end
GO
------------------------------------------------------------

/* WINKELMAND TOEVOEGEN
Result = 1 nieuwe record toegevoegd.
Result = 2 niet toegevoegd record bestaat al.
*/

create procedure spWinkelmandToevoegen
	@id int,
	@datum_aanmaak date,
	@klant_id int,
	@status nvarchar(8)
as
begin
	/*declare variables*/
	declare @result int = 0
	if not exists (select * from Winkelmand where id = @id and datum_aanmaak = @datum_aanmaak and klant_id = @klant_id and status = @status)
	begin
		insert into Winkelmand(id, datum_aanmaak, klant_id, status)
		values
				(@id, @datum_aanmaak, @klant_id, @status)
		set @result = 1
		print @result
	end
	else
	begin
		set @result = 2
		print @result
	end
	return @result
end
GO

/*
WINKELMAND WIJZIGEN
return 1 = wijziging van WINKELMAND
return 2 = wijziging niet gemaakt al bestaande WINKELMAND
*/

create procedure spWinkelmandWijzigen
	@id int,
	@datum_aanmaak date,
	@klant_id int,
	@status nvarchar(8)
AS
BEGIN
	/*declare variables*/
	declare @result int = 1
	if exists (select *
			   from Winkelmand 
			   where id = @id)
BEGIN
UPDATE Winkelmand
set datum_aanmaak = @datum_aanmaak, klant_id = @klant_id, status = @status
where id = @id;
END
ELSE
BEGIN
	SET @result = 2;
END
return @result
end;
GO

/*
WINKELMAND VERWIJDEREN
return 1 = WINKELMAND bestaat niet
*/
create procedure spWinkelmand_Verwijderen
	@id int
AS
BEGIN
	declare @resultaat int = 0
	if exists (select * from Winkelmand where id = @id)
	begin
		delete Winkelmand where id = @id
	end
	else
	begin
		set @resultaat = 1
	end
	return @resultaat
end
GO
------------------------------------------------------------

/* WINKELMAND_ITEM TOEVOEGEN
Result = 1 nieuwe item toegevoegd.
Result = 2 niet toegevoegd item bestaat al.
*/

create procedure spWinkelmand_itemToevoegen
	@winkelmand_id int,
	@product_id int,
	@aantal int
as
begin
	/*declare variables*/
	declare @result int = 0
	if not exists (select * from Winkelmand_item where winkelmand_id = @winkelmand_id and product_id = @product_id and aantal = @aantal)
	begin
		insert into Winkelmand_item(winkelmand_id, product_id, aantal)
		values
				(@winkelmand_id, @product_id, @aantal)
		set @result = 1
		print @result
	end
	else
	begin
		set @result = 2
		print @result
	end
	return @result
end
GO

/*
WINKELMAND_ITEM WIJZIGEN
return 1 = wijziging van WINKELMAND_ITEM
return 2 = wijziging niet gemaakt al bestaande WINKELMAND_ITEM
*/

create procedure spWinkelmand_itemWijzigen
	@winkelmand_id int,
	@product_id int,
	@aantal int
AS
BEGIN
	/*declare variables*/
	declare @result int = 1
	if exists (select *
			   from Winkelmand_item 
			   where winkelmand_id = @winkelmand_id)
BEGIN
UPDATE Winkelmand_item
set product_id = @product_id, aantal = @aantal
where winkelmand_id = @winkelmand_id;
END
ELSE
BEGIN
	SET @result = 2;
END
return @result
end;
GO

/*
WINKELMAND_ITEM VERWIJDEREN
return 1 = WINKELMAND_ITEM bestaat niet
*/
create procedure spWinkelmand_itemVerwijderen
	@winkelmand_id int
AS
BEGIN
	declare @resultaat int = 0
	if exists (select * from Winkelmand_item where winkelmand_id = @winkelmand_id)
	begin
		delete Winkelmand_item where winkelmand_id = @winkelmand_id
	end
	else
	begin
		set @resultaat = 1
	end
	return @resultaat
end
GO
------------------------------------------------------------

/* CATEGORIE TOEVOEGEN
Result = 1 nieuwe CATEGORIE toegevoegd.
Result = 2 niet toegevoegd Categorie bestaat al.
*/
create procedure spCategorieToevoegen
	@id int,
	@naam nvarchar(50)
as
begin
	/*declare variables*/
	declare @result int = 0
	if not exists (select * from Categorie where id = @id and naam = @naam)
	begin
		insert into Categorie(id, naam)
		values
				(@id, @naam)
		set @result = 1
		print @result
	end
	else
	begin
		set @result = 2
		print @result
	end
	return @result
end
GO

/*
CATEGORIE WIJZIGEN
return 1 = wijziging van Categorie
return 2 = wijziging niet gemaakt al bestaande Categorie
*/

create procedure spCategorieWijzigen
	@id int,
	@naam nvarchar(50)
AS
BEGIN
	/*declare variables*/
	declare @result int = 1
	if exists (select *
			   from Categorie 
			   where id = @id)
BEGIN
UPDATE Categorie
set naam = @naam
where id = @id;
END
ELSE
BEGIN
	SET @result = 2;
END
return @result
end;
GO

/*
CATEGORIE VERWIJDEREN
return 1 = CATEGORIE bestaat niet
*/
create procedure spCategorie_Verwijderen
	@id int
AS
BEGIN
	declare @resultaat int = 0
	if exists (select * from Categorie where id = @id)
	begin
		delete Categorie where id = @id
	end
	else
	begin
		set @resultaat = 1
	end
	return @resultaat
end
GO
------------------------------------------------------------

/* PRODUCT_CATEGORIE TOEVOEGEN
Result = 1 nieuwe Product_Categorie toegevoegd.
Result = 2 niet toegevoegd Product_Categorie bestaat al.
*/
create procedure spProduct_CategorieToevoegen
	@categorie_naam nvarchar(50),
	@product_id int
as
begin
	/*declare variables*/
	declare @result int = 0
	if not exists (select * from Product_Categorie where product_id = @product_id and categorie_naam = @categorie_naam)
	begin
		insert into Product_Categorie(product_id, categorie_naam)
		values
				(@product_id, @categorie_naam)
		set @result = 1
		print @result
	end
	else
	begin
		set @result = 2
		print @result
	end
	return @result
end
GO

/*
PRODUCT-CATEGORIE WIJZIGEN
return 1 = wijziging van Product_Categorie
return 2 = wijziging niet gemaakt al bestaande Product_Categorie
*/

create procedure spProduct_CategorieWijzigen
	@categorie_naam nvarchar(50),
	@product_id int
AS
BEGIN
	/*declare variables*/
	declare @result int = 1
	if exists (select *
			   from Product_Categorie 
			   where product_id = @product_id)
BEGIN
UPDATE Product_Categorie
set categorie_naam = @categorie_naam
where product_id = @product_id;
END
ELSE
BEGIN
	SET @result = 2;
END
return @result
end;
GO

/*
PRODUCT_CATEGORIE VERWIJDEREN
return 1 = Product_Categorie bestaat niet
*/
create procedure spProduct_Categorie_Verwijderen
	@product_id int
AS
BEGIN
	declare @resultaat int = 0
	if exists (select * from Product_Categorie where product_id = @product_id)
	begin
		delete Product_Categorie where product_id = @product_id
	end
	else
	begin
		set @resultaat = 1
	end
	return @resultaat
end
GO


/*FUNCTIES*/
--PRIJS WINKELMAND
CREATE FUNCTION fnPrijsWinkelmand ( @id int)
RETURNS DECIMAL
AS
BEGIN
/* declare variables */
DECLARE @total DECIMAL;
/* calculate total */
select @total = SUM(prijs)
from Product P JOIN Winkelmand_item W ON P.id = W.product_id
WHERE W.winkelmand_id = @id

/* return */
RETURN @total
END


GO
--AANTAL VERKOCHT
CREATE FUNCTION fnAantalVerkocht ( @ID INT )
RETURNS DECIMAL
AS BEGIN
/* declare variables */
DECLARE @aantal int;
DECLARE @status nvarchar(8);
DECLARE @Aantal_Verkocht int;
/* get values for employee */
SELECT
@aantal = aantal,
@status = status
FROM Winkelmand_item
WHERE product_id = @ID;
/* calculate total */
IF @status = 'verkocht'
SET @Aantal_Verkocht = @Aantal_Verkocht + @aantal
ELSE
SET @Aantal_Verkocht = @Aantal_Verkocht
/* return */
RETURN @Aantal_Verkocht
END
GO

--AANTAL KLANTEN
CREATE FUNCTION fnAantalKlanten ( @ID INT )
RETURNS DECIMAL
AS BEGIN
/* declare variables */
DECLARE @aantal_klanten int;
/* get values for employee */
SELECT @aantal_klanten = COUNT(id)
FROM Klant
/* return */
RETURN @aantal_klanten
END
GO

--UITVERKOOP 20%
CREATE FUNCTION fnUitverkoopProducten (@ID INT)
RETURNS DECIMAL
AS BEGIN
/* declare variables */
DECLARE @NieuwPrijs decimal (6,2);
DECLARE @prijs decimal (6,2);

/* get values for employee */
SELECT @prijs = prijs
FROM Product
/*Calculate*/
SET @NieuwPrijs = (@prijs / 100) * 20
/* return */
RETURN @NieuwPrijs
END
GO

/*TRIGGERS*/
--Delete klant & winkelmand vd klant
create trigger triKlantVerwijderen on Klant instead of delete
as
begin
	declare @id int 
	set @id = (select id from deleted)
	delete Klant where id = @id
	delete Winkelmand where klant_id = @id
end
go

--Insert Product
--Hier wordt bijgehouden wanneer er een nieuwe Product wordt ingesteld binnen onze database.
create trigger triProductToevoegen on Product instead of insert
as
begin
		declare @id int;
		declare @naam nvarchar(50);
		declare @beschrijving nvarchar(500);
		declare @levertijd nvarchar(3);
		declare @voorraad nvarchar(3);
		declare @kleur nvarchar(10);
		declare @prijs decimal(6,2);
		
		select @id = Product.id from inserted Product;
		select @naam = Product.naam from inserted Product;
		select @beschrijving = Product.beschrijving from inserted Product;
		select @levertijd = Product.levertijd from inserted Product;
		select @voorraad = Product.voorraad from inserted Product;
		select @kleur = Product.kleur from inserted Product;
		select @prijs = Product.prijs from inserted Product;

		INSERT INTO ProductenDB
		VALUES (@id, @naam, @beschrijving, @levertijd, @voorraad, @kleur, @prijs)
END
GO

