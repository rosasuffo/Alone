// every class that inherits this interface will be able to listen to a subject
public interface IObserver
{
    public void OnNotify(PlayerActions action);
}
