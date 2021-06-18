
![status](
https://travis-ci.com/microfisher/NETCore-Multilayer-Framework.svg?branch=master)

# 雪花框架（.NET Core 高性能多层架构）
基于.NET Core + Mysql + Redis + Dapper + Swagger + CSRedis设计的多层架构，涵盖了仓储设计模式、全局日志、JWT授权认证、Swagger API管理、请求时间统计等功能，Docker配置文件、服务安装文件等，便于开发项目直接使用。



## 类库介绍

Snowflake.Core ：公共类库，包含一些与业务无关的代码、常用的Helper、或Http请求时间统计等。

Snowflake.Data ：数据层，仓库模式实现的高性能增删改查。

Snowflake.Services ：业务层，封装项目业务核心逻辑。

Snowflake.Identity ：接口层，提供给前端项目组调用。

![截图](
https://raw.githubusercontent.com/microfisher/NETCore-Multilayer-Framework/master/snapshot.png)

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

