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
using System.Xml;

namespace StreamWriterTest
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private string filePath = @"D:\FileCreateTest\test.xml";
        int cnt = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cnt++;
            StreamWriter sw = null;
            sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8);
            XmlDocument doc = new XmlDocument();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineOnAttributes = true;

            XmlWriter xmlWriter = XmlWriter.Create(sw);

            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("IntgrTrgtData_Info");
            xmlWriter.WriteStartElement("Project_Info");
            xmlWriter.WriteElementString("Folder_Path", "123123123");
            xmlWriter.WriteElementString("File_Name", "`122124124");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("DTM_Info");
            xmlWriter.WriteElementString("File_Path", "124516126126");
            xmlWriter.WriteElementString("DB_ID", "");
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Ortho_Info");
            xmlWriter.WriteElementString("File_Path", "12413516161261265");
            xmlWriter.WriteElementString("DB_ID", "");
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();

            xmlWriter.Flush();
            xmlWriter.Close();

            sw.Close();

            XmlTextReader reader = new XmlTextReader(filePath);
            XmlDocument xmldoc = new XmlDocument();
            doc.PreserveWhitespace = false;
            doc.Load(reader);
            reader.Close();
            doc.Save(filePath);
        }
    }
}
