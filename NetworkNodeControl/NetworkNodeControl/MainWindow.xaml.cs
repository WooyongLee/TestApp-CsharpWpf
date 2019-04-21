using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NetworkNodeControl
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public int indexOfMountingNode = 0;
        public int indexOfGroundNode = 0;

        int nodeIndex = 0;
        int numOfLine = 0;

        public NetworkNode[] nodes;

        // 노드의 위치를 저장하는 Dictionary
        // key : 노드의 인덱스, value : 노드의 위치(x, y)
        public Dictionary<int, Point> NodePositionDic;

        List<Ellipse> EllipseList;
        List<Line> LineList;
        List<TextBlock> TextBlockList;

        // RA 데이터가 들어오는 상황을 추가
        private Dictionary<int, List<int>> RAFrameUnitDic = new Dictionary<int, List<int>>();

        private bool IsRAFirstReceive = true;

        public MainWindow()
        {
            InitializeComponent();

            nodes = new NetworkNode[ConstValue.MaxNode + 1];

            EllipseList = new List<Ellipse>();
            LineList = new List<Line>();
            TextBlockList = new List<TextBlock>();

            nodes[nodeIndex++] = new NetworkNode(NetworkType.None);

            NodePositionDic = new Dictionary<int, Point>();

            this.SetPointDic();
           // this.AddAllNode();
        }

        // RA 데이터가 들어올 시 노드 그리기를 제어
        public void SetRANodeData(int _GrantTermID, int ConnectedTermID)
        {
            // 키가 없다면 키를 만들고 새로 들어온 노드와 최초 연결
            if (!RAFrameUnitDic.ContainsKey(_GrantTermID))
            {
                List<int> AddedList = new List<int>();
                AddedList.Add(ConnectedTermID);
                RAFrameUnitDic.Add(_GrantTermID, AddedList);

                // 최초에 데이터를 받은 경우에 연결 생성
                AddConnection(_GrantTermID, ConnectedTermID, IsRAFirstReceive);

                // To Do :: 이미 이전의 키와 연결된 노드가 있는 경우에
                // [(1) 노드 재생성 방지] 및 [(2)기존 노드와의 연결]

            }

            // 키가 있다면 원래 노드와 연결
            else
            {
                RAFrameUnitDic[_GrantTermID].Add(ConnectedTermID);
            }

        }

        // 노드 좌표 Dictionary에 좌표들을 채우는 함수
        public void SetPointDic()
        {
            int coordX = 200, coordY = 120;
            Point CenterPos = new Point(coordX, coordY);
            int basicLength = 140;

            // 센터 노드 배치
            NodePositionDic.Add(0, CenterPos);

            // 1 ~ 6 노드 배치(센터로 부터 거리는 basicLength 만큼)
            NodePositionDic.Add(1, new Point(coordX - basicLength / 2, coordY + basicLength * Math.Sin(60))); // 노드 1
            NodePositionDic.Add(2, new Point(coordX + basicLength / 2, coordY + basicLength * Math.Sin(60))); // 노드 2
            NodePositionDic.Add(3, new Point(coordX + basicLength , coordY)); // 노드 3
            NodePositionDic.Add(4, new Point(coordX + basicLength / 2, coordY - basicLength * Math.Sin(60))); // 노드 4
            NodePositionDic.Add(5, new Point(coordX - basicLength / 2, coordY - basicLength * Math.Sin(60))); // 노드 5
            NodePositionDic.Add(6, new Point(coordX - basicLength , coordY)); // 노드 6

            // 7 ~ 10 노드 배치(센터로 부터 거리는 basicLength * 2 만큼)
            NodePositionDic.Add(7, new Point(coordX - 3 * basicLength / 2, coordY + basicLength * Math.Sin(60))); // 노드 7
            NodePositionDic.Add(8, new Point(coordX + 3 * basicLength / 2, coordY + basicLength * Math.Sin(60))); // 노드 8
            NodePositionDic.Add(9, new Point(coordX + 3 * basicLength / 2, coordY - basicLength * Math.Sin(60))); // 노드 9
            NodePositionDic.Add(10, new Point(coordX - 3 * basicLength / 2, coordY - basicLength * Math.Sin(60))); // 노드 10

            // 11 ~ 16 노드 배치 (센터로 부터 거리는 basicLength * 2 만큼, 제 2 노드 고려)
            NodePositionDic.Add(11, new Point(coordX, coordY - basicLength * 2 * Math.Sin(60))); // 노드 11
            NodePositionDic.Add(12, new Point(coordX - basicLength, coordY + basicLength * 2 * Math.Sin(60))); // 노드 12
            NodePositionDic.Add(13, new Point(coordX, coordY + basicLength * 2 * Math.Sin(60))); // 노드 13
            NodePositionDic.Add(14, new Point(coordX + basicLength,  coordY + basicLength * 2 * Math.Sin(60))); // 노드 14

        }

        // RA 데이터를 가지고 노드 연결도를 그리기
        private void DrawDiagramFromRAData()
        {
            if (RAFrameUnitDic.Count <= 0)
            {
                return;
            }

            // 단일 Level의 노드 데이터롤 그릴 때
            if (RAFrameUnitDic.Count == 1)
            {
                // 아마 한번 반복할 것임
                foreach (KeyValuePair<int, List<int>> pair in RAFrameUnitDic)
                {

                }
            }
        }

        // 들어온 RA 데이터를 통해서 연결 구축하기
        public void AddConnection(int keyNode, int ConnectedNode, bool IsFirstRcv)
        {
            // 최초 데이터 수신 시시작 노드 그려주기
            if ( IsFirstRcv)
            {
                AddNode(keyNode);
            }
            AddNode(ConnectedNode);

            // To Do :: 선 긋기
        }

        // 노드 좌표 Dictionary에 좌표들을 채우는 함수 ver 2
        public void SetPointDic2()
        {
            int coordX = 200, coordY = 120;
            Point CenterPos = new Point(coordX, coordY);
            int basicLength = 120;

            // 센터 노드 배치
            NodePositionDic.Add(0, CenterPos);

            // 1 ~ 6 노드 배치(센터로 부터 거리는 basicLength 만큼)
            NodePositionDic.Add(1, new Point(coordX - basicLength * Math.Sin(45), coordY + basicLength * Math.Sin(45))); // 노드 1
            NodePositionDic.Add(2, new Point(coordX + basicLength * Math.Sin(45), coordY + basicLength * Math.Sin(45))); // 노드 2
            NodePositionDic.Add(3, new Point(coordX + basicLength, coordY)); // 노드 3
            NodePositionDic.Add(4, new Point(coordX + basicLength * Math.Sin(45), coordY - basicLength * Math.Sin(45))); // 노드 4
            NodePositionDic.Add(5, new Point(coordX - basicLength * Math.Sin(45), coordY - basicLength * Math.Sin(45))); // 노드 5
            NodePositionDic.Add(6, new Point(coordX - basicLength, coordY)); // 노드 6

            // 7 ~ 10 노드 배치(센터로 부터 거리는 basicLength * 2 만큼)
            NodePositionDic.Add(7, new Point(coordX - 3 * Math.Sin(45), coordY + basicLength * Math.Sin(45))); // 노드 7
            NodePositionDic.Add(8, new Point(coordX + 3 * Math.Sin(45), coordY + basicLength * Math.Sin(45))); // 노드 8
            NodePositionDic.Add(9, new Point(coordX + 3 * Math.Sin(45), coordY - basicLength * Math.Sin(45))); // 노드 9
            NodePositionDic.Add(10, new Point(coordX - 3 * Math.Sin(45), coordY - basicLength * Math.Sin(45))); // 노드 10

            // 11 ~ 16 노드 배치 (센터로 부터 거리는 basicLength * 2 만큼, 제 2 노드 고려)
            NodePositionDic.Add(11, new Point(coordX, coordY - basicLength * 2 * Math.Sin(60))); // 노드 11
            NodePositionDic.Add(12, new Point(coordX - basicLength, coordY + basicLength * 2 * Math.Sin(60))); // 노드 12
            NodePositionDic.Add(13, new Point(coordX, coordY + basicLength * 2 * Math.Sin(60))); // 노드 13
            NodePositionDic.Add(14, new Point(coordX + basicLength, coordY + basicLength * 2 * Math.Sin(60))); // 노드 14

        }

        public Point getEllipseCenter(Ellipse ellipse)
        {
            Point pt = new Point();
            double top = Canvas.GetTop(ellipse);
            double left = Canvas.GetLeft(ellipse);

            pt.Y = top + ConstValue.EllipseHeight / 2;
            pt.X = left + ConstValue.EllipseWidth / 2;
            return pt;
        }

        public void CreateLine(Point p1, Point p2)
        {
            Line line = new Line();

            LineCutter(ref p1, ref p2);

            // 중복 라인 확인
            foreach (Line iterator_line in LineList)
            {
                if (iterator_line.X1 == p1.X && iterator_line.Y1 == p1.Y && iterator_line.X2 == p2.X && iterator_line.Y2 == p2.Y
                    || iterator_line.X2 == p1.X && iterator_line.Y2 == p1.Y && iterator_line.X1 == p2.X && iterator_line.Y1 == p2.Y)
                {
                    return;
                }
            }

            //첫번째 좌표 설정
            line.X1 = p1.X;
            line.Y1 = p1.Y;

            //두번째 좌표 설정
            line.X2 = p2.X;
            line.Y2 = p2.Y;

            SolidColorBrush brush = new SolidColorBrush(Colors.Blue);
            line.Stroke = brush;//선색 지정
            line.StrokeThickness = 3;//선 두께 지정
            line.Opacity = .7; // 투명도

            this.canvas.Children.Add(line);
            LineList.Add(line);
            numOfLine++;
        }

        // 서로 연결 할 때 중점을 연결하지 않고, 타원의 외곽에 한 점에서 이어지는 듯 하게 만들기 위한 함수
        public void LineCutter(ref Point p1, ref Point p2)
        {
            const double absValue = 2.2;

            double diffX = Math.Abs(p1.X - p2.X);
            double diffY = Math.Abs(p1.Y - p2.Y);

            double ratioX = diffX / (diffX + diffY);
            double ratioY = diffY / (diffX + diffY);

            if ((p1.X > p2.X) && (p1.Y > p2.Y))
            {
                p1.X = p1.X - ConstValue.EllipseRadius * ratioX - absValue;
                p2.X = p2.X + ConstValue.EllipseRadius * ratioX + absValue;

                p1.Y = p1.Y - ConstValue.EllipseRadius * ratioY - absValue;
                p2.Y = p2.Y + ConstValue.EllipseRadius * ratioY + absValue;
            }

            else if ((p1.X < p2.X) && (p1.Y > p2.Y))
            {
                p1.X = p1.X + ConstValue.EllipseRadius * ratioX + absValue;
                p2.X = p2.X - ConstValue.EllipseRadius * ratioX - absValue;

                p1.Y = p1.Y - ConstValue.EllipseRadius * ratioY - absValue;
                p2.Y = p2.Y + ConstValue.EllipseRadius * ratioY + absValue;
            }

            else if ((p1.X > p2.X) && (p1.Y < p2.Y))
            {
                p1.X = p1.X - ConstValue.EllipseRadius * ratioX - absValue;
                p2.X = p2.X + ConstValue.EllipseRadius * ratioX + absValue;

                p1.Y = p1.Y + ConstValue.EllipseRadius * ratioY + absValue;
                p2.Y = p2.Y - ConstValue.EllipseRadius * ratioY - absValue;
            }

            else if ((p1.X < p2.X) && (p1.Y < p2.Y))
            {
                p1.X = p1.X + ConstValue.EllipseRadius * ratioX + absValue;
                p2.X = p2.X - ConstValue.EllipseRadius * ratioX - absValue;

                p1.Y = p1.Y + ConstValue.EllipseRadius * ratioY + absValue;
                p2.Y = p2.Y - ConstValue.EllipseRadius * ratioY - absValue;
            }

            // 두 좌표 중 한 좌표가 같은 경우에 대한 처리
            else if (p1.X == p2.X)
            {
                if (p1.Y < p2.Y)
                {
                    p1.Y = p1.Y + ConstValue.EllipseRadius;
                    p2.Y = p2.Y - ConstValue.EllipseRadius;
                }

                else if (p1.Y < p2.Y)
                {
                    p1.Y = p1.Y - ConstValue.EllipseRadius;
                    p2.Y = p2.Y + ConstValue.EllipseRadius;
                }
            }

            else if (p1.Y == p2.Y)
            {
                if (p1.X < p2.X)
                {
                    p1.X = p1.X + ConstValue.EllipseRadius;
                    p2.X = p2.X - ConstValue.EllipseRadius;
                }

                else if (p1.X < p2.X)
                {
                    p1.X = p1.X - ConstValue.EllipseRadius;
                    p2.X = p2.X + ConstValue.EllipseRadius;
                }
            }
        }

        public void DeleteLine(Point p1, Point p2)
        {
            LineCutter(ref p1, ref p2);
            Line removedLine = LineList.Find(x => x.X1 == p1.X && x.Y1 == p1.Y && x.X2 == p2.X && x.Y2 == p2.Y);
            this.canvas.Children.Remove(removedLine);
            LineList.Remove(removedLine);
        }
        
        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(NodeToTextBox.Text) > 0 || int.Parse(NodeFromTextBox.Text) > 0)
            {
                // i 보다 큰 노드 번호는 ㄴㄴ
                if (nodeIndex < int.Parse(NodeFromTextBox.Text) || nodeIndex < int.Parse(NodeToTextBox.Text))
                {
                    return;
                }

                string fromNodeName = "Node_" + NodeFromTextBox.Text;
                string toNodeName = "Node_" + NodeToTextBox.Text;

                Ellipse fromNode = EllipseList.Find(x => x.Name.Contains(fromNodeName));
                Ellipse toNode = EllipseList.Find(x => x.Name.Contains(toNodeName));

                Point p1 = getEllipseCenter(fromNode);
                Point p2 = getEllipseCenter(toNode);

                CreateLine(p1, p2);
            }
        }

        private void DeleteLineButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(NodeToTextBox.Text) > 0 || int.Parse(NodeFromTextBox.Text) > 0)
            {
                // i 보다 큰 노드 번호는 ㄴㄴ
                if (nodeIndex < int.Parse(NodeFromTextBox.Text) || nodeIndex < int.Parse(NodeToTextBox.Text))
                {
                    return;
                }

                string fromNodeName = "Node_" + NodeFromTextBox.Text;
                string toNodeName = "Node_" + NodeToTextBox.Text;

                Ellipse fromNode = EllipseList.Find(x => x.Name.Contains(fromNodeName));
                Ellipse toNode = EllipseList.Find(x => x.Name.Contains(toNodeName));

                Point p1 = getEllipseCenter(fromNode);
                Point p2 = getEllipseCenter(toNode);

                DeleteLine(p1, p2);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Line line in LineList)
            {
                this.canvas.Children.Remove(line);
            }

            foreach (TextBlock tb in TextBlockList)
            {
                this.canvas.Children.Remove(tb);
            }
            foreach (Ellipse ellipse in EllipseList)
            {
                this.canvas.Children.Remove(ellipse);
            }

            foreach (NetworkNode node in nodes)
            {
                if (node != null)
                {
                    this.canvas.Children.Remove(node.ellipse);
                }
            }

            LineList.Clear();
            TextBlockList.Clear();
            nodeIndex = 1;

        }

        // 모든 노드를 Canvas에 세팅
        public void AddAllNode()
        {
            for ( int i = 0; i < 15; i++)
            {
                AddNode();
            }
        }


        /// <summary>
        /// 노드를 추가함
        /// </summary>
        /// <param name="termID">기존 노드(키값)</param>
        /// <param name="ConnectedNode">연결당할 노드</param>
        public void AddNode(int termID)
        {
            // 최초의 노드 생성 및 노드번호 할당
            nodes[nodeIndex] = new NetworkNode(NetworkType.Ground);
            nodes[nodeIndex].nodeNumber = termID;

            // 노드 이름 설정
            nodes[nodeIndex].ellipse.Name = "Node_" + nodeIndex.ToString();

            // 최초 위치 설정
            double coordX = NodePositionDic[nodeIndex - 1].X;
            double coordY = NodePositionDic[nodeIndex - 1].Y;

            // Canvas에 해당 좌표에 타원 설정 및 추가
            Canvas.SetLeft(nodes[nodeIndex].ellipse, coordX);
            Canvas.SetTop(nodes[nodeIndex].ellipse, coordY);
            canvas.Children.Add(nodes[nodeIndex].ellipse);
            EllipseList.Add(nodes[nodeIndex].ellipse);

            // 타원 내에 텍스트박스 설정
            TextBlock textBlock = new TextBlock();

            textBlock.Text = nodeIndex.ToString() + "번";
            Canvas.SetLeft(textBlock, coordX + 10);
            Canvas.SetTop(textBlock, coordY + 10);
            TextBlockList.Add(textBlock);

            canvas.Children.Add(textBlock);
            nodeIndex++;
        }

        public void AddNode()
        {
            if (nodeIndex > ConstValue.MaxNode || nodeIndex == 0) return;

            // 노드에 타원 생성 및 노드번호 할당
            nodes[nodeIndex].ellipse = new Ellipse { Width = ConstValue.EllipseWidth, Height = ConstValue.EllipseHeight };
            nodes[nodeIndex].nodeNumber = nodeIndex;

            // 노드 이름 설정
            nodes[nodeIndex].ellipse.Name = "Node_" + nodeIndex.ToString();
            
            double coordX = NodePositionDic[nodeIndex - 1].X;
            double coordY = NodePositionDic[nodeIndex - 1].Y;

            // Canvas에 해당 좌표에 타원 설정 및 추가
            Canvas.SetLeft(nodes[nodeIndex].ellipse, coordX);
            Canvas.SetTop(nodes[nodeIndex].ellipse, coordY);
            canvas.Children.Add(nodes[nodeIndex].ellipse);
            EllipseList.Add(nodes[nodeIndex].ellipse);

            // 타원 내에 텍스트박스 설정
            TextBlock textBlock = new TextBlock();

            textBlock.Text = nodeIndex.ToString() + "번";
            Canvas.SetLeft(textBlock, coordX + 10);
            Canvas.SetTop(textBlock, coordY + 10);
            TextBlockList.Add(textBlock);

            nodeIndex++;
            canvas.Children.Add(textBlock);
        }

        private void AddNodeButton_Click(object sender, RoutedEventArgs e)
        {
            AddNode();
        }

        public void EllipseColorConverter(Ellipse _ellipse, SolidColorBrush _brush, int index)
        {
            canvas.Children.Remove(_ellipse);
            canvas.Children.Remove(TextBlockList[index]);

            _ellipse.Fill = _brush;

            double top = Canvas.GetTop(_ellipse);
            double left = Canvas.GetLeft(_ellipse);

            Canvas.SetLeft(_ellipse, left);
            Canvas.SetTop(_ellipse, top);
            canvas.Children.Add(_ellipse);

            Canvas.SetLeft(TextBlockList[index], left + 10);
            Canvas.SetTop(TextBlockList[index], top + 10);
            canvas.Children.Add(TextBlockList[index]);
        }

        // 해당 노드의 색깔 바꾸기
        private void OnOffButton_Click(object sender, RoutedEventArgs e)
        {
            int targetNodeNum = int.Parse(NodeOnOffTextBox.Text);
            if (targetNodeNum < 0 && targetNodeNum > nodeIndex)
            {
                return;
            }
            
            SolidColorBrush brush;
            if (nodes[targetNodeNum].networkConnectionState)
            {
                nodes[targetNodeNum].networkConnectionState = false;
                brush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                nodes[targetNodeNum].networkConnectionState = true;
                brush = new SolidColorBrush(Colors.Green);
            }

            EllipseColorConverter(nodes[targetNodeNum].ellipse, brush, targetNodeNum-1);
        }

        private void canvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point p = Mouse.GetPosition(canvas);
            p.X += canvas.Margin.Left;
            p.Y += canvas.Margin.Right;

            Ellipse ellipse = new Ellipse();

            Canvas.SetLeft(ellipse, p.X);
            Canvas.SetTop(ellipse, p.Y);
            canvas.Children.Add(ellipse);
        }
        //Ellipse CreateEllipse(double desiredCenterX, double desiredCenterY)
        //{
        //    Ellipse ellipse = new Ellipse { Width = EllipseWidth, Height = EllipseHeight };
        //    double left = desiredCenterX - (EllipseWidth / 2);
        //    double top = desiredCenterY - (EllipseHeight / 2);

        //    ellipse.Margin = new Thickness(left, top, 0, 0);
        //    return ellipse;
        //}


        // 네트워크 노드 정보들
    }
    
    public class NetworkNode
    {
        // 네트워크 연결 상태,  연결 = true
        public bool networkConnectionState = true;
        public NetworkType networkType { get; set; }
        public int nodeNumber;

        public Ellipse ellipse;
        public List<int> ConnectedNode;

        public NetworkNode()
        {

        }
        public NetworkNode(NetworkType type)
        {
            networkType = type;
            this.ellipse = new Ellipse { Width = ConstValue.EllipseWidth, Height = ConstValue.EllipseHeight };
            ConnectedNode = new List<int>();
        }
    }

    // 네트워크 타입 정의, 지상 및 탑재형
    public enum NetworkType
    {
        // 지상 2개 탑재 14개
        Ground, Mounting, None
    };
}
