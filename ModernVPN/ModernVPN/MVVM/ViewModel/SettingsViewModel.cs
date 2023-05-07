namespace ModernVPN.MVVM.ViewModel
{
    class SettingsViewModel
    {
        public GlobalViewModel Global { get; } = GlobalViewModel.Instance; 
        public SettingsViewModel()
        {
            
        }
    }
}
