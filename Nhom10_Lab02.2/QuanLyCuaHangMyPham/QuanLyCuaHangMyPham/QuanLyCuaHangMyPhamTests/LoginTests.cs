using CosmeticLib;

namespace QuanLyCuaHangMyPhamTests
{
    public class LoginTests
    {
        [Theory]
        [InlineData("admin", "1")]
        [InlineData("staff", "1")]
        [InlineData("staff", "69")]
        public void Login(string userName, string password)
        {
            Assert.Equal(true, fLogin.Login(userName, password));
        }
    }
}