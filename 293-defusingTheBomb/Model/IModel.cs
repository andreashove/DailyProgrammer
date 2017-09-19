namespace _293_defusingTheBomb
{
    interface IModel
    {
        string GetWires();
        string CutWire(int i);
        void InitializeWires();
        void UpdateDifficultySetting(bool diff);
        int GetTriggerwire();
    }
}
