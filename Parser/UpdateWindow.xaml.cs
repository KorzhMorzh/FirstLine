using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Parser
{
    /// <summary>
    /// Логика взаимодействия для UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow
    {
        public UpdateWindow()
        {
            InitializeComponent();
            var listChanges = DataHandler.GetChanges();
            totalChanges.Text = listChanges.Count.ToString();
            for (int i = 0; i < listChanges.Count; i++)
            {
                GridOfChanges.RowDefinitions.Add(new RowDefinition());
                var expander = new Expander();
                var dataGrid = new DataGrid()
                {
                    Width = 460,
                    HeadersVisibility = DataGridHeadersVisibility.Column,
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden
                };

                Style elementStyle = new Style(typeof(TextBlock));
                elementStyle.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));

                var nameColumn = new DataGridTextColumn { ElementStyle = elementStyle, Width = 60 };
                var oldColumn = new DataGridTextColumn { ElementStyle = elementStyle, Width = 200 };
                var newColumn = new DataGridTextColumn { ElementStyle = elementStyle, Width = 200 };

                expander.Content = dataGrid;

                if (listChanges[i].StringNewThreat[0] == "")
                {
                    expander.Header = "Запись " + listChanges[i].StringOldThreat[0] + " удалена";
                }
                else if (listChanges[i].StringOldThreat[0] == "")
                {
                    expander.Header = "Запись " + listChanges[i].StringOldThreat[0] + " добавлена";
                }
                else
                {
                    expander.Header = listChanges[i].StringNewThreat[0];
                }


                nameColumn.Binding = new Binding("[0]") { Mode = BindingMode.OneWay };
                oldColumn.Binding = new Binding("[1]") { Mode = BindingMode.OneWay };
                newColumn.Binding = new Binding("[2]") { Mode = BindingMode.OneWay };

                dataGrid.Columns.Add(nameColumn);
                dataGrid.Columns.Add(oldColumn);
                dataGrid.Columns.Add(newColumn);

                Grid.SetRow(expander, i);

                GridOfChanges.Children.Add(expander);
                var cellsName = new[]
                {
                    "", "Имя", "Описание", "Источник угрозы", "Объект воздействия угрозы",
                    "Нарушение конфиденциальности", "Нарушение целостности", "Нарушение доступности"
                };
                dataGrid.Columns[1].Header = "Было";
                dataGrid.Columns[2].Header = "Стало";
                for (int j = 1; j < 8; j++)
                {
                    dataGrid.Items.Add(new[]
                        {cellsName[j], listChanges[i].StringOldThreat[j], listChanges[i].StringNewThreat[j]});
                }
            }
        }
    }
}