# 🏆 Bài 23: Challenge — Plugin System 🔌

> **Dự án lớn:** Xây dựng hệ thống Plugin mở rộng được — ứng dụng thực tế của interface nâng cao!

---

## 📋 Mô tả dự án

Bạn đang xây dựng một **Application Framework** có hệ thống plugin. Các plugin có thể được thêm vào, cấu hình, và quản lý mà **KHÔNG sửa code framework**. Đây là ứng dụng thực tế của Dependency Injection, ISP, và interface patterns.

---

## 🏗️ Kiến trúc

```
  ┌─────────────────────────────────────────────────────────┐
  │                    PLUGIN SYSTEM                         │
  │                                                         │
  │  Interfaces:                                            │
  │  ┌──────────┐ ┌─────────────┐ ┌──────────┐ ┌────────┐ │
  │  │ IPlugin  │ │IConfigurable│ │ ILoggable│ │IHealth │ │
  │  └────┬─────┘ └──────┬──────┘ └────┬─────┘ └───┬────┘ │
  │       │              │             │            │       │
  │  Plugins:                                               │
  │  ┌──────────────────────────────────────────────────┐   │
  │  │ LogPlugin      : IPlugin, IConfigurable          │   │
  │  │ SecurityPlugin : IPlugin, IConfigurable, ILoggable│  │
  │  │ CachePlugin    : IPlugin, IConfigurable, IHealth │   │
  │  │ MetricsPlugin  : IPlugin, ILoggable, IHealth     │   │
  │  └──────────────────────────────────────────────────┘   │
  │                                                         │
  │  Manager:                                               │
  │  ┌──────────────────────────────────────────────────┐   │
  │  │ PluginManager                                    │   │
  │  │ ├── RegisterPlugin(IPlugin)                      │   │
  │  │ ├── InitializeAll()                              │   │
  │  │ ├── ExecuteAll()                                 │   │
  │  │ ├── ConfigureAll(Dictionary<string,string>)      │   │
  │  │ ├── HealthCheckAll()                             │   │
  │  │ └── ShutdownAll()                                │   │
  │  └──────────────────────────────────────────────────┘   │
  │                                                         │
  │  Application:                                           │
  │  ┌──────────────────────────────────────────────────┐   │
  │  │ App ← nhận PluginManager qua Constructor (DI)   │   │
  │  └──────────────────────────────────────────────────┘   │
  └─────────────────────────────────────────────────────────┘
```

---

## 📝 Yêu cầu chi tiết

### Phần 1: Interfaces

```csharp
// Interface chính — TẤT CẢ plugin phải implement
interface IPlugin : IDisposable
{
    string Name { get; }
    string Version { get; }
    string Description { get; }
    bool IsEnabled { get; }

    void Initialize();      // Khởi tạo plugin
    void Execute();         // Chạy chức năng chính
    void Shutdown();        // Dừng plugin

    void Enable();
    void Disable();
}

// Plugin có thể cấu hình
interface IConfigurable
{
    void Configure(string key, string value);
    string GetConfig(string key);
    string[] GetAllConfigKeys();
    void ResetConfig();
}

// Plugin có thể ghi log
interface ILoggable
{
    void SetLogLevel(string level);   // "DEBUG", "INFO", "WARN", "ERROR"
    string[] GetLogs();
    void ClearLogs();
}

// Plugin có thể kiểm tra "sức khỏe"
interface IHealthCheckable
{
    bool IsHealthy();
    string GetHealthStatus();      // "Healthy", "Degraded", "Unhealthy"
    DateTime LastChecked { get; }
}
```

### Phần 2: Concrete Plugins

#### LogPlugin — Ghi log ứng dụng

```
LogPlugin : IPlugin, IConfigurable
├── Config: "log_level" (DEBUG/INFO/WARN/ERROR), "output" (console/file)
├── Initialize(): Thiết lập log system
├── Execute(): Ghi 1 log entry demo
├── Shutdown(): Flush và đóng log
└── Dispose(): Giải phóng resources
```

#### SecurityPlugin — Bảo mật

```
SecurityPlugin : IPlugin, IConfigurable, ILoggable
├── Config: "encryption" (AES/RSA), "key_length" (128/256)
├── Initialize(): Tạo encryption keys
├── Execute(): Mã hóa demo data, log hoạt động
├── Shutdown(): Xóa keys khỏi memory
├── ILoggable: Ghi log mọi hoạt động bảo mật
└── Dispose(): Xóa sạch sensitive data
```

#### CachePlugin — Bộ nhớ đệm

```
CachePlugin : IPlugin, IConfigurable, IHealthCheckable
├── Config: "max_size" (MB), "expiry" (seconds)
├── Fields: cacheItems (string[] keys, string[] values)
├── Initialize(): Khởi tạo cache storage
├── Execute(): Demo thêm/đọc cache items
├── IHealthCheckable: Kiểm tra cache size, hit rate
├── Shutdown(): Flush cache
└── Dispose(): Giải phóng memory
```

