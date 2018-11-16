using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
