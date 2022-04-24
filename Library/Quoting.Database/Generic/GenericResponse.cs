namespace QuotingApi.ViewModel
{
    public class GenericResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }


    }

    public class GenericResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public T Result { get; set; }   
    }
}
