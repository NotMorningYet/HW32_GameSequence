namespace Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders
{
    public interface IDataReadable<TData> where TData : ISaveData
    {
        void ReadFrom(TData data);
    }

}

