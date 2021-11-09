using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace MatrixOperations.BL
{
    public class Matrix<T> : IEnumerable<T>
    {
        protected T[,] _elements;

        public int Width => _elements.GetLength(1);
        public int Height => _elements.GetLength(0);
        public T this[int x, int y]
        {
            get => _elements[x, y];
            set
            {
                _elements[x, y] = value;
            }
        }
        public T[,] Elements => _elements;
        public Matrix<T> Transposed
        {
            get
            {
                var result = new Matrix<T>(Width, Height);
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        result[i, j] = this[j, i];
                    }
                }
                return result;
            }
        }

        public Matrix(T[,] elements)
        {
            _elements = elements;
        }

        public Matrix(int width, int heigth)
        {
            _elements = new T[heigth, width];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _elements.OfType<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        public T[] GetColomn(int index)
        {
            var result = new T[Height];
            for (int i = 0; i < Height; i++)
            {
                result[i] = _elements[i, index];
            }
            return result;
        }

        public T[] GetRow(int index)
        {
            var result = new T[Width];
            for (int i = 0; i < Width; i++)
            {
                result[i] = _elements[index, i];
            }
            return result;
        }

        public override string ToString()
        {
            var result = string.Empty;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    result += " " + _elements[i, j];
                }
                result += "\n";
            }
            return result;
        }
    }
}
