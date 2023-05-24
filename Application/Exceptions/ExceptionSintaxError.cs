namespace Application.Exceptions
{
    public class ExceptionSintaxError : Exception
    {
        public ExceptionSintaxError() : base()
        {
        }
        public ExceptionSintaxError(string message) : base(message)
        {
        }
        public ExceptionSintaxError(string message, Exception ex) : base(message)
        {
        }
    }
}
