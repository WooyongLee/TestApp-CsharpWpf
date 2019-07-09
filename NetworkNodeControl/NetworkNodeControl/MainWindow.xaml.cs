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

        public NetworkNode[] Nodes;

        // 노드의 위치를 저장하는 Dictionary
        // key : 노드의 인덱스, value : 노드의 위치(x, y)
        public Dictionary<int, Point> NodePositionDic;


        List<Ellipse> EllipseList;
        List<Line> LineList;
        List<TextBlock> TextBlockList;

        // RA 데이터가 들어오는 상황을 추가 ([UL_GrantTermID] : [DL_TermID List] Pair)
        private Dictionary<int, List<int>> RAFrameUnitDic = new Dictionary<int, List<int>>();

        public MainWindow()
        {
            InitializeComponent();

            Nodes = new NetworkNode[NodeConstValue.MaxNode + 1];

            EllipseList = new List<Ellipse>();
            LineList = new List<Line>();
            TextBlockList = new List<TextBlock>();

            Nodes[nodeIndex] = new NetworkNode(NetworkType.None);

            // 노드 객체 배열에 모든 Item들 생성
            for (int i = 1; i < Nodes.Length; i++)
            {
                Nodes[i] = new NetworkNode();
            }

            NodePositionDic = new Dictionary<int, Point>();

           // this.SetAllPointDic();
           // this.AddAllNode();
        }

        // 절차 : 제어부로부터 WDLN RA 받음 -> DrawDiagramFromRAData() -> AddConnection() ->

        // RA 데이터를 가지고 노드 연결도를 그리기
        private void DrawDiagramFromRAData()
        {
            // 19. 5. 17 변경
            // RA 들어온다는 가정 부분
            if (RAFrameUnitDic.Count <= 0)
            {
                return;
            }

            int iterIndex = 0;
            // 단일 Level의 노드 데이터롤 그릴 때
            // 아마 한번 반복할 것임
            foreach (KeyValuePair<int, List<int>> pair in RAFrameUnitDic)
            {
                bool IsNewKey = true;
                for (int i = 0; i < pair.Value.Count; i++)
                {
                    // Key :: GrantTermID (SourceID), Value :: DL_TermID (ConnectedTermID) // 19. 5. 17 변경 (대부분 삭제)
                    AddConnection(pair.Key, pair.Value[i], IsNewKey);
                    // IsNewKey = false;
                }

                iterIndex++;
            }
        }

        //****
        // 19. 5. 17 SetRANodeData() 삭제
        //****

        // 임시변수
        int posIndex = 0;
        int CenterPosCount = 0;

        // canvas 내에 노드들의 위치를 설정
        public void SetNodePoisitionDic(int nodeLevel, int termID, bool Is1stNode = false)
        {
            // 첫 노드는 center에 배치
            if (Is1stNode)
            {
                NodePositionDic.Add(termID, NodeConstValue.CalcCenterPos(NodeConstValue.CenterPos, CenterPosCount));
                CenterPosCount += 45; // 추후 랜덤하게 변경
            }

            else
            {
                // 같은 레벨에서는 여섯방향 공간을 돌면서 노드를 배치하기
                if (termID < 1) termID = 1;
                nodeLevel = (termID - 1) / 6;
                switch (termID % 12)
                {
                    case 1: NodePositionDic.Add(termID, NodeConstValue.SetNodePos(nodeLevel, ENodeDirection.ThreeToFive)); break;
                    case 2: NodePositionDic.Add(termID, NodeConstValue.SetNodePos(nodeLevel, ENodeDirection.FiveToSeven)); break;
                    case 3: NodePositionDic.Add(termID, NodeConstValue.SetNodePos(nodeLevel, ENodeDirection.SevenToNine)); break;
                    case 4: NodePositionDic.Add(termID, NodeConstValue.SetNodePos(nodeLevel, ENodeDirection.NineToEleven)); break;
                    case 5: NodePositionDic.Add(termID, NodeConstValue.SetNodePos(nodeLevel, ENodeDirection.EleventToOne)); break;
                    case 6: NodePositionDic.Add(termID, NodeConstValue.SetNodePos(nodeLevel, ENodeDirection.OneToThree)); break;

                    case 7: NodePositionDic.Add(termID, NodeConstValue.SetNodePos(nodeLevel, ENodeDirection.TenToTwelve)); break;
                    case 8: NodePositionDic.Add(termID, NodeConstValue.SetNodePos(nodeLevel, ENodeDirection.TwelveToTwo)); break;
                    case 9: NodePositionDic.Add(termID, NodeConstValue.SetNodePos(nodeLevel, ENodeDirection.TwoToFour)); break;
                    case 10: NodePositionDic.Add(termID, NodeConstValue.SetNodePos(nodeLevel, ENodeDirection.FourToSix)); break;
                    case 11: NodePositionDic.Add(termID, NodeConstValue.SetNodePos(nodeLevel, ENodeDirection.SixToEight)); break;
                    case 0: NodePositionDic.Add(termID, NodeConstValue.SetNodePos(nodeLevel, ENodeDirection.EightToTen)); break;
                }
            }
        }

        // 노드 좌표 Dictionary에 모든 좌표들을 고정적으로 채우는 함수
        public void SetAllPointDic()
        {
            int coordX = 200, coordY = 120;
            Point CenterPos = new Point(coordX, coordY);
            int basicLength = 140;

            // 센터 노드 배치
            NodePositionDic.Add(1, CenterPos);

            // 1 ~ 6 노드 배치(센터로 부터 거리는 basicLength 만큼)
            NodePositionDic.Add(2, new Point(coordX - basicLength / 2, coordY + basicLength * Math.Sin(60))); // 노드 1
            NodePositionDic.Add(3, new Point(coordX + basicLength / 2, coordY + basicLength * Math.Sin(60))); // 노드 2
            NodePositionDic.Add(4, new Point(coordX + basicLength , coordY)); // 노드 3
            NodePositionDic.Add(5, new Point(coordX + basicLength / 2, coordY - basicLength * Math.Sin(60))); // 노드 4
            NodePositionDic.Add(6, new Point(coordX - basicLength / 2, coordY - basicLength * Math.Sin(60))); // 노드 5
            NodePositionDic.Add(7, new Point(coordX - basicLength , coordY)); // 노드 6

            // 7 ~ 10 노드 배치(센터로 부터 거리는 basicLength * 2 만큼)
            NodePositionDic.Add(8, new Point(coordX - 3 * basicLength / 2, coordY + basicLength * Math.Sin(60))); // 노드 7
            NodePositionDic.Add(9, new Point(coordX + 3 * basicLength / 2, coordY + basicLength * Math.Sin(60))); // 노드 8
            NodePositionDic.Add(10, new Point(coordX + 3 * basicLength / 2, coordY - basicLength * Math.Sin(60))); // 노드 9
            NodePositionDic.Add(11, new Point(coordX - 3 * basicLength / 2, coordY - basicLength * Math.Sin(60))); // 노드 10

            // 11 ~ 16 노드 배치 (센터로 부터 거리는 basicLength * 2 만큼, 제 2 노드 고려)
            NodePositionDic.Add(12, new Point(coordX, coordY - basicLength * 2 * Math.Sin(60))); // 노드 11
            NodePositionDic.Add(13, new Point(coordX - basicLength, coordY + basicLength * 2 * Math.Sin(60))); // 노드 12
            NodePositionDic.Add(14, new Point(coordX, coordY + basicLength * 2 * Math.Sin(60))); // 노드 13
            NodePositionDic.Add(15, new Point(coordX + basicLength,  coordY + basicLength * 2 * Math.Sin(60))); // 노드 14

        }

        /// <summary>
        /// 들어온 RA 데이터를 통해서 연결 구축하기
        /// </summary>
        /// <param name="keyNodeTermID">키 노드</param>
        /// <param name="connectedNodeTermID">연결될 노드</param>
        /// <param name="IsFirstRcv">키가 첫번째로 들어왔는 지[To Do : 파라미터 명 변경]</param>
        public void AddConnection(int keyNodeTermID, int connectedNodeTermID, bool IsFirstRcv = false)
        {
            // 이미 TermId를 갖고 있는 경우에 처리하기

            // 최초 데이터 수신 시시작 노드 그려주기(중앙노드)
            if ( IsFirstRcv )
            {
                // 이미 추가된 노드가 아닌 경우에
                if (!IsAlreadyNodeArray(keyNodeTermID))
                {
                    // Notice : 노드를 추가하기 전 먼저 Position을 선정함
                    // 가장 중앙 노드를 받는 경우
                    SetNodePoisitionDic(1, keyNodeTermID, true);
                    AddNode(keyNodeTermID);
                }
            }

            // 이미 추가된 노드가 아닌 경우에
            if (!IsAlreadyNodeArray(connectedNodeTermID))
            {
                // 노드 그려주기
                // Notice : 노드를 추가하기 전 먼저 Position을 선정함
                SetNodePoisitionDic(1, connectedNodeTermID);
                AddNode(connectedNodeTermID);
            }

            Point nodeCenterPtOfCentralNode = this.GetEllipseCenter(Nodes[ExploreNodeIndex(keyNodeTermID)].ellipse);
            Point nodeCenterPtOfSourceNode = this.GetEllipseCenter(Nodes[ExploreNodeIndex(connectedNodeTermID)].ellipse);

            // 두 Ellipse 중점 사이에 선 긋기
            CreateLine(nodeCenterPtOfCentralNode, nodeCenterPtOfSourceNode);
        }

        // 들어온 TermID에 의해 이미 그려진 노드가 있는 지 확인하기
        public bool IsAlreadyNodeArray(int nodeTermID)
        {
            foreach (NetworkNode node in Nodes)
            {
                if (node.NodeTermID == nodeTermID)
                {
                    return true;
                }
            }
            return false;
        }

        public void MoveCanvas()
        {
            
        }

        // TermID 를 통해서 Network Node 배열의 인덱스를 반환함
        public int ExploreNodeIndex(int nodeTermID)
        {
            for (int i = 0; i < Nodes.Length; i++)
            {
                if (Nodes[i].NodeTermID == nodeTermID)
                {
                    return i;
                }
            }
            return 0;
        }

        public Point GetEllipseCenter(Ellipse ellipse)
        {
            Point pt = new Point();
            double top = Canvas.GetTop(ellipse);
            double left = Canvas.GetLeft(ellipse);

            pt.Y = top + NodeConstValue.EllipseHeight / 2;
            pt.X = left + NodeConstValue.EllipseWidth / 2;
            return pt;
        }

        public void CreateLine(Point p1, Point p2)
        {
            Line line = new Line();

            // Nan 값 확인 후 연산
            if (double.IsNaN(p1.X) || double.IsNaN(p2.X) || double.IsNaN(p1.Y) || double.IsNaN(p2.Y))
            {
                return;
            }

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
                p1.X = p1.X - NodeConstValue.EllipseRadius * ratioX - absValue;
                p2.X = p2.X + NodeConstValue.EllipseRadius * ratioX + absValue;

                p1.Y = p1.Y - NodeConstValue.EllipseRadius * ratioY - absValue;
                p2.Y = p2.Y + NodeConstValue.EllipseRadius * ratioY + absValue;
            }

            else if ((p1.X < p2.X) && (p1.Y > p2.Y))
            {
                p1.X = p1.X + NodeConstValue.EllipseRadius * ratioX + absValue;
                p2.X = p2.X - NodeConstValue.EllipseRadius * ratioX - absValue;

                p1.Y = p1.Y - NodeConstValue.EllipseRadius * ratioY - absValue;
                p2.Y = p2.Y + NodeConstValue.EllipseRadius * ratioY + absValue;
            }

            else if ((p1.X > p2.X) && (p1.Y < p2.Y))
            {
                p1.X = p1.X - NodeConstValue.EllipseRadius * ratioX - absValue;
                p2.X = p2.X + NodeConstValue.EllipseRadius * ratioX + absValue;

                p1.Y = p1.Y + NodeConstValue.EllipseRadius * ratioY + absValue;
                p2.Y = p2.Y - NodeConstValue.EllipseRadius * ratioY - absValue;
            }

            else if ((p1.X < p2.X) && (p1.Y < p2.Y))
            {
                p1.X = p1.X + NodeConstValue.EllipseRadius * ratioX + absValue;
                p2.X = p2.X - NodeConstValue.EllipseRadius * ratioX - absValue;

                p1.Y = p1.Y + NodeConstValue.EllipseRadius * ratioY + absValue;
                p2.Y = p2.Y - NodeConstValue.EllipseRadius * ratioY - absValue;
            }

            // 두 좌표 중 한 좌표가 같은 경우에 대한 처리
            else if (p1.X == p2.X)
            {
                if (p1.Y < p2.Y)
                {
                    p1.Y = p1.Y + NodeConstValue.EllipseRadius;
                    p2.Y = p2.Y - NodeConstValue.EllipseRadius;
                }

                else if (p1.Y > p2.Y)
                {
                    p1.Y = p1.Y - NodeConstValue.EllipseRadius;
                    p2.Y = p2.Y + NodeConstValue.EllipseRadius;
                }
            }

            else if (p1.Y == p2.Y)
            {
                if (p1.X < p2.X)
                {
                    p1.X = p1.X + NodeConstValue.EllipseRadius;
                    p2.X = p2.X - NodeConstValue.EllipseRadius;
                }

                else if (p1.X > p2.X)
                {
                    p1.X = p1.X - NodeConstValue.EllipseRadius;
                    p2.X = p2.X + NodeConstValue.EllipseRadius;
                }
            }
        }

        public void DeleteLine(Point p1, Point p2)
        {
            LineCutter(ref p1, ref p2);
            Line removedLine = LineList.Find(x => x.X1 == p1.X && x.Y1 == p1.Y && x.X2 == p2.X && x.Y2 == p2.Y);
            Dispatcher.Invoke(new Action(() =>
            {
                this.canvas.Children.Remove(removedLine);
            }));
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

                Point p1 = GetEllipseCenter(fromNode);
                Point p2 = GetEllipseCenter(toNode);

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

                Point p1 = GetEllipseCenter(fromNode);
                Point p2 = GetEllipseCenter(toNode);

                DeleteLine(p1, p2);
            }
        }

        public void ClearAllNode()
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

            foreach (NetworkNode node in Nodes)
            {
                if (node != null)
                {
                    this.canvas.Children.Remove(node.ellipse);
                }
            }

            for (int i = 0; i < Nodes.Length; i++)
            {
                Nodes[i] = null;
            }

            LineList.Clear();
            TextBlockList.Clear();
            nodeIndex = 1;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearAllNode();
        }

        // 모든 노드를 Canvas에 세팅
        public void AddAllNode()
        {
            for ( int i = 0; i < 15; i++)
            {
                AddNode(i);
            }
        }

        ////노드를 추가함
        //public void AddNode(int termID)
        //{
        //    // 최초의 노드 생성 및 노드번호 할당
        //    Nodes[nodeIndex] = new NetworkNode(NetworkType.Ground);
        //    Nodes[nodeIndex].NodeTermID = termID;

        //    // 노드 이름 설정
        //    Nodes[nodeIndex].ellipse.Name = "Node_" + nodeIndex.ToString();

        //    // 최초 위치 설정
        //    double coordX = NodePositionDic[nodeIndex - 1].X;
        //    double coordY = NodePositionDic[nodeIndex - 1].Y;

        //    // Canvas에 해당 좌표에 타원 설정 및 추가
        //    Canvas.SetLeft(Nodes[nodeIndex].ellipse, coordX);
        //    Canvas.SetTop(Nodes[nodeIndex].ellipse, coordY);
        //    canvas.Children.Add(Nodes[nodeIndex].ellipse);
        //    EllipseList.Add(Nodes[nodeIndex].ellipse);

        //    // 타원 내에 텍스트박스 설정
        //    TextBlock textBlock = new TextBlock();

        //    textBlock.Text = nodeIndex.ToString() + "번";
        //    Canvas.SetLeft(textBlock, coordX + 10);
        //    Canvas.SetTop(textBlock, coordY + 10);
        //    TextBlockList.Add(textBlock);

        //    canvas.Children.Add(textBlock);
        //    nodeIndex++;
        //}

        /// <summary>
        /// 노드를 Canvas에 추가함
        /// </summary>
        /// <param name="termID">Node의 Term ID</param>
        /// <returns>Network Node 데이터</returns>
        public NetworkNode AddNode(int termID)
        {
            if (nodeIndex >= NodeConstValue.MaxNode) return new NetworkNode();

            NetworkNode node = Nodes[nodeIndex];

            double coordX = NodePositionDic[nodeIndex].X;
            double coordY = NodePositionDic[nodeIndex].Y;


            // 노드 번호 할당 및 이름 설정
            node.NodeTermID = termID;
            node.ellipse.Name = "Node_" + nodeIndex.ToString();

            // 타원 생성
            node.ellipse = new Ellipse { Width = NodeConstValue.EllipseWidth, Height = NodeConstValue.EllipseHeight };

            // Canvas에 해당 좌표에 타원 설정 및 추가
            Canvas.SetLeft(node.ellipse, coordX);
            Canvas.SetTop(node.ellipse, coordY);
            canvas.Children.Add(node.ellipse);
            EllipseList.Add(node.ellipse);

            // 타원 내에 텍스트박스 설정
            node.textBlock.Text = termID.ToString() + "번";
            TextBlock textBlock = node.textBlock;

            Canvas.SetLeft(textBlock, coordX + 10);
            Canvas.SetTop(textBlock, coordY + 10);
            canvas.Children.Add(textBlock);
            TextBlockList.Add(textBlock);


            nodeIndex++;
            return node;
        }

        private void AddNodeButton_Click(object sender, RoutedEventArgs e)
        {
            // AddNode();
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
            if (Nodes[targetNodeNum].networkConnectionState)
            {
                Nodes[targetNodeNum].networkConnectionState = false;
                brush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                Nodes[targetNodeNum].networkConnectionState = true;
                brush = new SolidColorBrush(Colors.Green);
            }

            EllipseColorConverter(Nodes[targetNodeNum].ellipse, brush, targetNodeNum-1);
        }

        //private void canvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    Point p = Mouse.GetPosition(canvas);
        //    p.X += canvas.Margin.Left;
        //    p.Y += canvas.Margin.Right;

        //    Ellipse ellipse = new Ellipse();

        //    Canvas.SetLeft(ellipse, p.X);
        //    Canvas.SetTop(ellipse, p.Y);
        //    canvas.Children.Add(ellipse);
        //}

        private void ApplyRAData_Click(object sender, RoutedEventArgs e)
        {
            //int UL_GrantTermID = int.Parse(GrantTermIDTextBox.Text.ToString());
            //int Connected_TermID = int.Parse(ConnectedTermIDTextBox.Text.ToString());

            //this.SetRANodeData(UL_GrantTermID, Connected_TermID );
        }


        #region Canvas 내부 마우스 컨트롤 - 사용자가 노드를 옮길 수 있게 함
        bool activated;
        Point point;

        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            activated = false;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            activated = true;
            point = e.GetPosition(CanvasContainerGrid);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (activated)
            {
                // 변환 좌표로 개체를 이동
                translate.X = e.GetPosition(CanvasContainerGrid).X - point.X;
                translate.Y = e.GetPosition(CanvasContainerGrid).Y - point.Y;
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            activated = false;
        }
        #endregion

        #region 상황 발생기
        // 상황을 가정하기
        private void Situation1Btn_Click(object sender, RoutedEventArgs e)
        {
            CenterPosCount = 0;

            // 단일 Size의 RAFrame이 들어올 경우 가정
            List<int> ConnectedTermIDList = new List<int>();
            for ( int i = 0 ; i < 15 ; i++)
            {
                int termID = i + 1;
                ConnectedTermIDList.Add(termID);
            }

            // UL_Grant TermID에 0번 , Connected TermID에 1, 2, 3, 4, 5번까지 대입
            RAFrameUnitDic.Add(0, ConnectedTermIDList);

            DrawDiagramFromRAData();
        }

        private void Situation2Btn_Click(object sender, RoutedEventArgs e)
        {
            CenterPosCount = 0;

            // Size = 2의 RAFrame이 들어올 경우를 가정
            List<int> ConnectedTermIDList = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                int termID = i + 1;
                ConnectedTermIDList.Add(termID);
            }

            // UL_Grant TermID에 0번 , Connected TermID에 1, 2, 3번까지 대입
            RAFrameUnitDic.Add(0, ConnectedTermIDList);

            List<int> ConnectedTermIDList2 = new List<int>();
            for (int i = 0; i < 2; i++)
            {
                int termID = i + 5;
                ConnectedTermIDList2.Add(termID);
            }

            // UL_Grant TermID에 4번 , Connected TermID에 5, 6번까지 대입
            RAFrameUnitDic.Add(4, ConnectedTermIDList2);

            // 2번 노드가 5, 6에 맞물리게 됨
            DrawDiagramFromRAData();
        }

        private void Situation3Btn_Click(object sender, RoutedEventArgs e)
        {
            // Size = 2의 RAFrame이 들어올 경우를 가정
            List<int> ConnectedTermIDList = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                int termID = i + 2;
                ConnectedTermIDList.Add(termID);
            }

            // UL_Grant TermID에 1번 , Connected TermID에 2, 3, 4번까지 대입
            RAFrameUnitDic.Add(1, ConnectedTermIDList);

            List<int> ConnectedTermIDList2 = new List<int>();
            for (int i = 0; i < 2; i++)
            {
                int termID = i + 4;
                ConnectedTermIDList2.Add(termID);
            }

            // UL_Grant TermID에 2번 , Connected TermID에 4, 5번까지 대입
            RAFrameUnitDic.Add(2, ConnectedTermIDList2);

            // 2번 노드가 4, 5에 맞물리게 됨, 4는 2와 같은 레벨의 노드임

            DrawDiagramFromRAData();
        }

        private void Situation4Btn_Click(object sender, RoutedEventArgs e)
        {
            // Size = 2의 RAFrame이 들어올 경우를 가정
            List<int> ConnectedTermIDList = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                int termID = i + 3;
                ConnectedTermIDList.Add(termID);
            }

            // UL_Grant TermID에 1번 , Connected TermID에 3, 4, 5번까지 대입
            RAFrameUnitDic.Add(1, ConnectedTermIDList);

            List<int> ConnectedTermIDList2 = new List<int>();
            for (int i = 0; i < 2; i++)
            {
                int termID = i + 5;
                ConnectedTermIDList2.Add(termID);
            }

            // UL_Grant TermID에 2번 , Connected TermID에 6, 7번까지 대입
            RAFrameUnitDic.Add(2, ConnectedTermIDList2);

            // 1, 2 노드가 서로 독립되어있음

            DrawDiagramFromRAData();
        }
        #endregion

        // 모든 RA 데이터들 리셋하기
        private void ResetRAData_Click(object sender, RoutedEventArgs e)
        {
            // Canvas에 그려진 개체들 모두 지우기
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

            foreach (NetworkNode node in Nodes)
            {
                if (node != null)
                {
                    this.canvas.Children.Remove(node.ellipse);
                    this.canvas.Children.Remove(node.textBlock);
                }
            }

            // 모든 노드 TermID 초기화
            for (int i = 0; i < Nodes.Length; i++)
            {
                Nodes[i].NodeTermID = -1;
            }

            // 모든 List Clear
            LineList.Clear();
            EllipseList.Clear();
            TextBlockList.Clear();
            
            // 인덱스 제자리로
            nodeIndex = 0; // 19. 5. 17 변경

            // 상태변수 초기화

            // RA Frame Dictionary / Node Position Dictionary 초기화
            RAFrameUnitDic.Clear();
            NodePositionDic.Clear();
        }
    }

    // 네트워크 노드 정보들을 담은 클래스
    public class NetworkNode
    {
        // 네트워크 연결 상태,  연결 = true
        public bool networkConnectionState = true;

        // 네트워크 타입
        public NetworkType networkType { get; set; }

        // 노드의 TermID
        public int NodeTermID { get; set; }

        // 타원
        public Ellipse ellipse;

        // 타원에 찍힐 번호를 보여주는 TextBlock
        public TextBlock textBlock;

        // 연결된 노드의 번호 리스트
        public List<int> ConnectedNode;

        public NetworkNode()
        {
            this.NodeTermID = -1;
            this.ellipse = new Ellipse { Width = NodeConstValue.EllipseWidth, Height = NodeConstValue.EllipseHeight };
            this.textBlock = new TextBlock();
        }
        public NetworkNode(NetworkType type)
        {
            networkType = type;
            this.NodeTermID = -1;
            this.ellipse = new Ellipse { Width = NodeConstValue.EllipseWidth, Height = NodeConstValue.EllipseHeight };
            this.textBlock = new TextBlock();
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
