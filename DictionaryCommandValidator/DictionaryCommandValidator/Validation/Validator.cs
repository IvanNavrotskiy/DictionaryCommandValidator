using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator.Validation
{
    class Validator
    {
        public static bool Do(object obj, ValidationProperty[] props, out string message)
        {
            message = null;

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
                        if (prop.IsValid(item, out message))
                            continue;
                        else
                            return false;
                    }

                    var step = prop.PathArray[0];
                    var value = objDict[step];
                    var newProps = prop.GetChildProperty().ToArray();

                    if (value is Dictionary<string, object>)
                    {
                        var result = Do(value as Dictionary<string, object>, newProps, out message);
                        message = $"{step}:{message}";
                        return result;
                    }

                    if (value is object[])
                    {
                        var result = Do(value as object[], newProps, out message);
                        message = $"{step}.{message}";
                        return result;
                    }
                }
            }

            return true;
        }
    }
}
