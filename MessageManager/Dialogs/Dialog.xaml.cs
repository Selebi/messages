using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace MessageManager
{
    /// <summary>
    /// Варианты результата.
    /// </summary>
    public enum DialogResult
    {
        Yes,
        No,
        Cancel,
        Ok
    }

    /// <summary>
    /// Варианты окна диалога.
    /// </summary>
    public enum DialogType
    {
        Ok,
        OkCancel,
        Yes,
        YesCancel,
        YesNo,
        YesNoCancel
    }

    public partial class Dialog : Page, IDialog
    {
        public event Action<DialogResult> ButtonClick;
        public event Action Opened;
        public event Action Closed;

        public double AnimationSpeed { private get; set; } = 0.2;

        public Dialog(string title, string message, DialogType type, BitmapImage Image = null)
        {
            InitializeComponent();

            Title.Text = title;
            Message.Text = message;

            if (Image != null)
            {
                MessageImage.Source = Image;
            }

            Width = 1;
            Height = 1;
            MessageContent.Visibility = Visibility.Collapsed;

            switch (type)
            {
                case (DialogType.Ok):
                    AddButton("ОК", Button_OK_Click);
                    break;

                case (DialogType.OkCancel):
                    AddButton("Отмена", Button_Cancel_Click);
                    AddButton("ОК", Button_OK_Click);
                    break;

                case (DialogType.Yes):
                    AddButton("Да", Button_Yes_Click);
                    break;

                case (DialogType.YesCancel):
                    AddButton("Отмена", Button_Cancel_Click);
                    AddButton("Да", Button_Yes_Click);
                    break;

                case (DialogType.YesNo):
                    AddButton("Нет", Button_No_Click);
                    AddButton("Да", Button_Yes_Click);
                    break;

                case (DialogType.YesNoCancel):
                    AddButton("Отмена", Button_Cancel_Click);
                    AddButton("Нет", Button_No_Click);
                    AddButton("Да", Button_Yes_Click);
                    break;
            }

            // Метод добавления кнопок на панель диалога.
            void AddButton(string content, RoutedEventHandler handler)
            {
                var button = new Button
                {
                    Content = content,
                    Width = 80,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Margin = new Thickness(0, 0, 16, 0)
                };
                button.Click += handler;
                ButtonPanel.Children.Add(button);
            }
        }

        public void OpenAnimate(double ToWidth, double ToHeight)
        {
            // Анимация изменения ширины.
            var MessageWidthAnimation = new DoubleAnimation
            {
                From = this.ActualWidth,
                To = ToWidth,
                Duration = TimeSpan.FromSeconds(AnimationSpeed)
            };
            // Анимация изменения высоты.
            var MessageHeightAnimation = new DoubleAnimation
            {
                From = this.ActualHeight,
                To = ToHeight,
                Duration = TimeSpan.FromSeconds(AnimationSpeed)
            };
            MessageHeightAnimation.Completed += MessageAnimation_Completed;
            this.BeginAnimation(Page.WidthProperty, MessageWidthAnimation);
            this.BeginAnimation(Page.HeightProperty, MessageHeightAnimation);
            // Завершение анимации
            void MessageAnimation_Completed(object sender, EventArgs e)
            {
                // Для чёткого отображения.
                this.UseLayoutRounding = true;
                MessageContent.Visibility = Visibility.Visible;
                Opened?.Invoke();
            }
        }

        public void CloseAnimate()
        {
            MessageContent.Visibility = Visibility.Collapsed;
            // Для плавной отрисовки.
            this.UseLayoutRounding = false;
            // Анимация изменения ширины.
            var MessageWidthAnimation = new DoubleAnimation
            {
                From = this.ActualWidth,
                To = 1,
                Duration = TimeSpan.FromSeconds(AnimationSpeed)
            };
            // Анимация изменения высоты.
            var MessageHeightAnimation = new DoubleAnimation
            {
                From = this.ActualHeight,
                To = 1,
                Duration = TimeSpan.FromSeconds(AnimationSpeed)
            };
            MessageHeightAnimation.Completed += MessageAnimation_Completed;
            this.BeginAnimation(Page.WidthProperty, MessageWidthAnimation);
            this.BeginAnimation(Page.HeightProperty, MessageHeightAnimation);
            // Завершение анимации
            void MessageAnimation_Completed(object sender, EventArgs e)
            {
                Closed?.Invoke();
            }
        }

        private void Button_No_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick?.Invoke(DialogResult.No);
        }

        private void Button_Yes_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick?.Invoke(DialogResult.Yes);
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick?.Invoke(DialogResult.Ok);
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick?.Invoke(DialogResult.Cancel);
        }
    }
}