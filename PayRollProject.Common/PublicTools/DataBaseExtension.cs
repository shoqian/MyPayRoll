namespace PayRollProject.Common.PublicTools
{
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;

    public static class DataBaseExtension
    {
        /// <summary>
        /// متد تشخیص جداول
        /// </summary>
        /// <typeparam name="BaseType">مدل اینترفیس</typeparam>
        /// <param name="modelBuilder">ورودی</param>
        /// <param name="assembly">ورودی</param>
        public static void VerifyEntities<BaseType>(this ModelBuilder modelBuilder, params Assembly[] assembly)
        {
            // Reflection
            IEnumerable<Type> types = assembly.SelectMany(a => a.GetExportedTypes())
                .Where(c => c.IsClass && c.IsPublic && !c.IsAbstract && typeof(BaseType).IsAssignableFrom(c));

            foreach (var type in types)
                modelBuilder.Entity(type);
        }
    }
}