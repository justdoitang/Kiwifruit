using Kiwifruit.Models.Logs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Kiwifruit.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILoggers _loggers;
     
        public ValuesController(ILoggers loggers)
        {
            _loggers = loggers;
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public int Get()
        {
            //_log.Info("测试");
                        
            _loggers.Debug("ss","测试");
            return 1;
        }

    }
}
