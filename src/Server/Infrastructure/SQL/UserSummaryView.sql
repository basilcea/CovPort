Create OR ALTER VIEW UserSummaryView As
SELECT u.Id as UserId, u.Email, u.Name, u.UserRole, u.DateCreated as UserCreated, 
u.DateUpdated as UserUpdated, b.Id as BookingId, b.SpaceId, b.Status as BookingStatus, 
b.TestType, b.LocationName, b.DateCreated as BookingCreated, b.DateUpdated as BookingUpdated,
r.Id as TestId, r.Status as TestStatus,  r.Positive, r.DateCreated as TestCreated, r.DateUpdated as TestUpdated
from Users u 
left join Bookings b on b.UserId = u.Id 
left join Results r on r.UserId = u.Id and r.BookingId = b.Id and r.Status= 'COMPLETED'