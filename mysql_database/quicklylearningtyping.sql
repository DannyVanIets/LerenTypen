-- phpMyAdmin SQL Dump
-- version 4.9.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 28, 2019 at 12:23 PM
-- Server version: 10.4.8-MariaDB
-- PHP Version: 7.3.11

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
-- Table structure for table `accounts`
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
  `archived` tinyint(1) NOT NULL DEFAULT 0 COMMENT '0 = false, niet gearchiveerd, 1 = true, gearchiveerd'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `accounts`
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
-- Table structure for table `testcontent`
--

CREATE TABLE `testcontent` (
  `testContentID` int(11) NOT NULL,
  `testID` int(11) NOT NULL,
  `content` text NOT NULL COMMENT 'Hier komt het woord/zin in'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `testcontent`
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
-- Table structure for table `testresultcontent`
--

CREATE TABLE `testresultcontent` (
  `testResultContentID` int(11) NOT NULL,
  `testResultID` int(11) NOT NULL,
  `answer` text NOT NULL,
  `answerType` tinyint(1) NOT NULL COMMENT '0 = goed antwoord, 1 = fout antwoord',
  `rightAnswer` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `testresultcontent`
--

INSERT INTO `testresultcontent` (`testResultContentID`, `testResultID`, `answer`, `answerType`, `rightAnswer`) VALUES
(1, 2, 'Piet', 0, ''),
(2, 2, 'Klaas', 0, ''),
(3, 2, 'Jan', 0, ''),
(4, 3, 'Piet', 0, ''),
(5, 3, 'Klaas', 0, ''),
(6, 3, 'Jan', 0, ''),
(7, 4, 'Piet', 0, ''),
(8, 4, 'Klaas', 0, ''),
(9, 4, 'Jan', 0, ''),
(10, 5, 'Jan', 0, ''),
(11, 5, 'Piet', 0, ''),
(12, 5, 'Klaas', 0, ''),
(13, 5, 'asd asd \\', 1, 'Piet'),
(14, 5, 'asdsad', 1, 'Klaas'),
(15, 6, 'asdsadasd', 1, 'Piet'),
(16, 7, 'Piet', 0, ''),
(17, 7, 'Klaas', 0, ''),
(18, 7, 'Jan', 0, ''),
(19, 8, 'Klaas', 0, ''),
(20, 8, 'Jan', 0, ''),
(21, 8, 'Piet', 0, ''),
(22, 8, 'asdas', 1, 'Piet'),
(23, 8, 'asdasdasd', 1, 'Klaas'),
(24, 8, 'asdasd', 1, 'Jan'),
(25, 8, 'asdasd', 1, 'Piet'),
(26, 9, 'Piet', 0, ''),
(27, 9, 'Jan', 0, ''),
(28, 9, 'Klaas', 0, ''),
(29, 9, 'asdasd', 1, 'Klaas'),
(30, 10, 'Piet', 0, ''),
(31, 10, 'Klaas', 0, ''),
(32, 10, 'Jan', 0, ''),
(33, 11, 'Piet', 0, ''),
(34, 11, 'Jan', 0, ''),
(35, 11, 'Klaas', 0, ''),
(36, 11, 'hghggjhasgdas', 1, 'Klaas'),
(37, 12, 'Piet', 0, ''),
(38, 12, 'Klaas', 0, ''),
(39, 12, 'Jan', 0, ''),
(40, 14, 'Klaas', 0, ''),
(41, 14, 'asdasd', 1, 'Piet'),
(42, 15, 'Piet', 0, ''),
(43, 16, 'Piet', 0, ''),
(44, 16, 'Klaas', 0, ''),
(45, 16, 'Jan', 0, ''),
(46, 17, 'Piet', 0, ''),
(47, 17, 'Klaas', 0, ''),
(48, 17, 'Jan', 0, ''),
(49, 18, 'Klaas', 0, ''),
(50, 18, 'Jan', 0, ''),
(51, 18, 'Piet', 0, ''),
(52, 18, 'asdasdas', 1, 'Piet'),
(53, 19, 'Klaas', 0, ''),
(54, 19, 'Jan', 0, ''),
(55, 19, 'Piet', 0, ''),
(56, 19, 'asdasd', 1, 'Piet'),
(57, 20, 'Piet', 0, ''),
(58, 20, 'Jan', 0, ''),
(59, 20, 'Klaas', 0, ''),
(60, 20, 'aksdfja', 1, 'Klaas');

-- --------------------------------------------------------

--
-- Table structure for table `testresults`
--

CREATE TABLE `testresults` (
  `testResultID` int(11) NOT NULL,
  `testID` int(11) NOT NULL COMMENT 'Klik op het nummer om naar die test toe te gaan.',
  `accountID` int(11) NOT NULL COMMENT 'Hier staat het persoon die het heeft geoefend',
  `testResultsDate` date NOT NULL,
  `wordsEachMinute` int(11) NOT NULL,
  `pauses` int(11) NOT NULL DEFAULT 0,
  `score` int(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `testresults`
--

INSERT INTO `testresults` (`testResultID`, `testID`, `accountID`, `testResultsDate`, `wordsEachMinute`, `pauses`, `score`) VALUES
(1, 1, 1, '2019-11-18', 10, 0, 0),
(2, 1, 1, '2019-11-27', 60, 0, 0),
(3, 1, 1, '2019-11-27', 90, 0, 0),
(4, 1, 1, '2019-11-27', 30, 0, 0),
(5, 1, 1, '2019-11-27', 36, 0, 0),
(6, 1, 1, '2019-11-27', 0, 1, 0),
(7, 1, 1, '2019-11-27', 36, 0, 0),
(8, 1, 1, '2019-11-27', 60, 0, 0),
(9, 1, 1, '2019-11-27', 60, 0, 0),
(10, 1, 1, '2019-11-27', 36, 0, 0),
(11, 1, 1, '2019-11-27', 60, 0, 0),
(12, 1, 1, '2019-11-27', 45, 0, 0),
(13, 1, 1, '2019-11-27', 0, 0, 0),
(14, 1, 1, '2019-11-27', 1, 0, 0),
(15, 1, 1, '2019-11-27', 7, 0, 0),
(16, 1, 1, '2019-11-27', 11, 0, 0),
(17, 1, 1, '2019-11-27', 60, 0, 0),
(18, 1, 1, '2019-11-27', 45, 0, 0),
(19, 1, 1, '2019-11-27', 45, 0, 0),
(20, 1, 1, '2019-11-28', 90, 0, 75);

-- --------------------------------------------------------

--
-- Table structure for table `tests`
--

CREATE TABLE `tests` (
  `testID` int(11) NOT NULL,
  `accountID` int(11) NOT NULL COMMENT 'Dit is de persoon die de toets heeft gemaakt',
  `testName` text NOT NULL,
  `testType` tinyint(1) NOT NULL COMMENT '0 = woorden, 1 = zinnen',
  `testDifficulty` int(2) NOT NULL COMMENT '0 = makkelijk, 1 = middel, 2 = moeilijk',
  `createDate` date NOT NULL,
  `isPrivate` tinyint(1) NOT NULL DEFAULT 0 COMMENT '0 = nee, publiek, 1 = ja, private',
  `archived` tinyint(1) NOT NULL DEFAULT 0 COMMENT '0 = false, niet gearchiveerd, 1 = true, gearchiveerd',
  `timesMade` int(11) DEFAULT 0,
  `highscore` int(11) DEFAULT 0,
  `version` int(11) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tests`
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
-- Indexes for dumped tables
--

--
-- Indexes for table `accounts`
--
ALTER TABLE `accounts`
  ADD PRIMARY KEY (`accountID`);

--
-- Indexes for table `testcontent`
--
ALTER TABLE `testcontent`
  ADD PRIMARY KEY (`testContentID`),
  ADD KEY `FK_testID_testContent` (`testID`);

--
-- Indexes for table `testresultcontent`
--
ALTER TABLE `testresultcontent`
  ADD PRIMARY KEY (`testResultContentID`),
  ADD KEY `FK_testResultID_testResultContent` (`testResultID`);

--
-- Indexes for table `testresults`
--
ALTER TABLE `testresults`
  ADD PRIMARY KEY (`testResultID`),
  ADD KEY `FK_testID_testResults` (`testID`) USING BTREE,
  ADD KEY `FK_accountID_testResults` (`accountID`) USING BTREE;

--
-- Indexes for table `tests`
--
ALTER TABLE `tests`
  ADD PRIMARY KEY (`testID`),
  ADD KEY `FK_accountID_tests` (`accountID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `accounts`
--
ALTER TABLE `accounts`
  MODIFY `accountID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `testcontent`
--
ALTER TABLE `testcontent`
  MODIFY `testContentID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `testresultcontent`
--
ALTER TABLE `testresultcontent`
  MODIFY `testResultContentID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=61;

--
-- AUTO_INCREMENT for table `testresults`
--
ALTER TABLE `testresults`
  MODIFY `testResultID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `tests`
--
ALTER TABLE `tests`
  MODIFY `testID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `testcontent`
--
ALTER TABLE `testcontent`
  ADD CONSTRAINT `FK_testID_testContent` FOREIGN KEY (`testID`) REFERENCES `tests` (`testID`);

--
-- Constraints for table `testresultcontent`
--
ALTER TABLE `testresultcontent`
  ADD CONSTRAINT `FK_testResultID_testResultContent` FOREIGN KEY (`testResultID`) REFERENCES `testresults` (`testResultID`);

--
-- Constraints for table `testresults`
--
ALTER TABLE `testresults`
  ADD CONSTRAINT `accountID_foreign_key` FOREIGN KEY (`accountID`) REFERENCES `accounts` (`accountID`),
  ADD CONSTRAINT `testID_foreign_key` FOREIGN KEY (`testID`) REFERENCES `tests` (`testID`);

--
-- Constraints for table `tests`
--
ALTER TABLE `tests`
  ADD CONSTRAINT `FK_accountID_tests` FOREIGN KEY (`accountID`) REFERENCES `accounts` (`accountID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
