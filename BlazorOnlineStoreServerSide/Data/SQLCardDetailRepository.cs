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
    public class SQLCardDetailRepository : ICardDetailRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SQLCardDetailRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CardDetail> Add(CardDetail newEntity)
        {
            var cardDetail = await _context.CardDetails.AddAsync(newEntity);
            await _context.SaveChangesAsync();

            return cardDetail.Entity;
        }

        public async Task<CardDetail> Delete(int id)
        {
            var cardDetailToDelete = await _context.CardDetails.FindAsync(id);
            if (cardDetailToDelete != null)
            {
                _context.CardDetails.Remove(cardDetailToDelete);
                await _context.SaveChangesAsync();
            }
            return cardDetailToDelete;
        }

        public async Task<IEnumerable<CardDetail>> GetAll()
        {
            return await _context.CardDetails.Include(cdd => cdd.BillingAddress)
                         .ToListAsync();
        }

        public async Task<CardDetail> GetById(int id)
        {
            return await _context.CardDetails.Include(cdd => cdd.BillingAddress)
                         .FirstOrDefaultAsync(cdd => cdd.CardDetailID == id);
        }

        public async Task<IPagedList<CardDetail>> GetPagedList(Pagination pagination)
        {
            return await _context.CardDetails.Include(cdd => cdd.BillingAddress)
                         .ToPagedListAsync(pagination.PageNumber, pagination.PageSize);
        }

        public async Task<IEnumerable<CardDetail>> Search(string searchKey)
        {
            if (string.IsNullOrWhiteSpace(searchKey))
            {
                return await _context.CardDetails.Include(cdd => cdd.BillingAddress)
                         .ToListAsync();
            }
            return await _context.CardDetails.Include(cdd => cdd.BillingAddress)
                         .Where(cdd => cdd.CardNumber.Contains(searchKey) ||
                         cdd.CardSecurityNumber.Contains(searchKey) ||
                         cdd.NameOnCard.Contains(searchKey) || cdd.BillingAddress.Street.Contains(searchKey) ||
                         cdd.BillingAddress.State.Contains(searchKey) || cdd.BillingAddress.City.Contains(searchKey) ||
                         cdd.BillingAddress.Country.Contains(searchKey) || cdd.BillingAddress.PostCode.Contains(searchKey)).ToListAsync();

        }

        public async Task<CardDetail> Update(CardDetail updatedEntity)
        {
             var result = await _context.CardDetails.FirstOrDefaultAsync(cdd =>
                                cdd.CardDetailID == updatedEntity.CardDetailID);

            _mapper.Map(updatedEntity, result);

            result.BillingAddress = updatedEntity.BillingAddress;

            await _context.SaveChangesAsync();

            return result;

        }

    }
}