#### MetricsPlugin — Thu thập metrics

```
MetricsPlugin : IPlugin, ILoggable, IHealthCheckable
├── Fields: metrics (counter, timestamps)
├── Initialize(): Bắt đầu thu thập
├── Execute(): Thu thập metrics (CPU giả lập, Memory giả lập)
├── ILoggable: Log các metric events
├── IHealthCheckable: Kiểm tra metrics pipeline
├── Shutdown(): Ghi report cuối cùng
└── Dispose(): Dọn dẹp
```

### Phần 3: PluginManager

```csharp
class PluginManager : IDisposable
{
    private IPlugin[] plugins;
    private int pluginCount;

    public PluginManager(int maxPlugins) { }

    // Quản lý plugins
    public void RegisterPlugin(IPlugin plugin) { }
    public void RemovePlugin(string name) { }
    public IPlugin? GetPlugin(string name) { }

    // Lifecycle
    public void InitializeAll() { }     // Gọi Initialize() cho TẤT CẢ
    public void ExecuteAll() { }        // Gọi Execute() cho enabled plugins
    public void ShutdownAll() { }       // Gọi Shutdown() cho TẤT CẢ

    // Interface-based operations — dùng ISP!
    public void ConfigurePlugin(string name, string key, string value)
    {
        IPlugin? plugin = GetPlugin(name);
        if (plugin is IConfigurable configurable)
        {
            configurable.Configure(key, value);
        }
        else
        {
            Console.WriteLine($"  ⚠️ Plugin '{name}' không hỗ trợ cấu hình.");
        }
    }

    public void HealthCheckAll()
    {
        // Chỉ check plugin implement IHealthCheckable
        foreach (IPlugin p in GetEnabledPlugins())
        {
            if (p is IHealthCheckable health)
            {
                Console.WriteLine($"  {p.Name}: {health.GetHealthStatus()}");
            }
        }
    }

    public void ShowAllLogs()
    {
        // Chỉ show logs từ plugin implement ILoggable
    }

    // Thống kê
    public void ShowPluginStatus() { }    // In bảng trạng thái tất cả plugins

    // Helpers
    private IPlugin[] GetEnabledPlugins() { }

    // IDisposable — Dispose tất cả plugins
    public void Dispose() { }
}
```

### Phần 4: Application

```csharp
class Application
{
    private string appName;
    private PluginManager pluginManager;   // DI qua constructor!

    public Application(string name, PluginManager manager)
    {
        appName = name;
        pluginManager = manager;
    }

    public void Run()
    {
        Console.WriteLine($"╔══════════════════════════════════════╗");
        Console.WriteLine($"║   🔌 {appName,-31} ║");
        Console.WriteLine($"╚══════════════════════════════════════╝\n");

        // 1. Khởi tạo tất cả plugins
        pluginManager.InitializeAll();

        // 2. Hiển thị trạng thái
        pluginManager.ShowPluginStatus();

        // 3. Chạy tất cả plugins
        pluginManager.ExecuteAll();

        // 4. Health check
        pluginManager.HealthCheckAll();

        // 5. Show logs
        pluginManager.ShowAllLogs();
    }

    public void Shutdown()
    {
        pluginManager.ShutdownAll();
        Console.WriteLine($"\n  🛑 {appName} đã dừng.");
    }
}
```

---

## 🎮 Main Program — Mô phỏng

```csharp
static void Main()
{
    Console.OutputEncoding = System.Text.Encoding.UTF8;

    // 1. Tạo PluginManager
    using PluginManager manager = new PluginManager(10);

    // 2. Đăng ký plugins
    manager.RegisterPlugin(new LogPlugin("v1.0"));
    manager.RegisterPlugin(new SecurityPlugin("v2.1"));
    manager.RegisterPlugin(new CachePlugin("v1.5"));
    manager.RegisterPlugin(new MetricsPlugin("v1.2"));

    // 3. Cấu hình plugins (chỉ IConfigurable)
    manager.ConfigurePlugin("LogPlugin", "log_level", "DEBUG");
    manager.ConfigurePlugin("LogPlugin", "output", "console");

    manager.ConfigurePlugin("SecurityPlugin", "encryption", "AES");
    manager.ConfigurePlugin("SecurityPlugin", "key_length", "256");

    manager.ConfigurePlugin("CachePlugin", "max_size", "512");
    manager.ConfigurePlugin("CachePlugin", "expiry", "3600");

    // MetricsPlugin KHÔNG phải IConfigurable → thông báo không hỗ trợ
    manager.ConfigurePlugin("MetricsPlugin", "interval", "5");

    // 4. Tạo Application (DI: inject PluginManager)
    Application app = new Application("My Awesome App", manager);

    // 5. Chạy
    app.Run();

    // 6. Test disable/enable
    Console.WriteLine("\n═══ DISABLE CachePlugin ═══");
    IPlugin? cache = manager.GetPlugin("CachePlugin");
    cache?.Disable();

    Console.WriteLine("\n═══ CHẠY LẠI (CachePlugin bị disable) ═══");
    manager.ExecuteAll();   // CachePlugin sẽ bị bỏ qua!

    // 7. Health check
    Console.WriteLine("\n═══ HEALTH CHECK ═══");
    manager.HealthCheckAll();

    // 8. Shutdown
    app.Shutdown();
}
// ← using PluginManager → Dispose tất cả plugins tự động!
```

