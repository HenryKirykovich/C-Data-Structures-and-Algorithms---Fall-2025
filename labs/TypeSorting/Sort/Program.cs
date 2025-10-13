// See https://aka.ms/new-console-template for more information
Console.WriteLine("#1 SelectionSorting");
Console.WriteLine();
int [] numbers = { 5, 3, 8, 1, 2 };
// void SelectioningSort(int[] arr)
// {
    // for (int i = 0; i < arr.Length; i++)
    // {
        // int min = i;
        // for (int j = i + 1; j < arr.Length; j++)
        // {
            // if (arr[j] < arr[min])
            // {
                // min = j;
            // }
        // }
        // int tmp = arr[i];
        // arr[i] = arr[min];
        // arr[min] = tmp;
    // }
// }
// 
// 
// Console.WriteLine("Unsorted array: " + string.Join(", ", numbers));
// SelectioningSort(numbers);
// Console.WriteLine("Sorted array: " + string.Join(", ", numbers));   
// 


// void InsertionSort(int[] a)
// {
    // for (int i = 1; i < a.Length; i++)
    // {
        // int key = a[i], j = i - 1;
        // while (j >= 0 && a[j] > key) { a[j + 1] = a[j]; j--; }
        // a[j + 1] = key;
    // }
// }
// 
// Console.WriteLine("# InsertionSorting");
// Console.WriteLine();
// Console.WriteLine("Unsorted array: " + string.Join(", ", numbers));
// InsertionSort(numbers);
// Console.WriteLine("Sorted array: " + string.Join(", ", numbers));
// 
// Console.WriteLine();
// Console.WriteLine("# BubbleSorting");
// Console.WriteLine();
// void BubbleSort(int[] arr)
// {
// 
    // for (int i = 0; i < arr.Length - 1; i++)
    // {
        // for (int j = 0; j < arr.Length - i - 1; j++)
        // {
            // if (arr[j] > arr[j + 1])
            // {
                // swap arr[j] and arr[j+1]
                // int temp = arr[j];
                // arr[j] = arr[j + 1];
                // arr[j + 1] = temp;
            // }
        // }
    // }
// }

// Console.WriteLine("Unsorted array: " + string.Join(", ", numbers));
// BubbleSort(numbers);
// Console.WriteLine("Sorted array: " + string.Join(", ", numbers));
// 
Console.WriteLine();
Console.WriteLine("Quick Sort");
Console.WriteLine();
void QuickSort(int[] a,int l,int r)
{
    if (l >= r) return;
    int i = l, j = r, p = a[(l + r) / 2];
    while (i <= j)
    {
        while (a[i] < p) i++; while (a[j] > p) j--;
        if (i <= j) { (a[i], a[j]) = (a[j], a[i]); i++; j--; }
    }
    if (l < j) QuickSort(a, l, j);
    if (i < r) QuickSort(a, i, r);
}
Console.WriteLine("Unsorted array: " + string.Join(", ", numbers));
QuickSort(numbers, 0, numbers.Length - 1);
Console.WriteLine("Sorted array: " + string.Join(", ", numbers));       