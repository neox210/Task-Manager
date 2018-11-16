using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.ViewModels.Dialogs
{
    public interface IDialogHelper
    {
        bool DecisionDialog(string message);
        void SuccessDialog(string message);
        void FailDialog(string message);
    }
}
