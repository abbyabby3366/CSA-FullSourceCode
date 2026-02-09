ALTER TABLE `csa`.`admin`   
  ADD COLUMN `TeamId` INT NULL AFTER `CreateDate`,
  ADD COLUMN `DivisionId` INT NULL AFTER `TeamId`;


ALTER TABLE `csa`.`member`   
  ADD COLUMN `CompanyTypeId` INT DEFAULT 1  NOT NULL  COMMENT '1-Government 2-Private' AFTER `CompanyName`,
  ADD COLUMN `RetirementAge` INT NULL AFTER `CompanyTypeId`,
  ADD COLUMN `MemberTypeId` INT DEFAULT 1  NOT NULL  COMMENT '1-Member 2-Agent 3-Hero' AFTER `LastUpdateWalletPoint`;

ALTER TABLE `csa`.`member`   
  CHANGE `StatusId` `StatusId` INT NOT NULL  COMMENT '1-WaitingApproval 2-Active 3-Inactive';

CREATE TABLE `csa`.`application`(  
  `ApplicationId` BIGINT NOT NULL AUTO_INCREMENT,
  `MemberId` BIGINT NOT NULL,
  `CreateDate` DATETIME NOT NULL,
  `CreditRemark` TEXT,
  `CustomerStatusId` INT NOT NULL COMMENT '1-Eligible 2-Burst',
  `BurstReasonId` INT,
  `ApplicationStatusId` INT NOT NULL COMMENT '1-Pre-checking',
  `ApplicationStatusLastChangeDate` DATETIME,
  `ApplicationStatusLastChangeAdminId` INT,
  `ReferrerMemberId` BIGINT,
  `AMAdminId` INT,
  `PFCAdminId` INT,
  `RMAdminId` INT,
  `UMAdminId` INT,
  `PAAdminId` INT,
  `SourceId` INT,
  `RejectedDate` DATETIME,
  `RejectedAdminId` INT,
  `RejectedReason` TEXT,
  `RejectedApplicationStatusId` INT,
  PRIMARY KEY (`ApplicationId`),
  FOREIGN KEY (`MemberId`) REFERENCES `csa`.`member`(`MemberId`),
  FOREIGN KEY (`ReferrerMemberId`) REFERENCES `csa`.`member`(`MemberId`),
  FOREIGN KEY (`AMAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  FOREIGN KEY (`PFCAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  FOREIGN KEY (`RMAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  FOREIGN KEY (`UMAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  FOREIGN KEY (`PAAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  FOREIGN KEY (`ApplicationStatusLastChangeAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  FOREIGN KEY (`RejectedAdminId`) REFERENCES `csa`.`announcement`(`AnnouncementId`)
);

CREATE TABLE `csa`.`application_1`(  
  `Application1Id` BIGINT NOT NULL AUTO_INCREMENT,
  `ApplicationId` BIGINT NOT NULL,
  `RAMCIReportFileId` VARCHAR(50),
  `RAMCIReportAdminId` INT,
  `RAMCIReportLastUpdate` DATETIME,
  `CCRISDocumentFileId` VARCHAR(50),
  `CCRISDocumentAdminId` INT,
  `CCRISDocumentLastUpdate` DATETIME,
  `EligibilityId` INT,
  `EligibilityAdminId` INT,
  `EligibilityLastUpdate` DATETIME,
  `LegalSuitsCheck` INT,
  `BankruptcyCheck` INT,
  `SpecialAttentionCheck` INT,
  `BadPaymentRecordCheck` INT,
  PRIMARY KEY (`Application1Id`),
  FOREIGN KEY (`ApplicationId`) REFERENCES `csa`.`application`(`ApplicationId`)
);

CREATE TABLE `csa`.`application_document`(  
  `ApplicationDocumentId` BIGINT NOT NULL AUTO_INCREMENT,
  `ApplicationId` BIGINT NOT NULL,
  `ApplicationStatusId` INT,
  `FileId` VARCHAR(50),
  `Remark` VARCHAR(500),
  `CreateDate` DATETIME,
  `CreateAdminId` INT,
  PRIMARY KEY (`ApplicationDocumentId`),
  FOREIGN KEY (`ApplicationId`) REFERENCES `csa`.`application`(`ApplicationId`),
  FOREIGN KEY (`CreateAdminId`) REFERENCES `csa`.`admin`(`AdminId`)
);


CREATE TABLE `csa`.`application_2`(  
  `Application2Id` BIGINT NOT NULL AUTO_INCREMENT,
  `ApplicationID` BIGINT NOT NULL,
  `ProposalFileId` VARCHAR(50),
  `ProposalAdminId` INT,
  `ProposalLastUpdate` DATETIME,
  `SalaryGross` DECIMAL(13,2),
  `SalaryDeduction` DECIMAL(13,2),
  `NetIncome` DECIMAL(13,2),
  `PriorDSRB1` DECIMAL(13,2),
  `PriorDSRB2` DECIMAL(13,2),
  `PriorDSRB3` DECIMAL(13,2),
  `PriorDSRB4` DECIMAL(13,2),
  `PriorDSRBAverage` DECIMAL(13,2),
  `CommitmentOutstanding` DECIMAL(13,2),
  `CommitmentInstallment` DECIMAL(13,2),
  `OthersNetBalance` DECIMAL(13,2),
  `OthersBPA` DECIMAL(13,2),
  `OthersComparisonDSR` DECIMAL(13,2),
  `OthersComparisonDSRPctCommitment` DECIMAL(3,2),
  `OthersPctRefresh` DECIMAL(3,2),
  `OthersProposedRefresh` DECIMAL(13,2),
  `OthersCompositionDSR` DECIMAL(13,2),
  `OthersCompositionDSRPctCommitment` DECIMAL(3,2),
  `RefreshTotal` DECIMAL(13,2),
  `RefreshRemainCommitment` DECIMAL(13,2),
  `ReloanTotal` DECIMAL(13,2),
  `ReloanMonthly` DECIMAL(13,2),
  `ReloanBersih` DECIMAL(13,2),
  `ReloanBelanja` DECIMAL(13,2),
  `ReloanDeposit` DECIMAL(13,2),
  `ReloanDanaBantuan` DECIMAL(13,2),
  `ReloanServiceFee` DECIMAL(13,2),
  `ReloanServiceFeePct` DECIMAL(3,2),
  `ReloanIncomeAfterRNR` DECIMAL(13,2),
  `ReloanDifference` DECIMAL(13,2),
  `ModelBackgroundScreeningId` INT,
  `ModelCompositionDSRId` INT,
  `ModelCommitmentId` INT,
  `ModelSettlementId` INT,
  `ModelServiceFeeId` int,
  `ModelNetIncomeAfterRNRId` int,
  `ModelStatusId` int,
  `ModelStatusProposalId` int,
  `ModelCheckId` int,
  `ReviewAdminId` int,
  `ReviewStatusId` int,
  `ReviewDate` datetime,
  `ReviewComment` text,
  `ApproveAdminId` int,
  `ApproveDate` datetime,
  `ApproveComment` text,
  primary key (`Application2Id`),
  foreign key (`ApplicationID`) references `csa`.`application`(`ApplicationId`),
  foreign key (`ProposalAdminId`) references `csa`.`admin`(`AdminId`),
  foreign key (`ReviewAdminId`) references `csa`.`admin`(`AdminId`),
  foreign key (`ApproveAdminId`) references `csa`.`admin`(`AdminId`)
);

CREATE TABLE `csa`.`application_3`(  
  `Application3Id` BIGINT NOT NULL AUTO_INCREMENT,
  `ApplicationId` BIGINT NOT NULL,
  `ProposalStatusId` INT,
  PRIMARY KEY (`Application3Id`),
  FOREIGN KEY (`ApplicationId`) REFERENCES `csa`.`application`(`ApplicationId`)
);
ALTER TABLE `csa`.`application_3`   
  ADD COLUMN `ProposalStatusAdminId` INT NULL AFTER `ProposalStatusId`,
  ADD COLUMN `ProposalStatusLastUpdate` DATETIME NULL AFTER `ProposalStatusAdminId`,
  ADD FOREIGN KEY (`ProposalStatusAdminId`) REFERENCES `csa`.`admin`(`AdminId`);

CREATE TABLE `csa`.`application_4`(  
  `Application4Id` BIGINT NOT NULL AUTO_INCREMENT,
  `ApplicationId` BIGINT,
  `ProposalSendId` INT,
  `ProposalSendAdminId` INT,
  `ProposalSendLastUpdate` DATETIME,
  `SuratAkuanId` INT,
  `SuratAkuanAdminId` INT,
  `SuratAkuanLastUpdate` DATETIME,
  `ComprehensiveFormId` INT,
  `ComprehensiveFormAdminId` INT,
  `ComprehensiveFormLastUpdate` DATETIME,
  PRIMARY KEY (`Application4Id`),
  FOREIGN KEY (`ApplicationId`) REFERENCES `csa`.`application`(`ApplicationId`),
  FOREIGN KEY (`ProposalSendAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  FOREIGN KEY (`SuratAkuanAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  FOREIGN KEY (`ComprehensiveFormAdminId`) REFERENCES `csa`.`admin`(`AdminId`)
);


CREATE TABLE `csa`.`application_5`(  
  `Application5Id` BIGINT NOT NULL AUTO_INCREMENT,
  `ApplicationId` BIGINT NOT NULL,
  `PayslipFileId` VARCHAR(50),
  `PayslipAdminId` INT,
  `PayslipLastUpdate` DATETIME,
  `RAMCIFileId` VARCHAR(50),
  `RAMCIAdminId` INT,
  `RAMCILastUpdate` DATETIME,
  `CTOSFileId` VARCHAR(50),
  `CTOSAdminId` INT,
  `CTOSLastUpdate` DATETIME,
  `RedemptionLetterFileId` VARCHAR(50),
  `RedemptionLetterAdminId` INT,
  `RedemptionLetterLastUpdate` DATETIME,
  `ApplicantAddress` VARCHAR(500),
  `BankruptcyStatus` VARCHAR(255),
  `LegalCase` VARCHAR(255),
  `HealthCreditScore` VARCHAR(100),
  `Commitements` VARCHAR(500),
  PRIMARY KEY (`Application5Id`),
  FOREIGN KEY (`ApplicationId`) REFERENCES `csa`.`application`(`ApplicationId`),
  FOREIGN KEY (`PayslipAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  FOREIGN KEY (`RAMCIAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  FOREIGN KEY (`CTOSAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  FOREIGN KEY (`RedemptionLetterAdminId`) REFERENCES `csa`.`admin`(`AdminId`)
);


CREATE TABLE `csa`.`application_6`(  
  `Application6Id` BIGINT NOT NULL AUTO_INCREMENT,
  `ApplicationId` BIGINT NOT NULL,
  `PaymentReceiptFileId` VARCHAR(50),
  `PaymentReceiptAdminId` INT,
  `PaymentReceiptLastUpdate` DATETIME,
  PRIMARY KEY (`Application6Id`)
);

CREATE TABLE `csa`.`settlement`(  
  `SettlementId` BIGINT NOT NULL AUTO_INCREMENT,
  `ApplicationID` INT,
  `Amount` DECIMAL(13,2),
  `TotalPct` DECIMAL(3,2),
  `TotalPctAmount` DECIMAL(13,2),
  `PaymentDate` DATETIME,
  `BankId` INT,
  `BankAccountNumber` VARCHAR(50),
  `DueDate` DATETIME,
  `Facilities` VARCHAR(255),
  `AmountFacilities` DECIMAL(13,2),
  `FlexyCampaignId` INT,
  `TotalCampaign` DECIMAL(13,2),
  `RedemptionLetterDate` DATETIME,
  `RedemptionAmount` DECIMAL(13,2),
  `LoanReleaseDate` DATETIME,
  `SettlementStatusId` INT,
  `Remark` VARCHAR(500),
  `CreateAdminId` INT,
  PRIMARY KEY (`SettlementId`),
  FOREIGN KEY (`ApplicationID`) REFERENCES `csa`.`application`(`AMAdminId`),
  FOREIGN KEY (`BankId`) REFERENCES `csa`.`bank`(`BankId`),
  FOREIGN KEY (`CreateAdminId`) REFERENCES `csa`.`admin`(`AdminId`)
);
CREATE TABLE `csa`.`caseupdate`(  
  `CaseUpdateId` BIGINT NOT NULL AUTO_INCREMENT,
  `ApplicationId` BIGINT NOT NULL,
  `BankId` INT,
  `LoanAmount` DECIMAL(13,2),
  `SubmitDate` DATETIME,
  `Banker` VARCHAR(255),
  `CompleteStatusId` INT,
  `Consolidate` VARCHAR(255),
  `CashNet` DECIMAL(13,2),
  `Instalment` DECIMAL(13,2),
  `ApprovedDate` DATETIME,
  `SignDate` DATETIME,
  `DisbursementDate` DATETIME,
  `UpdateDate` DATETIME,
  `LoanAccountNumber` VARCHAR(100),
  `1stDueDate` DATETIME,
  `Remarkds` VARCHAR(500),
  `AdminId` INT,
  PRIMARY KEY (`CaseUpdateId`),
  FOREIGN KEY (`ApplicationId`) REFERENCES `csa`.`application`(`ApplicationId`),
  FOREIGN KEY (`BankId`) REFERENCES `csa`.`bank`(`BankId`),
  FOREIGN KEY (`AdminId`) REFERENCES `csa`.`admin`(`AdminId`)
);


ALTER TABLE `csa`.`application_6`  
  ADD FOREIGN KEY (`ApplicationId`) REFERENCES `csa`.`application`(`ApplicationId`),
  ADD FOREIGN KEY (`PaymentReceiptAdminId`) REFERENCES `csa`.`admin`(`AdminId`);

CREATE TABLE `csa`.`application_7`(  
  `Application7Id` BIGINT NOT NULL AUTO_INCREMENT,
  `ApplicationId` BIGINT NOT NULL,
  `ReleaseLetterFileId` VARCHAR(50),
  `ReleaseLetterAdminId` INT,
  `ReleaseLetterLastUpdate` DATETIME,
  `CCRISReportFileId` VARCHAR(50),
  `CCRISReportAdminId` INT,
  `CCRISReportLastUpdate` DATETIME,
  PRIMARY KEY (`Application7Id`),
  FOREIGN KEY (`ApplicationId`) REFERENCES `csa`.`application`(`ApplicationId`),
  FOREIGN KEY (`ReleaseLetterAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  FOREIGN KEY (`CCRISReportAdminId`) REFERENCES `csa`.`admin`(`AdminId`)
);

CREATE TABLE `csa`.`application_8`(  
  `Application8Id` BIGINT NOT NULL AUTO_INCREMENT,
  `ApplicationId` BIGINT NOT NULL,
  PRIMARY KEY (`Application8Id`),
  FOREIGN KEY (`ApplicationId`) REFERENCES `csa`.`application`(`ApplicationId`)
);


ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `IdentityCardFileId` VARCHAR(50) NULL AFTER `ApplicationId`,
  ADD COLUMN `IdentityCardAdminId` INT NULL AFTER `IdentityCardFileId`,
  ADD COLUMN `IdentityCardMemberId` BIGINT NULL AFTER `IdentityCardAdminId`,
  ADD COLUMN `IdentityCardLastUpdate` DATETIME NULL AFTER `IdentityCardMemberId`,
  ADD FOREIGN KEY (`IdentityCardAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`IdentityCardMemberId`) REFERENCES `csa`.`member`(`MemberId`);

ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `IdentityCardFileId` VARCHAR(50) NULL,
  ADD COLUMN `IdentityCardAdminId` INT NULL,
  ADD COLUMN `IdentityCardMemberId` BIGINT NULL,
  ADD COLUMN `IdentityCardLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`IdentityCardAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`IdentityCardMemberId`) REFERENCES `csa`.`member`(`MemberId`);


ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `PayslipFileId` VARCHAR(50) NULL,
  ADD COLUMN `PayslipAdminId` INT NULL,
  ADD COLUMN `PayslipMemberId` BIGINT NULL,
  ADD COLUMN `PayslipLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`PayslipAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`PayslipMemberId`) REFERENCES `csa`.`member`(`MemberId`);

ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `ECFileId` VARCHAR(50) NULL,
  ADD COLUMN `ECAdminId` INT NULL,
  ADD COLUMN `ECMemberId` BIGINT NULL,
  ADD COLUMN `ECLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`ECAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`ECMemberId`) REFERENCES `csa`.`member`(`MemberId`);

ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `HRMISFileId` VARCHAR(50) NULL,
  ADD COLUMN `HRMISAdminId` INT NULL,
  ADD COLUMN `HRMISMemberId` BIGINT NULL,
  ADD COLUMN `HRMISLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`HRMISAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`HRMISMemberId`) REFERENCES `csa`.`member`(`MemberId`);

ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `BankStatementFileId` VARCHAR(50) NULL,
  ADD COLUMN `BankStatementAdminId` INT NULL,
  ADD COLUMN `BankStatementMemberId` BIGINT NULL,
  ADD COLUMN `BankStatementLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`BankStatementAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`BankStatementMemberId`) REFERENCES `csa`.`member`(`MemberId`);

ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `LPPSAFileId` VARCHAR(50) NULL,
  ADD COLUMN `LPPSAAdminId` INT NULL,
  ADD COLUMN `LPPSAMemberId` BIGINT NULL,
  ADD COLUMN `LPPSALastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`LPPSAAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`LPPSAMemberId`) REFERENCES `csa`.`member`(`MemberId`);

ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `LicenseFileId` VARCHAR(50) NULL,
  ADD COLUMN `LicenseAdminId` INT NULL,
  ADD COLUMN `LicenseMemberId` BIGINT NULL,
  ADD COLUMN `LicenseLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`LicenseAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`LicenseMemberId`) REFERENCES `csa`.`member`(`MemberId`);

ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `RedemptionLetterFileId` VARCHAR(50) NULL,
  ADD COLUMN `RedemptionLetterAdminId` INT NULL,
  ADD COLUMN `RedemptionLetterMemberId` BIGINT NULL,
  ADD COLUMN `RedemptionLetterLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`RedemptionLetterAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`RedemptionLetterMemberId`) REFERENCES `csa`.`member`(`MemberId`);

ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `CCStatementFileId` VARCHAR(50) NULL,
  ADD COLUMN `CCStatementAdminId` INT NULL,
  ADD COLUMN `CCStatementMemberId` BIGINT NULL,
  ADD COLUMN `CCStatementLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`CCStatementAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`CCStatementMemberId`) REFERENCES `csa`.`member`(`MemberId`);

ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `RAMCIFileId` VARCHAR(50) NULL,
  ADD COLUMN `RAMCIAdminId` INT NULL,
  ADD COLUMN `RAMCIMemberId` BIGINT NULL,
  ADD COLUMN `RAMCILastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`RAMCIAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`RAMCIMemberId`) REFERENCES `csa`.`member`(`MemberId`);

ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `SignatureFileId` VARCHAR(50) NULL,
  ADD COLUMN `SignatureAdminId` INT NULL,
  ADD COLUMN `SignatureMemberId` BIGINT NULL,
  ADD COLUMN `SignatureLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`SignatureAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`SignatureMemberId`) REFERENCES `csa`.`member`(`MemberId`);

ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `BIROFileId` VARCHAR(50) NULL,
  ADD COLUMN `BIROAdminId` INT NULL,
  ADD COLUMN `BIROMemberId` BIGINT NULL,
  ADD COLUMN `BIROLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`BIROAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`BIROMemberId`) REFERENCES `csa`.`member`(`MemberId`);

ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `KEW320FileId` VARCHAR(50) NULL,
  ADD COLUMN `KEW320AdminId` INT NULL,
  ADD COLUMN `KEW320MemberId` BIGINT NULL,
  ADD COLUMN `KEW320LastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`KEW320AdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`KEW320MemberId`) REFERENCES `csa`.`member`(`MemberId`);

ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `StaffCardFileId` VARCHAR(50) NULL,
  ADD COLUMN `StaffCardAdminId` INT NULL,
  ADD COLUMN `StaffCardMemberId` BIGINT NULL,
  ADD COLUMN `StaffCardLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`StaffCardAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`StaffCardMemberId`) REFERENCES `csa`.`member`(`MemberId`);


ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `PostDatedChequeFileId` VARCHAR(50) NULL,
  ADD COLUMN `PostDatedChequeAdminId` INT NULL,
  ADD COLUMN `PostDatedChequeMemberId` BIGINT NULL,
  ADD COLUMN `PostDatedChequeLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`PostDatedChequeAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`PostDatedChequeMemberId`) REFERENCES `csa`.`member`(`MemberId`);

ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `CompanyConfirmationFileId` VARCHAR(50) NULL,
  ADD COLUMN `CompanyConfirmationAdminId` INT NULL,
  ADD COLUMN `CompanyConfirmationMemberId` BIGINT NULL,
  ADD COLUMN `CompanyConfirmationLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`CompanyConfirmationAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`CompanyConfirmationMemberId`) REFERENCES `csa`.`member`(`MemberId`);

ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `EPFFileId` VARCHAR(50) NULL,
  ADD COLUMN `EPFAdminId` INT NULL,
  ADD COLUMN `EPFMemberId` BIGINT NULL,
  ADD COLUMN `EPFLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`EPFAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`EPFMemberId`) REFERENCES `csa`.`member`(`MemberId`);


ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `EAFormFileId` VARCHAR(50) NULL,
  ADD COLUMN `EAFormAdminId` INT NULL,
  ADD COLUMN `EAFormMemberId` BIGINT NULL,
  ADD COLUMN `EAFormLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`EAFormAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`EAFormMemberId`) REFERENCES `csa`.`member`(`MemberId`);

ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `BillUtilitiesFileId` VARCHAR(50) NULL,
  ADD COLUMN `BillUtilitiesAdminId` INT NULL,
  ADD COLUMN `BillUtilitiesMemberId` BIGINT NULL,
  ADD COLUMN `BillUtilitiesLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`BillUtilitiesAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  ADD FOREIGN KEY (`BillUtilitiesMemberId`) REFERENCES `csa`.`member`(`MemberId`);

ALTER TABLE `csa`.`application_8`   
  ADD COLUMN `WorkerTypeId` INT DEFAULT 1  NOT NULL  COMMENT '1-govt 2-private' AFTER `ApplicationId`;

CREATE TABLE `csa`.`application_9`(  
  `Application9Id` BIGINT NOT NULL AUTO_INCREMENT,
  `ApplicationId` BIGINT NOT NULL,
  `OfferLetterFileId` VARCHAR(50),
  `OfferLetterAdminId` INT NOT NULL,
  `OfferLetterLastUpdate` DATETIME,
  `ReloadStatusId` INT,
  `ApprovedDate` DATETIME,
  `SigningDate` DATETIME,
  PRIMARY KEY (`Application9Id`),
  FOREIGN KEY (`ApplicationId`) REFERENCES `csa`.`application`(`ApplicationId`),
  FOREIGN KEY (`OfferLetterAdminId`) REFERENCES `csa`.`admin`(`AdminId`)
);


CREATE TABLE `csa`.`application_10`(  
  `Application10Id` BIGINT NOT NULL AUTO_INCREMENT,
  `ApplicationId` BIGINT NOT NULL,
  PRIMARY KEY (`Application10Id`),
  FOREIGN KEY (`ApplicationId`) REFERENCES `csa`.`application`(`ApplicationId`)
);

ALTER TABLE `csa`.`application_10`   
  ADD COLUMN `DeclarationFormFileId` VARCHAR(50) NULL,
  ADD COLUMN `DeclarationFormAdminId` INT NULL,
  ADD COLUMN `DeclarationFormLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`DeclarationFormAdminId`) REFERENCES `csa`.`admin`(`AdminId`);

ALTER TABLE `csa`.`application_10`   
  ADD COLUMN `SettlementReceiptFileId` VARCHAR(50) NULL,
  ADD COLUMN `SettlementReceiptAdminId` INT NULL,
  ADD COLUMN `SettlementReceiptLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`SettlementReceiptAdminId`) REFERENCES `csa`.`admin`(`AdminId`);


ALTER TABLE `csa`.`application_10`   
  ADD COLUMN `ServiceFeeReceiptFileId` VARCHAR(50) NULL,
  ADD COLUMN `ServiceFeeReceiptAdminId` INT NULL,
  ADD COLUMN `ServiceFeeReceiptLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`ServiceFeeReceiptAdminId`) REFERENCES `csa`.`admin`(`AdminId`);


ALTER TABLE `csa`.`application_10`   
  ADD COLUMN `RezekiReceiptFileId` VARCHAR(50) NULL,
  ADD COLUMN `RezekiReceiptAdminId` INT NULL,
  ADD COLUMN `RezekiReceiptLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`RezekiReceiptAdminId`) REFERENCES `csa`.`admin`(`AdminId`);

ALTER TABLE `csa`.`application_10`   
  ADD COLUMN `RezekiAgreementFileId` VARCHAR(50) NULL,
  ADD COLUMN `RezekiAgreementAdminId` INT NULL,
  ADD COLUMN `RezekiAgreementLastUpdate` DATETIME NULL,
  ADD FOREIGN KEY (`RezekiAgreementAdminId`) REFERENCES `csa`.`admin`(`AdminId`);


ALTER TABLE `csa`.`application_10`   
  ADD COLUMN `ServiceFee` DECIMAL(13,2) NULL AFTER `RezekiAgreementLastUpdate`,
  ADD COLUMN `DepositAmount` DECIMAL(13,2) NULL AFTER `ServiceFee`,
  ADD COLUMN `DepositDate` DATETIME NULL AFTER `DepositAmount`;
  
ALTER TABLE `csa`.`application_2`   
  ADD COLUMN `ApproveStatusId` int NULL AFTER `ApproveAdminId`;
  
ALTER TABLE `csa`.`application`   
  ADD COLUMN `CreditStatusId` int NULL AFTER `RejectedApplicationStatusId`,
  ADD COLUMN `ScoreClass` VARCHAR(100) NULL AFTER `CreditStatusId`;
  
ALTER TABLE `csa`.`application_6`
MODIFY `ApplicationId` BIGINT NOT NULL;

CREATE TABLE `csa`.`application_file`(  
  `ApplicationFileId` BIGINT NOT NULL AUTO_INCREMENT,
  `ApplicationId` BIGINT NOT NULL,
  `FileId` VARCHAR(50),
  `GROUP` VARCHAR(50),  
  `CreateDate` DATETIME,
  `CreateAdminId` INT,
  `CreateMemberId` BIGINT,
  PRIMARY KEY (`ApplicationFileId`),
  FOREIGN KEY (`ApplicationId`) REFERENCES `csa`.`application`(`ApplicationId`),     
  FOREIGN KEY (`CreateAdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  FOREIGN KEY (`CreateMemberId`) REFERENCES `csa`.`member`(`MemberId`)
);

ALTER TABLE `csa`.`application_9`
ADD COLUMN `ApprovedAmount` DECIMAL(13,2);

CREATE TABLE application_history(  
  ApplicationHistoryId BIGINT NOT NULL AUTO_INCREMENT,
  ApplicationId BIGINT NOT NULL,
  CreateDate DATETIME NOT NULL,
  AdminId INT,
  MemberId BIGINT,
  Content MEDIUMTEXT,
  PRIMARY KEY (ApplicationHistoryId),
  FOREIGN KEY (ApplicationId) REFERENCES csa.application(ApplicationId),
  FOREIGN KEY (AdminId) REFERENCES csa.admin(AdminId),
  FOREIGN KEY (MemberId) REFERENCES csa.member(MemberId)
);

CREATE TABLE csa.commission(  
  CommissionId BIGINT NOT NULL AUTO_INCREMENT,
  CommissionTypeId INT NOT NULL COMMENT '1-SurveyEligible 2-ReferrerSurveyEligible 3-SurveyAccepted',
  MemberId BIGINT NOT NULL,
  ReferralMemberId BIGINT,
  AmountCash DECIMAL(13,2),
  AmountPoint DECIMAL(13,2),
  StatusId INT NOT NULL COMMENT '1-Floating 2-Cancelled 3-Paid 4-Refunded',
  Remarks TEXT,
  CreateDate DATETIME NOT NULL,
  PRIMARY KEY (CommissionId)
);

ALTER TABLE `csa`.`member`
ADD COLUMN `ProgramEventId` INT,
ADD COLUMN `TaxNumber` VARCHAR(200),
ADD COLUMN `RaceId` INT,
ADD COLUMN `ReligionId` INT,
ADD COLUMN `HighestLevelOfEducationId` INT,
ADD COLUMN `MaritalStatusId` INT,
ADD COLUMN `SpouseFullName` VARCHAR(200),
ADD COLUMN `SpouseIdentificationNumber` VARCHAR(200),
ADD COLUMN `SpouseContactInformation` VARCHAR(200),
ADD COLUMN `SpouseOccupation` VARCHAR(200),
ADD COLUMN `SpouseCompanyAddress` VARCHAR(200),
ADD COLUMN `SpouseSalary` DECIMAL(13,2),
ADD COLUMN `NumberOfDependent` INT,
ADD COLUMN `IsHaveOKU` TINYINT,
ADD COLUMN `FatherName` VARCHAR(200),
ADD COLUMN `FatherContactNumber` VARCHAR(200),
ADD COLUMN `FatherAddress` TEXT,
ADD COLUMN `MotherName` VARCHAR(200),
ADD COLUMN `MotherConcatNumber` VARCHAR(200),
ADD COLUMN `MotherAddress` TEXT,
ADD COLUMN `CompanyEmployerTypeId` INT,
ADD COLUMN `CompanyJobTitle` VARCHAR(200),
ADD COLUMN `CompanySectorId` INT,
ADD COLUMN `CompanyDepartmentId` INT,
ADD COLUMN `CompanyAddress` TEXT,
ADD COLUMN `CompanyOfficeContactNumber` VARCHAR(200),
ADD COLUMN `CompanyEmploymentStatusId` INT,
ADD COLUMN `CompanyYearOfService` INT,
ADD COLUMN `EmergencyConcatPerson` VARCHAR(200),
ADD COLUMN `EmergencyRelationId` INT,
ADD COLUMN `EmergencyConcatNumber` VARCHAR(200),
ADD COLUMN `EmergencyPersonICNumber` VARCHAR(200),
ADD COLUMN `EmergencyPersonOccupation` VARCHAR(200),
ADD COLUMN `EmergencyPersonAddress` VARCHAR(200),
ADD COLUMN `PayslipFileId` VARCHAR(50),
ADD COLUMN `OfferLetterFileId` VARCHAR(50),
ADD COLUMN `OtherPreferredLanguage` VARCHAR(200),
ADD COLUMN `OtherHobbies` VARCHAR(200),
ADD COLUMN `OtherSocialMediaHandles` VARCHAR(200),
ADD COLUMN `OtherFBName` VARCHAR(200),
ADD COLUMN `SurveyStatusId` INT;

ALTER TABLE member
ADD CONSTRAINT uniquePhoneNumber UNIQUE (PhoneNumber);

ALTER TABLE `csa`.`member`
ADD COLUMN `SalaryRangeId` INT;

CREATE TABLE csa.sector(  
  SectorId INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(100),  
  PRIMARY KEY (SectorId)
);

CREATE TABLE csa.job_position(  
  JobPositionId INT NOT NULL AUTO_INCREMENT,
  SectorId INT,
  `Name` VARCHAR(100),  
  PRIMARY KEY (JobPositionId),
  FOREIGN KEY (SectorId) REFERENCES csa.sector(SectorId)
);

INSERT INTO sector (SectorId,`Name`) VALUES 
(1,'Education'),
(2,'Finance & Banking'),
(3,'Medical'),
(4,'Automotive'),
(5,'Insurance'),
(6,'Oil and Gas'),
(7,'Religious'),
(8,'Construction and Infrastructure'),
(9,'Manufacturing (E&E)'),
(10,'Information & Communication'),
(11,'Hospitality / Food Service');

INSERT INTO job_position (SectorId,`Name`) VALUES 
(1,'Administrative Executive'),  
(1,'Admin assistant/ Lab Assistant'),
(1,'Teacher/ Tutor/ Subject teacher'),
(1,'Education Officer/Counsellor'),
(1,'Trainer/ Manager'),
(1,'PPD officer'),
(1,'Lecturer'),
(1,'Senior Lecturer/ Professor'),
(1,'Researcher'), 
(1,'Dean (School/ Faculty)'), 
(1,'Principal/ Headmaster'), 
(1,'Deputy/ Vice Principal'), 
(1,'Vice Chancellor');

INSERT INTO job_position (SectorId,`Name`) VALUES 
(2,'Customer Service Officer/ Bank Teller'),  
(2,'Clerk/ Admin Assistant/ Personal Assistant'),  
(2,'Credit/ Loan/ Finance Officer'),  
(2,'Junior Administrative'),  
(2,'Banker/ Junior Auditor'),  
(2,'Senior Auditor'),  
(2,'Trader'),  
(2,'Manager / Senior Level / HOD'),  
(2,'Branch Manager'),  
(2,'Vice President'),  
(2,'Board of Directors'),  
(2,'CEO/COO/CFO/CRO/CCO/CTO'),  
(2,'Executive'),  
(2,'Teller'); 

INSERT INTO job_position (SectorId,`Name`) VALUES 
(3,'Medical / Nurse / Lab Assistant'), 
(3,'Administrator/ Officer'), 
(3,'Staff Nurse (Registered Nurse)'), 
(3,'Nurse Manager / Nurse Supervisor'), 
(3,'Senior Staff'), 
(3,'Chief Nursing Officer (CNO)'), 
(3,'Junior doctor (Housemanship)'), 
(3,'Researcher/ Scientist'), 
(3,'Medical Officer (MO)'), 
(3,'Physio/ Psycho/ Speech/ Health Therapist'), 
(3,'Medical Specialist/ Senior Medical Officer'), 
(3,'Manager/ HOD'), 
(3,'CMO/ Director'); 

INSERT INTO job_position (SectorId,`Name`) VALUES 
(4,'Technician/ Operator/ Electrician'), 
(4,'Junior Automotive Engineer'), 
(4,'Admin Officer / Clerk'), 
(4,'Sales / Marketing Executive'), 
(4,'Supervisor/ Senior Level Staff'), 
(4,'Senior Engineer/ Specialised Engineer'), 
(4,'Manager/HOD'), 
(4,'COO/CEO/CTO/Director'); 

INSERT INTO job_position (SectorId,`Name`) VALUES 
(5,'Administrative / Underwriting Assistant'), 
(5,'Sales Agent/ Financial Advisor'), 
(5,'Claims Officer'), 
(5,'Customer Service Representative (CSR)'), 
(5,'Junior Underwriter/ Executive/ Actuary'), 
(5,'Senior Underwriter/ Executive/ Actuary'), 
(5,'Data Analyst/ Scientist'), 
(5,'Manager/HOD'), 
(5,'CCO/ CSO/ CRO/ CMO/ COO/CEO/BOD'), 
(5,'Executive'), 
(5,'Teller'), 
(5,'Auditor');

INSERT INTO job_position (SectorId,`Name`) VALUES 
(6,'Assistant / Technician/ Operator'), 
(6,'Officer/ Coordinator'), 
(6,'Executive'), 
(6,'Junior Engineer'), 
(6,'Senior Executive/ management'), 
(6,'Senior Engineer'), 
(6,'Supervisor / Senior level staff'), 
(6,'Manager'), 
(6,'CFO/COO/CEO/BOD'); 

INSERT INTO job_position (SectorId,`Name`) VALUES 
(7,'Imam'),
(7,'Mufti'),
(7,'Islamic Judge (Kadi/Qadi)'),
(7,'Islamic Teacher (Ustaz/Ustazah)'),
(7,'Muezzin (Bilal)'),
(7,'Religious Affairs Officer'),
(7,'Assistant Religious Affairs Officer'),
(7,'Assistant Syariah Officer'),
(7,'Islamic Missionary (Mubaligh)'),
(7,'Islamic Counselor'),
(7,'Mosque Administrator'),
(7,'Islamic Education Teacher'),
(7,'Islamic Affairs Supervisor'),
(7,'Religious Research Officer'),
(7,'Dakwah Program Manager'),
(7,'Islamic Affairs Assistant'),
(7,'Islamic Marriage Counselor'),
(7,'Director of Religious Affairs'),
(7,'Chaplain'),
(7,'Priest'),
(7,'Pastor'),
(7,'Monk/Nun'); 


INSERT INTO job_position (SectorId,`Name`) VALUES 
(8,'Worker/Helper/Technician Assistant'),
(8,'Junior Technician/ Drafter'),
(8,'Project Coordinator'),
(8,'Junior Engineer'),
(8,'Executive'),
(8,'Quantity Surveyor'),
(8,'Supervisor/ Senior Executive'),
(8,'Junior Architect'),
(8,'Senior Engineer'),
(8,'Manager'),
(8,'Architect'),
(8,'CEO/COO/ Director');

INSERT INTO job_position (SectorId,`Name`) VALUES 
(9,'Operator/ Technician/ Worker'),
(9,'Junior Engineer'),
(9,'Executive'),
(9,'Senior Engineer'),
(9,'Supervisor/ Senior Executive'),
(9,'Manager/HOD'),
(9,'CTO/CEO/BOD');

INSERT INTO job_position (SectorId,`Name`) VALUES 
(10,'Help Desk Technician'),
(10,'Executive Technology (ICT) / Telecommunication'),
(10,'Software/Web/Mobile Application/ any Developer'),
(10,'IT Consultant'),
(10,'Systems/ Cybersecurity Analyst'),
(10,'Network/Database Administrator'),
(10,'IT/ Project/ any Manager'),
(10,'IT Support/AI Specialist'),
(10,'Cloud/Developer/System/ Network/ any Engineer'),
(10,'Business/Data Analyst'),
(10,'Front-End/ Back-End/ Full Stack'),
(10,'UX/UI Designer'),
(10,'Quality Assurance (QA) Tester'),
(10,'Software Architect'),
(10,'IT Auditor'),
(10,'Data Scientist'),
(10,'Manager/HOD'),
(10,'CTO/CEO/BOD');

INSERT INTO job_position (SectorId,`Name`) VALUES 
(11,'Chef'),
(11,'Assistant / Helper'),
(11,'Admin/ Operation/ any Executive'),
(11,'Supervisor/ Senior Executive'),
(11,'Manager'),
(11,'Director');

ALTER TABLE `csa`.`member`
ADD COLUMN `CompanyEmployerTypeOther` VARCHAR(200),
ADD COLUMN `CompanyEmploymentStatusOther` VARCHAR(200),
ADD COLUMN `AdminRemark` VARCHAR(500),
ADD COLUMN `CompanySectorOther` VARCHAR(200),
ADD COLUMN `CompanyDepartementOther` VARCHAR(200),
ADD COLUMN `CompanyEmployerName` VARCHAR(200);

ALTER TABLE `csa`.`job_position`
ADD COLUMN `MalayName` VARCHAR(100);

ALTER TABLE `csa`.`sector`
ADD COLUMN `MalayName` VARCHAR(100);

UPDATE job_position SET MalayName='Eksekutif Pentadbiran' WHERE JobPositionId = 1;
UPDATE job_position SET MalayName='Pembantu Pentadbiran/ Pembantu Makmal' WHERE JobPositionId = 2;
UPDATE job_position SET MalayName='Guru/ Tutor/ Guru Mata Pelajaran' WHERE JobPositionId = 3;
UPDATE job_position SET MalayName='Pegawai Pendidikan/ Kaunselor' WHERE JobPositionId = 4;
UPDATE job_position SET MalayName='Jurulatih/ Pengurus' WHERE JobPositionId = 5;
UPDATE job_position SET MalayName='Pegawai PPD' WHERE JobPositionId = 6;
UPDATE job_position SET MalayName='Pensyarah' WHERE JobPositionId = 7;
UPDATE job_position SET MalayName='Pensyarah Kanan/ Profesor' WHERE JobPositionId = 8;
UPDATE job_position SET MalayName='Penyelidik' WHERE JobPositionId = 9;
UPDATE job_position SET MalayName='Dekan (Sekolah/ Fakulti)' WHERE JobPositionId = 10;
UPDATE job_position SET MalayName='Pengetua/ Guru Besar' WHERE JobPositionId = 11;
UPDATE job_position SET MalayName='Timbalan/ Penolong Pengetua' WHERE JobPositionId = 12;
UPDATE job_position SET MalayName='Naib Canselor' WHERE JobPositionId = 13;
UPDATE job_position SET MalayName='Pegawai Khidmat Pelanggan/ Juruwang Bank' WHERE JobPositionId = 14;
UPDATE job_position SET MalayName='Kerani/ Pembantu Pentadbiran/ Pembantu Peribadi' WHERE JobPositionId = 15;
UPDATE job_position SET MalayName='Pegawai Kredit/ Pinjaman/ Kewangan' WHERE JobPositionId = 16;
UPDATE job_position SET MalayName='Pentadbir Junior' WHERE JobPositionId = 17;
UPDATE job_position SET MalayName='Pegawai Bank/ Juruaudit Junior' WHERE JobPositionId = 18;
UPDATE job_position SET MalayName='Juruaudit Senior' WHERE JobPositionId = 19;
UPDATE job_position SET MalayName='Peniaga' WHERE JobPositionId = 20;
UPDATE job_position SET MalayName='Pengurus/ Peringkat Kanan/ Ketua Jabatan' WHERE JobPositionId = 21;
UPDATE job_position SET MalayName='Pengurus Cawangan' WHERE JobPositionId = 22;
UPDATE job_position SET MalayName='Naib Presiden' WHERE JobPositionId = 23;
UPDATE job_position SET MalayName='Lembaga Pengarah' WHERE JobPositionId = 24;
UPDATE job_position SET MalayName='CEO/COO/CFO/CRO/CCO/CTO' WHERE JobPositionId = 25;
UPDATE job_position SET MalayName='Eksekutif' WHERE JobPositionId = 26;
UPDATE job_position SET MalayName='Juruwang' WHERE JobPositionId = 27;
UPDATE job_position SET MalayName='Pembantu Perubatan/ Jururawat/ Pembantu Makmal' WHERE JobPositionId = 28;
UPDATE job_position SET MalayName='Pentadbir/ Pegawai' WHERE JobPositionId = 29;
UPDATE job_position SET MalayName='Jururawat (Jururawat Berdaftar)' WHERE JobPositionId = 30;
UPDATE job_position SET MalayName='Pengurus Jururawat/ Penyelia Jururawat' WHERE JobPositionId = 31;
UPDATE job_position SET MalayName='Staf Senior' WHERE JobPositionId = 32;
UPDATE job_position SET MalayName='Ketua Pegawai Kejururawatan (CNO)' WHERE JobPositionId = 33;
UPDATE job_position SET MalayName='Doktor Junior (Housemanship)' WHERE JobPositionId = 34;
UPDATE job_position SET MalayName='Penyelidik/ Saintis' WHERE JobPositionId = 35;
UPDATE job_position SET MalayName='Pegawai Perubatan (MO)' WHERE JobPositionId = 36;
UPDATE job_position SET MalayName='Ahli Terapi Fisioterapi/ Psikologi/ Pertuturan/ Kesihatan' WHERE JobPositionId = 37;
UPDATE job_position SET MalayName='Pakar Perubatan/ Pegawai Perubatan Senior' WHERE JobPositionId = 38;
UPDATE job_position SET MalayName='Pengurus/ Ketua Jabatan' WHERE JobPositionId = 39;
UPDATE job_position SET MalayName='CMO/ Pengarah' WHERE JobPositionId = 40;
UPDATE job_position SET MalayName='Juruteknik/ Operator/ Juruelektrik' WHERE JobPositionId = 41;
UPDATE job_position SET MalayName='Jurutera Automotif Junior' WHERE JobPositionId = 42;
UPDATE job_position SET MalayName='Pegawai Pentadbiran/ Kerani' WHERE JobPositionId = 43;
UPDATE job_position SET MalayName='Eksekutif Jualan/ Pemasaran' WHERE JobPositionId = 44;
UPDATE job_position SET MalayName='Penyelia/ Staf Peringkat Kanan' WHERE JobPositionId = 45;
UPDATE job_position SET MalayName='Jurutera Senior/ Jurutera Khas' WHERE JobPositionId = 46;
UPDATE job_position SET MalayName='Pengurus/ Ketua Jabatan' WHERE JobPositionId = 47;
UPDATE job_position SET MalayName='COO/CEO/CTO/ Pengarah' WHERE JobPositionId = 48;
UPDATE job_position SET MalayName='Pembantu Pentadbiran/ Underwriting' WHERE JobPositionId = 49;
UPDATE job_position SET MalayName='Ejen Jualan/ Penasihat Kewangan' WHERE JobPositionId = 50;
UPDATE job_position SET MalayName='Pegawai Tuntutan' WHERE JobPositionId = 51;
UPDATE job_position SET MalayName='Wakil Khidmat Pelanggan (CSR)' WHERE JobPositionId = 52;
UPDATE job_position SET MalayName='Underwriter/ Eksekutif/ Aktuari Junior' WHERE JobPositionId = 53;
UPDATE job_position SET MalayName='Underwriter/ Eksekutif/ Aktuari Senior' WHERE JobPositionId = 54;
UPDATE job_position SET MalayName='Penganalisis Data/ Saintis' WHERE JobPositionId = 55;
UPDATE job_position SET MalayName='Pengurus/ Ketua Jabatan' WHERE JobPositionId = 56;
UPDATE job_position SET MalayName='CCO/ CSO/ CRO/ CMO/ COO/ CEO/ Lembaga Pengarah' WHERE JobPositionId = 57;
UPDATE job_position SET MalayName='Eksekutif' WHERE JobPositionId = 58;
UPDATE job_position SET MalayName='Juruwang' WHERE JobPositionId = 59;
UPDATE job_position SET MalayName='Juruaudit' WHERE JobPositionId = 60;

UPDATE job_position SET MalayName='Pembantu/ Juruteknik/ Operator' WHERE JobPositionId = 61;
UPDATE job_position SET MalayName='Pegawai/ Penyelaras' WHERE JobPositionId = 62;
UPDATE job_position SET MalayName='Eksekutif' WHERE JobPositionId = 63;
UPDATE job_position SET MalayName='Jurutera Junior' WHERE JobPositionId = 64;
UPDATE job_position SET MalayName='Eksekutif Senior/ Pengurusan' WHERE JobPositionId = 65;
UPDATE job_position SET MalayName='Jurutera Senior' WHERE JobPositionId = 66;
UPDATE job_position SET MalayName='Penyelia/ Staf Peringkat Kanan' WHERE JobPositionId = 67;
UPDATE job_position SET MalayName='Pengurus' WHERE JobPositionId = 68;
UPDATE job_position SET MalayName='CFO/COO/CEO/ Lembaga Pengarah' WHERE JobPositionId = 69;

UPDATE job_position SET MalayName='Imam' WHERE JobPositionId = 70;
UPDATE job_position SET MalayName='Mufti' WHERE JobPositionId = 71;
UPDATE job_position SET MalayName='Hakim Islam (Kadi/Qadi)' WHERE JobPositionId = 72;
UPDATE job_position SET MalayName='Guru Agama (Ustaz/Ustazah)' WHERE JobPositionId = 73;
UPDATE job_position SET MalayName='Bilal (Muazin)' WHERE JobPositionId = 74;
UPDATE job_position SET MalayName='Pegawai Hal Ehwal Agama' WHERE JobPositionId = 75;
UPDATE job_position SET MalayName='Penolong Pegawai Hal Ehwal Agama' WHERE JobPositionId = 76;
UPDATE job_position SET MalayName='Penolong Pegawai Syariah' WHERE JobPositionId = 77;
UPDATE job_position SET MalayName='Pendakwah Islam (Mubaligh)' WHERE JobPositionId = 78;
UPDATE job_position SET MalayName='Kaunselor Islam' WHERE JobPositionId = 79;
UPDATE job_position SET MalayName='Pentadbir Masjid' WHERE JobPositionId = 80;
UPDATE job_position SET MalayName='Guru Pendidikan Islam' WHERE JobPositionId = 81;
UPDATE job_position SET MalayName='Penyelia Hal Ehwal Islam' WHERE JobPositionId = 82;
UPDATE job_position SET MalayName='Pegawai Penyelidik Agama' WHERE JobPositionId = 83;
UPDATE job_position SET MalayName='Pengurus Program Dakwah' WHERE JobPositionId = 84;
UPDATE job_position SET MalayName='Pembantu Hal Ehwal Islam' WHERE JobPositionId = 85;
UPDATE job_position SET MalayName='Kaunselor Perkahwinan Islam' WHERE JobPositionId = 86;
UPDATE job_position SET MalayName='Pengarah Hal Ehwal Agama' WHERE JobPositionId = 87;
UPDATE job_position SET MalayName='Paderi' WHERE JobPositionId = 88;
UPDATE job_position SET MalayName='Paderi (Kristian)' WHERE JobPositionId = 89;
UPDATE job_position SET MalayName='Paderi Gereja (Pastor)' WHERE JobPositionId = 90;
UPDATE job_position SET MalayName='Sami/Biarawati' WHERE JobPositionId = 91;

UPDATE job_position SET MalayName='Pekerja/Pembantu/Pembantu Juruteknik' WHERE JobPositionId = 92;
UPDATE job_position SET MalayName='Juruteknik Junior/Pelukis Pelan' WHERE JobPositionId = 93;
UPDATE job_position SET MalayName='Penyelaras Projek' WHERE JobPositionId = 94;
UPDATE job_position SET MalayName='Jurutera Junior' WHERE JobPositionId = 95;
UPDATE job_position SET MalayName='Eksekutif' WHERE JobPositionId = 96;
UPDATE job_position SET MalayName='Juruukur Bahan' WHERE JobPositionId = 97;
UPDATE job_position SET MalayName='Penyelia/Eksekutif Kanan' WHERE JobPositionId = 98;
UPDATE job_position SET MalayName='Arkitek Junior' WHERE JobPositionId = 99;
UPDATE job_position SET MalayName='Jurutera Kanan' WHERE JobPositionId = 100;
UPDATE job_position SET MalayName='Pengurus' WHERE JobPositionId = 101;
UPDATE job_position SET MalayName='Arkitek' WHERE JobPositionId = 102;
UPDATE job_position SET MalayName='CEO/COO/Pengarah' WHERE JobPositionId = 103;

UPDATE job_position SET MalayName='Operator/Juruteknik/Pekerja' WHERE JobPositionId = 104;
UPDATE job_position SET MalayName='Jurutera Junior' WHERE JobPositionId = 105;
UPDATE job_position SET MalayName='Eksekutif' WHERE JobPositionId = 106;
UPDATE job_position SET MalayName='Jurutera Kanan' WHERE JobPositionId = 107;
UPDATE job_position SET MalayName='Penyelia/Eksekutif Kanan' WHERE JobPositionId = 108;
UPDATE job_position SET MalayName='Pengurus/Ketua Jabatan' WHERE JobPositionId = 109;
UPDATE job_position SET MalayName='CTO/CEO/Lembaga Pengarah' WHERE JobPositionId = 110;

UPDATE job_position SET MalayName='Juruteknik Meja Bantuan' WHERE JobPositionId = 111;
UPDATE job_position SET MalayName='Eksekutif Teknologi (ICT)/Telekomunikasi' WHERE JobPositionId = 112;
UPDATE job_position SET MalayName='Pembangun Perisian/Web/Aplikasi Mudah Alih/ Pembangun berkaitan' WHERE JobPositionId = 113;
UPDATE job_position SET MalayName='Perunding IT' WHERE JobPositionId = 114;
UPDATE job_position SET MalayName='Penganalisis Sistem/Keselamatan Siber' WHERE JobPositionId = 115;
UPDATE job_position SET MalayName='Pentadbir Rangkaian/Pangkalan Data' WHERE JobPositionId = 116;
UPDATE job_position SET MalayName='Pengurus IT/Projek/ Pengurus berkaitan' WHERE JobPositionId = 117;
UPDATE job_position SET MalayName='Sokongan IT/Pakar AI' WHERE JobPositionId = 118;
UPDATE job_position SET MalayName='Jurutera Awan/Pembangun/Sistem/Rangkaian/ Jurutera berkaitan' WHERE JobPositionId = 119;
UPDATE job_position SET MalayName='Penganalisis Perniagaan/Data' WHERE JobPositionId = 120;
UPDATE job_position SET MalayName='Front-End/Back-End/Full Stack' WHERE JobPositionId = 121;
UPDATE job_position SET MalayName='Pereka UX/UI' WHERE JobPositionId = 122;
UPDATE job_position SET MalayName='Penguji Jaminan Kualiti (QA)' WHERE JobPositionId = 123;
UPDATE job_position SET MalayName='Arkitek Perisian' WHERE JobPositionId = 124;
UPDATE job_position SET MalayName='Juruaudit IT' WHERE JobPositionId = 125;
UPDATE job_position SET MalayName='Saintis Data' WHERE JobPositionId = 126;
UPDATE job_position SET MalayName='Pengurus/Ketua Jabatan' WHERE JobPositionId = 127;
UPDATE job_position SET MalayName='CTO/CEO/Lembaga Pengarah' WHERE JobPositionId = 128;

UPDATE job_position SET MalayName='Cef' WHERE JobPositionId = 129;
UPDATE job_position SET MalayName='Pembantu' WHERE JobPositionId = 130;
UPDATE job_position SET MalayName='Eksekutif Pentadbiran/Operasi/Eksekutif berkaitan' WHERE JobPositionId = 131;
UPDATE job_position SET MalayName='Penyelia/Eksekutif Kanan' WHERE JobPositionId = 132;
UPDATE job_position SET MalayName='Pengurus' WHERE JobPositionId = 133;
UPDATE job_position SET MalayName='Pengarah' WHERE JobPositionId = 134;

UPDATE sector SET MalayName = 'Pendidikan' WHERE SectorId = 1;
UPDATE sector SET MalayName = 'Kewangan & Perbankan' WHERE SectorId = 2;
UPDATE sector SET MalayName = 'Perubatan' WHERE SectorId = 3;
UPDATE sector SET MalayName = 'Automotif' WHERE SectorId = 4;
UPDATE sector SET MalayName = 'Insurans' WHERE SectorId = 5;
UPDATE sector SET MalayName = 'Minyak dan Gas' WHERE SectorId = 6;
UPDATE sector SET MalayName = 'Agama' WHERE SectorId = 7;
UPDATE sector SET MalayName = 'Pembinaan dan Infrastruktur' WHERE SectorId = 8;
UPDATE sector SET MalayName = 'Pembuatan (E&E)' WHERE SectorId = 9;
UPDATE sector SET MalayName = 'Maklumat & Komunikasi' WHERE SectorId = 10;
UPDATE sector SET MalayName = 'Hospitaliti / Perkhidmatan Makanan' WHERE SectorId = 11;

ALTER TABLE `csa`.`history`
ADD COLUMN `InitAmount` DECIMAL(13,2);

ALTER TABLE `csa`.`withdrawal`
ADD COLUMN `ParentMemberId` BIGINT,
ADD FOREIGN KEY (ParentMemberId) REFERENCES `csa`.`member`(`MemberId`);

INSERT INTO setting (`Code`,`StrValue`,`TextValue`) VALUES 
("YabamCompleteSurveyEmail","Tahniah! Anda telah Berjaya melengkapi Tinjauan Program Literasi yang
dibawakan khas oleh YABAM",'<p>Salam Sejahtera {Dear},</p>
    
<p>Terima kasih kerana meluangkan masa untuk melengkapi butiran Tinjauan Program Literasi dan berdaftar sebagai ahli baru Yayasan Amanah Bantuan Awam Malaysia (YABAM). Kami amat menghargai komitmen anda dalam menyertai inisiatif ini.</p>

<p>Berikut adalah maklumat penting bagi Langkah seterusnya:</p>

<ol>
<li>
    <p>Token Penghargaan RM200 (Tinjauan):</p>
    <p>Token penghargaan bernilai RM200 akan dikreditkan ke akaun anda dalam tempoh 3 hari bekerja selepas proses validasi selesai dijalankan.</p>
</li>
<li>
    <p>Makluman Kelayakan Zoom 1-1:</p>
    <p>Anda akan dihubungi melalui emel dalam tempoh 7-14 hari untuk memaklumkan tentang kelayakan anda menyertai sesi Zoom 1-1 bersama Pegawai YABAM.</p>
</li>
</ol>

<p>Sekiranya anda mempunyai sebarang pertanyaan atau memerlukan bantuan lanjut, sila hubungi kami di <a href="mailto:info@yabam.org.my">info@yabam.org.my</a> atau Whatsapp ke 012-8011578 (Rose).</p>

<p>Sekali lagi, terima kasih atas kepercayaan dan sokongan anda terhadap YABAM. Kami tidak sabar untuk berhubung dengan anda dan membantu anda mencapai kejayaan dalam program ini!</p>

<p>Salam hormat,<br>
Yayasan Amanah Bantuan Awam Malaysia (YABAM)</p>'),
("CommissionRegisterNewMember","200",NULL),
("CommissionRegisterUpline","300",NULL);

ALTER TABLE member
ADD CONSTRAINT uniqueICNumber UNIQUE (ICNumber);

UPDATE setting SET StrValue='Tahniah! Anda telah Berjaya melengkapi Tinjauan Program Literasi yang dibawakan khas oleh YABAM', TextValue='<p>Salam Sejahtera {Dear},</p>

    <p>Terima kasih kerana meluangkan masa untuk melengkapi butiran Tinjauan Program Literasi dan berdaftar sebagai ahli baru Yayasan Amanah Bantuan Awam Malaysia (YABAM). Kami amat menghargai komitmen anda dalam menyertai program ini.</p>

    <p>Berikut adalah maklumat penting bagi Langkah seterusnya:</p>

    <ol>
        <li><strong>Ganjaran RM200 (Tinjauan):</strong><br>
            Ganjaran bernilai RM200 akan dikreditkan ke akaun anda sekurangnya dalam tempoh 3 hari bekerja selepas proses validasi selesai dijalankan.</li>
        <li><strong>Makluman Kelayakan Zoom 1-1:</strong><br>
            Anda akan dihubungi melalui emel dalam tempoh 7-14 hari untuk memaklumkan tentang kelayakan anda menyertai sesi Zoom 1-1 bersama Pegawai YABAM.</li>
    </ol>

    <p>Ikuti langkah berikut untuk semak status Ganjaran anda :-</p>
    <ol>
        <li>Tekan <strong>Link Member Portal</strong> <a href="{Link}">{Link}</a></li>
        <li>Login YABAM Member Portal {Username} & {Password}</li>
        <li>Klik <strong>Ganjaran Status</strong></li>
        <li>Semak Status Ganjaran anda di ruangan yang tertera</li>
    </ol>

    <p>Sekiranya anda mempunyai sebarang pertanyaan atau memerlukan bantuan lanjut, sila hubungi kami melalui emel <a href="mailto:info@yabam.org.my">info@yabam.org.my</a> atau Whatsapp ke 012-8011578 (Sarah).</p>

    <p>Sekali lagi, terima kasih atas kepercayaan dan sokongan anda terhadap YABAM. Kami tidak sabar untuk berhubung dengan anda dan membantu anda mencapai kejayaan dalam program ini!</p>

    <p>Salam hormat,</p>
    <p>Yayasan Amanah Bantuan Awam Malaysia (YABAM)</p>' WHERE `Code`='YabamCompleteSurveyEmail';
	
	
CREATE TABLE `csa`.`application_remark`(  
  `ApplicationRemarkId` BIGINT NOT NULL AUTO_INCREMENT,
  `ApplicationId` BIGINT NOT NULL,
  `ApplicationStatusId` INT,
  `Remark` TEXT,
  `AdminId` INT NOT NULL,
  `UpdatedAdminId` INT,  
  `CreatedDate` DATETIME NOT NULL,
  `UpdatedDate` DATETIME,
  PRIMARY KEY (`ApplicationRemarkId`),
  FOREIGN KEY (`ApplicationId`) REFERENCES `csa`.`application`(`ApplicationId`),
  FOREIGN KEY (`AdminId`) REFERENCES `csa`.`admin`(`AdminId`),
  FOREIGN KEY (`UpdatedAdminId`) REFERENCES `csa`.`admin`(`AdminId`)
);

ALTER TABLE `csa`.`application`
ADD COLUMN `PreparedAdminId` INT AFTER `SourceId`,
ADD COLUMN `AnalyzedAdminId` INT AFTER `PreparedAdminId`,
ADD FOREIGN KEY (PreparedAdminId) REFERENCES `csa`.`admin`(`AdminId`),
ADD FOREIGN KEY (AnalyzedAdminId) REFERENCES `csa`.`admin`(`AdminId`);

ALTER TABLE `csa`.`bank`
ADD COLUMN `Index` INT default 0 NOT NULL;

-- for clean bank
-- update member set BankId = null;
-- UPDATE withdrawal SET BankId = NULL;
-- Truncate settlement;
-- TRUNCATE caseupdate;
-- TRUNCATE bank;
-- ALTER TABLE bank AUTO_INCREMENT = 1;

ALTER TABLE bank AUTO_INCREMENT = 1;

INSERT INTO `bank` (`Name`,StatusId,CountryId,`Index`) VALUES 
('BANK RAKYAT (BIMB)',1,1,0),
('BANK ISLAM (BKRM)',1,1,1),
('BANK MUAMALAT (MUAMALAT)',1,1,2),
('BANK SIMPANAN NASIONAL (BSN)',1,1,3),
('MALAYSIA BUILDING SOCIETY BERHAD (MBSB)',1,1,4),
('KOPERASI CO-OPBANK PERTAMA MALAYSIA BERHAD (CBP)',1,1,5),
('AFFIN BANK',1,1,6),
('RHB BANK',1,1,7),
('CIMB BANK',1,1,8),
('MAYBANK',1,1,9),
('HONG LEONG BANK',1,1,10),
('AMBANK',1,1,11),
('PUBLIC BANK',1,1,12),
('AGRO BANK',1,1,13),
('STANDARD CHARTERED',1,1,14),
('OCBC',1,1,15);

ALTER TABLE settlement
ADD FacilitiesId INT,
ADD FlexyCampaignOther VARCHAR(200),
CHANGE Facilities FacilitiesOther VARCHAR(200);

ALTER TABLE application_2
ADD VerifiedAdminId INT,
ADD VerifiedStatusId INT,
ADD VerifiedDate DATETIME,
ADD VerifiedComment TEXT,
ADD FOREIGN KEY (VerifiedAdminId) REFERENCES `admin`(AdminId);

ALTER TABLE application_1
ADD PayslipFileId VARCHAR(50),
ADD PayslipAdminId INT,
ADD PayslipLastUpdate DATETIME,
ADD FOREIGN KEY (PayslipAdminId) REFERENCES `admin`(AdminId);

-- clean data before modify table
-- update application_5 set BankruptcyStatus=null,LegalCase=null,HealthCreditScore=null,Commitements=null;

ALTER TABLE application_5
MODIFY COLUMN BankruptcyStatus INT,
MODIFY COLUMN LegalCase INT,
MODIFY COLUMN HealthCreditScore INT,
MODIFY COLUMN Commitements DECIMAL(13,2);

ALTER TABLE application_7
ADD BankruptcyStatus INT,
ADD LegalCase INT,
ADD HealthCreditScore INT,
ADD Commitments VARCHAR(200);

ALTER TABLE application_7
ADD HRMISFileId VARCHAR(50),
ADD HRMISAdminId INT,
ADD HRMISLastUpdate DATETIME,
ADD ANMFileId VARCHAR(50),
ADD ANMAdminId INT,
ADD ANMLastUpdate DATETIME,
ADD LPSAFileId VARCHAR(50),
ADD LPSAAdminId INT,
ADD LPSALastUpdate DATETIME,
ADD AngkasaFileId VARCHAR(50),
ADD AngkasaAdminId INT,
ADD AngkasaLastUpdate DATETIME,
ADD FOREIGN KEY (HRMISAdminId) REFERENCES `admin`(AdminId),
ADD FOREIGN KEY (ANMAdminId) REFERENCES `admin`(AdminId),
ADD FOREIGN KEY (LPSAAdminId) REFERENCES `admin`(AdminId),
ADD FOREIGN KEY (AngkasaAdminId) REFERENCES `admin`(AdminId);

ALTER TABLE member
ADD FileNumber VARCHAR(50);

-- set null first for role
UPDATE admin SET RoleId = NULL;

INSERT INTO role (`Name`,`AccessList`) VALUES 
('CREDIT TEAM','1'),
('VERIFIED OFFICERS','2'),
('CREDIT DEPT','3'),
('SALES DIRECTOR','4'),
('RM','5'),
('PA','6');

ALTER TABLE application_10
ADD SettlementDate DATETIME,
ADD SettlementAmount DECIMAL(13,2),
ADD SettlementCPct DECIMAL(3,2),
ADD CollectionAmountPct DECIMAL(3,2),
ADD SettlementDuration INT,
ADD TotalReloan DECIMAL(13,2),
ADD TotalLoanRepayment DECIMAL(13,2),
ADD DBBBankAccount VARCHAR(200),
ADD DBBTenure VARCHAR(200),
ADD DBBAgreementDate DATETIME,
ADD MonthlyFund DECIMAL(13,2),
ADD DBBAmount DECIMAL(13,2),
ADD ReceiptNo VARCHAR(200),
ADD TaxNumber VARCHAR(255),
ADD StatusId INT,
ADD InstallmentDate DATETIME;

ALTER TABLE Member
ADD BankOther varchar(255) AFTER BankId;

INSERT INTO `bank` (BankId,`Name`,StatusId,CountryId,`Index`) VALUES 
(999,'Other',1,1,999);

ALTER TABLE Member
ADD WalletSavings DECIMAL(13,2) AFTER WalletPoint,
ADD NotEntitled INT AFTER StatusId;

