using NZWalks.API.Models.Domain;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repositories
{
	public interface IWalkRepository
	{
		public Task<Walk> CreateAsync(Walk walk);
		public Task<List<Walk>> GetAllAsync(string? FilterOn = null, string? FilterQuery = null , string? SortedBy = null , bool IsAscending =true );
		public Task<Walk> GetByIdASync(Guid id);
		public Task<Walk> UpdateAsync( Guid id , Walk walk);
		public Task<Walk> DeleteAsync( Guid id);
		
		
	}
}
