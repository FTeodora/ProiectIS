namespace ProiectIS.Models
{
    public class NotificationsDispatcher//: IObservable<SavedQuiz>
    {
        private List<IObserver<SavedQuiz>> observers;
        public NotificationsDispatcher()
        {
            observers=new List<IObserver<SavedQuiz>>();
        }
        public IDisposable Subscribe(IObserver<SavedQuiz> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
                
            }
            return new Unsubscriber(observers, observer);
        }
        /*
        void IObserver<SavedQuiz>.OnCompleted()
        {
            throw new NotImplementedException();
        }

        void IObserver<SavedQuiz>.OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        void IObserver<SavedQuiz>.OnNext(SavedQuiz value)
        {

            
        }*/
        private class Unsubscriber : IDisposable
        {
            private List<IObserver<SavedQuiz>> _observers;
            private IObserver<SavedQuiz> _observer;

            public Unsubscriber(List<IObserver<SavedQuiz>> observers, IObserver<SavedQuiz> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public void notify(SavedQuiz value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
           
            foreach (var observer in observers)
            {
                observer.OnNext(value);
            }
        }
        public void EndTransmission()
        {
            foreach (var observer in observers.ToArray())
                if (observers.Contains(observer))
                    observer.OnCompleted();

            observers.Clear();
        }
    }
}
