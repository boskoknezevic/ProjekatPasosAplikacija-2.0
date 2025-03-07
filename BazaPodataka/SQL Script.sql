USE [master]
GO
/****** Object:  Database [izdavanjePasosa]    Script Date: 9/19/2024 3:31:04 AM ******/
CREATE DATABASE [izdavanjePasosa]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'izdavanjePasosa', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\izdavanjePasosa.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'izdavanjePasosa_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\izdavanjePasosa_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [izdavanjePasosa] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [izdavanjePasosa].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [izdavanjePasosa] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [izdavanjePasosa] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [izdavanjePasosa] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [izdavanjePasosa] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [izdavanjePasosa] SET ARITHABORT OFF 
GO
ALTER DATABASE [izdavanjePasosa] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [izdavanjePasosa] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [izdavanjePasosa] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [izdavanjePasosa] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [izdavanjePasosa] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [izdavanjePasosa] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [izdavanjePasosa] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [izdavanjePasosa] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [izdavanjePasosa] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [izdavanjePasosa] SET  ENABLE_BROKER 
GO
ALTER DATABASE [izdavanjePasosa] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [izdavanjePasosa] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [izdavanjePasosa] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [izdavanjePasosa] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [izdavanjePasosa] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [izdavanjePasosa] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [izdavanjePasosa] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [izdavanjePasosa] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [izdavanjePasosa] SET  MULTI_USER 
GO
ALTER DATABASE [izdavanjePasosa] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [izdavanjePasosa] SET DB_CHAINING OFF 
GO
ALTER DATABASE [izdavanjePasosa] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [izdavanjePasosa] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [izdavanjePasosa] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [izdavanjePasosa] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [izdavanjePasosa] SET QUERY_STORE = ON
GO
ALTER DATABASE [izdavanjePasosa] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [izdavanjePasosa]
GO
/****** Object:  Table [dbo].[KORISNIK]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KORISNIK](
	[JMBG] [nvarchar](13) NOT NULL,
	[Ime] [nvarchar](20) NOT NULL,
	[Prezime] [nvarchar](40) NOT NULL,
	[DatumRodjenja] [date] NOT NULL,
	[Adresa] [nvarchar](40) NOT NULL,
	[Telefon] [nvarchar](15) NOT NULL,
	[PTTMesto] [int] NOT NULL,
	[IDDrzavaRodjenja] [nvarchar](3) NOT NULL,
	[IDDrzavaDrzavljanstva] [nvarchar](3) NOT NULL,
	[KorisnickoIme] [nvarchar](20) NOT NULL,
	[PolKorisnika] [int] NOT NULL,
	[Email] [nvarchar](40) NOT NULL,
	[Lozinka] [nvarchar](20) NOT NULL,
	[TipKorisnika] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[JMBG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_KorisnickoIme] UNIQUE NONCLUSTERED 
(
	[KorisnickoIme] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PASOS]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PASOS](
	[BrojPasosa] [int] NOT NULL,
	[JMBGKorisnika] [nvarchar](13) NOT NULL,
	[Ime] [nvarchar](20) NOT NULL,
	[Prezime] [nvarchar](40) NOT NULL,
	[PTTMesto] [int] NOT NULL,
	[IDDrzavaRodjenja] [nvarchar](3) NOT NULL,
	[IDDrzavaDrzavljanstva] [nvarchar](3) NOT NULL,
	[PTTMestaPU] [int] NOT NULL,
	[DatumPocetkaVazenja] [date] NULL,
	[DatumIsteka] [date] NULL,
	[StatusDokumenta] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BrojPasosa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MESTO]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MESTO](
	[PTT] [int] NOT NULL,
	[Naziv] [nvarchar](40) NOT NULL,
	[SifraDrzave] [nvarchar](3) NULL,
PRIMARY KEY CLUSTERED 
(
	[PTT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DRZAVA]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DRZAVA](
	[SifraDrzave] [nvarchar](3) NOT NULL,
	[Naziv] [nvarchar](40) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SifraDrzave] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[STATUSDOKUMENTA]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STATUSDOKUMENTA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Opis] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POLKORISNIKA]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POLKORISNIKA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Opis] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[PasosView]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PasosView] AS
SELECT 
    P.BrojPasosa,
    P.StatusDokumenta AS StatusDokumentaID,
    S.Opis AS StatusDokumenta,
    K.JMBG AS JMBGKorisnika,
    K.Ime AS ImeKorisnika,
    K.Prezime AS PrezimeKorisnika,
    K.DatumRodjenja,
    M.PTT AS PTTMestoID,
    M.Naziv AS Mesto,
    DR1.SifraDrzave AS IDDrzavaRodjenja,
    DR1.Naziv AS DrzavaRodjenja,
    DR2.SifraDrzave AS IDDrzaveDrzavljanstva,
    DR2.Naziv AS DrzavaDrzavljanstva,
    MP.PTT AS PTTMestoPU,
    MP.Naziv AS MestoPU,
    P.DatumPocetkaVazenja,
    P.DatumIsteka,
    K.PolKorisnika AS PolKorisnikaID,
    PK.Opis AS PolKorisnika
FROM 
    dbo.PASOS P
    JOIN dbo.KORISNIK K ON P.JMBGKorisnika = K.JMBG
    JOIN dbo.MESTO M ON P.PTTMesto = M.PTT
    JOIN dbo.DRZAVA DR1 ON P.IDDrzavaRodjenja = DR1.SifraDrzave
    JOIN dbo.DRZAVA DR2 ON P.IDDrzavaDrzavljanstva = DR2.SifraDrzave
    JOIN dbo.MESTO MP ON P.PTTMestaPU = MP.PTT
    JOIN dbo.StatusDokumenta S ON P.StatusDokumenta = S.ID
    JOIN dbo.PolKorisnika PK ON K.PolKorisnika = PK.ID; 
GO
/****** Object:  Table [dbo].[TERMIN]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TERMIN](
	[IDTermina] [int] IDENTITY(1,1) NOT NULL,
	[BrojPasosa] [int] NOT NULL,
	[Datum] [date] NOT NULL,
	[Vreme] [time](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IDTermina] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TerminView]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TerminView] AS
SELECT 
    T.IDTermina,
    P.BrojPasosa,
    K.JMBG AS JMBGKorisnika,
    K.Ime AS ImeKorisnika,
    K.Prezime AS PrezimeKorisnika,
    T.Datum,
    T.Vreme
FROM 
    dbo.TERMIN T
    JOIN dbo.PASOS P ON T.BrojPasosa = P.BrojPasosa
    JOIN dbo.KORISNIK K ON P.JMBGKorisnika = K.JMBG;
GO
/****** Object:  Table [dbo].[TIPKORISNIKA]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TIPKORISNIKA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Opis] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[KorisnikView]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[KorisnikView] AS
SELECT 
    K.JMBG,
    K.Ime,
    K.Prezime,
    K.DatumRodjenja,
    K.Adresa,
    K.Telefon,
    M.PTT AS PTTMestoID,
    M.Naziv AS Mesto,
    DR1.SifraDrzave AS IDDrzavaRodjenja,
    DR1.Naziv AS DrzavaRodjenja,
    DR2.SifraDrzave AS IDDrzavaDrzavljanstva,
    DR2.Naziv AS DrzavaDrzavljanstva,
    K.KorisnickoIme,
    P.ID AS PolKorisnikaID,
    P.Opis AS Pol,
    K.Email,
    K.Lozinka,
    T.ID AS TipKorisnikaID,
    T.Opis AS TipKorisnika
FROM 
    dbo.KORISNIK K
    JOIN dbo.MESTO M ON K.PTTMesto = M.PTT
    JOIN dbo.DRZAVA DR1 ON K.IDDrzavaRodjenja = DR1.SifraDrzave
    JOIN dbo.DRZAVA DR2 ON K.IDDrzavaDrzavljanstva = DR2.SifraDrzave
    JOIN dbo.POLKORISNIKA P ON K.PolKorisnika = P.ID
    JOIN dbo.TIPKORISNIKA T ON K.TipKorisnika = T.ID;
GO
/****** Object:  Table [dbo].[ZAHTEVZAIZDAVANJEPASOSA]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ZAHTEVZAIZDAVANJEPASOSA](
	[IDZahteva] [int] IDENTITY(1,1) NOT NULL,
	[DatumiVremeZahteva] [datetime] NOT NULL,
	[JMBGKorisnika] [nvarchar](13) NOT NULL,
	[OcekivaniDatumIzdavanja] [date] NOT NULL,
	[PTTMestaPU] [int] NOT NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IDZahteva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TIPSTATUSA]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TIPSTATUSA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Opis] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ZahtevZaIzdavanjePasosaView]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ZahtevZaIzdavanjePasosaView] AS
SELECT 
    Z.IDZahteva,
    Z.DatumiVremeZahteva,
    K.JMBG AS JMBGKorisnika,
    K.Ime AS ImeKorisnika,
    K.Prezime AS PrezimeKorisnika,
    Z.OcekivaniDatumIzdavanja,
    M.PTT AS PTTMestoPU,
    M.Naziv AS MestoPU,
    S.ID AS StatusID,
    S.Opis AS Status
FROM 
    dbo.ZAHTEVZAIZDAVANJEPASOSA Z
    JOIN dbo.KORISNIK K ON Z.JMBGKorisnika = K.JMBG
    JOIN dbo.MESTO M ON Z.PTTMestaPU = M.PTT
    JOIN dbo.TIPSTATUSA S ON Z.Status = S.ID;
GO
ALTER TABLE [dbo].[KORISNIK]  WITH CHECK ADD  CONSTRAINT [FK_Korisnik_DrzavaDrzavljanstva] FOREIGN KEY([IDDrzavaDrzavljanstva])
REFERENCES [dbo].[DRZAVA] ([SifraDrzave])
GO
ALTER TABLE [dbo].[KORISNIK] CHECK CONSTRAINT [FK_Korisnik_DrzavaDrzavljanstva]
GO
ALTER TABLE [dbo].[KORISNIK]  WITH CHECK ADD  CONSTRAINT [FK_Korisnik_DrzavaRodjenja] FOREIGN KEY([IDDrzavaRodjenja])
REFERENCES [dbo].[DRZAVA] ([SifraDrzave])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KORISNIK] CHECK CONSTRAINT [FK_Korisnik_DrzavaRodjenja]
GO
ALTER TABLE [dbo].[KORISNIK]  WITH CHECK ADD  CONSTRAINT [FK_Korisnik_Mesto] FOREIGN KEY([PTTMesto])
REFERENCES [dbo].[MESTO] ([PTT])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KORISNIK] CHECK CONSTRAINT [FK_Korisnik_Mesto]
GO
ALTER TABLE [dbo].[KORISNIK]  WITH CHECK ADD  CONSTRAINT [FK_Korisnik_Pol] FOREIGN KEY([PolKorisnika])
REFERENCES [dbo].[POLKORISNIKA] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KORISNIK] CHECK CONSTRAINT [FK_Korisnik_Pol]
GO
ALTER TABLE [dbo].[KORISNIK]  WITH CHECK ADD  CONSTRAINT [FK_Korisnik_TipKorisnika] FOREIGN KEY([TipKorisnika])
REFERENCES [dbo].[TIPKORISNIKA] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KORISNIK] CHECK CONSTRAINT [FK_Korisnik_TipKorisnika]
GO
ALTER TABLE [dbo].[PASOS]  WITH CHECK ADD  CONSTRAINT [FK_Pasos_DrzavaDrzavljanstva] FOREIGN KEY([IDDrzavaDrzavljanstva])
REFERENCES [dbo].[DRZAVA] ([SifraDrzave])
GO
ALTER TABLE [dbo].[PASOS] CHECK CONSTRAINT [FK_Pasos_DrzavaDrzavljanstva]
GO
ALTER TABLE [dbo].[PASOS]  WITH CHECK ADD  CONSTRAINT [FK_Pasos_DrzavaRodjenja] FOREIGN KEY([IDDrzavaRodjenja])
REFERENCES [dbo].[DRZAVA] ([SifraDrzave])
GO
ALTER TABLE [dbo].[PASOS] CHECK CONSTRAINT [FK_Pasos_DrzavaRodjenja]
GO
ALTER TABLE [dbo].[PASOS]  WITH CHECK ADD  CONSTRAINT [FK_Pasos_Korisnik] FOREIGN KEY([JMBGKorisnika])
REFERENCES [dbo].[KORISNIK] ([JMBG])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PASOS] CHECK CONSTRAINT [FK_Pasos_Korisnik]
GO
ALTER TABLE [dbo].[PASOS]  WITH CHECK ADD  CONSTRAINT [FK_Pasos_Mesto] FOREIGN KEY([PTTMesto])
REFERENCES [dbo].[MESTO] ([PTT])
GO
ALTER TABLE [dbo].[PASOS] CHECK CONSTRAINT [FK_Pasos_Mesto]
GO
ALTER TABLE [dbo].[PASOS]  WITH CHECK ADD  CONSTRAINT [FK_Pasos_MestoPU] FOREIGN KEY([PTTMestaPU])
REFERENCES [dbo].[MESTO] ([PTT])
GO
ALTER TABLE [dbo].[PASOS] CHECK CONSTRAINT [FK_Pasos_MestoPU]
GO
ALTER TABLE [dbo].[PASOS]  WITH CHECK ADD  CONSTRAINT [FK_Pasos_StatusDokumenta] FOREIGN KEY([StatusDokumenta])
REFERENCES [dbo].[STATUSDOKUMENTA] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PASOS] CHECK CONSTRAINT [FK_Pasos_StatusDokumenta]
GO
ALTER TABLE [dbo].[TERMIN]  WITH CHECK ADD  CONSTRAINT [FK_Termin_Pasos] FOREIGN KEY([BrojPasosa])
REFERENCES [dbo].[PASOS] ([BrojPasosa])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TERMIN] CHECK CONSTRAINT [FK_Termin_Pasos]
GO
ALTER TABLE [dbo].[ZAHTEVZAIZDAVANJEPASOSA]  WITH CHECK ADD  CONSTRAINT [FK_Zahtev_MestoPU] FOREIGN KEY([PTTMestaPU])
REFERENCES [dbo].[MESTO] ([PTT])
GO
ALTER TABLE [dbo].[ZAHTEVZAIZDAVANJEPASOSA] CHECK CONSTRAINT [FK_Zahtev_MestoPU]
GO
ALTER TABLE [dbo].[ZAHTEVZAIZDAVANJEPASOSA]  WITH CHECK ADD  CONSTRAINT [FK_Zahtev_Status] FOREIGN KEY([Status])
REFERENCES [dbo].[TIPSTATUSA] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ZAHTEVZAIZDAVANJEPASOSA] CHECK CONSTRAINT [FK_Zahtev_Status]
GO
ALTER TABLE [dbo].[ZAHTEVZAIZDAVANJEPASOSA]  WITH CHECK ADD  CONSTRAINT [FK_ZahtevKorisnik] FOREIGN KEY([JMBGKorisnika])
REFERENCES [dbo].[KORISNIK] ([JMBG])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ZAHTEVZAIZDAVANJEPASOSA] CHECK CONSTRAINT [FK_ZahtevKorisnik]
GO
/****** Object:  StoredProcedure [dbo].[AžurirajStatusDokumentaPasosa]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AžurirajStatusDokumentaPasosa](
    @BrojPasosa [int],
    @Status [int]
)
AS
BEGIN

    UPDATE PASOS
    SET StatusDokumenta = @Status
    WHERE BrojPasosa = @BrojPasosa;

END
GO
/****** Object:  StoredProcedure [dbo].[DajKorisnikaPoJMBG]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DajKorisnikaPoJMBG]
    @JMBG nvarchar(13)
AS
BEGIN
    SELECT * FROM KorisnikView WHERE JMBG=@JMBG
END
GO
/****** Object:  StoredProcedure [dbo].[DajKorisnikaPoPrezimenu]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DajKorisnikaPoPrezimenu]
    @Prezime nvarchar(40)
AS
BEGIN
    SELECT * FROM KorisnikView WHERE Prezime LIKE '%' + @Prezime + '%'
END
GO
/****** Object:  StoredProcedure [dbo].[DajPasosIzPogleda]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DajPasosIzPogleda]
AS
BEGIN
    SELECT * FROM PasosView;
END
GO
/****** Object:  StoredProcedure [dbo].[DajPasosPoDatumom]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DajPasosPoDatumom](
    @IzabraniDatum DATE
)
AS
BEGIN
    SELECT *
    FROM TerminView
    WHERE Datum = @IzabraniDatum;
END
GO
/****** Object:  StoredProcedure [dbo].[DajPasosPoJMBG]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DajPasosPoJMBG](
    @JMBGKorisnika NVARCHAR(13)
)
AS
BEGIN
    SELECT *
    FROM PasosView
    WHERE JMBGKorisnika = @JMBGKorisnika;
END
GO
/****** Object:  StoredProcedure [dbo].[DajPasosPoPrezimenu]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DajPasosPoPrezimenu](
    @Prezime NVARCHAR(40)
)
AS
BEGIN
    SELECT *
    FROM PasosView
    WHERE PrezimeKorisnika = @Prezime;
END
GO
/****** Object:  StoredProcedure [dbo].[DajSveKorisnike]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DajSveKorisnike]
AS
BEGIN
    SELECT * FROM KorisnikView
END
GO
/****** Object:  StoredProcedure [dbo].[DajSveTermine]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DajSveTermine]
AS
BEGIN
    SELECT *
    FROM TerminView;
END
GO
/****** Object:  StoredProcedure [dbo].[DajSveZahtevePoJMBG]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DajSveZahtevePoJMBG]
(
    @JMBGKorisnika  nvarchar(13)
)
AS
BEGIN
    SELECT * FROM ZAHTEVZAIZDAVANJEPASOSAVIEW
    WHERE JMBGKorisnika = @JMBGKorisnika
END
GO
/****** Object:  StoredProcedure [dbo].[DajSveZahtevePoStatusu]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DajSveZahtevePoStatusu]
(
    @Status [int]
)
AS
BEGIN
    SELECT * FROM ZAHTEVZAIZDAVANJEPASOSAVIEW
    WHERE StatusID = @Status
END
GO
/****** Object:  StoredProcedure [dbo].[DajSveZahteveSaStatusom]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DajSveZahteveSaStatusom]
AS
BEGIN
    SELECT * FROM ZAHTEVZAIZDAVANJEPASOSAVIEW
END
GO
/****** Object:  StoredProcedure [dbo].[DajTerminPoID]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[DajTerminPoID](
@IDTermina int
)
AS 
Select * from TERMINView  where IDTermina = @IDTermina
GO
/****** Object:  StoredProcedure [dbo].[DodajDatumePasosu]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[DodajDatumePasosu](
	@DatumIsteka date,
	@DatumPocetkaVazenja date)
AS
	begin	
	update PASOS set DatumIsteka=@DatumIsteka, DatumPocetkaVazenja=@DatumPocetkaVazenja
	end
GO
/****** Object:  StoredProcedure [dbo].[DrzavaIzlistaj]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DrzavaIzlistaj]
AS
BEGIN
    SELECT * FROM DRZAVA
END
GO
/****** Object:  StoredProcedure [dbo].[IzlistajDatumeTermina]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IzlistajDatumeTermina]
AS
BEGIN
    SELECT DISTINCT Datum
    FROM TerminView;
END
GO
/****** Object:  StoredProcedure [dbo].[IzmeniKorisnika]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[IzmeniKorisnika]
    @StariJMBG nvarchar(13),
    @JMBG nvarchar(13),
    @Ime nvarchar(20),
    @Prezime nvarchar(40),
    @DatumRodjenja date,
    @Adresa nvarchar(40),
    @Telefon nvarchar(15),
    @PTTMesto int,
    @IDDrzavaRodjenja nvarchar(3),
    @IDDrzavaDrzavljanstva nvarchar(3),
    @PolKorisnika int,
    @Email nvarchar(40),
    @Lozinka nvarchar(20)
AS
BEGIN
    UPDATE KORISNIK
    SET JMBG = @JMBG,
        Ime = @Ime,
        Prezime = @Prezime,
        DatumRodjenja = @DatumRodjenja,
        Adresa = @Adresa,
        Telefon = @Telefon,
        PTTMesto = @PTTMesto,
        IDDrzavaRodjenja = @IDDrzavaRodjenja,
        IDDrzavaDrzavljanstva = @IDDrzavaDrzavljanstva,
        PolKorisnika = @PolKorisnika,
        Email = @Email,
        Lozinka = @Lozinka
    WHERE JMBG = @StariJMBG
END
GO
/****** Object:  StoredProcedure [dbo].[IzmeniPasos]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[IzmeniPasos](
@BrojPasosa int,
@IDDrzavaRodjenja nvarchar(3),
@IDDrzavaDrzavljanstva nvarchar(3),
@StatusDokumentaID int
)
AS
update PASOS set IDDrzavaDrzavljanstva = @IDDrzavaDrzavljanstva, IDDrzavaRodjenja = @IDDrzavaRodjenja, StatusDokumenta = @StatusDokumentaID where BrojPasosa = @BrojPasosa
GO
/****** Object:  StoredProcedure [dbo].[IzmeniTermin]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[IzmeniTermin](
@IDTermina int,
@Datum date,
@Vreme time
)
AS 
Update TERMIN set Datum = @Datum, Vreme = @Vreme where IDTermina = @IDTermina
GO
/****** Object:  StoredProcedure [dbo].[IzmeniTipKorisnika]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[IzmeniTipKorisnika]
    @JMBG nvarchar(13),
    @TipKorisnika int
AS
BEGIN
    UPDATE KORISNIK
    SET TipKorisnika = @TipKorisnika
    WHERE JMBG = @JMBG
END
GO
/****** Object:  StoredProcedure [dbo].[IzmeniZahtev]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IzmeniZahtev]
	@IDZahteva int,
    @OcekivaniDatum DATE,
    @MestoPU int,
    @Status INT
AS
BEGIN
    UPDATE ZAHTEVZAIZDAVANJEPASOSA
    SET
        OcekivaniDatumIzdavanja = @OcekivaniDatum,
        PTTMestaPU = @MestoPU,
        Status = @Status
    WHERE
        IDZahteva = @IDZahteva; 
END
GO
/****** Object:  StoredProcedure [dbo].[MestoIzlistaj]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[MestoIzlistaj]
AS
BEGIN
    SELECT * FROM MESTO
END
GO
/****** Object:  StoredProcedure [dbo].[NoviKorisnik]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NoviKorisnik]
( 
    @JMBG nvarchar(13),
    @Ime nvarchar(20),
    @Prezime nvarchar(40),
    @DatumRodjenja date,
    @Adresa nvarchar(40),
    @Telefon nvarchar(15),
    @PTTMesto int,
    @IDDrzavaRodjenja nvarchar(3),
    @IDDrzavaDrzavljanstva nvarchar(3),
    @KorisnickoIme nvarchar(20),
    @PolKorisnika int,
    @Email nvarchar(40),
    @Lozinka nvarchar(20),
    @TipKorisnika int
)
AS
BEGIN
    INSERT INTO KORISNIK (JMBG, Ime, Prezime, DatumRodjenja, Adresa, Telefon, PTTMesto, IDDrzavaRodjenja, IDDrzavaDrzavljanstva, KorisnickoIme, PolKorisnika, Email, Lozinka, TipKorisnika)
    VALUES (@JMBG, @Ime, @Prezime, @DatumRodjenja, @Adresa, @Telefon, @PTTMesto, @IDDrzavaRodjenja, @IDDrzavaDrzavljanstva, @KorisnickoIme, @PolKorisnika, @Email, @Lozinka, @TipKorisnika)
END
GO
/****** Object:  StoredProcedure [dbo].[NoviPasosITermin]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NoviPasosITermin]
(
    @BrojPasosa [int],
    @Datum [date],
    @Vreme [time],
    @JMBGKorisnika  [nvarchar](13),
    @PTTMesto [int]
)
AS
BEGIN
    BEGIN TRANSACTION;

    INSERT INTO PASOS (
        BrojPasosa,
        JMBGKorisnika,
        Ime,
        Prezime,
        PTTMesto,
        IDDrzavaRodjenja,
        IDDrzavaDrzavljanstva,
        PTTMestaPU,
        StatusDokumenta
    )
    VALUES (
        @BrojPasosa,
        @JMBGKorisnika,
        (SELECT Ime FROM KORISNIK WHERE JMBG = @JMBGKorisnika),
        (SELECT Prezime FROM KORISNIK WHERE JMBG = @JMBGKorisnika),
        @PTTMesto,
        (SELECT IDDrzavaRodjenja FROM KORISNIK WHERE JMBG = @JMBGKorisnika),
        (SELECT IDDrzavaDrzavljanstva FROM KORISNIK WHERE JMBG = @JMBGKorisnika),
        (SELECT PTTMestaPU FROM ZAHTEVZAIZDAVANJEPASOSA WHERE JMBGKorisnika = @JMBGKorisnika),
        1
    );

    -- Dodavanje novog termina
    INSERT INTO TERMIN (Datum, Vreme, BrojPasosa)
    VALUES (@Datum, @Vreme, @BrojPasosa);

    COMMIT;
END
GO
/****** Object:  StoredProcedure [dbo].[NoviZahtev]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[NoviZahtev]
(
    @DatumIVremeZahteva [datetime],
    @JMBGKorisnika [nvarchar](13) ,
    @OcekivaniDatumIzdavanja [date],
    @PTTMestaPU [int]
)
AS
BEGIN
    INSERT INTO ZAHTEVZAIZDAVANJEPASOSA (DatumiVremeZahteva, JMBGKorisnika, OcekivaniDatumIzdavanja, PTTMestaPU, Status)
    VALUES (@DatumIVremeZahteva, @JMBGKorisnika, @OcekivaniDatumIzdavanja, @PTTMestaPU, 2)
END
GO
/****** Object:  StoredProcedure [dbo].[ObrisiKorisnika]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ObrisiKorisnika]
    @JMBG nvarchar(13)
AS
BEGIN
    DELETE FROM KORISNIK WHERE JMBG = @JMBG
END
GO
/****** Object:  StoredProcedure [dbo].[ObrisiPasosITermin]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ObrisiPasosITermin](
    @IDTermina [int]
)
AS
BEGIN
    BEGIN TRANSACTION

    -- Brisanje pasoša
    DELETE FROM PASOS WHERE BrojPasosa = (SELECT BrojPasosa FROM TERMIN WHERE IDTermina = @IDTermina);

    -- Brisanje termina
    DELETE FROM TERMIN WHERE IDTermina = @IDTermina;

    COMMIT
END
GO
/****** Object:  StoredProcedure [dbo].[ObrisiZahtev]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ObrisiZahtev]
(
    @IDZahteva [int]
)
AS
BEGIN
    DELETE FROM ZAHTEVZAIZDAVANJEPASOSA
    WHERE IDZahteva = @IDZahteva
END
GO
/****** Object:  StoredProcedure [dbo].[OdbijZahtev]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OdbijZahtev]
(
    @IDZahteva [int]
)
AS
BEGIN
    UPDATE ZAHTEVZAIZDAVANJEPASOSA
    SET Status = 1
    WHERE IDZahteva = @IDZahteva
END
GO
/****** Object:  StoredProcedure [dbo].[OdobriZahtev]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OdobriZahtev]
(
    @IDZahteva [int]
)
AS
BEGIN
    UPDATE ZAHTEVZAIZDAVANJEPASOSA
    SET Status = 3
    WHERE IDZahteva = @IDZahteva
END
GO
/****** Object:  StoredProcedure [dbo].[PolKorisnikaIzlistaj]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PolKorisnikaIzlistaj]
AS
BEGIN
    SELECT * FROM POLKORISNIKA
END
GO
/****** Object:  StoredProcedure [dbo].[PronadjiKorisnikaPoEmailu]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PronadjiKorisnikaPoEmailu]
    @Email nvarchar(40)
AS
BEGIN
    SELECT * FROM KorisnikView WHERE Email = @Email
END
GO
/****** Object:  StoredProcedure [dbo].[StatusDokumentaIzlistaj]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[StatusDokumentaIzlistaj]
AS
BEGIN
    SELECT * FROM STATUSDOKUMENTA
END
GO
/****** Object:  StoredProcedure [dbo].[TipKorisnikaIzlistaj]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TipKorisnikaIzlistaj]
AS
BEGIN
    SELECT * FROM TIPKORISNIKA
END
GO
/****** Object:  StoredProcedure [dbo].[TipStatusaIzlistaj]    Script Date: 9/19/2024 3:31:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TipStatusaIzlistaj]
AS
BEGIN
    SELECT * FROM TIPSTATUSA
END
GO
USE [master]
GO
ALTER DATABASE [izdavanjePasosa] SET  READ_WRITE 
GO
