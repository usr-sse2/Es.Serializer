using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

public class TestAttribute : Attribute
{
    public string Description { get; set; }

    public TestAttribute() {
    }

    public TestAttribute(string description) {
        Description = description;
    }
}

public static class TestExcute
{
    public static void Excute(Type t) {
        var dataAccess = Assembly.GetAssembly(t);

        IList<ExecuteFunc> list = new List<ExecuteFunc>();

        foreach (var type in dataAccess.GetTypes()) {
            var clazz = type.GetConstructor(Type.EmptyTypes);
            if (clazz == null) continue;
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance)) {
                var attr = method.GetCustomAttributes(typeof(TestAttribute), false).FirstOrDefault() as TestAttribute;
                if (attr != null) {
                    object instance = Activator.CreateInstance(type);
                    ExecuteFunc func = new ExecuteFunc(instance, method, attr.Description);
                    list.Add(func);
                }
            }
        }

        if (list.Count > 0) {
            StringBuilder text = new StringBuilder();

            lrTag("Select the use-case", "-", 20);

            for (int i = 0; i < list.Count; i++) {
                text.AppendFormat("[{0}] {1}{2}", i + 1, list[i], Environment.NewLine);
            }

            text.AppendLine("\r\n[0] \texit. ");
            string _display = text.ToString();

            Console.Out.WriteLine(ConsoleColor.Green, _display);
            Console.Out.Write("select>");
            string input = Console.ReadLine();
            while (input != "0" && input != "quit" && input != "q" && input != "exit") {
                if (input.Equals("cls", StringComparison.OrdinalIgnoreCase)) {
                    Console.Clear();
                }
                int idx;
                if (int.TryParse(input, out idx)) {
                    if (idx > 0 && idx <= list.Count) {
                        Console.Clear();
                        Console.Out.WriteLine(ConsoleColor.DarkCyan, list[idx - 1] + " Running...");
                        list[idx - 1].Execute();
                        Console.Out.WriteLine(ConsoleColor.DarkCyan, list[idx - 1] + " Complete...");
                    }
                }
                Console.Out.WriteLine();
                lrTag("Select the use-case", "-", 20);
                Console.Out.WriteLine(ConsoleColor.Green, _display);
                Console.Out.Write("select>");
                input = Console.ReadLine();
            }
        }
    }

    public readonly static string SPACE = "  ";

    public static void lrTag(string view, string tag, int size) {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < size; i++) {
            sb.Append(tag);
        }

        Console.Out.WriteLine(ConsoleColor.Yellow, sb + SPACE + view + SPACE + sb);
    }

    public static void WriteLine(this TextWriter writer, ConsoleColor color, string format, params object[] args) {
        Console.ForegroundColor = color;
        writer.WriteLine(format, args);
        Console.ResetColor();
    }

    private class ExecuteFunc
    {
        private object _instance;
        private MethodInfo _method;
        private string _description;

        public ExecuteFunc(object instance, MethodInfo method, string description = "") {
            _instance = instance;
            _method = method;

            if (string.IsNullOrEmpty(description)) {
                _description = string.Concat("\t", instance.GetType().FullName, ".", method.Name);
            }
            else {
                _description = string.Concat("\t", instance.GetType().FullName, "." + method.Name, Environment.NewLine, "\t", description);
            }
        }

        public void Execute() {
            _method.Invoke(_instance, null);
        }

        public override string ToString() {
            return _description;
        }
    }
}