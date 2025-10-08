using System;

namespace OOAP1.Task9PowerSet
{
    public abstract class HashTable<T>
    {
        private const int PUT_NIL = 0;
        private const int PUT_OK = 1;
        private const int PUT_ERR_FULL = 2;
        private const int PUT_ERR_COLLISION = 3;
        
        private const int REMOVE_NIL = 0;
        private const int REMOVE_OK = 1;
        private const int REMOVE_ERR_EMPTY = 2;
        private const int REMOVE_ERR_NOT_EXISTS = 3;
        
        private Slot[] _slots;
        private int _putStatus;
        private int _removeStatus;
        private int _count;
        
        #region Конструктор
        
        // Постусловие: создана пустая хэш-таблица для capacity количества элементов.
        protected HashTable(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException("Capacity must be non-negative!");
            
            _slots = new Slot[capacity];
            
            ResetToInitialState();
        }
        
        #endregion
        

        #region Команды

        // Предусловие:
        //    - Хэш-таблица не полна.
        //    - В таблице имеется свободный слот для value.
        // Постусловие: в хэш-таблицу добавлен элемент value.
        public void Put(T value)
        {
            if (IsFull())
            {
                _putStatus = PUT_ERR_FULL;
                return;
            }
            
            int index = FindSlotIndex(value, slot => !slot.HasValue || slot.Value.Equals(value));

            bool isSlotFound = index >= 0;
            bool isSlotEmpty = isSlotFound && !_slots[index].HasValue;
            
            if (isSlotEmpty)
            {
                _slots[index] = new ValueSlot(value);
                ++_count;
            }

            if (isSlotFound)
                _putStatus = PUT_OK;
            else
                _putStatus = PUT_ERR_COLLISION;
        }

        // Предусловие:
        //    - Хэш-таблица не пуста.
        //    - В таблице имеется значение value.
        // Постусловие: из хэш-таблицы удален элемент value.
        public void Remove(T value)
        {
            if (IsEmpty())
            {
                _removeStatus = REMOVE_ERR_EMPTY;
                return;
            }
            
            int index = FindSlotIndex(value, slot => slot.HasValue && slot.Value.Equals(value));
            
            bool slotFount = index >= 0;
            
            if (slotFount)
            {
                _slots[index] = new EmptySlot();
                --_count;
                _removeStatus = REMOVE_OK;
            }
            else
            {
                _removeStatus = REMOVE_ERR_NOT_EXISTS; 
            }
        }

        // Постусловие: хэш-таблица очищена.
        public void Clear() => ResetToInitialState();
        
        #endregion


        #region Запросы
        
        public bool Contains(T value) => FindSlotIndex(value, slot => slot.HasValue && slot.Value.Equals(value)) != -1;
        
        public int Count() => _count;
        
        #endregion


        #region Дополнительные запросы
        
        public int GetPutStatus() => _putStatus;

        public int GetRemoveStatus() => _removeStatus;
        
        #endregion
        
        
        protected bool IsFull() => _count == _slots.Length;
        protected bool IsEmpty() => _count == 0;

        
        private void ResetToInitialState()
        {
            for(int i = 0; i < _slots.Length; i++) 
                _slots[i] = new EmptySlot();
            
            _count = 0;
            
            _putStatus = PUT_NIL;
            _removeStatus = REMOVE_NIL;
        }
        
        private int FindSlotIndex(T value, Predicate<Slot> comparer)
        {
            int capacity = _slots.Length;
            
            int hash1 = FirstHashFun(value, capacity);
            int hash2 = SecondHashFun(value, capacity);

            for (int iteration = 0, index = hash1 ; iteration < capacity; ++iteration, index = GetNextIndex(iteration, hash1, hash2, capacity))
            {
                if (comparer.Invoke(_slots[index]))
                    return index;
            }

            return -1;
        }
        
        private int GetNextIndex(int index, int hash1, int hash2, int capacity)
        {
            return (hash1 + index * hash2) % capacity;
        }
        
        private int FirstHashFun(T value, int capacity)
        {
            return Math.Abs(value.GetHashCode()) % capacity;
        }
        
        private int SecondHashFun(T value, int capacity)
        {
            return 1 + (Math.Abs(value.GetHashCode()) % (capacity - 2));
        }
        
        private abstract class Slot
        {
            public abstract T Value { get; }

            public abstract bool HasValue { get; }
        }
    
        private class ValueSlot : Slot
        {
            public override T Value { get; }
        
            public ValueSlot(T value)
            {
                Value = value;
            }
        
            public override bool HasValue => true;
        }
    
        private class EmptySlot : Slot
        {
            public override T Value { get; } = default;

            public override bool HasValue => false;
        }
    }
    
    public abstract class PowerSet<T> : HashTable<T>
    {
        // Конструктор
        // Постусловие: создано пустое множество для заданного количества элементов.
        protected PowerSet(int capacity) : base(capacity) { }
    }
    
    public class PowerSetImpl<T> : PowerSet<T>
    {
        public PowerSetImpl(int capacity) : base(capacity) { }
    }
}