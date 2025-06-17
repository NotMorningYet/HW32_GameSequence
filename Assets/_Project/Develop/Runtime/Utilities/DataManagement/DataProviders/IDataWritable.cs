namespace Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders
{
    public interface IDataWritable<TData> where TData : ISaveData
    {
        void WriteTo(TData data);       
    }
}

