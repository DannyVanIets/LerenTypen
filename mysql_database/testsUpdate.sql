-- phpMyAdmin SQL Dump
-- version 4.9.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 25, 2019 at 12:39 PM
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
-- Indexes for table `tests`
--
ALTER TABLE `tests`
  ADD PRIMARY KEY (`testID`),
  ADD KEY `FK_accountID_tests` (`accountID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tests`
--
ALTER TABLE `tests`
  MODIFY `testID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `tests`
--
ALTER TABLE `tests`
  ADD CONSTRAINT `FK_accountID_tests` FOREIGN KEY (`accountID`) REFERENCES `accounts` (`accountID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
