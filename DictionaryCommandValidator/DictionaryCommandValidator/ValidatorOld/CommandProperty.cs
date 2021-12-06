using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator.ValidatorOld
{
    class CommandProperty
    {
        public string Path { get; set; }
        public BaseValidationSettings Settings { get; set; }
        public CommandProperty InnerProperty { get; set; }
        public string Message { get; private set; }

        public static CommandProperty NotNull(string prop) => new CommandProperty() { Path = prop, Settings = new NotNullValidationSettings() };

        public bool IsValid(object obj, out string message)
        {
            message = null;

            //if (InnerProperty != null && obj is Dictionary<string, object>)
            //    return InnerProperty.IsValid(obj[Path], out message);

            if (!Settings?.IsConditionSatisfied(obj, Path) ?? false)
            {
                message = Settings?.GetMessage(Path) ?? String.Empty;
                return false;
            }

            return true;
        }
    }
}
