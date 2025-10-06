using System;
using System.Collections.Generic;

namespace OOAP1.Task8NativeDictionary
{
    public interface NativeDictionary<T>
    {
        // Конструктор
        // Постусловие: создан пустой словарь.
        // public NativeDictionary<T> NativeDictionary();
        
        
        // Команды 
        
        // Предусловие: нет такого key в словаре.
        // Постусловие: значение value добавлено в словарь.
        void Put(string key, T value);
        
        // Предусловие: есть такой key в словаре.
        // Постусловие: значение value обновлено в словаре.
        void Update(string key, T value);
        
        // Предусловие: есть такой key в словаре.
        // Постусловие: значение value удалено из словаря.
        void Remove(string key);
        
        
        // Запросы
        
        // Предусловие: есть такой key в словаре.
        T Get(string key);
        
        bool HasKey(string key); // Проверка, есть ли такой key в словаре (добавлен для удобства, можно выразить через Get + чтение статуса get)
        
        
        // Дополнительные запросы
        
        // (NIL - Put не вызывался; OK - успешно; ERR_KEY_EXISTS - ошибка, такой ключ уже есть в словаре, ERR_COLLISION - ошибка, возникла коллизия, не удалось вставить пару ключ/значение)
        int GetPutStatus();
        
        // (NIL - Update не вызывался; OK - успешно; ERR_NOT_FOUND - ошибка, не удалось найти элемент с таким ключом в словаре)
        int GetUpdateStatus();
        
        // (NIL - Remove не вызывался; OK - успешно; ERR_NOT_FOUND - ошибка, не удалось найти элемент с таким ключом в словаре)
        int GetRemoveStatus();
        
        // (NIL - Get не вызывался; OK - успешно; ERR_NOT_FOUND - ошибка, не удалось найти элемент с таким ключом в словаре)
        int GetGetStatus();
    }

    public class NativeDictionaryImpl<T> : NativeDictionary<T>
    {
        private const int CAPACITY_MULTIPLIER = 2;
        private const double MAX_LOAD_FACTOR = 0.7;
        
        private static readonly int[] PRIMES = {
            17, 31, 61, 127, 251, 509, 1021, 2039, 4093, 8191, 
            16381, 32749, 65521, 131071, 262139, 524287, 1048573
        };

        private string DEL_MARK = "del";
        
        public const int PUT_NIL = 0;
        public const int PUT_OK = 1;
        public const int PUT_ERR_KEY_EXISTS = 2;
        public const int PUT_ERR_COLLISION = 3;
        
        public const int UPDATE_NIL = 0;
        public const int UPDATE_OK = 1;
        public const int UPDATE_ERR_KEY_NOT_EXISTS = 2;
        
        public const int REMOVE_NIL = 0;
        public const int REMOVE_OK = 1;
        public const int REMOVE_ERR_KEY_NOT_EXISTS = 2;
        
        public const int GET_NIL = 0;
        public const int GET_OK = 1;
        public const int GET_ERR_KEY_NOT_EXISTS = 2;
        
        private string [] _slots;
        private T [] _values;
        private int _count;
        private int _primeIndex;
        
        private int _putStatus;
        private int _updateStatus;
        private int _removeStatus;
        private int _getStatus;

        public NativeDictionaryImpl()
        {
            _primeIndex = 0;
            int capacity = PRIMES[_primeIndex];
            
            _slots = new string[capacity];
            _values = new T[capacity];
            
            _count = 0;
            
            _putStatus = PUT_NIL;
            _updateStatus = UPDATE_NIL;
            _removeStatus = REMOVE_NIL;
            _getStatus = GET_NIL;
        }
        
        public void Put(string key, T value)
        {
            if (HasKey(key))
            {
                _putStatus = PUT_ERR_KEY_EXISTS;
                return;
            }
            
            if (IsExtendCapacityRequired(_count, _values.Length))
                ExtendCapacity();
            
            int index = FindSlotIndex(key, slot => slot == null || slot == DEL_MARK);

            if (index < 0)
            {
                ExtendCapacity();
                index = FindSlotIndex(key, slot => slot == null || slot == DEL_MARK);
            }

            if (index < 0)
            {
                _putStatus = PUT_ERR_COLLISION;
            }
            else
            {
                _slots[index] = key;
                _values[index] = value;
                _count++;

                _putStatus = PUT_OK;
            }
        }

        public void Update(string key, T value)
        {
            if (HasKey(key))
            {
                int index = FindSlotIndex(key, slot => slot != null && slot != DEL_MARK && slot.Equals(key));
                
                _slots[index] = key;
                _values[index] = value;
                
                _updateStatus = UPDATE_OK;
            }
            else
            {
                _updateStatus = UPDATE_ERR_KEY_NOT_EXISTS;
            }
        }

        public void Remove(string key)
        {
            if (HasKey(key))
            {
                int index = FindSlotIndex(key, slot => slot != null && slot != DEL_MARK && slot.Equals(key));

                _slots[index] = DEL_MARK;
                _values[index] = default;
                _count--;

                _removeStatus = REMOVE_OK;
            }
            else
            {
                _removeStatus = REMOVE_ERR_KEY_NOT_EXISTS;
            }
        }

        public T Get(string key)
        {
            T result = default;
            
            if (HasKey(key))
            {
                int index = FindSlotIndex(key, slot => slot != null && slot != DEL_MARK && slot.Equals(key));

                result = _values[index];

                _getStatus = GET_OK;
            }
            else
            {
                _getStatus = GET_ERR_KEY_NOT_EXISTS;
            }

            return result;
        }

        public bool HasKey(string key) => 
            FindSlotIndex(key, slot => slot != null && slot.Equals(key)) >= 0;

        public int GetPutStatus() => _putStatus;

        public int GetUpdateStatus() => _updateStatus;

        public int GetRemoveStatus() => _removeStatus;

        public int GetGetStatus() => _getStatus;
        
        private int FindSlotIndex(string key, Predicate<string> comparer)
        {
            int capacity = _slots.Length;
            
            int hash1 = FirstHashFun(key, capacity);
            int hash2 = SecondHashFun(key, capacity);

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
        
        private int FirstHashFun(string key, int capacity)
        {
            return Math.Abs(key.GetHashCode()) % capacity;
        }
        
        private int SecondHashFun(string key, int capacity)
        {
            return 1 + (Math.Abs(key.GetHashCode()) % (capacity - 2));
        }
        
        private bool IsExtendCapacityRequired(int count, int capacity) => (double)count / capacity >= MAX_LOAD_FACTOR;
        
        private void ExtendCapacity()
        {
            ++_primeIndex;
            
            int newCapacity = _primeIndex < PRIMES.Length ? PRIMES[_primeIndex] : _slots.Length * CAPACITY_MULTIPLIER;
            
            MakeArrays(newCapacity);
        }
        
        private void MakeArrays(int newCapacity)
        {
            string [] oldSlots = _slots;
            T [] oldValues = _values;
            
            _slots = new string[newCapacity];
            _values = new T[newCapacity];

            for (var index = 0; index < oldSlots.Length; index++)
            {
                if (oldSlots[index] != null)
                {
                    int newIndex = FindSlotIndex(oldSlots[index], slot => slot == null);
                    _slots[newIndex] = oldSlots[index];
                    _values[newIndex] = oldValues[index];
                }
            }
        }
    }
}