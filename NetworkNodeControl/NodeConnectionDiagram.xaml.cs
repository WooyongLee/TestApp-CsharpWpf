using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GCS_UI.Interlinked.DLP
{
    // 19. 4. 17 LWY :: 노드 연결도에 들어갈 UserControl 생성
    /// <summary>
    /// NodeConnectionDiagram.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NodeConnectionDiagram : UserControl
    {


        // 그리는 노드 크기 정보
        private const int EllipseRadius = 20; 
        private const int EllipseWidth = 40; 
        private const int EllipseHeight = 40;

        // 최대 노드의 갯수
        const int MaxNode = 16;

        private int numOfLine = 0; // 선의 갯수
        private int nodeIndex = 0; // 현재 바라보는 노드의 인덱스 

        // 노드의 위치를 저장하는 Dictionary
        // key : 노드의 인덱스, value : 노드의 위치(x, y)
        public Dictionary<int, Point> NodePositionDic;

        // List 배열 객체
        public List<Line> LineList;

        // Wrapper로 부터 받은 데이터를 노드를 그릴 수 있도록 만들어 줄 객체 선언
        public NodeData ResAllocNodeData;

        // 생성자
        public NodeConnectionDiagram()
        {
            InitializeComponent();

            ResAllocNodeData = new NodeData();
            NodePositionDic = new Dictionary<int, Point>();

            LineList = new List<Line>();

            this.SetPointDic();
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
            NodePositionDic.Add(1, new Point(coordX - basicLength / 2, coordY + basicLength * (int)Math.Sin(60))); // 노드 1
            NodePositionDic.Add(2, new Point(coordX + basicLength / 2, coordY + basicLength * (int)Math.Sin(60))); // 노드 2
            NodePositionDic.Add(3, new Point(coordX + basicLength, coordY)); // 노드 3
            NodePositionDic.Add(4, new Point(coordX + basicLength / 2, coordY - basicLength * (int)Math.Sin(60))); // 노드 4
            NodePositionDic.Add(5, new Point(coordX - basicLength / 2, coordY - basicLength * (int)Math.Sin(60))); // 노드 5
            NodePositionDic.Add(6, new Point(coordX - basicLength, coordY)); // 노드 6

            // 7 ~ 10 노드 배치(센터로 부터 거리는 basicLength * 2 만큼)
            NodePositionDic.Add(7, new Point(coordX - 3 * basicLength / 2, coordY + basicLength * (int)Math.Sin(60))); // 노드 7
            NodePositionDic.Add(8, new Point(coordX + 3 * basicLength / 2, coordY + basicLength * (int)Math.Sin(60))); // 노드 8
            NodePositionDic.Add(9, new Point(coordX + 3 * basicLength / 2, coordY - basicLength * (int)Math.Sin(60))); // 노드 9
            NodePositionDic.Add(10, new Point(coordX - 3 * basicLength / 2, coordY - basicLength * (int)Math.Sin(60))); // 노드 10

            // 11 ~ 16 노드 배치 (센터로 부터 거리는 basicLength * 2 만큼, 제 2 노드 고려)
            NodePositionDic.Add(11, new Point(coordX, coordY - basicLength * 2 * (int)Math.Sin(60))); // 노드 11
            NodePositionDic.Add(12, new Point(coordX - basicLength, coordY + basicLength * 2 * (int)Math.Sin(60))); // 노드 12
            NodePositionDic.Add(13, new Point(coordX, coordY + basicLength * 2 * (int)Math.Sin(60))); // 노드 13
            NodePositionDic.Add(14, new Point(coordX + basicLength, coordY + basicLength * 2 * (int)Math.Sin(60))); // 노드 14
        }
        
        // 타원의 중점을 구하는 함수
        public static Point GetEllipseCenter(Ellipse ellipse)
        {
            Point pt = new Point();
            int top = (int)Canvas.GetTop(ellipse);
            int left = (int)Canvas.GetLeft(ellipse);

            pt.Y = top + EllipseHeight / 2;
            pt.X = left + EllipseWidth / 2;
            return pt;
        }

        // 서로 연결 할 때 중점을 연결하지 않고, 타원의 외곽에 한 점에서 이어지는 듯 하게 만들기 위한 함수
        public static void LineCutter(ref Point p1, ref Point p2)
        {
            const double absValue = 2.2;

            double diffX = Math.Abs(p1.X - p2.X);
            double diffY = Math.Abs(p1.Y - p2.Y);

            double ratioX = diffX / (diffX + diffY);
            double ratioY = diffY / (diffX + diffY);

            if ((p1.X > p2.X) && (p1.Y > p2.Y))
            {
                p1.X = p1.X - (int)(EllipseRadius * ratioX - absValue);
                p2.X = p2.X + (int)(EllipseRadius * ratioX + absValue);

                p1.Y = p1.Y - (int)(EllipseRadius * ratioY - absValue);
                p2.Y = p2.Y + (int)(EllipseRadius * ratioY + absValue);
            }

            else if ((p1.X < p2.X) && (p1.Y > p2.Y))
            {
                p1.X = p1.X + (int)(EllipseRadius * ratioX + absValue);
                p2.X = p2.X - (int)(EllipseRadius * ratioX - absValue);

                p1.Y = p1.Y - (int)(EllipseRadius * ratioY - absValue);
                p2.Y = p2.Y + (int)(EllipseRadius * ratioY + absValue);
            }

            else if ((p1.X > p2.X) && (p1.Y < p2.Y))
            {
                p1.X = p1.X - (int)(EllipseRadius * ratioX - absValue);
                p2.X = p2.X + (int)(EllipseRadius * ratioX + absValue);

                p1.Y = p1.Y + (int)(EllipseRadius * ratioY + absValue);
                p2.Y = p2.Y - (int)(EllipseRadius * ratioY - absValue);
            }

            else if ((p1.X < p2.X) && (p1.Y < p2.Y))
            {
                p1.X = p1.X + (int)(EllipseRadius * ratioX + absValue);
                p2.X = p2.X - (int)(EllipseRadius * ratioX - absValue);

                p1.Y = p1.Y + (int)(EllipseRadius * ratioY + absValue);
                p2.Y = p2.Y - (int)(EllipseRadius * ratioY - absValue);
            }

            // 두 좌표 중 한 좌표가 같은 경우에 대한 처리
            else if (p1.X == p2.X)
            {
                if (p1.Y < p2.Y)
                {
                    p1.Y = p1.Y + EllipseRadius;
                    p2.Y = p2.Y - EllipseRadius;
                }

                else if (p1.Y < p2.Y)
                {
                    p1.Y = p1.Y - EllipseRadius;
                    p2.Y = p2.Y + EllipseRadius;
                }
            }

            else if (p1.Y == p2.Y)
            {
                if (p1.X < p2.X)
                {
                    p1.X = p1.X + EllipseRadius;
                    p2.X = p2.X - EllipseRadius;
                }

                else if (p1.X < p2.X)
                {
                    p1.X = p1.X - EllipseRadius;
                    p2.X = p2.X + EllipseRadius;
                }
            }
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

        public void DeleteLine(Point p1, Point p2)
        {
            LineCutter(ref p1, ref p2);
            Line removedLine = LineList.Find(x => x.X1 == p1.X && x.Y1 == p1.Y && x.X2 == p2.X && x.Y2 == p2.Y);
            this.canvas.Children.Remove(removedLine);
            LineList.Remove(removedLine);
        }

        public void AddNode()
        {
            if (nodeIndex > MaxNode || nodeIndex == 0) return;

            //if (nodeIndex < 2)
            //{
            //    nodes[nodeIndex] = new NetworkNode(NetworkType.Ground);
            //}
            //else
            //{
            //    nodes[nodeIndex] = new NetworkNode(NetworkType.Mounting);
            //}

            //// 노드에 타원 생성 및 노드번호 할당
            //nodes[nodeIndex].ellipse = new Ellipse { Width = EllipseWidth, Height = EllipseHeight };
            //nodes[nodeIndex].nodeNumber = nodeIndex;

            //// 노드 이름 설정
            //nodes[nodeIndex].ellipse.Name = "Node_" + nodeIndex.ToString();

            ////Ellipse ellipse = new Ellipse { Width = EllipseWidth, Height = EllipseHeight };

            ////ellipse.Name = "Node_" + nodeIndex.ToString();

            //double coordX = NodePositionDic[nodeIndex - 1].X;
            //double coordY = NodePositionDic[nodeIndex - 1].Y;

            //// Canvas에 해당 좌표에 타원 설정 및 추가
            //Canvas.SetLeft(nodes[nodeIndex].ellipse, coordX);
            //Canvas.SetTop(nodes[nodeIndex].ellipse, coordY);
            //canvas.Children.Add(nodes[nodeIndex].ellipse);
            //EllipseList.Add(nodes[nodeIndex].ellipse);

            ////Canvas.SetLeft(ellipse, coordX);
            ////Canvas.SetTop(ellipse, coordY);
            ////canvas.Children.Add(ellipse);
            ////EllipseList.Add(ellipse);

            //// 타원 내에 텍스트박스 설정
            //TextBlock textBlock = new TextBlock();

            //textBlock.Text = nodeIndex.ToString() + "번";
            //Canvas.SetLeft(textBlock, coordX + 10);
            //Canvas.SetTop(textBlock, coordY + 10);
            //TextBlockList.Add(textBlock);

            //nodeIndex++;
            //canvas.Children.Add(textBlock);
        }


    }


    // 연결도 그릴 노드 데이터를 담은 클래스
    public class NodeData
    {
        public List<GrantTermID> ULGrantList { get; set; }
        public NodeData()
        {
            ULGrantList = new List<GrantTermID>();
        }
    }

    public class GrantTermID
    {
        public uint UL_GrantTermID { get; set; }
        public List<DLGrant> DLGrantList { get; set; }

        public GrantTermID()
        {
            DLGrantList = new List<DLGrant>();
        }
    }

    public class DLGrant
    {
        public uint DL_TermID { get; set; }
    }
}
