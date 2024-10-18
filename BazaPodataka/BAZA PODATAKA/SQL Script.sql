USE master
GO

CREATE DATABASE izdavanjePasosa 
GO

USE izdavanjePasosa
GO

CREATE TABLE KORISNIK(
    JMBG nvarchar(13) NOT NULL PRIMARY KEY,
    Ime nvarchar(20) NOT NULL,
    Prezime nvarchar(40) NOT NULL,
    DatumRodjenja date NOT NULL,
    Adresa nvarchar(40) NOT NULL,
    Telefon nvarchar(15) NOT NULL,
    PTTMesto int NOT NULL,
    IDDrzavaRodjenja nvarchar(3) NOT NULL,
    IDDrzavaDrzavljanstva nvarchar(3) NOT NULL,
    KorisnickoIme nvarchar(20) NOT NULL,
    PolKorisnika int NOT NULL,
    Email nvarchar(40) NOT NULL,
    Lozinka nvarchar(20) NOT NULL,
    TipKorisnika int NOT NULL
)
GO

CREATE TABLE PASOS(
    BrojPasosa int PRIMARY KEY,
    JMBGKorisnika nvarchar(13) NOT NULL,
    Ime nvarchar(20) NOT NULL,
    Prezime nvarchar(40) NOT NULL,
    PTTMesto int NOT NULL,
    IDDrzavaRodjenja nvarchar(3) NOT NULL,
    IDDrzavaDrzavljanstva nvarchar(3) NOT NULL,
    PTTMestaPU int NOT NULL,
    DatumPocetkaVazenja date NOT NULL,
    DatumIsteka date NOT NULL,
    StatusDokumenta int NOT NULL
)
GO

CREATE TABLE TERMIN(
    IDTermina int IDENTITY(1,1) PRIMARY KEY,
    BrojPasosa int NOT NULL,
    Datum date NOT NULL,
    Vreme time NOT NULL
)
GO

CREATE TABLE ZAHTEVZAIZDAVANJEPASOSA(
    IDZahteva int IDENTITY(1,1) PRIMARY KEY,
    DatumiVremeZahteva datetime NOT NULL,
    JMBGKorisnika nvarchar(13) NOT NULL,
    OcekivaniDatumIzdavanja date NOT NULL,
    PTTMestaPU int NOT NULL,
    Status int NOT NULL
)
GO

CREATE TABLE TIPKORISNIKA(
    ID int IDENTITY(1,1) PRIMARY KEY,
    Opis nvarchar(20) NOT NULL
)
GO

CREATE TABLE TIPSTATUSA(
    ID int IDENTITY(1,1) PRIMARY KEY,
    Opis nvarchar(20) NOT NULL
)
GO

CREATE TABLE MESTO(
    PTT int PRIMARY KEY,
    Naziv nvarchar(40) NOT NULL,
    SifraDrzave nvarchar(3) 
)
GO

CREATE TABLE DRZAVA(
    SifraDrzave nvarchar(3) PRIMARY KEY,
    Naziv nvarchar(40) NOT NULL
)
GO

CREATE TABLE STATUSDOKUMENTA(
    ID int IDENTITY(1,1) PRIMARY KEY,
    Opis nvarchar(20) NOT NULL
)
GO

CREATE TABLE POLKORISNIKA(
    ID int IDENTITY(1,1) PRIMARY KEY,
    Opis nvarchar(20) NOT NULL
)
GO

ALTER TABLE KORISNIK
ADD CONSTRAINT UQ_KorisnickoIme
    UNIQUE (KorisnickoIme);

ALTER TABLE KORISNIK
ADD CONSTRAINT UQ_Email
    UNIQUE (Email);

ALTER TABLE KORISNIK
ADD CONSTRAINT FK_Korisnik_Mesto
    FOREIGN KEY (PTTMesto) REFERENCES MESTO(PTT)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

ALTER TABLE KORISNIK
ADD CONSTRAINT FK_Korisnik_DrzavaRodjenja
    FOREIGN KEY (IDDrzavaRodjenja) REFERENCES DRZAVA(SifraDrzave)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

ALTER TABLE KORISNIK
ADD CONSTRAINT FK_Korisnik_DrzavaDrzavljanstva
    FOREIGN KEY (IDDrzavaDrzavljanstva) REFERENCES DRZAVA(SifraDrzave)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION;

ALTER TABLE KORISNIK
ADD CONSTRAINT FK_Korisnik_Pol
    FOREIGN KEY (PolKorisnika) REFERENCES POLKORISNIKA(ID)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

ALTER TABLE KORISNIK
ADD CONSTRAINT FK_Korisnik_TipKorisnika
    FOREIGN KEY (TipKorisnika) REFERENCES TIPKORISNIKA(ID)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

