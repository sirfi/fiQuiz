-- MySQL dump 10.13  Distrib 8.0.17, for Win64 (x86_64)
--
-- Host: serce.onunki.com    Database: fiquizdb
-- ------------------------------------------------------
-- Server version	5.7.28

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) COLLATE utf8_turkish_ci NOT NULL,
  `ProductVersion` varchar(32) COLLATE utf8_turkish_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `aspnetroleclaims`
--

DROP TABLE IF EXISTS `aspnetroleclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetroleclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) COLLATE utf8_turkish_ci NOT NULL,
  `ClaimType` longtext COLLATE utf8_turkish_ci,
  `ClaimValue` longtext COLLATE utf8_turkish_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `aspnetroles`
--

DROP TABLE IF EXISTS `aspnetroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetroles` (
  `Id` varchar(255) COLLATE utf8_turkish_ci NOT NULL,
  `Name` varchar(256) COLLATE utf8_turkish_ci DEFAULT NULL,
  `NormalizedName` varchar(256) COLLATE utf8_turkish_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext COLLATE utf8_turkish_ci,
  `FullName` longtext COLLATE utf8_turkish_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `aspnetuserclaims`
--

DROP TABLE IF EXISTS `aspnetuserclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) COLLATE utf8_turkish_ci NOT NULL,
  `ClaimType` longtext COLLATE utf8_turkish_ci,
  `ClaimValue` longtext COLLATE utf8_turkish_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `aspnetuserlogins`
--

DROP TABLE IF EXISTS `aspnetuserlogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` varchar(255) COLLATE utf8_turkish_ci NOT NULL,
  `ProviderKey` varchar(255) COLLATE utf8_turkish_ci NOT NULL,
  `ProviderDisplayName` longtext COLLATE utf8_turkish_ci,
  `UserId` varchar(255) COLLATE utf8_turkish_ci NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `aspnetuserroles`
--

