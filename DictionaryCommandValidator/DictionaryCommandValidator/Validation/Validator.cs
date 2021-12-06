using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator.Validation
{
    class Validator
    {
        //public CommandProperty[] Propperties { get; set; }
        public static bool Do(object obj, Property[] props, string message)
        {
            var objDict = obj as Dictionary<string, object>;
            if (objDict != null)
            {
                return props.All(p => DoDictionaryRecursive(objDict, p, message));
            }

            //var objArray = obj as object[]

            //foreach (var prop in props)
            //{
            //    var objDict = obj as 
            //    if (obj is Dictionary<string, object>)
            //    {
            //        var res = DoDictionaryRecursive()
            //    }

            //}


            //
            message = null;
            //return props.All(p =>
            //{
            //    if (obj is Dictionary<string, object>)
            //        return DoDictionaryRecursive(obj as Dictionary<string, object>, p, message));

            return true;

            //});
        }


        private static bool DoArrayRecursive(object[] array, Property prop, string message)
        {
            return array.Cast<Dictionary<string, object>>().All(d => DoDictionaryRecursive(d, prop, message));
        }

        private static bool DoDictionaryRecursive(Dictionary<string, object> dict, Property prop, string message)
        {
            string[] pathArray = prop.Path.Split(".");

            if (pathArray.Length == 1)
                return prop.IsValid(dict, message);

            // deep clone?
            prop.Path = String.Join(".", pathArray.Skip(1).ToArray());
            var value = dict[pathArray[0]];
           
            if (value is Dictionary<string, object>)
            {
                return DoDictionaryRecursive(value as Dictionary<string, object>, prop, message);
            }

            if (value is object[])
            {
                return DoArrayRecursive(value as object[], prop, message);
                //return (value as object[]).Cast<Dictionary<string, object>>().All(d => DoDictionaryRecursive(d, prop, message));
            }

            return true;
        }

    }
}
