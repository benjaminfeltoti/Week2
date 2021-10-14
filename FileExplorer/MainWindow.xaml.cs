using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string drive in Directory.GetLogicalDrives())
            {
                TreeViewItem item = new TreeViewItem()
                {
                    Header = drive,
                    Tag = drive
                };

                item.Items.Add(null);
                item.Expanded += Folder_Expanded;
                folderTrVw.Items.Add(item);
            }
        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count != 1 || item.Items[0] != null)
            {
                return;
            }

            item.Items.Clear();
            string fullPath = (string)item.Tag;
            createTreeViewSubItem(item, fullPath, ElementType.Folder);
            createTreeViewSubItem(item, fullPath, ElementType.File);
        }

        enum ElementType { Folder, File };

        private void createTreeViewSubItem(TreeViewItem item, string fullPath, ElementType type)
        {
            getElements(fullPath, type).ForEach(path =>
            {
                TreeViewItem subItem = new TreeViewItem()
                {
                    Header = getFileFolderName(path),
                    Tag = path
                };

                switch (type)
                {
                    case ElementType.Folder:
                        subItem.Items.Add(null);
                        subItem.Expanded += Folder_Expanded;
                        item.Items.Add(subItem);
                        break;
                    case ElementType.File:
                        item.Items.Add(subItem);
                        break;
                    default:
                        break;
                }
            });

        }

        private static List<string> getElements(string fullPath, ElementType type)
        {
            var elements = new List<string>();
            string[] stringArray = null;

            switch (type)
            {
                case ElementType.Folder:
                    try
                    {
                        stringArray = Directory.GetDirectories(fullPath);
                    }
                    catch { }
                    break;
                case ElementType.File:
                    try
                    {
                        stringArray = Directory.GetFiles(fullPath);
                    }
                    catch { }
                    break;
            }

            if (stringArray != null && stringArray.Length > 0)
            {
                elements.AddRange(stringArray);
            }

            return elements;
        }

        public static string getFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            string normalizedPath = path.Replace('/', '\\');
            int lastIndex = normalizedPath.LastIndexOf('\\');
            if (lastIndex <= 0)
            {
                return path;
            }

            return path.Substring(lastIndex + 1);
        }


    }
}
