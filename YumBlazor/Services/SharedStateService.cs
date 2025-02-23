namespace YumBlazor.Services
{
    public class SharedStateService
    {
        public event Action OnChange;
        private int _count;
        
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
               
            }
        }
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
