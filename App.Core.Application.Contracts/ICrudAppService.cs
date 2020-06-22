using System.Collections.Generic;
using System.Threading.Tasks;
using App.Core.Data;
using App.Core.Entities;

namespace App.Core.Application.Contracts
{
    public interface ICrudAppService<TGetOutputDto, TGetListOutputDto,in TKey,in TGetListInput,in TCreateInput,in TUpdateInput>
        where TGetOutputDto : IEntityDto<TKey>
        where TGetListOutputDto : IEntityDto<TKey>
    {
        Task<PagedResultDto<TGetListOutputDto>> GetListAsync(TGetListInput input);

        Task<TGetOutputDto> GetAsync(TKey id);

        Task<TGetOutputDto> CreateAsync(TCreateInput createInput);

        Task<TGetOutputDto> UpdateAsync(TKey id, TUpdateInput updateInput);

        Task DeleteAsync(TKey id);
    }
    
    
}