using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace quanLyNo_BE.Controllers;
public interface IRepository<T>
{
    IEnumerable<T> Index();
    T GetById(int id);
    public void Delete(int id);
    public void Create(T entity);
    public void Update(T entity);
}
