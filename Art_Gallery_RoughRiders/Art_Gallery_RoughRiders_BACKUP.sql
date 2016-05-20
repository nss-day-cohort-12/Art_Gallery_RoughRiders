CREATE TABLE [dbo].[Agent] (
    [IdAgent]          INT       IDENTITY (1,1)   NOT NULL,
    [AgentFirstName]   VARCHAR (50) NOT NULL,
    [AgentLastName]    VARCHAR (50) NOT NULL,
    [AgentLocation]    VARCHAR (50) NOT NULL,
    [AgentAddress]     VARCHAR (50) NOT NULL,
    [AgentPhoneNumber] INT          NOT NULL,
    [Active]           BIT          NOT NULL,
    PRIMARY KEY CLUSTERED ([IdAgent] ASC)
);

CREATE TABLE [dbo].[Artist]
(
    [IdArtist] INT NOT NULL IDENTITY (1,1) PRIMARY KEY, 
    [ArtistName] VARCHAR(50) NOT NULL, 
    [ArtistBirthYear] INT NOT NULL, 
    [ArtistDeathYear] INT NOT NULL
)
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
    CONSTRAINT [FK_Invoice_Agent] FOREIGN KEY ([IdAgent]) REFERENCES [dbo].[Agent] ([IdAgent]),
    CONSTRAINT [FK_Invoice_Customer] FOREIGN KEY ([IdCustomer]) REFERENCES [dbo].[Customer] ([IdCustomer])
);