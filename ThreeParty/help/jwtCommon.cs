using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using ThreeParty.Models;

namespace ThreeParty.help
{
    public class jwtCommon
    {
        //私钥  web.config中配置 
        private static string secret = ConfigurationManager.AppSettings["Secret"].ToString();

        /// <summary>
        /// 生成JwtToken
        /// </summary>
        /// <param name="payload">不敏感的用户数据</param>
        /// <returns></returns>
        public static string SetJwtEncode(Dictionary<string, object> payload)
        { 

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, secret);
            return token;
        }
         


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


        public static object GetJwtDecodeobject(string token)
        {
            string secret = "";   //不需要密码
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
            object t = decoder.DecodeToObject< object>(token, secret, verify: false);//token为之前传递的字符串
            return t;
        }
    }
}