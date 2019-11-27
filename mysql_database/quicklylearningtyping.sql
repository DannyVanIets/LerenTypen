-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Gegenereerd op: 27 nov 2019 om 11:06
-- Serverversie: 10.1.35-MariaDB
-- PHP-versie: 7.2.9

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
(3, 2, 'HenkerDenker', 'ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae', '1990-12-20', 'Henk', 'Denker', 'Wat is je favoriete datum?', '2001-10-30', 0);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `testcontent`
--

CREATE TABLE `testcontent` (
  `testContentID` int(11) NOT NULL,
  `testID` int(11) NOT NULL,
  `content` text NOT NULL COMMENT 'Hier komt het woord/zin in'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `testcontent`
--

INSERT INTO `testcontent` (`testContentID`, `testID`, `content`) VALUES
(1, 1, 'Piet'),
(2, 1, 'Klaas'),
(3, 1, 'Jan');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `testresultcontent`
--

CREATE TABLE `testresultcontent` (
  `testResultContentID` int(11) NOT NULL,
  `testResultID` int(11) NOT NULL,
  `answer` text NOT NULL,
  `answerType` tinyint(1) NOT NULL COMMENT '0 = goed antwoord, 1 = fout antwoord',
  `rightAnswer` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `testresults`
--

CREATE TABLE `testresults` (
  `testResultID` int(11) NOT NULL,
  `testID` int(11) NOT NULL COMMENT 'Klik op het nummer om naar die test toe te gaan.',
  `accountID` int(11) NOT NULL COMMENT 'Hier staat het persoon die het heeft geoefend',
  `testResultsDate` date NOT NULL,
  `wordsEachMinute` int(11) NOT NULL,
  `pauses` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `testresults`
--

INSERT INTO `testresults` (`testResultID`, `testID`, `accountID`, `testResultsDate`, `wordsEachMinute`, `pauses`) VALUES
(1, 1, 1, '2019-11-18', 10, 0);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `tests`
--

CREATE TABLE `tests` (
  `testID` int(11) NOT NULL,
  `accountID` int(11) NOT NULL COMMENT 'Dit is de persoon die de toets heeft gemaakt',
  `testName` text NOT NULL,
  `testType` tinyint(1) NOT NULL COMMENT '0 = woorden, 1 = zinnen',
  `testDifficulty` tinyint(1) NOT NULL COMMENT '0 = makkelijk, 1 = middel, 2 = moeilijk',
  `createDate` date NOT NULL,
  `isPrivate` tinyint(1) NOT NULL DEFAULT '0' COMMENT '0 = nee, publiek, 1 = ja, private',
  `archived` tinyint(1) NOT NULL DEFAULT '0' COMMENT '0 = false, niet gearchiveerd, 1 = true, gearchiveerd',
  `timesMade` int(11) DEFAULT '0',
  `highscore` int(11) DEFAULT '0',
  `version` int(11) DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `tests`
--

INSERT INTO `tests` (`testID`, `accountID`, `testName`, `testType`, `testDifficulty`, `createDate`, `isPrivate`, `archived`, `timesMade`, `highscore`, `version`) VALUES
(1, 1, 'Leren oefenen met namen!', 0, 0, '2019-11-26', 0, 0, 0, 0, 1);

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `accounts`
--
ALTER TABLE `accounts`
  ADD PRIMARY KEY (`accountID`);

--
-- Indexen voor tabel `testcontent`
--
ALTER TABLE `testcontent`
  ADD PRIMARY KEY (`testContentID`),
  ADD KEY `FK_testID_testContent` (`testID`);

--
-- Indexen voor tabel `testresultcontent`
--
ALTER TABLE `testresultcontent`
  ADD PRIMARY KEY (`testResultContentID`),
  ADD KEY `FK_testResultID_testResultContent` (`testResultID`);

--
-- Indexen voor tabel `testresults`
--
ALTER TABLE `testresults`
  ADD PRIMARY KEY (`testResultID`),
  ADD KEY `FK_testID_testResults` (`testID`) USING BTREE,
  ADD KEY `FK_accountID_testResults` (`accountID`) USING BTREE;

--
-- Indexen voor tabel `tests`
--
ALTER TABLE `tests`
  ADD PRIMARY KEY (`testID`),
  ADD KEY `FK_accountID_tests` (`accountID`);

--
-- AUTO_INCREMENT voor geëxporteerde tabellen
--

--
-- AUTO_INCREMENT voor een tabel `accounts`
--
ALTER TABLE `accounts`
  MODIFY `accountID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT voor een tabel `testcontent`
--
ALTER TABLE `testcontent`
  MODIFY `testContentID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT voor een tabel `testresultcontent`
--
ALTER TABLE `testresultcontent`
  MODIFY `testResultContentID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `testresults`
--
ALTER TABLE `testresults`
  MODIFY `testResultID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT voor een tabel `tests`
--
ALTER TABLE `tests`
  MODIFY `testID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Beperkingen voor geëxporteerde tabellen
--

--
-- Beperkingen voor tabel `testcontent`
--
ALTER TABLE `testcontent`
  ADD CONSTRAINT `FK_testID_testContent` FOREIGN KEY (`testID`) REFERENCES `tests` (`testID`);

--
-- Beperkingen voor tabel `testresultcontent`
--
ALTER TABLE `testresultcontent`
  ADD CONSTRAINT `FK_testResultID_testResultContent` FOREIGN KEY (`testResultID`) REFERENCES `testresults` (`testResultID`);

--
-- Beperkingen voor tabel `testresults`
--
ALTER TABLE `testresults`
  ADD CONSTRAINT `accountID_foreign_key` FOREIGN KEY (`accountID`) REFERENCES `accounts` (`accountID`),
  ADD CONSTRAINT `testID_foreign_key` FOREIGN KEY (`testID`) REFERENCES `tests` (`testID`);

--
-- Beperkingen voor tabel `tests`
--
ALTER TABLE `tests`
  ADD CONSTRAINT `FK_accountID_tests` FOREIGN KEY (`accountID`) REFERENCES `accounts` (`accountID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
