public class GameStateManager
{
    public IPlayerState CurrentState { get; set; }
    public IPlayerState PreviousState { get; set; }

    private readonly PlayerController _playerController;

    public GameStateManager(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void Transition(IPlayerState state)
    {
        if (CurrentState != null)
        {
            DeactivatePreviousState();
        }

        CurrentState = state;
        CurrentState.Handle(_playerController);
    }

    public void DeactivatePreviousState()
    {
        PreviousState = CurrentState;
        PreviousState.DeactivateState();
    }
}