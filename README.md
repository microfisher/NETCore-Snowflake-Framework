
# JWT授权微服务
基于.NET Core 3.1 + Mysql + Redis + Dapper + Swagger + CSRedis 开发的JWT授权认证微服务，涵盖了日志、授权认证、Swagger API管理、请求时间统计等功能，Docker配置文件、服务安装文件等，便于开发项目直接使用。

## 基本使用

1.打开数据层下边的Schemes文件夹，在Mysql里新建Snowflake数据库并执行SQL文件。

2.按照界面层中的appsettings.json配置Mysql、Redis连接字符串，Rabbitmq可以不用配置。

3.启动网站，创建账号、登陆、退出试试。

## Docker安装

1.创建容器互通的网桥：docker network create starnet

2.打开界面层下的Dockerfile用命令编译成镜像包：docker build -t microfisher/snowflake -f Dockerfile .

3.运行镜像包：docker run -d --restart always -p 10000:10000 --name microfisher/snowflake --network starnet  microfisher/snowflake:latest

4.打开本地电脑的10000端口查看网站。

## Centos服务安装

1.将界面层下的snowflake.service文件拷贝至Centos服务器的/etc/systemd/system下。

2.执行：systemctl enable snowflake.service

3.执行：systemctl start snowflake.service

4.打开你服务器的10000端口查看网站。

## 类库介绍

Microfisher.Snowflake.Core ：公共类库，包含一些常用Helper、Http请求时间统计等。

Microfisher.Snowflake.Data ：数据层，使用仓库模式实现的高性能增删改查。

Microfisher.Snowflake.Services ：业务层，封装项目核心逻辑。

Microfisher.Snowflake.Web ： 界面层，你懂的。
