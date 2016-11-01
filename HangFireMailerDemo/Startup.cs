using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(HangFireMailerDemo.Startup))]

namespace HangFireMailerDemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
