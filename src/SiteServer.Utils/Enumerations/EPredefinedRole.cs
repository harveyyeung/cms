using System.Collections.Generic;

namespace SiteServer.Utils.Enumerations
{
	public enum EPredefinedRole
	{
        ConsoleAdministrator,				//超级管理员
		SystemAdministrator,				//站点管理员
        Administrator,						//管理员
	}

	public class EPredefinedRoleUtils
	{
		public static string GetValue(EPredefinedRole type)
		{
		    if (type == EPredefinedRole.ConsoleAdministrator)
			{
				return "ConsoleAdministrator";
			}
		    if (type == EPredefinedRole.SystemAdministrator)
		    {
		        return "SystemAdministrator";
		    }
		    if (type == EPredefinedRole.Administrator)
		    {
		        return "Administrator";
		    }
		    return string.Empty;
		}

		public static string GetText(EPredefinedRole type)
		{
		    if (type == EPredefinedRole.ConsoleAdministrator)
			{
				return "超级管理员";
			}
		    if (type == EPredefinedRole.SystemAdministrator)
		    {
		        return "站点管理员";
		    }
		    if (type == EPredefinedRole.Administrator)
		    {
		        return "管理员";
		    }
		    return string.Empty;
		}

		public static bool IsPredefinedRole(string roleName)
		{
			var retval = false;
			if (Equals(EPredefinedRole.ConsoleAdministrator, roleName))
			{
				retval = true;
			}
            else if (Equals(EPredefinedRole.SystemAdministrator, roleName))
			{
				retval = true;
			}
			else if (Equals(EPredefinedRole.Administrator, roleName))
			{
				retval = true;
            }

			return retval;
		}

        public static EPredefinedRole GetEnumType(string typeStr)
        {
            var retval = EPredefinedRole.Administrator;

            if (Equals(EPredefinedRole.ConsoleAdministrator, typeStr))
            {
                retval = EPredefinedRole.ConsoleAdministrator;
            }
            else if (Equals(EPredefinedRole.SystemAdministrator, typeStr))
            {
                retval = EPredefinedRole.SystemAdministrator;
            }

            return retval;
        }

		public static EPredefinedRole GetEnumTypeByRoles(IList<string> roles)
		{
			var isConsoleAdministrator = false;
			var isSystemAdministrator = false;

			if (roles != null)
			{
				foreach (var role in roles)
				{
					if (Equals(EPredefinedRole.ConsoleAdministrator, role))
					{
						isConsoleAdministrator = true;
					}
                    else if (Equals(EPredefinedRole.SystemAdministrator, role))
					{
						isSystemAdministrator = true;
					}
				}
			}
			if (isConsoleAdministrator) return EPredefinedRole.ConsoleAdministrator;
            if (isSystemAdministrator) return EPredefinedRole.SystemAdministrator;
            return EPredefinedRole.Administrator;
		}

		public static bool Equals(EPredefinedRole type, string typeStr)
		{
			if (string.IsNullOrEmpty(typeStr)) return false;
			if (string.Equals(GetValue(type).ToLower(), typeStr.ToLower()))
			{
				return true;
			}
			return false;
		}

        public static bool Equals(string typeStr, EPredefinedRole type)
        {
            return Equals(type, typeStr);
        }

		

		public static bool IsConsoleAdministrator(IList<string> roles)
		{
			return roles != null && roles.Contains(GetValue(EPredefinedRole.ConsoleAdministrator));
		}

		public static bool IsSystemAdministrator(IList<string> roles)
		{
		    return roles != null && (roles.Contains(GetValue(EPredefinedRole.ConsoleAdministrator)) || roles.Contains(GetValue(EPredefinedRole.SystemAdministrator)));

   //         var retval = false;
			//if (roles != null && roles.Length > 0)
			//{
			//	foreach (var role in roles)
			//	{
			//	    if (Equals(EPredefinedRole.ConsoleAdministrator, role))
			//		{
			//			retval = true;
			//			break;
			//		}
			//	    if (Equals(EPredefinedRole.SystemAdministrator, role))
			//	    {
			//	        retval = true;
			//	        break;
			//	    }
			//	}
			//}
			//return retval;
		}

        public static bool IsAdministrator(List<string> roles)
        {
            return roles != null && (roles.Contains(GetValue(EPredefinedRole.ConsoleAdministrator)) || roles.Contains(GetValue(EPredefinedRole.SystemAdministrator)) || roles.Contains(GetValue(EPredefinedRole.Administrator)));

            //var retval = false;
            //if (roles != null && roles.Length > 0)
            //{
            //    foreach (var role in roles)
            //    {
            //        if (Equals(EPredefinedRole.ConsoleAdministrator, role))
            //        {
            //            retval = true;
            //            break;
            //        }
            //        if (Equals(EPredefinedRole.SystemAdministrator, role))
            //        {
            //            retval = true;
            //            break;
            //        }
            //        if(Equals(EPredefinedRole.Administrator,role))
            //        {
            //            retval = true;
            //            break;
            //        }
            //    }
            //}
            //return retval;
        }

		public static List<string> GetAllPredefinedRoleName()
		{
            return new List<string>
		    {
		        GetValue(EPredefinedRole.Administrator),
		        GetValue(EPredefinedRole.SystemAdministrator),
		        GetValue(EPredefinedRole.ConsoleAdministrator)
		    };
		}

        public static List<EPredefinedRole> GetAllPredefinedRole()
        {
            return new List<EPredefinedRole>
            {
                EPredefinedRole.Administrator,
                EPredefinedRole.SystemAdministrator,
                EPredefinedRole.ConsoleAdministrator
            };
        }
    }
}
