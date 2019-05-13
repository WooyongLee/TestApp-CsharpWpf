using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetworkNodeControl
{
    public static class NodeConstValue
    {
        public readonly static int EllipseRadius = 20;
        public readonly static int EllipseWidth = 40;
        public readonly static int EllipseHeight = 40;

        public readonly static int numOfMountingNode = 14;
        public readonly static int numOfGroundNode = 2;
        public readonly static int MaxNode = 16;

        public readonly static int CenterCoordX = 200;
        public readonly static int CenterCoordY = 120;
        public readonly static Point CenterPos = new Point(CenterCoordX, CenterCoordY);

        public readonly static int LineLength = 140;

        public readonly static double Degree180 = (Math.PI);
        public readonly static double Degree120 = (Math.PI / 3 * 2);
        public readonly static double Degree90 = (Math.PI / 2);
        public readonly static double Degree60 = (Math.PI / 3);
        public readonly static double Degree30 = (Math.PI / 6);

        // 해당하는 노드의 방향으로 위치 설정
        public static Point SetNodePos(int Depth, ENodeDirection eNodeDirection)
        {
            // 랜덤 객체 생성
            Random rDegree = new Random();

            // 0 ~ 1 사이 랜덤 값
            double rDouble = rDegree.NextDouble();

            // 60 - 120도 사이의 임의의 수 구하기
            double rDegree120Num = rDegree.NextDouble() * (Degree120 + Degree60);

            // 0 - 60도 사이의 임의의 수 구하기
            double rDegree60Num = rDegree.NextDouble() * Degree60;

            if (rDegree120Num < 0.15)
            {
                rDouble += 0.15;
            }

            switch (eNodeDirection)
            {
            

                case ENodeDirection.LeftTop:
                    return new Point(CenterPos.X - LineLength * Math.Cos(rDegree60Num), CenterPos.Y - LineLength * Math.Sin(rDegree60Num));

                case ENodeDirection.RightTop:
                    return new Point(CenterPos.X + LineLength * (rDouble-0.5), CenterPos.Y - LineLength * Math.Sin(rDegree120Num));

                case ENodeDirection.Right:
                    return new Point(CenterPos.X + LineLength * Math.Cos(rDegree60Num), CenterPos.Y - LineLength * Math.Sin(rDegree60Num));

                case ENodeDirection.RightBottom:
                    return new Point(CenterPos.X + LineLength * Math.Cos(rDegree60Num), CenterPos.Y + LineLength * Math.Sin(rDegree60Num));

                case ENodeDirection.LeftBottom:
                    return new Point(CenterPos.X + LineLength * (rDouble - 0.5), CenterPos.Y +  LineLength * Math.Sin(rDegree120Num));

                case ENodeDirection.Left:
                    return new Point(CenterPos.X - LineLength * Math.Cos(rDegree60Num), CenterPos.Y + LineLength * Math.Sin(rDegree60Num));
            }


            return new Point(CenterPos.X - LineLength * Math.Cos(rDegree60Num), CenterPos.Y - LineLength * Math.Sin(rDegree60Num));
        }
    }

    public enum ENodeDirection
    { 
        Center = 0, 
        LeftTop = 1, // 9 - 11시 방향 
        RightTop = 2, // 11 - 1시 방향
        Right = 3, // 1 - 3시 방향
        RightBottom = 4, // 3 - 5시 방향
        LeftBottom = 5, // 5 - 7시 방향
        Left = 6, // 7 - 9시 방향
    }
}
