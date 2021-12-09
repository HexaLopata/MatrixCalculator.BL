using System;
using System.Collections.Generic;

namespace MatrixOperations.BL
{
    public class IntMatrix : Matrix<int>
    {
        public IntMatrix Inversed
        {
            get
            {
                var det = Determinant;
                if (det == 0)
                    throw new ArgumentException("There is no inverse matrix if the determinant is 0");
                return (1 / det) * new IntMatrix(GetMatrixOfAlgebraicComplement().Transposed);
            }
        }

        public int Determinant
        {
            get
            {
                if (Width != Height)
                    throw new ArgumentException("The determinant can only be found in a square matrix");
                if (Width == 1)
                    return this[0, 0];

                int result = 0;

                for (int i = 0; i < Width; i++)
                {
                    result += this[0, i] * GetAlgebraicComplement(i, 0);
                }

                return result;
            }
        }

        public IntMatrix(int[,] elements) : base(elements) { }
        public IntMatrix(Matrix<int> matrix) : base(matrix.Elements) { }
        public IntMatrix(int width, int height) : base(width, height) { }
        public IntMatrix(int width, int heigth, IEnumerable<int> source) : base(width, heigth, source) { }

        public static IntMatrix operator +(IntMatrix a, IntMatrix b)
        {
            if (a.Height != b.Height || a.Width != b.Width)
                throw new ArgumentException("The sizes of the matrices must match");

            var result = new IntMatrix(a.Width, a.Height);
            for (int i = 0; i < a.Height; i++)
                for (int j = 0; j < a.Width; j++)
                    result[i, j] = a[i, j] + b[i, j];

            return result;
        }

        public static IntMatrix operator -(IntMatrix a, IntMatrix b)
        {
            if (a.Height != b.Height || a.Width != b.Width)
                throw new ArgumentException("The sizes of the matrices must match");

            var result = new IntMatrix(a.Width, a.Height);
            for (int i = 0; i < a.Height; i++)
                for (int j = 0; j < a.Width; j++)
                    result[i, j] = a[i, j] - b[i, j];

            return result;
        }

        public static IntMatrix operator *(IntMatrix a, IntMatrix b)
        {
            if (a.Width != b.Height)
                throw new ArgumentException("The width of the first matrix must match the height of the second matrix");

            var result = new IntMatrix(b.Width, a.Height);
            for (int i = 0; i < result.Height; i++)
                for (int j = 0; j < result.Width; j++)
                    for (int k = 0; k < a.Width; k++)
                        result[i, j] += a[i, k] * b[k, j];

            return result;
        }

        public static IntMatrix operator *(IntMatrix a, int b)
        {
            var result = new IntMatrix(a.Width, a.Height);
            for (int i = 0; i < result.Height; i++)
                for (int j = 0; j < result.Width; j++)
                    result[i, j] = a[i, j] * b;

            return result;
        }

        public static IntMatrix operator *(int a, IntMatrix b)
        {
            return b * a;
        }

        public int GetAlgebraicComplement(int x, int y)
        {
            var isPowerEven = (x + y) % 2 == 0;
            return GetMinor(x, y).Determinant * (isPowerEven ? 1 : -1);
        }

        public IntMatrix GetMatrixOfAlgebraicComplement()
        {
            var result = new IntMatrix(Width, Height);
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    result[i, j] = GetAlgebraicComplement(j, i);
                }
            }
            return result;
        }

        public IntMatrix GetMinor(int x, int y)
        {
            if (Width != Height)
                throw new ArgumentException("A minor can only be found in a square matrix");

            var elements = new List<int>();
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (i != y && j != x)
                        elements.Add(this[i, j]);
                }
            }
            return new IntMatrix(Width - 1, Height - 1, elements);
        }
    }
}