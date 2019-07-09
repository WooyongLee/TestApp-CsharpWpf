using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public readonly static double LineLength = 200;

        public readonly static double Degree180 = (Math.PI);
        public readonly static double Degree150 = (Math.PI / 5 * 6);
        public readonly static double Degree120 = (Math.PI / 3 * 2);
        public readonly static double Degree90 = (Math.PI / 2);
        public readonly static double Degree60 = (Math.PI / 3);
        public readonly static double Degree45 = (Math.PI / 4);
        public readonly static double Degree30 = (Math.PI / 6);

        // 19. 7. 9 LWY : 여러개의 CenterPos가 존재할 경우 (ULGrantTermID 2 이상)
        public static Point CalcCenterPos(Point pt, int centerPosCnt)
        {
            pt.X = pt.X + centerPosCnt;
            return pt;
        }

        // 해당하는 노드의 방향으로 위치 설정
        public static Point SetNodePos(int Depth, ENodeDirection eNodeDirection)
        {
            int offset = 25;
            double curLineLength = (double)Depth * LineLength / 3 + LineLength;

            // 특정 범위에서 부호를 결정할 부호변수
            int plusminus = 1;

            int randomLength = 7;
            Random[] rDegree = new Random[randomLength];
            for (int i = 0; i < rDegree.Length; i++)
            {
                rDegree[i] = new Random();
            }

            // 0 ~ 1 사이 랜덤 값
            double rDouble = rDegree[0].NextDouble();
            if (rDouble >= 0.5)
            {
                plusminus = 1;
            }
            else
            {
                plusminus = -1;
            }

            double randOffset = rDegree[0].NextDouble() - 0.5;
            randOffset = randOffset * offset;

            // 60 - 120 도 사이의 임의의 수 구하기
            // double rDegree60To120Num = rDegree[1].NextDouble() * (Degree120 + Degree60);

            // 0 - 60 도 사이의 임의의 수 구하기
            double rDegree0To60Num = rDegree[2].NextDouble() * Degree60;

            // 0 - 45 도 사이의 임의의 수 구하기
            // double rDegree0To45Num = rDegree[3].NextDouble() * Degree45;

            // 30 - 90 도 사이의 임의의 수 구하기
            double rDegree30To90Num = rDegree[4].NextDouble() * Degree30;

            Thread.Sleep(23);

            switch (eNodeDirection)
            {
                // 9 - 11 시 ~ 7 - 9 시 방향
                case ENodeDirection.NineToEleven:
                    return new Point(CenterPos.X - curLineLength * Math.Cos(Degree60) / 2 + randOffset, CenterPos.Y + curLineLength * Math.Sin(Degree60) / 2 + randOffset);

                case ENodeDirection.EleventToOne:
                    return new Point(CenterPos.X + randOffset, CenterPos.Y + (curLineLength / 2) + randOffset);

                case ENodeDirection.OneToThree:
                    return new Point(CenterPos.X + curLineLength * Math.Cos(Degree60) / 2 + randOffset, CenterPos.Y + curLineLength * Math.Sin(Degree60) / 2 + randOffset);

                case ENodeDirection.ThreeToFive:
                    return new Point(CenterPos.X + curLineLength * Math.Cos(Degree60) / 2 + randOffset, CenterPos.Y - curLineLength * Math.Sin(Degree60) / 2 + randOffset);

                case ENodeDirection.FiveToSeven:
                    return new Point(CenterPos.X + randOffset, CenterPos.Y - (curLineLength / 2) + randOffset);

                case ENodeDirection.SevenToNine:
                    return new Point(CenterPos.X - curLineLength * Math.Cos(Degree60) / 2 + randOffset, CenterPos.Y - curLineLength * Math.Sin(Degree60) / 2 + randOffset);

                // 10 - 12 시 ~ 8 - 10 시 방향
                case ENodeDirection.SixToEight:
                    return new Point(CenterPos.X - curLineLength * Math.Cos(Degree30) / 2 + randOffset, CenterPos.Y - curLineLength * Math.Sin(Degree30) / 2 + randOffset);

                case ENodeDirection.FourToSix:
                    return new Point(CenterPos.X + curLineLength * Math.Cos(Degree30) / 2 + randOffset, CenterPos.Y - curLineLength * Math.Sin(Degree30) / 2 + randOffset);

                case ENodeDirection.TwoToFour:
                    return new Point(CenterPos.X + (curLineLength / 2) + randOffset, CenterPos.Y + randOffset);

                case ENodeDirection.TwelveToTwo:
                    return new Point(CenterPos.X + curLineLength * Math.Cos(Degree30) / 2 + randOffset, CenterPos.Y + curLineLength * Math.Sin(Degree30) / 2 + randOffset);

                case ENodeDirection.TenToTwelve:
                    return new Point(CenterPos.X - curLineLength * Math.Cos(Degree30) / 2 + randOffset, CenterPos.Y + curLineLength * Math.Sin(Degree30) / 2 + randOffset);

                case ENodeDirection.EightToTen:
                    return new Point(CenterPos.X - (curLineLength / 2) + randOffset, CenterPos.Y + randOffset);

                #region 이전코드
                //case ENodeDirection.NineToEleven:
                //    return new Point(CenterPos.X - (int)(curLineLength * Math.Cos(rDegree0To60Num)), CenterPos.Y - (int)(curLineLength * Math.Sin(rDegree0To60Num)));

                //case ENodeDirection.EleventToOne:
                //    return new Point(CenterPos.X + (int)(curLineLength * Math.Sin(rDegree0To60Num)) * plusminus, CenterPos.Y + (int)(curLineLength * Math.Cos(rDegree0To60Num)));

                //case ENodeDirection.OneToThree:
                //    return new Point(CenterPos.X + (int)(curLineLength * Math.Cos(rDegree0To60Num)), CenterPos.Y - (int)(curLineLength * Math.Sin(rDegree0To60Num)));

                //case ENodeDirection.ThreeToFive:
                //    return new Point(CenterPos.X + (int)(curLineLength * Math.Cos(rDegree0To60Num)), CenterPos.Y + (int)(curLineLength * Math.Sin(rDegree0To60Num)));

                //case ENodeDirection.FiveToSeven:
                //    return new Point(CenterPos.X - (int)(curLineLength * Math.Sin(rDegree0To60Num)) * plusminus, CenterPos.Y - (int)(curLineLength * Math.Cos(rDegree0To60Num)));

                //case ENodeDirection.SevenToNine:
                //    return new Point(CenterPos.X - (int)(curLineLength * Math.Cos(rDegree0To60Num)), CenterPos.Y + (int)(curLineLength * Math.Sin(rDegree0To60Num)));

                //// 10 - 12 시 ~ 8 - 10 시 방향
                //case ENodeDirection.TenToTwelve:
                //    return new Point(CenterPos.X - (int)(curLineLength * Math.Sin(rDegree30To90Num)), CenterPos.Y + (int)(curLineLength * Math.Cos(rDegree30To90Num)));

                //case ENodeDirection.TwelveToTwo:
                //    return new Point(CenterPos.X + (int)(curLineLength * Math.Sin(rDegree30To90Num)), CenterPos.Y + (int)(curLineLength * Math.Cos(rDegree30To90Num)));

                //case ENodeDirection.TwoToFour:
                //    return new Point(CenterPos.X + (int)(curLineLength * Math.Cos(rDegree30To90Num)), CenterPos.Y + (int)(curLineLength * Math.Sin(rDegree30To90Num)) * plusminus);

                //case ENodeDirection.FourToSix:
                //    return new Point(CenterPos.X + (int)(curLineLength * Math.Sin(rDegree30To90Num)), CenterPos.Y - (int)(curLineLength * Math.Cos(rDegree30To90Num)));

                //case ENodeDirection.SixToEight:
                //    return new Point(CenterPos.X - (int)(curLineLength * Math.Sin(rDegree30To90Num)), CenterPos.Y - (int)(curLineLength * Math.Cos(rDegree30To90Num)));

                //case ENodeDirection.EightToTen:
                //    return new Point(CenterPos.X - (int)(curLineLength * Math.Cos(rDegree30To90Num)), CenterPos.Y - (int)(curLineLength * Math.Sin(rDegree30To90Num)) * plusminus);
                #endregion
            }

            return new Point(CenterPos.X - (int)(curLineLength * Math.Cos(rDegree0To60Num)), CenterPos.Y - (int)(curLineLength * Math.Sin(rDegree0To60Num)));
        }
    }

    public enum ENodeDirection
    {
        Center = 0,

        NineToEleven = 1, // 9 - 11시 방향 (좌상향)
        EleventToOne = 2, // 11 - 1시 방향 (상향)
        OneToThree = 3, // 1 - 3시 방향 (우상향)
        ThreeToFive = 4, // 3 - 5시 방향 (우하향)
        FiveToSeven = 5, // 5 - 7시 방향 (하향)
        SevenToNine = 6, // 7 - 9시 방향 (좌하향)

        SixToEight = 7,// 6시 - 8시 방향 (좌하향)
        FourToSix = 8,// 4 - 6 방향 (우하향)
        TwoToFour = 9,// 2 - 4 방향 (우상향)
        TwelveToTwo = 10,// 12 - 2 방향 (상향)
        TenToTwelve = 11,// 10 - 12 방향 (좌상향)
        EightToTen = 12,// 8 - 10 방향 (좌향)
    }
}
