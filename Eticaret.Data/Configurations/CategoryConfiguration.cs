using Eticaret.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Data.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Image).HasMaxLength(50);
            builder.HasData(
                new Category
                {
                    Name="Elektronik",
                    Id=1,
                    IsActive=true,
                    IsTopMenu=true,
                    ParentId=0,
                    OrderNo=1,
                },
                new Category
                {
                    Name="Bilgisayar",
                    Id=2,
                    IsActive=true,
                    IsTopMenu=true,
                    ParentId=0,
                    OrderNo=2,
                } // we can add different category like this one
                );
        }
    }
}
