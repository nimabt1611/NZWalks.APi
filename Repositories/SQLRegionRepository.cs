using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
	public class SQLRegionRepository : IRegionRepository
	{
		private readonly NZWalksDbContext dbcontext;

		public SQLRegionRepository(NZWalksDbContext dbcontext)
        {
			this.dbcontext = dbcontext;
		}

		public async Task<List<Region>> GetAllAsync()
		{

			return await dbcontext.Regions.ToListAsync();
		}

		public async Task<Region?> GetByIdAsync(Guid id)
		{

			return await dbcontext.Regions.FirstOrDefaultAsync(c => c.Id == id);
		}
		public async Task<Region> CreateAsync(Region region)
		{
			await dbcontext.Regions.AddAsync(region);
			await dbcontext.SaveChangesAsync();
			return region;
		}

		public async Task<Region> UpdateAsync( Guid id , Region region)
		{
			var existregion = await dbcontext.Regions.FirstOrDefaultAsync(c => c.Id == id);
			if(existregion == null)
			{
				return null;
			}

			existregion.Code = region.Code;
			existregion.Name = region.Name;
			existregion.RegionIMageUrl = region.RegionIMageUrl;

			await dbcontext.SaveChangesAsync();

			return existregion;
		}

		public async Task<Region> DeleteAsync(Guid id)
		{
			var existregion = await dbcontext.Regions.FirstOrDefaultAsync(c => c.Id == id);
			if(existregion == null)
			{
				return null;
			}
			dbcontext.Regions.Remove(existregion);
			await dbcontext.SaveChangesAsync();
			return existregion;
		}
	}
}
