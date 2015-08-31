// ==++==
//
//  Copyright (c) . All rights reserved.
//
// ==--==
/* ---------------------------------------------------------------------------
 *
 * Author			: v.la
 * Email			: v.la@live.cn
 * Created			: 2015-08-27
 * Class			: Program.cs
 *
 * ---------------------------------------------------------------------------
 * */

using Es.Serializer;
/// <summary>
/// The PerformanceTest namespace.
/// </summary>
namespace PerformanceTest
{
    /// <summary>
    /// Class Program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args) {
            SerializerFactory.AddSerializer<JilSerializer>("jil");
            SerializerFactory.AddSerializer<JsonNetSerializer>("jsonNet");
            SerializerFactory.AddSerializer<ProtoBufSerializer>("ProtoBuf");
            TestExcute.Excute(typeof(Program));
        }
    }
}