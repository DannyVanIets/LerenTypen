-- phpMyAdmin SQL Dump
-- version 4.8.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Gegenereerd op: 18 nov 2019 om 12:25
-- Serverversie: 10.1.37-MariaDB
-- PHP-versie: 7.3.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `quicklylearningtyping`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `accounts`
--

CREATE TABLE `accounts` (
  `accountID` int(11) NOT NULL,
  `accountType` smallint(1) NOT NULL COMMENT '0 = student, 1  = docent, 2 = administrator',
  `accountUsername` text NOT NULL,
  `accountPassword` text NOT NULL COMMENT 'Wordt nog encryptie voor bedacht',
  `accountBirthdate` date NOT NULL,
  `accountFirstName` text NOT NULL,
  `accountSurname` text NOT NULL,
  `accountSecurityQuestion` text NOT NULL,
  `accountSecurityAnswer` text NOT NULL,
  `archived` tinyint(1) NOT NULL DEFAULT '0' COMMENT '0 = false, niet gearchiveerd, 1 = true, gearchiveerd'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `accounts`
--

INSERT INTO `accounts` (`accountID`, `accountType`, `accountUsername`, `accountPassword`, `accountBirthdate`, `accountFirstName`, `accountSurname`, `accountSecurityQuestion`, `accountSecurityAnswer`, `archived`) VALUES
(1, 0, 'Danny van Iets', 'paddenstoel09', '1998-04-09', 'Danny', 'van Iets', 'Wat is de naam van je favoriete huisdier?', 'Paco', 0),
(2, 1, 'EJ Voorhoeve', 'items03', '1995-05-12', 'Eltjo', 'Voorhoeve', 'Wat is de naam van je oma?', 'Margriet', 0),
(3, 2, 'HenkerDenker', 'denkeffena1990', '1990-12-20', 'Henk', 'Denker', 'Wat is je favoriete datum?', '2001-10-30', 0);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `testresults`
--

CREATE TABLE `testresults` (
  `testResultID` int(11) NOT NULL,
  `testID` int(11) NOT NULL COMMENT 'Klik op het nummer om naar die account toe te gaan.',
  `accountID` int(11) NOT NULL COMMENT 'Klik op het nummer om naar die toets toe te gaan.',
  `testWrongAnswers` enum('','','','') NOT NULL,
  `testResultsDate` date NOT NULL,
  `wordsEachMinute` int(11) NOT NULL,
  `numberOfErrors` int(11) NOT NULL,
  `pauses` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `testresults`
--

INSERT INTO `testresults` (`testResultID`, `testID`, `accountID`, `testWrongAnswers`, `testResultsDate`, `wordsEachMinute`, `numberOfErrors`, `pauses`) VALUES
(1, 1, 1, '', '2019-11-18', 10, 11, 0);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `tests`
--

CREATE TABLE `tests` (
  `testID` int(11) NOT NULL,
  `testName` text NOT NULL,
  `testType` tinyint(1) NOT NULL COMMENT '0 = woorden, 1 = zinnen',
  `testContent` enum('Klaas','Piet','Danny','Klaas-Jan') NOT NULL,
  `archived` tinyint(1) NOT NULL DEFAULT '0' COMMENT '0 = false, niet gearchiveerd, 1 = true, gearchiveerd'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `tests`
--

INSERT INTO `tests` (`testID`, `testName`, `testType`, `testContent`, `archived`) VALUES
(1, 'Leren oefenen met namen!', 0, 'Klaas', 0);

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `accounts`
--
ALTER TABLE `accounts`
  ADD PRIMARY KEY (`accountID`);

--
-- Indexen voor tabel `testresults`
--
ALTER TABLE `testresults`
  ADD PRIMARY KEY (`testResultID`),
  ADD KEY `testID_foreign_key` (`testID`),
  ADD KEY `accountID_foreign_key` (`accountID`);

--
-- Indexen voor tabel `tests`
--
ALTER TABLE `tests`
  ADD PRIMARY KEY (`testID`);

--
-- AUTO_INCREMENT voor geëxporteerde tabellen
--

--
-- AUTO_INCREMENT voor een tabel `accounts`
--
ALTER TABLE `accounts`
  MODIFY `accountID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT voor een tabel `testresults`
--
ALTER TABLE `testresults`
  MODIFY `testResultID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT voor een tabel `tests`
--
ALTER TABLE `tests`
  MODIFY `testID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Beperkingen voor geëxporteerde tabellen
--

--
-- Beperkingen voor tabel `testresults`
--
ALTER TABLE `testresults`
  ADD CONSTRAINT `accountID_foreign_key` FOREIGN KEY (`accountID`) REFERENCES `accounts` (`accountID`),
  ADD CONSTRAINT `testID_foreign_key` FOREIGN KEY (`testID`) REFERENCES `tests` (`testID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
