using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixOperations.BL
{
    public class RealMatrix : Matrix<double>
    {
        public RealMatrix Inversed
        {
            get
            {
                var det = Determinant;
                if (det == 0)
                    throw new ArgumentException("Обратной матрицы не существует, если определитель равен 0");
                return (1 / det) * new RealMatrix(GetMatrixOfAlgebraicComplement().Transposed);
            }
        }

        public double Determinant
        {
            get
            {
                if (Width != Height)
                    throw new ArgumentException("Определитель можно найти только у квадратной матрицы");
                if (Width == 1)
                    return this[0, 0];

                double result = 0;

                for (int i = 0; i < Width; i++)
                {
                    result += this[0, i] * GetAlgebraicComplement(i, 0);
                }

                return result;
            }
        }

        public RealMatrix(double[,] elements) : base(elements) { }
        public RealMatrix(Matrix<double> matrix) : base(matrix.Elements) { }
        public RealMatrix(int width, int height) : base(width, height) { }
        public RealMatrix(int width, int heigth, IEnumerable<double> source) : base(width, heigth, source) { }

        public static RealMatrix operator +(RealMatrix a, RealMatrix b)
        {
            if (a.Height != b.Height || a.Width != b.Width)
                throw new ArgumentException("Размеры матриц должны совпадать");

            var result = new RealMatrix(a.Width, a.Height);
            for (int i = 0; i < a.Height; i++)
                for (int j = 0; j < a.Width; j++)
                    result[i, j] = a[i, j] + b[i, j];

            return result;
        }

        public static RealMatrix operator -(RealMatrix a, RealMatrix b)
        {
            if (a.Height != b.Height || a.Width != b.Width)
                throw new ArgumentException("Размеры матриц должны совпадать");

            var result = new RealMatrix(a.Width, a.Height);
            for (int i = 0; i < a.Height; i++)
                for (int j = 0; j < a.Width; j++)
                    result[i, j] = a[i, j] - b[i, j];

            return result;
        }

        public static RealMatrix operator *(RealMatrix a, RealMatrix b)
        {
            if (a.Width != b.Height)
                throw new ArgumentException("Ширина первой матрицы должна совпадать с высотой второй");

            var result = new RealMatrix(b.Width, a.Height);
            for (int i = 0; i < result.Height; i++)
                for (int j = 0; j < result.Width; j++)
                    for (int k = 0; k < a.Width; k++)
                        result[i, j] += a[i, k] * b[k, j];

            return result;
        }

        public static RealMatrix operator *(RealMatrix a, double b)
        {
            var result = new RealMatrix(a.Width, a.Height);
            for (int i = 0; i < result.Height; i++)
                for (int j = 0; j < result.Width; j++)
                    result[i, j] = a[i, j] * b;

            return result;
        }

        public static RealMatrix operator *(double a, RealMatrix b)
        {
            return b * a;
        }

        public double GetAlgebraicComplement(int x, int y)
        {
            var isPowerEven = (x + y) % 2 == 0;
            return GetMinor(x, y).Determinant * (isPowerEven ? 1 : -1);
        }

        public RealMatrix GetMatrixOfAlgebraicComplement()
        {
            var result = new RealMatrix(Width, Height);
            for(int i = 0; i < Height; i++)
            {
                for(int j = 0; j < Width; j++)
                {
                    result[i, j] = GetAlgebraicComplement(j, i);
                }
            }
            return result;
        }

        public RealMatrix GetMinor(int x, int y)
        {
            if (Width != Height)
                throw new ArgumentException("Минор можно найти только у квадратной матрицы");

            var elements = new List<double>();
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (i != y && j != x)
                        elements.Add(this[i, j]);
                }
            }
            return new RealMatrix(Width - 1, Height - 1, elements);
        }
    }
}