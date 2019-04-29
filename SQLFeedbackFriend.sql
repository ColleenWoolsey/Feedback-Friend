CREATE TABLE `User`
(
  `UserId` int PRIMARY KEY,
  `FirstName` varchar(255) NOT NULL,
  `LastName` varchar(255) NOT NULL,
  `Email` Email NOT NULL,
  `Password` varchar(255) NOT NULL
);

CREATE TABLE `Survey`
(
  `SurveyId` int PRIMARY KEY,
  `UserId` int NOT NULL,
  `SurveyName` varchar(255) NOT NULL,
  `Instructions` varchar(255) NOT NULL,
  `Description` varchar(255)
);

CREATE TABLE `UserSurvey`
(
  `UserSurveyId` int PRIMARY KEY,
  `SurveyId` int NOT NULL,
  `ResponderId` int,
  `FocusId` int
);

CREATE TABLE `Question`
(
  `QuestionId` int PRIMARY KEY,
  `SurveyId` int NOT NULL,
  `Question` varchar(255) NOT NULL
);

CREATE TABLE `Answer`
(
  `AnswerId` int PRIMARY KEY,
  `ResponderId` int NOT NULL,
  `UserSurveyId` int NOT NULL,
  `QuestionId` int NOT NULL,
  `Response` int NOT NULL
);

ALTER TABLE `User` ADD FOREIGN KEY (`UserId`) REFERENCES `Survey` (`UserId`);

ALTER TABLE `Question` ADD FOREIGN KEY (`SurveyId`) REFERENCES `Survey` (`SurveyId`);

ALTER TABLE `Survey` ADD FOREIGN KEY (`SurveyId`) REFERENCES `UserSurvey` (`SurveyId`);

ALTER TABLE `User` ADD FOREIGN KEY (`UserId`) REFERENCES `UserSurvey` (`ResponderId`);

ALTER TABLE `User` ADD FOREIGN KEY (`UserId`) REFERENCES `UserSurvey` (`FocusId`);

ALTER TABLE `User` ADD FOREIGN KEY (`UserId`) REFERENCES `Answer` (`ResponderId`);

ALTER TABLE `Question` ADD FOREIGN KEY (`QuestionId`) REFERENCES `Answer` (`QuestionId`);

ALTER TABLE `UserSurvey` ADD FOREIGN KEY (`UserSurveyId`) REFERENCES `Answer` (`UserSurveyId`);
