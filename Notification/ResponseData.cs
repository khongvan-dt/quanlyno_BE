namespace BE_RADIOCAB.Models
{
    public class ResponseData
    {
        public bool Succeed { get; set; } = true;

        public int Code { get; set; }

        public string Message { get; set; } = string.Empty;
    }

    public class ResponseData<DataType> : ResponseData
    {
        public DataType? Data { get; set; } = default;
    }
}
