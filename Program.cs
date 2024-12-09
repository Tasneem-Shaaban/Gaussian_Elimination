using System;

namespace Gaussian_Elimination
{
    internal class Program
    {
        // Function to get the augmented matrix
        public static float[,] Matrix(out int rows, out int columns)
        {
            // Get the number of rows
            Console.WriteLine("Please Enter The Number Of Equations!");
            rows = int.Parse(Console.ReadLine());

            // Get the number of columns
            Console.WriteLine("Please Enter The Number Of Variables!");
            columns = int.Parse(Console.ReadLine()) + 1; // +1 for the augmented part

            // Define & Get the matrix
            float[,] matrix = new float[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (j == columns - 1)
                    {
                        Console.WriteLine("Enter The Result:");
                    }
                    else
                    {
                        Console.WriteLine($"Enter The Coefficient Of Variable {j + 1} In Equation {i + 1}:");
                    }
                    matrix[i, j] = float.Parse(Console.ReadLine());
                }
            }

            return matrix;
        }

        // Function to swap rows
        public static void SwapRows(float[,] matrix, int row1, int row2, int columns)
        {
            for (int j = 0; j < columns; j++)
            {
                float temp = matrix[row1, j];
                matrix[row1, j] = matrix[row2, j];
                matrix[row2, j] = temp;
            }
        }

        // Ensure the first element of the matrix is non-zero by swapping rows
        public static void EnsureFirstElementNonZero(float[,] matrix, int rows, int columns)
        {
            int firstRow = 0;
            while (matrix[0, 0] == 0 && firstRow < rows)
            {
                SwapRows(matrix, 0, firstRow, columns);
                firstRow++;
            }

            if (matrix[0, 0] == 0)
            {
                throw new InvalidOperationException("Matrix cannot be transformed to have a non-zero first element.");
            }
        }

        // Put the matrix in the row-echelon form
        public static void Row_Echelon(float[,] matrix, int rows, int columns)
        {
            EnsureFirstElementNonZero(matrix, rows, columns);

            for (int k = 0; k < rows; k++)
            {
                // 0's under the leadings
                for (int i = k + 1; i < rows; i++)
                {
                    if (matrix[k, k] == 0)
                        continue; // skip if the leading element is zero

                    float factor = matrix[i, k] / matrix[k, k];
                    for (int j = k; j < columns; j++)
                    {
                        matrix[i, j] -= factor * matrix[k, j];
                    }
                }

                // Leading 1 in each row
                if (matrix[k, k] != 0)
                {
                    float leading = matrix[k, k];
                    for (int j = k; j < columns; j++)
                    {
                        matrix[k, j] /= leading;
                    }
                }
            }
        }

        // Gaussian Elimination
        public static float[] Gaussian_Elimination(float[,] matrix, int rows, int columns)
        {
            // 1D array to store the answer
            float[] solution = new float[rows];
            for (int i = rows - 1; i >= 0; i--)
            {
                solution[i] = matrix[i, columns - 1];
                for (int j = i + 1; j < rows; j++)
                {
                    solution[i] -= matrix[i, j] * solution[j];
                }
                solution[i] /= matrix[i, i];
            }
            return solution;
        }

        static void Main(string[] args)
        {
            int NoRows, NoColumns;
            float[,] matrix = Matrix(out NoRows, out NoColumns);

            Row_Echelon(matrix, NoRows, NoColumns);

            float[] solution = Gaussian_Elimination(matrix, NoRows, NoColumns);

            Console.WriteLine("Solution:");
            for (int i = 0; i < solution.Length; i++)
            {
                Console.WriteLine($"x{i + 1} = {solution[i]}");
            }

            Console.ReadKey();
        }
    }
}
