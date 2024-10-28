### 1. 注意.Net Core 3.1 控制台程序和.Net 5及以上的Nlog用法区别
1. .Net Core 3.1控制台程序的用法参考[Nlog Github Tutorial](https://github.com/NLog/NLog/wiki/Getting-started-with-.NET-Core-2---Console-application)
2. .Net 5的WPF应用程序只需要nuget安装Nlog即可

### 2. Nlog.config文件使用一定要复制到bin目录下，不管是更新复制还是始终复制

### 3. WPF 应用程序中使用 NLog 将日志信息输出到 Visual Studio 调试窗口
1. 在 .NET Core 和 .NET 5 以上的版本中，WPF 应用程序的 Console.WriteLine 输出不会自动显示在 Visual Studio 的调试窗口中。这是因为在这些版本中，WPF 应用程序的标准输出（Console）流与 Visual Studio 的输出窗口之间没有直接连接。 在 .NET Framework 中，Console.WriteLine 的输出会自动重定向到 Visual Studio 的输出窗口，因为 WPF 应用程序默认在调试模式下运行时会连接到控制台输出。
2. 以下是示例代码：
```xml
    <!-- 输出到控制台 -->
<targets>
    <target xsi:type="Console" name="logconsole"
        layout="${longdate}|${level}|${message} |${exception:format=tostring}" />
</targets>
    <rules>
    <logger name="*" minlevel="Trace" writeTo="logconsole" />
    </rules>
        <!-- 输出到调试窗口 -->
<targets> 
    <target xsi:type="Debugger" name="debugTarget"
        layout="${longdate}|${level}|${message} |${exception:format=tostring}" />
</targets>
    <rules>
    <logger name="*" minlevel="Trace" writeTo="debugTarget" />
    </rules>

```