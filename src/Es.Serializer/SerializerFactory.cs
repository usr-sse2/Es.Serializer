// ==++==
//
//  Copyright (c) . All rights reserved.
//
// ==--==
/* ---------------------------------------------------------------------------
 *
 * Author			: v.la
 * Email			: v.la@live.cn
 * Created			: 2015-08-31
 * Class			: SerializerFactory.cs
 *
 * ---------------------------------------------------------------------------
 * */

using System;
using System.Collections.Generic;
using System.Threading;

namespace Es.Serializer
{
    /// <summary>
    /// Class SerializerFactory.
    /// </summary>
    public sealed class SerializerFactory
    {
        private static Dictionary<string, ObjectSerializerBase> _objectSerializerCache;

        private static XmlSerializer _xml;



        private static ObjectSerializerBase _default;

#if NETFULL

        private static DataContractSerializer _dataContract;

        private static SoapSerializer _soap;

        private static BinarySerializer _binary;

#endif

        /// <summary>
        /// Initializes static members of the <see cref="SerializerFactory"/> class.
        /// </summary>
        static SerializerFactory() {
            _objectSerializerCache = new Dictionary<string, ObjectSerializerBase>(StringComparer.OrdinalIgnoreCase);
#if NETFULL
            _binary = new BinarySerializer();
            _soap = new SoapSerializer();
            _objectSerializerCache["soap"] = _soap;
            _objectSerializerCache["binary"] = _binary;

             _dataContract = new DataContractSerializer();
           
            _objectSerializerCache["DataContract"] = _dataContract;
            _objectSerializerCache["dc"] = _dataContract;
#endif
            _xml = new XmlSerializer();
            _objectSerializerCache["xml"] = _xml;

            _default = _xml;
        }

        /// <summary>
        /// Gets the alias.
        /// </summary>
        /// <value>The alias.</value>
        public static IEnumerable<string> Alias
        {
            get
            {
                return _objectSerializerCache.Keys;
            }
        }

        /// <summary>
        /// Gets the specified alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>ObjectSerializerBase.</returns>
        public static ObjectSerializerBase Get(string alias) {
            return _objectSerializerCache[alias];
        }

        /// <summary>
        /// Determines whether [contains] [the specified alias].
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns><c>true</c> if [contains] [the specified alias]; otherwise, <c>false</c>.</returns>
        public static bool Contains(string alias) {
            return _objectSerializerCache.ContainsKey(alias);
        }

        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <value>The default.</value>
        public static ObjectSerializerBase Default
        {
            get { return _default; }
        }

        /// <summary>
        /// Sets the default.
        /// </summary>
        /// <param name="def">The definition.</param>
        /// <returns>ObjectSerializerBase.</returns>
        public static ObjectSerializerBase SetDefault(ObjectSerializerBase def) {
            ObjectSerializerBase oldValue, currentValue;
            currentValue = _default;
            do {
                oldValue = currentValue;
                currentValue = Interlocked.CompareExchange(ref _default, def, oldValue);
            }
            while (currentValue != oldValue);
            return _default;
        }

        /// <summary>
        /// Adds the serializer.
        /// </summary>
        /// <typeparam name="TSerializer">The type of the t serializer.</typeparam>
        /// <param name="alias">The alias.</param>
        public static void AddSerializer<TSerializer>(string alias)
           where TSerializer : ObjectSerializerBase {
            AddSerializer<TSerializer>(new[] { alias });
        }

        /// <summary>
        /// Adds the serializer.
        /// </summary>
        /// <typeparam name="TSerializer">The type of the t serializer.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="alias">The alias.</param>
        public static void AddSerializer<TSerializer>(TSerializer instance, string alias)
           where TSerializer : ObjectSerializerBase {
            AddSerializer(instance, new[] { alias });
        }

        /// <summary>
        /// Adds the serializer.
        /// </summary>
        /// <typeparam name="TSerializer">The type of the t serializer.</typeparam>
        /// <param name="alias">The alias.</param>
        public static void AddSerializer<TSerializer>(string[] alias)
          where TSerializer : ObjectSerializerBase {
            var instance = Activator.CreateInstance(typeof(TSerializer)) as ObjectSerializerBase;
            AddSerializer(instance, alias);
        }

        /// <summary>
        /// Adds the serializer.
        /// </summary>
        /// <typeparam name="TSerializer">The type of the t serializer.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="alias">The alias.</param>
        public static void AddSerializer<TSerializer>(TSerializer instance, string[] alias)
          where TSerializer : ObjectSerializerBase {
            lock (_objectSerializerCache) {
                foreach (var name in alias)
                    _objectSerializerCache[name] = instance;
            }
        }
    }
}