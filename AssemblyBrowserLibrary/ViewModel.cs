using System;
using System.ComponentModel;
using AssemblyBrowserLibrary.ResultStructure;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace AssemblyBrowserLibrary
{
    // ViewModel part in MVVM
    public class ViewModel : INotifyPropertyChanged
    {
        private Result _result;
        private AssemblyReader _asmReader;
        private Command _openCommand;

        public ViewModel()
        {
            _asmReader = new AssemblyReader();
        }

        public Result Result
        {
            get { return _result; }
            set
            {
                _result = value;

                // if result is changed, 
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // if even is not null, invoke it
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Command OpenCommand
        {
            get
            {
                // if command is not created, create one
                return _openCommand ??
                    (_openCommand = new Command(obj =>
                    {
                        try
                        {
                            // reading selected assembly
                            OpenFileDialog openFileDialog = new OpenFileDialog();
                            if (openFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                Result = _asmReader.Read(openFileDialog.FileName);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }));
            }
        }
    }
}