ALTER TABLE PASOS
ADD CONSTRAINT FK_Pasos_Korisnik
    FOREIGN KEY (JMBGKorisnika) REFERENCES KORISNIK(JMBG)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

ALTER TABLE PASOS
ADD CONSTRAINT FK_Pasos_StatusDokumenta
    FOREIGN KEY (StatusDokumenta) REFERENCES StatusDokumenta(ID)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

ALTER TABLE PASOS
ADD CONSTRAINT FK_Pasos_Mesto
    FOREIGN KEY (PTTMesto) REFERENCES MESTO(PTT)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION;

ALTER TABLE PASOS
ADD CONSTRAINT FK_Pasos_DrzavaRodjenja
    FOREIGN KEY (IDDrzavaRodjenja) REFERENCES DRZAVA(SifraDrzave)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION;

ALTER TABLE PASOS
ADD CONSTRAINT FK_Pasos_DrzavaDrzavljanstva
    FOREIGN KEY (IDDrzavaDrzavljanstva) REFERENCES DRZAVA(SifraDrzave)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION;

ALTER TABLE PASOS
ADD CONSTRAINT FK_Pasos_MestoPU
    FOREIGN KEY (PTTMestaPU) REFERENCES MESTO(PTT)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION;

ALTER TABLE TERMIN
ADD CONSTRAINT FK_Termin_Pasos
    FOREIGN KEY (BrojPasosa) REFERENCES PASOS(BrojPasosa)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

ALTER TABLE ZAHTEVZAIZDAVANJEPASOSA
ADD CONSTRAINT FK_ZahtevKorisnik
    FOREIGN KEY (JMBGKorisnika) REFERENCES KORISNIK(JMBG)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

ALTER TABLE ZAHTEVZAIZDAVANJEPASOSA
ADD CONSTRAINT FK_Zahtev_MestoPU
    FOREIGN KEY (PTTMestaPU) REFERENCES MESTO(PTT)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION;

ALTER TABLE ZAHTEVZAIZDAVANJEPASOSA
ADD CONSTRAINT FK_Zahtev_Status
    FOREIGN KEY (Status) REFERENCES TIPSTATUSA(ID)
    ON DELETE CASCADE
    ON UPDATE CASCADE;
	GO

CREATE VIEW KorisnikView AS
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

CREATE VIEW PasosView AS
SELECT 
    P.BrojPasosa,
	P.StatusDokumenta,
	S.Opis,
    K.JMBG AS JMBGKorisnika,
    K.Ime AS ImeKorisnika,
    K.Prezime AS PrezimeKorisnika,
    M.PTT AS PTTMestoID,
    M.Naziv AS Mesto,
    DR1.SifraDrzave AS IDDrzavaRodjenja,
    DR1.Naziv AS DrzavaRodjenja,
    DR2.SifraDrzave AS IDDrzavaDrzavljanstva,
    DR2.Naziv AS DrzavaDrzavljanstva,
    MP.PTT AS PTTMestoPU,
    MP.Naziv AS MestoPU,
    P.DatumPocetkaVazenja,
    P.DatumIsteka
FROM 
    dbo.PASOS P
    JOIN dbo.KORISNIK K ON P.JMBGKorisnika = K.JMBG
    JOIN dbo.MESTO M ON P.PTTMesto = M.PTT
    JOIN dbo.DRZAVA DR1 ON P.IDDrzavaRodjenja = DR1.SifraDrzave
    JOIN dbo.DRZAVA DR2 ON P.IDDrzavaDrzavljanstva = DR2.SifraDrzave
    JOIN dbo.MESTO MP ON P.PTTMestaPU = MP.PTT
	JOIN dbo.StatusDokumenta S ON P.StatusDokumenta = S.ID;
	GO

	CREATE VIEW TerminView AS
SELECT 
    T.IDTermina,
    P.BrojPasosa,
    K.Ime AS ImeKorisnika,
    K.Prezime AS PrezimeKorisnika,
    T.Datum,
    T.Vreme
FROM 
    dbo.TERMIN T
    JOIN dbo.PASOS P ON T.BrojPasosa = P.BrojPasosa
    JOIN dbo.KORISNIK K ON P.JMBGKorisnika = K.JMBG;
GO
CREATE VIEW ZahtevZaIzdavanjePasosaView AS
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
INSERT INTO TIPKORISNIKA (Opis) VALUES ('admin');
INSERT INTO TIPKORISNIKA (Opis) VALUES ('sluzbenik');
INSERT INTO TIPKORISNIKA (Opis) VALUES ('gradjanin');

INSERT INTO TIPSTATUSA (Opis) VALUES ('odbijen');
INSERT INTO TIPSTATUSA (Opis) VALUES ('na_cekanju');
INSERT INTO TIPSTATUSA (Opis) VALUES ('odobren');

