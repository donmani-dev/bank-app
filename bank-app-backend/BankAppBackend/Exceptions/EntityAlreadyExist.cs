namespace BankAppBackend.Exceptions
{
    public class EntityAlreadyExist:Exception
    {
        public EntityAlreadyExist(string exceptionMessage):base(exceptionMessage) { }
    }

    public class EntityNotFound : Exception
    {
        public EntityNotFound(string exceptionMessage):base(exceptionMessage) { }
    }
}
