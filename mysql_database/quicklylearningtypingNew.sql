-- phpMyAdmin SQL Dump
-- version 4.9.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 25, 2019 at 02:34 PM
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
(1, 0, 'Danny van Iets', 'paddenstoel09', '1998-04-09', 'Danny', 'van Iets', 'Wat is de naam van je favoriete huisdier?', 'Paco', 0),
(2, 1, 'EJ Voorhoeve', 'items03', '1995-05-12', 'Eltjo', 'Voorhoeve', 'Wat is de naam van je oma?', 'Margriet', 0),
(3, 2, 'HenkerDenker', 'denkeffena1990', '1990-12-20', 'Henk', 'Denker', 'Wat is je favoriete datum?', '2001-10-30', 0);

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
(3, 1, 'Jan');

-- --------------------------------------------------------

--
-- Table structure for table `testresultcontent`
--

CREATE TABLE `testresultcontent` (
  `testResultContentID` int(11) NOT NULL,
  `testResultID` int(11) NOT NULL,
  `answer` int(11) NOT NULL,
  `answerType` tinyint(1) NOT NULL COMMENT '0 = goed antwoord, 1 = fout antwoord'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
  `pauses` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `testresults`
--

INSERT INTO `testresults` (`testResultID`, `testID`, `accountID`, `testResultsDate`, `wordsEachMinute`, `pauses`) VALUES
(1, 1, 1, '2019-11-18', 10, 0);

-- --------------------------------------------------------

--
-- Table structure for table `tests`
--

CREATE TABLE `tests` (
  `testID` int(11) NOT NULL,
  `accountID` int(11) NOT NULL COMMENT 'Dit is de persoon die de toets heeft gemaakt',
  `testName` text NOT NULL,
  `testType` tinyint(1) NOT NULL COMMENT '0 = woorden, 1 = zinnen',
  `testDifficulty` tinyint(1) NOT NULL COMMENT '0 = makkelijk, 1 = middel, 2 = moeilijk',
  `createDate` date NOT NULL,
  `isPrivate` tinyint(1) NOT NULL DEFAULT 0 COMMENT '0 = nee, publiek, 1 = ja, private',
  `archived` tinyint(1) NOT NULL DEFAULT 0 COMMENT '0 = false, niet gearchiveerd, 1 = true, gearchiveerd',
  `version` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tests`
--

INSERT INTO `tests` (`testID`, `accountID`, `testName`, `testType`, `testDifficulty`, `createDate`, `isPrivate`, `archived`, `version`) VALUES
(1, 1, 'Leren oefenen met namen!', 0, 0, '0000-00-00', 0, 0, 0);

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
  MODIFY `accountID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `testcontent`
--
ALTER TABLE `testcontent`
  MODIFY `testContentID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `testresultcontent`
--
ALTER TABLE `testresultcontent`
  MODIFY `testResultContentID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `testresults`
--
ALTER TABLE `testresults`
  MODIFY `testResultID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `tests`
--
ALTER TABLE `tests`
  MODIFY `testID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

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
