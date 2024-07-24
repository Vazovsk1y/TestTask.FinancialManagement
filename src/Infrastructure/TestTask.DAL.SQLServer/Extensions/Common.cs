using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Domain.Common;

namespace TestTask.DAL.SQLServer.Extensions;

internal static class Common
{
    public static void ConfigureId<TEntity, TId>(this EntityTypeBuilder<TEntity> typeBuilder)
        where TEntity : Entity<TId>
        where TId : IValueId<TId>
    {
        typeBuilder.HasKey(e => e.Id);

        typeBuilder.Property(e => e.Id)
                .HasConversion(
                    e => e.Value,
                    e => (TId)Activator.CreateInstance(typeof(TId), e)!
                );
    }
    public static void ConfigureEnumPropertyAsString<TEntity, TEnum>(this EntityTypeBuilder<TEntity> typeBuilder, Expression<Func<TEntity, TEnum>> expression) 
		where TEnum : struct
		where TEntity : class
	{
		typeBuilder.Property(expression)
			.HasConversion(e => e.ToString(), i => Enum.Parse<TEnum>(i!));
	}

}