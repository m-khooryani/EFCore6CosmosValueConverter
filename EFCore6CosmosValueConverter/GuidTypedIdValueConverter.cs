using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCore6CosmosValueConverter
{
    class GuidTypedIdValueConverter<TTypedIdValue> : ValueConverter<TTypedIdValue, string>
        where TTypedIdValue : TypedId<Guid>
    {
        public GuidTypedIdValueConverter(ConverterMappingHints mappingHints = null)
            : base(id => id.Value.ToString(), value => Create(Guid.Parse(value)),
                  mappingHints)
        {
        }

        private static TTypedIdValue Create(Guid id)
        {
            return Activator.CreateInstance(typeof(TTypedIdValue), id) as TTypedIdValue;
        }
    }
}
