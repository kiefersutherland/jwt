# jwt (Json web token)
a .net4.5.2  jwt  mvc demo 


.net mvc 使用**JWT**的一个demo

## 测试方式:
## 1. 使用http://localhost:10086/jwt/CreateToken ，用户名/密码均为admin 获取一个token

## 2. 访问http://localhost:10086/home/about 页面，需要在header中带一个名为auth的信息,内容为上面获得的token

## 3. 启动JWT.MvcDemo和front项目，打开http://localhost:30314/   可以在线调试。

## 4.调用第三方系统验证登录  ThreeParty启动。  从http://localhost:30314/ 跳转到 http://localhost:26617/java/info ,后台验证。并解密出token附带信息。

## 其它说明 
## 1. token有效期为2分钟。  在**overtime**配置。
## 2. iat时间已被加密
## 3. 具体看代码。简单明了

