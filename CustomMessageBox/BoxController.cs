
using System.Linq;
using System.Windows;

namespace CustomMessageBox
{
    public enum MsgBoxButtonType { Yes_No, OK }

    // MessageBox를 제어하는 클래스
    public class MessageBoxControl
    {
        public static string Show(string Text)
        {
            return Show(Text, MsgBoxButtonType.OK);
        }

        public static string Show(string Text, MsgBoxButtonType btnType)
        {
            MessageBoxUI messageBox = new MessageBoxUI(Text, btnType);
            messageBox.Show();
            return messageBox.ReturnString;

        }
    }

    // Notification Box를 제어하는 클래스
    public class NotificationBoxControl
    {
        public static string Show(string text)
        {
            NotificationBoxUI notificationBox = new NotificationBoxUI(text);
            notificationBox.Show();
            return "1";
        }

        public static string ShowDialog(string text)
        {
            NotificationBoxUI notificationBox = new NotificationBoxUI(text);
            notificationBox.ShowDialog();
            return "1";
        }

    }

    // MessageBox, NotificationBox를 닫는 기능을 하는 클래스
    public class CloseMessage
    {
        public static void AllShortNotification()
        {
            foreach(NotificationBoxUI window in Application.Current.Windows.OfType<NotificationBoxUI>())
            {
                window.FastClose();
            }
            while (Application.Current.Windows.OfType<NotificationBoxUI>().Count() > 0 )
            {

            }
        }

        public static void AllMessageBoxes()
        {
            foreach (MessageBoxUI window in Application.Current.Windows.OfType<MessageBoxUI>())
            {
                window.Close();
            }
            while (Application.Current.Windows.OfType<MessageBoxUI>().Count() > 0)
            {

            }
        }

        public static void AllMessages()
        {

        }
    }

}
