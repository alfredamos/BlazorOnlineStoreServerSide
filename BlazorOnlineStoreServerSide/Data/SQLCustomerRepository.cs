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
    public class SQLCustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SQLCustomerRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Customer> Add(Customer newEntity)
        {
            var customer = await _context.Customers.AddAsync(newEntity);
            //_context.Database.EnsureCreated();
            await _context.SaveChangesAsync();

            return customer.Entity;
        }

        public async Task<Customer> Delete(int id)
        {
            var customerToDelete = await _context.Customers.FindAsync(id);

            if (customerToDelete != null)
            {
                _context.Customers.Remove(customerToDelete);
                await _context.SaveChangesAsync();
            }

            return customerToDelete;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.AsSplitQuery().Include(cst => cst.Addresses)
                         .Include(cus => cus.CardDetails).ToListAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _context.Customers.AsSplitQuery().Include(cst => cst.Addresses)
                         .Include(cus => cus.CardDetails)
                         .FirstOrDefaultAsync(cst => cst.CustomerID == id);
        }

        public async Task<IPagedList<Customer>> GetPagedList(Pagination pagination)
        {
            return await _context.Customers.AsSplitQuery().Include(cus => cus.Addresses)
                         .Include(cus => cus.CardDetails)
                         .ToPagedListAsync(pagination.PageNumber, pagination.PageSize);
        }

        public async Task<IEnumerable<Customer>> Search(string searchKey)
        {
            return await _context.Customers.AsSplitQuery().Include(cus => cus.Addresses)
                         .Include(cus => cus.CardDetails)
                         .Where(cs => cs.FirstName.Contains(searchKey) ||
                         cs.LastName.Contains(searchKey) || cs.Email.Contains(searchKey) ||
                         cs.Phone.Contains(searchKey) || cs.Addresses.Any(q => q.City.Contains(searchKey)) ||
                         cs.Addresses.Any(q => q.Street.Contains(searchKey)) || cs.Addresses.Any(q => q.State.Contains(searchKey))
                         || cs.Addresses.Any(q => q.Country.Contains(searchKey) || cs.Addresses.Any(q => q.PostCode.Contains(searchKey))) ||
                         cs.CardDetails.Any(q => q.NameOnCard.Contains(searchKey))).ToListAsync();
        }

        public async Task<Customer> Update(Customer updatedEntity)
        {
            var result = await _context.Customers.FirstOrDefaultAsync(cs => cs.CustomerID 
                       == updatedEntity.CustomerID);

            _mapper.Map(updatedEntity, result);

            result.Addresses = updatedEntity.Addresses;
            result.CardDetails = updatedEntity.CardDetails;

            await _context.SaveChangesAsync();

            return result;
        }
    }
}
