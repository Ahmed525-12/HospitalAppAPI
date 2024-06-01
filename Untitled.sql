CREATE TABLE [employee] (
  [id] string PRIMARY KEY,
  [display_name] string,
  [acess_data] string,
  [password] string,
  [user_name] string,
  [email] string,
  [phone] int,
  [department] string,
  [rating] int,
  [repoets] string,
  [history] string,
  [mediscines] string,
  [salary] int,
  [notes] string,
  [complaints] string,
  [shifts] datetime,
  [created_at] timestamp
)
GO

CREATE TABLE [Guest] (
  [id] string PRIMARY KEY,
  [username] nvarchar(255),
  [IdenityCardNumber] int,
  [display_name] string,
  [password] string,
  [user_name] string,
  [email] string,
  [phone] int,
  [rating] int,
  [repoets] string,
  [history] string,
  [mediscines] string,
  [notes] string,
  [complaints] string,
  [sessionMedical] string,
  [sessionMedicine] string,
  [created_at] timestamp
)
GO

CREATE TABLE [Roles] (
  [id] integer PRIMARY KEY,
  [RoleName] string,
  [created_at] timestamp
)
GO

CREATE TABLE [department] (
  [id] integer PRIMARY KEY,
  [DepartmentName] string,
  [created_at] timestamp
)
GO

CREATE TABLE [Reports] (
  [id] integer PRIMARY KEY,
  [title] string,
  [employeeId] string,
  [guestId] string,
  [descreption] string,
  [medicines] string,
  [created_at] timestamp
)
GO

CREATE TABLE [pharmacy] (
  [id] integer PRIMARY KEY,
  [employeeId] string,
  [guestId] string,
  [orders] string,
  [medicines] string,
  [created_at] timestamp
)
GO

CREATE TABLE [medicines] (
  [id] integer PRIMARY KEY,
  [Name] string,
  [Category] string,
  [price] string,
  [expDate] datetime,
  [created_at] timestamp
)
GO

CREATE TABLE [Notes] (
  [id] integer PRIMARY KEY,
  [Name] string,
  [employeeId] string,
  [guestId] string,
  [description] string,
  [created_at] timestamp
)
GO

CREATE TABLE [Session] (
  [id] integer PRIMARY KEY,
  [Title] string,
  [employeeId] string,
  [guestId] string,
  [price] int,
  [departmentId] int,
  [ispay] bool,
  [created_at] timestamp
)
GO

CREATE TABLE [History] (
  [id] integer PRIMARY KEY,
  [Title] string,
  [employeeId] string,
  [guestId] string,
  [SessionId] int,
  [departmentId] int,
  [ispay] bool,
  [created_at] timestamp
)
GO
