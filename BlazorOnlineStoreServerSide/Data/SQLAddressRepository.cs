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
    public class SQLAddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SQLAddressRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Address> Add(Address newEntity)
        {
            var address = await _context.Addresses.AddAsync(newEntity);
            await _context.SaveChangesAsync();

            return address.Entity;
        }

        public async Task<Address> Delete(int id)
        {
            var addressToDeplete = await _context.Addresses.FindAsync(id);
            if (addressToDeplete != null)
            {
                _context.Addresses.Remove(addressToDeplete);
                await _context.SaveChangesAsync();
            }

            return addressToDeplete;
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Address> GetById(int id)
        {
            return await _context.Addresses.FindAsync(id);
        }

        public async Task<IPagedList<Address>> GetPagedList(Pagination pagination)
        {
            return await _context.Addresses.ToPagedListAsync(pagination.PageNumber, pagination.PageSize);
        }

        public async Task<IEnumerable<Address>> Search(string searchKey)
        {
            if (string.IsNullOrWhiteSpace(searchKey))
            {
                return await _context.Addresses.ToListAsync();
            }
            return await _context.Addresses.Where(da => da.City.Contains(searchKey) ||
                         da.Country.Contains(searchKey) || da.PostCode.Contains(searchKey) ||
                         da.State.Contains(searchKey) || da.Street.Contains(searchKey)).ToListAsync();
        }

        public async Task<Address> Update(Address updatedEntity)
        {
            var result = await _context.Addresses.FirstOrDefaultAsync(da => da.AddressID 
                               == updatedEntity.AddressID);

            _mapper.Map(updatedEntity, result);

            await _context.SaveChangesAsync();

            return result;

        }
    }
}
