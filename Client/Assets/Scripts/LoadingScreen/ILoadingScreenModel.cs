namespace LoadingScreen
{
    public interface ILoadingScreenModel
    {
        void SetMaxLoadElementsCount(int value);
        void IncrementProgressValue();
        void UpdateScreenMessage(string message);
        void Show();
        void Hide();
    }
}