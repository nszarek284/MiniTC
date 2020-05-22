using Microsoft.Expression.Interactivity.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using System.Diagnostics;

namespace MiniTC_Projekt.Models
{
    class ModelDirInfo
    {
       private bool _drive;
       private bool _parent;

       private string _directory;

       public ModelDirInfo()
        {
            FullDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }

        public ModelDirInfo(string FullDirectory, bool _parent1)
        {
            FullDirectoryPath = FullDirectory;
            _parent = _parent1;

            if (FullDirectoryPath.Length == 3)
            {
                _drive = true;
            }
            else
            {
                _drive = false;
            }
        }
       public string FullDirectoryPath
       {
            get;
            private set;
       }
       public string ParentDirectoryPath
       {
           get
           {
                if (_drive)
                {
                    return null;
                }

                _directory = FullDirectoryPath.Substring(0, FullDirectoryPath.LastIndexOf('\\'));

                if (_directory.Length == 2)
                {
                    _directory += '\\';
                }

                return _directory;
            }
       }

        public string Drive
        {
            get 
            {
                if (!_drive)
                    return FullDirectoryPath.Substring(0, 3);
                else
                    return FullDirectoryPath;
            }
        }

        public bool isDirectory
        {
            get => File.GetAttributes(FullDirectoryPath).HasFlag(FileAttributes.Directory);
        }

        public string Name
        {
            get => FullDirectoryPath.Substring(FullDirectoryPath.LastIndexOf('\\') + 1);
        }

        public List<ModelDirInfo> ListOfChildren
        {
            get
            {
                string[] listOfDirectories = Directory.GetDirectories(FullDirectoryPath);
                string[] listOfFiles = Directory.GetFiles(FullDirectoryPath);
                List<ModelDirInfo> InfoList = new List<ModelDirInfo>();

                _parent = false;

                if(ParentDirectoryPath != null)
                {
                    InfoList.Add(new ModelDirInfo(ParentDirectoryPath, true));
                }

                foreach(string d in listOfDirectories)
                {
                    InfoList.Add(new ModelDirInfo(d,false));
                }

                foreach(string f in listOfFiles)
                {
                    InfoList.Add(new ModelDirInfo(f, false));
                }

                return InfoList;
            }
        }

        public override string ToString()
        {
            if (_parent)
            {
                return "..";
            }
            else if (isDirectory)
            {
                return "<D>" + Name;
            }
            else
            {
                return Name;
            }
        }



    }
}
