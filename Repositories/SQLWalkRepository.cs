using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
	public class SQLWalkRepository : IWalkRepository
	{
		private readonly NZWalksDbContext dbContext;

		public SQLWalkRepository(NZWalksDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<Walk> CreateAsync(Walk walk)
		{
			await dbContext.Walks.AddAsync(walk);
			await dbContext.SaveChangesAsync();
			return walk;

		}

		public async Task<Walk> DeleteAsync(Guid id)
		{
			var existwalks = dbContext.Walks.FirstOrDefault(c => c.Id == id);
			if(existwalks ==null)
		{
				return null;
			}
			dbContext.Walks.Remove(existwalks);
			await dbContext.SaveChangesAsync();
			return existwalks;
		}

		public async Task<List<Walk>> GetAllAsync(string? FilterOn = null, string? FilterQuery = null , string? SortedBy = null ,bool IsAscending = true)

		{
			var walks = dbContext.Walks
				.Include(c => c.Difficulty)
				.Include(c => c.Region).AsQueryable();

			if(string.IsNullOrWhiteSpace(FilterOn) == false && string.IsNullOrWhiteSpace(FilterQuery) == false)
			{
				if(FilterOn.Equals("Name" , StringComparison.OrdinalIgnoreCase))
				{
					walks = walks.Where(c => c.Name.Contains(FilterOn));
				}
			}

			if(string.IsNullOrWhiteSpace(SortedBy))
			{
				if(SortedBy.Equals("Name" , StringComparison.OrdinalIgnoreCase))
				{
					walks = IsAscending ? walks.OrderBy(c => c.Name) : walks.OrderByDescending(c => c.Name);
				}
			}


			if (string.IsNullOrWhiteSpace(SortedBy))
			{
				if (SortedBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
				{
					walks = IsAscending ? walks.OrderBy(c => c.LengtjKm) : walks.OrderByDescending(c => c.LengtjKm);
				}
			}






			return await dbContext.Walks.ToListAsync();

			
		}

		public async Task<Walk> GetByIdASync(Guid id)
		{
			return await dbContext.Walks.Include(c => c.Difficulty).Include(c => c.Region).FirstOrDefaultAsync(c=>c.Id == id);	
		}

		public async Task<Walk> UpdateAsync(Guid id , Walk walk)
		{
			var existWalk = await dbContext.Walks.FirstOrDefaultAsync(c=>c.Id==id);
			 
			if(existWalk == null)
			{
				return null;
			}
			existWalk.Name = walk.Name;
			existWalk.LengtjKm = walk.LengtjKm;
			existWalk.Discription = walk.Discription;
			existWalk.WalkImageUrl = walk.WalkImageUrl;
			existWalk.DifficultyId = walk.DifficultyId;
			existWalk.RegionId = walk.RegionId;

			await dbContext.SaveChangesAsync();

			return existWalk;
		}

		
	}
}
