using CosmeticLib.DAO;

namespace CosmeticLib
{
    public class fLogin
    {
        public static bool Login(string userName, string passWord)
        {
            return AccountDAO.Instance.Login(userName, passWord);
        }
    }
}