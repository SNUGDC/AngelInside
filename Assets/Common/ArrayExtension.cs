static class ArrayExtension
{
    public static int RowLength<T>(this T[,] array)
    {
        return array.GetLength(0);
    }

    public static int ColLength<T>(this T[,] array)
    {
        return array.GetLength(1);
    }

    public static T[] Row<T>(this T[,] array, int row)
    {
        int columns = array.ColLength();
        T[] result = new T[columns];
        for (int i = 0; i < columns; i++)
        {
            result[i] = array[row, i];
        }
        return result;
    }

    public static T[] Col<T>(this T[,] array, int col)
    {
        int rows = array.RowLength();
        T[] result = new T[rows];
        for (int i = 0; i < rows; i++)
        {
            result[i] = array[i, col];
        }
        return result;
    }
}
