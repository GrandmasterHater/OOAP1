using System;

namespace OOAP1.Task4DynArray
{
    public interface DynArray<T>
    {
        // Конструктор
        
        // Постусловие: создан пустой динамический массив ёмкостью 16 элементов.
        // DynArray<T> DynArray();
        
        // Предусловие: ёмкость больше или равна нулю.
        // Постусловие: создан пустой динамический массив заданной емкости.
        // Конструктор DynArray<T> DynArray(int capacity);
        
        // Команды

        // Предусловие: индекс в диапазоне от 0 до количества элементов (Size).
        // Постусловие: элемент вставлен по указанному индексу.
        void Insert(int index, T value); // -- вставка элемента по указанному индексу.
        
        // Предусловие: индекс должен быть неотрицательным и меньше текущего количества элементов.
        // Постусловие: элемент заменён по указанному индексу.
        void Replace(int index, T value); // -- замена элемента по указанному индексу.
        
        // Постусловие: элемент добавлен в конец списка.
        void Append(T value); // -- добавление элемента в конец списка. Тоже как удобство, поскольку это частный случай Insert.
        
        // Предусловие: индекс должен быть неотрицательным и меньше текущего количества элементов.
        // Постусловие: элемент по указанному индексу удалён.
        void Remove(int index); // -- удаление элемента по указанному индексу.
        
        // Постусловие: массив пуст.
        void Clear(); // -- очистить список.
        
        // Предусловие: ёмкость больше или равна нулю.
        // Постусловие:
        //     - Ёмкость массива изменена на заданную.
        //     - Элементы с индексом входящим в диапазон новой ёмкости массива не изменены.
        void Resize(int capacity); //  -- изменить ёмкость массива. (Дополнительная команда, скорее как удобство использования)
        
        
        // Запросы
        
        // Предусловие: индекс должен быть неотрицательным и меньше текущего числа элементов.
        T Get(int index); // -- получить элемент по указанному индексу.
        
        int Count(); // -- количество элементов в списке.
        
        int Capacity(); // -- текущая ёмкость списка.
        
        bool IsEmpty(); // -- проверяет пуст ли массив. Нужен для удобства работы с массивом.
        
        
        // Дополнительные запросы

        // Статус выполнения последней операции Insert.
        int GetInsertStatus(); // 0 - не вызывался, 1 - успешно, 2 - индекс вне диапазона.
        
        // Статус выполнения последней операции Replace.
        int GetReplaceStatus(); // 0 - не вызывался, 1 - успешно, 2 - индекс вне диапазона.
        
        // Статус выполнения последней операции Remove.
        int GetRemoveStatus(); // 0 - не вызывался, 1 - успешно, 2 - индекс вне диапазона.
        
        // Статус выполнения последней операции Resize.
        int GetResizeStatus(); // 0 - не вызывался, 1 - успешно, 2 - ёмкость ниже нуля.

        // Статус выполнения последней операции Get.
        int GetGetStatus(); // 0 - не вызывался, 1 - успешно, 2 - индекс вне диапазона.
    }

    public class DynArrayImpl<T> : DynArray<T>
    {
        private const int MIN_CAPACITY = 16;
        private const int CAPACITY_MULTIPLIER = 2;
        private const float CAPACITY_DIVIDER = 1.5f;
        private const float FILLING_PERCENT_FOR_REDUCE = 0.5f;
        
        private const int INSERT_NIL = 0;
        private const int INSERT_OK = 1;
        private const int INSER_ERR_INDEX_OUT_OF_RANGE = 2;
        
        private const int REPLACE_NIL = 0;
        private const int REPLACE_OK = 1;
        private const int REPLACE_ERR_INDEX_OUT_OF_RANGE = 2;
        
        private const int REMOVE_NIL = 0;
        private const int REMOVE_OK = 1;
        private const int REMOVE_ERR_INDEX_OUT_OF_RANGE = 2;
        
        private const int RESIZE_NIL = 0;
        private const int RESIZE_OK = 1;
        private const int RESIZE_ERR = 2;
        
        private const int GET_NIL = 0;
        private const int GET_OK = 1;
        private const int GET_ERR_INDEX_OUT_OF_RANGE = 2;
        
        private int _insertStatus;
        private int _replaceStatus;
        private int _removeStatus;
        private int _resizeStatus;
        private int _getGetStatus;
        
        private T [] _array;
        private int _count;
        private int _capacity;

