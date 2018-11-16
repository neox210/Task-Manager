using System.Collections.Generic;
using System.Collections.ObjectModel;
using Moq;
using NUnit.Framework;
using TaskManager.Models;
using TaskManager.Models.Settings;
using TaskManager.Repository;
using TaskManager.ViewModels;

namespace TaskManager.Tests
{
    [TestFixture]
    public class MainWindowModelTest
    {
        private Mock<MainWindowModel> _mainWindow;
        private Mock<IUnitOfWork> _unitOfWork;
        [SetUp]
        public void Initialize()
        {
            var tasks = new Mock<ITaskRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(u => u.Tasks).Returns(tasks.Object);
            

            var appSettings = new Mock<IAppSettings>();
            appSettings.Setup(a => a[ConstValues.TaskMaxSubjectName]).Returns(100.ToString);
            appSettings.Setup(a => a[ConstValues.TaskMaxDescriptionName]).Returns(1000.ToString);
            appSettings.Setup(a => a[ConstValues.PriorityConfigName]).Returns("Normal");
            appSettings.Setup(a => a[ConstValues.StatusConfigName]).Returns("New");

            var mainWindow = new Mock<MainWindowModel>(_unitOfWork.Object, new FakeDialogHelper(), appSettings.Object);
                      
            _mainWindow = mainWindow;
        }

        [Test]
        public void AddCommand_WhenCalled_AddsTask()
        {
            var task = new Task()
            {
                Id = 1,
                Description = "desc",
                Subject = "sub"
            };

            _unitOfWork.Setup(u => u.Tasks.Add(task)).Verifiable();
            _mainWindow.Setup(m => m.EditableTask).Returns(task);
            _mainWindow.Setup(m => m.TaskList).Returns(new ObservableCollection<Task>());

            _mainWindow.Object.AddCommand.Execute(null);

            _unitOfWork.Verify(u => u.Tasks.Add(task));
            _unitOfWork.Verify(u => u.Complete());
            Assert.That(_mainWindow.Object.TaskList, Has.Count.EqualTo(1)); 
        }

        [Test]
        public void AddCommand_WhenCalledWithBadSubject_DoNotAddsTask()
        {
            var task = new Task()
            {
                Id = 1,
                Description = "desc",
                Subject = ""
            };

            _unitOfWork.Setup(u => u.Tasks.Add(task)).Verifiable();
            _mainWindow.Setup(m => m.EditableTask).Returns(task);
            _mainWindow.Setup(m => m.TaskList).Returns(new ObservableCollection<Task>());

            _mainWindow.Object.AddCommand.Execute(null);

            _unitOfWork.Verify(u => u.Tasks.Add(task), Times.Never);
            _unitOfWork.Verify(u => u.Complete(), Times.Never);
            Assert.That(_mainWindow.Object.TaskList, Has.Count.EqualTo(0));
        }

        [Test]
        public void AddCommand_WhenCalledWithBadDescription_DoNotAddsTask()
        {
            var task = new Task()
            {
                Id = 1,
                Description = "",
                Subject = "sub"
            };

            _unitOfWork.Setup(u => u.Tasks.Add(task)).Verifiable();
            _mainWindow.Setup(m => m.EditableTask).Returns(task);
            _mainWindow.Setup(m => m.TaskList).Returns(new ObservableCollection<Task>());

            _mainWindow.Object.AddCommand.Execute(null);

            _unitOfWork.Verify(u => u.Tasks.Add(task), Times.Never);
            _unitOfWork.Verify(u => u.Complete(), Times.Never);
            Assert.That(_mainWindow.Object.TaskList, Has.Count.EqualTo(0));
        }

        [Test]
        public void EditCommand_WhenCalled_Edits_Task()
        {
            var task = new Task()
            {
                Id = 1,
                Description = "desc",
                Subject = "sub"
            };
            _mainWindow.Setup(m => m.EditableTask).Returns(task);

            _mainWindow.Object.EditCommand.Execute(null);

            _unitOfWork.Verify(u => u.Complete());
        }

        [Test]
        public void EditCommand_WhenCalledWithBadDescription_DoNotEdits_Task()
        {
            var task = new Task()
            {
                Id = 1,
                Description = "",
                Subject = "sub"
            };
            _mainWindow.Setup(m => m.EditableTask).Returns(task);

            _mainWindow.Object.EditCommand.Execute(null);

            _unitOfWork.Verify(u => u.Complete(), Times.Never);
        }

        [Test]
        public void EditCommand_WhenCalledWithBadSubject_DoNotEdits_Task()
        {
            var task = new Task()
            {
                Id = 1,
                Description = "desc",
                Subject = ""
            };
            _mainWindow.Setup(m => m.EditableTask).Returns(task);

            _mainWindow.Object.EditCommand.Execute(null);

            _unitOfWork.Verify(u => u.Complete(), Times.Never);
        }

        [Test]
        public void DeleteCommand_WhenCalled_DeletesTask()
        {
            var task = new Task()
            {
                Id = 1,
                Description = "desc",
                Subject = "sub"
            };
            _unitOfWork.Setup(u => u.Tasks.Remove(task)).Verifiable();
            _mainWindow.Setup(m => m.EditableTask).Returns(task);
            _mainWindow.Setup(m => m.TaskList).Returns(new ObservableCollection<Task>(){task});

            _mainWindow.Object.DeleteCommand.Execute(null);

            _unitOfWork.Verify(u => u.Tasks.Remove(task));
            _unitOfWork.Verify(u => u.Complete());
            Assert.That(_mainWindow.Object.TaskList, Has.Count.EqualTo(0));
        }

        [Test]
        public void FilterTasksCommand_WhenCalled_ReturnsFilteredTasks()
        {
            var task = new Task()
            {
                Id = 1,
                Description = "desc",
                Subject = "sub"
            };
            var filter = "filter";
            IEnumerable<Task> tasks = new List<Task>() {task, task};
            _unitOfWork.Setup(u => u.Tasks.GetFiltered(filter)).Returns(tasks).Verifiable();
            _mainWindow.Setup(m => m.TaskFilter).Returns(filter);
            _mainWindow.Setup(m => m.TaskList).Returns(new ObservableCollection<Task>());

            _mainWindow.Object.FilterTasksCommand.Execute(null);

            _unitOfWork.Verify(u => u.Tasks.GetFiltered(filter));
            Assert.That(_mainWindow.Object.TaskList, Has.Count.EqualTo(2)); 
        }

        [Test]
        public void ClearFilterTasks_WhenCalled_ReturnsAllTasks()
        {
            var task = new Task()
            {
                Id = 1,
                Description = "desc",
                Subject = "sub"
            };
            IEnumerable<Task> tasks = new List<Task>() { task, task };
            _unitOfWork.Setup(u => u.Tasks.GetAll()).Returns(tasks);
            _mainWindow.Setup(m => m.TaskList).Returns(new ObservableCollection<Task>());

            _mainWindow.Object.ClearFilterTasksCommand.Execute(null);
            
            _unitOfWork.Verify(u => u.Tasks.GetAll());
            Assert.That(_mainWindow.Object.TaskList, Has.Count.EqualTo(2));
        }
    }
}
