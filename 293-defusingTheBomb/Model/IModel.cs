namespace _293_defusingTheBomb
{
    interface IModel
    {
        string GetWires();
        string GetWiresFinished();
        string CutWire(int i);
        void InitializeWires();
        bool GameIsWon();
    }
}
