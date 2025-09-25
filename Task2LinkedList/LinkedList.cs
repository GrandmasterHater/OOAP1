namespace OOAP1.Task2LinkedList
{
    // Задание 2.2: В эффективной реализации операция tail не сводима к другим поскольку ожидается что, операция будет
    // выполнена за O(1) временной сложности. Если бы мы выражали tail через head и последовательные right, то придётся
    // проходить весь список и временная сложность была бы O(n).
    
    // Задание 2.3: Операцию поиска всех узлов теперь можно выразить через сочетание операций head, find и get.
    
    // Задание 2.1 
    public abstract class LinkedList<T>
    {
        public const int HEAD_NIL = 0; // Head еще не вызывалась
        public const int HEAD_OK = 1; // последний Head отработал нормально
        public const int HEAD_ERR_LIST_EMPTY = 2; // Список пуст

        public const int TAIL_NIL = 0; // Tail еще не вызывалась
        public const int TAIL_OK = 1; // последний Tail отработал нормально
        public const int TAIL_ERR_LIST_EMPTY = 2; // Список пуст

        public const int RIGHT_NIL = 0; // Right еще не вызывалась
        public const int RIGHT_OK = 1; // последний Right отработал нормально
        public const int RIGHT_ERR_CURSOR_POSITION = 2; // Курсор в конце списка
        public const int RIGHT_ERR_LIST_EMPTY = 3; // Список пуст

        public const int FIND_NIL = 0; // Find еще не вызывалась
        public const int FIND_OK = 1; // последний Find отработал нормально
        public const int FIND_NOT_FOUND = 2; // узел с искомым значением не найден
        public const int FIND_ERR_LIST_EMPTY = 3; // Список пуст

        public const int PUT_RIGHT_NIL = 0; // PutRight еще не вызывалась
        public const int PUT_RIGHT_OK = 1; // последний PutRight отработал нормально
        public const int PUT_RIGHT_ERR_LIST_EMPTY = 2; // Список пуст

        public const int PUT_LEFT_NIL = 0; // PutLeft еще не вызывалась
        public const int PUT_LEFT_OK = 1; // последний PutLeft отработал нормально
        public const int PUT_LEFT_ERR_LIST_EMPTY = 2; // Список пуст

        public const int REMOVE_NIL = 0; // Remove еще не вызывалась
        public const int REMOVE_OK = 1; // последний Remove отработал нормально
        public const int REMOVE_ERR_LIST_EMPTY = 2; // Список пуст

        public const int ADD_TO_EMPTY_NIL = 0; // AddToEmpty еще не вызывалась
        public const int ADD_TO_EMPTY_OK = 1; // последний AddToEmpty отработал нормально
        public const int ADD_TO_EMPTY_ERR_LIST_NOT_EMPTY = 2; // Список не пуст

        public const int ADD_TAIL_NIL = 0; // AddTail еще не вызывалась
        public const int ADD_TAIL_OK = 1; // последний AddTail отработал нормально
        public const int ADD_TAIL_ERR_LIST_EMPTY = 2; // Список пуст

        public const int REPLACE_NIL = 0; // Replace еще не вызывалась
        public const int REPLACE_OK = 1; // последний Replace отработал нормально
        public const int REPLACE_ERR_LIST_EMPTY = 2; // Список пуст

        public const int GET_NIL = 0; // Get еще не вызывалась
        public const int GET_OK = 1; // последний Get отработал нормально
        public const int GET_ERR_LIST_EMPTY = 2; // Список пуст


        // Конструктор 

        // Постусловие: создан новый пустой список.
        // LinkedList<T> LinkedList()


        // Команды

        // Предусловие: список не пуст.
        // Постусловие: курсор указывает на первый элемент списка.
        public abstract void Head(); // -- установить курсор на первый узел в списке; 

        // Предусловие: список не пуст.
        // Постусловие: курсор указывает на последний элемент списка.
        public abstract void Tail(); // -- установить курсор на последний узел в списке;

        // Предусловие: курсор указывает на узел и он не является последним.
        // Постусловие: курсор установлен на правый соседний узел относительно текущего
        public abstract void Right(); // -- сдвинуть курсор на один узел вправо;

        // Предусловие: курсор указывает на узел.
        // Постусловие: курсор установлен на следующий узел относительно текущего с искомым значением. Если такого узла нет, то курсор позицию не меняет.
        public abstract void Find(T value); // -- установить курсор на следующий узел с искомым значением (по отношению к текущему узлу);

        // Предусловие: курсор указывает на узел.
        // Постусловие: в список добавлено новое значение справа от текущего узла.
        public abstract void PutRight(T value); // -- вставить следом за текущим узлом новый узел с заданным значением;

        // Предусловие: курсор указывает на узел.
        // Постусловие: в список добавлено новое значение слева от текущего узла.
        public abstract void PutLeft(T value); // -- вставить перед текущим узлом новый узел с заданным значением;

        // Предусловие: курсор указывает на узел.
        // Постусловие:
        //     - Из списка удален текущий узел.
        //     - Курсор смещён к правому соседу, если он есть, в противном случае - к левому соседу. Если список пуст, то курсор не установлен.
        public abstract void Remove(); // -- удалить текущий узел

        // Постусловие: из списка удалены все узлы с указанным значением.
        public abstract void RemoveAll(T value); // -- удалить в списке все узлы с заданным значением;

        // Постусловие: список пуст.
        public abstract void Clear(); // -- очистить список;

        // Предусловие: указатель курсора не установлен.
        // Постусловие: 
        //     - В список добавлено новое значение.
        //     - Курсор указывает на добавленное значение.
        public abstract void AddToEmpty(T value); // -- добавить новый узел в пустой список.

        // Предусловие: список не пуст.
        // Постусловие: в конец списка добавлено новое значение.
        public abstract void AddTail(T value); // -- добавить новый узел в хвост списка;

        // Предусловие: курсор указывает на узел.
        // Постусловие: значение текущего узла заменено на заданное
        public abstract void Replace(T value); // -- заменить значение текущего узла на заданное;

        // Запросы

        public abstract int Size(); // -- посчитать количество узлов в списке;

        // Предусловие: курсор указывает на узел.
        public abstract T Get(); // -- получить значение текущего узла;

        public abstract bool IsHead(); // -- находится ли курсор в начале списка?

        public abstract bool IsTail(); // -- находится ли курсор в конце списка?

        public abstract bool IsValue(); // -- установлен ли курсор на какой-либо узел в списке?

        // Дополнительные запросы
        
        public abstract int GetHeadStatus(); // -- возвращает статус завершения последней операции Head()

        public abstract int GetTailStatus(); // -- возвращает статус завершения последней операции Tail()

        public abstract int GetRightStatus(); // -- возвращает статус завершения последней операции Right()

        public abstract int GetFindStatus(); // -- возвращает статус завершения последней операции Find()

        public abstract int GetPutRightStatus(); // -- возвращает статус завершения последней операции PutRight()

        public abstract int GetPutLeftStatus(); // -- возвращает статус завершения последней операции PutLeft()

        public abstract int GetRemoveStatus(); // -- возвращает статус завершения последней операции Remove()

        public abstract int GetAddToEmptyStatus(); // -- возвращает статус завершения последней операции AddToEmpty()

        public abstract int GetAddTailStatus(); // -- возвращает статус завершения последней операции AddTail()

        public abstract int GetReplaceStatus(); // -- возвращает статус завершения последней операции Replace()

        public abstract int GetGetStatus(); // -- возвращает статус завершения последней операции (для любой команды)
    }

    public class LinkedListImpl<T> : LinkedList<T>
    {
        private Node _head = null;
        private Node _tail = null;
        private Node _cursor = null;
        private int _size = 0;
        
        // поля для хранения статусов команд
        private int _headStatus;
        private int _tailStatus;
        private int _rightStatus;               
        private int _findStatus;
        private int _putRightStatus;
        private int _putLeftStatus;
        private int _removeStatus;
        private int _addToEmptyStatus;
        private int _addTailStatus;
        private int _replaceStatus;
        private int _getGetStatus;

        public LinkedListImpl() =>
            ResetStateToInitial();
        
        
        #region Команды
        
        public override void Head()
        {
            if (_size > 0)
            {
                _cursor = _head;
                _headStatus = HEAD_OK;
            }
            else
            {
                _headStatus = HEAD_ERR_LIST_EMPTY;
            }
        }

        public override void Tail()
        {
            if (_size > 0)
            {
                _cursor = _tail;
                _tailStatus = TAIL_OK;
            }
            else
            {
                _tailStatus = TAIL_ERR_LIST_EMPTY;
            }
        }

        public override void Right()
        {
            bool isCursorOnTail = _cursor == _tail;
            
            if (IsValue() && !isCursorOnTail)
            {
                _cursor = _cursor.Next;
                _rightStatus = RIGHT_OK;
            }
            else if (isCursorOnTail)
            {
                _rightStatus = RIGHT_ERR_CURSOR_POSITION;
            }
            else
            {
                _rightStatus = RIGHT_ERR_LIST_EMPTY;
            }
        }

        public override void Find(T value)
        {
            if (!IsValue())
            {
                _findStatus = FIND_ERR_LIST_EMPTY;
                return;
            }

            Node current;
            
            for (current = _cursor; current != null; current = current.Next)
            {
                if (current.Value.Equals(value))
                    break;
            }
            
            if (current != null)
            {
                _cursor = current;
                _findStatus = FIND_OK;
            }
            else
            {
                _findStatus = FIND_NOT_FOUND;
            }
        }

        public override void PutRight(T value)
        {
            if (!IsValue())
            {
                _putRightStatus = PUT_RIGHT_ERR_LIST_EMPTY;
                return;
            }
            
            InsertAfter(_cursor, value);
            _putRightStatus = PUT_RIGHT_OK;
        }

        public override void PutLeft(T value)
        {
            if (!IsValue())
            {
                _putLeftStatus = PUT_LEFT_ERR_LIST_EMPTY;
                return;
            }
            
            InsertBefore(_cursor, value);
            _putLeftStatus = PUT_LEFT_OK;
        }

        public override void Remove()
        {
            if (!IsValue())
            {
                _removeStatus = REMOVE_ERR_LIST_EMPTY;
                return;
            }
            
            Node nextCursor = GetNextCursorAfterDeleteNode();
            
            DeleteNode(_cursor);
            _removeStatus = REMOVE_OK;
            
            _cursor = nextCursor;
        }

        public override void RemoveAll(T value)
        {
            if (!IsValue())
                return;

            for (Node node = _head, nextNode = node?.Next; node != null; node = nextNode, nextNode = nextNode?.Next)
            {
                bool isNodeCursor = node == _cursor;
                bool isEquals = node.Value.Equals(value);
                Node nextCursor = _cursor;

                if (isEquals && isNodeCursor)
                    nextCursor = GetNextCursorAfterDeleteNode();

                if (isEquals)
                    DeleteNode(node);
                
                _cursor = nextCursor;
            }
        }

        public override void Clear() => 
            ResetStateToInitial();
        

        public override void AddToEmpty(T value)
        {
            if (IsValue())
            {
                _addToEmptyStatus = ADD_TO_EMPTY_ERR_LIST_NOT_EMPTY;
            }
            else
            {
                _head = new Node(value);
                _tail = _head;
                _size = 1;
                _addToEmptyStatus = ADD_TO_EMPTY_OK;
            }
        }

        public override void AddTail(T value)
        {
            if (IsValue())
            {
                InsertAfter(_tail, value);
                _addTailStatus = ADD_TAIL_OK;
            }
            else
            {
                _addTailStatus = ADD_TAIL_ERR_LIST_EMPTY;
            }
        }

        public override void Replace(T value)
        {
            if (IsValue())
            {
                _cursor.Value = value;
                _replaceStatus = REPLACE_OK;
            }
            else
            {
                _replaceStatus = REPLACE_ERR_LIST_EMPTY;
            }
        }
        
        #endregion

        #region Запросы

        public override int Size() => _size;

        public override T Get()
        {
            T result = default;
            
            if (IsValue())
            {
                result = _cursor.Value;
                _getGetStatus = GET_OK;
            }
            else
            {
                _getGetStatus = GET_ERR_LIST_EMPTY;
            }

            return result;
        }

        public override bool IsHead() => _cursor == _head;

        public override bool IsTail() => _cursor == _tail;

        public override bool IsValue() => _cursor != null;
        
        #endregion

        #region Дополнительные запросы

        public override int GetHeadStatus() => _headStatus;

        public override int GetTailStatus() => _tailStatus;

        public override int GetRightStatus() => _rightStatus;

        public override int GetFindStatus() => _findStatus;

        public override int GetPutRightStatus() => _putRightStatus;

        public override int GetPutLeftStatus() => _putLeftStatus;

        public override int GetRemoveStatus() => _removeStatus;

        public override int GetAddToEmptyStatus() => _addToEmptyStatus;

        public override int GetAddTailStatus() => _addTailStatus;
        public override int GetReplaceStatus() => _replaceStatus;

        public override int GetGetStatus() => _getGetStatus;
        
        #endregion
        
        private void ResetStateToInitial()
        {
            _head = null;
            _tail = null;
            _cursor = null;
            _size = 0;
            
            _headStatus = HEAD_NIL;
            _tailStatus = TAIL_NIL;
            _rightStatus = RIGHT_NIL;                                                                    
            _findStatus = FIND_NIL;
            _putRightStatus = PUT_RIGHT_NIL;
            _putLeftStatus = PUT_LEFT_NIL;
            _removeStatus = REMOVE_NIL;
            _addToEmptyStatus = ADD_TO_EMPTY_NIL;
            _addTailStatus = ADD_TAIL_NIL;
            _replaceStatus = REPLACE_NIL;
            _getGetStatus = GET_NIL;
        }
        
        private void InsertAfter(Node currentNode, T value)
        {
            Node newNode = new Node(value);

            newNode.Next = currentNode.Next;
            newNode.Prev = currentNode;
            currentNode.Next = newNode;
            
            if (currentNode == _tail)
                _tail = newNode;
            
            ++_size;
        }
        
        private void InsertBefore(Node currentNode, T value)
        {
            Node newNode = new Node(value);

            newNode.Next = currentNode;
            newNode.Prev = currentNode.Prev;
            currentNode.Prev = newNode;
            
            if (_head == currentNode)
                _head = newNode;
            
            ++_size;
        }
        
        private void DeleteNode(Node node)
        {
            if (node.Prev != null)
                node.Prev.Next = node.Next;
            else
                _head = node.Next;
            
            if (node.Next != null)
                node.Next.Prev = node.Prev;
            else
                _tail = node.Prev;

            --_size;
        }
        
        private Node GetNextCursorAfterDeleteNode()
        {
            Node nextCursor;

            if (_cursor.Next != null)
                nextCursor = _cursor.Next;
            else if (_cursor.Prev != null)
                nextCursor = _cursor.Prev;
            else 
                nextCursor = null;

            return nextCursor;
        }
        
        private class Node
        {
            public T Value;
            public Node Next;
            public Node Prev;

            public Node(T value)
            {
                Value = value;
            }
        }
    }
}
