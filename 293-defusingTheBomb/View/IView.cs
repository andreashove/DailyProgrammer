namespace _293_defusingTheBomb
{
    interface IView
    {
        void WriteToUser(string s);
        void WriteHeaderAndRulesToUser();
        void WriteToUser(int i);
        void ClearConsole();
        string GetUserInput();

    }
}
