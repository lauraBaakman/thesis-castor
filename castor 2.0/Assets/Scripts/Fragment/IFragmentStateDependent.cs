namespace Fragment
{
    public interface IFragmentStateDependent
    {
        void OnStateToLocked();
        void OnStateToUnLocked();

        void OnStateToSelected();
        void OnStateToDeselected();

        void OnStateToHidden();
        void OnStateToVisible();
    }
}