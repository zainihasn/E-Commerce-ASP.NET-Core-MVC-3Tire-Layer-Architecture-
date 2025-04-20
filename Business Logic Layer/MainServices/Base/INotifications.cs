namespace BusinessLogicLayer.Service.Base
{
    public interface INotifications
    {
        Task SendOrderEmailToAdmin(string a);
        Task SendMessageAsync( string a);
    }
}
