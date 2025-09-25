namespace OOAP1.Task2LinkedList
{
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

        public const int FIND_NIL = 0; // Find еще не вызывалась
        public const int FIND_OK = 1; // последний Find отработал нормально
        public const int FIND_ERR_LIST_EMPTY = 2; // Список пуст

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
        public abstract void Tail();

        // Предусловие: курсор не соответствует концу списка.
        // Постусловие: курсор установлен на правый соседний узел относительно текущего
        public abstract void Right();

        // Предусловие: список не пуст.
        // Постусловие: курсор установлен на следующий узел относительно текущего с искомым значением
        public abstract void Find(T value);

        // Предусловие: список не пуст.
        // Постусловие: в список добавлено новое значение справа от текущего узла.
        public abstract void PutRight(T value);

        // Предусловие: список не пуст.
        // Постусловие: в список добавлено новое значение слева от текущего узла.
        public abstract void PutLeft(T value);

        // Предусловие: список не пуст.
        // Постусловие:
        //     - Из списка удален текущий узел.
        //     - Курсор смещён к правому соседу, если он есть, в противно случае - к левому соседу. Если список пуст, то курсор не установлен.
        public abstract void Remove();

        // Постусловие: из списка удалены все узлы с указанным значением.
        public abstract void RemoveAll(T value);

        // Постусловие: список пуст.
        public abstract void Clear();

        // Предусловие: список пуст.
        // Постусловие: 
        //     - В список добавлено новое значение.
        //     - Курсор указывает на добавленное значение.
        public abstract void AddToEmpty();

        // Предусловие: список не пуст.
        // Постусловие: в конец списка добавлено новое значение.
        public abstract void AddTail();

        // Предусловие: список не пуст.
        // Постусловие: значение текущего узла заменено на заданное
        public abstract void Replace();

        // Запросы

        public abstract int Size(); // -- посчитать количество узлов в списке;

        // Предусловие: список не пуст.
        public abstract T Get(); // -- получить значение текущего узла;

        public abstract bool IsHead(); // -- находится ли курсор в начале списка?

        public abstract bool IsTail(); // -- находится ли курсор в конце списка?

        public abstract bool IsValue(); // -- установлен ли курсор на какой-либо узел в списке?

        // Доп. запросы
        
        public abstract int GetHeadStatus();

        public abstract int GetTailStatus();

        public abstract int GetRightStatus();

        public abstract int GetFindStatus();

        public abstract int GetPutRightStatus();

        public abstract int GetPutLeftStatus();

        public abstract int GetRemoveStatus();

        public abstract int GetAddToEmptyStatus();

        public abstract int GetAddTailStatus();

        public abstract int GetReplaceStatus();
        
        public abstract int GetStatus(); 
    }

    public class LinkedListImpl<T> : LinkedList<T>
    {
        
    }
}
