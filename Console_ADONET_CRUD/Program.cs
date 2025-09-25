using Console_ADONET_CRUD;

Console.WriteLine("Press Key - 'R' : To Read Data , 'C' : To Insert , 'U' : To Update , 'D' : To Delete , Others : To Exit Program");
string input = Console.ReadLine().ToUpper();

while (input == "R" || input == "C" || input == "U" || input == "D")
{
    ADOHelper aDOHelper = new ADOHelper();
    switch (input)
    {
        case "R":
            aDOHelper.ReadData();
            break;
        case "C":
            aDOHelper.InsertData();
            break;
        case "U":
            aDOHelper.UpdateData();
            break;
        case "D":
            aDOHelper.DeleteData();
            break;
        default:
            break;
    }
    Console.WriteLine("Press Key - 'R' : To Read Data , 'C' : To Insert , 'U' : To Update , 'D' : To Delete , Others : To Exit Program");
    input = Console.ReadLine().ToUpper();
}


