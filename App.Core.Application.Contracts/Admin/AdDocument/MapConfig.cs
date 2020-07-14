using App.Core.Application.Contracts.Amdin.AdDocument.Output;
using App.Core.Entitys.Admin;
using AutoMapper;

namespace App.Core.Application.Contracts.Admin.AdDocument
{
    /// <summary>
    /// 映射配置
    /// </summary>
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<AdDocumentModel, DocumentGetOutput>();
        }
    }
}
