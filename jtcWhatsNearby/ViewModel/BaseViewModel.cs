
namespace jtcWhatsNearby.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        //  Base view model from which all others derive
        public BaseViewModel()
        {

        }

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(IsNotBusy))]
        bool isBusy;

        public bool IsNotBusy => !isBusy;

        [ObservableProperty]
        string title;

        [ObservableProperty]
        bool isRefreshing;
    }
}
