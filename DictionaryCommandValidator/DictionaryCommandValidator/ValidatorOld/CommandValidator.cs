using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryCommandValidator.ValidatorOld
{
    class CommandValidator
    {
        public static bool Do(object obj, CommandProperty[] props, out string message)
        {
            message = null;

            foreach (var p in props)
            {
                if (p.InnerProperty != null)
                {

                }

                if (obj is Dictionary<string, object>)
                {
                    return p.IsValid(obj, out message);
                }

                var objArray = obj as object[];
                if (objArray != null)
                {
                    //return objArray.All(o => p.IsValid(o, out message));

                    //objArray.Cast<Dictionary<string, object>>().ToList().ForEach(d => p);

                    //foreach (var item in objArray)
                    //{

                    //}
                }

            }

            return true;

        }

    }
}
