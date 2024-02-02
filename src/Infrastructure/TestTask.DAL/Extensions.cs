using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace TestTask.DAL;

internal static class Extensions
{
	public static void ConfigureEnumPropertyAsString<TEntity, TEnum>(this EntityTypeBuilder<TEntity> typeBuilder, Expression<Func<TEntity, TEnum>> expression) 
		where TEnum : struct
		where TEntity : class
	{
		typeBuilder.Property(expression)
			.HasConversion(e => e.ToString(), i => Enum.Parse<TEnum>(i!));
	}

}