CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `hotels` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Stars` int NOT NULL,
    `Address` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `WebPageUrl` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_hotels` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221128102529_InitialCreate', '6.0.7');

COMMIT;

START TRANSACTION;

CREATE UNIQUE INDEX `IX_hotels_Name` ON `hotels` (`Name`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221128105408_HotelNameUnique', '6.0.7');

COMMIT;

START TRANSACTION;

CREATE TABLE `travelModes` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_travelModes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `offers` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Destination` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `Price` decimal(65,30) NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `MaxParticipants` int NOT NULL,
    `Photo` longblob NULL,
    `ModeId` int NOT NULL,
    `HotelId` int NOT NULL,
    CONSTRAINT `PK_offers` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_offers_hotels_HotelId` FOREIGN KEY (`HotelId`) REFERENCES `hotels` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_offers_travelModes_ModeId` FOREIGN KEY (`ModeId`) REFERENCES `travelModes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `applies` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `NumberOfPeople` int NOT NULL,
    `Email` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `PhoneNumber` varchar(30) CHARACTER SET utf8mb4 NULL,
    `OfferId` int NULL,
    CONSTRAINT `PK_applies` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_applies_offers_OfferId` FOREIGN KEY (`OfferId`) REFERENCES `offers` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_applies_OfferId` ON `applies` (`OfferId`);

CREATE INDEX `IX_offers_HotelId` ON `offers` (`HotelId`);

CREATE INDEX `IX_offers_ModeId` ON `offers` (`ModeId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221128111124_ThreeMoreTables', '6.0.7');

COMMIT;

START TRANSACTION;

INSERT INTO `travelModes` (`Id`, `Name`)
VALUES (1, 'busz');

INSERT INTO `travelModes` (`Id`, `Name`)
VALUES (2, 'repülő');

INSERT INTO `travelModes` (`Id`, `Name`)
VALUES (5, 'hajó');

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20221128111653_travelModeRecords', '6.0.7');

COMMIT;

