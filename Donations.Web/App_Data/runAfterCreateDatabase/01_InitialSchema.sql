USE [Donations_JBacik]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Donation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Country] [varchar](100) NOT NULL,
	[Address1] [varchar](100) NOT NULL,
	[Address2] [varchar](100) NULL,
	[City] [varchar](50) NOT NULL,
	[StateProvince] [varchar](50) NOT NULL,
	[PostalCode] [varchar](10) NOT NULL,
	[PaymentId] [varchar](50) NOT NULL,
	[ConfirmationNumber] varchar(50) NOT NULL,
	[DonationAmount] [decimal](16, 2) NOT NULL,
	[HasCoveredFees] [bit] NOT NULL,
	[CoveredFeeAmount] [decimal](16, 2) NOT NULL,
	[TotalDonation] [decimal](16, 2) NOT NULL,
	[DonatedOnUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_Donation] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Donation] ADD  CONSTRAINT [DF_Donation_HasCoveredFees]  DEFAULT ((0)) FOR [HasCoveredFees]
GO

ALTER TABLE [dbo].[Donation] ADD  CONSTRAINT [DF_Donation_CoveredFeeAmount]  DEFAULT ((0.00)) FOR [CoveredFeeAmount]
GO

ALTER TABLE [dbo].[Donation] ADD  CONSTRAINT [DF_Donation_DonatedOnUtc]  DEFAULT (getutcdate()) FOR [DonatedOnUtc]
GO