---

## ✅ Output mong đợi (tham khảo)

```
╔══════════════════════════════════════╗
║   🔌 My Awesome App                  ║
╚══════════════════════════════════════╝

═══ KHỞI TẠO PLUGINS ═══
  ✅ LogPlugin v1.0 — Khởi tạo thành công
  ✅ SecurityPlugin v2.1 — Khởi tạo thành công
  ✅ CachePlugin v1.5 — Khởi tạo thành công
  ✅ MetricsPlugin v1.2 — Khởi tạo thành công

═══ TRẠNG THÁI PLUGINS ═══
┌──────────────────┬─────────┬────────────┬──────────────┬──────────────┐
│ Plugin           │ Version │ Enabled    │ Configurable │ HealthCheck  │
├──────────────────┼─────────┼────────────┼──────────────┼──────────────┤
│ LogPlugin        │ v1.0    │ ✅ Enabled │ ✅ Yes       │ ❌ No        │
│ SecurityPlugin   │ v2.1    │ ✅ Enabled │ ✅ Yes       │ ❌ No        │
│ CachePlugin      │ v1.5    │ ✅ Enabled │ ✅ Yes       │ ✅ Yes       │
│ MetricsPlugin    │ v1.2    │ ✅ Enabled │ ❌ No        │ ✅ Yes       │
└──────────────────┴─────────┴────────────┴──────────────┴──────────────┘

═══ THỰC THI PLUGINS ═══
  🔌 [LogPlugin] Ghi log: "Application event logged"
     Config: log_level=DEBUG, output=console
  🔌 [SecurityPlugin] Mã hóa data bằng AES-256...
     🔐 Log: "Encryption performed successfully"
  🔌 [CachePlugin] Thêm 3 items vào cache (max: 512MB)
     Cache hit rate: 85%
  🔌 [MetricsPlugin] Thu thập metrics: CPU=45%, Memory=62%

═══ HEALTH CHECK ═══
  CachePlugin: ✅ Healthy (85% hit rate, 23/512 MB used)
  MetricsPlugin: ✅ Healthy (Pipeline active, 0 errors)

═══ DISABLE CachePlugin ═══
  🔴 CachePlugin đã bị disable.

═══ CHẠY LẠI (CachePlugin bị disable) ═══
  🔌 [LogPlugin] Ghi log: "Application event logged"
  🔌 [SecurityPlugin] Mã hóa data bằng AES-256...
  ⏸ CachePlugin — DISABLED, bỏ qua.
  🔌 [MetricsPlugin] Thu thập metrics...

═══ SHUTDOWN ═══
  🛑 LogPlugin — Flushed & closed.
  🛑 SecurityPlugin — Keys cleared.
  🛑 CachePlugin — Cache flushed.
  🛑 MetricsPlugin — Final report saved.

  🛑 My Awesome App đã dừng.
  🗑️ PluginManager disposed: 4 plugins released.
```

---

## 🎯 Tiêu chí đánh giá

| Tiêu chí | Điểm | Mô tả |
|-----------|-------|--------|
| Interfaces đúng (ISP) | 20% | 4 interfaces nhỏ, mỗi cái 1 mục đích |
| Plugins implement đúng | 25% | 4 plugins, mỗi cái implement interface khác nhau |
| PluginManager | 20% | Register, Execute, Config qua interface type check |
| IDisposable & using | 15% | PluginManager + plugins dispose đúng cách |
| Dependency Injection | 10% | Application nhận PluginManager qua constructor |
| Code quality | 10% | Clean code, naming, comments |

---

## 💡 Bonus (tùy chọn)

1. **Thêm plugin mới:** `NotificationPlugin : IPlugin, IConfigurable, ILoggable`
   - Config: "channel" (email/sms/push), "recipient"
   - Không sửa PluginManager!

2. **Plugin Priority:** Thêm `IPlugin.Priority` (int), ExecuteAll() theo thứ tự priority

3. **Plugin Dependencies:** `IPlugin.Dependencies` → string[] tên plugins cần chạy trước

4. **Event System:** `interface IEventHandler { void OnEvent(string eventName, string data); }`
   - PluginManager phát events, plugins lắng nghe

5. **Export Plugin Status:** Implement `IExportable` cho PluginManager — export trạng thái ra text

> **Mục tiêu:** Hoàn thành trong 60-90 phút. Đây là bài tổng hợp tất cả kiến thức interface nâng cao! 🚀
