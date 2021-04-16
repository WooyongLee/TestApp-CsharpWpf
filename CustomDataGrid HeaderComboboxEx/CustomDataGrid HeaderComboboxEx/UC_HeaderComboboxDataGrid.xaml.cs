using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace CustomDataGrid_HeaderComboboxEx
{
    /// <summary>
    /// UC_HeaderComboboxDataGrid.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UC_HeaderComboboxDataGrid : UserControl
    {
        public ObservableCollection<ComboboxItem> ComboboxItemObsCollection = new ObservableCollection<ComboboxItem>();

        public UC_HeaderComboboxDataGrid()
        {
            InitializeComponent();

            this.ComboboxDataGrid.DataContext = new MainViewModel();
            this.AddComboboxColumn();
        }

        public void AddComboboxColumn()
        {
            // 일반적인 단일 Column Text를 붙일 때
            // var col = new DataGridTemplateColumn { Header = "ComboboxEx" };

            DataGridTemplateColumn TemplateColumn1 = (DataGridTemplateColumn)this.Resources["ComboboxInDataGridTemplate"];
            // 동일한 Resource 추가 시 Key 중복 에러 발생함
            // DataGridTemplateColumn TemplateColumn2 = (DataGridTemplateColumn)this.Resources["ComboboxInDataGridTemplate"];
            // DataGridTemplateColumn TemplateColumn3 = (DataGridTemplateColumn)this.Resources["ComboboxInDataGridTemplate"];

            ComboboxDataGrid.Columns.Add(TemplateColumn1);
            //ComboboxDataGrid.Columns.Add(TemplateColumn2);
            //ComboboxDataGrid.Columns.Add(TemplateColumn3);

            // 내가 생각하는 흐름

            // 1. column에 대한 Template(DataGridHeader In Combobox)을 Xaml내에서 찾는다.
            // Resource에 지정해 둔 Key를 이용해 FindKey로
            // ex. CellTemplate = (DataTemplate)this.Resources["DateTimeColumnDataTemplate"];

            // 2. Template을 ObservableCollection에 Binding한다.
            // ex. column.Itemssource = ViewMode;

            // 3. 찾은 Template을 column에 붙인다.
            // ex. DataGrid.Columns.Add(FoundTemplate);

            // 4. 테스트한다.
            // 
        }
    }

    // Combobox에 들어갈 Item들을 정의한 클래스
    public class ComboboxItem
    {
        public string Item { get; set; }
    }
}
