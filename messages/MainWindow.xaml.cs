using System.Windows;
using MessageManager;

namespace messages
{
    public partial class MainWindow : Window
    {
        MessageManager.MessageManager messageManager = new MessageManager.MessageManager();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_ClickOK(object sender, RoutedEventArgs e)
        {
            DialogResult result = await messageManager.ShowDialog(MainGrid, "Тестовый заголовок сообщения", "Тестовый текст сообщения", DialogType.Ok);
            if (result == MessageManager.DialogResult.Ok)
            {
                await messageManager.ShowDialog(MainGrid, "Результат", "Была нажата кнопка ОК", DialogType.Ok);
            }
        }

        private async void Button_ClickOKCancel(object sender, RoutedEventArgs e)
        {
            DialogResult result = await messageManager.ShowDialog(MainGrid, "Тестовый заголовок сообщения", "Тестовый текст сообщения", DialogType.OkCancel);
            switch (result)
            {
                case MessageManager.DialogResult.Ok:
                    await messageManager.ShowDialog(MainGrid, "Результат", "Была нажата кнопка ОК", DialogType.Ok);
                    break;

                case MessageManager.DialogResult.Cancel:
                    await messageManager.ShowDialog(MainGrid, "Результат", "Была нажата кнопка Отмена", DialogType.Ok);
                    break;
            }
        }

        private async void Button_ClickYes(object sender, RoutedEventArgs e)
        {
            DialogResult result = await messageManager.ShowDialog(MainGrid, "Тестовый заголовок сообщения", "Тестовый текст сообщения", DialogType.Yes);
            if (result == MessageManager.DialogResult.Yes)
            {
                await messageManager.ShowDialog(MainGrid, "Результат", "Была нажата кнопка Да", DialogType.Ok);
            }
        }

        private async void Button_ClickYesNo(object sender, RoutedEventArgs e)
        {
            DialogResult result = await messageManager.ShowDialog(MainGrid, "Тестовый заголовок сообщения", "Тестовый текст сообщения", DialogType.YesNo);
            switch (result)
            {
                case MessageManager.DialogResult.Yes:
                    await messageManager.ShowDialog(MainGrid, "Результат", "Была нажата кнопка Да", DialogType.Ok);
                    break;

                case MessageManager.DialogResult.No:
                    await messageManager.ShowDialog(MainGrid, "Результат", "Была нажата кнопка Нет", DialogType.Ok);
                    break;
            }
        }

        private async void Button_ClickYesNoCancel(object sender, RoutedEventArgs e)
        {
            DialogResult result = await messageManager.ShowDialog(MainGrid, "Тестовый заголовок сообщения", "Тестовый текст сообщения", DialogType.YesNoCancel);
            switch (result)
            {
                case MessageManager.DialogResult.Yes:
                    await messageManager.ShowDialog(MainGrid, "Результат", "Была нажата кнопка Да", DialogType.Ok);
                    break;

                case MessageManager.DialogResult.No:
                    await messageManager.ShowDialog(MainGrid, "Результат", "Была нажата кнопка Нет", DialogType.Ok);
                    break;

                case MessageManager.DialogResult.Cancel:
                    await messageManager.ShowDialog(MainGrid, "Результат", "Была нажата кнопка Отмена", DialogType.Ok);
                    break;
            }
        }
    }
}
