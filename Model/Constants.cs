using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTemplateEcreo.Model
{
    public static class Constants
    {
        public static string UserRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://206.189.49.197/User/{0}" : "http://206.189.49.197/User/{0}";

        public static string AbsenceRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://206.189.49.197/Absence/{0}" : "http://206.189.49.197/Absence/{0}";

        public static string AuthenticationRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://206.189.49.197/Authentication/{0}" : "http://206.189.49.197/Authentication/{0}";

        //public static string UserRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8127/User/{0}" : "http://localhost:8127/User/{0}";
        //public static string AbsenceRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8127/Absence/{0}" : "http://localhost:8127/Absence/{0}";
        //public static string AuthenticationRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8127/Authentication/{0}" : "http://localhost:8127/Authentication/{0}";

        public static string ImageRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://206.189.49.197/Media/{0}" : "http://206.189.49.197/Media/{0}";
        public static string AdminstratorUpdateRoleRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://206.189.49.197/Administrator/UpdateRole/{0}" : "http://206.189.49.197/Administrator/UpdateRole/{0}";
        public static string AdminstratorGetUserInfoRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://206.189.49.197/Administrator/GetUserInformation/{0}" : "http://206.189.49.197/Administrator/GetUserInformation/{0}";
        public static string AdminstratorGetUserPasswordRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://206.189.49.197/Administrator/GetPassword/{0}" : "http://206.189.49.197/Administrator/GetPassword/{0}";

        //Tjek docs
        public static string AdminstratorGetUserRoleRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://206.189.49.197/Administrator/GetPassword/{0}" : "http://localhost:8127/Media/{0}";




        public static string AuthenticationKey = "F-JaNdRfUserjd89#5*6Xn2r5usErw8x/A?D(G+KbPeShV";

    }
}
