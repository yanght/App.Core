using App.Core.Application.Contracts.LinCms;
using App.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Application.LinCms
{
    public class BookService : IBookService
    {
        private IBookRepository _repo;
        public BookService(IBookRepository repo)
        {
            _repo = repo;
        }

        public object GetBooks()
        {
            return _repo.GetBooks();
        }
    }
}