        #region Конструкторы
        
        public DynArrayImpl()
        {
            CreateArray(MIN_CAPACITY);
        }
        
        public DynArrayImpl(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException("Capacity must be non-negative!");
            
            CreateArray(capacity);
        }
        
        #endregion

        #region Команды
        
        public void Insert(int index, T value)
        {
            if (index < 0 || index > _count)
            {
                _insertStatus = INSER_ERR_INDEX_OUT_OF_RANGE;
                return;
            }

            InsertToIndex(index, value);
            
            _insertStatus = INSERT_OK;
        }

        public void Replace(int index, T value)
        {
            if (index < 0 || index >= _count)
            {
                _replaceStatus = REPLACE_ERR_INDEX_OUT_OF_RANGE;
                return;
            }
            
            _array[index] = value;
            _replaceStatus = REPLACE_OK;
        }

        public void Append(T value) =>
            InsertToIndex(_count, value);
        

        public void Remove(int index)
        {
            if (index < 0 || index >= _count)
            {
                _removeStatus = REMOVE_ERR_INDEX_OUT_OF_RANGE;
                return;
            }
            
            if (index < _count - 1)
            {
                int nextIndex = index + 1;
                Array.Copy(_array, nextIndex, _array, index, _count - nextIndex);
            }
            else
            {
                _array[index] = default;
            }
            
            --_count;
            
            _removeStatus = REMOVE_OK;
                
            if (IsReduceCapacityRequired())
                ReduceCapacity();
        }

        public void Clear()
        {
            Array.Clear(_array, 0, _count);
            _count = 0;
            
            ResetStatuses();
        }

        public void Resize(int capacity)
        {
            if (capacity < 0)
            {
                _resizeStatus = RESIZE_ERR;
            }
            else
            {
                MakeArray(capacity);
                _resizeStatus = RESIZE_OK;
            }
        }
        
        #endregion
        

        #region Запросы

        public T Get(int index)
        {
            T result = default;
            
            if (index < 0 || index >= _count)
            {
                _getGetStatus = GET_ERR_INDEX_OUT_OF_RANGE;
            }
            else
            {
                result = _array[index];
                _getGetStatus = GET_OK;
            }
                
            return result;
        }

        public int Count() => _count;

        public int Capacity() => _capacity;

        public bool IsEmpty() => _count == 0;
        
        #endregion


        #region Дополнительные запросы

        public int GetInsertStatus() => _insertStatus;

        public int GetReplaceStatus() => _replaceStatus;

        public int GetRemoveStatus() => _removeStatus;
        
        public int GetResizeStatus() => _resizeStatus;

        public int GetGetStatus() => _getGetStatus;
        
        #endregion
        
        private void CreateArray(int capacity)
        {
            _capacity = capacity;
            _count = 0;
            _array = new T[capacity];
            
            ResetStatuses();
        }
        
        private void ResetStatuses()
        {
            _insertStatus  = INSERT_NIL; 
            _replaceStatus = REPLACE_NIL;
            _removeStatus  = REMOVE_NIL; 
            _resizeStatus  = RESIZE_NIL; 
            _getGetStatus  = GET_NIL; 
        }
        
        private bool IsExtendCapacityRequired() => _count == _capacity;
        
        private void ExtendCapacity()
        {
            MakeArray(_capacity * CAPACITY_MULTIPLIER);
        }
        
        private bool IsReduceCapacityRequired()
        {
            float fillingPercent = (float)_count / _capacity;
            
            return fillingPercent < FILLING_PERCENT_FOR_REDUCE;
        }
        
        private void ReduceCapacity()
        {
            int calculatedCapacity = (int)(_capacity / CAPACITY_DIVIDER);
            int newCapacity = calculatedCapacity < MIN_CAPACITY ? MIN_CAPACITY : calculatedCapacity;
            MakeArray(newCapacity);
        }
        
        private void MakeArray(int newCapacity)
        {
            Array.Resize(ref _array, newCapacity);
            _capacity = newCapacity;

            if (_count > newCapacity)
            {
                _count = newCapacity;
            }
        }

        private void InsertToIndex(int index, T value)
        {
            bool isExtendRequired = IsExtendCapacityRequired();
            
            if (isExtendRequired)
                ExtendCapacity();
            
            if (index != _count)
                Array.Copy(_array, index, _array, index + 1, _count - index);
            
            _array[index] = value;
            ++_count;
        }
    }
}