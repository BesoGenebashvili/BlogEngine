SET IDENTITY_INSERT AspNetUsers ON

INSERT INTO AspNetUsers ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName])
                 VALUES (1, N'Admin@gmail.com', N'ADMIN@GMAIL.COM', N'Admin@gmail.com', N'ADMIN@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEMVrQ73g3IREcjyBJ7F0kesTO4PVSWkS1qj9n0ht8rbzkP6iU9y3kP8zb24rJJ1pIQ==', N'K4DG2TT5NP5BKPGWIC6NATICRYEWHCLK', N'5b304513-f4c6-4f97-86ad-55af1a7694ac', NULL, 0, 0, NULL, 1, 0, N'Admin', N'Admin')

SET IDENTITY_INSERT AspNetUsers OFF