﻿CREATE TABLE [dbo].[VOTE_CANDIDATE]
(
	[CANDIDATE_ID] INT NOT NULL Identity(1,1) PRIMARY KEY,
	    --CONTROL FIELD
	[CREATOR] NVARCHAR(255) NOT NULL,
	[CREATION_DATE] DATETIME NOT NULL Default(GetDate()),
	[MODIFIER] NVARCHAR(255) NULL,
	[MODIFICATION_DATE] DATETIME NULL,
	[API_NAME] NVARCHAR(255) NULL,


	[VOTE_ID] INT NOT NULL ,
	[CAND_ITEM] NVARCHAR(MAX) NOT NULL,
	[IS_ADD_TEXT] NVARCHAR(1) NULL
)
GO



CREATE INDEX [IX_VOTE_CANDIDATE_01] ON [dbo].[VOTE_CANDIDATE] (VOTE_ID, CANDIDATE_ID)

