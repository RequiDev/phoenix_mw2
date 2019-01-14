using Phoenix.ConsoleSystem;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Phoenix.CommandSystem
{
    internal static class CommandHandler
    {
        public static List<Command> Commands = new List<Command>();
        public static void Worker()
        {
            while (Phoenix.Memory.IsProcessRunning)
            {
                var fullCommand = Console.ReadLine();
                var commandArray = fullCommand.ToLower().Split(' ');
                var command = commandArray[0];
                var param = commandArray.Length > 1 ? commandArray[1] : "";
                var value = commandArray.Length > 2 ? commandArray[2] : "";
                HandleCommand(command, param, value);
                Console.WriteCommandLine();
            }
        }

        public static void Setup()
        {
            Console.Title = "";
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteWatermark();

            Commands.Add(new Command("esp", "ESP"));
   //         Commands.Add(new Command("aimbot", "Aimbot"));
			//Commands.Add(new Command("fov", "Field of View"));

			AddParameter("esp", "active", "0", "Wether esp is active or not.");

   //         AddParameter("aimbot", "fov", "1", "Field of view radius for the aimbot.");
   //         AddParameter("aimbot", "smooth", "0", "How much smooth will be applied to the aimbot.");
   //         AddParameter("aimbot", "bone", "14", "On which bone the aimbot will aim.");
   //         AddParameter("aimbot", "key", "1", "Key to press to activate the aimbot.");
   //         AddParameter("aimbot", "visible", "0", "Basic visible check for the aimbot");
   //         AddParameter("aimbot", "norecoil", "1", "Recoil compensation for the aimbot.");

			//AddParameter("fov", "value", "65", "Field of View Value.");
        }

        private static void AddParameter(string command, string parameter, string defaultValue, string desc = "This is a basic parameter")
        {
            GetCommand(command).Parameters.Add(new CommandParameter(parameter, new CommandParameterValue(defaultValue), desc));
        }

        private static void HandleCommand(string command, string parameter, string value)
        {
            switch (command)
            {
                case "load":
                    Load(parameter);
                    break;
                case "save":
                    Save(parameter);
                    break;
                case "help":
                    DisplayHelp();
                    break;
				case "rank":
					GiveMaxRank();
					break;
				case "prestige":
					GivePrestige(parameter);
					break;
				case "perks":
					UnlockPerks();
					break;
				case "weapons":
					UnlockWeapons();
					break;
				default:
                    var cmd = GetCommand(command);
                    if (!cmd)
                    {
                        Console.WriteSuccess($"  Could not find command '{command}'.", false);
                        return;
                    }
                    if (parameter == "")
                    {
                        DisplayParameters(cmd);
                        return;
                    }
                    var param = GetParameter(command, parameter);
                    if (!param)
                    {
                        Console.WriteSuccess($"  Could not find parameter '{parameter}' in command '{command}'.", false);
                        return;
                    }
                    if (value == "")
                    {
                        Console.WriteNotification($"  - {cmd.Name} {param.Name} ({param.Description})\n    Current value of '{command} {parameter}' is {GetParameter(command, parameter).Value}\n");
                        return;
                    }
                    param.Value = new CommandParameterValue(value);
                    if (param.Value.ToFloat() < 0.0f)
                    {
                        Console.WriteSuccess($"  Value has to be convertable to a digit", false);
                        return;
                    }
                    Console.WriteNotification($"  Set value of '{command} {parameter}' to '{value}'.");
                    break;
            }
        }

        public static void Save(string file = "./settings.ini")
        {
            if (string.IsNullOrEmpty(file)) file = "./settings.ini";
            if (!file.EndsWith(".ini")) file += ".ini";
            if (!file.StartsWith("./")) file = "./" + file;
            foreach (var cmd in Commands)
            {
                foreach(var param in cmd.Parameters)
                {
                    WriteValue(cmd.Name, param.Name, param.Value.Value, file);
                }
            }
            //SaveSkins();
            Console.WriteNotification($"  Saved Settings to {file.Replace("./", "")}!");
        }

        public static void Load(string file = "./settings.ini")
        {
            if (string.IsNullOrEmpty(file)) file = "./settings.ini";
            if (!file.EndsWith(".ini")) file += ".ini";
            if (!file.StartsWith("./")) file = "./" + file;
            if (!File.Exists(file))
            {
                Console.WriteSuccess($"  {file.Replace("./", "")} does not exist. Did not change anything.\n", false);
                return;
            }
            foreach (var cmd in Commands)
            {
                foreach (var param in cmd.Parameters)
                {
                    param.Value.Value = ParseInteger(ReadValue(cmd.Name, param.Name, file), param.Value.ToInt32()).ToString();
                }
            }
            //LoadSkins();
            Console.WriteNotification($"  Loaded Settings from {file.Replace("./", "")}!\n");
        }

        private static void DisplayParameters(Command cmd)
        {
            Console.WriteNotification($"  {cmd.Name} ({cmd.Description})");
            cmd.Parameters.ForEach(delegate (CommandParameter param)
            {
                Console.WriteNotification($"    - {cmd.Name} {param.Name} ({param.Description})");
            });
        }

        private static void DisplayHelp()
        {
            Commands.ForEach(delegate (Command pCmd)
            {
                Console.WriteNotification($"  - {pCmd.Name} ({pCmd.Description})");
            });
        }

		private static void GiveMaxRank()
		{
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8B768, 2516000);
		}

		private static void GivePrestige(string value)
		{
			int intVal;
			int.TryParse(value, out intVal);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8B770, intVal);
		}

		private static void UnlockPerks()
		{
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEB7, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEBB, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEC2, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEBC, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEBF, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEBD, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEC5, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEBE, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEC0, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEC6, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEBD, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEC5, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEBE, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEC0, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEC6, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEC6, 999999999);
		}

		private static void UnlockWeapons()
		{
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE20, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE1F, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDA8, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE24, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDF9, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDF8, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BD9E, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDFD, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE30, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE2F, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDAC, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE34, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE01, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE00, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDA0, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE05, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE08, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE09, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDA3, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE0D, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDF1, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDF0, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BD9C, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDF5, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE28, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE27, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDAA, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE2C, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE38, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE37, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDAE, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE3C, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE11, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE10, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDA4, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE15, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE6E, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE6D, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDBE, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE71, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE59, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE58, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDB8, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE5C, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE52, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE51, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDB6, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE55, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE67, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE66, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDBC, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE6A, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE60, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE5F, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDBA, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE63, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE7C, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE7B, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDC2, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE7F, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE75, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE74, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDC0, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE78, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE8F, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE80, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDE4, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE93, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE83, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE82, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDC4, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE86, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE19, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE18, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDA6, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE1C, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE40, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE3F, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDB0, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE42, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE46, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE45, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDB2, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE48, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE8A, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE89, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDE2, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE8C, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE4C, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE4B, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDB4, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE4E, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BF19, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BF1D, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE98, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDC8, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE9A, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDCA, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE9C, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDCC, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE96, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDC6, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEA2, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDD2, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEA4, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDD4, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BE9E, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDCE, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEA8, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDD8, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEA0, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDD0, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEA6, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDD6, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEAC, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDDC, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEB0, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDE0, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEAA, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDDA, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEAE, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDDE, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEB2, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDE6, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEB5, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDEC, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEB6, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDEE, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEB4, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDEA, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEB3, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BDE8, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEB7, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEBB, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEC2, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEBC, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEBF, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEBD, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEC5, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEBE, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEC0, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEC6, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEBD, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEC5, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEBE, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEC0, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEC6, 999999999);
			Phoenix.Memory.Write<int>((System.IntPtr)0x1B8BEC6, 999999999);
		}

		private static Command GetCommand(string command)
        {
            return Commands.FirstOrDefault(com => com.Name == command);

        }
        public static CommandParameter GetParameter(string command, string parameter)
        {
            return GetCommand(command).Parameters.FirstOrDefault(param => param.Name == parameter);
        }

        #region ReadWrite
        public static void WriteValue(string section, string key, string value, string File = ".\\settings.ini")
        {
            WritePrivateProfileString(section, key, value, File);
        }

        public static string ReadValue(string section, string key, string File = ".\\settings.ini")
        {
            var temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", temp, 255, File);

            return temp.ToString();
        }
        #endregion
        #region Parsing
        public static bool ParseBoolean(string input, bool defaultVal = false)
        {
            if (string.IsNullOrEmpty(input))
                return defaultVal;

            bool output;

            if (!bool.TryParse(input, out output))
                return defaultVal;

            return output;
        }

        public static int ParseInteger(string input, int defaultVal = 0)
        {
            if (string.IsNullOrEmpty(input))
                return defaultVal;

            int output;

            if (!int.TryParse(input, out output))
                return defaultVal;

            return output;
        }

        public static float ParseFloat(string input, float defaultVal = 0.0f)
        {
            if (string.IsNullOrEmpty(input))
                return defaultVal;

            float output;

            if (!float.TryParse(input, out output))
                return defaultVal;

            return output;
        }
        #endregion
        #region Native
        [DllImport("kernel32")]
        static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        #endregion
    }
}
