  //int file1byte = 0;
            //int file2byte = 0;
            //int totalCount = 0;
            //int totalSimi = 0;
 
 //using (FileStream fileStream1 = new FileStream(f1, FileMode.Open, FileAccess.Read),
            //                  fileStream2 = new FileStream(f2, FileMode.Open, FileAccess.Read))
            //{
            //    do
            //    {
            //        totalCount = totalCount + 1;
            //        file1byte = fileStream1.ReadByte();
            //        file2byte = fileStream2.ReadByte();
            //        if (file1byte == file2byte)
            //        {
            //            totalSimi = totalSimi + 1;
            //            lsByte.Add((byte)file1byte);
            //        }
            //    }
            //    while (file1byte != -1);
            //}


            using System.Text;
 
public class Example
{
    public static void Main()
    {
        byte[] bytes = Encoding.Default.GetBytes("ABC123");
        Console.WriteLine("Byte Array is: " + String.Join(" ", bytes));
 
        string str = Encoding.Default.GetString(bytes);
        Console.WriteLine("The String is: " + str);
    }
}
 
/*
    Output:
 
    Byte Array is: 65 66 67 49 50 51
    The String is: ABC123
*/
