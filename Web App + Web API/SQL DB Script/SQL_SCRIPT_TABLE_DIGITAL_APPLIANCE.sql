/****** Object:  Table [dbo].[digital_appliance]    Script Date: 7/01/2019 9:39:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[digital_appliance](
	[appliance_key] [bigint] IDENTITY(1,1) NOT NULL,
	[appliance_name] [nvarchar](100) NOT NULL,
	[appliance_desc] [nvarchar](500) NOT NULL,
	[user_email] [nvarchar](200) NOT NULL,
	[status_on_off] [char](1) NOT NULL,
	[last_status_change] [datetime] NULL,
	[active_yn] [char](1) NOT NULL,
 CONSTRAINT [PK_digital_appliance_master] PRIMARY KEY CLUSTERED 
(
	[appliance_key] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

