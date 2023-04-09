using System.Windows.Markup;
using System;
using System.Linq;

namespace CV19.Infrastructure.Common
{
    [MarkupExtensionReturnType(typeof(int[]))]
    class StringToIntArray : MarkupExtension 
    {
        /// <summary>
        /// Класс MarkupExtension позволяет осуществлять использование механизма "расширения разметки", внутри представления,
        /// при обработке экземпляра для текущего ссылочного типа.
        /// </summary>
        
        [ConstructorArgument("_str")]
        public string str { get; set; }
        public char separator { get; private set; } = ';';
        public StringToIntArray() { }
        public StringToIntArray(string _str) => str = _str;
        public override object ProvideValue(IServiceProvider serviceProvider)  => str
                .Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries)
                .DefaultIfEmpty()
                .Select(int.Parse)
                .ToArray();
    }
}
