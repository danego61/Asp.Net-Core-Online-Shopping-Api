using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShopping.Core.Aspects.Postsharp.AuthorizationAspects
{

    [Serializable]
    public class SecuredOperation : OnMethodBoundaryAspect
    {

        public string[] Rolles;

        public string PropertyName { get; set; }

        public SecuredOperation(params string[] rolles)
        {
            Rolles = rolles;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            try
            {
                foreach (string role in Rolles)
                {
                    if (Thread.CurrentPrincipal.IsInRole(role))
                    {
                        if (PropertyName != null && role != "Admin")
                        {
                            object argument = args.Arguments[0];
                            object propVal = PropertyName == "arg" ? argument : argument.GetType().GetProperty(PropertyName)?.GetValue(argument);
                            int userID = Convert.ToInt32(((ClaimsPrincipal)Thread.CurrentPrincipal).FindFirst(ClaimTypes.NameIdentifier).Value);
                            if (userID != propVal as int?)
                                break;
                            return;
                        }
                        else
                            return;
                    }
                }
            }
            catch
            {
                throw new SecurityException("You are not authorized!");
            }

            throw new SecurityException("You are not authorized!");

        }

    }

}
