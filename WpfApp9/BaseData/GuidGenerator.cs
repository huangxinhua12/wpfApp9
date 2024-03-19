using System;

namespace WpfApp9.BaseData
{
    public class GuidGenerator
    {
        public static Guid GenerateUuid()  
        {  
            return Guid.NewGuid();  
        }  
    }
}