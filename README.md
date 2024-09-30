# SafeSharp

SafeSharp is a .NET Standard 2.0 library that provides exception-safe wrappers around the Aimsharp API methods. It enhances the robustness and reliability of applications or plugins utilizing the Aimsharp API by handling exceptions gracefully and logging errors to organized log files.

## Features

- **Exception-Safe Wrappers**: All Aimsharp API methods are wrapped to handle exceptions, ensuring your application doesn't crash due to unhandled exceptions.
- **Comprehensive Logging**: Error messages are logged to files based on the current 1-hour window, making it easier to debug issues within specific time frames.
- **Thread-Safe Logging**: File writes are thread-safe, preventing concurrent write issues when multiple threads log simultaneously.
- **Easy Integration**: Designed to be a drop-in replacement for direct Aimsharp API calls with minimal changes to your existing codebase.
- **XML Documentation**: All methods include XML documentation, providing IntelliSense support and enhancing code readability.
- **Configurable Settings**: Static classes now offer methods to configure default values and logging behavior.

## Installation

1. **Download the Release**
   - Place the `SafeSharp.dll` and `netstandard.dll` file in your Aimsharp `Bin` directory.
2. **Include the Project**
   - Add the `SafeSharp` dependency to your solution.
   - Alternatively, clone this repo and compile the project and reference the resulting DLL in your application.
3. **Add References**
   - Ensure your project references the necessary assemblies:
     - `using SafeSharp`

## Usage

Here's how you can use the `Aimsharp` and `Logging` classes in your application:

### Example Rotation Usage

SafeSharp is designed to be a drop-in replacement for the Aimsharp API. Here's an example of how you can use the `Aimsharp` wrapper in your rotation:

```csharp
using SafeSharp;
// using AimsharpWow.API; We don't need this anymore, so it's commented out. You can remove it entirely if you want.

namespace AimsharpWow.Modules
{
    public class Asbru : Rotation
    {
        public Asbru()
        {
            Aimsharp.PrintMessage("Constructor");
        }

        public override void LoadSettings()
        {
            Aimsharp.PrintMessage("LoadSettings");
        }

        public override void Initialize()
        {
            Aimsharp.PrintMessage("Initialize");
        }

        public override bool CombatTick()
        {
            return false;
        }

        public override bool OutOfCombatTick()
        {
            return false;
        }

        public override bool MountedTick()
        {
            return false;
        }
    }
}
```

### Configuring Default Values

Before using the Aimsharp wrapper, you can configure the default values to be returned in case of exceptions:

```csharp
using SafeSharp;

// Configure default values
Aimsharp.ConfigureDefaults(defaultInt: 0, defaultFloat: 0f, defaultBool: false);
```

### Using the Static Aimsharp Wrapper

```csharp
using SafeSharp;

public class MyRotation
{
    public void Execute()
    {
        if (Aimsharp.CanCast("Fireball"))
        {
            Aimsharp.Cast("Fireball");
        }
        int playerHealth = Aimsharp.Health();
        if (playerHealth < 50)
        {
            Aimsharp.Cast("Ice Block");
        }
    }
}
```

### Configuring Logging

You can configure the logging behavior:

```csharp
using SafeSharp;

// Configure logging settings
Logging.Configure(logToAimsharp: true, logToFile: true);
```

### Logging Errors

The `Aimsharp` wrapper automatically logs errors using the `Logging` class. However, you can also log custom messages:

```csharp
using SafeSharp;

public class MyClass
{
    public void MyMethod()
    {
        try
        {
            // Your code here
        }
        catch (Exception ex)
        {
            Logging.LogError("An error occurred in MyMethod.");
            Logging.LogError(ex.ToString());
        }
    }
}
```

### Log Files

- Logs are stored in the `Logs` directory within your application's base directory.
- Each log file corresponds to a 1-hour window, e.g., `2024-09-29 05-06.log`.
- Log entries include timestamps with millisecond precision and log levels for clarity.

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Commit your changes with clear commit messages.
4. Create a pull request describing your changes.

Please ensure your code adheres to the existing coding style and includes appropriate documentation.