using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CustomUIElementEx
{
    /// <summary>
    /// EventTestWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class EventTestWindow : Window
    {
        public EventTestWindow()
        {
            InitializeComponent();
        }

        // Source Label MouseDown 이벤트 함수
        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataObject data = new DataObject(DataFormats.Text, ((Label)e.Source).Content);

            // Return Effect가 먹지 않는 현상이 있음 => 분명 Copy로 지정했는데 None으로 반환함
            DragDropEffects retEffect = DragDrop.DoDragDrop((DependencyObject)e.Source, data, DragDropEffects.Copy);

            // Not called until drag-and-drop is done
            ((Label)e.Source).Content = "DragDrop done";
        }

        // Target Label Drop 이벤트 함수
        private void Label_Drop(object sender, DragEventArgs e)
        {
            ((Label)e.Source).Content = (string)e.Data.GetData(DataFormats.Text);
        }

        // 커서 절의
        private Cursor customCursor = null;
        private void Label_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            //if (e.Effects == DragDropEffects.Copy)
            if (e.Effects == DragDropEffects.None)
            {
                // 커서를 해당 문자열로 생성
                if (customCursor == null)
                {
                    customCursor = CursorHelper.CreateCursor("WOWOWOWOW");
                    // customCursor = CursorHelper.CreateCursor2(e.Source as UIElement); // 해당 
                    //e.UseDefaultCursors = false;
                    //Mouse.SetCursor(Cursors.Hand);
                }

                // 커서를 설정
                if (customCursor != null)
                {
                    e.UseDefaultCursors = false;
                    Mouse.SetCursor(customCursor);
                }
            }
            
            // Copy 명령이 아닌 경우 Default 커서로 설정
            else
                e.UseDefaultCursors = true;

            e.Handled = true;
        }

        // StackPanel에 대한 Left Mouse Down에 대한 이벤트 함수
        private void MainPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 현재 마우스가 위치한 UI Element 집합에 대한 Xaml 코드를 문자열 형태로 저장
            string xaml = XamlWriter.Save(e.Source);

            // xaml 형태의 데이터 객체 생성
            DataObject data = new DataObject(DataFormats.Xaml, xaml);

            // 현재 마우스 소스와 xaml 데이터를 Copy  Buffer에 저장 -> 드래그 드롭 처리
            var d = DragDrop.DoDragDrop((DependencyObject)e.Source, data, DragDropEffects.Copy);
        }
    }

    // 커스텀 커서 설정 클래스
    public static class CursorHelper
    {
        private static class NativeMethods
        {
            public struct IconInfo
            {
                public bool fIcon;
                public int xHotspot;
                public int yHotspot;
                public IntPtr hbmMask;
                public IntPtr hbmColor;
            }

            /// <summary>
            /// 아이콘 간접 생성
            /// </summary>
            /// <param name="icon">아이콘 정보</param>
            /// <returns>아이콘 핸들</returns>
            [DllImport("user32.dll")]
            public static extern SafeIconHandle CreateIconIndirect(ref IconInfo icon);

            /// <summary>
            /// 아이콘 제거
            /// </summary>
            /// <param name="hIcon">아이콘 핸들</param>
            /// <returns>제거 결과</returns>
            [DllImport("user32.dll")]
            public static extern bool DestroyIcon(IntPtr hIcon);

            /// <summary>
            /// 아이콘 정보 가져오기
            /// </summary>
            /// <param name="hIcon">아이콘 핸들</param>
            /// <param name="pIconInfo">아이콘 정보(Refenrence)</param>
            /// <returns></returns>
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
        }

        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        private class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public SafeIconHandle() : base(true)
            {
            }

            override protected bool ReleaseHandle()
            {
                return NativeMethods.DestroyIcon(handle);
            }
        }

        // 내부적인 커서를 ICon 형태로 생성
        private static Cursor InternalCreateCursor(System.Drawing.Bitmap bmp)
        {
            // 커서 ICon
            var iconInfo = new NativeMethods.IconInfo();

            // Cursor가 저장된 Bitmap을 통해 IconInfo의 속성들을 채움(hbmColor, hbmMask 등)
            NativeMethods.GetIconInfo(bmp.GetHicon(), ref iconInfo);

            iconInfo.xHotspot = 0;
            iconInfo.yHotspot = 0;
            iconInfo.fIcon = false;

            SafeIconHandle cursorHandle = NativeMethods.CreateIconIndirect(ref iconInfo);
            return CursorInteropHelper.Create(cursorHandle);
        }
        
        // 커서를 파라미터로 설정한 문자열로 커스텀하게 생성
        public static Cursor CreateCursor(string cursorText)
        {
            // Text to render, 렌더링하는 텍스트의 속성 정의
            FormattedText fmtText = new FormattedText(cursorText,
                    new CultureInfo("en-us"),
                    FlowDirection.LeftToRight,
                    new Typeface(new FontFamily("Arial"), FontStyles.Normal, FontWeights.Normal, new FontStretch()),
                    12.0,  // FontSize
                    Brushes.Black);

            // The Visual to use as the source of the RenderTargetBitmap.
            // 그림 문맥에 대한 Text Drawing
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            drawingContext.DrawText(fmtText, new Point());
            drawingContext.Close();

            // The BitmapSource that is rendered with a Visual.
            RenderTargetBitmap rtb = new RenderTargetBitmap(
                (int)drawingVisual.ContentBounds.Width,
                (int)drawingVisual.ContentBounds.Height,
                96,   // dpiX
                96,   // dpiY
                PixelFormats.Pbgra32);
            rtb.Render(drawingVisual);

            // Encoding the RenderBitmapTarget into a bitmap (as PNG)
            // 렌더링 할 Cursor 이미지를 Png 파일로 인코딩, 저장
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                using (var bmp = new System.Drawing.Bitmap(ms))
                {
                    // 내부적인 커서로 생성
                    return InternalCreateCursor(bmp);
                }
            }
        }

        // 커서를 해당 UI Element를 이용하여 생성
        public static Cursor CreateCursor2(UIElement element)
        {
            element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            element.Arrange(new Rect(new Point(), element.DesiredSize));

            RenderTargetBitmap rtb =
              new RenderTargetBitmap(
                (int)element.DesiredSize.Width,
                (int)element.DesiredSize.Height,
                96, 96, PixelFormats.Pbgra32);

            rtb.Render(element);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                using (var bmp = new System.Drawing.Bitmap(ms))
                {
                    return InternalCreateCursor(bmp);
                }
            }
        }
    }
}
