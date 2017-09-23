using AxosoftAPI.NET;
using AxosoftAPI.NET.Core;

namespace Toggl_To_Axosoft_Integration.Business.Api.Axosoft
{
    public class AxosoftWorkLogs
    {
        public void GetAxoftWorkLogs()
        {
            var client = new Proxy();
            var result = new AxosoftAPI.NET.WorkLogs(client).Get();
        }
    }
}