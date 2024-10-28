### 1. ע��.Net Core 3.1 ����̨�����.Net 5�����ϵ�Nlog�÷�����
1. .Net Core 3.1����̨������÷��ο�[Nlog Github Tutorial](https://github.com/NLog/NLog/wiki/Getting-started-with-.NET-Core-2---Console-application)
2. .Net 5��WPFӦ�ó���ֻ��Ҫnuget��װNlog����

### 2. Nlog.config�ļ�ʹ��һ��Ҫ���Ƶ�binĿ¼�£������Ǹ��¸��ƻ���ʼ�ո���

### 3. WPF Ӧ�ó�����ʹ�� NLog ����־��Ϣ����� Visual Studio ���Դ���
1. �� .NET Core �� .NET 5 ���ϵİ汾�У�WPF Ӧ�ó���� Console.WriteLine ��������Զ���ʾ�� Visual Studio �ĵ��Դ����С�������Ϊ����Щ�汾�У�WPF Ӧ�ó���ı�׼�����Console������ Visual Studio ���������֮��û��ֱ�����ӡ� �� .NET Framework �У�Console.WriteLine ��������Զ��ض��� Visual Studio ��������ڣ���Ϊ WPF Ӧ�ó���Ĭ���ڵ���ģʽ������ʱ�����ӵ�����̨�����
2. ������ʾ�����룺
```xml
    <!-- ���������̨ -->
<targets>
    <target xsi:type="Console" name="logconsole"
        layout="${longdate}|${level}|${message} |${exception:format=tostring}" />
</targets>
    <rules>
    <logger name="*" minlevel="Trace" writeTo="logconsole" />
    </rules>
        <!-- ��������Դ��� -->
<targets> 
    <target xsi:type="Debugger" name="debugTarget"
        layout="${longdate}|${level}|${message} |${exception:format=tostring}" />
</targets>
    <rules>
    <logger name="*" minlevel="Trace" writeTo="debugTarget" />
    </rules>

```