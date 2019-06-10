using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFDatagridCustomization.ViewModel;
using System.Windows.Controls.Primitives;

namespace WPFDatagridCustomization
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = new MainWindowViewModel();
		}

		private void ToggleButton_Click(object sender, RoutedEventArgs e)
		{
			DependencyObject obj = (DependencyObject)e.OriginalSource;
			while (!(obj is DataGridRow) && obj != null) obj = VisualTreeHelper.GetParent(obj);
			if (obj is DataGridRow)
			{
				if ((obj as DataGridRow).DetailsVisibility == Visibility.Visible)
				{					
					(obj as DataGridRow).IsSelected = false;
				}
				else
				{					
					(obj as DataGridRow).IsSelected = true;
				}
			}
		}

		private void dataGrid1_RowDetailsVisibilityChanged(object sender, DataGridRowDetailsEventArgs e)
		{
			DataGridRow row = e.Row as DataGridRow;
			FrameworkElement tb = GetTemplateChildByName(row, "RowHeaderToggleButton");
			if (tb != null)
			{
				if (row.DetailsVisibility == System.Windows.Visibility.Visible)
				{
					(tb as ToggleButton).IsChecked = true;
				}
				else
				{
					(tb as ToggleButton).IsChecked = false;
				}
			}

		}
		public static FrameworkElement GetTemplateChildByName(DependencyObject parent, string name)
		{
			int childnum = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < childnum; i++)
			{
				var child = VisualTreeHelper.GetChild(parent, i);
				if (child is FrameworkElement &&

						((FrameworkElement)child).Name == name)
				{
					return child as FrameworkElement;
				}
				else
				{
					var s = GetTemplateChildByName(child, name);
					if (s != null)
						return s;
				}
			}
			return null;
		}

		private void dataGrid1_Loaded(object sender, RoutedEventArgs e)
		{
			DataGrid dg = sender as DataGrid;
			Border border = VisualTreeHelper.GetChild(dg, 0) as Border;
			ScrollViewer scrollViewer = VisualTreeHelper.GetChild(border, 0) as ScrollViewer;
			Grid grid = VisualTreeHelper.GetChild(scrollViewer, 0) as Grid;
			Button button = VisualTreeHelper.GetChild(grid, 0) as Button;

			if (button != null && button.Command != null && button.Command == DataGrid.SelectAllCommand)
			{
				button.IsEnabled = false;
				button.Opacity = 0;
			}
		}

		//void button_Click(object sender, RoutedEventArgs e)
		//{
		//    foreach (var item in dataGrid1.Items)
		//    {
		//        DataGridRow row = dataGrid1.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;		//       
		//    }
		//}
	}
}
