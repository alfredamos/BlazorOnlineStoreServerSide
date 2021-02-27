using AutoMapper;
using BlazorOnlineStoreServerSide.Contracts;
using BlazorOnlineStoreServerSide.Pagin;
using BlazorOnlineStoreServerSide.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace BlazorOnlineStoreServerSide.Data
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SQLProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Product> Add(Product newEntity)
        {
            var product = await _context.Products.AddAsync(newEntity);
            await _context.SaveChangesAsync();

            return product.Entity;
        }

        public async Task<Product> Delete(int id)
        {
            var productToDelete = await _context.Products.FindAsync(id);

            if (productToDelete != null)
            {
                _context.Products.Remove(productToDelete);
                await _context.SaveChangesAsync();
            }
            return productToDelete;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IPagedList<Product>> GetPagedList(Pagination pagination)
        {
            return await _context.Products.ToPagedListAsync(pagination.PageNumber, pagination.PageSize);
        }

        public async Task<IEnumerable<Product>> Search(string searchKey)
        {
            if (string.IsNullOrWhiteSpace(searchKey))
            {
                return await _context.Products.ToListAsync(); 
            }
            return await _context.Products.Where(prod => prod.Brand.Contains(searchKey) ||
                        prod.Description.Contains(searchKey) || prod.Name.Contains(searchKey) ||
                        prod.Description.Contains(searchKey)).ToListAsync();

        }

        public async Task<Product> Update(Product updatedEntity)
        {
            var result = await _context.Products.FirstOrDefaultAsync(pd => pd.ProductID == updatedEntity.ProductID);

            _mapper.Map(updatedEntity, result);
            
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
