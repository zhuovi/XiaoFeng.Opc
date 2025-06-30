![eelf](https://www.eelf.cn/logox.png)

![GitHub top language](https://img.shields.io/github/languages/top/zhuovi/xiaofeng.opc?logo=github)
![GitHub License](https://img.shields.io/github/license/zhuovi/xiaofeng.opc?logo=github)
![Nuget Downloads](https://img.shields.io/nuget/dt/xiaofeng.opc?logo=nuget)
![Nuget](https://img.shields.io/nuget/v/xiaofeng.opc?logo=nuget)
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/xiaofeng.opc?label=dev%20nuget&logo=nuget)

Nuget：XiaoFeng.Opc

| QQ群号 | QQ群 | 公众号 |
| :----:| :----: | :----: |
| 748408911  | ![QQ 群](https://user-images.githubusercontent.com/16105174/198058269-0ea5928c-a2fc-4049-86da-cca2249229ae.png) | ![畅聊了个科技](https://user-images.githubusercontent.com/16105174/198059698-adbf29c3-60c2-4c76-b894-21793b40cf34.jpg) |


源码： https://github.com/zhuovi/xiaofeng.opc

教程： https://www.eelf.cn

通用的opc ua客户端类库，基于netstandard2.0;netstandard2.1;创建，基于官方opc ua基金会跨平台库创建，封装了节点读写、批量节点读写、引用读取、历史数据读取、方法调用、节点订阅、批量订阅、获取节点可用编码、获取指定节点的所有引用等操作。


## 感谢支持

| 名称 | LOGO |
| :----:| :----: |
| JetBrains | [![JetBrains](https://resources.jetbrains.com/storage/products/company/brand/logos/jb_beam.svg?_ga=2.18748729.1472960975.1710982503-1993260277.1703834590&_gl=1*1o75dn2*_ga*MTk5MzI2MDI3Ny4xNzAzODM0NTkw*_ga_9J976DJZ68*MTcxMDk4MjUwMi43LjEuMTcxMDk4NDUwOC4zOC4wLjA.)](https://jb.gg/OpenSourceSupport) |
| Visual Studio | [![Visual Studio](https://visualstudio.microsoft.com/wp-content/uploads/2021/10/Product-Icon.svg)](https://visualstudio.microsoft.com/) |

## XiaoFeng.Opc

XiaoFeng.Opc generator with [XiaoFeng.Opc](https://github.com/zhuovi/XiaoFeng.Opc).

## Install

.NET CLI

```
$ dotnet add package XiaoFeng.Opc --version 1.0.0
```

Package Manager

```
PM> Install-Package XiaoFeng.Opc -Version 1.0.0
```

PackageReference

```
<PackageReference Include="XiaoFeng.Opc" Version="1.0.0" />
```

Paket CLI

```
> paket add XiaoFeng.Opc --version 1.0.0
```

Script & Interactive

```
> #r "nuget: XiaoFeng.Opc, 1.0.0"
```

Cake

```
// Install XiaoFeng.Opc as a Cake Addin
#addin nuget:?package=XiaoFeng.Opc&version=1.0.0

// Install XiaoFeng.Opc as a Cake Tool
#tool nuget:?package=XiaoFeng.Opc&version=1.0.0
```


# XiaoFeng 类库包含库
| 命名空间 | 所属类库 | 开源状态 | 说明 | 包含功能 |
| :----| :---- | :---- | :----: | :---- |
| XiaoFeng.Prototype | XiaoFeng.Core | :white_check_mark: | 扩展库 | ToCase 类型转换<br/>ToTimestamp,ToTimestamps 时间转时间戳<br/>GetBasePath 获取文件绝对路径,支持Linux,Windows<br/>GetFileName 获取文件名称<br/>GetMatch,GetMatches,GetMatchs,IsMatch,ReplacePatten,RemovePattern 正则表达式操作<br/> |
| XiaoFeng.Net | XiaoFeng.Net | :white_check_mark: | 网络库 | XiaoFeng网络库，封装了Socket客户端，服务端（Socket,WebSocket），根据当前库可轻松实现订阅，发布等功能。|
| XiaoFeng.Http | XiaoFeng.Core | :white_check_mark: | 模拟请求库 | 模拟网络请求 |
| XiaoFeng.Data | XiaoFeng.Core | :white_check_mark: | 数据库操作库 | 支持SQLSERVER,MYSQL,ORACLE,达梦,SQLITE,ACCESS,OLEDB,ODBC等数十种数据库 |
| XiaoFeng.Cache | XiaoFeng.Core | :white_check_mark: | 缓存库 |  内存缓存,Redis,MemcachedCache,MemoryCache,FileCache缓存 |
| XiaoFeng.Config | XiaoFeng.Core | :white_check_mark: | 配置文件库 | 通过创建模型自动生成配置文件，可为xml,json,ini文件格式 |
| XiaoFeng.Cryptography | XiaoFeng.Core | :white_check_mark: | 加密算法库 | AES,DES,RSA,MD5,DES3,SHA,HMAC,RC4加密算法 |
| XiaoFeng.Excel | XiaoFeng.Excel | :white_check_mark: | Excel操作库 | Excel操作，创建excel,编辑excel,读取excel内容，边框，字体，样式等功能  |
| XiaoFeng.Ftp | XiaoFeng.Ftp | :white_check_mark: | FTP请求库 | FTP客户端 |
| XiaoFeng.IO | XiaoFeng.Core | :white_check_mark: | 文件操作库 | 文件读写操作 |
| XiaoFeng.Json | XiaoFeng.Core | :white_check_mark: | Json序列化，反序列化库 | Json序列化，反序列化库 |
| XiaoFeng.Xml | XiaoFeng.Core | :white_check_mark: | Xml序列化，反序列化库 | Xml序列化，反序列化库 |
| XiaoFeng.Log | XiaoFeng.Core | :white_check_mark: | 日志库 | 写日志文件,数据库 |
| XiaoFeng.Memcached | XiaoFeng.Memcached | :white_check_mark: | Memcached缓存库 | Memcached中间件,支持.NET框架、.NET内核和.NET标准库,一种非常方便操作的客户端工具。实现了Set,Add,Replace,PrePend,Append,Cas,Get,Gets,Gat,Gats,Delete,Touch,Stats,Stats Items,Stats Slabs,Stats Sizes,Flush_All,Increment,Decrement,线程池功能。|
| XiaoFeng.Redis | XiaoFeng.Redis | :white_check_mark: | Redis缓存库 | Redis中间件,支持.NET框架、.NET内核和.NET标准库,一种非常方便操作的客户端工具。实现了Hash,Key,String,ZSet,Stream,Log,List,订阅发布,线程池功能; |
| XiaoFeng.Threading | XiaoFeng.Core | :white_check_mark: | 线程库 | 线程任务,线程队列 |
| XiaoFeng.Mvc | XiaoFeng.Mvc | :x: | 低代码WEB开发框架 | .net core 基础类，快速开发CMS框架，真正的低代码平台，自带角色权限，WebAPI平台，后台管理，可托管到服务运行命令为:应用.exe install 服务名 服务说明,命令还有 delete 删除 start 启动  stop 停止。 |
| XiaoFeng.Proxy | XiaoFeng.Proxy | :white_check_mark: | 代理库 | 开发中 |
| XiaoFeng.TDengine | XiaoFeng.TDengine | :white_check_mark: | TDengine 客户端 | 开发中 |
| XiaoFeng.GB28181 | XiaoFeng.GB28181 | :white_check_mark: | 视频监控库，SIP类库，GB28181协议 | 开发中 |
| XiaoFeng.Onvif | XiaoFeng.Onvif | :white_check_mark: | 视频监控库Onvif协议 | XiaoFeng.Onvif 基于.NET平台使用C#封装Onvif常用接口、设备、媒体、云台等功能， 拒绝WCF服务引用动态代理生成wsdl类文件 ， 使用原生XML扩展标记语言封装参数，所有的数据流向都可控。 |
| FayElf.Plugins.WeChat | FayElf.Plugins.WeChat | :white_check_mark: | 微信公众号，小程序类库 | 微信公众号，小程序类库。 |
| XiaoFeng.Mqtt | XiaoFeng.Mqtt | :white_check_mark: | MQTT协议 | XiaoFeng.Mqtt中间件,支持.NET框架、.NET内核和.NET标准库,一种非常方便操作的客户端工具。实现了MQTT客户端，MQTT服务端,同时支持TCP，WebSocket连接。支持协议版本3.0.0,3.1.0,5.0.0。 |
| XiaoFeng.Modbus | XiaoFeng.Modbus | :white_check_mark: | MODBUS协议 | MODBUS协议,支持RTU、ASCII、TCP三种方式进行通信，自动离线保存服务端数据 |
| XiaoFeng.DouYin | XiaoFeng.DouYin | :white_check_mark: | 抖音开放平台SDK | 抖音开放平台接口 |
| XiaoFeng.KuaiShou | XiaoFeng.KuaiShou | :white_check_mark: | 快手开放平台SDK | 快手开放平台接口 |
| XiaoFeng.Mvc.AdminWinDesk | XiaoFeng.Mvc.AdminWinDesk | :white_check_mark: | XiaoFeng.Mvc后台皮肤 | 模仿windows桌面后台皮肤 |
| FayElf.Cube.Blog | FayElf.Cube.Blog | :white_check_mark: | XiaoFeng.Mvc开发的技术博客 | 使用低代码开发框架（XiaoFeng.Mvc）+Windows后台皮肤(XiaoFeng.Mvc.AdminWinDesk)，开发的一个博客平台。 |
| XiaoFeng.Ofd | XiaoFeng.Ofd | :white_check_mark: | OFD读写库 | OFD 读写处理库，支持文档的生成、文档编辑、文档批注、数字签名、文档合并、文档拆分、文档转换至PDF、文档查询等功能。 |
| XiaoFeng.Opc | XiaoFeng.Opc | :white_check_mark: | OPC读取写入类 | 通用的opc ua客户端类库，基于netstandard2.0;netstandard2.1;创建，基于官方opc ua基金会跨平台库创建，封装了节点读写、批量节点读写、引用读取、历史数据读取、方法调用、节点订阅、批量订阅、获取节点可用编码、获取指定节点的所有引用等操作。|

## 用法实例

---

下边用法实例中用到的wt方法为输出控制台方法如下

```csharp
/// <summary>
/// 输出控制台
/// </summary>
/// <param name="msg">消息</param>
/// <param name="isTime">是否显示时间</param>
static void w(object msg, bool isTime = false)
{
    var message = "";
    var valType = msg.GetType().GetValueType();
    if (valType == ValueTypes.Value || valType == ValueTypes.String || valType == ValueTypes.Enum) message = msg.ToString();
    else message = msg.ToJson();
    Console.Write($"{message}");
    if (isTime)
        Console.Write($" - {DateTime.Now:yyyy-MM-dd HH:mm:ss.fffffff}");
    Console.WriteLine();
}
/// <summary>
/// 输出控制台
/// </summary>
/// <param name="msg">消息</param>
static void wt(object msg)
{
    w(msg, true);
}
```

### 实例化客户端

```csharp
//实例化一个客户端
var uaClient = new XiaoFeng.Opc.UaClient();
```

下边属性可以不设置

```csharp
//设置应用名称
uaClient.ApplicationName = "ELFOpcUaClient";
//设置会话名称 不设置默认用 应用名称
uaClient.SessionName = "ELFUaClient";
//设置会超时时间 单位为毫秒 不设置默认为 60秒
uaClient.SessionTimeout = 60000;
//设置断开后重连时长 单位为毫秒 不设置默认为 10秒
uaClient.ReconnectionPeriod = 10000;
//连接事件
uaClient.ConnectEvent += e =>
{
    wt("连接成功了." + e.Message);
};
```

### 设置日志输出文件

```csharp
//设置日志输出文件
uaClient.SetLogPathName("opctrace.log", true);
```

### 连接OPC服务器

```csharp
await uaClient.ConnectAsync("opc.tcp://localhost:53530/OPCUA/EELFServer").ConfigureAwait(false);
```

### 读取服务器所有树节点

```csharp
//读取服务器所有树节点
var AllNodes = await uaClient.GetNodeTreeAsync(Opc.Ua.ObjectIds.RootFolder, true).ConfigureAwait(false);
```

### 先定义一个节点

下边所有实例都基于 nodeId 节点来操作;

```csharp
var nodeId = new NodeValueId("ns=2;s=MyLevel;");
//NodeValueId 转换为 NodeId
var _nodeId = (NodeId)nodeId;
//NodeId 转换为 NodeValueId
var _nodeValueId = (NodeValueId)_nodeId;
```

### 读取节点

```csharp
//返回的是一个DataValue对象
var dataValue = await uaClient.ReadDataValueAsync(nodeId).ConfigureAwait(false);
//读取节点
var node = await uaClient.ReadNodeAsync(nodeId);
//读取节点 返回一个DataValue对象
var dataValue1 = await uaClient.ReadAsync("ns=2;s=MyLevel").ConfigureAwait(false);
//读取节点 返回一个DataValue对象
var dataValue2 = await uaClient.ReadAsync("Mylevel", 2).ConfigureAwait(false);
//读取节点 返回一个DataValue对象
var dataValue3 = await uaClient.ReadAsync(nodeId).ConfigureAwait(false);
//读取节点 返回一个List<DataValue> 集合
var dataValues = await uaClient.ReadAsync(new List<NodeValueId> { nodeId }).ConfigureAwait(false);

```

### 写入节点值

```csharp
//写入节点值
var writeValue = await uaClient.WriteAsync("ns=2;s=MyLevel", 30).ConfigureAwait(false);
//写入节点值
var writeValue1 = await uaClient.WriteAsync(new WriteNode(nodeId, 30)).ConfigureAwait(false);
```

### 读取历史数据

```csharp
var historyValues = await uaClient.HistoryReadAsync(nodeId).ConfigureAwait(false);
var historyValues1 = await uaClient.HistoryReadAsync(nodeId,DateTime.Now.AddDays(-1),DateTime.Now,10).ConfigureAwait(false);
```

### 删除存在的节点

```csharp
var deleteNode = await uaClient.DeleteExsistNodeAsync(nodeId).ConfigureAwait(false);
var deleteNode1 = await uaClient.DeleteExsistNodeAsync("ns=2;s=MyLevel").ConfigureAwait(false);
var deleteNode2 = await uaClient.DeleteExsistNodeAsync(new List<string> { "myLevel" }).ConfigureAwait(false);
var deleteNode3 = await uaClient.DeleteExsistNodeAsync(new List<string> { "ns=2;s=MyLevel" }).ConfigureAwait(false);
var deleteNode4 = await uaClient.DeleteExsistNodeAsync(new List<NodeValueId> { "ns=2;s=MyLevel" }).ConfigureAwait(false);
var deleteNode5 = await uaClient.DeleteExsistNodeAsync(new List<NodeValueId> { nodeId }).ConfigureAwait(false);
```

### 添加新节点

```csharp
var addNode = await uaClient.AddNewNodeAsync(new AddNodesItem
{
    ParentNodeId = new ExpandedNodeId(new NodeId("ns=2;s=MyTag")),
    BrowseName = "测试节点",
    RequestedNewNodeId = new ExpandedNodeId((NodeId)nodeId),
    NodeClass = NodeClass.Variable
}).ConfigureAwait(false);
```
### 调用服务器方法

```csharp
var mehtodValue = await uaClient.CallMethodAsync("ns=2;s=MyMethodParent", "ns=2;s=MyMethod", new VariantCollection() { "a", "b" }).ConfigureAwait(false);
```

### 添加订阅

```csharp
await uaClient.AddSubscriptionAsync("subscription1", new List<NodeValueId>
{
    nodeId,
    "ns=6;s=DataItem;",
}, (a, b) =>
{
    if (b.NotificationValue is MonitoredItemNotification notification)
    {
        var value = notification.Value;
        wt($"{a.DisplayName}:{value.GetValue()}");
    }
    else
        wt($"{a.DisplayName}:");
}).ConfigureAwait(false);
```

### 移除订阅

```csharp
//移除指定名称的订阅
var status1 = await uaClient.RemoveSubscriptionAsync("subscription1").ConfigureAwait(false);
//移除所有订阅
await uaClient.RemoveAllSubscriptionAsync().ConfigureAwait(false);
```

### 设置订阅状态

```csharp
//停止订阅
await uaClient.SetSubscriptionStatusAsync("subscription1", false).ConfigureAwait(false);
```

### 添加订阅项

```csharp
//添加订阅项
uaClient.AddSubscriptionItem("subscription1", new NodeValueId("ns=6;s=DataItem_0001"));
//添加订阅项
uaClient.AddSubscriptionItem("subscription1", "ns=6;s=DataItem_0002");

```

### 移除订阅项

```csharp
uaClient.RemoveSubscriptionItem("subscription1", "ns=6;s=DataItem_0001");
```

### 设置订阅项状态

```csharp
await uaClient.SetSubscriptionItemStatusAsync("subscription1", nodeId, MonitoringMode.Reporting).ConfigureAwait(false);
```

### 设置用户凭证

```csharp
//设置匿名用户
uaClient.SetAnonymousUser();
//设置账号密码凭证
uaClient.SetUser("eelf","123456");
//设置用户证书凭证
uaClient.SetUser(X509Certificate2.CreateFromCertFile("user.pfx").GetBasePath());
```

### 获取服务器连接节点

```csharp
var endpoints = await uaClient.GetEndpointsAsnyc("opc.tcp://localhost:4840/OPCUA/EELFServer").ConfigureAwait(false);
```

### 发现本地服务

```csharp
//发现本地服务
var servers = await uaClient.DiscoverServersAsync("opc.tcp://localhost:4840").ConfigureAwait(false);
//发现网络服务
var servers = await uaClient.DiscoverNetworkServersAsync("opc.tcp://localhost:4840").ConfigureAwait(false)
```

### 获取指定节点的所有引用

```csharp
var references = await uaClient.FetchReferencesAsync(nodeId).ConfigureAwait(false);
```

### 获取节点可用编码

```csharp
var references = await uaClient.ReadAvailableEncodingsAsync(nodeId).ConfigureAwait(false);
```



# 作者介绍

* 网址 : https://www.eelf.cn
* QQ : 7092734
* Email : jacky@eelf.cn

---
