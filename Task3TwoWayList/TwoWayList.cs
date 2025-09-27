using OOAP1.Task2LinkedList;

namespace OOAP1.Task3TwoWayList
{
    // АТД базового списка определяющего набор общих методов
    public abstract class ParentList<T>
    {
        #region Статусы
        
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

        public const int REPLACE_NIL = 0; // Replace еще не вызывалась
        public const int REPLACE_OK = 1; // последний Replace отработал нормально
        public const int REPLACE_ERR_LIST_EMPTY = 2; // Список пуст

        public const int GET_NIL = 0; // Get еще не вызывалась
        public const int GET_OK = 1; // последний Get отработал нормально
        public const int GET_ERR_LIST_EMPTY = 2; // Список пуст
        
        #endregion
        
        private Node _head = null;
        private Node _tail = null;
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
        private int _replaceStatus;
        private int _getGetStatus;
        
        // Свойство для управления курсором из наследников
        protected Node Cursor { get; set; }
        

        // Конструктор 

        // Постусловие: создан новый пустой список.
        protected ParentList()
        {
            Clear();
        }


        #region Команды

        // Предусловие: список не пуст.
        // Постусловие: курсор указывает на первый элемент списка.
        public virtual void Head() // -- установить курсор на первый узел в списке; 
        {
            if (_size > 0)
            {
                Cursor = _head;
                _headStatus = HEAD_OK;
            }
            else
            {
                _headStatus = HEAD_ERR_LIST_EMPTY;
            }
        }

        // Предусловие: список не пуст.
        // Постусловие: курсор указывает на последний элемент списка.
        public virtual void Tail() // -- установить курсор на последний узел в списке;
        {
            if (_size > 0)
            {
                Cursor = _tail;
                _tailStatus = TAIL_OK;
            }
            else
            {
                _tailStatus = TAIL_ERR_LIST_EMPTY;
            }
        }

