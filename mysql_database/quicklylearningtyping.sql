-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Gegenereerd op: 28 nov 2019 om 12:53
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
(1, 2, 'Danny van Iets', 'paddenstoel09', '1998-04-09', 'Dan', 'van Iets', 'Wat is de naam van je favoriete huisdier?', 'Paco', 0),
(2, 1, 'EJ Voorhoeve', 'items03', '1995-05-12', 'Eltjo', 'Voorhoeve', 'Wat is de naam van je oma?', 'Margriet', 0),
(3, 2, 'H', 'ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae', '1990-12-17', 'Hans', 'Pans', 'Wat is de naam van je moeder?', 'asldklasd', 0),
(4, 0, 't', 'e3b98a4da31a127d4bde6e43033f66ba274cab0eb7eb1c70ec41402bf6273dd8', '1980-01-01', 't', 't', 'Wat is je geboorteplaats?', 't', 0),
(5, 2, 'j', '189f40034be7a199f1fa9891668ee3ab6049f82d38c68be70f596eab2e1857b7', '1981-04-21', 'j', 'j', 'Wat is je geboorteplaats?', 'j', 0),
(6, 0, 'k', '8254c329a92850f6d539dd376f4816ee2764517da5e0235514af433164480d7a', '1980-01-22', 'k', 'k', 'Wat is je geboorteplaats?', 'k', 0),
(7, 0, 'hjjkhk', '189f40034be7a199f1fa9891668ee3ab6049f82d38c68be70f596eab2e1857b7', '1980-01-31', 'j', 'j', 'Wat is je geboorteplaats?', 'j', 0),
(8, 2, 'hjhkjhkh', '189f40034be7a199f1fa9891668ee3ab6049f82d38c68be70f596eab2e1857b7', '1980-01-30', 'j', 'j', 'Wat is je geboorteplaats?', 'hjhjkhkjhkhjj', 0);

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
(3, 1, 'Jan'),
(7, 31, 'asd asd asd asdas d as da'),
(8, 31, 'asddasdasd as d'),
(9, 32, 'asd asd asd asdas d as da'),
(10, 32, 'asddasdasd as d'),
(11, 32, 'asd asd asd asdas d as da'),
(12, 32, 'asddasdasd as d'),
(13, 33, 'asd asd asd asdas d as da'),
(14, 33, 'asddasdasd as d'),
(15, 33, 'asd asd asd asdas d as da'),
(16, 33, 'asddasdasd as d'),
(17, 33, 'asd asd asd asdas d as da '),
(18, 33, 'asddasdasd as d '),
(19, 35, 'HenkerDenker'),
(20, 36, 'moenymuney');

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

--
-- Gegevens worden geëxporteerd voor tabel `testresultcontent`
--

