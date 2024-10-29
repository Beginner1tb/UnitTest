## 一、appsettings.json的配置文件的使用
### 1. 这里的json文件默认是只读的
当然也可以通过NewtonJson等库实现读写，但是在这里默认是使用Microsoft.Extensions.Configuration的方式进读取配置文件
### 2. 可以任意读取，不管是到默认配置还是到某一段程序中
#### a. 读取到配置中：（与下方读取到某一段程序中基本一致）
使用如下代码
```CSharp
private IConfiguration Configuration { get; set; }
var builderSql= new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("./Settings/postgres_settings.json", optional: false, reloadOnChange: true);

            _configurationSql=builderSql.Build();
```
应用时只要直接写出json键名即可
```CSharp
var connectionString = _configurationSql["Postgresql:connectionString"];
```
如果使用`` _configurationSql.GetConnectionString()``方法可能会出错，尽量使用json的原始键名

#### b. 读取到某一段程序中：使用Application.Properties字典
```CSharp
var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("./Settings/appsettings.json", optional: false, reloadOnChange: true);

var configuration = builder.Build();

// 将配置存储到Application.Properties
this.Properties["Configuration"] = configuration;

//比如在MainWindow.xaml.cs中
var _configuration = (IConfiguration)Application.Current.Properties["Configuration"];
string title = _configuration["应用程序设置1111:标题"];
```
这里的配置文件数据是存储在Application.Properties临时全局字典中的

#### c. 注意事项
① 从json文件读取参数并传递有很多方式，还包括``构造函数传递``，``依赖注入（DI）``和``EventArgs传递``

② 使用Application.Properties字典方式时，``this.Properties["Configuration"]``代表的是文件名为``Configuration``的配置文件，这里还可以用其他名称的文件名指代不同的配置；

③ json文件只要用UTF-8编码就支持中文，键名和键值都可以是中文；

④ 注意最基本的方法是`` _configuration["应用程序设置1111:标题"];``，所以要明确键名的层次；

## 二、Services服务类的使用
### 1. 注册服务
这里的Services文件夹中建立了``Services/ServiceRegistrar.cs``类，为了更好地管理你的服务注册逻辑，可以将所有服务注册代码抽取到 ServiceRegistrar.cs 文件中，使得代码更简洁和可维护。

注意：这里的注册逻辑是``依赖注入``中的``服务注册``。只是为了把相关的服务模块放入``IServiceCollection``中，然后进行统一的注册，实际注册还在程序初始化过程中，这里还可以放入测试的Mock相关的方法类，但是未实现。

目前代码中使用的是注册到MainWindow中，具体实现如下(存疑，是否可以注册到其他的位置)

```CSharp
var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

// 注册 MainWindow
services.AddTransient<MainWindow>();
```

### 2. 使用服务（构造函数注入）
#### a. 首先明确通过直接应用服务提供者和配置DI容器的需求
一般简单的服务注册，直接使用服务提供者和DI容器即可，其具体过程如下：
```CSharp
 // 1. 创建服务容器
var serviceCollection = new ServiceCollection();
 
// 2. 注册服务
serviceCollection.AddTransient<IMyService, MyService>();

// 3. 构建服务提供者
var serviceProvider = serviceCollection.BuildServiceProvider();

// 4. 获取并使用服务
var myService = serviceProvider.GetService<IMyService>();
myService?.Execute();
```
而对wpf应用来说，只需要在app.xaml中注册即可，具体实现如下
```CSharp
// 1. 创建服务容器
var serviceCollection = new ServiceCollection();

// 2. 注册服务
serviceCollection.AddTransient<IMyService, MyService>();

// 3. 注册窗口 (必须注册，以支持构造函数注入)
services.AddSingleton<MainWindow>();

// 4. 构建服务提供者
var serviceProvider = serviceCollection.BuildServiceProvider();

// 5. 设置主窗口并传递依赖项
var mainWindow = Services.GetRequiredService<MainWindow>();
```
此时，相关服务已经注册到了MainWindow，这时在MainWindow.xaml中就可以直接使用了，此时应该用的是构造函数注入
相关代码如下：
```CSharp
// 构造函数注入
public MainWindow(INlogRepositories nlogRepositories, ISqlRepositories sqlRepositories)
```
此时，相关初始化已经在app.xaml.cs和Services/ServiceRegistrar.cs中完成

