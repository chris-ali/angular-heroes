namespace angular_heroes.Infrastructure
{
    public interface ICurrentUserAccessor
    {
        public string GetCurrentUserName();
    }
}