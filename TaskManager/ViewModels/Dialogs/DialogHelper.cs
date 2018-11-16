#region

using System.Windows;

#endregion

namespace TaskManager.ViewModels.Dialogs
{
    public class DialogHelper : IDialogHelper
    {
        public bool DecisionDialog(string message)
        {
            var result = MessageBox.Show(message, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }

        public void SuccessDialog(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButton.OK);
        }

        public void FailDialog(string message)
        {
            MessageBox.Show(message, "Failed", MessageBoxButton.OK);
        }
    }
}
