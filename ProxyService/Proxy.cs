using Global;

namespace ProxyService
{
    public class Proxy : IGateway
    {
        #region IGateway Members

        public bool ValidateUser(Userobj user, string url)
        {
            return true;
        }

        #endregion
    }
}
