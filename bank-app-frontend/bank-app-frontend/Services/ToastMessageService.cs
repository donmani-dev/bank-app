using BlazorBootstrap;

namespace Services
{
    public class ToastMessageService
    {
        private ToastService toastService;
        public ToastMessageService(ToastService toastService) {
            this.toastService = toastService;
        }

        public void ShowToastMessage(ToastType toastType, string header, string description) {
            toastService.Notify(new(toastType,IconName.Activity.ToString(), header, description));
        }

    }
}
