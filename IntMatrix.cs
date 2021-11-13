using System;
using System.Collections.Generic;

namespace MatrixOperations.BL
{
    public class IntMatrix : Matrix<int>
    {
        public IntMatrix(int[,] elements) : base(elements) { }
        public IntMatrix(Matrix<int> matrix) : base(matrix.Elements) { }
        public IntMatrix(int width, int height) : base(width, height) { }
        public IntMatrix(int width, int heigth, IEnumerable<int> source) : base(width, heigth, source) { }

        public static IntMatrix operator +(IntMatrix a, IntMatrix b)
        {
            if(a.Height != b.Height || a.Width != b.Width)
                throw new ArgumentException("Размеры матриц должны совпадать");
                
            var result = new IntMatrix(a.Width, a.Height);
            for(int i = 0; i < a.Height; i++)
                for(int j = 0; j < a.Width; j++)
                    result[i, j] = a[i, j] + b[i, j];

            return result;
        }

        public static IntMatrix operator -(IntMatrix a, IntMatrix b)
        {
            if(a.Height != b.Height || a.Width != b.Width)
                throw new ArgumentException("Размеры матриц должны совпадать");
            
            var result = new IntMatrix(a.Width, a.Height);
            for(int i = 0; i < a.Height; i++)
                for(int j = 0; j < a.Width; j++)
                    result[i, j] = a[i, j] - b[i, j];

            return result;
        }

        public static IntMatrix operator *(IntMatrix a, IntMatrix b)
        {
            if(a.Width != b.Height)
                throw new ArgumentException("Ширина первой матрицы должна совпадать с высотой второй");
            
            var result = new IntMatrix(b.Width, a.Height);
            for(int i = 0; i < result.Height; i++)
                for(int j = 0; j < result.Width; j++)
                    for(int k = 0; k < a.Width; k++)
                        result[i, j] += a[i, k] * b[k, j];

            return result;
        }
    }
}
