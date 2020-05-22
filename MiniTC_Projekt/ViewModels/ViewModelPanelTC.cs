using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MiniTC_Projekt.Models;

namespace MiniTC_Projekt.ViewModels
{
    internal class ViewModelPanelTC : ViewModelBase
    {
        private int _selectedIndex;

        private string _selectedItem;
        private string _selectedDrive = null;

        private ModelDirInfo _currentDir = new ModelDirInfo();


        private RelayCommand _changeDir = null;
        private RelayCommand _changeDrive = null;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set 
            {
                _selectedIndex = value;
                
                if(SelectedIndex == -1)
                {
                    SelectedItem = null;
                }
                else
                {
                    SelectedItem = Directories[SelectedIndex].FullDirectoryPath;
                }
            }
        }

        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                onPropertyChanged(nameof(SelectedItem));
            }
        }

        public string ItemName
        {
            get => Directories[SelectedIndex].Name;
        }

        public List<ModelDirInfo> Directories
        {
            get => _currentDir.ListOfChildren;
        }

        public string CurrentDir
        {
            get => _currentDir.FullDirectoryPath;
        }

        public string Drive
        {
            get
            {
                if(_selectedDrive != null)
                {
                    return _selectedDrive;
                }
                else
                {
                    return _currentDir.Drive;
                }
            }
            set => _selectedDrive = value;
        }

        public static string[] DrivesNames
        {
            get => Directory.GetLogicalDrives();
        }

        public void Refresh()
        {
            onPropertyChanged(nameof(Directories));
        }

        public RelayCommand ChangeDir
        {
            get
            {
                if (_changeDir == null)
                {
                    _changeDir = new RelayCommand(
                        arg =>
                        {
                            _currentDir = Directories[SelectedIndex];
                            onPropertyChanged(nameof(CurrentDir), nameof(Drive), nameof(Directories));
                        },
                        arg => SelectedIndex != -1 && Directories[SelectedIndex].isDirectory);
                }
                return _changeDir;
            }
        }

        public RelayCommand ChangeDrive
        {
            get
            {
                if(_changeDrive == null)
                {
                    _changeDrive = new RelayCommand(
                        arg =>
                        {
                            _currentDir = new ModelDirInfo(Drive,false);
                            onPropertyChanged(nameof(CurrentDir), nameof(Directories));
                        },
                        arg => true );
                }
                return _changeDrive;
            }
        }

    }
}
