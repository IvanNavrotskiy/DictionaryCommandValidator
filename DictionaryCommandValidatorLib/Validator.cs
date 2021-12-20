using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator
{
    public class Validator
    {
        /// <summary>
        /// Initialize recursive validation
        /// </summary>
        /// <param name="obj">not null</param>
        /// <param name="props">not null</param>
        /// <param name="failMessage"></param>
        /// <returns></returns>
        public static bool Do(object obj, ValidationProperty[] props, out string failMessage)
        {
            if (obj == null)
                throw new ArgumentNullException("obj is null");

            if (props == null)
                throw new ArgumentNullException("props is null");

            failMessage = null;

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