INSERT INTO [dbo].[DRZAVA] (SifraDrzave, Naziv)
VALUES 
('SRB', 'Srbija'),
('USA', 'Sjedinjene Američke Države'),
('CAN', 'Kanada'),
('DEU', 'Nemačka'),
('FRA', 'Francuska'),
('GBR', 'Ujedinjeno Kraljevstvo'),
('ITA', 'Italija'),
('CHN', 'Kina'),
('JPN', 'Japan'),
('AUS', 'Australija');

INSERT INTO [dbo].[MESTO] (PTT, Naziv, SifraDrzave)
VALUES 
(11000, 'Beograd', 'SRB'),
(21000, 'Novi Sad', 'SRB'),
(34000, 'Kragujevac', 'SRB'),
(18000, 'Niš', 'SRB'),
(25000, 'Sombor', 'SRB'),
(31000, 'Užice', 'SRB'),
(24000, 'Subotica','SRB'),
(15000, 'Šabac', 'SRB'),
(22000, 'Sremska Mitrovica', 'SRB'),
(23000, 'Zrenjanin', 'SRB');

INSERT INTO STATUSDOKUMENTA (Opis) VALUES ('zaprimljen');
INSERT INTO STATUSDOKUMENTA (Opis) VALUES ('odbijen');
INSERT INTO STATUSDOKUMENTA (Opis) VALUES ('u obradi');
INSERT INTO STATUSDOKUMENTA (Opis) VALUES ('gotov za izdavanje');
INSERT INTO STATUSDOKUMENTA (Opis) VALUES ('izdat');

INSERT INTO POLKORISNIKA (Opis) VALUES ('Muski');
INSERT INTO POLKORISNIKA (Opis) VALUES ('Zenski');

INSERT INTO KORISNIK (
    JMBG,
    Ime,
    Prezime,
    DatumRodjenja,
    Adresa,
    Telefon,
    PTTMesto,
    IDDrzavaRodjenja,
    IDDrzavaDrzavljanstva,
    KorisnickoIme,
    PolKorisnika,
    Email,
    Lozinka,
    TipKorisnika
) VALUES (
    '1234567890123',
    'Admin',
    'Admin',
    '2001-11-25',
    'Glavna 1',
    '123456789',
    23000,
    'SRB', 
    'SRB', 
    'admin',
    1,
    'admin@izdavanjepasosa.com',
    'lozinka',
    1
);

GO
CREATE PROCEDURE NoviKorisnik
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

CREATE PROCEDURE IzmeniKorisnika
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
    @KorisnickoIme nvarchar(20),
    @PolKorisnika int,
    @Email nvarchar(40),
    @Lozinka nvarchar(20),
    @TipKorisnika int
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
        KorisnickoIme = @KorisnickoIme,
        PolKorisnika = @PolKorisnika,
        Email = @Email,
        Lozinka = @Lozinka,
        TipKorisnika = @TipKorisnika
    WHERE JMBG = @StariJMBG
END
GO


CREATE PROCEDURE ObrisiKorisnika
    @JMBG nvarchar(13)
AS
BEGIN
    DELETE FROM KORISNIK WHERE JMBG = @JMBG
END
GO

CREATE PROCEDURE IzmeniTipKorisnika
    @JMBG nvarchar(13),
    @TipKorisnika int
AS
BEGIN
    UPDATE KORISNIK
    SET TipKorisnika = @TipKorisnika
    WHERE JMBG = @JMBG
END
GO

CREATE PROCEDURE DajSveKorisnike
AS
BEGIN
    SELECT * FROM KorisnikView
END
GO

CREATE PROCEDURE DajKorisnikaPoPrezimenu
    @Prezime nvarchar(40)
AS
BEGIN
    SELECT * FROM KorisnikView WHERE Prezime LIKE '%' + @Prezime + '%'
END
GO

CREATE PROCEDURE DajKorisnikaPoJMBG
    @JMBG nvarchar(13)
AS
BEGIN
    SELECT * FROM KorisnikView WHERE JMBG=@JMBG
END
GO

CREATE PROCEDURE PronadjiKorisnikaPoEmailu
    @Email nvarchar(40)
AS
BEGIN
    SELECT * FROM KorisnikView WHERE Email = @Email
END
GO

CREATE PROCEDURE TipKorisnikaIzlistaj
AS
BEGIN
    SELECT * FROM TIPKORISNIKA
END
GO

CREATE PROCEDURE TipStatusaIzlistaj
AS
BEGIN
    SELECT * FROM TIPSTATUSA
END
GO

CREATE PROCEDURE MestoIzlistaj
AS
BEGIN
    SELECT * FROM MESTO