INSERT INTO `testresultcontent` (`testResultContentID`, `testResultID`, `answer`, `answerType`, `rightAnswer`) VALUES
(1, 2, 'Piet', 0, ''),
(2, 2, 'Klaas', 0, ''),
(3, 2, 'Jan', 0, ''),
(4, 3, 'Piet', 0, ''),
(5, 3, 'Klaas', 0, ''),
(6, 3, 'Jan', 0, ''),
(57, 20, 'Piet', 0, ''),
(58, 20, 'Jan', 0, ''),
(59, 20, 'Klaas', 0, ''),
(60, 20, 'aksdfja', 1, 'Klaas');

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
  `pauses` int(11) NOT NULL DEFAULT '0',
  `score` int(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `testresults`
--

INSERT INTO `testresults` (`testResultID`, `testID`, `accountID`, `testResultsDate`, `wordsEachMinute`, `pauses`, `score`) VALUES
(1, 1, 1, '2019-11-18', 10, 0, 100),
(2, 1, 1, '2019-11-27', 60, 0, 100),
(3, 1, 1, '2019-11-27', 90, 0, 100),
(20, 1, 1, '2019-11-28', 90, 0, 75);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `tests`
--

CREATE TABLE `tests` (
  `testID` int(11) NOT NULL,
  `accountID` int(11) NOT NULL COMMENT 'Dit is de persoon die de toets heeft gemaakt',
  `testName` text NOT NULL,
  `testType` tinyint(1) NOT NULL COMMENT '0 = woorden, 1 = zinnen',
  `testDifficulty` int(2) NOT NULL COMMENT '0 = makkelijk, 1 = middel, 2 = moeilijk',
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
(1, 1, 'Leren oefenen met namen!', 0, 0, '2019-11-26', 0, 0, 0, 0, 1),
(3, 3, 'Woef', 1, 2, '2019-11-27', 1, 0, 0, 0, 1),
(4, 3, 'Woef', 0, 0, '2019-11-27', 1, 0, 0, 0, 1),
(5, 3, 'Woef', 0, 0, '2019-11-27', 1, 0, 0, 0, 1),
(6, 3, 'Woef', 0, 0, '2019-11-27', 1, 0, 0, 0, 1),
(7, 3, 'Woef', 0, 0, '2019-11-27', 1, 0, 0, 0, 1),
(8, 3, 'Woef', 0, 0, '2019-11-27', 1, 0, 0, 0, 1),
(9, 3, 'ASDASD', 0, 0, '2019-11-27', 1, 0, 0, 0, 1),
(10, 3, 'ASDASD', 0, 0, '2019-11-27', 0, 0, 0, 0, 1),
(11, 3, 'ASDASD', 0, 0, '2019-11-27', 0, 0, 0, 0, 1),
(12, 3, 'ASDASD', 0, 0, '2019-11-27', 0, 0, 0, 0, 1),
(13, 3, 'ASDASD', 0, 0, '2019-11-27', 0, 0, 0, 0, 1),
(14, 3, 'ASDASD', 0, 0, '2019-11-27', 0, 0, 0, 0, 1),
(15, 3, 'ASDASD', 0, 0, '2019-11-27', 0, 0, 0, 0, 1),
(16, 3, 'ASDASD', 0, 0, '2019-11-27', 0, 0, 0, 0, 1),
(17, 3, 'ASDASD', 0, 0, '2019-11-27', 0, 0, 0, 0, 1),
(18, 3, 'ASDASD', 0, 0, '2019-11-27', 0, 0, 0, 0, 1),
(19, 3, 'ASDASD', 0, 0, '2019-11-27', 0, 0, 0, 0, 1),
(20, 3, 'ASDASD', 0, 0, '2019-11-27', 0, 0, 0, 0, 1),
(21, 3, 'ASDASD', 0, 0, '2019-11-27', 0, 0, 0, 0, 1),
(22, 3, 'ASDASD', 0, 0, '2019-11-27', 0, 0, 0, 0, 1),
(23, 3, 'ASDASD', 0, 0, '2019-11-27', 0, 0, 0, 0, 1),
(24, 3, 'ASDASD', 0, 0, '2019-11-27', 0, 0, 0, 0, 1),
(25, 3, 'ASDASD', 0, 0, '2019-11-27', 1, 0, 0, 0, 1),
(26, 3, 'asd', 0, 0, '2019-11-27', 1, 0, 0, 0, 1),
(27, 3, 'hjkhjhjsajhdjashdkkashdkjasjdhjkashdkjashdjkashdkj', 0, 0, '2019-11-27', 1, 0, 0, 0, 1),
(28, 3, 'hjkhjhjsajhdjashdkkashdkjasjdhjkashdkjashdjkashdkj', 0, 0, '2019-11-27', 1, 0, 0, 0, 1),
(29, 3, 'asdasd', 0, 0, '2019-11-27', 1, 0, 0, 0, 1),
(30, 3, 'asdasdasd', 1, 0, '2019-11-27', 0, 0, 0, 0, 1),
(31, 3, 'asdasdas ', 0, 0, '2019-11-27', 1, 0, 0, 0, 1),
(32, 3, 'asdasdas ', 0, 0, '2019-11-27', 0, 0, 0, 0, 1),
(33, 3, 'asdasdas ', 1, 0, '2019-11-27', 0, 0, 0, 0, 1),
(34, 3, 'asdasd', 1, 0, '2019-11-27', 1, 0, 0, 0, 1),
(35, 3, 'asdasdasd', 0, 0, '2019-11-27', 1, 0, 0, 0, 1),
(36, 5, 'Muney', 1, 2, '2019-11-28', 1, 0, 0, 0, 1);

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
  MODIFY `accountID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT voor een tabel `testcontent`
--
ALTER TABLE `testcontent`
  MODIFY `testContentID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT voor een tabel `testresultcontent`
--
ALTER TABLE `testresultcontent`
  MODIFY `testResultContentID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=61;

--
-- AUTO_INCREMENT voor een tabel `testresults`
--
ALTER TABLE `testresults`
  MODIFY `testResultID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT voor een tabel `tests`
--
ALTER TABLE `tests`
  MODIFY `testID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;

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
