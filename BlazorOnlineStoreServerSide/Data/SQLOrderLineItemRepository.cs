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
    public class SQLOrderLineItemRepository : IOrderLineItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SQLOrderLineItemRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddRange(List<OrderLineItem> newEntity)
        {
            await _context.OrderLineItems.AddRangeAsync(newEntity);            
            await _context.SaveChangesAsync();            
        }

        public async Task<OrderLineItem> Add(OrderLineItem newEntity)
        {
            var orderLineItem = await _context.OrderLineItems.AddAsync(newEntity);
            await _context.SaveChangesAsync();

            return orderLineItem.Entity;
        }

        public async Task<OrderLineItem> Delete(int id)
        {
            var orderLineItemToDelete = await _context.OrderLineItems.FindAsync(id);
            if (orderLineItemToDelete != null)
            {
                _context.OrderLineItems.Remove(orderLineItemToDelete);
                await _context.SaveChangesAsync();
            }

            return orderLineItemToDelete;
        }

        public async Task<IEnumerable<OrderLineItem>> GetAll()
        {
            return await _context.OrderLineItems.Include(ordl => ordl.Product)
                .Include(ordl => ordl.Order).ToListAsync();           
        }

        public async Task<OrderLineItem> GetById(int id)
        {
            return await _context.OrderLineItems.Include(ordl => ordl.Product)
                    .Include(ordl => ordl.Order).FirstOrDefaultAsync(ordl => ordl.OrderID == id);
        }

        public async Task<IEnumerable<OrderLineItem>> Search(string searchKey)
        {
            if (string.IsNullOrWhiteSpace(searchKey))
            {
                return await _context.OrderLineItems.Include(ordl => ordl.Product)
                .Include(ordl => ordl.Order).ToListAsync();
            }
            return await _context.OrderLineItems.Include(ordl => ordl.Product).Include(ordl => ordl.Order)
                .Where(ordl => ordl.Order.AdminUser.Contains(searchKey) || ordl.Order.Customer.Phone.Contains(searchKey) ||
                ordl.Order.Customer.FirstName.Contains(searchKey) || ordl.Product.Brand.Contains(searchKey) ||
                ordl.Order.Customer.LastName.Contains(searchKey) || ordl.Order.Customer.Email.Contains(searchKey) ||
                ordl.Product.Description.Contains(searchKey) || ordl.Product.Name.Contains(searchKey)).ToListAsync();

        }

        public async Task<OrderLineItem> Update(OrderLineItem updatedEntity)
        {
            var result = await _context.OrderLineItems.FirstOrDefaultAsync(ordl => ordl.OrderLineItemID == updatedEntity.OrderLineItemID);

            _mapper.Map(updatedEntity, result);

            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<IPagedList<OrderLineItem>> GetPagedList(Pagination pagination)
        {
            return await _context.OrderLineItems.ToPagedListAsync(pagination.PageNumber, pagination.PageSize);
        }
    }
}
