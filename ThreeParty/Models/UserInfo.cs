using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThreeParty.Models
{
    public class UserInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 签发时间----加密时间戳
        /// </summary>
        public string iat { get; set; }
        //超时时间
        public string exp { get; set; }
    }
}