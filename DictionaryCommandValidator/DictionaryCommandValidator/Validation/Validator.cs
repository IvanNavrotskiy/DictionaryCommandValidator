using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator.Validation
{
    class Validator
    {
        public static bool Do(object obj, Property[] props, out string message)
        {
            foreach (var prop in props)
            {
                var objDict = obj as Dictionary<string, object>;
                if (objDict != null)
                {
                    string[] pathArray = prop.Path.Split(".");
                    if (pathArray.Length == 1)
                        return prop.IsValid(obj, out message);

                    var newProp = new Property() 
                    { 
                        Conditions = prop.Conditions, Path = String.Join(".", pathArray.Skip(1).ToArray()) 
                    };
   
                    var value = objDict[pathArray[0]];
                    if (value is Dictionary<string, object>)
                    {
                        return Do(value as Dictionary<string, object>, newProp.ToArray(), out message);
                    }

                    if (value is object[])
                    {
                        return Do(value as object[], newProp.ToArray(), out message);
                    }
                }

                var objArray = obj as object[];
                if (objArray != null)
                {
                    string tmpMess = null;
                    var res =  objArray.Cast<Dictionary<string, object>>().All(d => Do(d, prop.ToArray(), out tmpMess));
                    message = tmpMess;
                    return res;
                }
            }

            message = null;
            return true;
        }

    }
}
