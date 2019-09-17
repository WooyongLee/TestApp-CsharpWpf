using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace TreeViewExample
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<TreeItem> MenuItemList = new List<TreeItem>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TreeRootAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TreeRootTextBox.Text))
            {
                string rootStr = TreeRootTextBox.Text;
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    // 이전에 동일한 Title 있는 경우를 탐색
                    int countOfAlreadyTitle = MenuItemList.Where(x => x.TrackNum == rootStr).Count();
                    // var AlreadyTitle = MenuItemList.Where(x => x.Title == rootStr).Select(x => x.Title);
                    // int index = MenuItemList.FindIndex(x => x.Title == rootStr);

                    // 동일한 MenuItem Title 없는 경우 새로 root를 만들어 주기
                    if (countOfAlreadyTitle == 0)
                    {
                        TreeItem root = new TreeItem() { TrackNum = rootStr };
                        MenuItemList.Add(root);
                        MyTreeView.Items.Add(MenuItemList.Last());
                    }

                    else
                    {
                        StateTextBlock.Text = "이미 해당루트 있음";
                        return;
                    }
                }));
            }
        }

        private void TreeItemAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TreeItemTextBox.Text) && !string.IsNullOrEmpty(TreeRootTextBox.Text))
            {
                // 해당 Root에 Item 추가
                string rootStr = TreeRootTextBox.Text;
                string itemStr = TreeItemTextBox.Text;
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    // 이전에 동일한 Title 있는 경우를 탐색
                    int countOfAlreadyTitle = MenuItemList.Where(x => x.TrackNum == rootStr).Count();
                    var AlreadyTitle = MenuItemList.Where(x => x.TrackNum == rootStr).Select(x => x.TrackNum);
                    int index = MenuItemList.FindIndex(x => x.TrackNum == rootStr);

                    // 동일한 MenuItem Title 없는 경우 예외 발생
                    if (countOfAlreadyTitle == 0)
                    {
                        StateTextBlock.Text = "루트가 없음, 루트 추가 바람";
                        return;
                    }

                    // 그렇지 않는 경우 기존의 root를 찾아서
                    else
                    {
                        if (MenuItemList.Count > index)
                        {
                            if (MenuItemList[index].Items.Where(x => x.TrackNum == itemStr).Count() == 0)
                            {
                                MenuItemList[index].Items.Add(new TreeItem() { TrackNum = itemStr });
                            }
                            else
                            {
                                StateTextBlock.Text = "이미 해당아이템 있음";
                                return;
                            }
                        }

                        else
                        {
                            StateTextBlock.Text = "인덱스 벗어남";
                            return;
                        }
                    }
                }));
            }
        }
    }

    public class TreeItem
    {
        public TreeItem()
        {
            this.Items = new ObservableCollection<TreeItem>();
        }

        public string TrackNum { get; set; }
        public ObservableCollection<TreeItem> Items { get; set; }
    }
}
