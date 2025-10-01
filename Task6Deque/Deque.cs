using System.Collections.Generic;

namespace OOAP1.Task6Deque
{
    public abstract class ParentQueue<T>
    {
        public const int REMOVE_FRONT_NIL = 0;
        public const int REMOVE_FRONT_OK = 1;
        public const int REMOVE_FRONT_ERR_QUEUE_EMPTY = 2;
        
        public const int GET_FRONT_NIL = 0;
        public const int GET_FRONT_OK = 1;
        public const int GET_FRONT_ERR_QUEUE_EMPTY = 2;
        
        private int _removeFrontStatus;
        private int _getFrontStatus;
        
        protected LinkedList<T> Items { get; }
        
        
        // Конструктор
        
        protected ParentQueue()
        {
            Items = new LinkedList<T>();
            
            Clear();
        }


        #region Команды

        // Постусловие: элемент добавлен в хвост очереди.
        public void AddTail(T item) => Items.AddLast(item);
        

        // Предусловие: очередь не пуста.
        // Постусловие: элемент удалён из головы очереди.
        public void RemoveFront()
        {
            if (Items.Count == 0)
            {
                _removeFrontStatus = REMOVE_FRONT_ERR_QUEUE_EMPTY;
            }
            else
            {
                Items.RemoveFirst();
                _removeFrontStatus = REMOVE_FRONT_OK;
            }
        }

        // Постусловие: из очереди удалены все элементы.
        public void Clear()
        {
            ResetToInitialState();
            
            OnClear();
        }
        
        #endregion


        #region Запросы

        //Предусловие: очередь не пуста.
        public T GetFront()
        {
            T result = default;
            
            if (Items.Count == 0)
            {
                _getFrontStatus = GET_FRONT_ERR_QUEUE_EMPTY;
            }
            else
            {
                result = Items.First.Value;
                _getFrontStatus = GET_FRONT_OK;
            }
            
            return result;
        }

        public int Count() => Items.Count;
        
        #endregion


        #region Дополнительные запросы

        public int GetRemoveFrontStatus() => _removeFrontStatus;

        public int GetGetFrontStatus() => _getFrontStatus;
        
        #endregion
        
        
        protected virtual void OnClear() { }
        
        private void ResetToInitialState()
        {
            Items.Clear();
            
            _removeFrontStatus = REMOVE_FRONT_NIL;
            _getFrontStatus = GET_FRONT_NIL;
        }
    }

    public abstract class Queue<T> : ParentQueue<T>
    {
        // Конструктор
        
        // Постусловие: создана пустая очередь.
        // public Queue<T> Queue()
    }
    
    public abstract class Deque<T> : ParentQueue<T>
    {
        public const int REMOVE_TAIL_NIL = 0;
        public const int REMOVE_TAIL_OK = 1;
        public const int REMOVE_TAIL_ERR_QUEUE_EMPTY = 2;
        
        public const int GET_TAIL_NIL = 0;
        public const int GET_TAIL_OK = 1;
        public const int GET_TAIL_ERR_QUEUE_EMPTY = 2;
        
        // Конструктор
        
        // Постусловие: создана пустая очередь.
        // public Deque<T> Deque()
        
        
        // Команды
        
        // Постусловие: элемент добавлен в голову очереди.
        public abstract void AddHead(T item);
        
        // Предусловие: очередь не пуста.
        // Постусловие: элемент удалён из хвоста очереди.
        public abstract void RemoveTail();
        
        
        // Запросы
        
        // Предусловие: очередь не пуста.
        public abstract T GetTail();
        
        
        // Дополнительные запросы
        
        public abstract int GetRemoveTailStatus();
        
        public abstract int GetGetTailStatus();
    }
    
    
    public class QueueImpl<T> : Queue<T> { }


    public class DequeImpl<T> : Deque<T>
    {
        private int _removeTailStatus;
        private int _getTailStatus;

        public override void AddHead(T item) => Items.AddFirst(item);

        public override void RemoveTail()
        {
            if (Items.Count == 0)
            {
                _removeTailStatus = REMOVE_TAIL_ERR_QUEUE_EMPTY;
            }
            else
            {
                Items.RemoveLast();
                _removeTailStatus = REMOVE_TAIL_OK;
            }
        }

        public override T GetTail()
        {
            T result = default;
            
            if (Items.Count == 0)
            {
                _getTailStatus = GET_TAIL_ERR_QUEUE_EMPTY;
            }
            else
            {
                result = Items.Last.Value;
                _getTailStatus = GET_TAIL_OK;
            }

            return result;
        }

        public override int GetRemoveTailStatus() => _removeTailStatus;

        public override int GetGetTailStatus() => _getTailStatus;

        protected override void OnClear()
        {
            _removeTailStatus = REMOVE_TAIL_NIL;
            _getTailStatus = GET_TAIL_NIL;
        }
    }
}
