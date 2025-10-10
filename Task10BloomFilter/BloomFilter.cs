using System;
using System.Collections;
using System.Linq;

namespace OOAP1.Task10BloomFilter
{
    public interface BloomFilter<T>
    {
        // Конструктор
        // Постусловие: создан фильтр для len количества элементов.
        // public BloomFilter (int len)
        
        
        // Команды
        
        // Постусловие: значение value добавлено в фильтр.
        void Add(T value);
        
        
        // Запросы

        bool HasValue(T value); // True если в фильтре возможно содержится такое значение, false - если значение точно не содержится.
    }

    public class BloomFilterImpl<T> : BloomFilter<T>
    {
        private const double P_FALSE_POSITIVE = 0.01d;

        private int _hashFunCount;
        private BitArray _filter;
        
        public BloomFilterImpl(int len)
        {
            int bitsCount = CalculateBitsCount(len);
            _filter = new BitArray(bitsCount);

            _hashFunCount = CalculateHashFunCount(len, bitsCount);
        }
        
        public void Add(T value)
        {
            foreach (int index in GetBloomIndexes(value))
            {
                _filter[index] = true;
            }
        }

        public bool HasValue(T value) =>
            GetBloomIndexes(value).All(index => _filter.Get(index));
        
        private int CalculateBitsCount(int len)
        {
            double log2 = Math.Log(2.0d);
            
            double bitsCount = - (len * Math.Log(P_FALSE_POSITIVE)) / Math.Pow(log2, 2.0d);

            return Convert.ToInt32(Math.Ceiling(bitsCount));
        }
        
        private int CalculateHashFunCount(int len, int bitsCount)
        {
            double hashFunCount = (bitsCount / (double)len) * Math.Log(2.0d);

            return Math.Max(1, Convert.ToInt32(Math.Round(hashFunCount)));
        }
        
        private int[] GetBloomIndexes(T value)
        {
            int bitsCount = _filter.Length;
                
            int hash1 = FirstHashFun(value, bitsCount);
            int hash2 = SecondHashFun(value, bitsCount);

            int[] indexes = new int[_hashFunCount];

            for (int i = 0 ; i < _hashFunCount; ++i)
            {
                int index = GetIndex(i, hash1, hash2, bitsCount);
                
                if (index < 0) 
                    index += bitsCount; 
                
                indexes[i] = index;
            }

            return indexes;
        }
        
        private int GetIndex(int index, int hash1, int hash2, int capacity)
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
    }
}