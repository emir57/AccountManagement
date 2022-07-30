using Core.Entity.Concrete;

namespace AccountManager.Business.Constants
{
    public class BusinessMessages
    {
        public static readonly string SuccessAdd = "Successfully add";
        public static readonly string SuccessUpdate = "Successfully update";
        public static readonly string SuccessDelete = "Successfully delete";


        public static readonly string UnSuccessAdd = "UnSuccessfully add";
        public static readonly string UnSuccessUpdate = "UnSuccessfully update";
        public static readonly string UnSuccessDelete = "UnSuccessfully delete";


        public static readonly string SuccessGet = "Successfully get";
        public static readonly string UnSuccessGet = "Successfully get";

        public static readonly string SuccessList = "UnSuccessfully list";
        public static readonly string UnSuccessList = "UnSuccessfully list";

        public static readonly string NotFound = "Not found entity";

        public static readonly string WrongPassword = "Wrong password";

        internal static readonly string UserAlreadyExists = "User already exists";
        internal static readonly string NotFoundAccount = "Not found account";

        internal static readonly string UnSuccessRegister = "UnSuccessfully register";
        internal static readonly string SuccessRegister = "Successfully register";
        internal static readonly string SuccessCreateToken = "Success create token";
    }
}
