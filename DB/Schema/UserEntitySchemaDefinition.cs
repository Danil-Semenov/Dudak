using DB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB.Schema
{
    public class UserEntitySchemaDefinition : IEntityTypeConfiguration<User> 
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("person", "dbo");
            builder.Property(p => p.Id)
                .HasColumnName("id")
                .HasColumnType("SERIAL");
            builder.Property(p => p.Login)
                .HasColumnName("login")
                .HasMaxLength(30)
                .IsUnicode(false);
            builder.Property(p => p.Password)
                .HasColumnName("password")
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.HasKey(k=> k.Id);
            //builder.HasAlternateKey(k => k.Login);
        }
    }
}
