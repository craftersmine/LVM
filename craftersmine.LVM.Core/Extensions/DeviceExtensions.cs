using craftersmine.LVM.Core.Attributes;
using craftersmine.LVM.Core.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Extensions
{
    /// <summary>
    /// Contains extensions for <see cref="IDevice"/>
    /// </summary>
    public static class DeviceExtensions
    {
        /// <summary>
        /// Tries to call provided method by name and return it's values, otherwise throws an exception
        /// </summary>
        /// <param name="dev">Device</param>
        /// <param name="method">Calling method name</param>
        /// <param name="args">Calling method arguments</param>
        /// <returns>Calling method values</returns>
        /// <exception cref="craftersmine.LVM.Core.Exceptions.NoSuchMethodException">When device method with such method not found</exception>
        /// <exception cref="ArgumentNullException">When dev argument is null or method argument is null or empty</exception>
        public static object InvokeMethod(this IDevice dev, string method, params object[] args)
        {
            if (dev == null)
                throw new ArgumentNullException(nameof(dev), "Device is null");
            if (method.IsNullEmptyOrWhitespace())
                throw new ArgumentNullException(nameof(method), "Method name is null or empty");

            var deviceType = dev.GetType();

            if (deviceType.IsSubclassOf(typeof(BaseDevice)))
            {
                var luaCallbacks = deviceType.GetMethods().Where(m => m.GetCustomAttributes(typeof(LuaCallbackAttribute), false).Length > 0).ToArray();

                foreach (var luaCallback in luaCallbacks)
                {
                    if (luaCallback.Name == method)
                    {
                        var _params = luaCallback.GetParameters();
                        if (args.Length > _params.Length)
                            continue;
                        object[] _args = new object[(_params.Length - args.Length) + args.Length];
                        for (int i = 0; i < _args.Length; i++)
                            if (_params[i].HasDefaultValue)
                                _args[i] = _params[i].DefaultValue;

                        for (int i = 0; i < args.Length; i++)
                            _args[i] = args[i];

                        try
                        {
                            return luaCallback.Invoke(dev, _args);
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                    }
                }
            }

            throw new NoSuchMethodException(method, "No such method found in specified device");
        }

        /// <summary>
        /// Gets a value of specified property of specified device
        /// </summary>
        /// <param name="dev">Device to get property</param>
        /// <param name="property">Property name to get value</param>
        /// <returns><see cref="object"/> with property value</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="dev"/> or <paramref name="property"/> is null</exception>
        /// <exception cref="NoSuchPropertyException">When no property found with specified name</exception>
        public static object GetPropertyValue(this IDevice dev, string property)
        {
            if (dev == null)
                throw new ArgumentNullException(nameof(dev), "Device is null");
            if (property.IsNullEmptyOrWhitespace())
                throw new ArgumentNullException(nameof(property), "Property name is null or empty");

            var deviceType = dev.GetType();

            if (deviceType.IsSubclassOf(typeof(BaseDevice)))
            {
                var luaFields = deviceType.GetProperties().Where(m => m.GetCustomAttributes(typeof(LuaCallbackAttribute), false).Length > 0).ToArray();

                foreach (var luaField in luaFields)
                {
                    if (luaField.Name == property)
                    {
                        return luaField.GetValue(dev);
                    }
                }
            }
            throw new NoSuchPropertyException(property, "No such property found in specified device");
        }

        /// <summary>
        /// Sets a value of specified property of specified device
        /// </summary>
        /// <param name="dev">Device to set property</param>
        /// <param name="property">Property name to set value</param>
        /// <param name="value">Property value to set</param>
        /// <returns><see cref="object"/> with property value</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="dev"/> or <paramref name="property"/> is null</exception>
        /// <exception cref="NoSuchPropertyException">When no property found with specified name</exception>
        public static void SetPropertyValue(this IDevice dev, string property, object value)
        {
            if (dev == null)
                throw new ArgumentNullException(nameof(dev), "Device is null");
            if (property.IsNullEmptyOrWhitespace())
                throw new ArgumentNullException(nameof(property), "Property name is null or empty");

            var deviceType = dev.GetType();

            if (deviceType.IsSubclassOf(typeof(BaseDevice)))
            {
                var luaFields = deviceType.GetProperties().Where(m => m.GetCustomAttributes(typeof(LuaCallbackAttribute), false).Length > 0).ToArray();

                foreach (var luaField in luaFields)
                {
                    if (luaField.Name == property)
                    {
                        luaField.SetValue(dev, value);
                        return;
                    }
                }
            }
            throw new NoSuchPropertyException(property, "No such property found in specified device");
        }

        /// <summary>
        /// Gets <see cref="LuaCallbackAttribute"/> of specified method of device
        /// </summary>
        /// <param name="dev">Device</param>
        /// <param name="method">Method name to get <see cref="LuaCallbackAttribute"/></param>
        /// <returns><see cref="LuaCallbackAttribute"/> of method</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="dev"/> or <paramref name="method"/> is null</exception>
        /// <exception cref="NoSuchMethodException">When method with specified name is not found</exception>
        public static LuaCallbackAttribute GetLuaCallbackAttribute(this IDevice dev, string method)
        {

            if (dev == null)
                throw new ArgumentNullException(nameof(dev), "Device is null");
            if (method.IsNullEmptyOrWhitespace())
                throw new ArgumentNullException(nameof(method), "Method name is null or empty");

            var deviceType = dev.GetType();

            if (deviceType.IsSubclassOf(typeof(BaseDevice)))
            {
                var luaCallbacks = deviceType.GetMethods().Where(m => m.GetCustomAttributes(typeof(LuaCallbackAttribute), false).Length > 0).ToArray();

                foreach (var luaCallback in luaCallbacks)
                {
                    if (luaCallback.Name == method)
                    {
                        return (LuaCallbackAttribute)luaCallback.GetCustomAttributes(typeof(LuaCallbackAttribute), false)[0];
                    }
                }

                var luaFields = deviceType.GetProperties().Where(m => m.GetCustomAttributes(typeof(LuaCallbackAttribute), false).Length > 0).ToArray();

                foreach (var luaField in luaFields)
                {
                    if (luaField.Name == method)
                    {
                        return (LuaCallbackAttribute)luaField.GetCustomAttributes(typeof(LuaCallbackAttribute), false)[0];
                    }
                }
            }
            throw new NoSuchMethodException(method, "No such method found in specified device");
        }

        /// <summary>
        /// Gets device type name from <see cref="DeviceComponentAttribute"/>
        /// </summary>
        /// <param name="component">Device which type name should be got</param>
        /// <returns><see cref="string"/> with device type name in lowercase</returns>
        /// <exception cref="InvalidDeviceException">If specified device doesn't have a <see cref="DeviceComponentAttribute"/></exception>
        public static string GetDeviceTypeName(this IDevice component)
        {
            var attribute = component.GetComponentAttribute();
            if (attribute != null)
                return attribute.ComponentType.ToLower();
            throw new InvalidDeviceException(component.GetType().ToString() + " type is not valid device type! Did you forgot to add DeviceComponentAttribute?");
        }
    }
}
