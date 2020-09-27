SET IDENTITY_INSERT AspNetUserClaims ON

INSERT INTO AspNetUserClaims ([Id], [UserId], [ClaimType], [ClaimValue])
                      VALUES (1, 1, N'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', N'Admin')

SET IDENTITY_INSERT AspNetUserClaims OFF