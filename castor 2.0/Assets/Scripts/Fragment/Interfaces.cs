namespace Fragment
{
    public interface IFragmentStateChanged
    {
        void OnStateChanged(FragmentState newState);
    }

    public interface IFragmentStateElementToggled
    {
        void OnToggledLockedState(bool locked);
        void OnToggleSelectionState(bool selected);
    }

    public delegate void EventHandler();
}