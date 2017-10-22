using System;
using System.Collections.Generic;
using System.Text;
using SpyStore.DAL.EF;
using SpyStore.Models.Entities;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SpyStore.DAL.Tests.ContextTests
{
    [Collection("SpyStore.DAL")]
    public class CategoryTests : IDisposable
    {
        #region Properties

        private readonly StoreContext _db;

        #endregion Properties

        #region Constructor

        public CategoryTests()
        {
            _db = new StoreContext();
            CleanDatabase();
        }

        #endregion Constructor

        #region Interface Methods

        public void Dispose()
        {
            CleanDatabase();
            _db.Dispose();
        }
        
        #endregion Interface Methods

        #region Private Helper Methods

        private void CleanDatabase()
        {
            _db.Database.ExecuteSqlCommand("Delete from Store.Categories");
            _db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT (\"Store.Categories\", RESEED, -1);");
        }

        #endregion Private Helper Methods

        #region Unit Tests

        [Fact]
        public void FirstTest()
        {
            Assert.True(true);
        }

        [Fact]
        public void ShouldAddCategoryWithDbSet()
        {
            var category = new Category { CategoryName = "Foo" };
            _db.Categories.Add(category);

            Assert.Equal(EntityState.Added, _db.Entry(category).State);
            Assert.True(category.Id < 0);
            Assert.Null(category.TimeStamp);

            _db.SaveChanges();

            Assert.Equal(EntityState.Unchanged, _db.Entry(category).State);
            Assert.Equal(0, category.Id);
            Assert.NotNull(category.TimeStamp);
            Assert.Equal(1, _db.Categories.Count());
        }

        [Fact]
        public void ShouldAddCategoryWithContext()
        {
            var category = new Category { CategoryName = "Foo" };
            _db.Add(category);

            Assert.Equal(EntityState.Added, _db.Entry(category).State);
            Assert.True(category.Id < 0);
            Assert.Null(category.TimeStamp);

            _db.SaveChanges();
            Assert.Equal(EntityState.Unchanged, _db.Entry(category).State);
            Assert.Equal(0, category.Id);
            Assert.NotNull(category.TimeStamp);
            Assert.Equal(1, _db.Categories.Count());
        }

        [Fact]
        public void ShouldGetAllCategoriesOrderedByName()
        {
            _db.Categories.Add(new Category { CategoryName = "Foo" });
            _db.Categories.Add(new Category { CategoryName = "Bar" });
            _db.SaveChanges();

            var categories = _db.Categories.OrderBy(c => c.CategoryName).ToList();

            Assert.Equal(2, categories.Count());
            Assert.Equal("Bar", categories[0].CategoryName);
            Assert.Equal("Foo", categories[1].CategoryName);
        }

        [Fact]
        public void ShouldUpdateCategory()
        {
            var category = new Category { CategoryName = "Foo" };

            _db.Categories.Add(category);
            _db.SaveChanges();

            category.CategoryName = "Bar";
            _db.Categories.Update(category);

            Assert.Equal(EntityState.Modified, _db.Entry(category).State);

            _db.SaveChanges();

            Assert.Equal(EntityState.Unchanged, _db.Entry(category).State);

            StoreContext context;
            using (context = new StoreContext())
            {
                Assert.Equal("Bar", context.Categories.First().CategoryName);
            }               
        }

        [Fact]
        public void ShouldNotUpdateANonAttachedCategory()
        {
            var category = new Category { CategoryName = "Foo" };

            _db.Categories.Add(category);
            category.CategoryName = "Bar";

            Assert.Throws<InvalidOperationException>(() => _db.Categories.Update(category));
        }

        [Fact]
        public void ShouldDeleteACategory()
        {
            var category = new Category { CategoryName = "Foo" };

            _db.Categories.Add(category);
            _db.SaveChanges();

            Assert.Equal(1, _db.Categories.Count());

            _db.Categories.Remove(category);
            Assert.Equal(EntityState.Deleted, _db.Entry(category).State);

            _db.SaveChanges();
            Assert.Equal(EntityState.Detached, _db.Entry(category).State);
            Assert.Equal(0, _db.Categories.Count());
        }

        [Fact]
        public void ShouldDeleteACateogryWithTimestampData()
        {
            var category = new Category { CategoryName = "Foo" };
            _db.Categories.Add(category);
            _db.SaveChanges();

            var context = new StoreContext();
            var catToDelete = new Category { Id = category.Id, TimeStamp = category.TimeStamp };
            context.Entry(catToDelete).State = EntityState.Deleted;
            var affected = context.SaveChanges();

            Assert.Equal(1, affected);
        }

        [Fact]
        public void ShouldNotDeleteACateogryWithoutTimestampData()
        {
            var category = new Category { CategoryName = "Foo" };
            _db.Categories.Add(category);
            _db.SaveChanges();

            var context = new StoreContext();
            var catToDelete = new Category { Id = category.Id };

            Assert.Throws<InvalidOperationException>(() => context.Categories.Remove(catToDelete));

            //var ex = Assert.Throws<DbUpdateConcurrencyException>(() => context.SaveChanges());
            //Assert.Equal(1, ex.Entries.Count);
            //Assert.Equal(category.Id, ((Category)ex.Entries[0].Entity).Id);
        }

        #endregion Unit Tests
    }
}
