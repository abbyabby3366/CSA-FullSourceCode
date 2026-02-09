/*
SQLyog Community v11.31 (64 bit)
MySQL - 8.0.21 : Database - csa
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`csa` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `csa`;

/*Table structure for table `admin` */

DROP TABLE IF EXISTS `admin`;

CREATE TABLE `admin` (
  `AdminId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(225) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `GoogleId` varchar(255) DEFAULT NULL,
  `GoogleProfilePicture` text,
  `PasswordSalted` text,
  `Salt` text,
  `IsSuperAdmin` int NOT NULL,
  `RoleId` int DEFAULT NULL,
  `LastLogin` datetime DEFAULT NULL,
  `StatusId` int NOT NULL COMMENT '1-Active 2-Inactive',
  `CreateDate` datetime NOT NULL,
  `TeamId` int DEFAULT NULL,
  `DivisionId` int DEFAULT NULL,
  PRIMARY KEY (`AdminId`),
  UNIQUE KEY `Email` (`Email`),
  KEY `RoleId` (`RoleId`),
  CONSTRAINT `admin_ibfk_1` FOREIGN KEY (`RoleId`) REFERENCES `role` (`RoleId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

/*Data for the table `admin` */

insert  into `admin`(`AdminId`,`Name`,`Email`,`GoogleId`,`GoogleProfilePicture`,`PasswordSalted`,`Salt`,`IsSuperAdmin`,`RoleId`,`LastLogin`,`StatusId`,`CreateDate`,`TeamId`,`DivisionId`) values (1,'admin','admin@mail.com',NULL,NULL,'320a11ac3fdec75a61bc851f9c460c4434d0030094c16c060b3130626f0592e83248af21d7d12db385935117de30cc08c79b229a34b357a107091af2e606fea8','9Fc92pTyOnT6ohky9xeVrriu6Iipj2y9QbnU+rZ0t1AhVztFVTlWYRq5gEYeCNwnbXiv4bAbH07H4dZ8wJB3Yz5+JGtBO+gKxDPOeQ02KN9oy5qZE/TIy3fjWvvS71fTaz6mA4LaCtevCtrQbaAz8XMbFk5tQ9LTf2OsgcF/7GQ=',1,5,'2024-10-17 09:06:44',1,'2024-07-18 09:40:18',NULL,NULL),(6,'staff','staff@mail.com',NULL,NULL,'331829394b26d1807e4aefd6d6fbd6d7a9ccd15260e02a377c3dd79088d2e5504fbb00015cc0981b71a376e9adddd1b5c21ba249dc583fb96461dc9049ebfcea','ku9NpUiSdxTdiaXT2MuZab2IBfKfFEMkAW0pG/Gr9bv8B2cpY81csAQ8lFUmaJ8yv9o0y3nGZWBfqMcCHoa8wTTlPrFb11YCu42VtCbU5vePsnPPwPbmRnl1vIiLrg43sdO+fcOp+Md6MZWRau23opA+ldo/yFDzcgAT7sV3Lh8=',0,5,NULL,1,'2024-09-17 13:08:12',NULL,NULL);

/*Table structure for table `announcement` */

DROP TABLE IF EXISTS `announcement`;

CREATE TABLE `announcement` (
  `AnnouncementId` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(255) DEFAULT NULL,
  `Content` text,
  `ImageFileId` varchar(50) DEFAULT NULL,
  `ArticleDate` datetime DEFAULT NULL,
  `StartDate` datetime DEFAULT NULL,
  `EndDate` datetime DEFAULT NULL,
  `StatusId` int DEFAULT NULL,
  PRIMARY KEY (`AnnouncementId`),
  KEY `ImageFileId` (`ImageFileId`),
  CONSTRAINT `announcement_ibfk_1` FOREIGN KEY (`ImageFileId`) REFERENCES `file` (`FileId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `announcement` */

insert  into `announcement`(`AnnouncementId`,`Title`,`Content`,`ImageFileId`,`ArticleDate`,`StartDate`,`EndDate`,`StatusId`) values (1,'aa','bbb',NULL,'2024-07-17 14:59:06',NULL,'2024-07-17 15:03:31',0);

/*Table structure for table `application` */

DROP TABLE IF EXISTS `application`;

CREATE TABLE `application` (
  `ApplicationId` bigint NOT NULL AUTO_INCREMENT,
  `MemberId` bigint NOT NULL,
  `CreateDate` datetime NOT NULL,
  `CreditRemark` text,
  `CustomerStatusId` int NOT NULL COMMENT '1-Eligible 2-Burst',
  `BurstReasonId` int DEFAULT NULL,
  `ApplicationStatusId` int NOT NULL COMMENT '1-Pre-checking',
  `ApplicationStatusLastChangeDate` datetime DEFAULT NULL,
  `ApplicationStatusLastChangeAdminId` int DEFAULT NULL,
  `ReferrerMemberId` bigint DEFAULT NULL,
  `AMAdminId` int DEFAULT NULL,
  `PFCAdminId` int DEFAULT NULL,
  `RMAdminId` int DEFAULT NULL,
  `UMAdminId` int DEFAULT NULL,
  `PAAdminId` int DEFAULT NULL,
  `SourceId` int DEFAULT NULL,
  `RejectedDate` datetime DEFAULT NULL,
  `RejectedAdminId` int DEFAULT NULL,
  `RejectedReason` text,
  `RejectedApplicationStatusId` int DEFAULT NULL,
  `CreditStatusId` int DEFAULT NULL,
  `ScoreClass` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ApplicationId`),
  KEY `MemberId` (`MemberId`),
  KEY `ReferrerMemberId` (`ReferrerMemberId`),
  KEY `AMAdminId` (`AMAdminId`),
  KEY `PFCAdminId` (`PFCAdminId`),
  KEY `RMAdminId` (`RMAdminId`),
  KEY `UMAdminId` (`UMAdminId`),
  KEY `PAAdminId` (`PAAdminId`),
  KEY `ApplicationStatusLastChangeAdminId` (`ApplicationStatusLastChangeAdminId`),
  KEY `RejectedAdminId` (`RejectedAdminId`),
  CONSTRAINT `application_ibfk_1` FOREIGN KEY (`MemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_ibfk_2` FOREIGN KEY (`ReferrerMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_ibfk_3` FOREIGN KEY (`AMAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_ibfk_4` FOREIGN KEY (`PFCAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_ibfk_5` FOREIGN KEY (`RMAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_ibfk_6` FOREIGN KEY (`UMAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_ibfk_7` FOREIGN KEY (`PAAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_ibfk_8` FOREIGN KEY (`ApplicationStatusLastChangeAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_ibfk_9` FOREIGN KEY (`RejectedAdminId`) REFERENCES `announcement` (`AnnouncementId`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `application` */

insert  into `application`(`ApplicationId`,`MemberId`,`CreateDate`,`CreditRemark`,`CustomerStatusId`,`BurstReasonId`,`ApplicationStatusId`,`ApplicationStatusLastChangeDate`,`ApplicationStatusLastChangeAdminId`,`ReferrerMemberId`,`AMAdminId`,`PFCAdminId`,`RMAdminId`,`UMAdminId`,`PAAdminId`,`SourceId`,`RejectedDate`,`RejectedAdminId`,`RejectedReason`,`RejectedApplicationStatusId`,`CreditStatusId`,`ScoreClass`) values (1,2,'2024-10-07 10:48:51','test ya okay',1,NULL,2,NULL,NULL,6,NULL,NULL,NULL,NULL,NULL,1,NULL,NULL,NULL,NULL,1,NULL),(2,2,'2024-10-12 06:59:43','123',2,6,7,'2024-10-12 20:51:17',1,6,1,1,6,6,1,5,NULL,1,'iyakan',NULL,6,'456'),(3,1,'2024-10-12 12:36:42',NULL,1,NULL,2,'2024-10-12 12:45:26',1,6,1,6,1,6,NULL,1,NULL,NULL,NULL,NULL,1,NULL),(4,3,'2024-10-12 17:43:57',NULL,1,NULL,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(5,6,'2024-10-12 17:44:14',NULL,1,NULL,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(6,3,'2024-10-12 19:44:19',NULL,1,NULL,9,NULL,NULL,6,1,6,6,1,6,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(7,3,'2024-10-12 20:15:32',NULL,1,NULL,1,NULL,NULL,NULL,NULL,6,1,NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(8,6,'2024-10-12 21:29:50',NULL,1,NULL,2,'2024-10-12 21:58:27',1,NULL,NULL,1,6,NULL,NULL,1,NULL,1,'kagaklah',NULL,1,NULL),(9,3,'2024-10-12 22:19:34',NULL,1,NULL,2,'2024-10-12 22:26:55',1,NULL,NULL,NULL,NULL,NULL,NULL,1,NULL,NULL,NULL,NULL,1,NULL),(10,7,'2024-10-13 18:27:47',NULL,2,1,8,'2024-10-16 11:01:47',1,NULL,NULL,6,1,6,NULL,1,NULL,1,'suka2lah',NULL,1,NULL),(11,6,'2024-10-17 07:54:20',NULL,1,NULL,9,'2024-10-17 08:52:18',1,NULL,NULL,NULL,NULL,NULL,NULL,1,NULL,NULL,NULL,NULL,1,NULL);

/*Table structure for table `application_1` */

DROP TABLE IF EXISTS `application_1`;

CREATE TABLE `application_1` (
  `Application1Id` bigint NOT NULL AUTO_INCREMENT,
  `ApplicationId` bigint NOT NULL,
  `RAMCIReportFileId` varchar(50) DEFAULT NULL,
  `RAMCIReportAdminId` int DEFAULT NULL,
  `RAMCIReportLastUpdate` datetime DEFAULT NULL,
  `CCRISDocumentFileId` varchar(50) DEFAULT NULL,
  `CCRISDocumentAdminId` int DEFAULT NULL,
  `CCRISDocumentLastUpdate` datetime DEFAULT NULL,
  `EligibilityId` int DEFAULT NULL,
  `EligibilityAdminId` int DEFAULT NULL,
  `EligibilityLastUpdate` datetime DEFAULT NULL,
  `LegalSuitsCheck` int DEFAULT NULL,
  `BankruptcyCheck` int DEFAULT NULL,
  `SpecialAttentionCheck` int DEFAULT NULL,
  `BadPaymentRecordCheck` int DEFAULT NULL,
  PRIMARY KEY (`Application1Id`),
  KEY `ApplicationId` (`ApplicationId`),
  CONSTRAINT `application_1_ibfk_1` FOREIGN KEY (`ApplicationId`) REFERENCES `application` (`ApplicationId`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `application_1` */

insert  into `application_1`(`Application1Id`,`ApplicationId`,`RAMCIReportFileId`,`RAMCIReportAdminId`,`RAMCIReportLastUpdate`,`CCRISDocumentFileId`,`CCRISDocumentAdminId`,`CCRISDocumentLastUpdate`,`EligibilityId`,`EligibilityAdminId`,`EligibilityLastUpdate`,`LegalSuitsCheck`,`BankruptcyCheck`,`SpecialAttentionCheck`,`BadPaymentRecordCheck`) values (1,1,'41438f83-bc80-425b-845d-6e87f5e9828f',1,'2024-10-08 17:04:24','6dd10803-9f2e-487d-b805-9bd811b19c1d',1,'2024-10-08 19:20:36',0,1,'2024-10-09 16:05:39',0,0,0,0),(2,2,'c75d35e4-7759-4bad-a001-08e2b651180e',1,'2024-10-12 07:53:32',NULL,1,'2024-10-12 07:53:32',1,1,'2024-10-12 07:28:05',0,0,0,0),(3,3,NULL,NULL,NULL,NULL,NULL,NULL,0,1,'2024-10-12 12:45:16',0,0,0,0),(4,6,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(5,7,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(6,8,NULL,NULL,NULL,NULL,NULL,NULL,0,1,'2024-10-12 21:30:05',0,0,0,0),(7,9,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(8,10,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(9,11,NULL,NULL,NULL,NULL,NULL,NULL,0,1,'2024-10-17 07:56:32',0,0,0,0);

/*Table structure for table `application_10` */

DROP TABLE IF EXISTS `application_10`;

CREATE TABLE `application_10` (
  `Application10Id` bigint NOT NULL AUTO_INCREMENT,
  `ApplicationId` bigint NOT NULL,
  `DeclarationFormFileId` varchar(50) DEFAULT NULL,
  `DeclarationFormAdminId` int DEFAULT NULL,
  `DeclarationFormLastUpdate` datetime DEFAULT NULL,
  `SettlementReceiptFileId` varchar(50) DEFAULT NULL,
  `SettlementReceiptAdminId` int DEFAULT NULL,
  `SettlementReceiptLastUpdate` datetime DEFAULT NULL,
  `ServiceFeeReceiptFileId` varchar(50) DEFAULT NULL,
  `ServiceFeeReceiptAdminId` int DEFAULT NULL,
  `ServiceFeeReceiptLastUpdate` datetime DEFAULT NULL,
  `RezekiReceiptFileId` varchar(50) DEFAULT NULL,
  `RezekiReceiptAdminId` int DEFAULT NULL,
  `RezekiReceiptLastUpdate` datetime DEFAULT NULL,
  `RezekiAgreementFileId` varchar(50) DEFAULT NULL,
  `RezekiAgreementAdminId` int DEFAULT NULL,
  `RezekiAgreementLastUpdate` datetime DEFAULT NULL,
  `ServiceFee` decimal(13,2) DEFAULT NULL,
  `DepositAmount` decimal(13,2) DEFAULT NULL,
  `DepositDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Application10Id`),
  KEY `ApplicationId` (`ApplicationId`),
  KEY `DeclarationFormAdminId` (`DeclarationFormAdminId`),
  KEY `SettlementReceiptAdminId` (`SettlementReceiptAdminId`),
  KEY `ServiceFeeReceiptAdminId` (`ServiceFeeReceiptAdminId`),
  KEY `RezekiReceiptAdminId` (`RezekiReceiptAdminId`),
  KEY `RezekiAgreementAdminId` (`RezekiAgreementAdminId`),
  CONSTRAINT `application_10_ibfk_1` FOREIGN KEY (`ApplicationId`) REFERENCES `application` (`ApplicationId`),
  CONSTRAINT `application_10_ibfk_2` FOREIGN KEY (`DeclarationFormAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_10_ibfk_3` FOREIGN KEY (`SettlementReceiptAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_10_ibfk_4` FOREIGN KEY (`ServiceFeeReceiptAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_10_ibfk_5` FOREIGN KEY (`RezekiReceiptAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_10_ibfk_6` FOREIGN KEY (`RezekiAgreementAdminId`) REFERENCES `admin` (`AdminId`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `application_10` */

insert  into `application_10`(`Application10Id`,`ApplicationId`,`DeclarationFormFileId`,`DeclarationFormAdminId`,`DeclarationFormLastUpdate`,`SettlementReceiptFileId`,`SettlementReceiptAdminId`,`SettlementReceiptLastUpdate`,`ServiceFeeReceiptFileId`,`ServiceFeeReceiptAdminId`,`ServiceFeeReceiptLastUpdate`,`RezekiReceiptFileId`,`RezekiReceiptAdminId`,`RezekiReceiptLastUpdate`,`RezekiAgreementFileId`,`RezekiAgreementAdminId`,`RezekiAgreementLastUpdate`,`ServiceFee`,`DepositAmount`,`DepositDate`) values (1,1,'b65e1706-5f94-4fe1-b2e2-43ad49945080',1,'2024-10-11 22:03:36','2b79e107-932d-4503-b299-aabbc645823d',1,'2024-10-11 22:05:35','dd3d95e2-593c-4bad-9450-569c5963fe0c',1,'2024-10-11 22:05:35','57cd118d-b921-4654-95d5-4fceabf59b84',1,'2024-10-11 22:05:35','b0978af0-f123-4c52-97a5-9dc5323d02b4',1,'2024-10-11 22:05:35','200223213.33','622.33','2024-10-31 00:00:00'),(2,2,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(3,7,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(4,8,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(5,9,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(6,10,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(7,11,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);

/*Table structure for table `application_2` */

DROP TABLE IF EXISTS `application_2`;

CREATE TABLE `application_2` (
  `Application2Id` bigint NOT NULL AUTO_INCREMENT,
  `ApplicationID` bigint NOT NULL,
  `ProposalFileId` varchar(50) DEFAULT NULL,
  `ProposalAdminId` int DEFAULT NULL,
  `ProposalLastUpdate` datetime DEFAULT NULL,
  `SalaryGross` decimal(13,2) DEFAULT NULL,
  `SalaryDeduction` decimal(13,2) DEFAULT NULL,
  `NetIncome` decimal(13,2) DEFAULT NULL,
  `PriorDSRB1` decimal(13,2) DEFAULT NULL,
  `PriorDSRB2` decimal(13,2) DEFAULT NULL,
  `PriorDSRB3` decimal(13,2) DEFAULT NULL,
  `PriorDSRB4` decimal(13,2) DEFAULT NULL,
  `PriorDSRBAverage` decimal(13,2) DEFAULT NULL,
  `CommitmentOutstanding` decimal(13,2) DEFAULT NULL,
  `CommitmentInstallment` decimal(13,2) DEFAULT NULL,
  `OthersNetBalance` decimal(13,2) DEFAULT NULL,
  `OthersBPA` decimal(13,2) DEFAULT NULL,
  `OthersComparisonDSR` decimal(13,2) DEFAULT NULL,
  `OthersComparisonDSRPctCommitment` decimal(3,2) DEFAULT NULL,
  `OthersPctRefresh` decimal(3,2) DEFAULT NULL,
  `OthersProposedRefresh` decimal(13,2) DEFAULT NULL,
  `OthersCompositionDSR` decimal(13,2) DEFAULT NULL,
  `OthersCompositionDSRPctCommitment` decimal(3,2) DEFAULT NULL,
  `RefreshTotal` decimal(13,2) DEFAULT NULL,
  `RefreshRemainCommitment` decimal(13,2) DEFAULT NULL,
  `ReloanTotal` decimal(13,2) DEFAULT NULL,
  `ReloanMonthly` decimal(13,2) DEFAULT NULL,
  `ReloanBersih` decimal(13,2) DEFAULT NULL,
  `ReloanBelanja` decimal(13,2) DEFAULT NULL,
  `ReloanDeposit` decimal(13,2) DEFAULT NULL,
  `ReloanDanaBantuan` decimal(13,2) DEFAULT NULL,
  `ReloanServiceFee` decimal(13,2) DEFAULT NULL,
  `ReloanServiceFeePct` decimal(3,2) DEFAULT NULL,
  `ReloanIncomeAfterRNR` decimal(13,2) DEFAULT NULL,
  `ReloanDifference` decimal(13,2) DEFAULT NULL,
  `ModelBackgroundScreeningId` int DEFAULT NULL,
  `ModelCompositionDSRId` int DEFAULT NULL,
  `ModelCommitmentId` int DEFAULT NULL,
  `ModelSettlementId` int DEFAULT NULL,
  `ModelServiceFeeId` int DEFAULT NULL,
  `ModelNetIncomeAfterRNRId` int DEFAULT NULL,
  `ModelStatusId` int DEFAULT NULL,
  `ModelStatusProposalId` int DEFAULT NULL,
  `ModelCheckId` int DEFAULT NULL,
  `ReviewAdminId` int DEFAULT NULL,
  `ReviewStatusId` int DEFAULT NULL,
  `ReviewDate` datetime DEFAULT NULL,
  `ReviewComment` text,
  `ApproveAdminId` int DEFAULT NULL,
  `ApproveStatusId` int DEFAULT NULL,
  `ApproveDate` datetime DEFAULT NULL,
  `ApproveComment` text,
  PRIMARY KEY (`Application2Id`),
  KEY `ApplicationID` (`ApplicationID`),
  KEY `ProposalAdminId` (`ProposalAdminId`),
  KEY `ReviewAdminId` (`ReviewAdminId`),
  KEY `ApproveAdminId` (`ApproveAdminId`),
  CONSTRAINT `application_2_ibfk_1` FOREIGN KEY (`ApplicationID`) REFERENCES `application` (`ApplicationId`),
  CONSTRAINT `application_2_ibfk_2` FOREIGN KEY (`ProposalAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_2_ibfk_3` FOREIGN KEY (`ReviewAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_2_ibfk_4` FOREIGN KEY (`ApproveAdminId`) REFERENCES `admin` (`AdminId`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `application_2` */

insert  into `application_2`(`Application2Id`,`ApplicationID`,`ProposalFileId`,`ProposalAdminId`,`ProposalLastUpdate`,`SalaryGross`,`SalaryDeduction`,`NetIncome`,`PriorDSRB1`,`PriorDSRB2`,`PriorDSRB3`,`PriorDSRB4`,`PriorDSRBAverage`,`CommitmentOutstanding`,`CommitmentInstallment`,`OthersNetBalance`,`OthersBPA`,`OthersComparisonDSR`,`OthersComparisonDSRPctCommitment`,`OthersPctRefresh`,`OthersProposedRefresh`,`OthersCompositionDSR`,`OthersCompositionDSRPctCommitment`,`RefreshTotal`,`RefreshRemainCommitment`,`ReloanTotal`,`ReloanMonthly`,`ReloanBersih`,`ReloanBelanja`,`ReloanDeposit`,`ReloanDanaBantuan`,`ReloanServiceFee`,`ReloanServiceFeePct`,`ReloanIncomeAfterRNR`,`ReloanDifference`,`ModelBackgroundScreeningId`,`ModelCompositionDSRId`,`ModelCommitmentId`,`ModelSettlementId`,`ModelServiceFeeId`,`ModelNetIncomeAfterRNRId`,`ModelStatusId`,`ModelStatusProposalId`,`ModelCheckId`,`ReviewAdminId`,`ReviewStatusId`,`ReviewDate`,`ReviewComment`,`ApproveAdminId`,`ApproveStatusId`,`ApproveDate`,`ApproveComment`) values (1,1,'8098c2e9-11b2-4c95-927d-d1c3646ab99e',1,'2024-10-09 12:03:40','1.00','2.00','3.00','4.00','5.00','6.00','7.00','8.00','9.00','10.00','11.00','12.00','13.00','1.00','1.00','2.00','3.00','1.00','2.00','3.00','4.00','5.00','6.00','7.00','8.00','9.00','10.00','1.00','12.00','13.00',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1,2,'2024-10-11 00:00:00','kalian',6,3,'2024-05-16 00:00:00','dani'),(2,2,'4e8f4dcb-4f77-4ba6-a7aa-69e0c4dce938',1,'2024-10-12 07:54:00','100.00','200.00','300.00','1.00','2.00','3.00','4.00','5.00','6.00','7.00','8.00','9.00','10.00','1.00','0.50','11.00','12.00','0.30','13.00','14.00','15.00','16.00','17.00','18.00','19.00','20.00','21.00','0.25','22.00','23.00',1,1,1,1,1,1,1,1,1,1,1,'2024-10-14 00:00:00','awww',6,1,'2024-10-29 00:00:00','iyah'),(3,3,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(4,6,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(5,7,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(6,8,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1,1,1,1,1,1,1,1,1,6,1,NULL,'',1,1,NULL,''),(7,9,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(8,10,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(9,11,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1,1,1,1,1,1,1,1,1,NULL,1,NULL,'',NULL,1,NULL,'');

/*Table structure for table `application_3` */

DROP TABLE IF EXISTS `application_3`;

CREATE TABLE `application_3` (
  `Application3Id` bigint NOT NULL AUTO_INCREMENT,
  `ApplicationId` bigint NOT NULL,
  `ProposalStatusId` int DEFAULT NULL,
  `ProposalStatusAdminId` int DEFAULT NULL,
  `ProposalStatusLastUpdate` datetime DEFAULT NULL,
  PRIMARY KEY (`Application3Id`),
  KEY `ApplicationId` (`ApplicationId`),
  KEY `ProposalStatusAdminId` (`ProposalStatusAdminId`),
  CONSTRAINT `application_3_ibfk_1` FOREIGN KEY (`ApplicationId`) REFERENCES `application` (`ApplicationId`),
  CONSTRAINT `application_3_ibfk_2` FOREIGN KEY (`ProposalStatusAdminId`) REFERENCES `admin` (`AdminId`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `application_3` */

insert  into `application_3`(`Application3Id`,`ApplicationId`,`ProposalStatusId`,`ProposalStatusAdminId`,`ProposalStatusLastUpdate`) values (1,1,1,1,'2024-10-09 16:03:14'),(2,2,2,1,'2024-10-12 07:56:19'),(3,6,NULL,NULL,NULL),(4,7,NULL,NULL,NULL),(5,8,1,1,'2024-10-12 21:34:58'),(6,9,1,1,'2024-10-12 22:22:51'),(7,10,NULL,NULL,NULL),(8,11,1,1,'2024-10-17 08:50:27');

/*Table structure for table `application_4` */

DROP TABLE IF EXISTS `application_4`;

CREATE TABLE `application_4` (
  `Application4Id` bigint NOT NULL AUTO_INCREMENT,
  `ApplicationId` bigint DEFAULT NULL,
  `ProposalSendId` int DEFAULT NULL,
  `ProposalSendAdminId` int DEFAULT NULL,
  `ProposalSendLastUpdate` datetime DEFAULT NULL,
  `SuratAkuanId` int DEFAULT NULL,
  `SuratAkuanAdminId` int DEFAULT NULL,
  `SuratAkuanLastUpdate` datetime DEFAULT NULL,
  `ComprehensiveFormId` int DEFAULT NULL,
  `ComprehensiveFormAdminId` int DEFAULT NULL,
  `ComprehensiveFormLastUpdate` datetime DEFAULT NULL,
  PRIMARY KEY (`Application4Id`),
  KEY `ApplicationId` (`ApplicationId`),
  KEY `ProposalSendAdminId` (`ProposalSendAdminId`),
  KEY `SuratAkuanAdminId` (`SuratAkuanAdminId`),
  KEY `ComprehensiveFormAdminId` (`ComprehensiveFormAdminId`),
  CONSTRAINT `application_4_ibfk_1` FOREIGN KEY (`ApplicationId`) REFERENCES `application` (`ApplicationId`),
  CONSTRAINT `application_4_ibfk_2` FOREIGN KEY (`ProposalSendAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_4_ibfk_3` FOREIGN KEY (`SuratAkuanAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_4_ibfk_4` FOREIGN KEY (`ComprehensiveFormAdminId`) REFERENCES `admin` (`AdminId`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `application_4` */

insert  into `application_4`(`Application4Id`,`ApplicationId`,`ProposalSendId`,`ProposalSendAdminId`,`ProposalSendLastUpdate`,`SuratAkuanId`,`SuratAkuanAdminId`,`SuratAkuanLastUpdate`,`ComprehensiveFormId`,`ComprehensiveFormAdminId`,`ComprehensiveFormLastUpdate`) values (1,1,1,1,'2024-10-09 16:15:19',2,1,'2024-10-09 16:15:13',2,1,'2024-10-09 16:15:25'),(2,2,1,1,'2024-10-12 07:56:55',2,1,'2024-10-12 07:57:04',2,1,'2024-10-12 07:57:04'),(3,6,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(4,7,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(5,8,1,1,'2024-10-12 21:39:07',1,1,'2024-10-12 21:39:07',1,1,'2024-10-12 21:39:07'),(6,9,1,1,'2024-10-12 22:24:44',1,1,'2024-10-12 22:24:44',1,1,'2024-10-12 22:24:44'),(7,10,1,1,'2024-10-15 16:31:23',1,1,'2024-10-15 16:31:23',1,1,'2024-10-15 16:31:23'),(8,11,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);

/*Table structure for table `application_5` */

DROP TABLE IF EXISTS `application_5`;

CREATE TABLE `application_5` (
  `Application5Id` bigint NOT NULL AUTO_INCREMENT,
  `ApplicationId` bigint NOT NULL,
  `PayslipFileId` varchar(50) DEFAULT NULL,
  `PayslipAdminId` int DEFAULT NULL,
  `PayslipLastUpdate` datetime DEFAULT NULL,
  `RAMCIFileId` varchar(50) DEFAULT NULL,
  `RAMCIAdminId` int DEFAULT NULL,
  `RAMCILastUpdate` datetime DEFAULT NULL,
  `CTOSFileId` varchar(50) DEFAULT NULL,
  `CTOSAdminId` int DEFAULT NULL,
  `CTOSLastUpdate` datetime DEFAULT NULL,
  `RedemptionLetterFileId` varchar(50) DEFAULT NULL,
  `RedemptionLetterAdminId` int DEFAULT NULL,
  `RedemptionLetterLastUpdate` datetime DEFAULT NULL,
  `ApplicantAddress` varchar(500) DEFAULT NULL,
  `BankruptcyStatus` varchar(255) DEFAULT NULL,
  `LegalCase` varchar(255) DEFAULT NULL,
  `HealthCreditScore` varchar(100) DEFAULT NULL,
  `Commitements` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`Application5Id`),
  KEY `ApplicationId` (`ApplicationId`),
  KEY `PayslipAdminId` (`PayslipAdminId`),
  KEY `RAMCIAdminId` (`RAMCIAdminId`),
  KEY `CTOSAdminId` (`CTOSAdminId`),
  KEY `RedemptionLetterAdminId` (`RedemptionLetterAdminId`),
  CONSTRAINT `application_5_ibfk_1` FOREIGN KEY (`ApplicationId`) REFERENCES `application` (`ApplicationId`),
  CONSTRAINT `application_5_ibfk_2` FOREIGN KEY (`PayslipAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_5_ibfk_3` FOREIGN KEY (`RAMCIAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_5_ibfk_4` FOREIGN KEY (`CTOSAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_5_ibfk_5` FOREIGN KEY (`RedemptionLetterAdminId`) REFERENCES `admin` (`AdminId`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `application_5` */

insert  into `application_5`(`Application5Id`,`ApplicationId`,`PayslipFileId`,`PayslipAdminId`,`PayslipLastUpdate`,`RAMCIFileId`,`RAMCIAdminId`,`RAMCILastUpdate`,`CTOSFileId`,`CTOSAdminId`,`CTOSLastUpdate`,`RedemptionLetterFileId`,`RedemptionLetterAdminId`,`RedemptionLetterLastUpdate`,`ApplicantAddress`,`BankruptcyStatus`,`LegalCase`,`HealthCreditScore`,`Commitements`) values (1,1,'d73db5df-f66f-4ac6-8b08-2eae7f75ab84',1,'2024-10-09 17:25:24','37097772-90ed-415f-84ec-b85e5279efb6',1,'2024-10-09 17:25:24','db6291c6-da19-46b6-8d15-98e1020cfe2a',1,'2024-10-09 17:25:24','27ee04c3-ce6c-4cfa-8453-4e308107dd13',1,'2024-10-09 17:25:24','a','b','b','d','e'),(2,2,'4fe75626-4ef7-47ad-8e8c-ad1f23cf1a2c',1,'2024-10-12 07:57:42',NULL,1,'2024-10-12 07:57:42','42da6cdb-f05a-418d-ab8f-c86254280085',1,'2024-10-12 07:57:42','47669764-8c54-4f2c-a71a-38232e437de5',1,'2024-10-12 07:57:42','awd','123','123','12','33'),(3,6,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(4,7,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(5,8,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'','','','',''),(6,9,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'','','','',''),(7,10,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(8,11,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);

/*Table structure for table `application_6` */

DROP TABLE IF EXISTS `application_6`;

CREATE TABLE `application_6` (
  `Application6Id` bigint NOT NULL AUTO_INCREMENT,
  `ApplicationID` bigint NOT NULL,
  `PaymentReceiptFileId` varchar(50) DEFAULT NULL,
  `PaymentReceiptAdminId` int DEFAULT NULL,
  `PaymentReceiptLastUpdate` datetime DEFAULT NULL,
  PRIMARY KEY (`Application6Id`),
  KEY `ApplicationId` (`ApplicationID`),
  KEY `PaymentReceiptAdminId` (`PaymentReceiptAdminId`),
  CONSTRAINT `application_6_ibfk_1` FOREIGN KEY (`ApplicationID`) REFERENCES `application` (`ApplicationId`),
  CONSTRAINT `application_6_ibfk_2` FOREIGN KEY (`PaymentReceiptAdminId`) REFERENCES `admin` (`AdminId`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `application_6` */

insert  into `application_6`(`Application6Id`,`ApplicationID`,`PaymentReceiptFileId`,`PaymentReceiptAdminId`,`PaymentReceiptLastUpdate`) values (1,1,NULL,1,'2024-10-10 14:34:50'),(2,2,'d67eb22e-d018-4e70-94b4-54cf8ce52009',1,'2024-10-12 08:17:12'),(3,6,NULL,NULL,NULL),(4,7,NULL,NULL,NULL),(5,8,NULL,NULL,NULL),(6,9,NULL,NULL,NULL),(7,10,NULL,NULL,NULL),(8,11,NULL,NULL,NULL);

/*Table structure for table `application_7` */

DROP TABLE IF EXISTS `application_7`;

CREATE TABLE `application_7` (
  `Application7Id` bigint NOT NULL AUTO_INCREMENT,
  `ApplicationId` bigint NOT NULL,
  `ReleaseLetterFileId` varchar(50) DEFAULT NULL,
  `ReleaseLetterAdminId` int DEFAULT NULL,
  `ReleaseLetterLastUpdate` datetime DEFAULT NULL,
  `CCRISReportFileId` varchar(50) DEFAULT NULL,
  `CCRISReportAdminId` int DEFAULT NULL,
  `CCRISReportLastUpdate` datetime DEFAULT NULL,
  PRIMARY KEY (`Application7Id`),
  KEY `ApplicationId` (`ApplicationId`),
  KEY `ReleaseLetterAdminId` (`ReleaseLetterAdminId`),
  KEY `CCRISReportAdminId` (`CCRISReportAdminId`),
  CONSTRAINT `application_7_ibfk_1` FOREIGN KEY (`ApplicationId`) REFERENCES `application` (`ApplicationId`),
  CONSTRAINT `application_7_ibfk_2` FOREIGN KEY (`ReleaseLetterAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_7_ibfk_3` FOREIGN KEY (`CCRISReportAdminId`) REFERENCES `admin` (`AdminId`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `application_7` */

insert  into `application_7`(`Application7Id`,`ApplicationId`,`ReleaseLetterFileId`,`ReleaseLetterAdminId`,`ReleaseLetterLastUpdate`,`CCRISReportFileId`,`CCRISReportAdminId`,`CCRISReportLastUpdate`) values (1,1,'a579c901-8b55-4923-b1ae-e8374e420153',1,'2024-10-10 16:30:09','4f78268f-c252-4a79-9792-0154995d95fe',1,'2024-10-10 16:31:45'),(2,2,'717499f2-6abd-484f-bb75-21c65eb83137',1,'2024-10-12 20:49:15',NULL,NULL,NULL),(3,6,NULL,NULL,NULL,NULL,NULL,NULL),(4,7,NULL,NULL,NULL,NULL,NULL,NULL),(5,8,NULL,NULL,NULL,NULL,NULL,NULL),(6,9,NULL,NULL,NULL,NULL,NULL,NULL),(7,10,NULL,NULL,NULL,NULL,NULL,NULL),(8,11,NULL,NULL,NULL,NULL,NULL,NULL);

/*Table structure for table `application_8` */

DROP TABLE IF EXISTS `application_8`;

CREATE TABLE `application_8` (
  `Application8Id` bigint NOT NULL AUTO_INCREMENT,
  `ApplicationId` bigint NOT NULL,
  `WorkerTypeId` int NOT NULL DEFAULT '1' COMMENT '1-govt 2-private',
  `IdentityCardFileId` varchar(50) DEFAULT NULL,
  `IdentityCardAdminId` int DEFAULT NULL,
  `IdentityCardMemberId` bigint DEFAULT NULL,
  `IdentityCardLastUpdate` datetime DEFAULT NULL,
  `PayslipFileId` varchar(50) DEFAULT NULL,
  `PayslipAdminId` int DEFAULT NULL,
  `PayslipMemberId` bigint DEFAULT NULL,
  `PayslipLastUpdate` datetime DEFAULT NULL,
  `ECFileId` varchar(50) DEFAULT NULL,
  `ECAdminId` int DEFAULT NULL,
  `ECMemberId` bigint DEFAULT NULL,
  `ECLastUpdate` datetime DEFAULT NULL,
  `HRMISFileId` varchar(50) DEFAULT NULL,
  `HRMISAdminId` int DEFAULT NULL,
  `HRMISMemberId` bigint DEFAULT NULL,
  `HRMISLastUpdate` datetime DEFAULT NULL,
  `BankStatementFileId` varchar(50) DEFAULT NULL,
  `BankStatementAdminId` int DEFAULT NULL,
  `BankStatementMemberId` bigint DEFAULT NULL,
  `BankStatementLastUpdate` datetime DEFAULT NULL,
  `LPPSAFileId` varchar(50) DEFAULT NULL,
  `LPPSAAdminId` int DEFAULT NULL,
  `LPPSAMemberId` bigint DEFAULT NULL,
  `LPPSALastUpdate` datetime DEFAULT NULL,
  `LicenseFileId` varchar(50) DEFAULT NULL,
  `LicenseAdminId` int DEFAULT NULL,
  `LicenseMemberId` bigint DEFAULT NULL,
  `LicenseLastUpdate` datetime DEFAULT NULL,
  `RedemptionLetterFileId` varchar(50) DEFAULT NULL,
  `RedemptionLetterAdminId` int DEFAULT NULL,
  `RedemptionLetterMemberId` bigint DEFAULT NULL,
  `RedemptionLetterLastUpdate` datetime DEFAULT NULL,
  `CCStatementFileId` varchar(50) DEFAULT NULL,
  `CCStatementAdminId` int DEFAULT NULL,
  `CCStatementMemberId` bigint DEFAULT NULL,
  `CCStatementLastUpdate` datetime DEFAULT NULL,
  `RAMCIFileId` varchar(50) DEFAULT NULL,
  `RAMCIAdminId` int DEFAULT NULL,
  `RAMCIMemberId` bigint DEFAULT NULL,
  `RAMCILastUpdate` datetime DEFAULT NULL,
  `SignatureFileId` varchar(50) DEFAULT NULL,
  `SignatureAdminId` int DEFAULT NULL,
  `SignatureMemberId` bigint DEFAULT NULL,
  `SignatureLastUpdate` datetime DEFAULT NULL,
  `BIROFileId` varchar(50) DEFAULT NULL,
  `BIROAdminId` int DEFAULT NULL,
  `BIROMemberId` bigint DEFAULT NULL,
  `BIROLastUpdate` datetime DEFAULT NULL,
  `KEW320FileId` varchar(50) DEFAULT NULL,
  `KEW320AdminId` int DEFAULT NULL,
  `KEW320MemberId` bigint DEFAULT NULL,
  `KEW320LastUpdate` datetime DEFAULT NULL,
  `StaffCardFileId` varchar(50) DEFAULT NULL,
  `StaffCardAdminId` int DEFAULT NULL,
  `StaffCardMemberId` bigint DEFAULT NULL,
  `StaffCardLastUpdate` datetime DEFAULT NULL,
  `PostDatedChequeFileId` varchar(50) DEFAULT NULL,
  `PostDatedChequeAdminId` int DEFAULT NULL,
  `PostDatedChequeMemberId` bigint DEFAULT NULL,
  `PostDatedChequeLastUpdate` datetime DEFAULT NULL,
  `CompanyConfirmationFileId` varchar(50) DEFAULT NULL,
  `CompanyConfirmationAdminId` int DEFAULT NULL,
  `CompanyConfirmationMemberId` bigint DEFAULT NULL,
  `CompanyConfirmationLastUpdate` datetime DEFAULT NULL,
  `EPFFileId` varchar(50) DEFAULT NULL,
  `EPFAdminId` int DEFAULT NULL,
  `EPFMemberId` bigint DEFAULT NULL,
  `EPFLastUpdate` datetime DEFAULT NULL,
  `EAFormFileId` varchar(50) DEFAULT NULL,
  `EAFormAdminId` int DEFAULT NULL,
  `EAFormMemberId` bigint DEFAULT NULL,
  `EAFormLastUpdate` datetime DEFAULT NULL,
  `BillUtilitiesFileId` varchar(50) DEFAULT NULL,
  `BillUtilitiesAdminId` int DEFAULT NULL,
  `BillUtilitiesMemberId` bigint DEFAULT NULL,
  `BillUtilitiesLastUpdate` datetime DEFAULT NULL,
  PRIMARY KEY (`Application8Id`),
  KEY `ApplicationId` (`ApplicationId`),
  KEY `IdentityCardAdminId` (`IdentityCardAdminId`),
  KEY `IdentityCardMemberId` (`IdentityCardMemberId`),
  KEY `PayslipAdminId` (`PayslipAdminId`),
  KEY `PayslipMemberId` (`PayslipMemberId`),
  KEY `ECAdminId` (`ECAdminId`),
  KEY `ECMemberId` (`ECMemberId`),
  KEY `HRMISAdminId` (`HRMISAdminId`),
  KEY `HRMISMemberId` (`HRMISMemberId`),
  KEY `BankStatementAdminId` (`BankStatementAdminId`),
  KEY `BankStatementMemberId` (`BankStatementMemberId`),
  KEY `LPPSAAdminId` (`LPPSAAdminId`),
  KEY `LPPSAMemberId` (`LPPSAMemberId`),
  KEY `LicenseAdminId` (`LicenseAdminId`),
  KEY `LicenseMemberId` (`LicenseMemberId`),
  KEY `RedemptionLetterAdminId` (`RedemptionLetterAdminId`),
  KEY `RedemptionLetterMemberId` (`RedemptionLetterMemberId`),
  KEY `CCStatementAdminId` (`CCStatementAdminId`),
  KEY `CCStatementMemberId` (`CCStatementMemberId`),
  KEY `RAMCIAdminId` (`RAMCIAdminId`),
  KEY `RAMCIMemberId` (`RAMCIMemberId`),
  KEY `SignatureAdminId` (`SignatureAdminId`),
  KEY `SignatureMemberId` (`SignatureMemberId`),
  KEY `BIROAdminId` (`BIROAdminId`),
  KEY `BIROMemberId` (`BIROMemberId`),
  KEY `KEW320AdminId` (`KEW320AdminId`),
  KEY `KEW320MemberId` (`KEW320MemberId`),
  KEY `StaffCardAdminId` (`StaffCardAdminId`),
  KEY `StaffCardMemberId` (`StaffCardMemberId`),
  KEY `PostDatedChequeAdminId` (`PostDatedChequeAdminId`),
  KEY `PostDatedChequeMemberId` (`PostDatedChequeMemberId`),
  KEY `CompanyConfirmationAdminId` (`CompanyConfirmationAdminId`),
  KEY `CompanyConfirmationMemberId` (`CompanyConfirmationMemberId`),
  KEY `EPFAdminId` (`EPFAdminId`),
  KEY `EPFMemberId` (`EPFMemberId`),
  KEY `EAFormAdminId` (`EAFormAdminId`),
  KEY `EAFormMemberId` (`EAFormMemberId`),
  KEY `BillUtilitiesAdminId` (`BillUtilitiesAdminId`),
  KEY `BillUtilitiesMemberId` (`BillUtilitiesMemberId`),
  CONSTRAINT `application_8_ibfk_1` FOREIGN KEY (`ApplicationId`) REFERENCES `application` (`ApplicationId`),
  CONSTRAINT `application_8_ibfk_10` FOREIGN KEY (`BankStatementAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_11` FOREIGN KEY (`BankStatementMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_8_ibfk_12` FOREIGN KEY (`LPPSAAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_13` FOREIGN KEY (`LPPSAMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_8_ibfk_14` FOREIGN KEY (`LicenseAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_15` FOREIGN KEY (`LicenseMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_8_ibfk_16` FOREIGN KEY (`RedemptionLetterAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_17` FOREIGN KEY (`RedemptionLetterMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_8_ibfk_18` FOREIGN KEY (`CCStatementAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_19` FOREIGN KEY (`CCStatementMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_8_ibfk_2` FOREIGN KEY (`IdentityCardAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_20` FOREIGN KEY (`RAMCIAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_21` FOREIGN KEY (`RAMCIMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_8_ibfk_22` FOREIGN KEY (`SignatureAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_23` FOREIGN KEY (`SignatureMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_8_ibfk_24` FOREIGN KEY (`BIROAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_25` FOREIGN KEY (`BIROMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_8_ibfk_26` FOREIGN KEY (`KEW320AdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_27` FOREIGN KEY (`KEW320MemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_8_ibfk_28` FOREIGN KEY (`StaffCardAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_29` FOREIGN KEY (`StaffCardMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_8_ibfk_3` FOREIGN KEY (`IdentityCardMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_8_ibfk_30` FOREIGN KEY (`PostDatedChequeAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_31` FOREIGN KEY (`PostDatedChequeMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_8_ibfk_32` FOREIGN KEY (`CompanyConfirmationAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_33` FOREIGN KEY (`CompanyConfirmationMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_8_ibfk_34` FOREIGN KEY (`EPFAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_35` FOREIGN KEY (`EPFMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_8_ibfk_36` FOREIGN KEY (`EAFormAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_37` FOREIGN KEY (`EAFormMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_8_ibfk_38` FOREIGN KEY (`BillUtilitiesAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_39` FOREIGN KEY (`BillUtilitiesMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_8_ibfk_4` FOREIGN KEY (`PayslipAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_5` FOREIGN KEY (`PayslipMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_8_ibfk_6` FOREIGN KEY (`ECAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_7` FOREIGN KEY (`ECMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `application_8_ibfk_8` FOREIGN KEY (`HRMISAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_8_ibfk_9` FOREIGN KEY (`HRMISMemberId`) REFERENCES `member` (`MemberId`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `application_8` */

insert  into `application_8`(`Application8Id`,`ApplicationId`,`WorkerTypeId`,`IdentityCardFileId`,`IdentityCardAdminId`,`IdentityCardMemberId`,`IdentityCardLastUpdate`,`PayslipFileId`,`PayslipAdminId`,`PayslipMemberId`,`PayslipLastUpdate`,`ECFileId`,`ECAdminId`,`ECMemberId`,`ECLastUpdate`,`HRMISFileId`,`HRMISAdminId`,`HRMISMemberId`,`HRMISLastUpdate`,`BankStatementFileId`,`BankStatementAdminId`,`BankStatementMemberId`,`BankStatementLastUpdate`,`LPPSAFileId`,`LPPSAAdminId`,`LPPSAMemberId`,`LPPSALastUpdate`,`LicenseFileId`,`LicenseAdminId`,`LicenseMemberId`,`LicenseLastUpdate`,`RedemptionLetterFileId`,`RedemptionLetterAdminId`,`RedemptionLetterMemberId`,`RedemptionLetterLastUpdate`,`CCStatementFileId`,`CCStatementAdminId`,`CCStatementMemberId`,`CCStatementLastUpdate`,`RAMCIFileId`,`RAMCIAdminId`,`RAMCIMemberId`,`RAMCILastUpdate`,`SignatureFileId`,`SignatureAdminId`,`SignatureMemberId`,`SignatureLastUpdate`,`BIROFileId`,`BIROAdminId`,`BIROMemberId`,`BIROLastUpdate`,`KEW320FileId`,`KEW320AdminId`,`KEW320MemberId`,`KEW320LastUpdate`,`StaffCardFileId`,`StaffCardAdminId`,`StaffCardMemberId`,`StaffCardLastUpdate`,`PostDatedChequeFileId`,`PostDatedChequeAdminId`,`PostDatedChequeMemberId`,`PostDatedChequeLastUpdate`,`CompanyConfirmationFileId`,`CompanyConfirmationAdminId`,`CompanyConfirmationMemberId`,`CompanyConfirmationLastUpdate`,`EPFFileId`,`EPFAdminId`,`EPFMemberId`,`EPFLastUpdate`,`EAFormFileId`,`EAFormAdminId`,`EAFormMemberId`,`EAFormLastUpdate`,`BillUtilitiesFileId`,`BillUtilitiesAdminId`,`BillUtilitiesMemberId`,`BillUtilitiesLastUpdate`) values (2,1,1,'735fa881-550e-45ea-9c6d-7098054c3d08',1,NULL,'2024-10-11 17:04:24','2e7b172f-c6d3-47e8-80c7-0b820b2bcf40',1,NULL,'2024-10-11 17:04:24','ea9437b6-d59d-46a9-8269-c96e1934a024',1,NULL,'2024-10-11 17:04:24','dbdea45b-cdd3-455e-96ef-fefdcf0a5644',1,NULL,'2024-10-11 17:04:24','b24bdc01-ad6b-42bb-bf24-51c71b123db4',1,NULL,'2024-10-11 17:04:24','ef3618ec-b3df-42cb-b218-6088ac1a8a03',1,NULL,'2024-10-11 17:04:24','39a504c9-b973-4488-a5d2-7aa9f80f34bc',1,NULL,'2024-10-11 17:04:24','73a038cd-1beb-4d29-951a-cfe8246e9425',1,NULL,'2024-10-11 17:04:24','874dcd8d-bf1d-45a3-a79b-ab9df296e807',1,NULL,'2024-10-11 17:04:24','92165326-f252-4979-9b7b-6aa72ac554c2',1,NULL,'2024-10-11 17:04:24','7ef95c02-84ee-4132-98cf-4987aa732440',1,NULL,'2024-10-11 17:04:24','75d5bd93-2c9b-4f08-b2ec-09925b43d8b3',1,NULL,'2024-10-11 17:04:24','1d815afe-4a3e-47e4-9c1e-02fef22019ef',1,NULL,'2024-10-11 17:04:24','ada7f89e-3e78-40d1-b3ee-373808682282',1,NULL,'2024-10-11 17:05:06','8e1e9a28-c3c8-4ec0-a398-679a5c1d651d',1,NULL,'2024-10-11 17:05:06','3e90b393-6d7b-43f5-81d7-e60f51fcc2d7',1,NULL,'2024-10-11 17:05:06','e04ae242-cc93-4508-8699-b90bd10577bf',1,NULL,'2024-10-11 17:05:06','54046603-dfb2-40f7-80a0-50e086de1e30',1,NULL,'2024-10-11 17:05:06','11f8d101-1ced-437d-a376-de3f1fa522d3',1,NULL,'2024-10-11 17:05:06'),(3,2,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(4,6,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(5,7,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(6,8,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(7,9,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(8,10,2,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(9,11,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);

/*Table structure for table `application_9` */

DROP TABLE IF EXISTS `application_9`;

CREATE TABLE `application_9` (
  `Application9Id` bigint NOT NULL AUTO_INCREMENT,
  `ApplicationId` bigint NOT NULL,
  `OfferLetterFileId` varchar(50) DEFAULT NULL,
  `OfferLetterAdminId` int DEFAULT NULL,
  `OfferLetterLastUpdate` datetime DEFAULT NULL,
  `ReloadStatusId` int DEFAULT NULL,
  `ApprovedDate` datetime DEFAULT NULL,
  `SigningDate` datetime DEFAULT NULL,
  `ApprovedAmount` decimal(13,2) DEFAULT NULL,
  PRIMARY KEY (`Application9Id`),
  KEY `ApplicationId` (`ApplicationId`),
  KEY `OfferLetterAdminId` (`OfferLetterAdminId`),
  CONSTRAINT `application_9_ibfk_1` FOREIGN KEY (`ApplicationId`) REFERENCES `application` (`ApplicationId`),
  CONSTRAINT `application_9_ibfk_2` FOREIGN KEY (`OfferLetterAdminId`) REFERENCES `admin` (`AdminId`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `application_9` */

insert  into `application_9`(`Application9Id`,`ApplicationId`,`OfferLetterFileId`,`OfferLetterAdminId`,`OfferLetterLastUpdate`,`ReloadStatusId`,`ApprovedDate`,`SigningDate`,`ApprovedAmount`) values (4,1,'490cbc3e-10db-4626-9346-3bc6976c9d0f',1,'2024-10-11 18:23:41',2,'2024-06-05 00:00:00','2024-10-30 00:00:00',NULL),(5,2,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(6,6,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(7,7,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(8,8,'e5d467b8-3738-44ab-aadd-33abf6a466ad',1,'2024-10-12 21:54:53',1,NULL,NULL,NULL),(9,9,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(10,10,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(11,11,NULL,NULL,NULL,1,'2024-10-30 00:00:00','2024-10-23 00:00:00','50000.00');

/*Table structure for table `application_document` */

DROP TABLE IF EXISTS `application_document`;

CREATE TABLE `application_document` (
  `ApplicationDocumentId` bigint NOT NULL AUTO_INCREMENT,
  `ApplicationId` bigint NOT NULL,
  `ApplicationStatusId` int DEFAULT NULL,
  `FileId` varchar(50) DEFAULT NULL,
  `Remark` varchar(500) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `CreateAdminId` int DEFAULT NULL,
  PRIMARY KEY (`ApplicationDocumentId`),
  KEY `ApplicationId` (`ApplicationId`),
  KEY `CreateAdminId` (`CreateAdminId`),
  CONSTRAINT `application_document_ibfk_1` FOREIGN KEY (`ApplicationId`) REFERENCES `application` (`ApplicationId`),
  CONSTRAINT `application_document_ibfk_2` FOREIGN KEY (`CreateAdminId`) REFERENCES `admin` (`AdminId`)
) ENGINE=InnoDB AUTO_INCREMENT=47 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `application_document` */

insert  into `application_document`(`ApplicationDocumentId`,`ApplicationId`,`ApplicationStatusId`,`FileId`,`Remark`,`CreateDate`,`CreateAdminId`) values (7,1,1,'4e32cc4e-8c10-4bbd-986a-484999e9479a','data keamanan siap2','2024-10-08 17:04:24',1),(9,1,1,'a1c160e7-7a42-42fe-8214-356f3aaa2766','iya','2024-10-09 15:17:35',1),(13,1,3,'0c767db8-4586-4769-8f21-fdded57e7434','siap','2024-10-09 15:25:53',1),(14,1,4,'aa2709e0-42eb-480f-9501-11b9dbe9616c','app nya','2024-10-09 16:15:42',1),(16,1,5,'76b8357a-a022-4fa0-a9ba-aa3f818e4093','iaaa','2024-10-09 17:25:24',1),(18,1,6,'0c715ac8-220e-4948-87f4-4af0efbbfd3a','kkdls','2024-10-10 15:33:33',1),(19,1,7,'cdf77669-b2a3-4b35-a61e-b875b1d0cf0a','awd','2024-10-10 16:30:09',1),(20,1,8,'8b820187-9dc0-47e8-97f4-6f3a3a625089','oke siap','2024-10-11 16:46:45',1),(21,1,8,'64edde3a-7a15-4f15-b0fb-2f33490530bb','mantap sekali app','2024-10-11 17:04:24',1),(22,1,9,'792a0263-57ee-479b-9333-c9620b4d0435','','2024-10-11 18:34:17',1),(23,1,10,'bd977fed-9054-4fa1-9083-f704cf062b5b','eik','2024-10-11 22:02:06',1),(24,2,1,'c0f308ed-3c81-4f4e-8177-34a63e9b5faf','akai','2024-10-12 07:27:45',1),(25,2,3,'cabcea07-41d0-4dd2-80ea-accb83e61d4f','ppp','2024-10-12 07:56:19',1),(26,2,3,'3d95d09b-b3b1-44ad-ab9a-193905af88a0','oke','2024-10-12 07:56:32',1),(27,2,4,'e9e6293b-b08f-4d96-a090-32e6c5e56400','pl','2024-10-12 07:56:55',1),(29,2,6,'3576732f-36db-4a15-8439-fa82a49a5112','','2024-10-12 08:17:12',1),(30,2,10,'ff265abb-aa3a-42d2-9871-8f017e6d12aa','awd','2024-10-12 20:49:15',1),(31,2,7,'2b389783-a73d-49de-b015-2251d01cac12','status 3','2024-10-12 20:57:37',1),(32,8,1,'584ee6c8-ac89-4363-ae58-32641c2da48c','ke 1','2024-10-12 21:30:05',1),(33,8,1,'a5ca5532-da87-485f-a66d-911f55143036','ke 2','2024-10-12 21:34:58',1),(34,8,3,'9f5fb6f2-bbe7-4147-87f9-e536c7ffbbf3','ke 3','2024-10-12 21:38:53',1),(35,8,4,'4ec2539a-d23e-4169-a49a-fcef95b0e7fb','ke 4 deh','2024-10-12 21:39:07',1),(36,8,5,'30104ffe-6196-425f-a991-4b074dfb0944','ke 5','2024-10-12 21:39:30',1),(37,8,6,'2dc46cd7-bba8-4e04-bfa6-20f123a34113','ke 6 ada 1','2024-10-12 21:40:00',1),(38,8,6,'c4350afb-c8b5-4b99-b407-34f36b774f8c','ke 6 ada 2','2024-10-12 21:40:00',1),(39,8,7,'5ed12649-bb8a-43e7-82c9-608c87b7ceb4','ke 7','2024-10-12 21:40:19',1),(40,8,8,'7d93a7b5-193d-497c-a9e3-952f718e13c2','ke 8','2024-10-12 21:40:35',1),(41,8,9,'d25f9c9a-5e0f-4f9d-adc5-5b628e3bdf0a','iyah ke 9 aah','2024-10-12 21:40:47',1),(42,8,10,'628524ef-c651-460b-aac7-968a10f828a2','ke 10 finish','2024-10-12 21:41:06',1),(43,11,1,'6475a13d-956c-4036-8c8e-96ac23341de7','123','2024-10-17 08:16:14',1),(44,11,1,'bc557e91-363e-4aa8-be91-88cd0c1a7f06','123','2024-10-17 08:16:14',1),(45,11,3,'018bae9c-dcb3-4bcd-96c7-f1562af97452','awd','2024-10-17 08:50:27',1),(46,11,9,'3a42e502-7d7b-400d-9065-55db12caf164','awd','2024-10-17 09:07:29',1);

/*Table structure for table `application_file` */

DROP TABLE IF EXISTS `application_file`;

CREATE TABLE `application_file` (
  `ApplicationFileId` bigint NOT NULL AUTO_INCREMENT,
  `ApplicationId` bigint NOT NULL,
  `FileId` varchar(50) DEFAULT NULL,
  `GROUP` varchar(50) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `CreateAdminId` int DEFAULT NULL,
  `CreateMemberId` bigint DEFAULT NULL,
  PRIMARY KEY (`ApplicationFileId`),
  KEY `ApplicationId` (`ApplicationId`),
  KEY `CreateAdminId` (`CreateAdminId`),
  KEY `CreateMemberId` (`CreateMemberId`),
  CONSTRAINT `application_file_ibfk_1` FOREIGN KEY (`ApplicationId`) REFERENCES `application` (`ApplicationId`),
  CONSTRAINT `application_file_ibfk_2` FOREIGN KEY (`CreateAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `application_file_ibfk_3` FOREIGN KEY (`CreateMemberId`) REFERENCES `member` (`MemberId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `application_file` */

insert  into `application_file`(`ApplicationFileId`,`ApplicationId`,`FileId`,`GROUP`,`CreateDate`,`CreateAdminId`,`CreateMemberId`) values (1,10,'48879968-92ea-4237-bbcc-a8b303e980e2','payslip','2024-10-16 11:32:59',1,NULL),(4,10,'ab28657e-61e3-4b0a-95f1-56bc70a4d486','bank_statement','2024-10-16 11:51:43',1,NULL),(5,10,'0fa54066-f7f7-43c5-8b81-32ee406a4e2b','bank_statement','2024-10-16 11:51:43',1,NULL),(6,10,'a54ca41e-7197-4a32-b329-05011a6f457c','payslip','2024-10-16 11:56:11',1,NULL);

/*Table structure for table `audiencegroup` */

DROP TABLE IF EXISTS `audiencegroup`;

CREATE TABLE `audiencegroup` (
  `AudienceGroupId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `StatusId` int DEFAULT NULL,
  PRIMARY KEY (`AudienceGroupId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `audiencegroup` */

insert  into `audiencegroup`(`AudienceGroupId`,`Name`,`StatusId`) values (1,'aa',1);

/*Table structure for table `audiencegroup_tag` */

DROP TABLE IF EXISTS `audiencegroup_tag`;

CREATE TABLE `audiencegroup_tag` (
  `AudienceGroupTagId` int NOT NULL AUTO_INCREMENT,
  `AudienceGroupId` int NOT NULL,
  `TagId` int NOT NULL,
  PRIMARY KEY (`AudienceGroupTagId`),
  KEY `AudienceGroupId` (`AudienceGroupId`),
  KEY `TagId` (`TagId`),
  CONSTRAINT `audiencegroup_tag_ibfk_1` FOREIGN KEY (`AudienceGroupId`) REFERENCES `audiencegroup` (`AudienceGroupId`),
  CONSTRAINT `audiencegroup_tag_ibfk_2` FOREIGN KEY (`TagId`) REFERENCES `tag` (`TagId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `audiencegroup_tag` */

insert  into `audiencegroup_tag`(`AudienceGroupTagId`,`AudienceGroupId`,`TagId`) values (1,1,1);

/*Table structure for table `bank` */

DROP TABLE IF EXISTS `bank`;

CREATE TABLE `bank` (
  `BankId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `StatusId` int NOT NULL DEFAULT '1',
  `CountryId` int DEFAULT NULL,
  `SwiftCode` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`BankId`),
  KEY `CountryId` (`CountryId`),
  CONSTRAINT `bank_ibfk_1` FOREIGN KEY (`CountryId`) REFERENCES `country` (`CountryId`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8;

/*Data for the table `bank` */

insert  into `bank`(`BankId`,`Name`,`StatusId`,`CountryId`,`SwiftCode`) values (1,'MayBank',1,1,NULL),(2,'CIMB',1,1,NULL),(3,'Public Bank',1,1,NULL),(4,'RHB Bank',1,1,NULL),(5,'Hong Leong Bank',1,1,NULL),(6,'AmBank',1,1,NULL),(7,'UOB Malaysia',1,1,NULL),(8,'Bank Rakyat',1,1,NULL),(9,'OCBC Bank Malaysia',1,1,NULL),(10,'HSBC Bank Malaysia',1,1,NULL),(11,'Bank Islam Malaysia',1,1,NULL),(12,'Affin Bank',1,1,NULL),(13,'Alliance Bank Malaysia',1,1,NULL),(14,'Standard Chartered Bank Malaysia',1,1,NULL),(15,'MBSB Berhad',1,1,NULL),(16,'Citibank Malaysia',1,1,NULL),(17,'Bank Simpanan Nasional',1,1,NULL),(18,'Bank Muamalat Malaysia',1,1,NULL),(19,'Agrobank',1,1,NULL),(20,'Al-Rajhi Malaysia',1,1,NULL);

/*Table structure for table `campaign` */

DROP TABLE IF EXISTS `campaign`;

CREATE TABLE `campaign` (
  `CampaignId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) DEFAULT NULL,
  `Subject` varchar(255) DEFAULT NULL,
  `EmailTemplateId` int NOT NULL,
  `CreateDate` datetime NOT NULL,
  `ScheduledDate` datetime DEFAULT NULL,
  `FinishDate` datetime DEFAULT NULL,
  `AudienceGroupId` int DEFAULT NULL,
  `StatusId` int DEFAULT NULL COMMENT '1-Draft 2-Active 3-Inactive 4-Sending 5-Completed',
  PRIMARY KEY (`CampaignId`),
  KEY `EmailTemplateId` (`EmailTemplateId`),
  KEY `AudienceGroupId` (`AudienceGroupId`),
  CONSTRAINT `campaign_ibfk_1` FOREIGN KEY (`EmailTemplateId`) REFERENCES `emailtemplate` (`EmailTemplateId`),
  CONSTRAINT `campaign_ibfk_2` FOREIGN KEY (`AudienceGroupId`) REFERENCES `audiencegroup` (`AudienceGroupId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `campaign` */

insert  into `campaign`(`CampaignId`,`Name`,`Subject`,`EmailTemplateId`,`CreateDate`,`ScheduledDate`,`FinishDate`,`AudienceGroupId`,`StatusId`) values (1,'camp','camp sub',5,'2024-07-20 17:36:07',NULL,NULL,NULL,1),(2,'camp2','camp2 sub',5,'2024-07-20 17:36:29',NULL,NULL,NULL,1),(3,'camp33','camp3 sub3',5,'2024-07-20 17:37:32','2024-07-23 12:45:00',NULL,1,1);

/*Table structure for table `caseupdate` */

DROP TABLE IF EXISTS `caseupdate`;

CREATE TABLE `caseupdate` (
  `CaseUpdateId` bigint NOT NULL AUTO_INCREMENT,
  `ApplicationId` bigint NOT NULL,
  `BankId` int DEFAULT NULL,
  `LoanAmount` decimal(13,2) DEFAULT NULL,
  `SubmitDate` datetime DEFAULT NULL,
  `Banker` varchar(255) DEFAULT NULL,
  `CompleteStatusId` int DEFAULT NULL,
  `Consolidate` varchar(255) DEFAULT NULL,
  `CashNet` decimal(13,2) DEFAULT NULL,
  `Instalment` decimal(13,2) DEFAULT NULL,
  `ApprovedDate` datetime DEFAULT NULL,
  `SignDate` datetime DEFAULT NULL,
  `DisbursementDate` datetime DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `LoanAccountNumber` varchar(100) DEFAULT NULL,
  `1stDueDate` datetime DEFAULT NULL,
  `Remarkds` varchar(500) DEFAULT NULL,
  `AdminId` int DEFAULT NULL,
  PRIMARY KEY (`CaseUpdateId`),
  KEY `ApplicationId` (`ApplicationId`),
  KEY `BankId` (`BankId`),
  KEY `AdminId` (`AdminId`),
  CONSTRAINT `caseupdate_ibfk_1` FOREIGN KEY (`ApplicationId`) REFERENCES `application` (`ApplicationId`),
  CONSTRAINT `caseupdate_ibfk_2` FOREIGN KEY (`BankId`) REFERENCES `bank` (`BankId`),
  CONSTRAINT `caseupdate_ibfk_3` FOREIGN KEY (`AdminId`) REFERENCES `admin` (`AdminId`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `caseupdate` */

insert  into `caseupdate`(`CaseUpdateId`,`ApplicationId`,`BankId`,`LoanAmount`,`SubmitDate`,`Banker`,`CompleteStatusId`,`Consolidate`,`CashNet`,`Instalment`,`ApprovedDate`,`SignDate`,`DisbursementDate`,`UpdateDate`,`LoanAccountNumber`,`1stDueDate`,`Remarkds`,`AdminId`) values (6,1,12,NULL,NULL,'',0,'',NULL,NULL,NULL,NULL,NULL,NULL,'',NULL,'',1),(7,2,19,'1.00','2024-10-05 00:00:00','3',1,'4','5.00','6.00','2024-10-01 00:00:00','2024-10-02 00:00:00','2024-10-03 00:00:00','2024-10-04 00:00:00','2','2024-10-12 00:00:00','2',1),(10,2,11,'21313.00','2024-12-10 00:00:00','banker',0,'1313132','132131.00','31321.00','2024-12-10 00:00:00','2024-12-10 00:00:00','2024-12-10 00:00:00','2024-12-10 00:00:00','213213','2024-12-10 00:00:00','331',1);

/*Table structure for table `country` */

DROP TABLE IF EXISTS `country`;

CREATE TABLE `country` (
  `CountryId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Iso2` varchar(2) DEFAULT NULL,
  `Currency` varchar(5) DEFAULT NULL,
  `PhoneCode` int DEFAULT NULL,
  `IsActive` int NOT NULL,
  PRIMARY KEY (`CountryId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*Data for the table `country` */

insert  into `country`(`CountryId`,`Name`,`Iso2`,`Currency`,`PhoneCode`,`IsActive`) values (1,'Malaysia','MY','MYR',60,1);

/*Table structure for table `email` */

DROP TABLE IF EXISTS `email`;

CREATE TABLE `email` (
  `EmailId` bigint NOT NULL AUTO_INCREMENT,
  `CampaignId` int DEFAULT NULL,
  `Subject` varchar(500) DEFAULT NULL,
  `To` varchar(255) DEFAULT NULL,
  `Body` text,
  `CreateDate` datetime NOT NULL,
  `SentDate` datetime DEFAULT NULL,
  `Cc` text,
  `Bcc` text,
  `AttachmentFilename` varchar(255) DEFAULT NULL,
  `AttachmentFileId` varchar(50) DEFAULT NULL,
  `AttachmentFileExtension` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`EmailId`),
  KEY `CampaignId` (`CampaignId`),
  KEY `AttachmentFileId` (`AttachmentFileId`),
  CONSTRAINT `email_ibfk_1` FOREIGN KEY (`CampaignId`) REFERENCES `campaign` (`CampaignId`),
  CONSTRAINT `email_ibfk_2` FOREIGN KEY (`AttachmentFileId`) REFERENCES `file` (`FileId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `email` */

/*Table structure for table `emailtemplate` */

DROP TABLE IF EXISTS `emailtemplate`;

CREATE TABLE `emailtemplate` (
  `EmailTemplateId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `TemplateTypeId` int NOT NULL COMMENT '1-Html 2-PlainText',
  `CreateDate` datetime DEFAULT NULL,
  `Content` text,
  `StatusId` int DEFAULT NULL,
  PRIMARY KEY (`EmailTemplateId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

/*Data for the table `emailtemplate` */

insert  into `emailtemplate`(`EmailTemplateId`,`Name`,`TemplateTypeId`,`CreateDate`,`Content`,`StatusId`) values (5,'Example',1,'2024-07-19 09:24:41','<p>as<strong>asdawdddd</strong></p>',1),(6,'awd',1,'2024-10-14 10:26:35','<p>awdawd</p><p><br></p><p><br></p><p>awd</p><p>a</p><p>wd</p><p>aw</p><p>d</p>',1);

/*Table structure for table `file` */

DROP TABLE IF EXISTS `file`;

CREATE TABLE `file` (
  `FileId` varchar(50) NOT NULL,
  `Filename` varchar(255) DEFAULT NULL,
  `Extension` varchar(50) DEFAULT NULL,
  `Size` double DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  PRIMARY KEY (`FileId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `file` */

insert  into `file`(`FileId`,`Filename`,`Extension`,`Size`,`CreateDate`) values ('018bae9c-dcb3-4bcd-96c7-f1562af97452','TONI (1) (1) (3).png','.png',15715,'2024-10-17 08:50:27'),('054ebd56-3054-4c3f-9319-40f5d8b21ba8','BAKSOS2024 (1) (1).xlsx','.xlsx',21154,NULL),('0afd76a8-fb42-4996-8946-6a3fa0a298d4','wa.png','.png',167456,NULL),('0c715ac8-220e-4948-87f4-4af0efbbfd3a','TONI (2) (1).png','.png',14525,'2024-10-10 15:33:33'),('0c767db8-4586-4769-8f21-fdded57e7434','Application Agreement Document (1) (1).docx','.docx',11248,'2024-10-09 15:25:53'),('0ec61f57-8422-437b-9286-11b95379fc76','wa.png','.png',14525,NULL),('0fa54066-f7f7-43c5-8b81-32ee406a4e2b','Template Surat Akuan.txt','.txt',1245,'2024-10-16 11:51:43'),('10775c0e-9923-4d6a-9d67-c4b2ceffe1e6','TONI.png','.png',14525,NULL),('11f8d101-1ced-437d-a376-de3f1fa522d3','TONI (2) (1).png','.png',14525,'2024-10-11 17:05:06'),('1342f5af-91a3-4e85-84e1-fba30762f05a','Application Agreement Document (1) (1).docx','.docx',11248,NULL),('14b3c236-3421-4793-b079-468f93ae4e2b','TONI.png','.png',14525,'2024-10-12 07:53:32'),('17067be6-9ab3-473b-a648-e0af75aa250d','Composer-Setup.exe','.exe',1804192,'2024-10-10 14:11:43'),('1a8d6d33-3d1b-4b9d-b79e-291f1018ad99','wa.png','.png',167456,NULL),('1d815afe-4a3e-47e4-9c1e-02fef22019ef','TONI (2) (1).png','.png',14525,'2024-10-11 17:04:24'),('1f0ff9dd-d310-4427-ae31-8ddc7b003259','wa (2).png','.png',14525,'2024-10-11 16:50:17'),('26af01ed-e7a7-483e-8bc6-d914bc1a94b2','TONI (2) (1).png','.png',14525,'2024-10-11 16:50:56'),('27ee04c3-ce6c-4cfa-8453-4e308107dd13','download (1).htm','.htm',1452439,'2024-10-09 17:25:24'),('2ac0682a-8484-4a9d-92b9-6b97c856566a','C360_2017-09-21-15-43-26-973.jpg','.png',376198,NULL),('2b389783-a73d-49de-b015-2251d01cac12','TONI (1) (1).png','.png',15715,'2024-10-12 20:57:37'),('2b79e107-932d-4503-b299-aabbc645823d','wa (2).png','.png',14525,'2024-10-11 22:05:35'),('2dc46cd7-bba8-4e04-bfa6-20f123a34113','TONI (2).png','.png',14525,'2024-10-12 21:40:00'),('2e7b172f-c6d3-47e8-80c7-0b820b2bcf40','wa (2).png','.png',14525,'2024-10-11 17:04:24'),('2f94c608-6d18-41c4-bfef-307ca430031a','C360_2017-09-21-15-43-26-973.jpg','.jpg',14525,'2024-10-12 07:57:42'),('30104ffe-6196-425f-a991-4b074dfb0944','TONI (1) (1) (1).png','.png',15715,'2024-10-12 21:39:30'),('3061957d-be98-4ed0-8b8a-a1816ce636ec','wa.png','.png',14525,'2024-10-12 07:27:45'),('30cbf278-40d8-47f9-94a4-8f9c63f0a203','e5f754f30c122c7c7ac7a94eccd69ddd.png','.png',213100,NULL),('33804cac-40ad-4406-96a6-d9bf30e875b8','Application Agreement Document (1).docx','.docx',11248,NULL),('3576732f-36db-4a15-8439-fa82a49a5112','TONI (1) (1).png','.png',15715,'2024-10-12 08:17:12'),('357ff03c-258e-4585-9ed6-3db240391679','TONI.png','.png',14525,NULL),('3655661e-f6de-4ae2-bb67-c01cf0ee73c6','C360_2017-09-21-15-43-26-973.jpg','.jpg',376198,NULL),('37097772-90ed-415f-84ec-b85e5279efb6','Application Agreement Document (1) (1) (1).docx','.docx',11248,'2024-10-09 17:25:24'),('39a504c9-b973-4488-a5d2-7aa9f80f34bc','TONI (2) (1).png','.png',14525,'2024-10-11 17:04:24'),('3a42e502-7d7b-400d-9065-55db12caf164','TONI (1) (1) (3) (1).png','.png',15715,'2024-10-17 09:07:29'),('3d875dfa-a732-4c43-ad2b-5d1a1deb2533','wa.png','.png',167456,NULL),('3d95d09b-b3b1-44ad-ab9a-193905af88a0','TONI (1) (1).png','.png',15715,'2024-10-12 07:56:32'),('3e90b393-6d7b-43f5-81d7-e60f51fcc2d7','wa (2).png','.png',14525,'2024-10-11 17:05:06'),('3f8b3734-f980-45c7-a610-b62cfdc97243','wa.png','.png',167456,NULL),('41438f83-bc80-425b-845d-6e87f5e9828f','dimensiss docs.zip','.zip',665774,NULL),('42da6cdb-f05a-418d-ab8f-c86254280085','TONI (1) (1).png','.png',15715,'2024-10-12 07:57:42'),('4307b614-1688-4fde-97de-0e567c7ff879','BAKSOS2024 (1).xlsx','.xlsx',21154,NULL),('434d4bb3-b0d0-48b6-8f23-61ba23325ffa','Screenshot_12 (1).png','.png',49026,NULL),('4368c2dc-1474-4852-966c-64cd9c1c27fa','Mengenal-Lorem-Ipsum.jpg','.jpg',113642,NULL),('47669764-8c54-4f2c-a71a-38232e437de5','TONI (1) (1).png','.png',15715,'2024-10-12 07:57:42'),('48879968-92ea-4237-bbcc-a8b303e980e2','TONI (1) (1) (3) (1).png','.png',15715,'2024-10-16 11:32:59'),('490cbc3e-10db-4626-9346-3bc6976c9d0f','TONI (2) (1).png','.png',14525,'2024-10-11 18:23:41'),('49aeba62-6dc1-4ebd-bdd7-ee49ab28f9a6','BAKSOS2024 (1) (1).xlsx','.xlsx',21154,NULL),('49c35cd2-667d-452d-a559-e60d9a97979f','BAKSOS2024 (1) (1).xlsx','.xlsx',21154,NULL),('4a2fc4fc-2393-414e-9383-8b6bf9c40349','e5f754f30c122c7c7ac7a94eccd69ddd.png','.png',213100,NULL),('4e32cc4e-8c10-4bbd-986a-484999e9479a','BAKSOS2024 (1).xlsx','.xlsx',21154,NULL),('4e8f4dcb-4f77-4ba6-a7aa-69e0c4dce938','TONI (1) (1).png','.png',15715,'2024-10-12 07:54:00'),('4ec2539a-d23e-4169-a49a-fcef95b0e7fb','wa (1).png','.png',14525,'2024-10-12 21:39:07'),('4f78268f-c252-4a79-9792-0154995d95fe','wa (2).png','.png',14525,'2024-10-10 16:31:45'),('4fca784e-005e-40f7-b5da-7205bc8e738a','e5f754f30c122c7c7ac7a94eccd69ddd.png','.png',213100,NULL),('4fe75626-4ef7-47ad-8e8c-ad1f23cf1a2c','wa.png','.png',14525,'2024-10-12 07:57:42'),('51ae7111-da81-4d80-bcde-d8d8ad5e728c','TONI (1).png','.png',15715,NULL),('54046603-dfb2-40f7-80a0-50e086de1e30','wa (2).png','.png',14525,'2024-10-11 17:05:06'),('57c381bf-0b1e-4284-b8e2-3b1163003d4d','wa (1).png','.png',14525,NULL),('57cd118d-b921-4654-95d5-4fceabf59b84','wa (2).png','.png',14525,'2024-10-11 22:05:35'),('584ee6c8-ac89-4363-ae58-32641c2da48c','TONI (1) (1) (3) (1).png','.png',15715,'2024-10-12 21:30:05'),('5ea952c1-2f41-4533-99b2-2dd0e134c88a','TONI.png','.png',14525,NULL),('5ed12649-bb8a-43e7-82c9-608c87b7ceb4','TONI (1) (1) (1).png','.png',15715,'2024-10-12 21:40:19'),('628524ef-c651-460b-aac7-968a10f828a2','TONI (1) (1) (2).png','.png',15715,'2024-10-12 21:41:06'),('630174d3-1b73-4179-8713-96f42f3bc713','C360_2017-09-21-15-43-26-973.jpg','.jpg',376198,NULL),('6475a13d-956c-4036-8c8e-96ac23341de7','WhatsApp Image 2024-10-11 at 07.09.42.jpeg','.jpeg',73473,'2024-10-17 08:16:14'),('64edde3a-7a15-4f15-b0fb-2f33490530bb','BAKSOS2024 (1) (1) (2).xlsx','.xlsx',21154,'2024-10-11 17:04:24'),('6a6fde9f-4a54-4b5d-80c6-722a8b0fa80e','C360_2017-09-21-15-43-26-973.jpg','.jpg',376198,NULL),('6dd10803-9f2e-487d-b805-9bd811b19c1d','BAKSOS2024 (1) (1).xlsx','.xlsx',21154,NULL),('6ff1bb50-9e0a-4f86-a381-19b310e02f7e','wa.png','.png',14525,NULL),('717499f2-6abd-484f-bb75-21c65eb83137','wa (1).png','.png',14525,'2024-10-12 20:49:15'),('735fa881-550e-45ea-9c6d-7098054c3d08','TONI (2) (1).png','.png',14525,'2024-10-11 17:04:24'),('73a038cd-1beb-4d29-951a-cfe8246e9425','wa (2).png','.png',14525,'2024-10-11 17:04:24'),('750cfa6a-4ea5-4a4c-9054-edaea148b964','C360_2017-09-21-15-43-26-973.jpg','.jpg',376198,NULL),('75d5bd93-2c9b-4f08-b2ec-09925b43d8b3','wa (2).png','.png',14525,'2024-10-11 17:04:24'),('76b8357a-a022-4fa0-a9ba-aa3f818e4093','flatpickr-master.zip','.zip',248987,'2024-10-09 17:25:24'),('779d9cf3-7b7e-4ff1-811d-cb9c74ecacb5','TONI (1) (1) (3) (1) (1).png','.png',15715,'2024-10-16 11:50:56'),('792a0263-57ee-479b-9333-c9620b4d0435','TONI (2) (1).png','.png',14525,'2024-10-11 18:34:17'),('7abbff66-9b21-43f4-b3f5-71a12b068460','wa.png','.png',167456,NULL),('7ce7bdae-9f78-45a4-b7f7-70320c7bc4f8','C360_2017-09-21-15-43-26-973.jpg','.jpg',14525,NULL),('7d330fc4-a045-4daa-a918-8cd939d2466e','e5f754f30c122c7c7ac7a94eccd69ddd.png','.png',213100,NULL),('7d93a7b5-193d-497c-a9e3-952f718e13c2','TONI (1) (1) (1).png','.png',15715,'2024-10-12 21:40:35'),('7ef95c02-84ee-4132-98cf-4987aa732440','TONI (2) (1).png','.png',14525,'2024-10-11 17:04:24'),('8098c2e9-11b2-4c95-927d-d1c3646ab99e','Application Agreement Document (1) (1).docx','.docx',11248,NULL),('8217f513-c90c-46e7-802d-2f80ce202007','TONI.png','.png',14525,'2024-10-12 07:50:56'),('826b88bd-36a0-4655-9e0e-b9b14110caf2','IMG_20170910_123126_HDR.jpg','.jpg',354146,NULL),('82a04d3c-8fad-425a-85b1-c2efdbf0f05b','TONI (2) (1).png','.png',14525,'2024-10-11 16:50:56'),('851171cc-5961-4176-90ad-ff6d64fec7ba','BAKSOS2024 (1) (1).xlsx','.xlsx',21154,'2024-10-09 17:22:25'),('873c55d8-2a29-4e46-b4a4-da6a9e6aca71','wa.png','.png',167456,NULL),('874dcd8d-bf1d-45a3-a79b-ab9df296e807','TONI (2) (1).png','.png',14525,'2024-10-11 17:04:24'),('8acb94e0-72a0-4869-b882-5d0b418f0a54','TONI (2).png','.png',14525,NULL),('8b820187-9dc0-47e8-97f4-6f3a3a625089','TONI (2) (1).png','.png',14525,'2024-10-11 16:46:45'),('8e03da18-1691-4a90-9d63-4c9087bb677f','BAKSOS2024 (1) (1) (1).xlsx','.xlsx',21154,NULL),('8e1e9a28-c3c8-4ec0-a398-679a5c1d651d','TONI (2) (1).png','.png',14525,'2024-10-11 17:05:06'),('8f2051f7-dc5a-4a11-9e0f-c8c0fa827242','Application Agreement Document (1) (1).docx','.docx',11248,'2024-10-09 17:22:25'),('9041bd82-8f5b-4adf-8afc-abc513218213','wa.png','.png',14525,'2024-10-09 17:22:25'),('92165326-f252-4979-9b7b-6aa72ac554c2','wa (2).png','.png',14525,'2024-10-11 17:04:24'),('93077beb-6655-4ead-b97f-51d9ea6af295','TONI (2).png','.png',14525,'2024-10-09 17:22:25'),('94e0cccb-4c3a-4db7-bde8-ef215073e040','bootstrap-5.3.3-dist.zip','.zip',1502696,'2024-10-10 14:34:50'),('9680cd60-8cfc-40cc-8ab0-20235d2ee390','BAKSOS2024 (1) (1).xlsx','.xlsx',21154,NULL),('9c179796-e9a3-4896-ad58-db1822dacc14','wa (2).png','.png',14525,'2024-10-11 17:01:47'),('9c74a5f1-aea6-4af3-81f0-9bce9c73a7d0','BAKSOS2024 (1) (1).xlsx','.xlsx',21154,NULL),('9d802672-c60e-478c-9a01-3340fa1bbebe','wa (2).png','.png',14525,'2024-10-11 16:50:56'),('9f5fb6f2-bbe7-4147-87f9-e536c7ffbbf3','TONI (1) (1) (1).png','.png',15715,'2024-10-12 21:38:53'),('a0ee5378-484c-4234-9335-65737adf6366','wa (1).png','.png',14525,'2024-10-09 15:24:05'),('a1c160e7-7a42-42fe-8214-356f3aaa2766','Application Agreement Document (1) (1).docx','.docx',11248,NULL),('a310dad4-1f36-4cbd-8811-010dbf62086a','TONI (2) (1).png','.png',14525,'2024-10-11 16:50:17'),('a54ca41e-7197-4a32-b329-05011a6f457c','WhatsApp Image 2024-10-11 at 07.09.42.jpeg','.jpeg',73473,'2024-10-16 11:56:11'),('a579c901-8b55-4923-b1ae-e8374e420153','TONI (2) (1).png','.png',14525,'2024-10-10 16:30:09'),('a598bea6-fc20-4b10-bdb6-6dfb3736f873','wa.png','.png',167456,NULL),('a5ca5532-da87-485f-a66d-911f55143036','wa (1).png','.png',14525,'2024-10-12 21:34:58'),('a6a7a609-8c8a-4696-a1dc-95801afa2939','wa (2).png','.png',14525,'2024-10-11 16:50:56'),('a7dbc15d-dedf-40d7-9ae2-863cd0311c06','wa.png','.png',14525,NULL),('aa216222-6868-4fe5-9f72-a1585878e74a','e5f754f30c122c7c7ac7a94eccd69ddd.png','.png',213100,NULL),('aa2709e0-42eb-480f-9501-11b9dbe9616c','dimensiss docs.zip','.zip',665774,'2024-10-09 16:15:42'),('ab28657e-61e3-4b0a-95f1-56bc70a4d486','wa (1) (1).png','.png',14525,'2024-10-16 11:51:43'),('ac904c7c-d9ef-4dff-83de-ad371616c4b8','TONI (1).png','.png',15715,NULL),('acbeefd9-f329-4a3e-bdd2-ece4a089739c','PNG.png','.png',207770,NULL),('ada7f89e-3e78-40d1-b3ee-373808682282','wa (2).png','.png',14525,'2024-10-11 17:05:06'),('afce38d6-c600-4ec2-a2a1-18f2b84de445','e5f754f30c122c7c7ac7a94eccd69ddd.png','.png',213100,NULL),('b0978af0-f123-4c52-97a5-9dc5323d02b4','TONI (2) (1) (1).png','.png',14525,'2024-10-11 22:05:35'),('b24bdc01-ad6b-42bb-bf24-51c71b123db4','TONI (2) (1).png','.png',14525,'2024-10-11 17:04:24'),('b65e1706-5f94-4fe1-b2e2-43ad49945080','TONI (2) (1) (1).png','.png',14525,'2024-10-11 22:03:36'),('b694d1d9-3a37-435d-b81d-6934a027d114','download.htm','.htm',1452439,'2024-10-09 17:22:25'),('b9e6d480-823f-4ed8-b671-74eaf89bf367','TONI (1).png','.png',15715,'2024-10-12 07:45:17'),('bc557e91-363e-4aa8-be91-88cd0c1a7f06','WhatsApp Image 2024-10-11 at 07.09.42.jpeg','.jpeg',73473,'2024-10-17 08:16:14'),('bd977fed-9054-4fa1-9083-f704cf062b5b','TONI (2) (1).png','.png',14525,'2024-10-11 22:02:06'),('c0de4de5-b91e-4ac2-911d-22a5721754f3','78cbbd763ac7966388812774d23c88b1.png','.png',97886,NULL),('c0f308ed-3c81-4f4e-8177-34a63e9b5faf','TONI.png','.png',14525,'2024-10-12 07:27:45'),('c2b3c9df-6925-4ef1-b77a-5905759b12a4','TONI (2) (1).png','.png',14525,'2024-10-11 16:50:56'),('c4350afb-c8b5-4b99-b407-34f36b774f8c','wa (1).png','.png',14525,'2024-10-12 21:40:00'),('c601e1ef-c1b3-4750-b26e-ccc964117363','e5f754f30c122c7c7ac7a94eccd69ddd.png','.png',213100,NULL),('c75d35e4-7759-4bad-a001-08e2b651180e','TONI (1).png','.png',15715,'2024-10-12 07:53:32'),('caadd803-992a-49fb-94c1-81701fa7df06','TONI (2) (1).png','.png',14525,'2024-10-11 16:50:56'),('cabcea07-41d0-4dd2-80ea-accb83e61d4f','C360_2017-09-21-15-43-26-973.jpg','.jpg',14525,'2024-10-12 07:56:19'),('ccf5ffd6-6e59-460a-87ef-7b2c37c19acd','wa.png','.png',14525,NULL),('cd386282-e12d-4dd1-8e18-0dc485e4faae','TONI.png','.png',14525,NULL),('cdf77669-b2a3-4b35-a61e-b875b1d0cf0a','TONI (2) (1).png','.png',14525,'2024-10-10 16:30:09'),('d25f9c9a-5e0f-4f9d-adc5-5b628e3bdf0a','TONI (1) (1) (1).png','.png',15715,'2024-10-12 21:40:47'),('d67eb22e-d018-4e70-94b4-54cf8ce52009','TONI (2).png','.png',14525,'2024-10-12 08:17:12'),('d73db5df-f66f-4ac6-8b08-2eae7f75ab84','TONI (2) (1).png','.png',14525,'2024-10-09 17:25:24'),('d9c7b5b6-e1fa-4322-a20a-5ed01509332c','pexels-joyston-judah-331625-933054.jpg','.jpg',235414,NULL),('db6291c6-da19-46b6-8d15-98e1020cfe2a','TONI (2) (1).png','.png',14525,'2024-10-09 17:25:24'),('dbdea45b-cdd3-455e-96ef-fefdcf0a5644','wa (2).png','.png',14525,'2024-10-11 17:04:24'),('dd3d95e2-593c-4bad-9450-569c5963fe0c','TONI (2) (1) (1).png','.png',14525,'2024-10-11 22:05:35'),('dd8906bb-62e4-4be0-b6a6-8ba10c44f402','PNG.png','.png',207770,NULL),('e04ae242-cc93-4508-8699-b90bd10577bf','TONI (2) (1).png','.png',14525,'2024-10-11 17:05:06'),('e1406d49-172e-41b4-b14d-8dae9efa322a','wa.png','.png',14525,NULL),('e1a65261-ca9c-4d8f-b82c-45ff7aef0d7b','e5f754f30c122c7c7ac7a94eccd69ddd.png','.png',213100,NULL),('e5d467b8-3738-44ab-aadd-33abf6a466ad','TONI (1) (1) (1).png','.png',15715,'2024-10-12 21:54:53'),('e795dce5-2db7-42f6-bc9c-5d589ccda2b5','IMG_20171221_131334.jpg','.jpg',152113,NULL),('e845fd9a-a9a1-4925-ba42-094bc572a920','C360_2017-09-21-15-43-26-973.jpg','.jpg',14525,'2024-10-12 07:57:42'),('e9e6293b-b08f-4d96-a090-32e6c5e56400','wa.png','.png',14525,'2024-10-12 07:56:55'),('ea9437b6-d59d-46a9-8269-c96e1934a024','TONI (2) (1).png','.png',14525,'2024-10-11 17:04:24'),('ec03fe49-2a44-4d1e-923e-f437d73311ab','wa.png','.png',167456,NULL),('ef3618ec-b3df-42cb-b218-6088ac1a8a03','wa (2).png','.png',14525,'2024-10-11 17:04:24'),('f0f34fdc-b7e1-4f9c-b73d-74dca58fbf00','e5f754f30c122c7c7ac7a94eccd69ddd.png','.png',213100,NULL),('f2951ee6-95d6-4e3a-928b-6ebf832b6912','e5f754f30c122c7c7ac7a94eccd69ddd.png','.png',213100,NULL),('f2b1ac70-1e57-418f-9c91-5444a14b266a','wa.png','.png',167456,NULL),('f8bdbddf-579c-412a-b018-74237607fd48','TONI.png','.png',14525,NULL),('fccdb5e6-671b-4dd2-a2f8-4b4d153e2e0f','TONI (2) (1).png','.png',14525,'2024-10-11 18:19:59'),('fd293ab5-4d37-4b7a-b5b5-192ba51fb365','wa (1).png','.png',14525,'2024-10-16 11:32:59'),('fd619bd7-0bca-4bbd-8dcc-c4b083e52b76','TONI.png','.png',14525,'2024-10-12 07:27:45'),('ff265abb-aa3a-42d2-9871-8f017e6d12aa','TONI (2).png','.png',14525,'2024-10-12 20:49:15');

/*Table structure for table `floatwallet` */

DROP TABLE IF EXISTS `floatwallet`;

CREATE TABLE `floatwallet` (
  `FloatWalletId` bigint NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime NOT NULL,
  `MemberId` bigint NOT NULL,
  `CashChange` decimal(13,2) DEFAULT NULL,
  `PointChange` decimal(13,2) DEFAULT NULL,
  `StatusId` int DEFAULT NULL COMMENT '1-NotAdded 2-Added',
  PRIMARY KEY (`FloatWalletId`),
  KEY `MemberId` (`MemberId`),
  CONSTRAINT `floatwallet_ibfk_1` FOREIGN KEY (`MemberId`) REFERENCES `member` (`MemberId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `floatwallet` */

/*Table structure for table `history` */

DROP TABLE IF EXISTS `history`;

CREATE TABLE `history` (
  `HistoryId` bigint NOT NULL AUTO_INCREMENT,
  `MemberId` bigint DEFAULT NULL,
  `AdminId` int DEFAULT NULL,
  `TransactionTypeId` int NOT NULL,
  `TransactionAmount` decimal(13,2) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `Description` text,
  `StatusId` int DEFAULT NULL,
  PRIMARY KEY (`HistoryId`),
  KEY `MemberId` (`MemberId`),
  KEY `AdminId` (`AdminId`),
  CONSTRAINT `history_ibfk_1` FOREIGN KEY (`MemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `history_ibfk_2` FOREIGN KEY (`AdminId`) REFERENCES `admin` (`AdminId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `history` */

insert  into `history`(`HistoryId`,`MemberId`,`AdminId`,`TransactionTypeId`,`TransactionAmount`,`CreateDate`,`Description`,`StatusId`) values (1,1,NULL,1,'32.00','2024-07-16 18:08:05',NULL,1);

/*Table structure for table `member` */

DROP TABLE IF EXISTS `member`;

CREATE TABLE `member` (
  `MemberId` bigint NOT NULL AUTO_INCREMENT,
  `MemberCode` varchar(100) DEFAULT NULL,
  `FirstName` varchar(255) DEFAULT NULL,
  `LastName` varchar(255) DEFAULT NULL,
  `ICNumber` varchar(50) DEFAULT NULL,
  `Birthdate` datetime DEFAULT NULL,
  `GenderId` int DEFAULT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `Salt` text,
  `PasswordSalted` text,
  `GoogleId` varchar(255) DEFAULT NULL,
  `ReferrerMemberId` bigint DEFAULT NULL,
  `ReferralTypeId` int DEFAULT NULL,
  `ReferralAmount` decimal(13,2) DEFAULT NULL,
  `PhoneNumber` varchar(50) DEFAULT NULL,
  `CompanyName` varchar(100) DEFAULT NULL,
  `CompanyTypeId` int NOT NULL DEFAULT '1' COMMENT '1-Government 2-Private',
  `RetirementAge` int DEFAULT NULL,
  `Occupation` varchar(255) DEFAULT NULL,
  `Salary` decimal(13,2) DEFAULT NULL,
  `StreetAddress1` text,
  `StreetAddress2` text,
  `City` varchar(100) DEFAULT NULL,
  `StateId` int DEFAULT NULL,
  `CountryId` int DEFAULT NULL,
  `Postcode` varchar(20) DEFAULT NULL,
  `ICFileId` varchar(50) DEFAULT NULL,
  `CreateDate` datetime NOT NULL,
  `BankId` int DEFAULT NULL,
  `BankAccountName` varchar(255) DEFAULT NULL,
  `BankAccountNumber` varchar(100) DEFAULT NULL,
  `StatusId` int NOT NULL COMMENT '1-WaitingApproval 2-Active 3-Inactive',
  `ForgotPasswordOTP` varchar(50) DEFAULT NULL,
  `NewAccountOTP` varchar(50) DEFAULT NULL,
  `ProfileFileId` varchar(50) DEFAULT NULL,
  `WalletCash` decimal(13,2) DEFAULT NULL,
  `WalletPoint` decimal(13,2) DEFAULT NULL,
  `LastUpdateWalletCash` datetime DEFAULT NULL,
  `LastUpdateWalletPoint` datetime DEFAULT NULL,
  `MemberTypeId` int NOT NULL DEFAULT '1' COMMENT '1-Member 2-Agent 3-Hero',
  PRIMARY KEY (`MemberId`),
  UNIQUE KEY `Email` (`Email`),
  KEY `ReferrerMemberId` (`ReferrerMemberId`),
  KEY `CountryId` (`CountryId`),
  KEY `StateId` (`StateId`),
  KEY `BankId` (`BankId`),
  KEY `ProfileFileId` (`ProfileFileId`),
  KEY `ICFileId` (`ICFileId`),
  CONSTRAINT `member_ibfk_1` FOREIGN KEY (`ReferrerMemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `member_ibfk_2` FOREIGN KEY (`CountryId`) REFERENCES `country` (`CountryId`),
  CONSTRAINT `member_ibfk_3` FOREIGN KEY (`StateId`) REFERENCES `state` (`StateId`),
  CONSTRAINT `member_ibfk_4` FOREIGN KEY (`BankId`) REFERENCES `bank` (`BankId`),
  CONSTRAINT `member_ibfk_5` FOREIGN KEY (`ProfileFileId`) REFERENCES `file` (`FileId`),
  CONSTRAINT `member_ibfk_6` FOREIGN KEY (`ICFileId`) REFERENCES `file` (`FileId`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

/*Data for the table `member` */

insert  into `member`(`MemberId`,`MemberCode`,`FirstName`,`LastName`,`ICNumber`,`Birthdate`,`GenderId`,`Email`,`Salt`,`PasswordSalted`,`GoogleId`,`ReferrerMemberId`,`ReferralTypeId`,`ReferralAmount`,`PhoneNumber`,`CompanyName`,`CompanyTypeId`,`RetirementAge`,`Occupation`,`Salary`,`StreetAddress1`,`StreetAddress2`,`City`,`StateId`,`CountryId`,`Postcode`,`ICFileId`,`CreateDate`,`BankId`,`BankAccountName`,`BankAccountNumber`,`StatusId`,`ForgotPasswordOTP`,`NewAccountOTP`,`ProfileFileId`,`WalletCash`,`WalletPoint`,`LastUpdateWalletCash`,`LastUpdateWalletPoint`,`MemberTypeId`) values (1,'123','aaaa','bbbb','333',NULL,1,'kan@mail.com','lqk7v+kw6+k6XbNTIfbE4bBtiVElnwJWrQeo4EbZGR5Ac2ihSslym44ud1H7HZMxsJc2omqI189yCWT6FovZcSh3In5W3lbX0VI85s6VXKqES7lPSithj2ybYZRidpYUGBlOwk4qON1T7TtQDV+zvdo7gkrd3B/vU8WOhNHzB20=','552d1dd03e5f24116b34d94597d870c511590aa287e9437c90edc47eacf21a7f180f648d71609a7c76f212daee97c6c99265fb513267dac2c3b1110044be52bb',NULL,NULL,NULL,NULL,'222','CSa dawaidalj',1,NULL,NULL,NULL,'12312312313asdasdas',NULL,NULL,NULL,NULL,NULL,'ec03fe49-2a44-4d1e-923e-f437d73311ab','2024-07-15 09:32:18',3,'kakala','2222222222',1,NULL,NULL,'4368c2dc-1474-4852-966c-64cd9c1c27fa','222.00',NULL,NULL,NULL,1),(2,'awd','vv','ccc','234234',NULL,NULL,NULL,'q0bK7aAaOkmIuAUjJpoz7qNsr1yjEmEGbEXOkJ5JDPDqWdurz2FhLC6nFwEQg+NI52eT/rscx1MXfUQffJKPZijRSFk5hb1C+NyNp5raA4s4/PsaI7iyQiZPdA+roTQBKlfBPjPKjnv9YXfB+8eJfbrGZnchcjexOqQD4pEt0rU=','2e7f90b026bc0a7605ca4d499fa103983d37cddcc8c3eb9fc7a76545f0dcc022388d6a42fe358a981cbddaef59dcde02cd7fa1782774d180eda3b9fe0fdf6de7',NULL,1,1,NULL,'022','Company sample',1,60,NULL,'50000.00',NULL,NULL,NULL,4,1,NULL,NULL,'2024-07-16 09:33:39',12,NULL,NULL,1,NULL,NULL,'3655661e-f6de-4ae2-bb67-c01cf0ee73c6',NULL,NULL,NULL,NULL,3),(3,NULL,'kaka','anda','12315',NULL,NULL,'kakaanda@mail.com','pG3orTDE0BqqebiSlLb0on5g+zL9nIF3fpqy+mjJR8Y6pPf5NIh2KWN8/LZMNsT8/MH6iqM/ux+4sB1CvXgOpahLqxq50pZCpO7vsNxDjVlIwDzPOmbrpYd62WlRlbzEuajBRtW9ug3H8TQJ9s8bCONCHIXTtFp7sMnsr+X0mNU=','bb2d7a64e53bcf7f9353e41bedee7c404a21d116d1b56e59206295a9c91f7c54e83a5b1577d43f6fbdc6b8be4aaf4b98b3411afc1cd3bfcced22fb56938684da',NULL,1,1,NULL,'62321313',NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'873c55d8-2a29-4e46-b4a4-da6a9e6aca71','2024-07-17 13:04:03',NULL,NULL,NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1),(4,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'8ToGftrjpkvJJCn/H1MEyX0qdyv+ZvWeHzpzCCOsTNzluWH7dK7ggQrpzyQfppv3hG5xBsP+Ojv5aVN9kjwDuSKUqMpqkL6W9pR81KNoZ6iuD6M4sKSyL4GAsv3r1CvJdqVc89Yz0mWJmR042IDnBrzEleETs8+FQxNy+QqQixA=','ca8f00edec5f5016a23a949248b8b5c481063268ed391acd274cda42b065ad5950e3cb96158dcdcff4902181fd37ee89220c1ac3eb4092a5be9722664270ff8c',NULL,2,NULL,NULL,'6232153',NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'2024-07-19 09:29:41',NULL,NULL,NULL,1,NULL,NULL,'826b88bd-36a0-4655-9e0e-b9b14110caf2',NULL,NULL,NULL,NULL,1),(5,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'swBlvbRv9vRZ6GhwhUeGV1Des94HO0eFPabEs7GgLYoIDpPUcqhUevrZwf9DE3/GV/zd0R06wcoI7fHnUmFy/Yc/xhc+IgRk1KKigux0yYY9dj6P6+gPYrAiB964uurJ5nKWF37bN1I8KDsubnwUHsBb7wajRUDtl7FEy8Jl5mU=','681cfe7a69ae19078ec842b4b2f39cc38edd122529a20b5389b5cf5d9acfe20ff99a5fbf07c9627e594cda840c90066dd6113a77ff72f3aa397fcc3021f6693a',NULL,2,NULL,NULL,'62351351',NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'2024-07-19 09:38:29',NULL,NULL,NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1),(6,'asd ajl','Dani','setiawan',NULL,NULL,1,'dani@mail.com','s5v0KGmtd6cOSzd/h+ZHarjIyyJd2YI0lrLae4aPHr91tWNSOwiQtCnPAe2xuwvazfVREBNpGOEglIK+T4VvsyKxhsDhYwSBQNDxvbo1q1oXDYxJSFpiew4kionrGk5C80cCnn8iqaYJDOKeGizksvDL4Sh1N1Wa9KfHR5sNYAA=','bba37a5a68111db77c055b25b8723885bd1391092cc9510c32b066c94cde7e7ef85415b304e325a1eac7db11e29fd7b64318bf705aeaf6931cf62ca662035845',NULL,8,NULL,NULL,'621243646546','Hudan',1,NULL,'Bos','100000000.00','pasir honje',NULL,NULL,4,1,NULL,NULL,'2024-10-14 14:06:50',12,'DHARSHAN KUMAR AL KANAN SEEN','125144-73-8184',1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1),(7,'lkajwdl12','maka','nan',NULL,NULL,1,'maka@mail.com','DwdddDaoiZrTX+QJsYby3if7dXxEnWvfwJX+79g4kOMqHDqeQ8vyhyypRbLzKwnd8KgsbTxicifejgU0AbxNwfLCgndcrSwdNuGb3ooqTGoruKEINRto2UNiorDAdw6zqfkbvStul4B9+g1rhS8IO53Fl9c07xc1jWt5UwypN6Q=','dcee1e29a745f116fd92b652f750ded88f0a5664476ae0524c5e2829e02b3af449d9352ace4f408ba65880e82f732ff8e4318cc3b82fe150960993cf2e7bb12a',NULL,2,NULL,NULL,'6232135','awdaw',1,NULL,'awd','62.32','awd',NULL,NULL,NULL,1,NULL,'1a8d6d33-3d1b-4b9d-b79e-291f1018ad99','2024-10-14 14:06:50',12,'aa','aa',1,NULL,NULL,'f2b1ac70-1e57-418f-9c91-5444a14b266a',NULL,NULL,NULL,NULL,1),(8,NULL,'daw',NULL,NULL,NULL,1,NULL,'4yaNANm8aNeVhl4SK8YfHwxH3mdYw4ACSi+aonyzZLhAMrrmxIEr5qd+HFSVaZe5NR10NJEJOhPMa2XUNrXLThFBRYLjlHXaY6Ha6HVQUapATTLHah6Inj+QyHae+Fd+wjRFaTbPfoJtye+wX7xf6yxYyaiD2mZOryDEiU1DFKY=','24847120b8d620cd5cd44957a7f0ee175fbd0de930cc46aed5e8868ca597e99b49ad7b10e98fbea4b169cf0f047f36fa906a621c293a4eb4e09c679067d12c25',NULL,6,NULL,NULL,'123',NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1,NULL,NULL,'2024-10-15 16:05:16',NULL,NULL,NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,1);

/*Table structure for table `member_tag` */

DROP TABLE IF EXISTS `member_tag`;

CREATE TABLE `member_tag` (
  `MemberTagId` bigint NOT NULL AUTO_INCREMENT,
  `MemberId` bigint NOT NULL,
  `TagId` int NOT NULL,
  PRIMARY KEY (`MemberTagId`),
  KEY `MemberId` (`MemberId`),
  KEY `TagId` (`TagId`),
  CONSTRAINT `member_tag_ibfk_1` FOREIGN KEY (`MemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `member_tag_ibfk_2` FOREIGN KEY (`TagId`) REFERENCES `tag` (`TagId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `member_tag` */

/*Table structure for table `notification` */

DROP TABLE IF EXISTS `notification`;

CREATE TABLE `notification` (
  `NotificationId` bigint NOT NULL AUTO_INCREMENT,
  `MemberId` bigint NOT NULL,
  `Title` varchar(200) DEFAULT NULL,
  `Content` text,
  `NotificationTypeId` int NOT NULL COMMENT '1-Normal 2-Urgent',
  `CreateDate` datetime NOT NULL,
  `StatusId` int NOT NULL COMMENT '1-Unread 2-Read',
  PRIMARY KEY (`NotificationId`),
  KEY `MemberId` (`MemberId`),
  CONSTRAINT `notification_ibfk_1` FOREIGN KEY (`MemberId`) REFERENCES `member` (`MemberId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `notification` */

/*Table structure for table `role` */

DROP TABLE IF EXISTS `role`;

CREATE TABLE `role` (
  `RoleId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `AccessList` text COMMENT 'xml setting access list',
  PRIMARY KEY (`RoleId`),
  UNIQUE KEY `Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

/*Data for the table `role` */

insert  into `role`(`RoleId`,`Name`,`AccessList`) values (5,'Super Admin','1,2,3,4');

/*Table structure for table `setting` */

DROP TABLE IF EXISTS `setting`;

CREATE TABLE `setting` (
  `SettingId` int NOT NULL AUTO_INCREMENT,
  `Code` varchar(100) DEFAULT NULL,
  `StrValue` varchar(500) DEFAULT NULL,
  `TextValue` text,
  PRIMARY KEY (`SettingId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*Data for the table `setting` */

insert  into `setting`(`SettingId`,`Code`,`StrValue`,`TextValue`) values (1,'GoogleClientId',NULL,NULL),(2,'GoogleClientSecret',NULL,NULL);

/*Table structure for table `settlement` */

DROP TABLE IF EXISTS `settlement`;

CREATE TABLE `settlement` (
  `SettlementId` bigint NOT NULL AUTO_INCREMENT,
  `ApplicationID` bigint DEFAULT NULL,
  `Amount` decimal(13,2) DEFAULT NULL,
  `TotalPct` decimal(3,2) DEFAULT NULL,
  `TotalPctAmount` decimal(13,2) DEFAULT NULL,
  `PaymentDate` datetime DEFAULT NULL,
  `BankId` int DEFAULT NULL,
  `BankAccountNumber` varchar(50) DEFAULT NULL,
  `DueDate` datetime DEFAULT NULL,
  `Facilities` varchar(255) DEFAULT NULL,
  `AmountFacilities` decimal(13,2) DEFAULT NULL,
  `FlexyCampaignId` int DEFAULT NULL,
  `TotalCampaign` decimal(13,2) DEFAULT NULL,
  `RedemptionLetterDate` datetime DEFAULT NULL,
  `RedemptionAmount` decimal(13,2) DEFAULT NULL,
  `LoanReleaseDate` datetime DEFAULT NULL,
  `SettlementStatusId` int DEFAULT NULL,
  `Remark` varchar(500) DEFAULT NULL,
  `CreateAdminId` int DEFAULT NULL,
  PRIMARY KEY (`SettlementId`),
  KEY `BankId` (`BankId`),
  KEY `CreateAdminId` (`CreateAdminId`),
  KEY `ApplicationID` (`ApplicationID`),
  CONSTRAINT `settlement_ibfk_2` FOREIGN KEY (`BankId`) REFERENCES `bank` (`BankId`),
  CONSTRAINT `settlement_ibfk_3` FOREIGN KEY (`CreateAdminId`) REFERENCES `admin` (`AdminId`),
  CONSTRAINT `settlement_ibfk_4` FOREIGN KEY (`ApplicationID`) REFERENCES `application` (`ApplicationId`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `settlement` */

insert  into `settlement`(`SettlementId`,`ApplicationID`,`Amount`,`TotalPct`,`TotalPctAmount`,`PaymentDate`,`BankId`,`BankAccountNumber`,`DueDate`,`Facilities`,`AmountFacilities`,`FlexyCampaignId`,`TotalCampaign`,`RedemptionLetterDate`,`RedemptionAmount`,`LoanReleaseDate`,`SettlementStatusId`,`Remark`,`CreateAdminId`) values (7,1,NULL,NULL,NULL,NULL,12,'',NULL,'',NULL,0,NULL,NULL,NULL,NULL,1,'',1),(8,1,NULL,NULL,NULL,NULL,12,'',NULL,'',NULL,0,NULL,NULL,NULL,NULL,1,'',1),(9,2,NULL,NULL,NULL,NULL,12,'',NULL,'',NULL,0,NULL,NULL,NULL,NULL,1,'',1),(10,2,'3213213.00','1.00','1321321.00','2024-10-12 00:00:00',12,'2131','2024-10-12 00:00:00','21321','23132.00',0,'321321.00','2024-10-12 00:00:00','1321.00','2024-10-12 00:00:00',2,'321',1),(11,2,'5464646.00','1.00','6546546.00','2024-10-12 00:00:00',6,'4564654','2024-10-12 00:00:00','64654','65465.00',0,'654654.00','2024-10-12 00:00:00','6546.00','2024-10-12 00:00:00',2,'4654',1),(12,2,'1.00','0.02','3.00','2024-12-02 00:00:00',3,'4','2024-02-27 00:00:00','5','6.00',1,'7.00','2024-03-12 00:00:00','8.00','2024-04-12 00:00:00',2,'9',1);

/*Table structure for table `state` */

DROP TABLE IF EXISTS `state`;

CREATE TABLE `state` (
  `StateId` int NOT NULL AUTO_INCREMENT,
  `Code` varchar(3) DEFAULT NULL,
  `Name` varchar(100) DEFAULT NULL,
  `CountryId` int NOT NULL,
  PRIMARY KEY (`StateId`),
  KEY `CountryId` (`CountryId`),
  CONSTRAINT `state_ibfk_1` FOREIGN KEY (`CountryId`) REFERENCES `country` (`CountryId`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;

/*Data for the table `state` */

insert  into `state`(`StateId`,`Code`,`Name`,`CountryId`) values (1,'JHR','Johor',1),(2,'KDH','Kedah',1),(3,'KTN','Kelantan',1),(4,'KUL','Kuala Lumpur',1),(5,'LBN','Labuan',1),(6,'MLK','Melaka',1),(7,'NSN','Negeri Sembilan',1),(8,'PHG','Pahang',1),(9,'PJY','Putrajaya',1),(10,'PLS','Perlis',1),(11,'PNG','Penang',1),(12,'PRK','Perak',1),(13,'SBH','Sabah',1),(14,'SGR','Selangor',1),(15,'SWK','Sarawak',1),(16,'TRG','Terengganu',1);

/*Table structure for table `survey` */

DROP TABLE IF EXISTS `survey`;

CREATE TABLE `survey` (
  `SurveyId` bigint NOT NULL AUTO_INCREMENT,
  `MemberId` bigint NOT NULL,
  `Answer` text COMMENT 'XML content',
  `SurveyVersionId` int DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  PRIMARY KEY (`SurveyId`),
  KEY `MemberId` (`MemberId`),
  CONSTRAINT `survey_ibfk_1` FOREIGN KEY (`MemberId`) REFERENCES `member` (`MemberId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `survey` */

/*Table structure for table `tag` */

DROP TABLE IF EXISTS `tag`;

CREATE TABLE `tag` (
  `TagId` int NOT NULL AUTO_INCREMENT,
  `Label` varchar(50) NOT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `StatusID` int DEFAULT NULL,
  PRIMARY KEY (`TagId`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

/*Data for the table `tag` */

insert  into `tag`(`TagId`,`Label`,`CreateDate`,`StatusID`) values (1,'Platinum','2024-07-18 11:57:32',1),(2,'GOLD','2024-07-18 11:57:39',1),(4,'AKUN','2024-07-18 13:50:36',2),(7,'KELAS','2024-07-18 14:37:59',1),(10,'PIEA','2024-07-18 14:38:17',1),(11,'MAEEE','2024-07-18 14:38:24',1),(12,'KIRAN','2024-07-18 14:51:57',1);

/*Table structure for table `withdrawal` */

DROP TABLE IF EXISTS `withdrawal`;

CREATE TABLE `withdrawal` (
  `WithdrawalId` int NOT NULL AUTO_INCREMENT,
  `MemberId` bigint NOT NULL,
  `Amount` decimal(13,2) DEFAULT NULL,
  `BankId` int DEFAULT NULL,
  `BankAccountName` varchar(255) DEFAULT NULL,
  `BankAccountNumber` varchar(100) DEFAULT NULL,
  `CreateDate` datetime NOT NULL,
  `ResponseDate` datetime DEFAULT NULL,
  `StatusId` int DEFAULT NULL,
  `AdminId` int DEFAULT NULL,
  `AdminNote` text,
  PRIMARY KEY (`WithdrawalId`),
  KEY `MemberId` (`MemberId`),
  KEY `BankId` (`BankId`),
  KEY `AdminId` (`AdminId`),
  CONSTRAINT `withdrawal_ibfk_1` FOREIGN KEY (`MemberId`) REFERENCES `member` (`MemberId`),
  CONSTRAINT `withdrawal_ibfk_2` FOREIGN KEY (`BankId`) REFERENCES `bank` (`BankId`),
  CONSTRAINT `withdrawal_ibfk_3` FOREIGN KEY (`AdminId`) REFERENCES `admin` (`AdminId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*Data for the table `withdrawal` */

insert  into `withdrawal`(`WithdrawalId`,`MemberId`,`Amount`,`BankId`,`BankAccountName`,`BankAccountNumber`,`CreateDate`,`ResponseDate`,`StatusId`,`AdminId`,`AdminNote`) values (1,1,'21.00',1,'123','123123','2024-07-16 16:09:57','2024-08-13 11:14:36',5,1,NULL),(2,1,'50.00',2,'4564646','45984613213','2024-07-18 10:29:59','2024-07-18 10:29:59',1,NULL,NULL);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
