using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MessageManager
{
    public class MessageManager
    {
        /// <summary>
        /// Показать диалоговое окно в заданном гриде с блокировкой его элементов управления.
        /// </summary>
        /// <param name="SourceGrid">Грид в котором будет отображено диалоговое окно.</param>
        /// <param name="DialogTitle">Заголовок окна.</param>
        /// <param name="DialogText">Текст в окне.</param>
        /// <param name="Dialogtype">Тип окна (по умолчанию ОК).</param>
        /// <param name="DialogImage">Изображение в окне (по умолчанию пусто).</param>
        /// <returns></returns>
        public async Task<DialogResult> ShowDialog(Grid SourceGrid, string DialogTitle, string DialogText, DialogType Dialogtype = DialogType.Ok, BitmapImage DialogImage = null)
        {
            // Фрейм для блокирования элементов управления грида и отображения диалога.
            var DialogFrame = new Frame
            {
                Background = new SolidColorBrush(Color.FromArgb(0x80, 0xff, 0xff, 0xff)),
                NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden
            };
            IDialog dialog = new Dialog(DialogTitle, DialogText, Dialogtype, DialogImage);

            DialogFrame.Content = dialog;
            // Расположение фрейма с диалогом поверх элементов управления грида.
            SourceGrid.Children.Add(DialogFrame);
            // Запуск анимации появления диалога с указанием его конечной ширины и высоты.
            dialog.OpenAnimate(400, 140);

            var tcs = new TaskCompletionSource<DialogResult>();
            void action(DialogResult d) => tcs.TrySetResult(d);

            try
            {
                // Ожидание нажатия на кнопку диалога.
                dialog.ButtonClick += action;
                await tcs.Task;
                return tcs.Task.Result;
            }
            finally
            {
                // Закрытие диалога.
                dialog.ButtonClick -= action;
                dialog.CloseAnimate();
                dialog.Closed += Dialog_Closed;
            }
            // Завершение анимации закрытия диалога.
            void Dialog_Closed()
            {
                // Удаление фрейма из грида.
                SourceGrid.Children.Remove(DialogFrame);
            }
        }
    }
}