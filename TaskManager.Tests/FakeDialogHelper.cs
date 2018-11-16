using TaskManager.ViewModels.Dialogs;

namespace TaskManager.Tests
{
    public class FakeDialogHelper : IDialogHelper
    {
        public bool DecisionDialog(string message)
        {
            return true;
        }

        public void SuccessDialog(string message)
        {
            
        }

        public void FailDialog(string message)
        {
            
        }
    }
}