END
GO

CREATE PROCEDURE DrzavaIzlistaj
AS
BEGIN
    SELECT * FROM DRZAVA
END
GO

CREATE PROCEDURE StatusDokumentaIzlistaj
AS
BEGIN
    SELECT * FROM STATUSDOKUMENTA
END
GO

CREATE PROCEDURE PolKorisnikaIzlistaj
AS
BEGIN
    SELECT * FROM POLKORISNIKA
END
GO
CREATE PROCEDURE [DajPasosIzPogleda]
AS
BEGIN
    SELECT * FROM PasosView;
END
GO

CREATE PROCEDURE [DajPasosPoDatumom](
    @IzabraniDatum DATE
)
AS
BEGIN
    SELECT *
    FROM TerminView
    WHERE Datum = @IzabraniDatum;
END
GO

CREATE PROCEDURE [DajPasosPoJMBG](
    @JMBGKorisnika NVARCHAR(13)
)
AS
BEGIN
    SELECT *
    FROM PasosView
    WHERE JMBGKorisnika = @JMBGKorisnika;
END
GO

CREATE PROCEDURE [DajPasosPoPrezimenu](
    @Prezime NVARCHAR(40)
)
AS
BEGIN
    SELECT *
    FROM PasosView
    WHERE PrezimeKorisnika = @Prezime;
END
GO

CREATE PROCEDURE [AžurirajStatusDokumentaPasosa](
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
CREATE PROCEDURE IzlistajDatumeTermina
AS
BEGIN
    SELECT DISTINCT Datum
    FROM TerminView;
END
GO

CREATE PROCEDURE DajSveTermine
AS
BEGIN
    SELECT [IDTermina], [Datum], [Vreme]
    FROM TerminView;
END
GO
CREATE PROCEDURE NoviZahtev
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

CREATE PROCEDURE ObrisiZahtev
(
    @IDZahteva [int]
)
AS
BEGIN
    DELETE FROM ZAHTEVZAIZDAVANJEPASOSA
    WHERE IDZahteva = @IDZahteva
END
GO

CREATE PROCEDURE OdbijZahtev
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

CREATE PROCEDURE OdobriZahtev
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

CREATE PROCEDURE DajSveZahteveSaStatusom
AS
BEGIN
    SELECT * FROM ZAHTEVZAIZDAVANJEPASOSAVIEW
END
GO

CREATE PROCEDURE DajSveZahtevePoStatusu
(
    @Status [int]
)
AS
BEGIN
    SELECT * FROM ZAHTEVZAIZDAVANJEPASOSAVIEW
    WHERE Status = @Status
END
GO

CREATE PROCEDURE DajSveZahtevePoJMBG
(
    @JMBGKorisnika  nvarchar(3)
)
AS
BEGIN
    SELECT * FROM ZAHTEVZAIZDAVANJEPASOSAVIEW
    WHERE JMBGKorisnika = @JMBGKorisnika
END
GO
CREATE PROCEDURE [NoviPasosITermin](
    @BrojPasosa [int],
    @Datum [date],
    @Vreme [time],
    @JMBGKorisnika [nvarchar](13) ,
    @DatumIsteka [date],
    @PTTMesto [int]
)
AS
BEGIN
    BEGIN TRANSACTION

    DECLARE @TerminID INT;

    -- Dodavanje novog termina
    INSERT INTO TERMIN (Datum, Vreme, BrojPasosa)
    VALUES (@Datum, @Vreme, @BrojPasosa);

    SET @TerminID = SCOPE_IDENTITY();

    -- Dodavanje novog pasoša
    INSERT INTO PASOS (BrojPasosa, JMBGKorisnika, Ime, Prezime, PTTMesto, IDDrzavaRodjenja, IDDrzavaDrzavljanstva, PTTMestaPU, DatumPocetkaVazenja, DatumIsteka)
    VALUES (
	@BrojPasosa,
        @JMBGKorisnika,
        (SELECT Ime FROM KORISNIK WHERE JMBG = @JMBGKorisnika),
        (SELECT Prezime FROM KORISNIK WHERE JMBG = @JMBGKorisnika),
        @PTTMesto,
        (SELECT IDDrzavaRodjenja FROM KORISNIK WHERE JMBG = @JMBGKorisnika),
        (SELECT IDDrzavaDrzavljanstva FROM KORISNIK WHERE JMBG = @JMBGKorisnika),
        (SELECT PTTMestaPU FROM ZAHTEVZAIZDAVANJEPASOSA WHERE JMBGKorisnika = @JMBGKorisnika),
        @Datum,
        @DatumIsteka
    );

    COMMIT
END
GO

CREATE PROCEDURE [ObrisiPasosITermin](
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
