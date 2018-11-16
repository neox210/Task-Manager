#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using CommonServiceLocator;
using TaskManager.Models;
using TaskManager.Models.Settings;
using TaskManager.Repository;
using TaskManager.ViewModels.Dialogs;
using TaskManager.ViewModels.ExtensionMethods;

#endregion

namespace TaskManager.ViewModels
{
    public class MainWindowModel : ObservableObject
    {
        public ICommand AddCommand => new DelegateCommand(AddTask);
        public ICommand EditCommand => new DelegateCommand(EditTask);
        public ICommand DeleteCommand => new DelegateCommand(DeleteTask);
        public ICommand ClearCommand => new DelegateCommand(ClearEditableTask);
        public ICommand FilterTasksCommand => new DelegateCommand(FilterTasks);
        public ICommand ClearFilterTasksCommand => new DelegateCommand(ClearFilterTasks);

        public virtual string TaskFilter { get; set; }

        public virtual Task EditableTask { get; private set; }
        public virtual ObservableCollection<Task> TaskList { get; private set; }
        public virtual ObservableCollection<Priority> Priorities { get; private set; }
        public virtual ObservableCollection<Status> Statuses { get; private set; }

        public Task SelectedTask
        {
            get => _selectedTask;
            set
            {
                if (_selectedTask == value) return;
                _selectedTask = value;
                EditableTask = _selectedTask;
                RaisePropertyChangedEvent(nameof(EditableTask));
            }
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDialogHelper _dialogHelper;
        private readonly IAppSettings _appSettings;
        private readonly ExceptionInterceptor _exceptionInteceptor;
        private Task _selectedTask;


        public MainWindowModel() : 
            this(ServiceLocator.Current.GetInstance<IUnitOfWork>(), 
                ServiceLocator.Current.GetInstance<IDialogHelper>(),
                ServiceLocator.Current.GetInstance<IAppSettings>())
        {
            
        }

        public MainWindowModel(IUnitOfWork unitOfWork, IDialogHelper dialogHelper, IAppSettings appSettings)
        {
            _unitOfWork = unitOfWork;
            _dialogHelper = dialogHelper;
            _appSettings = appSettings;
            _exceptionInteceptor = new ExceptionInterceptor(_dialogHelper);
            Initialize();
        }

        private void Initialize()
        {
             _exceptionInteceptor.TaskInterceptorAsync(() =>
            {
                var tasks = _unitOfWork.Tasks.GetAll();
                TaskList = new ObservableCollection<Task>(tasks);

                var priorities = _unitOfWork.Tasks.GetAvailablePriorities();
                Priorities = new ObservableCollection<Priority>(priorities);

                var statuses = _unitOfWork.Tasks.GetAvailableStatuses();
                Statuses = new ObservableCollection<Status>(statuses);

                EditableTask = CreateDefaultTask();

                RaisePropertyChangedEvent(nameof(TaskList));
                RaisePropertyChangedEvent(nameof(Priorities));
                RaisePropertyChangedEvent(nameof(Statuses));
                RaisePropertyChangedEvent(nameof(EditableTask));
            });
        }

        private Task CreateDefaultTask()
        {
            return new Task
            {
                Priority = Priorities.SingleOrDefault(p => p.Name.Contains(_appSettings[ConstValues.PriorityConfigName])),
                Status = Statuses.SingleOrDefault(p => p.Name.Contains(_appSettings[ConstValues.StatusConfigName])),
                Subject = "Enter subject...",
                Description = "Enter decription..."
            };
        }

        private void AddTask()
        {
            _exceptionInteceptor.TaskInterceptor(() =>
            {
            if (!TaskMeetsConditions(EditableTask)) return;
                if (!_dialogHelper.DecisionDialog("Do you want add this task as new one?")) return;
                _unitOfWork.Tasks.Add(EditableTask);
                _unitOfWork.Complete();
                TaskList.Add(EditableTask);
                _dialogHelper.SuccessDialog("Successfully added new task");
            });     
        }

        private bool TaskMeetsConditions(Task task)
        {
            return    task != null
                   && task.Subject.Length <= int.Parse(_appSettings[ConstValues.TaskMaxSubjectName])
                   && !string.IsNullOrWhiteSpace(task.Subject)
                   && task.Description.Length <= int.Parse(_appSettings[ConstValues.TaskMaxDescriptionName])
                   && !string.IsNullOrWhiteSpace(task.Description);
        }

        private void EditTask()
        {
            _exceptionInteceptor.TaskInterceptor(() =>
            {
                if (!TaskMeetsConditions(EditableTask)) return;
                if (!_dialogHelper.DecisionDialog("Do you want edit this task?")) return;

                _unitOfWork.Complete();
                RaisePropertyChangedEvent(nameof(TaskList));
                _dialogHelper.SuccessDialog("Successfully edited new task");
            });
        }

        private void DeleteTask()
        {
            _exceptionInteceptor.TaskInterceptor(() =>
            {
                if (!_dialogHelper.DecisionDialog("Do you want delete this task?")) return;

                _unitOfWork.Tasks.Remove(EditableTask);
                _unitOfWork.Complete();
                TaskList.Remove(EditableTask);
                _dialogHelper.SuccessDialog("Successfully deleted new task");
            });
        }

        private void ClearEditableTask()
        {  
            var task = CreateDefaultTask();
            EditableTask = task;
            RaisePropertyChangedEvent(nameof(EditableTask));
        }

        private void FilterTasks()
        {
            _exceptionInteceptor.TaskInterceptor(() =>
            {
                if (string.IsNullOrWhiteSpace(TaskFilter)) return;

                var filteredTasks = _unitOfWork.Tasks.GetFiltered(TaskFilter);
                TaskList.Clear();
                TaskList.AddRange(filteredTasks);
                EditableTask = CreateDefaultTask();
                RaisePropertyChangedEvent(nameof(TaskList));
            });
        }

        private void ClearFilterTasks()
        {
            _exceptionInteceptor.TaskInterceptor(() =>
            {
                var tasks = _unitOfWork.Tasks.GetAll();
                TaskList.Clear();
                TaskList.AddRange(tasks);
                EditableTask = CreateDefaultTask();
                RaisePropertyChangedEvent(nameof(TaskList));
            });
        }
    }
}
