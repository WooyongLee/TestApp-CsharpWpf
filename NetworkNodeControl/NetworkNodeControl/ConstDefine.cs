using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetworkNodeControl
{
    public static class ConstValue
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
    }
}
