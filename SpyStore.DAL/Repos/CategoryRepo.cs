using Microsoft.EntityFrameworkCore;
using SpyStore.DAL.EF;
using SpyStore.DAL.Repos.Base;
using SpyStore.DAL.Repos.Interfaces;
using SpyStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpyStore.DAL.Repos
{
    public class CategoryRepo : RepoBase<Category>, ICategoryRepo
    {
        #region Cosntructors

        public CategoryRepo() { }

        public CategoryRepo(DbContextOptions<StoreContext> options) : base(options) { }

        #endregion Constructors

        #region Overriden Methods

        public IEnumerable<Category> GetAllWithProducts() => Table.Include(x => x.Products).ToList();

        public Category GetOneWithProducts(int? id) => Table.Include(x => x.Products).SingleOrDefault(x => x.Id == id);

        #endregion Overriden Methods
    }
}