DROP TABLE IF EXISTS `aspnetuserroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserroles` (
  `UserId` varchar(255) COLLATE utf8_turkish_ci NOT NULL,
  `RoleId` varchar(255) COLLATE utf8_turkish_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `aspnetusers`
--

DROP TABLE IF EXISTS `aspnetusers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetusers` (
  `Id` varchar(255) COLLATE utf8_turkish_ci NOT NULL,
  `UserName` varchar(256) COLLATE utf8_turkish_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) COLLATE utf8_turkish_ci DEFAULT NULL,
  `Email` varchar(256) COLLATE utf8_turkish_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) COLLATE utf8_turkish_ci DEFAULT NULL,
  `EmailConfirmed` bit(1) NOT NULL,
  `PasswordHash` longtext COLLATE utf8_turkish_ci,
  `SecurityStamp` longtext COLLATE utf8_turkish_ci,
  `ConcurrencyStamp` longtext COLLATE utf8_turkish_ci,
  `PhoneNumber` longtext COLLATE utf8_turkish_ci,
  `PhoneNumberConfirmed` bit(1) NOT NULL,
  `TwoFactorEnabled` bit(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` bit(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  `FullName` longtext COLLATE utf8_turkish_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `aspnetusertokens`
--

DROP TABLE IF EXISTS `aspnetusertokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetusertokens` (
  `UserId` varchar(255) COLLATE utf8_turkish_ci NOT NULL,
  `LoginProvider` varchar(255) COLLATE utf8_turkish_ci NOT NULL,
  `Name` varchar(255) COLLATE utf8_turkish_ci NOT NULL,
  `Value` longtext COLLATE utf8_turkish_ci,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `questionanswers`
--

DROP TABLE IF EXISTS `questionanswers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `questionanswers` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreatedAt` datetime(6) NOT NULL,
  `CreatedBy` longtext COLLATE utf8_turkish_ci,
  `LastUpdatedAt` datetime(6) NOT NULL,
  `LastUpdatedBy` longtext COLLATE utf8_turkish_ci,
  `Answer` longtext COLLATE utf8_turkish_ci NOT NULL,
  `IsCorrect` bit(1) NOT NULL,
  `QuestionId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_QuestionAnswers_QuestionId` (`QuestionId`),
  CONSTRAINT `FK_QuestionAnswers_Questions_QuestionId` FOREIGN KEY (`QuestionId`) REFERENCES `questions` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=1760 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `questioncategories`
--

DROP TABLE IF EXISTS `questioncategories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `questioncategories` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreatedAt` datetime(6) NOT NULL,
  `CreatedBy` longtext COLLATE utf8_turkish_ci,
  `LastUpdatedAt` datetime(6) NOT NULL,
  `LastUpdatedBy` longtext COLLATE utf8_turkish_ci,
  `Name` longtext COLLATE utf8_turkish_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `questionreports`
--

DROP TABLE IF EXISTS `questionreports`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `questionreports` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreatedAt` datetime(6) NOT NULL,
  `CreatedBy` longtext COLLATE utf8_turkish_ci,
  `LastUpdatedAt` datetime(6) NOT NULL,
  `LastUpdatedBy` longtext COLLATE utf8_turkish_ci,
  `QuestionId` int(11) NOT NULL,
  `Description` longtext COLLATE utf8_turkish_ci,
  `IsClosed` bit(1) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_QuestionReports_QuestionId` (`QuestionId`),
  CONSTRAINT `FK_QuestionReports_Questions_QuestionId` FOREIGN KEY (`QuestionId`) REFERENCES `questions` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `questions`
--

DROP TABLE IF EXISTS `questions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `questions` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreatedAt` datetime(6) NOT NULL,
  `CreatedBy` longtext COLLATE utf8_turkish_ci,
  `LastUpdatedAt` datetime(6) NOT NULL,
  `LastUpdatedBy` longtext COLLATE utf8_turkish_ci,
  `QuestionText` longtext COLLATE utf8_turkish_ci NOT NULL,
  `CategoryId` int(11) NOT NULL,
  `ApprovalStatus` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `IX_Questions_CategoryId` (`CategoryId`),
  CONSTRAINT `FK_Questions_QuestionCategories_CategoryId` FOREIGN KEY (`CategoryId`) REFERENCES `questioncategories` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=446 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `quizquestionanswers`
--

DROP TABLE IF EXISTS `quizquestionanswers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `quizquestionanswers` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreatedAt` datetime(6) NOT NULL,
  `CreatedBy` longtext COLLATE utf8_turkish_ci,
  `LastUpdatedAt` datetime(6) NOT NULL,
  `LastUpdatedBy` longtext COLLATE utf8_turkish_ci,
  `QuizQuestionId` int(11) NOT NULL,
  `Option` int(11) NOT NULL,
  `AnswerId` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_QuizQuestionAnswers_AnswerId` (`AnswerId`),
  KEY `IX_QuizQuestionAnswers_QuizQuestionId` (`QuizQuestionId`),
  CONSTRAINT `FK_QuizQuestionAnswers_QuestionAnswers_AnswerId` FOREIGN KEY (`AnswerId`) REFERENCES `questionanswers` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_QuizQuestionAnswers_QuizQuestions_QuizQuestionId` FOREIGN KEY (`QuizQuestionId`) REFERENCES `quizquestions` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14681 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `quizquestions`
--

DROP TABLE IF EXISTS `quizquestions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `quizquestions` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreatedAt` datetime(6) NOT NULL,
  `CreatedBy` longtext COLLATE utf8_turkish_ci,
  `LastUpdatedAt` datetime(6) NOT NULL,
  `LastUpdatedBy` longtext COLLATE utf8_turkish_ci,
  `QuizId` int(11) NOT NULL,
  `QuestionId` int(11) NOT NULL,
  `CorrectAnswer` int(11) NOT NULL,
  `Answer` int(11) DEFAULT NULL,
  `AnsweredAt` datetime(6) DEFAULT NULL,
  `ShowedAt` datetime(6) DEFAULT NULL,
  `QuestionNumber` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `IX_QuizQuestions_QuestionId` (`QuestionId`),
  KEY `IX_QuizQuestions_QuizId` (`QuizId`),
  CONSTRAINT `FK_QuizQuestions_Questions_QuestionId` FOREIGN KEY (`QuestionId`) REFERENCES `questions` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_QuizQuestions_Quizzes_QuizId` FOREIGN KEY (`QuizId`) REFERENCES `quizzes` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3766 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `quizusedjokers`
--

DROP TABLE IF EXISTS `quizusedjokers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `quizusedjokers` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreatedAt` datetime(6) NOT NULL,
  `CreatedBy` longtext COLLATE utf8_turkish_ci,
  `LastUpdatedAt` datetime(6) NOT NULL,
  `LastUpdatedBy` longtext COLLATE utf8_turkish_ci,
  `QuizId` int(11) NOT NULL,
  `Joker` int(11) NOT NULL,
  `QuizQuestionId` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `IX_QuizUsedJokers_QuizId` (`QuizId`),
  KEY `IX_QuizUsedJokers_QuizQuestionId` (`QuizQuestionId`),
  CONSTRAINT `FK_QuizUsedJokers_QuizQuestions_QuizQuestionId` FOREIGN KEY (`QuizQuestionId`) REFERENCES `quizquestions` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_QuizUsedJokers_Quizzes_QuizId` FOREIGN KEY (`QuizId`) REFERENCES `quizzes` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=119 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `quizzes`
--

DROP TABLE IF EXISTS `quizzes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `quizzes` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CreatedAt` datetime(6) NOT NULL,
  `CreatedBy` longtext COLLATE utf8_turkish_ci,
  `LastUpdatedAt` datetime(6) NOT NULL,
  `LastUpdatedBy` longtext COLLATE utf8_turkish_ci,
  `CompletedFlag` bit(1) NOT NULL,
  `CompletedAt` datetime(6) DEFAULT NULL,
  `CompletionType` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=257 DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping routines for database 'fiquizdb'
--
/*!50003 DROP PROCEDURE IF EXISTS `selectquestions` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`fiquizdbo`@`%` PROCEDURE `selectquestions`(
IN QuestionCount INT
)
BEGIN
call selectquestionsbase(QuestionCount,NULL,NULL);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `selectquestionsbase` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`fiquizdbo`@`%` PROCEDURE `selectquestionsbase`(
IN QuestionCount INT,
IN QuestionNumber INT,
IN ExcludeQuestionId INT)
BEGIN

CREATE TEMPORARY TABLE IF NOT EXISTS questionstats (QuestionId INT, SelectionCount INT, AnsweredCount INT, CorrectAnsweredCount INT, WrongAnsweredCount INT, Level DOUBLE, PRIMARY KEY (QuestionId));
TRUNCATE TABLE questionstats;

INSERT INTO questionstats 
SELECT 
    qq2.*,
    IF(qq2.AnsweredCount=0,NULL,FLOOR(qq2.WrongAnsweredCount * (QuestionCount - 0.01) / qq2.AnsweredCount)) AS Level
FROM
    (SELECT 
        qq1.QuestionId,
            COALESCE(COUNT(qq1.QuestionId), 0) AS SelectionCount,
            COALESCE(SUM(Correct + Wrong), 0) AS AnsweredCount,
            COALESCE(SUM(Correct), 0) AS CorrectAnsweredCount,
            COALESCE(SUM(Wrong), 0) AS WrongAnsweredCount
    FROM
        (SELECT 
        qq.QuestionId,
            IF(qq.Answer = qq.CorrectAnswer, 1, 0) AS Correct,
            IF(qq.Answer <> qq.CorrectAnswer, 1, 0) AS Wrong
    FROM
        quizquestions qq where ExcludeQuestionId is null or qq.QuestionId!=ExcludeQuestionId) AS qq1
    GROUP BY qq1.QuestionId) AS qq2;

CREATE TEMPORARY TABLE IF NOT EXISTS questionindexes (QuestionId INT, QuestionIndex INT, PRIMARY KEY (QuestionId));
TRUNCATE TABLE questionindexes;

SET @row_number = 0; 
INSERT INTO questionindexes
SELECT q.Id AS QuestionId, COALESCE(qs.Level, (@row_number:=@row_number + 1) % QuestionCount) AS QuestionIndex
FROM questions q 
LEFT JOIN questionstats qs ON qs.QuestionId = q.Id
WHERE q.ApprovalStatus=1;

SELECT 
    q.*
FROM
    (SELECT 
        qi.QuestionIndex,
            SUBSTRING_INDEX(GROUP_CONCAT(QuestionId
                ORDER BY RAND()), ',', 1) AS QuestionId
    FROM
        questionindexes qi
    GROUP BY qi.QuestionIndex
    ORDER BY qi.QuestionIndex ASC) qi
        INNER JOIN
    questions q ON q.Id = qi.QuestionId
WHERE
    QuestionNumber IS NULL
        || qi.QuestionIndex = QuestionNumber - 1;

DROP TABLE questionindexes;
DROP TABLE questionstats;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-12-19 21:33:41
