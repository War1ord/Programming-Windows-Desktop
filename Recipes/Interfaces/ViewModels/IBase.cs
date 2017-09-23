using System.Windows.Input;

namespace Interfaces.ViewModels
{
    public interface IBase
    {
        bool CanExecute { get; }

		bool Add();
		bool Update();
		bool Delete();

	    ICommand AddCommand { get; set; }
	    ICommand UpdateCommand { get; set; }
	    ICommand DeleteCommand { get; set; }

    }
}