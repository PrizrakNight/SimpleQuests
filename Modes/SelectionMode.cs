using System;
using SimpleQuests.Localization;

namespace SimpleQuests.Modes
{
    public class SelectionMode<TElement> : IMode
    {
        public bool IsLaunched { get; private set; }

        public int LeftOffset = 1;

        public event Action<TElement> OnValid;

        public event Action<Exception> OnError;

        public event Action<int> OnIndexOut;

        public event Action OnStop;

        public event Action OnLaunch;

        private readonly TElement[] _elements;

        private readonly int _exitCode;

        public SelectionMode(TElement[] elements, int exitCode = 0)
        {
            _elements = elements;

            _exitCode = exitCode;
        }

        public void Launch()
        {
            IsLaunched = true;

            OnLaunch?.Invoke();

            while (IsLaunched)
            {
                if (int.TryParse(Console.ReadLine(), out int index))
                {
                    try
                    {
                        if (index == _exitCode)
                        {
                            Stop();
                            break;
                        }

                        TElement element = _elements[index - LeftOffset];

                        OnValid?.Invoke(element);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        OnIndexOut?.Invoke(index);
                    }
                    catch (Exception exception)
                    {
                        OnError?.Invoke(exception);
                    }
                }
                else Console.WriteLine(LocalizationService.CurrentReader["InvalidInputData"]);
            }
        }

        public void Stop() => OnStop?.Invoke();
    }
}