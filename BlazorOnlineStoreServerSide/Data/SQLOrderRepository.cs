using AutoMapper;
using BlazorOnlineStoreServerSide.Contracts;
using BlazorOnlineStoreServerSide.Data;
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
    public class SQLOrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SQLOrderRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Order> Add(Order newEntity)
        {            
            var order = await _context.Orders.AddAsync(newEntity);
            await _context.SaveChangesAsync();

            return order.Entity;
        }

        public async Task<Order> Delete(int id)
        {
            var orderToDelete = await _context.Orders.FindAsync(id);
            if (orderToDelete != null)
            {
                _context.Orders.Remove(orderToDelete);
                await _context.SaveChangesAsync();
            }
            return orderToDelete;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Orders.Include(ord => ord.OrderLineItems)
                         .Include(ord => ord.Customer).ToListAsync();            
        }

        public async Task<Order> GetById(int id)
        {
            return await _context.Orders.Include(ord => ord.OrderLineItems)
                .Include(ord => ord.Customer).FirstOrDefaultAsync(ord => ord.OrderID == id);
        }

        public async Task<IPagedList<Order>> GetPagedList(Pagination pagination)
        {
            return await _context.Orders.ToPagedListAsync(pagination.PageNumber, pagination.PageSize);
        }

        public async Task<IEnumerable<Order>> Search(string searchKey)
        {
            if (string.IsNullOrWhiteSpace(searchKey))
            {
                return await _context.Orders.Include(ord => ord.OrderLineItems).Include(ord => ord.Customer)
                         .ToListAsync();
            }
            return await _context.Orders.Include(ord => ord.OrderLineItems).Include(ord => ord.Customer)
                         .Where(ord => ord.AdminUser.Contains(searchKey) || ord.Customer.FirstName.Contains(searchKey)
                         || ord.UniqueID.Contains(searchKey) || ord.Customer.Email.Contains(searchKey) ||
                         ord.Customer.Phone.Contains(searchKey) || ord.OrderLineItems.Any(q => q.Product.Brand.Contains(searchKey)) ||
                         ord.OrderLineItems.Any(q => q.Product.Description.Contains(searchKey)) || ord.OrderLineItems.
                         Any(q => q.Product.Name.Contains(searchKey))).ToListAsync();
        }

        public async Task<Order> Update(Order updatedEntity)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(od => od.OrderID == updatedEntity.OrderID);

            _mapper.Map(updatedEntity, result);

            await _context.SaveChangesAsync();

            return result;
        }
    }
}
