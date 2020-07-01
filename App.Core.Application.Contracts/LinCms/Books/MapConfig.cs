using App.Core.Application.Contracts.LinCms.Books.Output;
using App.Core.Entitys.LinCms;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

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
