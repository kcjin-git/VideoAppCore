﻿CREATE TABLE [dbo].[VOTE_BALLOT]
(
    [BALLOT_ID] INT NOT NULL Identity(1,1)  PRIMARY KEY, 
	
	--CONTROL FIELD
	[CREATOR] NVARCHAR(255) NOT NULL,
	[CREATION_DATE] DATETIME NOT NULL Default(GetDate()),
	[MODIFIER] NVARCHAR(255) NULL,
	[MODIFICATION_DATE] DATETIME NULL,
	[API_NAME] NVARCHAR(255) NULL,

	[VOTE_ID] INT NOT NULL,
	[CANDIDATE_TYPE] NVARCHAR(10) NULL, -- ALL, ORGN, PERSON
	[ORGN_NAME] NVARCHAR(255) NULL,
	[EMAIL] NVARCHAR(255) NOT NULL,
	[CANDIDATE_ID] INT NOT NULL,

	[CAND_ADD_TEXT] NVARCHAR(MAX),
)
GO