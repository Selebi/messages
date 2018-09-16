using System;

namespace MessageManager
{
    interface IDialog
    {
        /// <summary>
        /// Скорость анимации в секундах (по умолчанию 0.2).
        /// </summary>
        double AnimationSpeed { set; }
        /// <summary>
        /// Нажатие кнопки на диалоге.
        /// </summary>
        event Action<DialogResult> ButtonClick;
        /// <summary>
        /// Открытие диалога.
        /// </summary>
        event Action Opened;
        /// <summary>
        /// Закрытие диалога.
        /// </summary>
        event Action Closed;
        /// <summary>
        /// Запуск анимации открытия диалога.
        /// </summary>
        /// <param name="ToWidth">Ширина диалога.</param>
        /// <param name="ToHeight">Высота диалога.</param>
        void OpenAnimate(double ToWidth, double ToHeight);
        /// <summary>
        /// Запуск анимации закрытия сообщения.
        /// </summary>
        void CloseAnimate();
    }
}
