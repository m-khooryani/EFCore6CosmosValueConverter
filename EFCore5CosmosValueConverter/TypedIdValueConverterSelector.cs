using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace EFCore6CosmosValueConverter
{
    public class TypedIdValueConverterSelector : ValueConverterSelector
    {
        private readonly ConcurrentDictionary<(Type ModelClrType, Type ProviderClrType), ValueConverterInfo> _converters
            = new();

        private static readonly Dictionary<Type, Type> _lookUp = new()
        {
            { typeof(Guid), typeof(GuidTypedIdValueConverter<>) },
        };

        public TypedIdValueConverterSelector(ValueConverterSelectorDependencies dependencies)
            : base(dependencies)
        {
        }

        public override IEnumerable<ValueConverterInfo> Select(Type modelClrType, Type providerClrType = null)
        {
            var baseConverters = base.Select(modelClrType, providerClrType);
            foreach (var converter in baseConverters)
            {
                yield return converter;
            }

            var underlyingModelType = UnwrapNullableType(modelClrType);
            var underlyingProviderType = UnwrapNullableType(providerClrType);

            foreach (var item in _lookUp)
            {
                var isTypedId = typeof(TypedId<>).MakeGenericType(item.Key).IsAssignableFrom(underlyingModelType);
                if (isTypedId)
                {
                    var converterType = item.Value.MakeGenericType(underlyingModelType);

                    yield return _converters.GetOrAdd((underlyingModelType, item.Key), _ =>
                    {
                        return new ValueConverterInfo(
                            modelClrType: modelClrType,
                            providerClrType: item.Key,
                            factory: valueConverterInfo => (ValueConverter)Activator.CreateInstance(converterType, valueConverterInfo.MappingHints));
                    });
                }
            }
        }

        private static Type UnwrapNullableType(Type type)
        {
            if (type is null)
            {
                return null;
            }

            return Nullable.GetUnderlyingType(type) ?? type;
        }
    }
}
