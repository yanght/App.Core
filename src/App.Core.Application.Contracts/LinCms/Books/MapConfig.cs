using App.Core.Application.Contracts.Amdin.AdDocument.Output;
using App.Core.Application.Contracts.LinCms.Books.Output;
using App.Core.Entitys.LinCms;
using AutoMapper;

namespace App.Core.Application.Contracts.LinCms.Books
{
    /// <summary>
    /// 映射配置
    /// </summary>
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<BookModel, BookGetOutput>();
        }
    }
}
