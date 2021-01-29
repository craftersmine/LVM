using craftersmine.LVM.Core.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Extensions
{
    /// <summary>
    /// Contains <see cref="object"/> extansions
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Gets methods with <see cref="LuaCallbackAttribute"/>
        /// </summary>
        /// <param name="obj">Methods of which object get</param>
        /// <returns><see cref="MethodInfo"/> array of methods</returns>
        public static MethodInfo[] GetLuaCallbacks(this object obj)
        {
            Type objType = obj.GetType();
            var reflectedMethods = objType.GetMethods().Where(m => m.GetCustomAttributes(typeof(LuaCallbackAttribute), false).Length > 0).ToArray();
            return reflectedMethods;
        }

        /// <summary>
        /// Gets method with specified name and <see cref="LuaCallbackAttribute"/>
        /// </summary>
        /// <param name="obj">Method of which object get</param>
        /// <param name="name">Name of method to get</param>
        /// <returns><see cref="MethodInfo"/> of method</returns>
        public static MethodInfo GetLuaCallback(this object obj, string name)
        {
            Type objType = obj.GetType();
            var reflectedMethod = objType.GetMethod(name);
            var attr = reflectedMethod.GetCustomAttribute(typeof(LuaCallbackAttribute));
            if (attr != null)
                return reflectedMethod;
            return null;
        }

        /// <summary>
        /// Gets properties with <see cref="LuaCallbackAttribute"/>
        /// </summary>
        /// <param name="obj">Properties of which object get</param>
        /// <returns><see cref="PropertyInfo"/> array of properties</returns>
        public static PropertyInfo[] GetLuaFields(this object obj)
        {
            Type objType = obj.GetType();
            var reflectedProperties = objType.GetProperties().Where(m => m.GetCustomAttributes(typeof(LuaCallbackAttribute), false).Length > 0).ToArray();
            return reflectedProperties;
        }

        /// <summary>
        /// Gets properties with specified name and <see cref="LuaCallbackAttribute"/>
        /// </summary>
        /// <param name="obj">Properties of which object get</param>
        /// <param name="name">Name of property to get</param>
        /// <returns><see cref="PropertyInfo"/> of property</returns>
        public static PropertyInfo GetLuaField(this object obj, string name)
        {
            Type objType = obj.GetType();
            var reflectedProperty = objType.GetProperty(name);
            var attr = reflectedProperty.GetCustomAttribute(typeof(LuaCallbackAttribute));
            if (attr != null)
                return reflectedProperty;
            return null;
        }
    }
}
