using System;
using System.Collections.Generic;

namespace OOAP1
{
    public class BoundedStack<T>
    {
        public const int PUSH_NIL = 0; // Push() ещё не вызывалась
        public const int PUSH_OK = 1; // последняя Push() отработала нормально
        public const int PUSH_FULL = 2; // Стек полон
        
        public const int POP_NIL = 0; // push() ещё не вызывалась
        public const int POP_OK = 1; // последняя pop() отработала нормально
        public const int POP_ERR = 2; // стек пуст

        public const int PEEK_NIL = 0; // push() ещё не вызывалась
        public const int PEEK_OK = 1; // последняя peek() вернула корректное значение 
        public const int PEEK_ERR = 2; // стек пуст
        
        public const int DEFAULT_SIZE = 32; // Размер стека по умолчанию
        
        // скрытые поля
        private List<T> _stack; 
        private int _peekStatus; 
        private int _popStatus;
        private int _pushStatus;
        private int _capacity;
        
        private BoundedStack(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException("Stack capacity must be non-negative!");
            
            _capacity = capacity;
            _stack = new List<T>(capacity);
        }
        
        // Постусловие: стек создан
        public static BoundedStack<T> Create() => new BoundedStack<T>(DEFAULT_SIZE); 
        
        // Предусловие: размер стека больше нуля
        // Постусловие: стек создан
        public static BoundedStack<T> Create(int capacity) => new BoundedStack<T>(capacity); 

        // Предусловие: в стеке есть свободное место?
        //
        // Постусловие (в стеке есть место):
        //    - Элемент добавлен в стек.
        //    - Выставлен статус PUSH_OK
        //    - Размер стека увеличился на 1 (Size())
        //
        // Постусловие (в стеке нет свободного места):
        //    - Выставлен статус PUSH_FULL
        //    - Верхний элемент в стеке не изменился
        //    - Размер стека не изменился (Size())
        public void Push(T value)
        {
            if (_stack.Count < _capacity)
            {
                _stack.Add(value);
                _pushStatus = PUSH_OK;
            }
            else
            {
                _pushStatus = PUSH_FULL;
            }
        }

        // Предусловие: в стеке есть элементы?
        //
        // Постусловие (в стеке есть элементы):
        //    - Удалён верхний элемент из стека.
        //    - Выставлен статус POP_OK
        //    - Размер стека уменьшился на 1 (Size())
        //
        // Постусловие (стек пуст):
        //    - Выставлен статус POP_ERR
        //    - Размер стека не изменился (Size())
        public void Pop()
        {
            if (_stack.Count > 0)
            {
                _stack.RemoveAt(_stack.Count - 1);
                _popStatus = POP_OK;
            }
            else
            {
                _popStatus = POP_ERR;
            }
        }
        
        // Постусловие:
        //    - Стек пуст
        //    - Статус push = PUSH_NIL
        //    - Статус pop = POP_NIL
        //    - Статус peek = PEEK_NIL
        //    - Размер стека 0
        public void Clear()
        {
            _stack.Clear();

            _peekStatus = PEEK_NIL;
            _popStatus = POP_NIL;
            _pushStatus = PUSH_NIL;
        }

        // Предусловие: в стеке есть элементы?
        //
        // Постусловие (в стеке есть элементы):
        //    - Возвращён верхний элемент из стека.
        //    - Выставлен статус POP_OK
        //
        // Постусловие (стек пуст):
        //    - Выставлен статус POP_ERR
        //
        // Постусловие (общее):
        //    - Размер стека не изменился (Size())
        public T Peek()
        {
            T result = default;
            
            if (_stack.Count > 0)
            {
                result = _stack[_stack.Count - 1];
                _peekStatus = PEEK_OK;
            }
            else
            {
                _peekStatus = PEEK_ERR;
            }

            return result;
        }

        public int Size() => _stack.Count;

        public int GetPopStatus() => _popStatus;

        public int GetPeekStatus() => _peekStatus;
        
        public int GetPushStatus() => _pushStatus;
    }
}