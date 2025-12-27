namespace BookManangementSystem.Helper
{
    public  class ApiResponce<T>
    {
         public  string Message { get; set; }
         public  bool Status { get; set; }
         public  T Data { get; set; }

        public static ApiResponce<T> Success(string message,T Data)
        {
            return new ApiResponce<T>
            {
                Message = message,
                Status = true,
                Data = Data
            };
        } 
        public static ApiResponce<T> Failure(string message)
        {
            return new ApiResponce<T>
            {
                Message = message,
                Status = false,
                Data = default(T)
            };
        }

    }
}
