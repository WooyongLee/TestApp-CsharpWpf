using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Xml;

namespace XMLParsingCtrl
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        // 읽어서 처리할 XML 파일 경로
        private readonly static string XmlFilePath = @"C:\Users\wooyo\Desktop\Parsing\2D\XMilmapConfig.xml";

        // 읽을 노드의 레벨
        private readonly static string SelectNodeLevel = "XMilmap/Scales/Scale";

        // XML 파일을 처리할 Document 객체
        public XmlDocument XmlDoc = new XmlDocument();

        // Map Data ListView와 바인딩 된 Observable List
        public ObservableCollection<MapItem> MapItemObservableList;

        public MainWindow()
        {
            InitializeComponent();

            // ObservableList 객체 생성
            MapItemObservableList = new ObservableCollection<MapItem>();

            // XML 파일 읽기
            this.ReadXMLFile();

            // 리스트 뷰와 Observable List 바인딩
            this.MapDataXMLListView.ItemsSource = MapItemObservableList;
        }

        public void ReadXMLFile()
        {
            if (this.MapItemObservableList.Count > 0)
            {
                this.MapItemObservableList.Clear();
            }

            // 해당 Path의 Xml 파일을 읽어옴
            XmlDoc.Load(XmlFilePath);

            XmlNodeList xmlNodeList = XmlDoc.SelectNodes(SelectNodeLevel);

            foreach (XmlNode xmlNode in xmlNodeList)
            {
                foreach (XmlElement node in xmlNode)
                {
                    string file = node.InnerText;

                    MapItemObservableList.Add(new MapItem { FilePath = file });
                }
            }
        }

        // 로컬 저장소를 탐색하여 MapData를 리스트에 넣고, 해당 Xml 파일에 추가
        // To Do :: 선택한 데이터를 [XML 파일에 추가하는 것] - [ListView에 추가하는 것]을 각자 분리하여 함수 구현
        private void AddMapData()
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = dlg.SelectedPath;

               // XmlDoc.Load(XmlFilePath); // XML 파일 이미 최초에 로드 하고 한번 더 로드 할 필요 없음

                string[] files = Directory.GetFiles(path, "*.toc", SearchOption.AllDirectories);

                XmlNodeList xmlNodeList = XmlDoc.SelectNodes(SelectNodeLevel);

                foreach (XmlNode xmlNode in xmlNodeList)
                {
                    foreach (XmlAttribute xmlAtb in xmlNode.Attributes)
                    {
                        if (xmlAtb.Name == "Type" && xmlAtb.Value == "RPF")
                        {
                            foreach (string file in files)
                            {
                                XmlElement xmlEle = XmlDoc.CreateElement("DataSource");
                                xmlEle.InnerText = file;
                                xmlNode.AppendChild(xmlEle);
                                MapItemObservableList.Add(new MapItem { FilePath = file });
                            }
                        }
                    }
                }

                XmlDoc.Save(XmlFilePath);
            }
        }

        // 체크박스에 체크된 항목을 삭제하기
        private void RemoveCheckedData()
        {
            // Observable List 갯수 확인
            if (this.MapItemObservableList.Count <= 0)
            {
                return;
            }

            // 1. Observable List를 탐색하면서 체크된 데이터를 찾아내어 제거
            // 2. 데이터의 내용과 XML Node의 string 데이터와 비교하여 같은 것을 XML 파일 내에서 제거
            for ( int index = 0; index < MapItemObservableList.Count; index++)
            {
                MapItem mapItem = MapItemObservableList[index];

                // 체크가 되어 있는 데이터들 확인
                if (mapItem.IsCheck)
                {
                    // XML 에서 체크된 파일 명에 노드를 삭제하기
                    this.DeleteXmlNode(mapItem.FilePath);

                    // 리스트 뷰에서 삭제
                    MapItemObservableList.RemoveAt(index);

                    // 삭제 후 index 제자리로(탐색하면서 index가 넘어감)
                    index--;

                }
            }
        }

        // 선택한 XML 노드 삭제하기
        private void DeleteXmlNode(string selectedNode)
        {
            XmlNodeList xmlNodeList = XmlDoc.SelectNodes(SelectNodeLevel);

            foreach (XmlNode xmlNode in xmlNodeList)
            {
                foreach (XmlElement node in xmlNode)
                {
                    // XML 노드에서 해당 node를 삭제함
                    xmlNode.RemoveChild(node);
                }
            }

            XmlDoc.Save(XmlFilePath); // XML파일 저장.. ^^
        }

        // 등록 버튼 클릭 이벤트
        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            this.AddMapData();

            // To Do :: 맨 아래 행으로 포커싱
        }

        // 삭제버튼 클릭 이벤트 - 체크된 항목이 삭제됨
        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            this.RemoveCheckedData();

            // To Do :: 삭제 된 행 바로 위로 포커싱
        }
    }

    public class MapItem
    {
        public bool IsCheck { get; set; }
        public string FilePath { get; set; }
        public string Scale { get; set; }
    }
}
