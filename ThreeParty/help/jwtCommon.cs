using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JWT;
using JWT.Serializers;
using ThreeParty.Models;

namespace ThreeParty.help
{
    public class jwtCommon
    {
        /// <summary>
        /// 根据jwtToken  获取实体(假如已知实体)
        /// </summary>
        /// <param name="token">jwtToken</param>
        /// <returns></returns>
        public static UserInfo GetJwtDecode(string token)
        { 
            string secret = "";   //不需要密码
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider); 
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
            var userInfo = decoder.DecodeToObject<UserInfo>(token, secret, verify: false);//token为之前传递的字符串
            return userInfo;
        }


        //只获取解密后的json字符串
        public static string getJwtDecodeString(string token)
        {
            try
            {
                string secret = "";   //不需要密码
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
                var str = decoder.Decode(token, secret, verify: false);
                return str;
            }
            catch (TokenExpiredException e)
            {
                return "Token has expired"; 
            }
            catch (SignatureVerificationException)
            {
                return ("Token has invalid signature");
            }

        }
    }
}