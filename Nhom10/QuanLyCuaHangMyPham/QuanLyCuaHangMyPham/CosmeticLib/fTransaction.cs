namespace CosmeticLib
{
    public class fTransaction
    {
        public static int ConvertPrice(string price)
        {
            int result = -1;
            if (price.Equals("0 đ"))
            {
                result = Convert.ToInt32(price.Split(' ')[0]);
                return result;
            }
            if (price.Contains(",00"))
            {
                string s1 = price.Split(',')[0];
                if (s1.Equals(""))
                {
                    result = Convert.ToInt32(s1.Split(' ')[0]);
                    return result;
                }
                string[] s2 = s1.Split('.');
                string s3 = "";
                foreach (string item in s2)
                {
                    s3 += item;
                }
                result = Convert.ToInt32(s3);
            }
            else
            {
                string s1 = price.Split(' ')[0];
                string[] s2 = s1.Split('.');
                string s3 = "";
                foreach (string item in s2)
                {
                    s3 += item;
                }
                result = Convert.ToInt32(s3);

            }
            return result;
        }
    }
}
