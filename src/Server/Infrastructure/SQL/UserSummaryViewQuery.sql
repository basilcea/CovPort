SELECT * FROM UserSummaryView
WHERE UserId=@userId
ORDER BY BookingId
OFFSET @PageSize * (@PageNumber - 1) ROWS
FETCH NEXT @PageSize ROWS ONLY