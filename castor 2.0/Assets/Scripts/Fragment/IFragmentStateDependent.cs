namespace Fragment
{
    public interface IFragmentStateDependent
    {
        void OnToggledLockedState(bool locked);
        void OnToggleSelectionState(bool selected);
    }

    public delegate void EventHandler();
}