using HotChocolate;

namespace graphqltest.Errors
{
    public class graphqltestErrorFilter: IErrorFilter
    {
        public IError OnError(IError error)
        {
            return error.WithMessage(error.Code);
        }
    }
}