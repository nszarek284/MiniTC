using MiniTC_Projekt.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Resx = MiniTC_Projekt.Properties.Resources;

namespace MiniTC_Projekt.ViewModels
{
    class ViewModelMainWindow : ViewModelBase
    {
        private RelayCommand _copying;

        public ViewModelPanelTC LeftPanel
        {
            get;
        }
        = new ViewModelPanelTC();

        public ViewModelPanelTC RightPanel
        {
            get;
        }
        = new ViewModelPanelTC();

        public RelayCommand Copy
        {
            get
            {
                if(_copying == null)
                {
                    _copying = new RelayCommand(
                        action =>
                        {
                            try
                            {
                                File.Copy(LeftPanel.SelectedItem, RightPanel.CurrentDir + "/" + LeftPanel.ItemName);
                                RightPanel.Refresh();
                            }
                            catch
                            {
                                MessageDialogBox msgWindow = new MessageDialogBox();
                                msgWindow.Caption = Properties.Resources.ErrorCaption;
                                msgWindow.Icon = MessageBoxImage.Error;
                                msgWindow.showMessageBox(Properties.Resources.ErrorText);
                                return;
                            }
                        },
                        condition => LeftPanel.CurrentDir != RightPanel.CurrentDir
                        ) ;
                }

                return _copying;
            }
        }
    }
}
