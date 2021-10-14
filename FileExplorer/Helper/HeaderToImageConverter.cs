using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FileExplorer.Helper
{
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter headerToImageConverter = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = (string)value;

            if (path == null)
            {
                return null;
            }

            string name = MainWindow.getFileFolderName(path);

            string imagePath = @"C:\Users\36302\source\repos\FileExplorer\FileExplorer\Images\file.png";

            if (string.IsNullOrEmpty(name))
            {
                imagePath = @"C:\Users\36302\source\repos\FileExplorer\FileExplorer\Images\drive.png";
            }
            else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
            {
                imagePath = @"C:\Users\36302\source\repos\FileExplorer\FileExplorer\Images\folder.png";
            }

            return new BitmapImage(new Uri(imagePath));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
