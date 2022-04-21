namespace Cloud5mins.domain
{
    class StorageTableHelper
    {
        private string StorageConnectionString { get; set; }

        public StorageTableHelper(){}

        public StorageTableHelper(string storageConnectionString){
            StorageConnectionString = storageConnectionString;
        }
    }
}