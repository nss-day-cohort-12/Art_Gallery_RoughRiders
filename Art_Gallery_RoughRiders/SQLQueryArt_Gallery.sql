CREATE TABLE [dbo].[Agent] (
    [IdAgent]          INT          IDENTITY (1, 1) NOT NULL,
    [AgentFirstName]   VARCHAR (50) NOT NULL,
    [AgentLastName]    VARCHAR (50) NOT NULL,
    [AgentLocation]    VARCHAR (50) NOT NULL,
    [AgentAddress]     VARCHAR (50) NOT NULL,
    [AgentPhoneNumber] INT          NOT NULL,
    [Active]           BIT          NOT NULL,
    PRIMARY KEY CLUSTERED ([IdAgent] ASC)
);



CREATE TABLE [dbo].[ArtPiece] (
    [IdArtPiece]          INT           IDENTITY (1, 1) NOT NULL,
    [IdArtWork]           INT           NOT NULL,
    [ArtPieceImage]       VARCHAR (MAX) NOT NULL,
    [ArtPieceDateCreated] DATETIME      NOT NULL,
    [ArtPiecePrice]       MONEY         NOT NULL,
    [ArtPieceSold]        BIT           NOT NULL,
    [ArtPieceLocation]    VARCHAR (MAX) NOT NULL,
    [ArtPieceEditionNum]  INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([IdArtPiece] ASC),
    CONSTRAINT [FK_ArtPiece_ArtWork] FOREIGN KEY ([IdArtWork]) REFERENCES [dbo].[ArtWork] ([IdArtWork])
);

CREATE TABLE [dbo].[ArtShow] (
    [IdArtShow]       INT           IDENTITY (1, 1) NOT NULL,
    [IdArtWork]       INT           NOT NULL,
    [ArtShowLocation] VARCHAR (MAX) NOT NULL,
    [ArtShowAgents]   VARCHAR (MAX) NOT NULL,
    [ArtShowOverhead] VARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdArtShow] ASC),
    CONSTRAINT [FK_ArtShow_ArtWork] FOREIGN KEY ([IdArtWork]) REFERENCES [dbo].[ArtWork] ([IdArtWork])
);

CREATE TABLE [dbo].[ArtWork] (
    [IdArtWork]           INT           IDENTITY (1, 1) NOT NULL,
    [IdArtist]            INT           NOT NULL,
    [ArtWorkTitle]        VARCHAR (MAX) NOT NULL,
    [ArtWorkYear]         INT           NOT NULL,
    [ArtWorkMedium]       VARCHAR (MAX) NOT NULL,
    [ArtWorkDimensions]   VARCHAR (MAX) NOT NULL,
    [ArtWorkNumMade]      INT           NOT NULL,
    [ArtWorkNumInventory] INT           NOT NULL,
    [ArtWorkNumSold]      INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([IdArtWork] ASC),
    CONSTRAINT [FK_ArtWork_Artist] FOREIGN KEY ([IdArtist]) REFERENCES [dbo].[Artist] ([IdArtist])
);

CREATE TABLE [dbo].[Customer] (
    [IdCustomer]          INT          IDENTITY (1, 1) NOT NULL,
    [IdAgent]             INT          NOT NULL,
    [CustomerFirstName]   VARCHAR (50) NOT NULL,
    [CustomerLastName]    VARCHAR (50) NOT NULL,
    [CustomerAddress]     VARCHAR (50) NOT NULL,
    [CustomerPhoneNumber] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdCustomer] ASC)
);


CREATE TABLE [dbo].[Invoice] (
    [IdInvoice]       INT          IDENTITY (1, 1) NOT NULL,
    [IdCustomer]      INT          NOT NULL,
    [IdAgent]         INT          NOT NULL,
    [PaymentMethod]   VARCHAR (50) NOT NULL,
    [ShippingAddress] VARCHAR (50) NOT NULL,
    [PieceSold]       VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdInvoice] ASC),
    CONSTRAINT [FK_Invoice_Customer] FOREIGN KEY ([IdCustomer]) REFERENCES [dbo].[Customer] ([IdCustomer]),
    CONSTRAINT [FK_Invoice_Agent] FOREIGN KEY ([IdAgent]) REFERENCES [dbo].[Agent] ([IdAgent])
);