        // Предусловие: курсор указывает на узел и он не является последним.
        // Постусловие: курсор установлен на правый соседний узел относительно текущего
        public virtual void Right() // -- сдвинуть курсор на один узел вправо;
        {
            bool isCursorOnTail = Cursor == _tail;
            
            if (IsValue() && !isCursorOnTail)
            {
                Cursor = Cursor.Next;
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

        // Предусловие: курсор указывает на узел.
        // Постусловие: курсор установлен на следующий узел относительно текущего с искомым значением. Если такого узла нет, то курсор позицию не меняет.
        public virtual void Find(T value) // -- установить курсор на следующий узел с искомым значением (по отношению к текущему узлу);
        {
            if (!IsValue())
            {
                _findStatus = FIND_ERR_LIST_EMPTY;
                return;
            }

            Node current;
            
            for (current = Cursor; current != null; current = current.Next)
            {
                if (current.Value.Equals(value))
                    break;
            }
            
            if (current != null)
            {
                Cursor = current;
                _findStatus = FIND_OK;
            }
            else
            {
                _findStatus = FIND_NOT_FOUND;
            }
        }

        // Предусловие: курсор указывает на узел.
        // Постусловие: в список добавлено новое значение справа от текущего узла.
        public virtual void PutRight(T value) // -- вставить следом за текущим узлом новый узел с заданным значением;
        {
            if (!IsValue())
            {
                _putRightStatus = PUT_RIGHT_ERR_LIST_EMPTY;
                return;
            }
            
            InsertAfter(Cursor, value);
            _putRightStatus = PUT_RIGHT_OK;
        }

        // Предусловие: курсор указывает на узел.
        // Постусловие: в список добавлено новое значение слева от текущего узла.
        public virtual void PutLeft(T value) // -- вставить перед текущим узлом новый узел с заданным значением;
        {
            if (!IsValue())
            {
                _putLeftStatus = PUT_LEFT_ERR_LIST_EMPTY;
                return;
            }
            
            InsertBefore(Cursor, value);
            _putLeftStatus = PUT_LEFT_OK;
        }

        // Предусловие: курсор указывает на узел.
        // Постусловие:
        //     - Из списка удален текущий узел.
        //     - Курсор смещён к правому соседу, если он есть, в противном случае - к левому соседу. Если список пуст, то курсор не установлен.
        public virtual void Remove() // -- удалить текущий узел
        {
            if (!IsValue())
            {
                _removeStatus = REMOVE_ERR_LIST_EMPTY;
                return;
            }
            
            Node nextCursor = GetNextCursorAfterDeleteNode();
            
            DeleteNode(Cursor);
            _removeStatus = REMOVE_OK;
            
            Cursor = nextCursor;
        }

        // Постусловие: из списка удалены все узлы с указанным значением.
        public virtual void RemoveAll(T value) // -- удалить в списке все узлы с заданным значением;
        {
            if (!IsValue())
                return;

            for (Node node = _head, nextNode = node?.Next; node != null; node = nextNode, nextNode = nextNode?.Next)
            {
                bool isNodeCursor = node == Cursor;
                bool isEquals = node.Value.Equals(value);
                Node nextCursor = Cursor;

                if (isEquals && isNodeCursor)
                    nextCursor = GetNextCursorAfterDeleteNode();

                if (isEquals)
                    DeleteNode(node);
                
                Cursor = nextCursor;
            }
        }

        // Постусловие: список пуст.
        public virtual void Clear() => // -- очистить список;
            ResetStateToInitial();

        // Предусловие: указатель курсора не установлен.
        // Постусловие: 
        //     - В список добавлено новое значение.
        //     - Курсор указывает на добавленное значение.
        public virtual void AddToEmpty(T value) // -- добавить новый узел в пустой список.
        {
            if (IsValue())
                _addToEmptyStatus = ADD_TO_EMPTY_ERR_LIST_NOT_EMPTY;
            else
                AddFirstValue(value);
        }
        
        // Постусловие: в конец списка добавлено новое значение.
        public virtual void AddTail(T value) // -- добавить новый узел в хвост списка;
        {
            if (IsValue())
                InsertAfter(_tail, value);
            else
                AddFirstValue(value);
        }

        // Предусловие: курсор указывает на узел.
        // Постусловие: значение текущего узла заменено на заданное
        public virtual void Replace(T value) // -- заменить значение текущего узла на заданное;
        {
            if (IsValue())
            {
                Cursor.Value = value;
                _replaceStatus = REPLACE_OK;
            }
            else
            {
                _replaceStatus = REPLACE_ERR_LIST_EMPTY;
            }
        }
        
        #endregion

        #region Запросы

        public virtual int Size() => _size; // -- посчитать количество узлов в списке;

        // Предусловие: курсор указывает на узел.
        public virtual T Get() // -- получить значение текущего узла;
        {
            T result = default;
            
            if (IsValue())
            {
                result = Cursor.Value;
                _getGetStatus = GET_OK;
            }
            else
            {
                _getGetStatus = GET_ERR_LIST_EMPTY;
            }

            return result;
        }

        public virtual bool IsHead() => Cursor == _head; // -- находится ли курсор в начале списка?

        public virtual bool IsTail() => Cursor == _tail; // -- находится ли курсор в конце списка?

        public virtual bool IsValue() => Cursor != null; // -- установлен ли курсор на какой-либо узел в списке?
        
        #endregion

        #region Дополнительные запросы
        
        public virtual int GetHeadStatus() => _headStatus; // -- возвращает статус завершения последней операции Head()

        public virtual int GetTailStatus() => _tailStatus; // -- возвращает статус завершения последней операции Tail()

        public virtual int GetRightStatus() => _rightStatus; // -- возвращает статус завершения последней операции Right()

        public virtual int GetFindStatus() => _findStatus; // -- возвращает статус завершения последней операции Find()

        public virtual int GetPutRightStatus() => _putRightStatus; // -- возвращает статус завершения последней операции PutRight()

        public virtual int GetPutLeftStatus() => _putLeftStatus; // -- возвращает статус завершения последней операции PutLeft()

        public virtual int GetRemoveStatus() => _removeStatus; // -- возвращает статус завершения последней операции Remove()

        public virtual int GetAddToEmptyStatus() => _addToEmptyStatus; // -- возвращает статус завершения последней операции AddToEmpty()

        public virtual int GetReplaceStatus() => _replaceStatus; // -- возвращает статус завершения последней операции Replace()

        public virtual int GetGetStatus() => _getGetStatus; // -- возвращает статус завершения последней операции (для любой команды)
        
        #endregion
        
        private void ResetStateToInitial()
        {
            _head = null;
            _tail = null;
            Cursor = null;
            _size = 0;
            
            _headStatus = HEAD_NIL;
            _tailStatus = TAIL_NIL;
            _rightStatus = RIGHT_NIL;                                                                    
            _findStatus = FIND_NIL;
            _putRightStatus = PUT_RIGHT_NIL;
            _putLeftStatus = PUT_LEFT_NIL;
            _removeStatus = REMOVE_NIL;
            _addToEmptyStatus = ADD_TO_EMPTY_NIL;
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

            if (Cursor.Next != null)
                nextCursor = Cursor.Next;
            else if (Cursor.Prev != null)
                nextCursor = Cursor.Prev;
            else 
                nextCursor = null;

            return nextCursor;
        }
        
        private void AddFirstValue(T value)
        {
            _head = new Node(value);
            _tail = _head;
            _size = 1;
            _addToEmptyStatus = ADD_TO_EMPTY_OK;
        }
        
        protected class Node
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
    
    // АТД и реализация LinkedList
    public class LinkedList<T> : ParentList<T> { }
    
    // АТД и реализация TwoWayList
    public class TwoWayList<T> : ParentList<T>
    {
        public const int LEFT_NIL = 0; // Left еще не вызывалась
        public const int LEFT_OK = 1; // последний Left отработал нормально
        public const int LEFT_ERR_CURSOR_POSITION = 2; // Курсор в начале списка
        public const int LEFT_ERR_LIST_EMPTY = 3; // Список пуст
        
        private int _leftStatus;

        public override void Clear()
        {
            base.Clear();
            
            _leftStatus = LEFT_NIL;
        }
        
        // Предусловие: курсор указывает на узел и он не является первым в списке.
        // Постусловие: курсор установлен на левый соседний узел относительно текущего.
        public void Left() // -- сдвинуть курсор на один узел влево;
        {
            bool isCursorOnHead = IsHead();
            
            if (IsValue() && !isCursorOnHead)
            {
                Cursor = Cursor.Prev;
                _leftStatus = LEFT_OK;
            }
            else if (isCursorOnHead)
            {
                _leftStatus = LEFT_ERR_CURSOR_POSITION;
            }
            else
            {
                _leftStatus = LEFT_ERR_LIST_EMPTY;
            }
        }

        public int GetLeftStatus() => _leftStatus; // -- возвращает статус завершения последней операции Left()
    }
}