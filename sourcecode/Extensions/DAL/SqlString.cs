//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Mysoft.Map.Extensions.DAL
//{
//    public sealed class LimitString
//    {
//        public LimitString(string value, int size)
//        {
//            if (string.IsNullOrEmpty(value)){
//                throw new ArgumentNullException("value");
//            }

//            Value = value;
//            Size = size;
//        }

//        public string Value { get; private set; }
//        public string Size { get; private set; }
//    }

//    public static class LimitStringHelper
//    {
//        public static LimitString AsLimitString(this string value, int size)
//        {
//            return new LimitString(value, size);
//        }
//    }
//}
