namespace Domain.ValueObjects
{
    public enum BookingStatus
    {
       PENDING,
       CLOSED,
       CANCELLED

    }

    public enum TestStatus
    {
      PENDING,
      COMPLETED
    }

    public enum TestType
    {
      PCR,
      RAPID
    }

    public enum Role
    {
      USER,
      ADMIN,
      LABADMIN
    }
    
}