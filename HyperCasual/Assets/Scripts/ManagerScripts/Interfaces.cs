public interface IPlayerState
{
    void Handle(PlayerController playerController);
    void DeactivateState();
}

public interface IListener
{
    void OnEventOccured(NotificationManager.EVENT_TYPE eventName, float parameter = 0);
}

public interface IPlayerInput
{
    float GiveInput();
}