# jwt (Json web token)
a .net4.5.2  jwt  mvc demo 


.net mvc 使用JWT的一个demo

测试方式:
1.使用http://localhost:10086/jwt/CreateToken ，用户名/密码均为admin 获取一个token
2.访问http://localhost:10086/home/about 页面，需要在header中带一个名为auth的信息,内容为上面获得的token

其它说明 
1.token有效期为2分钟。  在overtime配置。
