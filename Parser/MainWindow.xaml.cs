using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Parser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int _pageNumber = 1;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            NextPage.Click += Click_Handler;
            PrevPage.Click += Click_Handler;
        }

        private void Click_Handler(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridOfThreats.ItemsSource = DataHandler.BaseInfoThreats.GetRange((_pageNumber - 1) * 15, 15);
            }
            catch //Громоздко, можно обернуть в метод
            {
                if ((_pageNumber * 15 > DataHandler.BaseInfoThreats.Count - 1) &&
                    ((_pageNumber - 1) * 15 < DataHandler.BaseInfoThreats.Count - 1))
                {
                    DataGridOfThreats.ItemsSource = DataHandler.BaseInfoThreats.GetRange((_pageNumber - 1) * 15,
                        DataHandler.BaseInfoThreats.Count - (_pageNumber - 1) * 15);
                }
                else if (_pageNumber * 15 > DataHandler.BaseInfoThreats.Count - 1)
                {
                    _pageNumber -= 1;
                }
                else if (_pageNumber < 1)
                {
                    _pageNumber += 1;
                }
            }

            CurrentPage.Text = _pageNumber.ToString();
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (!File.Exists("thrlist.xlsx"))
            {
                MessageBoxResult result = MessageBox.Show("Провести первичную загрузку данных?",
                    "Файл с локальной базой не существует",
                    MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        try
                        {
                            DataHandler.Download("thrlist.xlsx");
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message, "Не удалось загрузить файл.");
                            MessageBox.Show("Дальнейшая работа невозможна", "Завершение программы",
                                MessageBoxButton.OK, MessageBoxImage.Stop);
                            Environment.Exit(0);
                        }

                        break;
                    case MessageBoxResult.Cancel:
                        MessageBox.Show("Дальнейшая работа невозможна", "Завершение программы",
                            MessageBoxButton.OK, MessageBoxImage.Stop);
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private void DataGridOfThreats_Loaded(object sender, RoutedEventArgs ev)
        {
            try
            {
                DataHandler.BaseInfoThreats = DataHandler.GetShortTreatsInfo();
            }
            catch
            {
                MessageBoxResult result = MessageBox.Show("Файл некорректный. Обновить данные?", "Ошибка",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        try
                        {
                            DataHandler.Download("thrlist.xlsx");
                            DataHandler.BaseInfoThreats = DataHandler.GetShortTreatsInfo();
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message, "Не удалось загрузить файл.");
                            MessageBox.Show("Дальнейшая работа невозможна", "Завершение программы",
                                MessageBoxButton.OK, MessageBoxImage.Stop);
                            Environment.Exit(0);
                        }

                        break;
                    case MessageBoxResult.No:
                        MessageBox.Show("Дальнейшая работа невозможна", "Завершение программы",
                            MessageBoxButton.OK, MessageBoxImage.Stop);
                        Environment.Exit(0);
                        break;
                }
            }

            DataGridOfThreats.ItemsSource = DataHandler.BaseInfoThreats.GetRange(0, 15);
            CurrentPage.Text = _pageNumber.ToString();
            TotalPage.Text = Math.Ceiling(DataHandler.BaseInfoThreats.Count / 15.0).ToString();
        }

        private void DataGridOfThreats_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Name")
            {
                Style elementStyle = new Style(typeof(TextBlock));
                elementStyle.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
                e.Column = e.Column as DataGridBoundColumn;
                if (e.Column != null)
                {
                    ((DataGridBoundColumn)e.Column).ElementStyle = elementStyle;
                    e.Column.Width = 400;
                    e.Column.Header = "Наименование угрозы";
                }
            }
            else
            {
                e.Column.Header = "ID";
            }
        }

        private void DataGridOfThreats_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var threat = DataGridOfThreats.SelectedItem as Threat;
            try
            {
                MessageBox.Show($"Идентификатор угрозы: {threat.FullId}\n\nНаименование угрозы: {threat.Name}" +
                                $"\n\nОписание угрозы: {threat.Description}\n\nИсточник угрозы: {threat.Source}" +
                                $"\n\nОбъект воздействия угрозы: {threat.ThreatSubject}\n\nНарушение конфидициальности: {threat.BreachOfConfidentiality}" +
                                $"\n\nНарушение целостности: {threat.BreachOfIntegrity}\n\nНарушение доступности: {threat.BreachOfAvailability}",
                    "Полные сведения об угрозе");
            }
            catch
            {
                // ignored
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            _pageNumber += 1;
        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            _pageNumber -= 1;
        }


        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            DataHandler.Save();
        }

        private void Update_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataHandler.CheckUpdate())
                {
                    MessageBox.Show("Локальная база актуальна", "Обновления не найдены");
                }
                else
                {
                    UpdateWindow updateWindow = new UpdateWindow();
                    updateWindow.Show();
                    DataHandler.Replace();
                    DataHandler.BaseInfoThreats = DataHandler.GetShortTreatsInfo();
                    DataGridOfThreats.ItemsSource = DataHandler.BaseInfoThreats.GetRange(0, 15);
                    CurrentPage.Text = _pageNumber.ToString();
                    _pageNumber = 1;
                    CurrentPage.Text = _pageNumber.ToString();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка");
            }
        }
    }
}