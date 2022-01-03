namespace ProiectIS.Models
{
    public class NotificationsDispatcher: IObservable<ScheduledQuiz>
    {

        private List<IObserver<ScheduledQuiz>> observers;
        public NotificationsDispatcher()
        {
            observers=new List<IObserver<ScheduledQuiz>>();
        }
        public NotificationsDispatcher(DateGrup sender)
        {
            observers = new List<IObserver<ScheduledQuiz>>();
            Subscribe(sender);
        }
        public IDisposable Subscribe(IObserver<ScheduledQuiz> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
       
            }
            return new Unsubscriber(observers, observer);
        }
        
        private class Unsubscriber : IDisposable
        {
            private List<IObserver<ScheduledQuiz>> _observers;
            private IObserver<ScheduledQuiz> _observer;

            public Unsubscriber(List<IObserver<ScheduledQuiz>> observers, IObserver<ScheduledQuiz> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public void notifyObservers(ScheduledQuiz value)
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
        public void complete()
        {
            foreach (var observer in observers.ToArray())
                if (observers.Contains(observer))
                    observer.OnCompleted();

            observers.Clear();
        }
    }
}
