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