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
  [repoerts] string,
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
  [repoerts] string,
  [history] string,
  [mediscines] string,
  [notes] string,
  [complaints] string,
  [sessionMedical] int,
  [sessionMedicine] int,
  [created_at] timestamp
)
GO

CREATE TABLE [Roles] (
  [id] integer PRIMARY KEY,
  [RoleName] string,
  [created_at] timestamp
)
GO

CREATE TABLE [Category] (
  [id] integer PRIMARY KEY,
  [CategoryName] string,
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
  [medicines] int,
  [created_at] timestamp
)
GO

CREATE TABLE [medicines] (
  [id] integer PRIMARY KEY,
  [Name] string,
  [Category] int,
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

CREATE TABLE [complaints] (
  [id] integer PRIMARY KEY,
  [Name] string,
  [employeeId] string,
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
  [promoCode] string,
  [datePick] datetime,
  [created_at] timestamp
)
GO

CREATE TABLE [orders] (
  [id] integer PRIMARY KEY,
  [guestId] string,
  [Totalprice] int,
  [promoCode] string,
  [datePick] datetime,
  [medicines] int,
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

ALTER TABLE [Roles] ADD FOREIGN KEY ([id]) REFERENCES [employee] ([acess_data])
GO

ALTER TABLE [Reports] ADD FOREIGN KEY ([id]) REFERENCES [employee] ([repoerts])
GO

ALTER TABLE [History] ADD FOREIGN KEY ([id]) REFERENCES [employee] ([history])
GO

ALTER TABLE [medicines] ADD FOREIGN KEY ([id]) REFERENCES [employee] ([mediscines])
GO

ALTER TABLE [Notes] ADD FOREIGN KEY ([id]) REFERENCES [employee] ([notes])
GO

ALTER TABLE [complaints] ADD FOREIGN KEY ([id]) REFERENCES [employee] ([complaints])
GO

ALTER TABLE [Reports] ADD FOREIGN KEY ([id]) REFERENCES [Guest] ([repoerts])
GO

ALTER TABLE [History] ADD FOREIGN KEY ([id]) REFERENCES [Guest] ([history])
GO

ALTER TABLE [medicines] ADD FOREIGN KEY ([id]) REFERENCES [Guest] ([mediscines])
GO

ALTER TABLE [Notes] ADD FOREIGN KEY ([id]) REFERENCES [Guest] ([notes])
GO

ALTER TABLE [complaints] ADD FOREIGN KEY ([id]) REFERENCES [Guest] ([complaints])
GO

ALTER TABLE [Session] ADD FOREIGN KEY ([id]) REFERENCES [Guest] ([sessionMedical])
GO

ALTER TABLE [orders] ADD FOREIGN KEY ([id]) REFERENCES [Guest] ([sessionMedicine])
GO

ALTER TABLE [medicines] ADD FOREIGN KEY ([id]) REFERENCES [Reports] ([medicines])
GO

ALTER TABLE [medicines] ADD FOREIGN KEY ([id]) REFERENCES [pharmacy] ([medicines])
GO

ALTER TABLE [orders] ADD FOREIGN KEY ([id]) REFERENCES [pharmacy] ([orders])
GO

ALTER TABLE [Category] ADD FOREIGN KEY ([id]) REFERENCES [medicines] ([Category])
GO

ALTER TABLE [medicines] ADD FOREIGN KEY ([id]) REFERENCES [orders] ([medicines])
GO
