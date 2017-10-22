using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SpyStore.DAL.EF;
using SpyStore.DAL.Repos.Base;
using SpyStore.Models.Entities;

namespace SpyStore.DAL.Repos
{
    public class CategoryRepo : RepoBase<Category>
    {
        #region Cosntructors

        public CategoryRepo() { }

        public CategoryRepo(DbContextOptions<StoreContext> options) : base(options) { }

        #endregion Constructors

        #region Overriden Methods

        public override IEnumerable<Category> GetAll()
        {
            return Table.OrderBy(x => x.CategoryName);
        }

        public override IEnumerable<Category> GetRange(int skip, int take)
        {
            return GetRange(Table.OrderBy(x => x.CategoryName), skip, take);
        }

        #endregion Overriden Methods
    }
}
