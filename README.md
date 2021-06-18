
![status](
https://travis-ci.com/microfisher/NETCore-Multilayer-Framework.svg?branch=master)

# 雪花框架（.NET Core 高性能多层架构）
基于.NET Core + Mysql + Redis + RabbitMQ 设计的高性能多层架构，使用了Dapper、CSRedis、Swagger、NLog、MySqlConnector、RabbitMQ.Client等开源项目，涵盖了仓储设计模式、全局日志、JWT授权认证、Swagger API管理、请求时间统计等功能，Docker配置文件、服务安装文件等，便于开发项目直接使用。

## 类库介绍

Snowflake.Core      核心层 - 包含系统配置、自动注入、系统异常、响应对象、通用Helper等与业务无关的通用代码。

Snowflake.Data      数据层 - 基于原生SQL的仓储设计模式及事务处理单元，实现了通用的数据操作。

Snowflake.Services  业务层 - 与具体业务相关的逻辑，同时包含了日志记录、异常抛出、IP地址获取、计算各个请求消耗时间。

Snowflake.Identity  接口层 - 基于AOP标签的权限认证，提供RESTFUL接口给前端。

## 基本使用

1.打开Snowflake.Identity下边的Databases文件夹，在Mysql里新建Snowflake数据库并执行init.sql文件。

2.修改Snowflake.Identity中的系统配置appsettings.json配置Mysql、Redis连接字符串。

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

