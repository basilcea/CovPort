CREATE OR ALTER View ReportView 
AS
SELECT s.Date, s.LocatiONName , s.SpacesCreated AS BookingCapacity,
(CASE WHEN b.Status = 'PENDING' THEN 1 ELSE 0 END ) AS Booking,
(CASE WHEN r.Status IS NULL THEN 0 ELSE 1 END) AS Test,
(CASE WHEN r.Status ='PENDING' THEN 1 ELSE 0 END) As Awaiting,
(CASE WHEN r.Status = 'COMPLETED' AND r.Positive = 1 THEN 1 ELSE 0 END) AS Positive,
(CASE WHEN r.Status = 'COMPLETED' AND r.Positive = 0 THEN 1 ELSE 0 END) AS Negative
from Spaces s
LEFT JOIN Bookings b ON b.SpaceId = s.Id
LEFT JOIN Results r  ON r.TestLocatiON = s.LocatiONName AND r.BookingId = b.Id