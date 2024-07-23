using Reactive.Field;

namespace LoadingScreen
{
    public class LoadingScreenModel : ILoadingScreenModel
    {
        public readonly ReactiveField<bool> IsShown = new();
        public readonly ReactiveField<string> CurrentScreenMessage = new();

        public float CurrentProgress { get; private set; }
        public int MaxLoadElementsCount { get; private set; }
        
        public LoadingScreenModel(bool initialState)
        {
            IsShown.Value = initialState;
        }

        public void SetMaxLoadElementsCount(int value)
        {
            MaxLoadElementsCount = value;
        }
        
        public void IncrementProgressValue()
        {
            CurrentProgress++;
        }
        
        public void UpdateScreenMessage(string message)
        {
            CurrentScreenMessage.Value = message;
        }
        
        public void Show()
        {
            IsShown.Value = true;
        }

        public void Hide()
        {
            IsShown.Value = false;
        }

        public void Reset()
        {
            CurrentScreenMessage.Value = string.Empty;
            CurrentProgress = 0;
            MaxLoadElementsCount = 0;
        }
    }
}