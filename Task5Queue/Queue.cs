using System.Collections.Generic;

namespace OOAP1.Task5Queue
{
    public interface Queue<T>
    {
        // Конструктор

        // Постусловие: создана пустая очередь.
        // public Queue<T> Queue();


        // Команды

        // Постусловие: элемент добавлен на вход очереди.
        void Enqueue(T item);

        // Предусловие: очередь не пуста.
        // Постусловие: элемент удалён с выхода очереди.
        void Dequeue();

        // Постусловие: из очереди удалены все элементы.
        void Clear();


        // Запросы

        //Предусловие: очередь не пуста.
        T Peek();

        int Count();


        // Дополнительные запросы (возможные статусы)

        int GetDequeueStatus(); // (0 - Не вызывалась, 1 - Успешно, 2 - Ошибка, очередь пуста)

        int GetPeekStatus(); // (0 - Не вызывалась, 1 - Успешно, 2 - Ошибка, очередь пуста)
    }

    
    public class QueueImpl<T> : Queue<T>
    {
        public const int DEQUEUE_NIL = 0;
        public const int DEQUEUE_OK = 1;
        public const int DEQUEUE_ERR_QUEUE_EMPTY = 2;
        
        public const int PEEK_NIL = 0;
        public const int PEEK_OK = 1;
        public const int PEEK_ERR_QUEUE_EMPTY = 2;
        
        private int _dequeueStatus;
        private int _peekStatus;
        private LinkedList<T> _items;

        public QueueImpl()
        {
            _items = new LinkedList<T>();
            
            ResetToInitialState();
        }

        public void Enqueue(T item) =>
            _items.AddFirst(item);

        public void Dequeue()
        {
            if (_items.Count == 0)
            {
                _dequeueStatus = DEQUEUE_ERR_QUEUE_EMPTY;
            }
            else
            {
                _items.RemoveLast();
                _dequeueStatus = DEQUEUE_OK;
            }
        }

        public void Clear() => ResetToInitialState();

        public T Peek()
        {
            T result = default;
            
            if (_items.Count == 0)
            {
                _peekStatus = PEEK_ERR_QUEUE_EMPTY;
            }
            else
            {
                result = _items.Last.Value;
                _peekStatus = PEEK_OK;
            }

            return result;
        }

        public int Count() => _items.Count;

        public int GetDequeueStatus() => _dequeueStatus;

        public int GetPeekStatus() => _peekStatus;
        
        private void ResetToInitialState()
        {
            _items.Clear();
            
            _dequeueStatus = DEQUEUE_NIL;
            _peekStatus = PEEK_NIL;
        }
    }
}

