namespace TaskManager.ViewModels.Dialogs
{
    public interface IDialogHelper
    {
        bool DecisionDialog(string message);
        void SuccessDialog(string message);
        void FailDialog(string message);
    }
}
