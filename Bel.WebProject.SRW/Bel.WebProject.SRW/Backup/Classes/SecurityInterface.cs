using System;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Text;
using APB.Framework.Security.Attributes;

namespace APB.Framework.Security
{

    public sealed class SecurityInterface
    {

        private object Parent;

        public void SetParent(object parent)
        {
            this.Parent = parent;
        }

        public Nullable<int> ObjectId
        {
            get
            {
                if (Parent == null)
                    throw new Exception("O método SetParent deve ser invocado antes de se obter o ID do objeto.");

                if (!Parent.GetType().IsDefined(typeof(SecurityObjectId), true))
                    return null;
                else
                    return ((SecurityObjectId)Parent.GetType().GetCustomAttributes(typeof(SecurityObjectId), true)[0]).ObjectId;
            }

        }

    }
}
