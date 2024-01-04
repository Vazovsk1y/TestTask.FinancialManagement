using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;
using TestTask.Domain.Common;

namespace TestTask.DAL;

internal static class Extensions
{
	public static void ConfigureNullableValueIdProperty<TEntity, TId>(this EntityTypeBuilder<TEntity> typeBuilder, Expression<Func<TEntity, TId?>> expression)
		where TEntity : class
		where TId : class, IValueId<TId>
	{
		typeBuilder.Property(expression).IsRequired(false);

		typeBuilder.Property(expression).HasConversion(
			e => e != null ? e.Value : (Guid?)null, 
			i => i != null ? (TId)Activator.CreateInstance(typeof(TId), i)! : null);
	}

	public static void ConfigureValueIdProperty<TEntity, TId>(this EntityTypeBuilder<TEntity> typeBuilder, Expression<Func<TEntity, TId>> expression)
		where TEntity : class
		where TId : class, IValueId<TId>
	{
		typeBuilder.Property(expression).HasConversion(
			e => e.Value,
			i => (TId)Activator.CreateInstance(typeof(TId), i)!);
	}

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