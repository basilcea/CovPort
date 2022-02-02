SELECT 
    LocationName, BookingCapacity, SUM(Booking), 
    SUM(Test), SUM(Positive), SUM(Negative), SUM(Awaiting)
FROM ReportView 
WHERE DATE=@Date
Group By LocationName, BookingCapacity
ORDER BY LocationName
OFFSET @PageSize * (@PageNumber - 1) ROWS
FETCH NEXT @PageSize ROWS ONLY