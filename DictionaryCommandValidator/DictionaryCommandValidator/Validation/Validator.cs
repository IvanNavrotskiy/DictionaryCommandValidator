using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator.Validation
{
    class Validator
    {
        public static bool Do(object obj, ValidationProperty[] props, out string failMessage)
        {
            failMessage = null;

            if (obj == null)
                throw new ArgumentNullException("obj");

            object[] objArray = obj as object[] ?? new object[] { obj };
            foreach (var item in objArray)
            {
                var objDict = item as Dictionary<string, object>;
                if (objDict == null)
                    continue;

                foreach (var prop in props)
                {
                    if (prop.PathLength == 1)
                    {
                        if (prop.IsValid(objDict, out failMessage))
                            continue;
                        else
                            return false;
                    }

                    var step = prop.PathArray[0];
                    var value = objDict[step];
                    var newProp = (ValidationProperty)prop.Clone();
                    newProp.ShiftPath();

                    if (value is Dictionary<string, object>)
                    {
                        var isValid = Do(value as Dictionary<string, object>, new[] { newProp }, out failMessage);
                        if (!isValid) 
                            failMessage = $"{step}.{failMessage}";

                        return isValid;
                    }

                    if (value is object[])
                    {
                        var isValid = Do(value as object[], new[] { newProp }, out failMessage);
                        if (!isValid)
                            failMessage = $"{step}.{failMessage}";

                        return isValid;
                    }
                }
            }

            return true;
        }
    }
}
