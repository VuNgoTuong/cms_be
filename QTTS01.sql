USE [QTTS01]
GO
/****** Object:  User [QTTS_THACO]    Script Date: 9/30/2023 4:13:37 PM ******/
CREATE USER [QTTS_THACO] FOR LOGIN [QTTS_THACO] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [QTTS_THACO]
GO
/****** Object:  UserDefinedFunction [dbo].[ufn_convertDatetime]    Script Date: 9/30/2023 4:13:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[ufn_convertDatetime] (@datetime DATETIME)
RETURNS varchar(25)
AS

BEGIN
DECLARE @datetimeString VARCHAR(25) = ISNULL(CONVERT(VARCHAR(MAX), @datetime, 21), NULL)
DECLARE @text VARCHAR(25)

IF @datetimeString = NULL
	SET @text = ''
ELSE
	SET @text = SUBSTRING(@datetimeString, 9, 2) + '/' + SUBSTRING(@datetimeString, 6, 2) + '/' + SUBSTRING(@datetimeString, 1, 4)
				+ ' ' + SUBSTRING(@datetimeString, 12, 12)

RETURN @text
END

GO
/****** Object:  Table [dbo].[QTTS01_Bank]    Script Date: 9/30/2023 4:13:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QTTS01_Bank](
	[id] [uniqueidentifier] NOT NULL,
	[bank_code] [nvarchar](150) NOT NULL,
	[bank_name] [nvarchar](150) NOT NULL,
	[description] [nvarchar](150) NULL,
	[is_active] [bit] NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_by] [nvarchar](150) NOT NULL,
	[modify_time] [datetime] NOT NULL,
	[modify_by] [nvarchar](150) NOT NULL,
	[tenant_id] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QTTS01_ChangePasswordLog]    Script Date: 9/30/2023 4:13:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QTTS01_ChangePasswordLog](
	[id] [uniqueidentifier] NOT NULL,
	[username] [varchar](50) NOT NULL,
	[old_password] [varchar](250) NOT NULL,
	[new_password] [varchar](250) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_by] [varchar](50) NOT NULL,
	[modify_time] [datetime] NOT NULL,
	[modify_by] [varchar](50) NOT NULL,
	[tenant_id] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QTTS01_FileManager]    Script Date: 9/30/2023 4:13:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QTTS01_FileManager](
	[id] [uniqueidentifier] NOT NULL,
	[file_name] [nvarchar](150) NOT NULL,
	[size] [nvarchar](150) NOT NULL,
	[url_file] [nvarchar](150) NOT NULL,
	[object_id] [uniqueidentifier] NOT NULL,
	[object_file] [nvarchar](150) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_by] [nvarchar](150) NOT NULL,
	[modify_time] [datetime] NOT NULL,
	[modify_by] [nvarchar](150) NOT NULL,
	[tenant_id] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QTTS01_Group]    Script Date: 9/30/2023 4:13:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QTTS01_Group](
	[id] [uniqueidentifier] NOT NULL,
	[group_code] [nvarchar](150) NOT NULL,
	[group_name] [nvarchar](150) NOT NULL,
	[is_active] [bit] NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_by] [nvarchar](150) NOT NULL,
	[modify_time] [datetime] NOT NULL,
	[modify_by] [nvarchar](150) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QTTS01_MapProfileUser]    Script Date: 9/30/2023 4:13:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QTTS01_MapProfileUser](
	[id] [uniqueidentifier] NOT NULL,
	[username] [varchar](50) NOT NULL,
	[profile_id] [uniqueidentifier] NOT NULL,
	[description] [nvarchar](150) NULL,
	[is_active] [bit] NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_by] [varchar](50) NOT NULL,
	[modify_time] [datetime] NOT NULL,
	[modify_by] [varchar](50) NOT NULL,
	[tenant_id] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QTTS01_Module]    Script Date: 9/30/2023 4:13:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QTTS01_Module](
	[id] [uniqueidentifier] NOT NULL,
	[module_name] [nvarchar](150) NOT NULL,
	[description] [nvarchar](250) NULL,
	[create_time] [datetime] NOT NULL,
	[create_by] [varchar](50) NOT NULL,
	[modify_time] [datetime] NOT NULL,
	[modify_by] [varchar](50) NOT NULL,
	[tenant_id] [uniqueidentifier] NOT NULL,
	[is_active] [bit] NOT NULL,
	[display_name] [nvarchar](150) NULL,
	[position] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QTTS01_Permission]    Script Date: 9/30/2023 4:13:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QTTS01_Permission](
	[id] [uniqueidentifier] NOT NULL,
	[profile_id] [uniqueidentifier] NOT NULL,
	[permissionobject_id] [uniqueidentifier] NOT NULL,
	[is_allow_access] [bit] NOT NULL,
	[is_allow_create] [bit] NOT NULL,
	[is_allow_edit] [bit] NOT NULL,
	[is_allow_delete] [bit] NOT NULL,
	[object_name] [nvarchar](150) NOT NULL,
	[description] [nvarchar](150) NULL,
	[is_active] [bit] NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_by] [varchar](50) NOT NULL,
	[modify_time] [datetime] NOT NULL,
	[modify_by] [varchar](50) NOT NULL,
	[tenant_id] [uniqueidentifier] NOT NULL,
	[is_show] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QTTS01_PermissionObject]    Script Date: 9/30/2023 4:13:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QTTS01_PermissionObject](
	[id] [uniqueidentifier] NOT NULL,
	[object_name] [nvarchar](150) NOT NULL,
	[module_id] [uniqueidentifier] NOT NULL,
	[description] [nvarchar](250) NULL,
	[is_active] [bit] NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_by] [varchar](50) NOT NULL,
	[modify_time] [datetime] NOT NULL,
	[modify_by] [varchar](50) NOT NULL,
	[tenant_id] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QTTS01_Profile]    Script Date: 9/30/2023 4:13:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QTTS01_Profile](
	[id] [uniqueidentifier] NOT NULL,
	[profile_name] [nvarchar](150) NOT NULL,
	[description] [nvarchar](150) NULL,
	[is_active] [bit] NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_by] [varchar](50) NOT NULL,
	[modify_time] [datetime] NOT NULL,
	[modify_by] [varchar](50) NOT NULL,
	[tenant_id] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QTTS01_Tenant]    Script Date: 9/30/2023 4:13:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QTTS01_Tenant](
	[id] [uniqueidentifier] NOT NULL,
	[tenant_name] [nvarchar](250) NOT NULL,
	[address] [nvarchar](250) NOT NULL,
	[phone] [varchar](50) NOT NULL,
	[email] [nvarchar](250) NOT NULL,
	[group_id] [uniqueidentifier] NOT NULL,
	[is_active] [bit] NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_by] [varchar](50) NOT NULL,
	[modify_time] [datetime] NOT NULL,
	[modify_by] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QTTS01_User]    Script Date: 9/30/2023 4:13:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QTTS01_User](
	[username] [varchar](50) NOT NULL,
	[fullname] [nvarchar](150) NULL,
	[phone] [varchar](50) NULL,
	[password] [varchar](250) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[avatar] [varchar](max) NULL,
	[description] [nvarchar](max) NULL,
	[is_rootuser] [bit] NOT NULL,
	[is_active] [bit] NOT NULL,
	[create_time] [datetime] NOT NULL,
	[create_by] [varchar](50) NOT NULL,
	[modify_time] [datetime] NOT NULL,
	[modify_by] [varchar](50) NOT NULL,
	[tenant_id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK__QTTS01_U__F3DBC573E4D21C50] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[QTTS01_Bank] ([id], [bank_code], [bank_name], [description], [is_active], [create_time], [create_by], [modify_time], [modify_by], [tenant_id]) VALUES (N'aa0d615f-2d43-42b4-bacb-578607858421', N'argi_001', N'ArgiBank', N'ngan hang argibank', 1, CAST(N'2023-09-18T00:00:00.000' AS DateTime), N'root', CAST(N'2023-09-18T00:00:00.000' AS DateTime), N'root', N'aa0d615f-2d43-42b4-bacb-578607858421')
INSERT [dbo].[QTTS01_Bank] ([id], [bank_code], [bank_name], [description], [is_active], [create_time], [create_by], [modify_time], [modify_by], [tenant_id]) VALUES (N'04fccc82-3469-44f5-b9aa-5edf3071b2cb', N'Vietcombank_001', N'VIETCOMBANK', N'ngan hang vietcombank', 1, CAST(N'2023-09-28T10:33:04.587' AS DateTime), N'root@thaco.com.vn', CAST(N'2023-09-28T10:33:04.587' AS DateTime), N'', N'aa0d615f-2d43-42b4-bacb-578607858421')
INSERT [dbo].[QTTS01_Bank] ([id], [bank_code], [bank_name], [description], [is_active], [create_time], [create_by], [modify_time], [modify_by], [tenant_id]) VALUES (N'd47c65e8-cde5-46c6-bc6b-b27b4da4f9f1', N'Techcombank_001', N'TECHCOMBANK', N'ngan hang Techcombank', 1, CAST(N'2023-09-29T16:00:36.447' AS DateTime), N'root@thaco.com.vn', CAST(N'2023-09-29T16:00:36.447' AS DateTime), N'', N'aa0d615f-2d43-42b4-bacb-578607858421')
GO
INSERT [dbo].[QTTS01_Group] ([id], [group_code], [group_name], [is_active], [create_time], [create_by], [modify_time], [modify_by]) VALUES (N'3ec50cdf-ba71-4dc0-9e72-4434dc75beae', N'VPTQ', N'THACO_GROUP', 1, CAST(N'2023-09-28T11:13:49.970' AS DateTime), N'root@thaco.com.vn', CAST(N'2023-09-28T11:13:49.970' AS DateTime), N'')
INSERT [dbo].[QTTS01_Group] ([id], [group_code], [group_name], [is_active], [create_time], [create_by], [modify_time], [modify_by]) VALUES (N'2b8a9c8b-1fed-4f8f-b7fd-d43018a886f1', N'THISO', N'THACO_THISO', 1, CAST(N'2023-09-28T11:33:12.350' AS DateTime), N'root@thaco.com.vn', CAST(N'2023-09-28T11:39:09.123' AS DateTime), N'root@thaco.com.vn')
INSERT [dbo].[QTTS01_Group] ([id], [group_code], [group_name], [is_active], [create_time], [create_by], [modify_time], [modify_by]) VALUES (N'4a41530a-a7d7-47e7-b038-eee46ee14657', N'THADICO', N'THACO_THADICO', 1, CAST(N'2023-09-29T16:57:20.390' AS DateTime), N'root@thaco.com.vn', CAST(N'2023-09-29T16:57:20.390' AS DateTime), N'')
GO
INSERT [dbo].[QTTS01_MapProfileUser] ([id], [username], [profile_id], [description], [is_active], [create_time], [create_by], [modify_time], [modify_by], [tenant_id]) VALUES (N'141b519a-7317-4513-be6b-bd049a21d706', N'WWWW@thaco.com.vn', N'4c4dc1d9-57f7-4df2-8204-120de6aad034', N'System', 1, CAST(N'2023-09-28T15:15:21.293' AS DateTime), N'root@thaco.com.vn', CAST(N'2023-09-28T15:15:21.293' AS DateTime), N'', N'aa0d615f-2d43-42b4-bacb-578607858421')
GO
INSERT [dbo].[QTTS01_Module] ([id], [module_name], [description], [create_time], [create_by], [modify_time], [modify_by], [tenant_id], [is_active], [display_name], [position]) VALUES (N'13c9d1bb-1cb6-4c7d-b2d2-87361d47d788', N'General Setting', N'General Setting', CAST(N'2023-06-20T09:36:04.800' AS DateTime), N'root@thaco.com.vn', CAST(N'2023-06-20T09:36:04.800' AS DateTime), N'root@thaco.com.vn', N'aa0d615f-2d43-42b4-bacb-578607858421', 1, N'General Setting', 7)
GO
INSERT [dbo].[QTTS01_Permission] ([id], [profile_id], [permissionobject_id], [is_allow_access], [is_allow_create], [is_allow_edit], [is_allow_delete], [object_name], [description], [is_active], [create_time], [create_by], [modify_time], [modify_by], [tenant_id], [is_show]) VALUES (N'bb1cc8d4-8d71-4f00-9ab1-8330821d426e', N'4c4dc1d9-57f7-4df2-8204-120de6aad034', N'd52102bc-e3f3-4f8b-bbb8-a9ed92925416', 1, 1, 1, 1, N'User Management', NULL, 1, CAST(N'2023-06-20T09:36:04.800' AS DateTime), N'root@thaco.com.vn', CAST(N'2023-06-20T09:36:04.800' AS DateTime), N'root@thaco.com.vn', N'aa0d615f-2d43-42b4-bacb-578607858421', 1)
GO
INSERT [dbo].[QTTS01_PermissionObject] ([id], [object_name], [module_id], [description], [is_active], [create_time], [create_by], [modify_time], [modify_by], [tenant_id]) VALUES (N'd52102bc-e3f3-4f8b-bbb8-a9ed92925416', N'User Management', N'13c9d1bb-1cb6-4c7d-b2d2-87361d47d788', N'User Management', 1, CAST(N'2023-06-20T09:36:04.800' AS DateTime), N'root@thaco.com.vn', CAST(N'2023-06-20T09:36:04.800' AS DateTime), N'root@thaco.com.vn', N'aa0d615f-2d43-42b4-bacb-578607858421')
GO
INSERT [dbo].[QTTS01_Profile] ([id], [profile_name], [description], [is_active], [create_time], [create_by], [modify_time], [modify_by], [tenant_id]) VALUES (N'4c4dc1d9-57f7-4df2-8204-120de6aad034', N'Admin', NULL, 1, CAST(N'2023-06-20T09:36:04.800' AS DateTime), N'root@thaco.com.vn', CAST(N'2023-06-20T09:36:04.800' AS DateTime), N'root@thaco.com.vn', N'aa0d615f-2d43-42b4-bacb-578607858421')
GO
INSERT [dbo].[QTTS01_Tenant] ([id], [tenant_name], [address], [phone], [email], [group_id], [is_active], [create_time], [create_by], [modify_time], [modify_by]) VALUES (N'aa0d615f-2d43-42b4-bacb-578607858421', N'Cong ty AuTO', N'', N'', N'', N'768384c1-bc50-4dbc-82b8-9eac8052bfe5', 1, CAST(N'2023-09-18T00:00:00.000' AS DateTime), N'root', CAST(N'2023-09-18T00:00:00.000' AS DateTime), N'root')
INSERT [dbo].[QTTS01_Tenant] ([id], [tenant_name], [address], [phone], [email], [group_id], [is_active], [create_time], [create_by], [modify_time], [modify_by]) VALUES (N'6cbaa376-cc20-42e9-baed-7712359a8259', N'IT DEVELOPMENT', N'HCM', N'0123456789', N'nguyenvancanh@thaco.com.vn', N'00000000-0000-0000-0000-000000000000', 1, CAST(N'2023-09-30T10:50:03.740' AS DateTime), N'root@thaco.com.vn', CAST(N'2023-09-30T10:50:03.740' AS DateTime), N'')
GO
INSERT [dbo].[QTTS01_User] ([username], [fullname], [phone], [password], [email], [avatar], [description], [is_rootuser], [is_active], [create_time], [create_by], [modify_time], [modify_by], [tenant_id]) VALUES (N'nguyenxuancanh@thaco.com.vn', N'Nguyen Canh', N'11111', N'3627909a29c31381a071ec27f7c9ca97726182aed29a7ddd2e54353322cfb30abb9e3a6df2ac2c20fe23436311d678564d0c8d305930575f60e2d3d048184d79', N'nguyenxuancanh@thaco.com.vn', NULL, NULL, 0, 0, CAST(N'2023-09-18T00:00:00.000' AS DateTime), N'root', CAST(N'2023-09-27T15:08:52.337' AS DateTime), N'root@thaco.com.vn', N'aa0d615f-2d43-42b4-bacb-578607858421')
INSERT [dbo].[QTTS01_User] ([username], [fullname], [phone], [password], [email], [avatar], [description], [is_rootuser], [is_active], [create_time], [create_by], [modify_time], [modify_by], [tenant_id]) VALUES (N'root@thaco.com.vn', N'root', N'', N'3627909a29c31381a071ec27f7c9ca97726182aed29a7ddd2e54353322cfb30abb9e3a6df2ac2c20fe23436311d678564d0c8d305930575f60e2d3d048184d79', N'root@thaco.com.vn', NULL, N'', 1, 1, CAST(N'2023-09-18T00:00:00.000' AS DateTime), N'root', CAST(N'2023-09-18T00:00:00.000' AS DateTime), N'root', N'aa0d615f-2d43-42b4-bacb-578607858421')
INSERT [dbo].[QTTS01_User] ([username], [fullname], [phone], [password], [email], [avatar], [description], [is_rootuser], [is_active], [create_time], [create_by], [modify_time], [modify_by], [tenant_id]) VALUES (N'WWWW@thaco.com.vn', N'NGUYEN CCCCC', N'1122343', N'3735799bc4c693c6d25d34862015522d2dc247b8fcd9bd7d91bcc666bb13ead5a2f815ab880bbb58d1f537bee552cca8499e03d815e18c96277a6b8d1731d91f', N'WWWW@thaco.com.vn', N'/Resources/tenant_aa0d615f-2d43-42b4-bacb-578607858421/avatar/0fe35752-e98c-47d5-9bb4-f2e5258162fb.jpg', N'WWWW', 0, 0, CAST(N'2023-09-28T15:15:21.293' AS DateTime), N'root@thaco.com.vn', CAST(N'2023-09-28T15:15:21.293' AS DateTime), N'', N'aa0d615f-2d43-42b4-bacb-578607858421')
GO
ALTER TABLE [dbo].[QTTS01_Tenant] ADD  CONSTRAINT [DF_QTTS01_Tenants_tenant_name]  DEFAULT ('') FOR [tenant_name]
GO
ALTER TABLE [dbo].[QTTS01_Tenant] ADD  CONSTRAINT [DF_QTTS01_Tenants_address]  DEFAULT ('') FOR [address]
GO
ALTER TABLE [dbo].[QTTS01_Tenant] ADD  CONSTRAINT [DF_QTTS01_Tenants_phone]  DEFAULT ('') FOR [phone]
GO
ALTER TABLE [dbo].[QTTS01_Tenant] ADD  CONSTRAINT [DF_QTTS01_Tenants_email]  DEFAULT ('') FOR [email]
GO
ALTER TABLE [dbo].[QTTS01_Tenant] ADD  CONSTRAINT [DF_QTTS01_Tenants_create_by]  DEFAULT ('') FOR [create_by]
GO
ALTER TABLE [dbo].[QTTS01_Tenant] ADD  CONSTRAINT [DF_QTTS01_Tenants_modify_by]  DEFAULT ('') FOR [modify_by]
GO
ALTER TABLE [dbo].[QTTS01_User] ADD  CONSTRAINT [DF_QTTS01_User_is_rootuser]  DEFAULT ((0)) FOR [is_rootuser]
GO
ALTER TABLE [dbo].[QTTS01_User] ADD  CONSTRAINT [DF_QTTS01_User_is_active]  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[QTTS01_User]  WITH CHECK ADD  CONSTRAINT [FK_QTTS01_User_QTTS01_User] FOREIGN KEY([username])
REFERENCES [dbo].[QTTS01_User] ([username])
GO
ALTER TABLE [dbo].[QTTS01_User] CHECK CONSTRAINT [FK_QTTS01_User_QTTS01_User]
GO
/****** Object:  StoredProcedure [dbo].[usp_Auth_Check_Permision_By_User]    Script Date: 9/30/2023 4:13:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		canhnx
-- Create date: 19-09-2023
-- Description:	Store check permission by user
-- =============================================
CREATE PROCEDURE [dbo].[usp_Auth_Check_Permision_By_User]
	@username VARCHAR(50),
	@tenantId UNIQUEIDENTIFIER,
	@permissionName VARCHAR(50),
	@permissionType VARCHAR(50),
	@rootPermission VARCHAR(MAX)
AS
BEGIN
	-- Lấy thông tin của user
	SELECT TOP 1 is_rootuser 
	INTO #TempPermisionByUser 
	FROM QTTS01_User 
	WHERE is_active = 1 
	AND username = @username
	AND tenant_id = @tenantId;

	-- Check exists user
	IF(SELECT COUNT(*) FROM #TempPermisionByUser) = 0
	BEGIN
		SELECT 0;
		RETURN;
	END;

	-- Case Root User có quyền Administrator sẽ có full quyền trong hệ thống
	IF(SELECT is_rootuser FROM #TempPermisionByUser) = 1 
	BEGIN
		SELECT 1;
		RETURN;
	END

	-- Case user thường có quyền Administrator
	--IF(SELECT is_administrator FROM #TempPermisionByUser) = 1
	--BEGIN
	--	---- Administrator của User thường có thế thao tác tất cả trừ các Permission Object của Root User
	--	IF (SELECT COUNT(*) WHERE @permissionName IN (SELECT * FROM STRING_SPLIT(@rootPermission,','))) > 0
	--	BEGIN
	--		SELECT 0;
	--		RETURN;
	--	END
	--	ELSE
	--	BEGIN
	--		SELECT 1
	--		RETURN;
	--	END
	--END
	
	SELECT CASE @permissionType
		WHEN 'access' THEN (SELECT COUNT(DISTINCT p.is_allow_access) FROM QTTS01_Permission as p
							JOIN QTTS01_MapProfileUser as m ON m.profile_id = p.profile_id 
							JOIN QTTS01_Profile as pf ON m.profile_id = pf.id
							WHERE m.username = @username
							AND pf.is_active = 1 
							AND p.tenant_id = @tenantId		
							AND p.is_allow_access = 1 
							AND p.object_name = @permissionName)
		WHEN 'create' THEN  (SELECT COUNT(DISTINCT p.is_allow_create) FROM QTTS01_Permission as p
							JOIN QTTS01_MapProfileUser as m ON m.profile_id = p.profile_id 
							JOIN QTTS01_Profile as pf ON m.profile_id = pf.id
							WHERE m.username = @username
							AND pf.is_active = 1 
							AND p.tenant_id = @tenantId					
							AND p.is_allow_create = 1 
							AND p.object_name = @permissionName)
		WHEN 'edit' THEN (SELECT COUNT(DISTINCT p.is_allow_edit) FROM QTTS01_Permission as p
							JOIN QTTS01_MapProfileUser as m ON m.profile_id = p.profile_id 
							JOIN QTTS01_Profile as pf ON m.profile_id = pf.id
							WHERE m.username = @username
							AND pf.is_active = 1 
							AND p.tenant_id = @tenantId							
							AND p.is_allow_edit = 1 
							AND p.object_name = @permissionName)
		WHEN 'delete' THEN (SELECT COUNT(DISTINCT p.is_allow_delete) FROM QTTS01_Permission as p
							JOIN QTTS01_MapProfileUser as m ON m.profile_id = p.profile_id 
							JOIN QTTS01_Profile as pf ON m.profile_id = pf.id
							WHERE m.username = @username
							AND pf.is_active = 1 
							AND p.tenant_id = @tenantId
							AND p.is_allow_delete = 1 
							AND p.object_name = @permissionName)
		ELSE 0
	END
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Module_Get_All]    Script Date: 9/30/2023 4:13:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		canhnx
-- Create date: 19-09-2023
-- Description:	Store get all module
-- =============================================
CREATE PROCEDURE [dbo].[usp_Module_Get_All]
	@username varchar(50),
	@is_admin bit,
	@tenant_id uniqueidentifier
AS
BEGIN

	CREATE TABLE #TempModuleResponse
	(
		id uniqueidentifier,
		module_name nvarchar(250),
		display_name nvarchar(250),
		position int,
		description nvarchar(250),
		is_active bit,
		is_show bit,
		create_time datetime,
		create_by varchar(50),
		modify_time datetime,
		modify_by varchar(50),
		tenant_id uniqueidentifier		
	)

	IF(@is_admin = 1)
	BEGIN
		INSERT INTO #TempModuleResponse
		SELECT 
		id,
		module_name,
		display_name,
		position,
		description,
		1 as is_avtive,
		1 as is_show,
		create_time,
		create_by,
		modify_time,
		modify_by,
		tenant_id
		FROM QTTS01_Module WHERE tenant_id = @tenant_id;

		SELECT * FROM #TempModuleResponse ORDER BY position;
		RETURN;
	END;

	SELECT profile_id 
	INTO #TempProfileByUser
	FROM QTTS01_MapProfileUser 
	WHERE username = @username 
	AND tenant_id = @tenant_id;

	SELECT p.permissionobject_id
	INTO #TempPermissionObject
	FROM QTTS01_Permission as p
	JOIN #TempProfileByUser as pf ON p.profile_id = pf.profile_id
	WHERE tenant_id = @tenant_id 
	AND is_allow_access = 1 
	AND is_show = 1;

	SELECT DISTINCT module_id
	INTO #TempModuleActive
	FROM QTTS01_PermissionObject as po
	JOIN #TempPermissionObject as tpo ON po.id = tpo.permissionobject_id
	WHERE tenant_id = @tenant_id;

	INSERT INTO #TempModuleResponse
	SELECT
	m.id,
	m.module_name,
	m.display_name,
	m.position,
	m.description,
	m.is_active,
	CASE 
		WHEN EXISTS (SELECT 1 FROM 
		#TempModuleActive as tm WHERE tm.module_id = m.id) THEN 1 ELSE 0
	END as is_show,
	m.create_time,
	m.create_by,
	m.modify_time,
	m.modify_by,
	m.tenant_id
	FROM QTTS01_Module AS m
	WHERE tenant_id = @tenant_id;

	SELECT * FROM #TempModuleResponse ORDER BY position;
END

GO
/****** Object:  StoredProcedure [dbo].[usp_PermissionObject_Get_All]    Script Date: 9/30/2023 4:13:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		canhnx
-- Create date: 19-09-2023
-- Description:	Store get all permission object
-- =============================================
CREATE PROCEDURE [dbo].[usp_PermissionObject_Get_All]
	@objectName NVARCHAR(150),
	@moduleName NVARCHAR(150),
	@description NVARCHAR(250),
	@createBy VARCHAR(50),
	@createTime NVARCHAR(250),
	@page INT,
	@limit INT,
	@tenantId UNIQUEIDENTIFIER,
	@totalRow INT OUTPUT
AS
BEGIN
	
	SELECT 
	p.id,
	p.object_name,
	p.module_id,
	m.module_name,
	p.description,
	p.is_active,
	p.create_time,
	p.create_by,
	p.modify_by,
	p.modify_time,
	p.tenant_id
	INTO #TempResponse
	FROM [dbo].[QTTS01_PermissionObject] AS p WITH (NOLOCK) 
	INNER JOIN QTTS01_Module AS m ON  p.module_id = m.id
	WHERE p.tenant_id = @tenantId
	AND (ISNULL(@objectName,'') = '' OR p.object_name LIKE N'%'+@objectName+'%')
	AND (ISNULL(@moduleName,'') = '' OR m.module_name LIKE N'%'+@moduleName+'%')
	AND (ISNULL(@description,'') = '' OR p.description LIKE N'%'+@description+'%')
	AND (ISNULL(@createBy,'') = '' OR p.create_by LIKE N'%'+@createBy+'%')
	AND (ISNULL(@createTime,'') = '' OR dbo.ufn_convertDatetime(p.create_time) LIKE N'%'+@createTime+'%')

	SET @totalRow = (SELECT COUNT(*) FROM #TempResponse)

	IF(@page = 0 AND @limit = 0)
	BEGIN
		SELECT *
		FROM #TempResponse
		ORDER BY modify_time DESC
	END
	ELSE
	BEGIN
		SELECT *
		FROM #TempResponse
		ORDER BY modify_time DESC
		OFFSET((@page - 1) * @limit) ROWS
		FETCH NEXT @limit ROWS ONLY
	END
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Profile_Get_Permission_By_Profile]    Script Date: 9/30/2023 4:13:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		canhnxx
-- Create date: 19-09-2023
-- Description:	Store get permission by profile
-- =============================================
CREATE PROCEDURE [dbo].[usp_Profile_Get_Permission_By_Profile]
	@profileId UNIQUEIDENTIFIER,
	@objectName NVARCHAR(150),
	@tenantId UNIQUEIDENTIFIER,
	@page INT,
	@limit INT,
	@totalRow INT OUTPUT
AS
BEGIN
	
	print(@profileId)
	print(@tenantId)

	SELECT 
	*
	INTO #TempPermissionResponse
	FROM QTTS01_Permission
	WHERE tenant_id = @tenantId AND profile_id = @profileId
	AND (ISNULL(@objectName,'') = '' OR object_name LIKE N'%'+@objectName+'%')

	SET @totalRow = (SELECT COUNT(*) FROM #TempPermissionResponse);

	IF(@page = 0 AND @limit = 0)
	BEGIN
		SELECT *
		FROM #TempPermissionResponse
		ORDER BY object_name

		RETURN;
	END
	ELSE
	BEGIN
		SELECT *
		FROM #TempPermissionResponse
		ORDER BY object_name
		OFFSET((@page - 1) * @limit) ROWS
		FETCH NEXT @limit ROWS ONLY

		RETURN;
	END
	
END

GO
/****** Object:  StoredProcedure [dbo].[usp_User_Get_User_Information]    Script Date: 9/30/2023 4:13:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		canhnx
-- Create date: 19-09-2023
-- Description:	Get user information
-- =============================================
CREATE PROCEDURE [dbo].[usp_User_Get_User_Information] 
	@username VARCHAR(50),
	@tenant_id UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE 
		@iS_ROOT INT = 0
	-- Lấy thông tin của user
	SET @iS_ROOT = (SELECT TOP 1 QTTS01_User.is_rootuser
	FROM QTTS01_User 
	WHERE is_active = 1 
	AND username = @username)
	
	IF @iS_ROOT = 1
	BEGIN
		SELECT
		usr.username,
		usr.fullname,
		usr.phone,
		usr.email,
		usr.description,
		usr.is_active,
		usr.avatar,
		usr.create_time,
		usr.create_by,
		usr.modify_time,
		usr.modify_by,
		usr.tenant_id
		FROM QTTS01_User as usr
		--WHERE is_active = 1
		--AND usr.username IN ((CASE WHEN  @iS_ROOT = 1 THEN
														--	('nguyenxuancanh@thaco.com.vn','root@thaco.com.vn')-- 
														--ELSE
														--	(SELECT @username)
														--END))	
	END
	ELSE
	BEGIN
		SELECT
		usr.username,
		usr.fullname,
		usr.phone,
		usr.email,
		usr.description,
		usr.is_active,
		usr.avatar,
		usr.create_time,
		usr.create_by,
		usr.modify_time,
		usr.modify_by,
		usr.tenant_id
		FROM QTTS01_User as usr
		WHERE usr.is_active = 1 
		AND usr.username IN ((CASE WHEN  @iS_ROOT = 1 THEN
														(SELECT  username FROM QTTS01_User) 
													ELSE
														@username
													END))	
		AND  usr.tenant_id IN ((CASE WHEN  @iS_ROOT = 1 THEN
														(SELECT  tenant_id FROM QTTS01_Tenants) 
													ELSE
														@tenant_id
													END))	
	END
END


GO
