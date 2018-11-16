#region

using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TaskManager.ViewModels.Dialogs;

#endregion

namespace TaskManager.ViewModels
{
    public class ExceptionInterceptor
    {
        private readonly IDialogHelper _dialogHelper;

        public ExceptionInterceptor(IDialogHelper dialogHelper)
        {
            _dialogHelper = dialogHelper;
        }
        public void TaskInterceptor(Action action)
        {
            try
            {
                action();
            }
            catch (SqlException e)
            {
                _dialogHelper.FailDialog(e.Message);
            }
            catch (Exception e)
            {
                _dialogHelper.FailDialog(e.Message);
            }
        }

        public void TaskInterceptorAsync(Action action)
        {
            try
            {
                Task.Factory.StartNew(action);
            }
            catch (SqlException e)
            {
                _dialogHelper.FailDialog(e.Message);
            }
            catch (Exception e)
            {
                _dialogHelper.FailDialog(e.Message);
            }
        }
    }
}
